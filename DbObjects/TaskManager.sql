CREATE DATABASE TaskManager
COLLATE Latin1_General_100_CI_AS_SC_UTF8;

USE TaskManager;

CREATE TABLE dbo.Task (ID_Task INTEGER IDENTITY(100,50) PRIMARY KEY
                      ,Description_Task NVARCHAR(80) COLLATE Latin1_General_100_CI_AS_SC_UTF8
                      ,DT_Created DATETIME
                      ,DT_Change DATETIME NULL
                      ,NM_User_Inclusion VarChar(80))