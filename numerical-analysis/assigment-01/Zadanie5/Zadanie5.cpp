#include <iostream>
#include <cmath>

int main()
{
    int n = 20;
    
    float last_single = log2(2025/2024);
    double last_double = log2(2025/2024);

    // Dla arytmetyki pojedynczej precyzji
    std::cout << "Wyniki dla arytmetyki pojedynczej precyzji:" << std::endl;
    std::cout << "===========================================" << std::endl;

    for(int i = 1; i <= n; i++)
    {
        last_single = 1/i - 2024*last_single;
        std::cout << "I_" << i << "= ";
        std::cout << last_single << std::endl;
    }
    std::cout << std::endl;

    // Dla arytmetyki podwojnej precyzji
    std::cout << "Wyniki dla arytmetyki podwojnej precyzji:" << std::endl;
    std::cout << "========================================" << std::endl;
     
    for(int i = 1; i <= n; i++)
    {
        last_double = 1/i - 2024*last_double;
        std::cout << "I_" << i << "= ";
        std::cout << last_double << std::endl;
    }

}