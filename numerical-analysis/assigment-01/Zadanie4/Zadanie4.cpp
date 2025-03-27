#include <iostream>
#include <cmath> 

int main()
{
    int n;
    std::cout << "Podaj \"n\": ";
    std::cin >> n;

    float single_y0 = 1.0;
    float single_y1 = -(1.0/6.0);

    double double_y0 = 1.0;
    double double_y1 = -(1.0/6.0);

    // Dla arytmetyki pojedynczej precyzji
    std::cout << "Wyniki dla arytmetyki pojedynczej precyzji:" << std::endl;
    std::cout << "===========================================" << std::endl;

    for(int i = 3; i <= n; i++)
    {
        float single_yn = 35.0/6.0 * single_y1 + single_y0;
        single_y0 = single_y1;
        single_y1 = single_yn;

        std::cout << i << ". ";
        std::cout << single_yn << std::endl;
    }
    std::cout << std::endl;

    // Dla arytmetyki podwojnej precyzji
    std::cout << "Wyniki dla arytmetyki podwojnej precyzji:" << std::endl;
    std::cout << "========================================" << std::endl;
     
    for(int i = 3; i <= n; i++)
    {
        double double_yn = 35.0/6.0 * double_y1 + double_y0;
        double_y0 = double_y1;
        double_y1 = double_yn;

        std::cout << i << ". ";
        std::cout << double_yn << std::endl;
    }

}