#include <iostream>
#include <cmath>

double my_sin(double x);

// Funkcje pomocnicze
double factorial(int n);
double power(double x, int n);

int main()
{   
    std::cout << my_sin(M_PI*2) << std::endl;
}

// Wykorzystałem rozwinięcie sinusa w szereg taylora
//
// sin(x) = x - x^3/3! + x^5/5! - x^7/7! + ... =
// Σ [od i=0 do ∞] (-1)^i * x^(2i + 1) / (2i + 1)!

double my_sin(double x)
{
    double y = 0.0;
    
    // Z okresowości sinusa dostosowuje x do mniejszej wartości 
    while(x > M_PI){
        x = x - 2*M_PI;
    }
    x = (x == M_PI) ? 0 : x;
    
    // Wyliczam
    for(int i = 0; i <= 2; i++)
    {
        y += power(-1, i) * power(x, 2*i+1) / factorial(2*i + 1);
        //std::cout << y << std::endl;
    }
    return y;
}

// Funkcje pomocnicze
double power(double x, int n)
{
    double result = 1.0;

    for(int i = 0; i < n; i++)
    {
        result *= x;
    }
    return result;
}
double factorial(int n)
{
    double result = 1.0;

    for(int i = 1; i <= n; i++)
    {
        result *= i;
    }

    return result;
}
