from domain.entities import Product
from domain.aggregates import Cart, Order
from domain.repositories import IProductRepository, ICartRepository, IOrderRepository

class InMemoryProductRepository(IProductRepository):
    def __init__(self):
        self.products = []

    def get_by_id(self, product_id: str):
        return next((p for p in self.products if p.id == product_id), None)

    def get_all(self):
        return self.products

    def add(self, product: Product):
        self.products.append(product)

    def update(self, product: Product):
        self.delete(product.id)
        self.add(product)

    def delete(self, product_id: str):
        self.products = [p for p in self.products if p.id != product_id]

class InMemoryCartRepository(ICartRepository):
    def __init__(self):
        self.carts = {}

    def get_by_id(self, cart_id: str):
        return self.carts.get(cart_id, Cart(cart_id))

    def save(self, cart: Cart):
        self.carts[cart.id] = cart

class InMemoryOrderRepository(IOrderRepository):
    def __init__(self):
        self.orders = {}

    def get_by_id(self, order_id: str) -> Order:
        return self.orders.get(order_id)

    def save(self, order: Order):
        self.orders[order.id] = order
    
    def update_status(self, order_id: str, new_status: str):
        order = self.get_by_id(order_id)
        if order is None:
            raise ValueError(f"Order with ID {order_id} not found.")
        
        valid_statuses = ['Pending', 'Shipped', 'Delivered', 'Cancelled']
        if new_status not in valid_statuses:
            raise ValueError(f"Invalid status: {new_status}")
        
        order.update_status(new_status)
        self.save(order)

    def delete(self, order_id: str):
        if order_id in self.orders:
            del self.orders[order_id]
