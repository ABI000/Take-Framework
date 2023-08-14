# 配置文件

由于功能暂时比较少，将功能的配置暂时集中在该文档，方便编写和查看。后续功能增加，将会把配置内容迁移至功能介绍中。

## Log

日志使用的是Serilog，没有做二次封装，所以这个完全可以根据Serilog的原生配置进行编写

```json

  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "./logs/log-.txt",
          "rollingInterval": "Day"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ]
  },

```
以上配置，请阅读Serilog文档。此处不再赘述。

## DB

数据库做了多库处理,允许系统同时连接多个数据库。

```json

  "DBSettings": {
    "DBSettingList": [
      {
        "IsDefault": true,
        "Name": "Sample",
        "ConnectionString": "xxxx"
      },
      {
        "Name": "Sample2",
        "ConnectionString": "xxxx"
      }
    ]
  },

```
IsDefault：是否为默认库，后期将移除，默认第一个配置为默认数据库

Name：连接名称，用于区分连接

ConnectionString：连接字符串

## Jwt配置

JWT 基本配置，未来将加入长效token处理

``` json
  "JwtConfiguration": {
    "SecretKey": "1234567890qwertyuiopasdfghjklzxcvbnm",
    "Expires": 3000
  }
```
SecretKey：公钥

Expires：token过期时间

## 本地化资源配置

将支持数据库、缓存以及文件作为本地化资源的来源

```json
  "LocalizationOptions": {
    "StorageType": "Cache",
    "Storage": "",
    "DefaultLanguageCode": "en-US"
  },
```

StorageType：本地化资源存储类型

Storage：存储的位置，仅作用于数据库和缓存

DefaultLanguageCode：默认语言编码