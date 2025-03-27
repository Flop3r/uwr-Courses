from pymongo import MongoClient

client = MongoClient('mongodb://127.0.0.1:27017')

db = client['library']
books = db['books']

books_filtered = books.find().sort([("title", 1)]).skip(1).limit(2)
for book in books_filtered:
    print(book)

# Assuming we're filtering books where the first copy is available
books_filtered = books.find({'copies.0.available': True})
print("\nDokumenty po filtrowaniu 'copies.available' na True:")
for book in books_filtered:
    print(book)
