from pymongo import MongoClient

client = MongoClient("mongodb://127.0.0.1:27017")

db = client['library']

books = db['books']
readers = db['readers']
borrowings = db['borrowings']

pipeline = [
    # Etap 1: Grupowanie po 'reader_name' (czytelnik) i zliczanie liczby książek wypożyczonych przez każdego
    {
        '$group': {
            '_id': '$reader_name',  
            'borrowed_books': { '$sum': 1 } 
        }
    },
    # Etap 2: Łączenie z kolekcją 'readers' na podstawie 'reader_name'
    {
        '$lookup': {
            'from': 'readers',  
            'localField': '_id', 
            'foreignField': 'name',  
            'as': 'reader_info'  
        }
    },
    # Etap 3: Rozwijanie tablicy 'reader_info' na osobne dokumenty
    {
        '$unwind': '$reader_info' 
    },
    # Etap 4: Projektowanie wyników - wybieramy interesujące nas pola
    {
        '$project': {
            'reader_name': '$reader_info.name',  
            'reader_email': '$reader_info.email', 
            'borrowed_books': 1
        }
    }
]

result = list(borrowings.aggregate(pipeline))

for doc in result:
    print(doc)
