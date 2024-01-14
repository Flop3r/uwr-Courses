/* Franciszek Przeliorz          */
/* Uniwerystet Wrocławski        */
/* Kurs: Programowanie Obiektowe */
/* Lista 5 : Zadanie 1           */
/*                               */
/* Kompilacja: javac Main.java   */

package ComparableClasses;

//* Klasa bazowa reprezentującą towar sklepowy.
//* Implementuje interfejs Comparable.
public class Product implements Comparable<Product> {
    private String name; // Nazwa
    private double price; // Cena

    // Konstruktor klasy.
    public Product(String name, double price) {
        this.name = name;
        this.price = price;
    }

    // Metoda porównująca dwa towary według ceny
    @Override
    public int compareTo(Product other) {
        if (other == null) {
            throw new NullPointerException("Cannot compare to null");
        }

        if (this.price < other.price) {
            return -1;
        } else if (this.price > other.price) {
            return 1;
        } else {
            return 0;
        }
    }

    // Przesłonięta metoda toString() zwracająca
    // nazwę towaru jako łańcuch znaków.
    @Override
    public String toString() {
        return name;
    }
}