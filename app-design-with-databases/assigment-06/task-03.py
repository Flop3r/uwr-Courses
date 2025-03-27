from pymongo import MongoClient
from datetime import datetime, timedelta

client = MongoClient("mongodb://127.0.0.1:27017")

# Dostęp do bazy danych 'library'
db = client['library']

# Walidacja schematu dla kolekcji 'books'
book_validation = {
    "$jsonSchema": {
        "bsonType": "object",
        "required": ["title", "author", "copies"],  
        "properties": {
            "title": {
                "bsonType": "string",
                "description": "Title must be a string"
            },
            "author": {
                "bsonType": "string",
                "description": "Author must be a string"
            },
            "copies": {
                "bsonType": "array",
                "items": {
                    "bsonType": "object",
                    "required": ["copy_number", "available"],
                    "properties": {
                        "copy_number": {
                            "bsonType": "int",
                            "description": "Copy number must be an integer"
                        },
                        "available": {
                            "bsonType": "bool",
                            "description": "Available must be a boolean"
                        }
                    }
                },
                "description": "Copies must be an array of objects"
            }
        }
    }
}

# Walidacja schematu dla kolekcji 'readers'
reader_validation = {
    "$jsonSchema": {
        "bsonType": "object",
        "required": ["name", "email"],
        "properties": {
            "name": {
                "bsonType": "string",
                "description": "Name must be a string"
            },
            "email": {
                "bsonType": "string",
                "pattern": "^.+@.+\..+$",
                "description": "Email must be a valid email string"
            }
        }
    }
}

# Walidacja schematu dla kolekcji 'borrowings'
borrowing_validation = {
    "$jsonSchema": {
        "bsonType": "object",
        "required": ["book_title", "reader_name", "borrow_date", "return_date"],  
        "properties": {
            "book_title": {
                "bsonType": "string",
                "description": "Book title must be a string"
            },
            "reader_name": {
                "bsonType": "string",
                "description": "Reader name must be a string"
            },
            "borrow_date": {
                "bsonType": "date",
                "description": "Borrow date must be a date"
            },
            "return_date": {
                "bsonType": "date",
                "description": "Return date must be a date or null"
            }
        }
    }
}

# Ustawienie walidacji dla kolekcji
validation_action = 'error'
db.command({"collMod": "books", "validator": book_validation, "validationAction": validation_action})
db.command({"collMod": "readers", "validator": reader_validation, "validationAction": validation_action})
db.command({"collMod": "borrowings", "validator": borrowing_validation, "validationAction": validation_action})
