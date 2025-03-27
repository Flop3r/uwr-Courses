import sys
from app import app  
from utils import ui_cli


def main():
    """
    Główny punkt wejścia aplikacji.
    - Jeśli '--cli' znajduje się w argumentach wiersza poleceń, uruchamia CLI.
    - W przeciwnym razie uruchamia aplikację webową Flask.
    """
    if '--cli' in sys.argv:
        print("Launching CLI interface...")
        ui_cli()
    else:
        print("Launching Web interface...")
        app.run(debug=True)


if __name__ == "__main__":
    main()
