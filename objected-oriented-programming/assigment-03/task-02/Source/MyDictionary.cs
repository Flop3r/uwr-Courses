using System;

/*
 * Franciszek Przeliorz
 * Uniwerystet Wrocławski
 * Kurs: Programowanie Obiektowe
 * Lista 3: Zadanie 2
 * 
 * Compilation command: mcs -t:library  MyDictionary.cs
 */ 

//* Zainicjowanie klasy MyDictionary *//
public class MyDictionary<K,V>
{
    private K[] keys;
    private V[] values;
    public int size;
    
    //* Konstruktor klasy *//
    public MyDictionary()
    {
        keys = null;
        values = null;
        size = 0;
    }
    
    //* Metoda dodająca nowy klucz i wartość do słownika *//
    public void Add(K key, V value)
    {
        
        K[] newKeys = new K[size + 1];
        V[] newValues  = new V[size + 1]; 
        
        //* Jeżeli w słowniku są juz jakies klucze i wartości>, *//
        //to do nowych tablic kluczy i wartosci *//
        //o długości o jeden większej, *//
        //* przepisujemy stare klucze i wartosci *//
        
        if (size > 0)
        {
            for (int i = 0; i < size; i++)
            {
                newKeys[i] = keys[i];
                newValues[i] = values[i];
            }
        }
        
        //* Na koniec słownika dodajemy nowy klucz i wartość *//
        newKeys[size] = key;
        newValues[size] = value;
        
        keys = newKeys;
        values = newValues;
        size++;
    }

    //* Metoda usuwająca podany klucz i wartość ze słownika *//
    public void Remove(K key)
    {
        //* Znajdujemy index w tablicach kluczy i elementów dla podanego klucza *//
        int index = GetIndex(key);
        size--;
        K[] newKeys = new K[size];
        V[] newValues  = new V[size];
    
        //* Do nowej tablicy klucza i wartości przepisujemy *//
        //* wszystkie klucze i wartosci starej tablicy*//
        //* z wyjątkiem tych o indeksie którego chcemy usunąć *//
        int i = 0, j = 0;
        while (i < size)
        {
            if (j == index)
            {
                j++;
                continue;
            }
    
            newKeys[i] = keys[j];
            newValues[i] = values[j];
            i++;
            j++;

        }
        
        //* Zmieniamy wartości keys i values na te które przepisyaliśmy *// 
        keys = newKeys;
        values = newValues;
    }
    
    //* Metoda zwracająca wartość dla podanego klucza *//
    public V Search(K key)
    {
        return values[GetIndex(key)];
    }
    
    //* Pomocnicza funkcja zwracająca (int) index dla podanego klucza *//
    private int GetIndex(K key)
    {
        for (int i = 0; i < size; i++)
        {
            if (keys[i].Equals(key))
            {
                return i;
            }
        }
        
        throw new Exception($"Klucz '{key}' nie został znaleziony w słowniku.");
    }
}