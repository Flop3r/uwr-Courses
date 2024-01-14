#include <stdio.h>
#include <stdlib.h>

/*
 * Franciszek Przeliorz
 * Lista 1: Zadanie 2
 * gcc Zad2.c
 */        

typedef struct{
    int nom;
    int denom;
}Ulamek;


Ulamek *nowy_ulamek(int nom, int denom); //*Funkcja tworząca nowy ułamek w postaci uproszczonej
void show(Ulamek *u); //*Funkcja wypisującą wartość ułamka;

//*Funkcje operacji na ułamkach (zwraca wskaźnik do nowoutworzonego ułamka)*//
Ulamek *addFrac(Ulamek *ux, Ulamek *uy);
Ulamek *subFrac(Ulamek *ux, Ulamek *uy);
Ulamek *multFrac(Ulamek *ux, Ulamek *uy);
Ulamek *divFrac(Ulamek *ux, Ulamek *uy);

//*Funkcje operacji na ułamkach (modyfikuje drugi z argumentów)*//
void addFrac_mod(Ulamek *ux, Ulamek *uy);
void subFrac_mod(Ulamek *ux, Ulamek *uy);
void multFrac_mod(Ulamek *ux, Ulamek *uy);
void divFrac_mod(Ulamek *ux, Ulamek *uy);

//*Funkcje pomocniczne*//
int NWD(int x, int y); //*Zwraca największy wspolny dzielnik
void simplify(Ulamek *u); //*Upraszca ułamemk
void expand(Ulamek *ux, Ulamek *uy); //Rozszerza ułamki do ułamkow o wspolnym mianowniku

int main(){
    Ulamek *u1 = nowy_ulamek(0, 3);
    Ulamek *u2 = nowy_ulamek(-3, 5);

    show(u1);
    show(u2);

    Ulamek *u3 = addFrac(u1, u2);
    show(u3);

    multFrac_mod(u2,u3);
    show(u3);
}

//* Funkcje pomocniczne *//
int NWD(int x, int y)
{
    while(x!=y)
       if(x>y)
           x-=y; 
       else
           y-=x; 
    return x; 
}

void simplify(Ulamek *u){
    int nom = u->nom;
    int denom = u->denom;
    int n = 1, m = 1;

    if(nom < 0){
        nom *= -1;
        n = -1;
    }
    if(denom < 0){
        denom *= -1;
        m = -1;
    }

    int p = 1;    

    if(nom > 0){
        p = NWD(nom, denom);
    }else nom = 0;

    u->nom = nom/p * n * m;
    u->denom = denom/p;
}

void expand(Ulamek *ux, Ulamek *uy){
    int xNom = ux->nom;
    int xDenom = ux->denom;
    int yNom = uy->nom;
    int yDenom = uy->denom;

    ux->nom *= yDenom;
    uy->nom *= xDenom;

    ux->denom *= yDenom;
    uy->denom *= xDenom; 
}

//* Nowy ułamek *//
Ulamek *nowy_ulamek(int nom, int denom){
    Ulamek *u = malloc(sizeof(Ulamek));
    u->nom = nom;
    u->denom = denom;
    simplify(u);
    return u;
}

//* Wyświetlenie ułamka *//
void show(Ulamek *u){
    if(u->nom == 0){
        printf("0\n");
    }else if(u->denom > 1)
        printf("%d/%d\n", u->nom, u->denom);
    else if(u->denom == 1){
        printf("%d\n", u->nom);
    }
}

//*Funkcje operacji na ułamkach (zwraca wskaźnik do nowoutworzonego ułamka)*//
Ulamek *addFrac(Ulamek *ux, Ulamek *uy){
    expand(ux, uy);
    int xNom = ux->nom;
    int xDenom = ux->denom;
    int yNom = uy->nom;
    int yDenom = uy->denom;

    return nowy_ulamek(xNom + yNom, yDenom);
}

Ulamek *subFrac(Ulamek *ux, Ulamek *uy){
    expand(ux, uy);
    int xNom = ux->nom;
    int xDenom = ux->denom;
    int yNom = uy->nom;
    int yDenom = uy->denom;

    return nowy_ulamek(xNom - yNom, yDenom);
}

Ulamek *multFrac(Ulamek *ux, Ulamek *uy){
    Ulamek *u = malloc(sizeof(Ulamek));
    int xNom = ux->nom;
    int xDenom = ux->denom;
    int yNom = uy->nom;
    int yDenom = uy->denom;

    return nowy_ulamek(xNom * yNom, yDenom * xDenom);
}

Ulamek *divFrac(Ulamek *ux, Ulamek *uy){
    Ulamek *u = malloc(sizeof(Ulamek));
    int xNom = ux->nom;
    int xDenom = ux->denom;
    int yNom = uy->nom;
    int yDenom = uy->denom;

    return nowy_ulamek(xNom * yDenom, xDenom * yNom);
}

//*Funkcje operacji na ułamkach (modyfikuje drugi z argumentów)*//
void addFrac_mod(Ulamek *ux, Ulamek *uy){
    expand(ux, uy);
    int xNom = ux->nom;
    int xDenom = ux->denom;
    int yNom = uy->nom;
    int yDenom = uy->denom;

    uy->nom = xNom + yNom;
    uy->denom = yDenom;
    
    simplify(uy);
}

void subFrac_mod(Ulamek *ux, Ulamek *uy){
    expand(ux, uy);
    int xNom = ux->nom;
    int xDenom = ux->denom;
    int yNom = uy->nom;
    int yDenom = uy->denom;

    uy->nom = xNom - yNom;
    uy->denom = yDenom;
    
    simplify(uy);
}

void multFrac_mod(Ulamek *ux, Ulamek *uy){
    int xNom = ux->nom;
    int xDenom = ux->denom;
    int yNom = uy->nom;
    int yDenom = uy->denom;

    uy->nom = xNom * yNom;
    uy->denom = yDenom * xDenom;
    
    simplify(uy);
}

void divFrac_mod(Ulamek *ux, Ulamek *uy){
    int xNom = ux->nom;
    int xDenom = ux->denom;
    int yNom = uy->nom;
    int yDenom = uy->denom;

    uy->nom = xNom * yDenom;
    uy->denom = xDenom * yNom;
    
   simplify(uy);
}