#include <LiquidCrystal.h>

#define trigPin 32
#define echoPin 21
#define sicaklik_pin 36
#define uzaklik_pin 37

LiquidCrystal lcd(8,9,4,5,6,7);

float sicaklik;
float analoggerilim;
float mesafesiniri = 100.0;
float sicakliksiniri = 20.0;

void setup () {
Serial.begin(9600);
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  pinMode(sicaklik_pin, OUTPUT);
  pinMode(uzaklik_pin, OUTPUT);
  lcd.begin(16, 2);
}

void loop () {
  
  lcd.setCursor(0, 0);
  if (Serial.available()) {
    String gelen = Serial.readString();
    if (gelen[0] == 's') {
      sicakliksiniri = (gelen.substring(1, gelen.length()-1)).toFloat();
      lcd.print("1girdi");
    }
    else if (gelen[0] == 'u') {
      mesafesiniri = (gelen.substring(1, gelen.length()-1)).toFloat();
      lcd.print("2girdi");
    }
  }
  analoggerilim = analogRead(A15);
  analoggerilim = (analoggerilim/1023)*5000;
  sicaklik = analoggerilim /10,0;

  String lcd_ekran_sonucu = String(sicaklik) + " derece               ";  
  lcd.print(lcd_ekran_sonucu);
  lcd.setCursor(0, 1);
  long sure, mesafe;
  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);
  sure = pulseIn(echoPin, HIGH);
  mesafe = (sure/2) / 29.1;
  
  Serial.println(String(mesafe) + "-" + String(sicaklik));
  
  if (mesafe >= 200 || mesafe <= 0) {
    lcd.print("Mesafe disi!");
    digitalWrite(uzaklik_pin, LOW);
  }
  else if (mesafe <= mesafesiniri) {
    
    lcd.print(String(mesafe) + " cm               ");
    digitalWrite(uzaklik_pin, HIGH);
  }
  else if (mesafe > mesafesiniri) {
    lcd.print(String(mesafe) + " cm               ");
    digitalWrite(uzaklik_pin, LOW);
  }
  if (sicaklik >= sicakliksiniri) {
    digitalWrite(sicaklik_pin, HIGH);
  }
  else if (sicaklik < sicakliksiniri) {
    digitalWrite(sicaklik_pin, LOW);
  }
 
 delay (100);
  
}



