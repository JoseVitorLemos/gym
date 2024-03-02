# CleanArchitecture-Structure

1. Presentation has the reference DependencyInversion and Services 
2. DependencyInversion has the reference Domain, Data, Service and Business
3. Service has the reference Domain and Business
4. Business has the reference Domain
5. Data has the reference domain
6. Domain has no reference 

</br>
</br>

![clean architecture](https://github.com/JoseVitorLemos/CleanArchitecture-Structure/assets/50563095/451aed5d-ec8e-4130-8c92-0cb42e1d875d)
<p>source: Clean Architecture: A Craftsman's Guide to Software Structure and Design</p>

</br>

<h1>Utils</h1>
<h3>Create projects</h3>

1. dotnet new classlib --name Clean.Arch.Presentatiom
2. dotnet new classlib --name Clean.Arch.Services
3. dotnet new classlib --name Clean.Arch.Domain
4. dotnet new classlib --name Clean.Arch.DependencyInversion
5. dotnet new classlib --name Clean.Arch.Data
6. dotnet new xunit --name Clean.Arch.Tests
7. dotnet new classlib --name Clean.Arch.Helpers
8. dotnet new classlib --name Clean.Arch.Business
9. dotnet new classlib --name Clean.Arch.Infrastructure

<h3>Create global solution</h3>

1. dotnet new sln --name CleanArchitecture 
2. dotnet sln CleanArchitecture.sln add Clean.Arch.Presentation/Clean.Arch.Presentation.csproj
3. dotnet sln CleanArchitecture.sln add Clean.Arch.Services/Clean.Arch.Services.csproj
4. dotnet sln CleanArchitecture.sln add Clean.Arch.Domain/Clean.Arch.Domain.csproj
5. dotnet sln CleanArchitecture.sln add Clean.Arch.DependencyInversion/Clean.Arch.DependencyInversion.csproj
6. dotnet sln CleanArchitecture.sln add Clean.Arch.Data/Clean.Arch.Data.csproj
7. dotnet sln CleanArchitecture.sln add Clean.Arch.Tests/Clean.Arch.Tests.csproj
8. dotnet sln CleanArchitecture.sln add Clean.Arch.Helpers/Clean.Arch.Helpers.csproj
8. dotnet sln CleanArchitecture.sln add Clean.Arch.Business/Clean.Arch.Business.csproj
9. dotnet sln CleanArchitecture.sln add Clean.Arch.Infrastructure/Clean.Arch.Infrastructure.csproj

<h3>Structure</h3>
<h4>Presentation </h4>
dotnet add Clean.Arch.Presentation/Clean.Arch.Presentation.csproj reference Clean.Arch.DependencyInversion/Clean.Arch.DependencyInversion.csproj
dotnet add Clean.Arch.Presentation/Clean.Arch.Presentation.csproj reference Clean.Arch.Services/Clean.Arch.Services.csproj

<h4>Infra Dependency Inversion</h4>
dotnet add Clean.Arch.DependencyInversion/Clean.Arch.DependencyInversion.csproj reference Clean.Arch.Domain/Clean.Arch.Domain.csproj
dotnet add Clean.Arch.DependencyInversion/Clean.Arch.DependencyInversion.csproj reference Clean.Arch.Data/Clean.Arch.Data.csproj
dotnet add Clean.Arch.DependencyInversion/Clean.Arch.DependencyInversion.csproj reference Clean.Arch.Services/Clean.Arch.Services.csproj
dotnet add Clean.Arch.DependencyInversion/Clean.Arch.DependencyInversion.csproj reference Clean.Arch.Business/Clean.Arch.Business.csproj
dotnet add Clean.Arch.DependencyInversion/Clean.Arch.DependencyInversion.csproj reference Clean.Arch.Infrastructure/Clean.Arch.Infrastructure.csproj

<h4>Services</h4>
dotnet add Clean.Arch.Services/Clean.Arch.Services.csproj reference Clean.Arch.Business/Clean.Arch.Business.csproj
dotnet add Clean.Arch.Services/Clean.Arch.Services.csproj reference Clean.Arch.Domain/Clean.Arch.Domain.csproj

<h4>Business</h4>
dotnet add Clean.Arch.Business/Clean.Arch.Business.csproj reference Clean.Arch.Domain/Clean.Arch.Domain.csproj
dotnet add Clean.Arch.Business/Clean.Arch.Business.csproj reference Clean.Arch.Infrastructure/Clean.Arch.Infrastructure.csproj

<h4>Data</h4>
dotnet add Clean.Arch.Data/Clean.Arch.Data.csproj reference Clean.Arch.Domain/Clean.Arch.Domain.csproj

<h3>Create migrations, update and remove</h3>
dotnet ef migrations add MIGRATION_NAME --project Clean.Arch.Data -s Clean.Arch.Presentation -c DataContext --verbose

dotnet ef migrations remove --project Clean.Arch.Data -s Clean.Arch.Presentation -c DataContext --verbose

dotnet ef database update --project Clean.Arch.Data -s Clean.Arch.Presentation -c DataContext --verbose
