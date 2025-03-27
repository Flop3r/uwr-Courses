from pymongo import MongoClient

client = MongoClient("mongodb://127.0.0.1:27017")

# Dostęp do bazy danych 'library'
db = client['library']

# Usuwanie walidatora z kolekcji 'books'
db.command({
    "collMod": "books",
    "validator": {},  # Usunięcie walidatora
    "validationAction": "warn"  # Ustawienie na "warn" zapewnia, że MongoDB będzie tylko ostrzegać, ale nie blokować niezgodnych danych
})

# Usuwanie walidatora z kolekcji 'readers'
db.command({
    "collMod": "readers",
    "validator": {},  # Usunięcie walidatora
    "validationAction": "warn"  # Ustawienie na "warn"
})

# Usuwanie walidatora z kolekcji 'borrowings'
db.command({
    "collMod": "borrowings",
    "validator": {},  # Usunięcie walidatora
    "validationAction": "warn"  # Ustawienie na "warn"
})

print("Validators removed successfully.")
