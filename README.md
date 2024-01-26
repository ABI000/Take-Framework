# Take-Framework

## 本地环境

- [Installation environment](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0)

## 如何运行

### Visual Studio Code

- 安装插件C# Dev Kit
- F5
- <http://localhost:5189/swagger/index.html>

### visual studio studio 2022

- 将src文件夹下的BrideWell.Api项目设置为启动项
- F5

## 分支规范

主要分支是*develop*

分支命名约定：请使用小写，使用 - 代替空格

## 常用命令

- 部署到Azure

在tool文件夹中使用它

```bash
./push.sh -r <%REPOSITORY%> -t <%Image TAG%> -u <%arc UserName%> -p <%arc PassWord%> -path <%项目名称.web路径%>
```

Example

```bash
./push.sh -r Sample.azurecr.io/Sample -t dev -u <%UserName%> -p <%PassWord%> -path ../../src/Sample/Sample.web
```

- Add DB Migrations

在<%项目名称%>.EntityFrameworkCore项目下使用

```bash
dotnet ef --startup-project ../<%项目名称%>.Web migrations add <%Version%> --context SampleContext
```

Example

```bash
dotnet ef --startup-project ../Sample.Web migrations add v2.15 --context SampleContext
```

- Update db

在<%项目名称%>.EntityFrameworkCore项目下使用

```bash
dotnet ef database update  --configuration <%Configuration%> --startup-project ../Sample.Web
```

Example

```bash
dotnet ef database update  --configuration Debug --startup-project ../Sample.Web
```
