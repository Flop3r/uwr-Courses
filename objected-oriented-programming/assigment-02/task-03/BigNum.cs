/*
 * Franciszek Przeliorz
 * Uniwerystet Wrocławski
 * Kurs: Programowanie Obiektowe
 * Lista 2: Zadanie 3
 * 
 * Compilation command: mcs BigNum.c
 */ 

using System;
using System.Linq;
using System.Collections.Generic;

//* Klasa BigNum, której obiekty pamiętają duże
// liczby całkowite z przedziału (-10^9, 10^9) *//
class BigNum
{
    public List<int> digits;
    private int sign;

    //* Konstuktor klasy BigNum *//
    public BigNum(int num)
    {
        if (num < -2000000000 || num > 2000000000)
        {
            throw new Exception("ERROR! Liczba musi być z przedziału" +
                                "(-20^9, 20^9)");
        }
        
        //* Jezeli liczba nalezy do przedzialu (-10^9, 10^9) *//
        List<int> digitResult = new List<int>();
        sign = 1;
        
        if (num < 0)
        {
            num = Math.Abs(num);
            sign = -1;
        }
        else if (num == 0)
        {
            digitResult.Insert(0, 0); 
        }

        while (num > 0)
        {
            digitResult.Insert(0, num % 10);
            num /= 10;
        }
        
        digits = digitResult;
    }

    //* Metoda drukująca na wyjściu standardowym liczbę BigNum *//
    public void Print()
    {
        if (sign == -1)
            Console.Write("-");

        foreach (int element in digits)
        {
            Console.Write(element);
        }

        Console.WriteLine();
    }

    //* Operator funckyjny dodawania wartości dwóch obiektów typu BigNum *//
    static public BigNum operator +(BigNum a, BigNum b)
    {
        if (a.sign > 0 && b.sign < 0)
            return Sub(a, b);
        if (a.sign < 0 && b.sign > 0)
            return Sub(b, a);
        
        return Add(a, b);
    }
    
    //* Operator funckyjny odejmowania wartości dwóch obiektów typu BigNum *//
    static public BigNum operator -(BigNum a, BigNum b)
    {
        if (a.sign > 0 && b.sign < 0)
            return Add(a, b);
        if (a.sign < 0 && b.sign > 0)
            return Add(a, b);
        if(a.sign < 0 && b.sign < 0)
            return Sub(b, a);
        
        return Sub(a, b);
    }
    
    //* Funckja zwracająca sumę wartości dwóch obiektów BigNum *//
    static private BigNum Add(BigNum a, BigNum b)
    {
        List<int> resultDigits = new List<int>();
        int n = a.digits.Count - 1;
        int m = b.digits.Count - 1;

        int carry = 0;
        while (n >= 0 || m >= 0)
        {
            int aSummand = (n >= 0) ? a.digits[n] : 0;
            int bSummand = (m >= 0) ? b.digits[m] : 0;

            int sum = aSummand + bSummand + carry;
            resultDigits.Insert(0,sum%10);
            carry = sum / 10;
            
            
            n--;
            m--;
        }

        if (carry > 0)
            resultDigits.Insert(0,carry); 
        
        BigNum result = new BigNum(0);
        result.digits = resultDigits;

        result.sign = 1;
        if (a.sign < 0)
            result.sign = -1;
        
        result.RemoveLeadingZeros();
        return result;
    }
    
    //* Funckja zwracająca różnicę wartości dwóch obiektów BigNum *//
    static private BigNum Sub(BigNum a, BigNum b)
    {
        
        List<int> resultDigits = new List<int>();
        
        BigNum result = new BigNum(0);
        result.sign = 1;
        
        
        int n = a.digits.Count - 1;
        int m = b.digits.Count - 1;
        
        List<int> nList = a.digits;
        List<int> mList = b.digits;
        
        if(a < b)
        {
            result.sign = -1;
            n = b.digits.Count - 1;
            m = a.digits.Count - 1;
            nList = b.digits;
            mList = a.digits;
        }


        int borrow = 0;
        while (n >= 0 || m >= 0)
        {
            int aSummand = (n >= 0) ? nList[n] : 0;
            int bSummand = (m >= 0) ? mList[m] : 0;

            int diff = aSummand - bSummand - borrow;
            if (diff < 0)
            {
                diff += 10;
                borrow = 1;
            }
            else
            {
                borrow = 0;
            }
            
            resultDigits.Insert(0,diff);
            
            n--;
            m--;
        }
        
        
        result.digits = resultDigits;
        result.RemoveLeadingZeros();
        return result;
    }
    
