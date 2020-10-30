-- Download and Install SQL Server 2019 https://go.microsoft.com/fwlink/?linkid=866662
CREATE LOGIN music_manager WITH PASSWORD = '1337';
CREATE DATABASE MusicManager;
EXEC sp_addrolemember N'db_owner', N'music_manager';