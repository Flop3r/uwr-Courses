# Franciszek Przeliorz
# Uniwerystet Wrocławski
# Kurs: Programowanie Obiektowe
# Lista 8 : Zadanie 2
#
# Compilation command: ruby Zadanie2.rb

# Klasa reprezentująca wyrażenia arytmetyczne w postaci ONP
class ONP
  # Konstruktor
  def initialize(expression)
    @expression = expression

  end

  # Metoda obliczająca wyrażenie
  def calculate(vars)
    stack = [] # pomocniczy stos
    step = 0 # licznik kroku (pętli)

    # Wypisanie wyrażenia
    puts "Wyrażenie: #{@expression.join(" ")}"

    # Wypisanie podstawień jeżeli takie istnieją
    if !vars.empty?
      puts "Podstawienie: "

      vars.each do |key, val|
        puts "#{key} => #{val}"
      end
    end

    # Obliczanie wyrażenia oraz wypisanie kroków
    puts "Obliczanie: "
    @expression.each do |i|
      step += 1

      if i.is_a? Numeric # Jeżeli i jest liczbą dodaj na stos
        stack.push(i)

      elsif i != '+' && i != '-' &&
        i != '*' && i != '/' # Jeżeli i jest zmienną podstaw wartość i dodaj na stos

        stack.push(vars[i])

      else # Jeżeli i jest operatorem wykonaj operacje na dwóch ostatnich liczbach na stosie

        a = stack.pop
        b = stack.pop

        result = 0

        if i == '+'
          result = b + a
        elsif i == '-'
          result = b - a
        elsif i == '*'
          result = b * a
        elsif i == '/'
          result = b / a
        end

        stack.push(result)
      end

      # Wypisanie kroku, wejścia i aktualnego stanu stosu
      puts "Krok: #{step} | Input: #{i} | Stack: #{stack.join(", ")}"
    end

    # Zwrócenie obliczonej wartości
    return stack.pop
  end
end

# Testowanie metod klasy ONP

# Test1
# Test na prostym wyrażeniu bez zmiennych
puts "Test na prostym wyrażeniu bez zmiennych"
expression = ONP.new([2, 3, '+'])
expected_result = 5
result = expression.calculate({})

puts "Oczekiwany wynik: #{expected_result}"
puts "Wynik: #{result}"
puts "============================================="

# Test2
# Test na dłuższym wyrażeniu bez zmiennych
puts "Test na dłuższym wyrażeniu bez zmiennych"
expression = ONP.new([4, 2, '*', 3, 5, '*', '+'])
expected_result = 23
result = expression.calculate({})

puts "Oczekiwany wynik: #{expected_result}"
puts "Wynik: #{result}"
puts "============================================="

# Test3
# Test dłuższego wyrażenia ze zmiennymi
puts "Test dłuższego wyrażenia ze zmiennymi"
expression = ONP.new(['x', 2, '*', 1, '+'])
vars = {'x' => 3}
expected_result = 7
result = expression.calculate(vars)

puts "Oczekiwany wynik: #{expected_result}"
puts "Wynik: #{result}"
puts "============================================="

# Test4
# Test skompilkowanego wyrażenia
puts "Test skompilkowanego wyrażenia"
expression = ONP.new(['a', 5, '*', 'b', 'c', '*', '+', 15, '/', 'd', '+', 'e', 2, '/', '-'])
vars = {'a' => 2, 'b' => 3, 'c' => 4, 'd' => 6, 'e' => 8}
expected_result = 3
result = expression.calculate(vars)

puts "Oczekiwany wynik: #{expected_result}"
puts "Wynik: #{result}"


