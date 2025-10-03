# Script de PowerShell para ejecutar la migración de PostgreSQL
# Asegúrate de tener PostgreSQL instalado y en el PATH

param(
    [string]$Host = "localhost",
    [int]$Port = 5432,
    [string]$Database = "test",
    [string]$Username = "postgres",
    [string]$Password = "postgres"
)

$scriptPath = Join-Path $PSScriptRoot "001_CreateClaimsTables_PostgreSQL.sql"

Write-Host "Ejecutando migración de PostgreSQL..." -ForegroundColor Green
Write-Host "Host: $Host" -ForegroundColor Yellow
Write-Host "Puerto: $Port" -ForegroundColor Yellow
Write-Host "Base de datos: $Database" -ForegroundColor Yellow
Write-Host "Usuario: $Username" -ForegroundColor Yellow
Write-Host "Script: $scriptPath" -ForegroundColor Yellow

# Verificar que el archivo existe
if (-not (Test-Path $scriptPath)) {
    Write-Error "El archivo de script no existe: $scriptPath"
    exit 1
}

# Ejecutar el script
try {
    $env:PGPASSWORD = $Password
    & psql -h $Host -p $Port -U $Username -d $Database -f $scriptPath
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "¡Migración ejecutada exitosamente!" -ForegroundColor Green
    } else {
        Write-Error "Error al ejecutar la migración. Código de salida: $LASTEXITCODE"
    }
} catch {
    Write-Error "Error al ejecutar psql: $($_.Exception.Message)"
    Write-Host "Asegúrate de que PostgreSQL esté instalado y psql esté en el PATH" -ForegroundColor Red
} finally {
    Remove-Item Env:PGPASSWORD -ErrorAction SilentlyContinue
}


