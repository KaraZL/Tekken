resource "random_string" "random-str" {
  length = 10
  special = false
  lower = true
  upper = false
}

resource "azurerm_log_analytics_workspace" "insights" {
    name = "logs-${random_string.random-str.id}"
    resource_group_name = azurerm_resource_group.tekken-rg.name
    location = azurerm_resource_group.tekken-rg.location
}