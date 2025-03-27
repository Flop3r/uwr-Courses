from domain.entities import Product
from pydantic import BaseModel, validator
from typing import Type

class CartItem(BaseModel):
    product_id: int
    product: Type[Product] 
    quantity: int

    @validator('quantity')
    def quantity_must_be_positive(cls, value):
        if value <= 0:
            raise ValueError('Quantity must be greater than 0.')
        return value

    @validator('product')
    def product_must_be_valid(cls, value):
        if not isinstance(value, Product):
            raise ValueError('Product must be an instance of the Product class.')
        return value

    def get_total_price(self) -> float:
        return self.product.price * self.quantity

    
class OrderItem(BaseModel):
    product_id: str
    quantity: int
    price: float

    # Walidator dla quantity, aby była większa niż 0
    @validator('quantity')
    def quantity_must_be_positive(cls, value):
        if value <= 0:
            raise ValueError('Quantity must be greater than 0.')
        return value

    # Walidator dla price, aby cena była większa lub równa 0
    @validator('price')
    def price_must_be_non_negative(cls, value):
        if value < 0:
            raise ValueError('Price must be greater than or equal to 0.')
        return value

    def get_total_price(self) -> float:
        return self.price * self.quantity