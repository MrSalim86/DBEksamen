@echo off
set COUNT=5
set USERS=users.csv
set BOOKS=books.csv

echo Checking Python and requests module...

:: Try to import requests, if it fails, install it
python -c "import requests" 2>NUL
IF ERRORLEVEL 1 (
    echo Installing 'requests' module...
    pip install requests
)

echo Starting mass rent/return script...
python mass_rent_return_api.py --count %COUNT% --users %USERS% --books %BOOKS%

pause