# Gym-Structure

1. Presentation has the reference DependencyInversion and Services 
2. DependencyInversion has the reference Domain, Data, Service and Business
3. Service has the reference Domain and Business
4. Business has the reference Domain
5. Data has the reference domain
6. Domain has no reference 

</br>
</br>

<h1>Utils</h1>
<h3>Create projects</h3>

1. dotnet new classlib --name Gym.Presentatiom
2. dotnet new classlib --name Gym.Services
3. dotnet new classlib --name Gym.Domain
4. dotnet new classlib --name Gym.DependencyInversion
5. dotnet new classlib --name Gym.Data
6. dotnet new xunit --name Gym.Tests
7. dotnet new classlib --name Gym.Helpers
8. dotnet new classlib --name Gym.Business
9. dotnet new classlib --name Gym.Infrastructure

<h3>Create global solution</h3>

1. dotnet new sln --name Gym 
2. dotnet sln Gym.sln add Gym.Presentation/Gym.Presentation.csproj
3. dotnet sln Gym.sln add Gym.Services/Gym.Services.csproj
4. dotnet sln Gym.sln add Gym.Domain/Gym.Domain.csproj
5. dotnet sln Gym.sln add Gym.DependencyInversion/Gym.DependencyInversion.csproj
6. dotnet sln Gym.sln add Gym.Data/Gym.Data.csproj
7. dotnet sln Gym.sln add Gym.Tests/Gym.Tests.csproj
8. dotnet sln Gym.sln add Gym.Helpers/Gym.Helpers.csproj
8. dotnet sln Gym.sln add Gym.Business/Gym.Business.csproj
9. dotnet sln Gym.sln add Gym.Infrastructure/Gym.Infrastructure.csproj

<h3>Structure</h3>
<h4>Presentation </h4>
dotnet add Gym.Presentation/Gym.Presentation.csproj reference Gym.DependencyInversion/Gym.DependencyInversion.csproj
dotnet add Gym.Presentation/Gym.Presentation.csproj reference Gym.Services/Gym.Services.csproj

<h4>Infra Dependency Inversion</h4>
dotnet add Gym.DependencyInversion/Gym.DependencyInversion.csproj reference Gym.Domain/Gym.Domain.csproj
dotnet add Gym.DependencyInversion/Gym.DependencyInversion.csproj reference Gym.Data/Gym.Data.csproj
dotnet add Gym.DependencyInversion/Gym.DependencyInversion.csproj reference Gym.Services/Gym.Services.csproj
dotnet add Gym.DependencyInversion/Gym.DependencyInversion.csproj reference Gym.Business/Gym.Business.csproj
dotnet add Gym.DependencyInversion/Gym.DependencyInversion.csproj reference Gym.Infrastructure/Gym.Infrastructure.csproj

<h4>Services</h4>
dotnet add Gym.Services/Gym.Services.csproj reference Gym.Business/Gym.Business.csproj
dotnet add Gym.Services/Gym.Services.csproj reference Gym.Domain/Gym.Domain.csproj

<h4>Business</h4>
dotnet add Gym.Business/Gym.Business.csproj reference Gym.Domain/Gym.Domain.csproj
dotnet add Gym.Business/Gym.Business.csproj reference Gym.Infrastructure/Gym.Infrastructure.csproj

<h4>Data</h4>
dotnet add Gym.Data/Gym.Data.csproj reference Gym.Domain/Gym.Domain.csproj

<h3>Create migrations, update and remove</h3>
dotnet ef migrations add MIGRATION_NAME --project Gym.Data -s Gym.Presentation -c DataContext --verbose

dotnet ef migrations remove --project Gym.Data -s Gym.Presentation -c DataContext --verbose

dotnet ef database update --project Gym.Data -s Gym.Presentation -c DataContext --verbose
