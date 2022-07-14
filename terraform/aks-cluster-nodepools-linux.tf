resource "azurerm_kubernetes_cluster_node_pool" "linux-user" {
  vnet_subnet_id = azurerm_subnet.aks-subnet.id

  name = "linuxpool"
  mode = "User"
  os_type = "Linux"

  kubernetes_cluster_id = azurerm_kubernetes_cluster.tekken-aks.id
  vm_size    = "Standard_D2_v2"
  node_count = 1
  orchestrator_version = data.azurerm_kubernetes_service_versions.current.latest_version
  os_disk_size_gb = 30
  

  node_labels = {
      "nodepool-type" = "user"
      "environment" = "${var.environment}"
      "nodepool-os" = "${var.default_os}"
      "app" = "dotnet-apps" #used back in yaml
    }

    tags = {
      "nodepool-type" = "user"
      "environment" = "${var.environment}"
      "nodepool-os" = "${var.default_os}"
      "app" = "dotnet-apps"
    }
}