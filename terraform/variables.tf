variable "environment" {
    type = string
    default = "dev"
}

variable "resource_group_name" {
    type = string
    default = "tekken-aks"
}

variable "location" {
    type = string
    default = "West Europe"
}

variable "default_os" {
    type = string
    default = "linux"
}

variable "ssh_public_key" {
    default = "../.ssh/azure-aks.pub"
    description = "SSH Public Key for Linux k8s worker nodes"
}