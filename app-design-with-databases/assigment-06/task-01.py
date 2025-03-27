from pymongo import MongoClient

client = MongoClient("mongodb://127.0.0.1:27017")

print(client.list_database_names())

db = client["library"]
books_collection = db["books"]
books_collection.delete_many({})

books = [
    {"title": "Book A", "author": "Franciszek Przeliorz", "year": 2024},
    {"title": "Book B", "author": "Steve Jobs", "year": 2004},
     {"title": "Book C", "author": "Bill Gates", "year": 1998}
]
books_collection.insert_many(books)

for book in books_collection.find():
    print(book)

client.close()
