// Franciszek Przeliorz          //
// Uniwerystet Wrocławski        //
// Kurs: Programowanie Obiektowe //
// Lista 5                       //
//                               //
// javac Main.java               //

import ComparableClasses.*;
import java.util.*;


public class Main {

    public static void main(String[] args) {

        //* TEST #0 *//
        System.out.println("TEST #0: OrderList\n");

        // Tworzenie obiektu listy typu OrderedList
        OrderedList<Integer> intList = new OrderedList<>();

        // Dodawanie elementów do listy w nieuporządkowanej kolenjości
        intList.addElement(5);
        intList.addElement(10);
        intList.addElement(2);
        intList.addElement(7);
        intList.addElement(1);

        // Wyświetlenie elementów listy
        System.out.println("Lista liczb w kolejności rosnącej:");
        System.out.println("Oczekiwany wynik: [1, 2, 5, 7, 10]");
        System.out.println("Wynik: " + intList.toString() + "\n");

        // Wyświetlenie pierwszego elementu listy
        System.out.println("Pierwszy pojazd na liście:");
        System.out.println("Oczekiwany wynik: 1");
        System.out.println("Wynik: " + intList.getFirst().toString() + "\n\n");



        //* TEST #1 *//
        System.out.println("TEST #1: OrderList i klasa Vehicles\n");

        // Tworzenie obiektu listy typu OrderedList
        OrderedList<Vehicle> vehiclesList = new OrderedList<>();

        // Dodawanie elementów do listy w nieuporządkowanej kolenjości
        vehiclesList.addElement(new Vehicle("Ford", "Mustang", 1967));
        vehiclesList.addElement(new Vehicle("Chevrolet", "Camaro", 1970));
        vehiclesList.addElement(new Vehicle("Dodge", "Challenger", 1970));
        vehiclesList.addElement(new Vehicle("Pontiac", "GTO", 1965));

        // Wyświetlenie elementów listy
        System.out.println("Lista pojazdów w kolejności rosnącej:");
        System.out.println("Oczekiwany wynik: [Pontiac GTO, Ford Mustang, Chevrolet Camaro, Dodge Challanger]");
        System.out.println("Wynik: " + vehiclesList.toString() + "\n");

        // Wyświetlenie pierwszego elementu listy
        System.out.println("Pierwszy pojazd na liście:");
        System.out.println("Oczekiwany wynik: Pontiac GTO");
        System.out.println("Wynik: " + vehiclesList.getFirst().toString() + "\n\n");



        //* TEST #2 *//
        System.out.println("TEST #2: OrderList i klasy Prodcut\n");

        // Tworzenie obiektu listy typu OrderedList
        OrderedList<Product> productList = new OrderedList<>();

        // Dodawanie elementów do listy w nieuporządkowanej kolenjości
        productList.addElement(new Product("Wine", 40.0));
        productList.addElement(new Product("Milk", 4.50));
        productList.addElement(new Product("Chocolate Bar", 4.49));
        productList.addElement(new Product("Donut" , 2.30));

        // Wyświetlenie elementów listy
        System.out.println("Lista produkt w kolejności od najtańszego, do najdroższego:");
        System.out.println("Oczekiwany wynik: [Donut, Chocolate Bar, Milk, Wine]");
        System.out.println("Wynik: " + productList.toString() + "\n");

        // Wyświetlenie pierwszego elementu listy
        System.out.println("Pierwszy produkt na liście:");
        System.out.println("Oczekiwany wynik: Donut");
        System.out.println("Wynik: " + productList.getFirst().toString() + "\n\n");




        //* TEST #3 *//
        System.out.println("TEST #3: OrderList i klasy MilitaryRank\n");

        // Tworzenie obiektu listy typu OrderedList
        OrderedList<MilitaryRank> militaryList = new OrderedList<>();

        // Dodawanie elementów do listy w nieuporządkowanej kolenjości
        militaryList.addElement(new MilitaryRank("Capral", 2));
        militaryList.addElement(new MilitaryRank("Captain", 4));
        militaryList.addElement(new MilitaryRank("Lieutenant", 3));
        militaryList.addElement(new MilitaryRank("Private", 1));


        // Wyświetlenie elementów listy
        System.out.println("Lista stopni wojskowych w kolejności rosnącej:");
        System.out.println("Oczekiwany wynik: [Private, Capral, Lieutenant, Captain]");
        System.out.println("Wynik: " + militaryList.toString() + "\n");

        // Wyświetlenie pierwszego elementu listy
        System.out.println("Pierwszy stopień na liście:");
        System.out.println("Oczekiwany wynik: Private");
        System.out.println("Wynik: " + militaryList.getFirst().toString() + "\n\n");


        //* TEST #4 *//
        System.out.println("TEST #4: OrderList i klasy Card\n");

        // Tworzenie obiektu listy typu OrderedList
        OrderedList<Card> deckList = new OrderedList<>();

        // Dodanie kart do talii
        deckList.addElement(new Card(Suit.CLUBS, Rank.NINE));
        deckList.addElement(new Card(Suit.DIAMONDS, Rank.KING));
        deckList.addElement(new Card(Suit.HEARTS, Rank.NINE));


        // Wyświetlenie posortowanej talii
        System.out.println("Posortowana talia kart:");
        System.out.println("Oczekiwany wynik: [NINE of CLUBS, NINE of HEARTS, KING of DIAMONDS]");
        System.out.println("Wynik: " + deckList + "\n");


        // Pobranie pierwszej karty z talii
        System.out.println("Pierwsza karta w talii: ");
        System.out.println("Oczekiwany wynik: NINE of CLUBS");
        System.out.println("Wynik: " + deckList.getFirst());


    }
}