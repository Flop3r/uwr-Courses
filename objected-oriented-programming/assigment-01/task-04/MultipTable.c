#include <stdio.h>
#include <stdlib.h>
#include <math.h>

/*
 * Franciszek Przeliorz
 * Uniwerystet Wrocławski
 * Kurs: Programowanie Obiektowe
 * Lista 1: Zadanie 4
 * 
 * Compilation command: gcc MultipTable.c
 */ 

//* Funckja drukującą w standardowym wyjściu tabliczkę mnożenia dla przedziału [x1, x2) x [y1, y2)
void tabliczka(float x1, float x2, float y1, float y2, float skok);

int main(){
    tabliczka(0, 9, 0, 9, 0.7);
}

void tabliczka(float x1, float x2, float y1, float y2, float skok){
    int x_size = (x2 - x1) / skok + 1;
    int y_size = (y2 - y1) / skok + 1;
    float *x = malloc(x_size * sizeof(float));
    float *y = malloc(y_size * sizeof(float));

    int space_n = 1;
    int p = floor(x2 * y2);

    while(p % 10){ //Obliczenie ilości spacji pomiędzy kolumnami
        space_n++;
        p /= 10;
    }

    for (int i = 0; i < x_size; i++) { //Inicjowanie listy elementow x
        x[i] = x1 + i * skok;
    }
    for (int i = 0; i < y_size; i++) { //Inicjowanie listy elementow y
        y[i] = y1 + i * skok;
    }
    
    for(int i = 0; i < 7; i++){ //"Drukowanie"
        putchar(' ');
    }

    for (int i = 0; i < y_size; i++) {
        printf("%.2f", y[i]);
        for(int n = space_n; n > 0; n--){
                putchar(' ');
        }
    }
    putchar('\n');

    for (int i = 0; i < x_size; i++) {
        printf("%.2f:  ", x[i]);

        for (int j = 0; j < y_size; j++) {
            printf("%.2f", x[i] * y[j]);

            for(int k = space_n; k > 0; k--){
                putchar(' ');
            }
        }
        putchar('\n');
    }
}
