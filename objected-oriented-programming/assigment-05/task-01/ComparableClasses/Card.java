/* Franciszek Przeliorz                   */
/* Uniwerystet Wrocławski                 */
/* Kurs: Programowanie Obiektowe          */
/* Lista 5 : Zadanie 1                    */
/*                                        */
/* Compilation command: javac Main.java   */

package ComparableClasses;


//* Klasa bazowa reprezentującą kartę.
//* Implementuje interfejs Comparable.
public class Card implements Comparable<Card> {
    private Suit suit; // Kolor karty.
    private Rank rank; // Figura karty.

    // Konstruktor klasy Card.
    public Card(Suit suit, Rank rank) {
        this.suit = suit;
        this.rank = rank;
    }

    // Metoda porównująca dwie karty według starszeństwa.
    @Override
    public int compareTo(Card other) {
        if (other == null) {
            throw new NullPointerException("BŁĄD!: Nie można porównać Card do null");
        }

        // Porównanie według koloru, w drugiej kolejności po figurze
        int suitCompare = this.suit.compareTo(other.suit);

        if (suitCompare != 0) {
            return suitCompare;
        } else {
            return this.rank.compareTo(other.rank);
        }
    }

    // Przesłonięta metoda toString() zwracająca
    // kolor i figurę karty jako łańcuch znaków.
    @Override
    public String toString() {
        return rank + " of " + suit;
    }

}

