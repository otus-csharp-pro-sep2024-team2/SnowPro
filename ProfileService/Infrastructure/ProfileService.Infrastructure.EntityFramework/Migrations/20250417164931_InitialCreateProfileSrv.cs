using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProfileService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateProfileSrv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LevelsTraining",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelsTraining", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesSportEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesSportEquipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileType = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    PhotoId = table.Column<Guid>(type: "uuid", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    TelegramName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    VersionNumber = table.Column<int>(type: "integer", nullable: false),
                    IsCurrentVersion = table.Column<bool>(type: "boolean", nullable: false),
                    OwnerProfileInfoId = table.Column<Guid>(type: "uuid", nullable: true),
                    PositionId = table.Column<int>(type: "integer", nullable: true),
                    HireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateDismissal = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExperienceBeforeHiring = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profiles_Profiles_OwnerProfileInfoId",
                        column: x => x.OwnerProfileInfoId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ProfileInfoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_Profiles_ProfileInfoId",
                        column: x => x.ProfileInfoId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypesSportEquipmentProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeSportEquipmentId = table.Column<int>(type: "integer", nullable: true),
                    LevelTrainingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesSportEquipmentProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypesSportEquipmentProfiles_LevelsTraining_LevelTrainingId",
                        column: x => x.LevelTrainingId,
                        principalTable: "LevelsTraining",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypesSportEquipmentProfiles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypesSportEquipmentProfiles_TypesSportEquipment_TypeSportEq~",
                        column: x => x.TypeSportEquipmentId,
                        principalTable: "TypesSportEquipment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FilesAchievement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    AchievementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesAchievement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilesAchievement_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_ProfileInfoId",
                table: "Achievements",
                column: "ProfileInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_FilesAchievement_AchievementId",
                table: "FilesAchievement",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_OwnerProfileInfoId",
                table: "Profiles",
                column: "OwnerProfileInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PositionId",
                table: "Profiles",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ProfileType_IsCurrentVersion",
                table: "Profiles",
                columns: new[] { "ProfileType", "IsCurrentVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_TypesSportEquipmentProfiles_LevelTrainingId",
                table: "TypesSportEquipmentProfiles",
                column: "LevelTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesSportEquipmentProfiles_ProfileId",
                table: "TypesSportEquipmentProfiles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesSportEquipmentProfiles_TypeSportEquipmentId",
                table: "TypesSportEquipmentProfiles",
                column: "TypeSportEquipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesAchievement");

            migrationBuilder.DropTable(
                name: "TypesSportEquipmentProfiles");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "LevelsTraining");

            migrationBuilder.DropTable(
                name: "TypesSportEquipment");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
