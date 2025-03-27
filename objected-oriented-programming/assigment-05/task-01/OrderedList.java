/* Franciszek Przeliorz                   */
/* Uniwerystet Wrocławski                 */
/* Kurs: Programowanie Obiektowe          */
/* Lista 5 : Zadanie 1                    */
/*                                        */
/* Compilation command: javac Main.java   */

import java.util.*;

//* Klasa przechowująca listę wartości w kolejności rosnącej *//
public class OrderedList<T extends Comparable<T>> {

    private List<T> list; // Lista dwukierunkowa


    // Konstruktor inicjalizujący listę
    public OrderedList() {
        list = new LinkedList<>();
    }


    // Metoda dodająca nowy element do klisty
    public void addElement(T elem) {
        ListIterator<T> iterator = list.listIterator();

        while (iterator.hasNext()) {
            if (elem.compareTo(iterator.next()) < 0) {
                iterator.previous();
                iterator.add(elem);
                return;
            }
        }
        iterator.add(elem);
    }


    // Metoda zwracająca pierwszy element listy
    public T getFirst() {
        if (list.size() > 0) {
            return list.get(0);
        }
        return null;
    }


    // Metoda zwracająca łańcuch znaków
    // składający się z elementów listy
    @Override
    public String toString() {
        return list.toString();
    }
}