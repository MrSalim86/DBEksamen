@echo off
echo Samler alle 150 SQL-filer til én samlet fil...
type NUL > combined_ratings.sql
for /L %%i in (1,1,150) do (
    setlocal enabledelayedexpansion
    set "filename=insert_ratings_part%%i.sql"
    if exist !filename! (
        echo Tilføjer !filename!...
        type !filename! >> combined_ratings.sql
    ) else (
        echo ADVARSEL: !filename! mangler!
    )
    endlocal
)
echo Færdig! Filen combined_ratings.sql er klar.
pause
