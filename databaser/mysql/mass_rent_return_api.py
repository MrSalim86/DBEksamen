import argparse
import random
import requests
import pandas as pd
import urllib3
import time

# Ignore SSL warnings for localhost
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

parser = argparse.ArgumentParser(description="Mass rent/return book tester")
parser.add_argument('--count', type=int, default=1000, help='Number of rent/return cycles')
parser.add_argument('--users', type=str, required=True, help='CSV file with user IDs')
parser.add_argument('--books', type=str, required=True, help='CSV file with book IDs')
args = parser.parse_args()

# Load CSV data
users_df = pd.read_csv(args.users)
books_df = pd.read_csv(args.books)

user_ids = users_df.iloc[:, 0].tolist()
book_ids = books_df.iloc[:, 0].tolist()

comments = [
    "Amazing book!", "Very boring", "Super spændende bog!", "Not bad",
    "Couldn’t put it down", "Too long", "Perfect for weekend", "Bad book",
    "Loved every chapter", "Would not recommend"
]

for i in range(args.count):
    user_id = random.choice(user_ids)
    book_id = random.choice(book_ids)
    rating = random.randint(1, 5)
    comment = random.choice(comments)

    rent_data = {
        "BookId": book_id,
        "UserId": user_id
    }

    return_data = {
        "bookId": book_id,
        "userId": user_id,
        "rating": rating,
        "comment": comment
    }

    try:
        rent_response = requests.post(
            "https://localhost:7154/api/Rent/abook",
            json=rent_data,
            verify=False
        )
        print(f"[{i+1}] RENT: Book {book_id} by {user_id} - Status: {rent_response.status_code}")

        return_response = requests.post(
            "https://localhost:7154/api/Rent/return",
            json=return_data,
            verify=False
        )
        print(f"[{i+1}] RETURN: Book {book_id} by {user_id} - Status: {return_response.status_code}")

    except Exception as e:
        print(f"[{i+1}] ERROR: {str(e)}")

    time.sleep(0.01)  # Optional delay to reduce server stress
