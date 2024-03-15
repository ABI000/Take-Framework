targetScope = 'tenant'

var spokeSubscriptionName = 'Test Sub'
resource spokeSubscription 'Microsoft.Subscription/aliases@2020-09-01' = {
  scope: tenant()
  name: guid(spokeSubscriptionName, tenant().tenantId)
  properties: {
    displayName: spokeSubscriptionName
    billingScope: '/providers/Microsoft.Billing/billingAccounts/foo:bar'
    workload: 'DevTest'
  }
}
//MS-AZR-0003P


