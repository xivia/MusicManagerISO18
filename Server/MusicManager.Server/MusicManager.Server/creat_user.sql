CREATE USER "music_manager"@"localhost" IDENTIFIED BY "1337";
CREATE DATABASE MusicManager;
GRANT ALL ON MusicManager.* TO "music_manager"@"localhost";