from flask import Flask, render_template, request, redirect, url_for, flash
from flask_wtf import FlaskForm
from wtforms import StringField, FloatField, IntegerField, TextAreaField, SubmitField
from wtforms.validators import DataRequired, NumberRange, Length
from domain.entities import Product
import os
from domain.aggregates import Order
from infrastructure.repositories_in_memory import (
    InMemoryProductRepository,
    InMemoryCartRepository,
    InMemoryOrderRepository,
)
from domain.value_objects import OrderItem

app = Flask(__name__)
app.secret_key = "supersecretkey"

# CSRF token
app.config['SECRET_KEY'] = os.urandom(24)  # Użyj losowego klucza dla bezpieczeństwa
app.config['WTF_CSRF_SECRET_KEY'] = os.urandom(24)  # Ustaw sekret dla tokenu CSRF
app.config['WTF_CSRF_ENABLED'] = True

# Repositories
product_repo = InMemoryProductRepository()
cart_repo = InMemoryCartRepository()
order_repo = InMemoryOrderRepository()

# Dodanie produktów do repozytorium
product_repo.add(Product(id="1", name="Laptop", price=1200.00, stock=10, description="High-performance laptop."))
product_repo.add(Product(id="2", name="Phone", price=800.00, stock=20, description="Smartphone with great camera."))
product_repo.add(Product(id="3", name="Headphones", price=150.00, stock=30, description="Noise-cancelling headphones."))

# Form for adding a new product
class ProductForm(FlaskForm):
    id = StringField("Product ID", validators=[DataRequired()])
    name = StringField("Name", validators=[DataRequired(), Length(min=1)])
    price = FloatField("Price", validators=[DataRequired(), NumberRange(min=0.01)])
    stock = IntegerField("Stock", validators=[DataRequired(), NumberRange(min=0)])
    description = StringField("Description", validators=[DataRequired(), Length(min=1)])
    submit = SubmitField("Add Product")

@app.route("/admin")
def admin():
    """
    Admin panel for managing products.
    """
    products = product_repo.get_all()
    form = ProductForm()
    return render_template("admin.html", products=products, form=form)


@app.route("/admin/add_product", methods=["POST"])
def add_product():
    """
    Add a new product to the repository.
    """
    form = ProductForm()
    if form.validate_on_submit():
        product_id = form.id.data
        name = form.name.data
        price = form.price.data
        stock = form.stock.data
        description = form.description.data

        if product_repo.get_by_id(product_id):
            flash("Product ID already exists!", "error")
        else:
            # Tworzenie i dodawanie produktu do repozytorium
            product_repo.add(Product(id=product_id, name=name, price=price, stock=stock, description=description))
            flash("Product added successfully!", "success")
            print(product_repo.get_all)
        return redirect(url_for("admin"))
    else:
        # Zapisanie i wyświetlenie błędów walidacji
        for field, errors in form.errors.items():
            for error in errors:
                print(f"Field {field}: {error}")
        flash("Form validation failed. Please check your inputs.", "error")
        return redirect(url_for("admin"))
    



@app.route("/")
def index():
    """
    Display all available products.
    """
    products = product_repo.get_all()
    return render_template("index.html", products=products)

@app.route("/cart")
def cart():
    """
    Display the contents of the cart.
    """
    cart = cart_repo.get_by_id("default")
    return render_template("cart.html", cart=cart)

@app.route("/add_to_cart/<product_id>")
def add_to_cart(product_id):
    """
    Add a product to the cart.
    """
    product = product_repo.get_by_id(product_id)
    if not product:
        flash("Product not found!", "error")
        return redirect(url_for("index"))

    cart = cart_repo.get_by_id("default")
    cart.add_product(product, 1)
    cart_repo.save(cart)
    flash(f"{product.name} added to cart.", "success")
    return redirect(url_for("index"))

@app.route("/admin/delete_product/<product_id>", methods=["POST"])
def delete_product(product_id):
    """
    Usuwanie produktu z repozytorium.
    """
    product = product_repo.get_by_id(product_id)
    if product:
        product_repo.delete(product_id)
        flash(f"Product {product.name} deleted successfully.", "success")
    else:
        flash("Product not found!", "error")
    
    return redirect(url_for("admin"))


@app.route("/checkout", methods=["POST"])
def checkout():
    """
    Place an order for the items in the cart and update product quantities.
    """
    cart = cart_repo.get_by_id("default")
    if not cart.items:
        flash("Cart is empty. Cannot place order.", "error")
        return redirect(url_for("cart"))

    total_amount = sum(item.product.price * item.quantity for item in cart.items)
    if total_amount < 1:
        flash("Total order amount must be at least 1.", "error")
        return redirect(url_for("cart"))

    # Aktualizowanie ilości produktu w sklepie
    for item in cart.items:
        product = product_repo.get_by_id(item.product.id)
        if product is None:
            flash(f"Product {item.product.name} not found!", "error")
            return redirect(url_for("cart"))
        if product.stock < item.quantity:
            flash(f"Not enough stock for {product.name}. Available: {product.stock}", "error")
            return redirect(url_for("cart"))
        product.stock -= item.quantity  # Zmniejszanie stanu magazynowego
        product_repo.update(product)    # Aktualizacja w repozytorium

    # Tworzenie zamówienia
    order_items = [
        OrderItem(item.product_id, item.quantity, item.product.price) for item in cart.items
    ]
    order = Order(order_id=str(len(order_repo.orders) + 1), customer_id="customer1", items=order_items)
    order_repo.save(order)

    # Czyszczenie koszyka
    cart.clear()
    cart_repo.save(cart)
    flash(f"Order placed successfully! Order ID: {order.id}", "success")
    return redirect(url_for("index"))

@app.route("/orders")
def orders():
    """
    Display all placed orders.
    """
    orders = list(order_repo.orders.values())
    return render_template("orders.html", orders=orders)
