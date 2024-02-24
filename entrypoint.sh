#!/bin/bash
wait_time=15s

# wait for SQL Server to come up
echo importing data will start in $wait_time...
sleep $wait_time
echo executing script...


echo executing DbObjects/setup.sql
/opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $SA_PASSWORD -i DbObjects/setup.sql