from pymongo import MongoClient
from datetime import datetime, timedelta

client = MongoClient("mongodb://127.0.0.1:27017")

print(client.list_database_names())

db = client["library"]
books = db['books']
books.delete_many({})

# Books
books.insert_many([
    {
        'title': 'Book A',
        'author': 'Steve Jobs',
        'copies': [
            {'copy_number': 1, 'available': True},
            {'copy_number': 2, 'available': False}
        ]
    },
    {
        'title': 'Book B',
        'author': 'Bill Gates',
        'copies': [
            {'copy_number': 1, 'available': False},
        ]
    }
])

# Readers
readers = db['readers']
readers.delete_many({})
readers.insert_many([
    {'name': 'Kacper', 'email': 'kacper@gmail.com'},
    {'name': 'Franek', 'email': 'franek@gmail.com'}
])

# Borrowings
today = datetime.now().replace(hour=0, minute=0, second=0, microsecond=0)
borrow_period = timedelta(days=14)

borrowings = db['borrowings']
borrowings.delete_many({})
borrowings.insert_many([
    {
        'book_title': 'Book A',
        'reader_name': 'Kacper',
        'borrow_date': today,
        'return_date': (today + borrow_period)
    },
    {
        'book_title': 'Book B',
        'reader_name': 'Franek',
        'borrow_date': today,
        'return_date': (today + borrow_period)
    }
])

for collection in ['books', 'readers', 'borrowings']:
    print(f"\nData from collection {collection}:")
    for doc in db[collection].find():
        print(doc)
