resource "azurerm_virtual_network" "tekkenvnet" {
  name                = "tekken-vnet"
  location            = azurerm_resource_group.tekken-rg.location
  resource_group_name = azurerm_resource_group.tekken-rg.name
  address_space       = ["10.0.0.0/8"]

  tags = {
    environment = "Development"
  }
}