#!/bin/bash
set -e

echo "⏳ Aguardando SQL Server iniciar..."

sleep 20

echo "▶ Executando script init.sql"
 /opt/mssql-tools/bin/sqlcmd \
   -S localhost \
   -U sa \
   -P "$SA_PASSWORD" \
   -i /docker/sql/init.sql

echo "✅ Banco inicializado"

tail -f /dev/null
