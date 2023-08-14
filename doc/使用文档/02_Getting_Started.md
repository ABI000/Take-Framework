# 快速开始


## 系统要求
- .NET Core 7.0+
- SQL Server 2017+

## 快速使用

拉取项目源码
```
git clone 
```

使用该项目需要自行编译，示例代码位于：sample文件夹下

webapi请使用sample.web

blazor请使用sample.blazor中的服务端和客户端


## 运行数据库迁移

使用webapi举例

修改sample.web.appstting.json 文件中的 DBSettings

```json
 "DBSettings": {
    "DBSettingList": [
      {
        "IsDefault": true,
        "Name": "Sample",
        "ConnectionString": "xxxx"
      }
    ]
  },

```

在vs中，设置sample.web为启动项，然后在程序包管理器控制台中，默认项目选择Sample.EntityFrameworkCore,输入一下命令生成数据库

```
 update-database
```

运行完命令后，在对应的数据库服务器上会生成相关数据库

## 运行项目

F5启动项目


