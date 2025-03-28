import os
import re
import shutil
import subprocess
from pathlib import Path

root_dir = Path(__file__).resolve().parent
shared_proj = root_dir / "SnowPro.Shared" / "src" / "SnowPro.Shared" / "SnowPro.Shared.csproj"
nuget_dir = root_dir / "nuget-packages"

print("Starting build and publish process...")
print(root_dir)

def bump_version(csproj_path):
    content = csproj_path.read_text(encoding="utf-8")
    match = re.search(r"<Version>(\d+)\.(\d+)\.(\d+)</Version>", content)
    if not match:
        raise Exception("Version tag not found in csproj.")

    major, minor, patch = map(int, match.groups())
    patch += 1
    new_version = f"{major}.{minor}.{patch}"
    new_content = re.sub(r"<Version>.*?</Version>", f"<Version>{new_version}</Version>", content)
    csproj_path.write_text(new_content, encoding="utf-8")
    print(f"Version updated to: {new_version}")
    return new_version

def run_cmd(cmd, cwd=None):
    result = subprocess.run(cmd, cwd=cwd, shell=True, capture_output=True, text=True)
    print(result.stdout)
    if result.returncode != 0:
        print(result.stderr)
        raise Exception(f"Command failed: {cmd}")
    return result.stdout

def clean_and_restore(project_path):
    print(f"clean_and_restore: {project_path}")
    for folder in ["bin", "obj"]:
        full_path = project_path / folder
        if full_path.exists():
            shutil.rmtree(full_path)
    project = str(project_path.relative_to(root_dir))
    cmd = f"dotnet restore --no-cache {project}"
    print(f"RESTORE: {cmd}")
    run_cmd(cmd, cwd=root_dir)

new_version = bump_version(shared_proj)

run_cmd("dotnet restore", cwd=shared_proj.parent)
run_cmd("dotnet build -c Release", cwd=shared_proj.parent)
run_cmd("dotnet pack -c Release", cwd=shared_proj.parent)

nupkg_name = f"SnowPro.Shared.{new_version}.nupkg"
src_nupkg = shared_proj.parent / "bin" / "Release" / nupkg_name
dst_nupkg = nuget_dir / nupkg_name
dst_nupkg.parent.mkdir(parents=True, exist_ok=True)
shutil.copy2(src_nupkg, dst_nupkg)
print(f"Copied {nupkg_name} to nuget-packages")

for csproj_path in root_dir.rglob("*.csproj"):
    if csproj_path.name == "SnowPro.Shared.csproj":
        continue

    text = csproj_path.read_text(encoding="utf-8")

    # Есть ли ссылка на SnowPro.Shared?
    if "PackageReference Include=\"SnowPro.Shared\"" in text:
        updated = re.sub(r'<PackageReference Include="SnowPro\.Shared" Version=".*?" />',
                         f'<PackageReference Include="SnowPro.Shared" Version="{new_version}" />',
                         text)
    else:
        # Добавляем блок с зависимостью
        insert_pos = text.rfind("</Project>")
        ref_block = (
            f'  <ItemGroup>\n'
            f'    <PackageReference Include="SnowPro.Shared" Version="{new_version}" />\n'
            f'  </ItemGroup>\n'
        )
        updated = text[:insert_pos] + ref_block + text[insert_pos:]
        print(f"Inserted PackageReference in {csproj_path}")

    csproj_path.write_text(updated, encoding="utf-8")
    print(f"Processed {csproj_path}")

    clean_and_restore(csproj_path)

print("Done.")
