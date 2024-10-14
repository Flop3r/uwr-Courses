#include <iostream>
#include <cmath>

float f_single(float x)
{
    return 1518 * ((2*x - sin(2*x)) / pow(x,3));
}
           
double f_double(double x)
{
    return 1518 * ((2*x - sin(2*x)) / pow(x,3));
}

int main()
{
    int n;
    std::cout << "Podaj \"n\":";
    std::cin >> n;
  
    // Dla arytmetyki pojedynczej precyzji
    std::cout << "Wyniki dla arytmetyki pojedynczej precyzji:" << std::endl;
    std::cout << "===========================================" << std::endl;

    for(int i = 0; i <= n; i++)
    {
        std::cout << i << ". ";
        std::cout << f_single(pow(10, -i)) << std::endl;
    }
    std::cout << std::endl;

    // Dla arytmetyki podwojnej precyzji
    std::cout << "Wyniki dla arytmetyki podwojnej precyzji:" << std::endl;
    std::cout << "========================================" << std::endl;
     
    for(int i = 0; i <= n; i++)
    {
        std::cout << i << ". ";
        std::cout << f_double(pow(10, -i)) << std::endl;
    }

}