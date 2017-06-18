# Deploying an Angular CLI project to Microsoft Azure

This project packages the `dist` folder of an [Angular](https://github.com/angular/angular-cli/wiki) project and serves it with [.NET Core](https://www.microsoft.com/net/core#windowscmd) suitable for deployment on Microsoft Azure.

## Precondition

```
\---projects
    +---dist-project
    |   +---.angular-cli.json
    |   \---dist
    \---deploy-angular-dotnet
        \---wwwroot
```
- The `dist-project` project should be a peer folder of this project
- Change to the root of this project
- Make sure `dist-project` has had `npm run build` appropriately

## Setup Project
```cmd
robocopy ../dist-project/dist wwwroot /MIR

dotnet restore

rem run locally 
set ASPNETCORE_ENVIRONMENT=Development && dotnet run
```
## Deploy to Azure
```cmd
az group create --name dist-project-resources --location centralus

az appservice plan create --name dist-project-plan --resource-group dist-project-resources --sku FREE

az webapp create --name dist-project --resource-group dist-project-resources --plan dist-project-plan

az webapp deployment source config-local-git --name dist-project --resource-group dist-project-resources --query url --output tsv

git remote add azure <git url from above>

git push azure master
```