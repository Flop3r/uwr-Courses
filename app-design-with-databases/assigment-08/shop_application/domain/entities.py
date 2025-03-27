from pydantic import BaseModel, Field, validator

class Product(BaseModel):
    id: str
    name: str
    price: float
    stock: int
    description: str

    # Walidacja ceny
    @validator("price")
    def price_must_be_positive(cls, value):
        if value < 0.01:
            raise ValueError("Price must be at least 0.01")
        return value

    # Walidacja zapasu
    @validator("stock")
    def stock_must_be_non_negative(cls, value):
        if value < 0:
            raise ValueError("Stock cannot be negative.")
        return value

    # Walidacja nazwy
    @validator("name")
    def name_must_be_non_empty(cls, value):
        if len(value) < 1:
            raise ValueError("Name must be at least 1 character long.")
        return value
    

    # Metoda aktualizacji zapasu
    def update_stock(self, quantity: int):
        if quantity > self.stock:
            raise ValueError("Not enough stock available.")
        self.stock -= quantity

    # Metoda aktualizacji produktu
    def update(self, name: str, price: float, description: str):
        if price <= 0:
            raise ValueError("Price must be greater than zero.")
        if len(description) < 1:
            raise ValueError("Description must be at least 1 character long.")
        self.name = name
        self.price = price
        self.description = description
