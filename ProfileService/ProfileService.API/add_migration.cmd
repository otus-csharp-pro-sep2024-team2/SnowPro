dotnet ef migrations add MigrationForRemoveProfileSrv --project ..\Infrastructure\ProfileService.Infrastructure.EntityFramework
dotnet ef database update 0 --project ..\Infrastructure\ProfileService.Infrastructure.EntityFramework
dotnet ef migrations remove --project ..\Infrastructure\ProfileService.Infrastructure.EntityFramework
dotnet ef migrations remove --project ..\Infrastructure\ProfileService.Infrastructure.EntityFramework
dotnet ef migrations add InitialCreateProfileSrv --project ..\Infrastructure\ProfileService.Infrastructure.EntityFramework
dotnet ef database update --project ..\Infrastructure\ProfileService.Infrastructure.EntityFramework
pause