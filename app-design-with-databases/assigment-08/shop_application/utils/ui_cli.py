from domain.entities import Product
from domain.aggregates import Cart, Order
from domain.value_objects import OrderItem
from infrastructure.repositories_in_memory import (
    InMemoryProductRepository,
    InMemoryCartRepository,
    InMemoryOrderRepository,
)

def display_products(products):
    """
    Displays a list of products.
    """
    print("\nAvailable Products:")
    if not products:
        print("No products available.")
        return
    for product in products:
        print(
            f"ID: {product.id}, Name: {product.name}, Price: {product.price}, "
            f"Stock: {product.stock}, Description: {product.description}"
        )


def display_cart(cart: Cart):
    """
    Displays the contents of the cart.
    """
    print("\nCart Contents:")
    if not cart.items:
        print("Cart is empty.")
        return
    for item in cart.items:
        print(
            f"Product ID: {item.product_id}, Name: {item.product.name}, "
            f"Quantity: {item.quantity}, Total: {item.product.price * item.quantity:.2f}"
        )
    print(f"Total Price: {cart.get_total():.2f}")


def display_orders(orders):
    """
    Displays all orders.
    """
    print("\nOrders:")
    if not orders:
        print("No orders available.")
        return
    for order in orders:
        print(
            f"Order ID: {order.id}, Customer ID: {order.customer_id}, Status: {order.status}, "
            f"Total Price: {order.total_price:.2f}"
        )


def user_interface():
    """
    Main user interface function.
    """
    # Initialize repositories
    product_repo = InMemoryProductRepository()
    cart_repo = InMemoryCartRepository()
    order_repo = InMemoryOrderRepository()

    # Add some initial products
    product_repo.add(Product("1", "Laptop", 1200.00, 10, "High-performance laptop."))
    product_repo.add(Product("2", "Phone", 800.00, 20, "Smartphone with great camera."))
    product_repo.add(Product("3", "Headphones", 150.00, 30, "Noise-cancelling headphones."))

    while True:
        print("\nMain Menu:")
        print("1. View Products")
        print("2. Add Product to Cart")
        print("3. View Cart")
        print("4. Remove Product from Cart")
        print("5. Place Order")
        print("6. View Orders")
        print("7. Update Order Status")
        print("8. Exit")
        
        choice = input("\nEnter your choice: ")

        if choice == "1":
            # View products
            products = product_repo.get_all()
            display_products(products)

        elif choice == "2":
            # Add product to cart
            cart_id = input("Enter Cart ID: ")
            cart = cart_repo.get_by_id(cart_id)
            product_id = input("Enter Product ID to add: ")
            product = product_repo.get_by_id(product_id)
            if product is None:
                print("Product not found.")
                continue
            quantity = int(input(f"Enter quantity (available: {product.stock}): "))
            try:
                cart.add_product(product, quantity)
                cart_repo.save(cart)
                print("Product added to cart.")
            except ValueError as e:
                print(e)

        elif choice == "3":
            # View cart
            cart_id = input("Enter Cart ID: ")
            cart = cart_repo.get_by_id(cart_id)
            display_cart(cart)

        elif choice == "4":
            # Remove product from cart
            cart_id = input("Enter Cart ID: ")
            cart = cart_repo.get_by_id(cart_id)
            product_id = input("Enter Product ID to remove: ")
            cart.remove_product_all(product_id)
            cart_repo.save(cart)
            print("Product removed from cart.")

        elif choice == "5":
            # Place order
            cart_id = input("Enter Cart ID: ")
            customer_id = input("Enter Customer ID: ")
            cart = cart_repo.get_by_id(cart_id)
            if not cart.items:
                print("Cart is empty. Cannot place order.")
                continue
            order_items = [
                OrderItem(item.product_id, item.quantity, item.product.price)
                for item in cart.items
            ]
            order = Order(order_id=str(len(order_repo.orders) + 1), customer_id=customer_id, items=order_items)
            order_repo.save(order)
            cart.clear()
            cart_repo.save(cart)
            print(f"Order placed successfully. Order ID: {order.id}")

        elif choice == "6":
            # View orders
            orders = list(order_repo.orders.values())
            display_orders(orders)

        elif choice == "7":
            # Update order status
            order_id = input("Enter Order ID: ")
            new_status = input("Enter new status (Pending, Shipped, Delivered, Cancelled): ")
            try:
                order_repo.update_status(order_id, new_status)
                print("Order status updated successfully.")
            except ValueError as e:
                print(e)

        elif choice == "8":
            # Exit
            print("Exiting the application. Goodbye!")
            break

        else:
            print("Invalid choice. Please try again.")
