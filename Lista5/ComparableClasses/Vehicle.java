package ComparableClasses;

// Franciszek Przeliorz          //
// Uniwerystet Wrocławski        //
// Kurs: Programowanie Obiektowe //
// Lista 5                       //

//* Klasa bazowa reprezentująca pojazd.
public class Vehicle implements Comparable<Vehicle> {
    private String make;
    private String model;
    private int year;

    public Vehicle(String make, String model, int year) {
        this.make = make;
        this.model = model;
        this.year = year;
    }

    @Override
    public int compareTo(Vehicle other) {
        return Integer.compare(this.year, other.year);
    }

    public String toString() {
        return make + " " + model;
    }
}
