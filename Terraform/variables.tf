variable "subscription_id" {
  type        = string
  description = "Azure subscription ID"
  default     = "3354a426-5b85-4323-9efe-9581e63d9653"
}

variable "rg_name" {
  type        = string
  description = "Resource group name"
  default     = "rg-finalproject3425"
}

variable "location" {
  type    = string
  default = "eastus2"
}

variable "environment" {
  type    = string
  default = "dev"
}

variable "storage_account_name" {
  type    = string
  default = "mediastore03"
}

variable "app_service_plan_name" {
  type    = string
  default = "edusync-plan25"
}

variable "backend_app_name" {
  type    = string
  default = "edusync-backend-api1"
}

variable "frontend_app_name" {
  type    = string
  default = "edusync-frontend03"
}

variable "sql_server_name" {
  type    = string
  default = "edusyncsqlserver25"
}

variable "sql_db_name" {
  type    = string
  default = "EduSyncDB1"
}

variable "sql_admin" {
  type    = string
  default = "sqladminedusync"
}

variable "sql_password" {
  type      = string
  sensitive = true
}

variable "sql_connection_string" {
  type      = string
  sensitive = true
}

variable "jwt_secret" {
  type      = string
  sensitive = true
}

variable "jwt_issuer" {
  type    = string
  default = "EduSync"
}

variable "jwt_audience" {
  type    = string
  default = "EduSyncUsers"
}
