/* 
  Made on Dec 19, 2020
  By MehranMaleki @ Electropeak
  Home
*/
#define Thumb_Hall_Sensor_Pin A0
#define Index_Hall_Sensor_Pin A1
#define Middle_Hall_Sensor_Pin A2
#define Ring_Hall_Sensor_Pin A3
#define Pinky_Hall_Sensor_Pin A4

void setup() {
  pinMode(Thumb_Hall_Sensor_Pin, INPUT);
  pinMode(Index_Hall_Sensor_Pin, INPUT);
  pinMode(Middle_Hall_Sensor_Pin, INPUT);
  pinMode(Ring_Hall_Sensor_Pin, INPUT);
  pinMode(Pinky_Hall_Sensor_Pin, INPUT);
  Serial.begin(9600);
}

void loop() {
  int voltages[5];
  voltages[0] = analogRead(Thumb_Hall_Sensor_Pin);
  voltages[1] = analogRead(Index_Hall_Sensor_Pin);
  voltages[2] = analogRead(Middle_Hall_Sensor_Pin);
  voltages[3] = analogRead(Ring_Hall_Sensor_Pin);
  voltages[4] = analogRead(Pinky_Hall_Sensor_Pin);
  Serial.println(String(voltages[0]) + "," + String(voltages[1]) + "," + String(voltages[2]) + "," + String(voltages[3]) + "," + voltages[4]);
  delay(200);
}
