void setup() {
  Serial.begin(9600);
  pinMode(0,OUTPUT);
}

void loop() {
  int valT = analogRead(0);
  float mV = (valT / 1024.0) * 5000;
  float temperature = (mV - 500)  / 10;

  Serial.print("Celsius: ");
  Serial.println(temperature);

  delay(60000); //1 minute
}
