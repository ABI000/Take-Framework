using './main.bicep'

// param rgCreate = false
// param textTranslationCreate = false
// param storageAccountCreate = false
// param DbCreate = false
// param serverCreate = false
// param appServicePlanCreate = false
// param containerRegistryCreate = false
// param emailServiceCreate = false
// param emaildomainCreate = false
// param communicationServiceCreate = false
// param webAppCreate = false
// param keyVaultCreate = false
// param addProjectsecretsCreate=false


param purpose = 'prod'
param environment = 'dev'
param projectName = 'bridewell'
param location = 'East US'
param sqlAdministratorLogin = 'bw'
param sqlAdministratorLoginPassword = 'XMPqC3o#08t9Yx5s'
param jwtSecretKey = 'qwertyuioasdfghjk23413efdfd0vs0d0asdif0d9ifa0ckkab79q84'
param tgphone = '+8613517757290'
param tgappid = '27779714'
param tgApiHash = 'a38b40001d3bc589a8c771092c7563e2'
param ErrorLogRecipient='tkqamobile@gmail.com'
param emailDataLocation = 'United States'