    //* Operator funkcyjny porównujący wartości dwóch liczb *//
    static public bool operator <(BigNum a, BigNum b)
    {
        if (a.digits.Count < b.digits.Count)
        {
            return true;
        }
        
        if (a.digits.Count > b.digits.Count)
        {
            return false;
        }


        for (int i = 0; i < a.digits.Count; i++)
        {
            if (a.digits[i] < b.digits[i])
                return true;
            
            if (a.digits[i] > b.digits[i])
                return false;
        }
        
        return false;
    }
    
    //* Operator funkcyjny porównujący wartości dwóch liczb *//
    static public bool operator >(BigNum a, BigNum b)
    {
        return b < a;
    }
    
    //* Metoda usuwająca niepotrzebne zera z początku listy *//
    public void RemoveLeadingZeros()
    {
        while (digits[0] == 0 && digits.Count > 1)
        {
            digits.RemoveAt(0);
        }
    }
}

class Program
{   
    //* Funkcja wywoławcza *//
    static void Main(string[] args)
    {
        TestZero();
        if (TestAddition() == false)
            return;
        if (TestSubtraction() == false)
             return;

        Console.WriteLine("ALL TEST PASSED!");
    }
    
    //* Do kodu ponizej użyłem pomocy gpt-3 *//
    //* Metoda tworząca 30 testów na operatorze BiNum + *//
    //* Zwraca wyniki testów *//
    static bool TestAddition()
    {
        Random random = new Random();
        bool passed = true;

        for (int i = 0; i < 30; i++)
        {
            int x = random.Next(-1000000000, 1000000000);
            int y = random.Next(-1000000000, 1000000000);

            BigNum a = new BigNum(x);
            BigNum b = new BigNum(y);
            BigNum c = a + b;
            BigNum expected = new BigNum(x + y);
            if (Enumerable.SequenceEqual(c.digits,expected.digits) == false)
            {
                Console.WriteLine($"ERROR! TEST {i + 1} NOT PASSED!");
                passed = false;
            }
            else
            {
                Console.WriteLine($"TEST {i + 1} PASSED!");
            }

            

            Console.WriteLine($"x: {x}");
            Console.WriteLine($"y: {y}");

            Console.Write("Wynik: ");
            c.Print();

            Console.Write("Oczekiwany wynik: ");
            expected.Print();
            Console.WriteLine();
        }

        if (passed == false)
            return false;
        
        return true;
    }
    
    //* Metoda tworząca 30 testów na operatorze BiNum - *//
    //* Zwraca wyniki testów *//
    static bool TestSubtraction()
    {
        Random random = new Random();
        bool passed = true;

        for (int i = 0; i < 30; i++)
        {
            int x = random.Next(-1000000000, 1000000000);
            int y = random.Next(-1000000000, 1000000000);

            BigNum a = new BigNum(x);
            BigNum b = new BigNum(y);
            BigNum c = a - b;
            BigNum expected = new BigNum(x - y);
            if (Enumerable.SequenceEqual(c.digits,expected.digits) == false)
            {
                Console.WriteLine($"ERROR! TEST {i + 31} NOT PASSED!");
                passed = false;
            }
            else
            {
                Console.WriteLine($"TEST {i + 31} PASSED!");
            }

            Console.WriteLine($"x: {x}");
            Console.WriteLine($"y: {y}");

            Console.Write("Wynik: ");
            c.Print();

            Console.Write("Oczekiwany wynik: ");
            expected.Print();
            Console.WriteLine();

        }

        if (passed == false)
            return false;
        
        return true;
    }
    
    //* Pomocnicza metoda z trzema testami z uzyciem zera*//
    static void TestZero()
    {
        Console.WriteLine("TEST 0:");
        int x = 0;
        int y = 18;
        int z = -18;
        
        BigNum a = new BigNum(x);
        BigNum b = new BigNum(y);
        BigNum c = new BigNum(z);
        
        
        Console.WriteLine($"x: {x}");
        Console.WriteLine($"y: {y}");
        Console.Write($"Wynik: {x} - {y} = ");
        (a - b).Print();
        Console.WriteLine();
        
        
        Console.WriteLine($"x: {x}");
        Console.WriteLine($"y: {z}");
        Console.Write($"Wynik: {x} - {z} = ");
        (a - c).Print();
        Console.WriteLine();
        
        
        Console.WriteLine($"x: {y}");
        Console.WriteLine($"y: {z}");
        Console.Write($"Wynik: {y} + {z} = ");
        (b + c).Print();
        Console.WriteLine();

        
        Console.WriteLine($"x: {y}");
        Console.WriteLine($"y: {z}");
        Console.Write($"Wynik: {y} - {z} = ");
        (b - c).Print();
        Console.WriteLine();
    }
}