@secure()
param sqlpwd string
param dbname string
param sqlurl string
param sqluser string
param sqlConnectionString string = ''
var sqlConnection = sqlConnectionString == '' ? 'Server=tcp:${sqlurl},1433;Initial Catalog=${dbname};Persist Security Info=False;User ID=${sqluser};Password=${sqlpwd};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;' : '${sqlConnectionString}'

param weburi string
@secure()
param jwtSecretKey string
param jwtAlgorithm string = 'HS256'
param jwtExpires int = 1440

param tgphone string
param tgappid string
param tgApiHash string

param AzureQueueDefaultQueueName string = 'DefaultQueue'

param AzureBlobDefaultContainer string = 'DefaultContainer'

param AzureMailSender string

param TGScreenshotQueue string = 'ScreenshotQueue'
param TGScreenshotErrorQueue string = 'ScreenshotErrorQueue'
param ErrorLogRecipient string

param storageAccountName string
resource storageAccountExisting 'Microsoft.Storage/storageAccounts@2022-09-01' existing = {
  name: storageAccountName
}
param TranslationName string
resource textTranslationExisting 'Microsoft.CognitiveServices/accounts@2023-05-01' existing = {
  name: TranslationName
}

param communicationServiceName string
resource communicationServiceExisting 'Microsoft.Communication/communicationServices@2023-04-01' existing = {
  name: communicationServiceName
}

var AzureBlobConnectionString = 'DefaultEndpointsProtocol=https;AccountName=${storageAccountExisting.name};AccountKey=${storageAccountExisting.listKeys().keys[0].value};EndpointSuffix=${environment().suffixes.storage}'

var secrets = [
  {
    name: 'Serilog'
    value: '{"Serilog":{"Using":["Serilog.Sinks.MSSqlServer"],"MinimumLevel":{"Default":"Information","Override":{"Microsoft.AspNetCore":"Warning"}},"WriteTo":[{"Name":"Console"},{"Name":"File","Args":{"path":"./logs/log-.txt","rollingInterval":"Day","formatter":"Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact"}},{"Name":"MSSqlServer","Args":{"connectionString":"${sqlConnection}","outputTemplate":"[{Timestamp:hh:mm:ss tt} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}","sinkOptionsSection":{"tableName":"Logs","schemaName":"EventLogging","autoCreateSqlTable":true,"restrictedToMinimumLevel":"Information","batchPostingLimit":1000,"period":"0.00:00:30"},"restrictedToMinimumLevel":"Warning","columnOptionsSection":{"disableTriggers":true,"clusteredColumnstoreIndex":false,"primaryKeyColumnName":"Id","addStandardColumns":[],"removeStandardColumns":["MessageTemplate","Properties"],"additionalColumns":[{"ColumnName":"IP","DataType":"varchar","PropertyName":"IP","AllowNull":true},{"ColumnName":"UserName","DataType":"varchar","PropertyName":"UserName","AllowNull":true}]},"id":{"nonClusteredIndex":true},"timeStamp":{"columnName":"CreatedTime","convertToUtc":true},"logEvent":{"columnName":"LogEvent","excludeAdditionalProperties":true,"excludeStandardColumns":true},"message":{"columnName":"message"},"exception":{"columnName":"exception"}}}]}}'
  }, {
    name: 'JwtConfiguration'
    value: '{"JwtConfiguration":{"Issuer":"${weburi}","ValidateIssuer":true,"Audience":"${weburi}","ValidateAudience":true,"Expires":${jwtExpires},"SecretKey":"${jwtSecretKey}","Algorithm":"${jwtAlgorithm}"}}'
  }, {
    name: 'DBSettings'
    value: '{"DBSettings":{"DBSettingList":[{"IsDefault":true,"Name":"BrideWell","ConnectionString":"${sqlConnection}"}]}}'
  }, {
    name: 'ClientConfiguration'
    value: '{"ClientConfiguration": {"AppId": ${tgappid},"ApiHash": "${tgApiHash}","DefaultPone": "${tgphone}","SqlConnectionString": "${sqlConnection}"   } }'
  }, {
    name: 'AzureTranslationConfiguration'
    value: '{"AzureTranslationConfiguration":{"ApiKey":"${textTranslationExisting.listKeys().key1}","Region":"${textTranslationExisting.location}","Uri":"${textTranslationExisting.properties.endpoint}"}}'
  }, {
    name: 'AzureQueueConfiguration'
    value: '{"AzureQueueConfiguration": {"ConnectionString": "${AzureBlobConnectionString}","DefaultQueueName": "${AzureQueueDefaultQueueName}"}}'
  }, {
    name: 'AzureBlobConfiguration'
    value: '{"AzureBlobConfiguration":{"ConnectionString":"${AzureBlobConnectionString}","DefaultContainer":"${AzureBlobDefaultContainer}"}}'
  }, {
    name: 'TGScreenshotQueueConfiguration'
    value: '{"TGScreenshotQueue": {"DefaultQueueName": "${TGScreenshotQueue}","DefaultErroQueueName": "${TGScreenshotErrorQueue}"}}'
  }, {
    name: 'AzureMailConfiguration'
    value: '{"AzureMailConfiguration": {"ConnectionString":"${communicationServiceExisting.listKeys().primaryConnectionString}","Sender":"${AzureMailSender}","ErrorLogRecipient":"${ErrorLogRecipient}"}}'
  }
]
output secretsObj array = secrets

param keyVaultName string

module secret './keyvault/addsecrets.bicep' = {
  name: '${keyVaultName}-secret'
  params: {
    secrets: secrets
    keyVaultName: keyVaultName
  }
}
param webappName string
resource vault 'Microsoft.KeyVault/vaults@2022-11-01' existing = {
  name: keyVaultName
}

resource webapp 'Microsoft.Web/sites@2022-09-01' existing = {
  name: webappName
}
var appsetting = union(list('${webapp.id}/config/appsettings', '2022-09-01').properties, {
    AzureKeyVaultConfiguration: vault.properties.vaultUri
  })
module addappsetting 'webapp/appsettings.bicep' = {
  name: 'addappsetting'
  params: {
    properties: appsetting
    webname: webapp.name
  }
}
