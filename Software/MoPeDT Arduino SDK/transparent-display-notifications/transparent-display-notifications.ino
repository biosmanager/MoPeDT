#include "HyperDisplay_UG2856KLBAG01.h"   // Your library can be installed here: http://librarymanager/All#SparkFun_Transparent_Graphical_OLED
                                          // The rest of the Layer Cake:         http://librarymanager/All#SparkFun_HyperDisplay_SSD1309
                                          //                                     http://librarymanager/All#SparkFun_HyperDisplay

UG2856KLBAG01_I2C myTOLED;  // Declare a I2C-based Transparent OLED object called myTOLED

void setDirectionRight() {
  myTOLED.setScroll(false);
  myTOLED.contHScrollSetupRight(0, 7, 7, 0, 127);
  myTOLED.setScroll(true);
}

void setDirectionLeft() {
  myTOLED.setScroll(false);
  myTOLED.contHScrollSetupLeft(0, 7, 7, 0, 127);
  myTOLED.setScroll(true);
}

void setDisplayOn(bool on) {
  myTOLED.setPower(on);
}

void drawCircle() {
  myTOLED.circleSet(myTOLED.xExt / 2, myTOLED.yExt / 2, myTOLED.yExt / 2 - 15, true);
}

void setup() {
  Serial.begin(9600);
  
  Wire.begin();
  myTOLED.begin(Wire, false, SSD1309_ARD_UNUSED_PIN);
  Wire.setClock(400000);

  delay(500);

  myTOLED.clearDisplay();
  drawCircle();
  myTOLED.setScroll(false);
  myTOLED.setPower(false);
}

void loop() {
  static char command;

  command = Serial.read();

  switch (command) {
    case '0':
      setDisplayOn(false);
      break;
    case '1':
      setDisplayOn(true);
      break;
    // Display is mounted from the back
    case 'R':
      setDirectionLeft();
      break;
    case 'L':
      setDirectionRight();
      break;
    case 'C':
      myTOLED.clearDisplay();
      break;
    case 'D':
      drawCircle();
      break;
    case 'S':
      myTOLED.setScroll(false);
      break;
  }
}
