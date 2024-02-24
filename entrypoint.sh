#!/bin/bash
wait_time=15s

# wait for SQL Server to come up
echo importing data will start in $wait_time...
sleep $wait_time
echo executing script...

# run the scripts in /Schemas folder to create the DB and the tables
for entry in "Schemas/*.sql"
do
  echo executing $entry
  /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $SA_PASSWORD -i $entry
done