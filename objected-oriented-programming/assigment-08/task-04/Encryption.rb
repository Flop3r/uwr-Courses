# Franciszek Przeliorz
# Uniwerystet Wrocławski
# Kurs: Programowanie Obiektowe
# Lista 8 : Zadanie 2
#
# Compilation command: ruby Zadanie2.rb

# Klasa przechowującą napis w postaci jawnej
class Explicit
  # Konstruktor
  def initialize(text)
    @text = text.downcase
  end

  # Metoda szyfrująca
  def encrypt(key)
    encrypted_text = ""

    @text.each_char do |letter|
      encrypted_text += key[letter] || letter
    end

    Encrypted.new(encrypted_text)
  end

  # Sprzężenie metoda to_string
  def to_s
    @text
  end
end

# Klasa przechowującą napis zaszyfrowany
class Encrypted
  # Konstruktor
  def initialize(text)
    @text = text.downcase
  end

  # Metoda deszyfrująca
  def decrypt(key)
    decrypted_text = ""

    @text.each_char do |letter|
      decrypted_text += key.invert[letter] || letter
    end

    Explicit.new(decrypted_text)
  end

  # Sprzężenie metody to_string
  def to_s
    @text
  end
end


### TESTY ###

# Słownik szyfrujący
dictionary_ruby = {
  'r' => 'y',  'u' => 'a', 'b' => 'r', 'y' => 'u'
}

# Testowanie klasy Explicit
puts "TEST METODY to_s DLA KLASY EXPLICIT:"

# Test dla tekstu napisanego małymi literami,
expl_text = Explicit.new("ruby")

puts "Dla \"ruby\" oczekiwana wartość: ruby"
puts "Wynik: " + expl_text.to_s
puts

# Test dla tekstu napisanego małymi i dużymi literami,
expl_text = Explicit.new("RuBy")

puts "Dla \"RuBy\" oczekiwana wartość: ruby"
puts "Wynik: " + expl_text.to_s
puts "======================================="

# Testowanie metody encrypt klasy Explicit
puts "TEST METODY ENCRYPT KLASY EXPLICIT:"

# Test dla tekstu, którego wszystkie litery są w słowniku
expl_text = Explicit.new("RuBy")
encr_text = expl_text.encrypt(dictionary_ruby)

puts "Dla \"RuBy\" oczekiwana wartość: yaru"
puts "Wynik: " + encr_text.to_s
puts

# Test dla tekstu, którego nie wszystkie litery są w słowniku
expl_text = Explicit.new("RuBBer")
encr_text = expl_text.encrypt(dictionary_ruby)

puts "Dla \"RuBBer\" oczekiwana wartość: yarrey"
puts "Wynik: " + encr_text.to_s
puts "======================================="


# Testowanie klasy Encrypted
puts "TEST METODY to_s DLA KLASY ENCRYPTED:"

# Test dla tekstu napisanego małymi literami,
encr_text = Encrypted.new("yaru")

puts "Dla \"yaru\" oczekiwana wartość: yaru"
puts "Wynik: " + encr_text.to_s
puts

# Test dla tekstu napisanego małymi i dużymi literami,
encr_text = Encrypted.new("yarU")

puts "Dla \"yarU\" oczekiwana wartość: yaru"
puts "Wynik: " + encr_text.to_s
puts "======================================="

# Testowanie metody DEcrypt klasy Explicit
puts "TEST METODY DECRYPT KLASY ENCRYPTED:"

# Test dla tekstu, którego wszystkie litery są w słowniku
encr_text = Encrypted.new("yarU")
expl_text = encr_text.decrypt(dictionary_ruby)

puts "Dla \"yarU\" oczekiwana wartość: ruby"
puts "Wynik: " + expl_text.to_s
puts

# Test dla tekstu, którego nie wszystkie litery są w słowniku
encr_text = Encrypted.new("yaRRey")
expl_text = encr_text.decrypt(dictionary_ruby)

puts "Dla \"yaRRey\" oczekiwana wartość: rubber"
puts "Wynik: " + expl_text.to_s


