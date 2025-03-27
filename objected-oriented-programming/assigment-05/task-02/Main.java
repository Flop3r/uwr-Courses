/* Franciszek Przeliorz                   */
/* Uniwerystet Wrocławski                 */
/* Kurs: Programowanie Obiektowe          */
/* Lista 5 : Zadanie 2                    */
/*                                        */
/* Compilation command: javac Main.java   */

//* Klasa reprezentująca wyrażenia matematyczne. *//
abstract class Expression
{
    // Metoda zwracającą wartość wyrażenia.
    public abstract int evaluate();

    //Metoda zwracającą reprezentację tekstową wyrażenia.
    public abstract String toString();
}

//* Klasa reprezentująca zmienną w wyrażeniu *//
class Var extends Expression
{
    private int value; // Wartość zmiennej
    private String name; // Nazwa zmiennej

    // Konstruktor
    public Var(String name)
    {
        this.name = name;
        value = 0;
    }

    // Metoda ustawiająca wartość zmiennej
    public void setValue(int value)
    {
        this.value = value;
    }

    @Override
    public int evaluate()
    {
        return value;
    }

    @Override
    public String toString()
    {
        return name;
    }
}

//* Klasa reprezentująca stałą w wyrażeniu *//
class Const extends Expression
{
    private int value; // Wartość stałej

    // Konstruktor
    public Const(int value)
    {
        this.value = value;
    }

    @Override
    public int evaluate()
    {
        return value;
    }

    @Override
    public String toString()
    {
        return Integer.toString(value);
    }
}

//* Klasa reprezentująca operację dodawania dwóch wyrażeń. *//
class Add extends Expression
{
    private Expression left, right; // Argumenty operacji

    // Konstruktor
    public Add(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    @Override
    public int evaluate()
    {
        return left.evaluate() + right.evaluate();
    }

    @Override
    public String toString()
    {
        return "(" + left.toString() + " + " + right.toString() + ")";
    }
}


//* Klasa reprezentująca operację mnożenia dwóch wyrażeń *//
class Mult extends Expression
{
    private Expression left, right; // Argumenty operacji


    // Konstruktor
    public Mult(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    @Override
    public int evaluate()
    {
        return left.evaluate() * right.evaluate();
    }

    @Override
    public String toString()
    {
        return "(" + left.toString() + " * " + right.toString() + ")";
    }
}


//* Klasa wywoławcza *//
public class Main {
    public static void main(String[] args) {

        // Inicjacja klasy dwóch zmiennych (obiektów typu Var)
        Var x = new Var("x");
        Var y = new Var("y");

        // Test ustawienia wartości zmiennych
        x.setValue(2);
        y.setValue(3);

        // Tworzenie drzewa wyrażeń
        Expression expr1 = new Add(new Const(4), x);
        Expression expr2 = new Mult(new Const(5), x);
        Expression expr3 = new Mult(new Add(new Const(2), x), new Const(1));
        Expression expr4 = new Add(new Mult(new Const(2), x), new Const(1));
        Expression expr5 = new Mult(x, y);

        // Test metody toString()
        System.out.println("TEST METODY toString():\n");

        System.out.println("Wyrażenie 1: " + expr1.toString());
        System.out.println("Oczekiwana wynik: (4 + x)\n");

        System.out.println("Wyrażenie 2: " + expr2.toString());
        System.out.println("Oczekiwana wynik: (5 * x)\n");

        System.out.println("Wyrażenie 3: " + expr3.toString());
        System.out.println("Oczekiwana wynik: ((2 + x) * 1)\n");

        System.out.println("Wyrażenie 4: " + expr4.toString());
        System.out.println("Oczekiwana wynik: ((2 * x) + 1)\n");

        System.out.println("Wyrażenie 5: " + expr5.toString());
        System.out.println("Oczekiwana wynik: (x * y)\n");

        System.out.println("==========================================\n");

        // Test metody evaluate() na wyrażeniach
        System.out.println("TEST METODY evaluate() NA WYRAŻENIACH:\n");

        System.out.println("Wartość wyrażenia " + expr1.toString() + " dla x=2: " + expr1.evaluate());
        System.out.println("Oczekiwana wartość: 6");
        System.out.println();

        System.out.println("Wartość wyrażenia " + expr2.toString() + " dla x=2: " + expr2.evaluate());
        System.out.println("Oczekiwana wartość: 10");
        System.out.println();

        System.out.println("Wartość wyrażenia " + expr3.toString() + " dla x=2: " + expr3.evaluate());
        System.out.println("Oczekiwana wartość: 4");
        System.out.println();

        System.out.println("Wartość wyrażenia " + expr4.toString() + " dla x=2: " + expr4.evaluate());
        System.out.println("Oczekiwana wartość: 5");
        System.out.println();

        System.out.println("Wartość wyrażenia " + expr5.toString() + " dla x=2 i y=3: " + expr4.evaluate());
        System.out.println("Oczekiwana wartość: 6");
        System.out.println();

        System.out.println("==========================================\n");

        // Test zmiany wartości zmiennych
        System.out.println("TEST ZMIANY WARTOŚCI ZMIENNYCH:\n");
        x.setValue(3);

        System.out.println("Wartość wyrażenia " + expr1.toString() + " dla x=3: " + expr1.evaluate());
        System.out.println("Oczekiwana wartość: 7\n");
    }
}
