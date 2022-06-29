resource "azurerm_virtual_network" "tekkenvnet" {
  name                = "tekken-vnet"
  location            = azurerm_resource_group.tekken-rg.location
  resource_group_name = azurerm_resource_group.tekken-rg.name
  address_space       = ["10.0.0.0/8"]

  tags = {
    environment = "Development"
  }
}

resource "azurerm_subnet" "aks-subnet" {
  name = "aks-subnet"
  virtual_network_name = azurerm_virtual_network.tekkenvnet.name
  resource_group_name = azurerm_resource_group.tekken-rg.name
  address_prefixes = [ "10.1.0.0/16" ]
}