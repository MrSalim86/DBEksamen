@echo off
echo Opretter database og tabeller med bruger 'Dev'...

mysql -u Dev -pDev123 < schema.sql
mysql -u Dev -pDev123 bookreview < books.sql
mysql -u Dev -pDev123 bookreview < users.sql
mysql -u Dev -pDev123 bookreview < ratings.sql

:: Tilføj rigtige inserts
mysql -u Dev -pDev123 bookreview < insert_books.sql
mysql -u Dev -pDev123 bookreview < insert_users.sql
mysql -u Dev -pDev123 bookreview < insert_ratings.sql

echo Import færdig!
pause
