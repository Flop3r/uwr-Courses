using System;

/*
 * Franciszek Przeliorz
 * Uniwerystet Wrocławski
 * Kurs: Programowanie Obiektowe
 * Lista 3: Zadanie 2
 * 
 * Compilation command: mcs Main.cs -r:MyDictionary.dll
 */ 

class Program
{
    static void Main(string[] args)
    {

        bool flagError = false;
        System.Console.WriteLine("TEST 1.0:  INICJACJA SŁOWNIKA O KLUCZACH I WARTOSCIACH TYPU INT " +
                                 "I DODAWANIE DO NIEGO ELEMENTÓW:");
        

            try
            {
                MyDictionary<int, string> Liczby = new MyDictionary<int, string>();
            }
            catch (Exception)
            {
                System.Console.WriteLine("ERROR!: Problem z inicjacją słownika z " +
                                         "kluczy i wartości o typach <int, int>!");
                flagError = true;
            } 
            
        if (!flagError) { 
            System.Console.WriteLine("TEST POZYTYWNY");
            
            MyDictionary<int, string> Liczby = new MyDictionary<int, string>();
            Liczby.Add(1, "jeden");
            System.Console.WriteLine($"Wielkość słownika Liczby: {Liczby.size}");
            System.Console.WriteLine($":Wartość klucza dla klucza równego 1: {Liczby.Search(1)}");
            System.Console.WriteLine();

            Liczby.Add(2, "dwa");
            System.Console.WriteLine($"Wielkość słownika Liczby: {Liczby.size}");
            System.Console.WriteLine($":Wartość klucza dla klucza równego 2: {Liczby.Search(2)}");
            System.Console.WriteLine();

            Liczby.Add(3, "trzy");
            System.Console.WriteLine($"Wielkość słownika Liczby: {Liczby.size}");
            System.Console.WriteLine($":Wartość klucza dla klucza równego 3: {Liczby.Search(3)}");
            System.Console.WriteLine();

            System.Console.WriteLine("TEST 1.1: USUWANIE ZE SŁOWNIKA ELEMENTU 2:");
            try
            {
                flagError = false;
                Liczby.Remove(2);
                System.Console.WriteLine($"Wielkość słownika Liczby: {Liczby.size}");
                System.Console.WriteLine();
            }
            catch (Exception)
            {
                System.Console.WriteLine("ERROR! PROBLEM Z USUNIĘCIEM ELEMENTU O KLUCZU 2 ZE SŁOWNIKA \"Liczby\" :");
                flagError = true;
            }

            if (!flagError)
                System.Console.WriteLine("POZYTYWNY WYNIK TESTU");
        }


        
        System.Console.WriteLine("TEST 2.0:  INICJACJA SŁOWNIKA O KLUCZACH I WARTOSCIACH TYPU STRING " +
                                 "I DODAWANIE DO NIEGO ELEMENTÓW: ");
        flagError = false;
        
        try
        {
            MyDictionary<string, string> ImionaNazwiska = new MyDictionary<string, string>();
        }
        catch (Exception)
        {
            System.Console.WriteLine("ERROR!: Problem z inicjacją słownika z " +
                                     "kluczy i wartości o typach <string, string>!");
            flagError = true;

        }

        if (!flagError)
        {
            System.Console.WriteLine("POZYTYWNY WYNIK TESTU");
            
            MyDictionary<string, string> ImionaNazwiska = new MyDictionary<string, string>();
            ImionaNazwiska.Add("Jan", "Kowalski");
            System.Console.WriteLine($"Wielkość słownika ImionaNazwiska: {ImionaNazwiska.size}");
            System.Console.WriteLine($":Wartość klucza dla klucza równego Jan: {ImionaNazwiska.Search("Jan")}");
            System.Console.WriteLine($"Oczekiwana wartość: Kowalski");
            System.Console.WriteLine();

            ImionaNazwiska.Add("Zofia", "Nowak");
            System.Console.WriteLine($"Wielkość słownika ImionaNazwiska: {ImionaNazwiska.size}");
            System.Console.WriteLine($":Wartość klucza dla klucza równego Zofia: {ImionaNazwiska.Search("Zofia")}");
            System.Console.WriteLine($"Oczekiwana wartość: Nowak");
            System.Console.WriteLine();

            ImionaNazwiska.Add("Franek", "Przeliorz");
            System.Console.WriteLine($"Wielkość słownika ImionaNazwiska: {ImionaNazwiska.size}");
            System.Console.WriteLine($":Wartość klucza dla klucza równego Franek: {ImionaNazwiska.Search("Franek")}");
            System.Console.WriteLine($"Oczekiwana wartość: Przeliorz");
            System.Console.WriteLine();

            System.Console.WriteLine("TEST 2.1: USUWANIE ZE SŁOWNIKA ELEMENTU O KLUCZU \"Franek:\"");

            ImionaNazwiska.Remove("Franek");
            System.Console.WriteLine($"Wielkość słownika ImionaNazwiska: {ImionaNazwiska.size}");
            System.Console.WriteLine();

            try
            {
                ImionaNazwiska.Search("Franek");
            }
            catch (Exception)
            {
                System.Console.WriteLine("Klucz nie został znaleziony w słowniku.");
            }
        }
    }
}
