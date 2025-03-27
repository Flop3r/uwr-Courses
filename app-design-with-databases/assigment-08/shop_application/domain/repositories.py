from abc import ABC, abstractmethod
from domain.entities import Product
from domain.aggregates import Cart, Order

class IProductRepository(ABC):
    @abstractmethod
    def get_by_id(self, product_id: str) -> Product: pass

    @abstractmethod
    def get_all(self) -> list: pass

    @abstractmethod
    def add(self, product: Product): pass

    @abstractmethod
    def update(self, product: Product): pass

    @abstractmethod
    def delete(self, product_id: str): pass

class ICartRepository(ABC):
    @abstractmethod
    def get_by_id(self, cart_id: str) -> Cart: pass

    @abstractmethod
    def save(self, cart: Cart): pass

class IOrderRepository(ABC):
    @abstractmethod
    def get_by_id(self, order_id: str) -> Order:
        pass

    @abstractmethod
    def save(self, order: Order):
        pass
    
    @abstractmethod
    def update_status(self, order_id: str, new_status: str):
        pass

    @abstractmethod
    def delete(self, order_id: str):
        pass