package ComparableClasses;

// Franciszek Przeliorz          //
// Uniwerystet Wrocławski        //
// Kurs: Programowanie Obiektowe //
// Lista 5                       //


//* Klasa bazowa reprezentującą stopień wojskowy.
//* Implementuje interfejs Comparable.
public class MilitaryRank implements Comparable<MilitaryRank> {

    private String name; // Stopień
    private int order;   // Pozycja w hierarchi
                         // (im większa liczba tym wyższa pozycja)

    // Konstruktor klasy.
    public MilitaryRank(String name, int order) {
        this.name = name;
        this.order = order;
    }

    // Metoda porównująca dwa stopnie wojskowe
    @Override
    public int compareTo(MilitaryRank other) {
        if (other == null) {
            throw new NullPointerException
                    ("BŁĄD! Nie można porównać MilitaryRank do null");
        }

        if (this.order < other.order) {
            return -1;
        } else if (this.order > other.order) {
            return 1;
        } else {
            return 0;
        }
    }

    // Przesłonięta metoda toString() zwracająca
    // stopień wojskowy jako łańcuch znaków.
    @Override
    public String toString() {
        return name;
    }
}