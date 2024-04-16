/* Franciszek Przeliorz                   */
/* Uniwerystet Wrocławski                 */
/* Kurs: Programowanie Obiektowe          */
/* Lista 5 : Zadanie 1                    */
/*                                        */
/* Compilation command: javac Main.java   */

package ComparableClasses;

//* Klasa bazowa reprezentująca pojazd.
public class Vehicle implements Comparable<Vehicle> {
    private String make;
    private String model;
    private int year;

    // Konstruktor klasy.
    public Vehicle(String make, String model, int year) {
        this.make = make;
        this.model = model;
        this.year = year;
    }

    // Metoda porównująca dwa pojazdy według rocznika
    @Override
    public int compareTo(Vehicle other) {

        return Integer.compare(this.year, other.year);
    }

    // Przesłonięta metoda toString() zwracająca
    // nazwę firmy i modelu pojazdu
    public String toString() {

        return make + " " + model;

    }
}
