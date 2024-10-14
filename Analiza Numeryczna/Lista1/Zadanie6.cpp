#include <iostream>
#include <cmath>

int main()
{
    int n;
    std::cout << "Podaj \"n\": ";
    std::cin >> n;

    float single_pi = 0.0;
    double double_pi = 0.0;

    // Dla arytmetyki pojedynczej precyzji
    std::cout << "Wyniki dla arytmetyki pojedynczej precyzji:" << std::endl;
    std::cout << "===========================================" << std::endl;

    for(int i = 0; i <= n; i++)
    {
        float formula = pow((-1.0),i) * (4.0/(2.0*i + 1));
        single_pi += formula;
    }

    std::cout << "pi= " << single_pi << std::endl << std::endl;

    // Dla arytmetyki podwojnej precyzji
    std::cout << "Wyniki dla arytmetyki podwojnej precyzji:" << std::endl;
    std::cout << "========================================" << std::endl;
     
    for(int i = 0; i <= n; i++)
    {
        double formula = pow((-1.0),i) * (4.0/(2.0*i + 1));
        double_pi += formula;
    }

    std::cout << "pi= " << double_pi << std::endl;

}