/*
* Franciszek Przeliorz
* Lista 2 - Zadanie 4
* mcs Zadanie4.cs
*/

using System;
using System.Collections.Generic;

class LazyIntList
{
    protected int NatSize;
    protected int NegSize;
    protected List<int> NatList = new List<int>();
    protected List<int> NegList = new List<int>();
    
    //* Metoda zwracająca liczbę elementów aktualnie przechowywanych w liście *//
    public int Size()
    {
        return NatSize + NegSize;
    }
    
    //* Metoda zwracająca i-ty element listy *//
    public int Element(int i)
    {
        if (i >= 0)
        {
            if (i >= NatSize)
            {
                for (int j = NatSize; j <= i; j++)
                {
                    NatList.Add(j);
                    NatSize++;
                }
            }
            return NatList[i];
        }

        if (-i >= NegSize + 1)
        {
            for (int j = -NegSize - 1; j >= i; j--)
            {
                NegList.Add(j);
                NegSize++;
            }
        }
        return NegList[-i - 1];
    }
}

class LazyPrimeList : LazyIntList
{   
    //* Metoda zwracająca i-tą liczbę pierwszą *//
    //* !!WAŻNE: Element(1) = 2 / Element(0) -> !ERROR*//
    public new int Element(int i)
    {
        i--;
        if (i < 0)
        {
            throw new Exception("Dla LazyPrimeList Element() " +
                                "zwraca wartość dla argumentów większych od 0");
        }
        
        if (i >= NatSize)
        {
            for (int j = NatSize; j <= i; j++)
            {
                int num = NatSize == 0 ? 2 : NatList[NatSize - 1] + 1;
                while (!IsPrime(num)) 
                    num++;
                
                NatList.Add(num);
                NatSize++;
            }
        }
        return NatList[i];
    }
    
    //* Metoda sprawdzająca czy liczba jest liczbą pierwszą *//
    private bool IsPrime(int num)
    {
        if (num < 2) 
            return false;
        if (num == 2) 
            return true;
        if (num % 2 == 0) 
            return false;
        
        for (int i = 3; i <= Math.Sqrt(num); i += 2)
        {
            if (num % i == 0) 
                return false;
        }

        return true;
    }
}


//* Do wygenerowania niektórych testów uzylem gpt-3*//
class Program
{
    static void Main(string[] arg)
    {
        LazyIntList lazylist = new LazyIntList();

    // test metody Element()
    Console.WriteLine("TEST METODY ELEMENT():");
    Console.WriteLine();
    Console.WriteLine("Użycie Element(50)");
    Console.WriteLine();
    
    lazylist.Element(50);
    int result = lazylist.Element(25);
    Console.WriteLine($"Element(25) = {result} (oczekiwany wynik: 25)");
    Console.WriteLine();
    
    // test metody Size()
    Console.WriteLine("TEST METODY SIZE(): ");
    Console.WriteLine($"Rozmiar listy: {lazylist.Size()} (oczekiwany wynik: 51)");
    Console.WriteLine();
    
    // test metody Element() z liczbami ujemnymi
    Console.WriteLine("TEST METODY ELEMENT() Z LICZBAMI UJEMNYMI: ");
    result = lazylist.Element(-1);
    Console.WriteLine($"Element(-1) = {result} (oczekiwany wynik: -1)");
    Console.WriteLine($"Rozmiar listy: {lazylist.Size()} (oczekiwany wynik: 52)");
    Console.WriteLine();
    
    // ponowne użycie listy
    lazylist = new LazyIntList();

    // test metody Element()
    Console.WriteLine("TEST METODY ELEMENT(): ");
    result = lazylist.Element(40);
    Console.WriteLine($"Element(40) = {result} (oczekiwany wynik: 40)");
    Console.WriteLine($"Rozmiar listy: {lazylist.Size()} (oczekiwany wynik: 41)");
    Console.WriteLine();
    
    result = lazylist.Element(38);
    Console.WriteLine($"Element(38) = {result} (oczekiwany wynik: 38)");
    Console.WriteLine($"Rozmiar listy: {lazylist.Size()} (oczekiwany wynik: 41)");
    Console.WriteLine();
    
    result = lazylist.Element(-38);
    Console.WriteLine($"Element(-38) = {result} (oczekiwany wynik: -38)");
    Console.WriteLine($"Rozmiar listy: {lazylist.Size()} (oczekiwany wynik: 79)"); // 79
    Console.WriteLine();
    
    result = lazylist.Element(-37);
    Console.WriteLine($"Element(-37) = {result} (oczekiwany wynik: -37)");
    Console.WriteLine($"Rozmiar listy: {lazylist.Size()} (oczekiwany wynik: 79)"); // 79
    Console.WriteLine();
    
    result = lazylist.Element(-41);
    Console.WriteLine($"Element(-41) = {result} (oczekiwany wynik: -41)");
    Console.WriteLine($"Rozmiar listy: {lazylist.Size()} (oczekiwany wynik: 82)"); // 82
    Console.WriteLine();

    LazyPrimeList primelist = new LazyPrimeList();

    // test metody Element() dla LazyPrimeList

    Console.WriteLine("TEST METODY ELEMENT() DLA LAZYPRIMELIST: ");
    Console.WriteLine($"Element(1) = {primelist.Element(1)} (oczekiwany wynik: 2)");
    Console.WriteLine($"Element(2) = {primelist.Element(2)} (oczekiwany wynik: 3)");
    Console.WriteLine($"Element(3) = {primelist.Element(3)} (oczekiwany wynik: 5)");
    Console.WriteLine($"Element(4) = {primelist.Element(4)} (oczekiwany wynik: 7)");
    Console.WriteLine($"Element(5) = {primelist.Element(5)} (oczekiwany wynik: 11)");
    Console.WriteLine($"Element(6) = {primelist.Element(6)} (oczekiwany wynik: 13)");
    Console.WriteLine($"Element(7) = {primelist.Element(7)} (oczekiwany wynik: 17)");
    Console.WriteLine($"Element(8) = {primelist.Element(8)} (oczekiwany wynik: 19)");
    Console.WriteLine($"Element(9) = {primelist.Element(9)} (oczekiwany wynik: 23)");
    Console.WriteLine($"Element(10) = {primelist.Element(10)} (oczekiwany wynik: 29)");
    Console.WriteLine($"Element(11) = {primelist.Element(11)} (oczekiwany wynik: 31)");
    Console.WriteLine();

    // test metody Size() dla LazyPrime
    
    Console.WriteLine("TEST MEOTDY SIZE() DLA LAZY PRIMELIST: ");
    Console.WriteLine($"Size() = {primelist.Size()} (oczekiwany wynik: 11)");
    }
}