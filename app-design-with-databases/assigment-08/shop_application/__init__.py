from infrastructure.repositories_in_memory import ProductRepository
from application.services import ProductService
from utils.ui import user_interface

if __name__ == "__main__":
    # Initialize the repository and service
    repository = ProductRepository()
    service = ProductService(repository)

    # Start the user interface
    user_interface(service)
