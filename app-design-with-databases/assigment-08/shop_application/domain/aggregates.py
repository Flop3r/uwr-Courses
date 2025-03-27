from domain.value_objects import CartItem, OrderItem
from domain.entities import Product
from pydantic import BaseModel, Field, validator, model_validator
from datetime import datetime

class Cart:
    def __init__(self, cart_id: str):
        self.id = cart_id
        self.items: list[CartItem] = []

    def add_product(self, product: Product, quantity: int) -> None:
        for item in self.items:
            if item.product_id == product.id:
                item.quantity += quantity
                return
        self.items.append(CartItem(product_id=product.id, product=product, quantity=quantity))

    def remove_product_all(self, product_id: str) -> None:
        self.items = [item for item in self.items if item.product_id != product_id]

    def remove_one(self, product_id: str) -> None:
        for item in self.items:
            if item.product_id == product_id:
                item.quantity -= 1
                if item.quantity <= 0:
                    self.remove_product(product_id)
                return

    def get_total(self) -> float:
        return sum(item.product.price * item.quantity for item in self.items)

    def clear(self) -> None:
        self.items.clear()


class Order(BaseModel):
    id: str
    customer_id: str
    order_date: datetime
    items: list
    status: str = "Pending"

    # Data zamówienia nie może być w przeszłości
    @model_validator(mode="before")
    def validate_order_date(cls, values):
        order_date = values.get("order_date")
        if order_date and order_date < datetime.now():
            raise ValueError("Order date cannot be in the past.")
        return values

    # Kwota zamówienia nie może być mniejsza niż 1
    @model_validator(mode="before")
    def validate_total_amount(cls, values):
        total_amount = sum(item.quantity * item.price for item in values.get('items', []))
        if total_amount < 1:
            raise ValueError("Total amount must be at least 1.")
        return values
    
    # Zamówienie nie może być puste
    @model_validator(mode="before")
    def validate_items(cls, values):
        items = values.get('items')
        if not items:
            raise ValueError("Cannot place an order with no items.")
        return values

    ####################################################################

    def update_status(self, new_status: str):
        valid_statuses = ["Pending", "Shipped", "Delivered", "Cancelled"]
        if new_status not in valid_statuses:
            raise ValueError(f"Invalid status: {new_status}")
        self.status = new_status

    def add_item(self, product_id: str, quantity: int, price: float):
        self.items.append(OrderItem(product_id, quantity, price))
        self.total_price = sum(item.get_total_price() for item in self.items)



