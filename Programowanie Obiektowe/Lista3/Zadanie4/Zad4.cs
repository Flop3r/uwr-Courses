using System;
/*
* Franciszek Przeliorz
* Lista 3 - Zadanie 4
* mcs Zad4.cs -r:Vector.dll
*/

class Program
{
    static void Main(string[] args)
    {
        // Test konstruktora
        // Wypisze "Wektor v1: 0 0 0"
        Vector v1 = new Vector(3);
        v1.Print(); 
       

        
        // Test ustawienia wartości składowych
        // Wypisze "Wektor v1: 1 2 3"
        v1[0] = 1;
        v1[1] = 2;
        v1[2] = 3;
        v1.Print(); 
        

        
        // Test dodawania wektorów
        // Wypisze "Wektor v3: 5 7 9"
        Vector v2 = new Vector(3);
        v2[0] = 4;
        v2[1] = 5;
        v2[2] = 6;
        
        Vector v3 = (v1 + v2);
        v3.Print(); 
        

        
        // Test iloczynu skalarnego
        // Wypisze "Iloczyn skalarny wektorów v1 i v2: 32"
        Console.WriteLine($"Iloczyn skalarny wektorów v1 i v2: {v1 * v2}"); 
        

        
        // Test mnożenia wektora przez skalar
        // Wypisze "Wektor v4: 2 4 6"
        float scalar = 2;
        Vector v4 = scalar * v1;
        v4.Print(); 
        

        
        // Test długości wektora
        // Wypisze "Długość wektora v1: 3.741657"
        Console.WriteLine($"Długość wektora v1: {v1.Norma()}"); 
        
        
        
        // Test błędu "Exception($"Wektory {v1} i {v2} są o różnych wymiarach!")"
        // Wypisze ""Wektory v1 i v5 są o różnych wymiarach!"!"
        Vector v5 = new Vector(4);

        try
        {
            (v1 + v5).Print();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}