param location string
param openAiLocation string
param workloadName string = 'cuisine'

var locationShorthand = {
  eastus: 'eus'
  northeurope: 'neu'
  westeurope: 'weu'
  swedencentral: 'swe'
}

var shorthand = locationShorthand[location]

resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  name: 'asp-${workloadName}-${shorthand}'
  location: location
  sku: {
    name: 'P1v2'
    tier: 'PremiumV2'
  }
  properties: {
    reserved: true
  }
}

resource appService 'Microsoft.Web/sites@2021-02-01' = {
  name: 'app-${workloadName}-${shorthand}'
  location: location
  properties: {
    serverFarmId: appServicePlan.id
  }
}

module openAiModule 'openai.bicep' = {
  name: 'openAiModule'
  params: {
    openAiLocation: openAiLocation
    workloadName: workloadName
    locationShorthand: locationShorthand
  }
}

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: 'ai-${workloadName}-${shorthand}'
  kind: 'web'
  location: location
  properties: {
    Application_Type: 'web'
  }
}
