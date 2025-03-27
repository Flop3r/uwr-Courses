from flask import Flask, jsonify, request
from flask_pymongo import PyMongo
from bson.objectid import ObjectId

app = Flask(__name__)

# Konfiguracja MongoDB
app.config["MONGO_URI"] = "mongodb://localhost:27017/products_db"
mongo = PyMongo(app)

# Kolekcja
products_collection = mongo.db.products

# Pobranie wszystkich produktów
@app.route('/products', methods=['GET'])
def get_products():
    products = []
    for product in products_collection.find():
        print(product)
        products.append({
            "id": str(product["_id"]),
            "name": product["name"],
            "description": product["description"],
            "price": product["price"]
        })
    return jsonify(products)

# Pobranie pojedynczego produktu po ID
@app.route('/products/<string:product_id>', methods=['GET'])
def get_product(product_id):
    product = products_collection.find_one({"_id": ObjectId(product_id)})
    if product:
        return jsonify({
            "id": str(product["_id"]),
            "name": product["name"],
            "description": product["description"],
            "price": product["price"]
        })
    return jsonify({"error": "Product not found"}), 404

@app.route('/products', methods=['POST'])
def create_product():
    data = request.json
    new_product = {
        "name": data.get("name"),
        "description": data.get("description"),
        "price": data.get("price")
    }
    inserted_product = products_collection.insert_one(new_product)
    new_product["_id"] = str(inserted_product.inserted_id) 
    return jsonify({
        "id": new_product["_id"],
        "name": new_product["name"],
        "description": new_product["description"],
        "price": new_product["price"]
    }), 201


@app.route('/products/<string:product_id>', methods=['PUT'])
def update_product(product_id):
    data = request.json
    updated_product = products_collection.find_one_and_update(
        {"_id": ObjectId(product_id)},
        {"$set": data},
        return_document=True
    )
    if updated_product:
        return jsonify({
            "id": str(updated_product["_id"]),  
            "name": updated_product["name"],
            "description": updated_product["description"],
            "price": updated_product["price"]
        }), 200
    return jsonify({"error": "Product not found"}), 404

# Usunięcie produktu
@app.route('/products/<string:product_id>', methods=['DELETE'])
def delete_product(product_id):
    result = products_collection.delete_one({"_id": ObjectId(product_id)})
    if result.deleted_count:
        return jsonify({"message": "Product deleted"}), 200
    return jsonify({"error": "Product not found"}), 404

if __name__ == '__main__':
    app.run(debug=True)
