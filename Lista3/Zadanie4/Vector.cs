using System;

/*
* Franciszek Przeliorz
* Lista 3 - Zadanie 2
* mcs -t:library  Vector.cs
*/

public class Vector
{
    private float[] coords;
    private int dim;
    
    //* Konstruktor klasy Vector *//
    public Vector(int n)
    {
        dim = n;
        coords = new float[dim];
    }
    
    //* Właściwość umożliwiająca dostęp do poszczególnych składowych wektora *//
    public float this[int i]
    {
        get { return coords[i]; }
        set { coords[i] = value; }
    }
    
    //* Operator funkcyjny dodawania wektorów *//
    public static Vector operator +(Vector v1, Vector v2)
    {
        
        // Sprawdzenie czy wektory są równych wymiarów
        if (v1.dim != v2.dim)
        {
            throw new Exception($"Wektory v1 i v2 są o różnych wymiarach!");
        }
        
        // Inicjacja nowego obiektu typu Vector
        // do przechowywania wyniku dodawania
        Vector sumVector = new Vector(v1.dim);
    
        for (int i = 0; i < v1.dim; i++)
        {
            sumVector[i] = v1[i] + v2[i];
        }
        
        return sumVector;
    }
    
    //* Operator mnożenia wektorów (iloczyn skalarny) *//
    public static float operator *(Vector v1, Vector v2)
    {
        // Jeżeli wektory są o różnych wymiarach zwraca błąd
        if (v1.dim != v2.dim)
        {
            throw new Exception($"Wektory v1 i v2 są o różnych wymiarach!");
        }
        
        // Inicjacja nowej zmiennej typu float,
        // do przechowywania wyniku iloczynu skalarnego
        float dotProduct = 0.0f;
        for (int i = 0; i < v1.dim; i++)
        {
            dotProduct += v1[i] * v2[i];
        }

        return dotProduct;
    }
    
    //* Operator mnożenia wektora przez skalar *//
    public static Vector operator *(float scalar, Vector v)
    {
        // Inicjacja nowego obiektu typu Vector
        // do przechowywania wyniku mnożenia przez skalar
        Vector newVector = new Vector(v.dim);

        for (int i = 0; i < v.dim; i++)
        {
            newVector[i] = scalar * v[i];
        }
        
        return newVector;
    }

    //* Metoda zwracająca długość wektora *//
    public float Norma()
    {
        return (float)Math.Sqrt(this * this);
    }
    
    //* Metoda drukująca współrzędne wektora *//

    public void Print()
    {
        Console.Write($"Wektor: ");
        for (int i = 0; i < dim; i++)
        {
            Console.Write($"{coords[i]} ");
        }
        
        Console.WriteLine();
    }
}