resource "azurerm_kubernetes_cluster" "tekken-aks" {
  name                = "${azurerm_resource_group.tekken-rg.name}-aks-cluster"
  location            = azurerm_resource_group.tekken-rg.location
  resource_group_name = azurerm_resource_group.tekken-rg.name
  dns_prefix          = "${azurerm_resource_group.tekken-rg.name}-aks-cluster"
  kubernetes_version = data.azurerm_kubernetes_service_versions.current.latest_version
  node_resource_group = "${azurerm_resource_group.tekken-rg.name}-nrg"
  azure_policy_enabled = true

  default_node_pool {
    vnet_subnet_id = azurerm_subnet.aks-subnet.id

    name       = "systempool"
    node_count = 1
    vm_size    = "Standard_D2_v2"
    orchestrator_version = data.azurerm_kubernetes_service_versions.current.latest_version
    os_disk_size_gb = 30

    type = "VirtualMachineScaleSets" #Necessary for scaling
    enable_auto_scaling = false
    min_count = null
    max_count = null

    zones = null

    node_labels = {
      "nodepool-type" = "system"
      "environment" = "${var.environment}"
      "nodepool-os" = "${var.default_os}"
      "app" = "system-apps" #used back in yaml
    }

    tags = {
      "nodepool-type" = "system"
      "environment" = "${var.environment}"
      "nodepool-os" = "${var.default_os}"
      "app" = "system-apps"
    }

  }

  linux_profile {
    admin_username = "aksadmin"
    ssh_key {
      key_data = file(var.ssh_public_key)
    }
  }

  network_profile {
    network_plugin = "azure"
  }

  identity {
    type = "SystemAssigned"
  }

  azure_active_directory_role_based_access_control {
    managed = true
    admin_group_object_ids = [ azuread_group.aks-administrators.object_id ]
    azure_rbac_enabled = true
  }

  oms_agent {
    log_analytics_workspace_id = azurerm_log_analytics_workspace.insights.id
  }

  tags = {
    Environment = var.environment
  }
}