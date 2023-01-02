#include <Wire.h>
#include "Qwiic_LED_Stick.h" // Click here to get the library: http://librarymanager/All#SparkFun_Qwiic_LED_Stick

#define LEFT_STICK_ADDRESS 0x22
#define RIGHT_STICK_ADDRESS 0x23
#define LED_BRIGHTNESS 15
#define ANIMATION_DELAY_MS 100

LED leftLedStick; 
LED rightLedStick;

bool ledsOn = true;
int animationDirection = 0;
int animationDelayMs = ANIMATION_DELAY_MS;
long timer = 0;
int i = 0;
  

void setup() {
  Wire.begin();
  Serial.begin(115200);
  
  //Start up communication with the LED Stick 1
  if (leftLedStick.begin(LEFT_STICK_ADDRESS) == false){
    Serial.println("Qwiic LED Stick 1 failed to begin. Please check wiring and try again!");
    while(1);
  }
  
  Serial.println("Qwiic LED Stick 1 ready!");
  //Set the whole 1st LED Stick dim white
  leftLedStick.setLEDColor(10, 0, 0);
  
  //Start up communication with another LED Stick 2 at address 0x29
  if (rightLedStick.begin(RIGHT_STICK_ADDRESS) == false){
    Serial.println("Qwiic LED Stick 2 failed to begin. Please check wiring and try again!");
    while(1);
  }

  Serial.println("Qwiic LED Stick 2 ready!");

  //Set the whole 2nd LED Stick dim white
  rightLedStick.setLEDColor(10, 0, 0);
}

void setLEDsOn(bool on) {
  if (on) {
    // Prevent flickering when LEDs are already on
    if (!ledsOn) {
      leftLedStick.setLEDBrightness(LED_BRIGHTNESS);
      rightLedStick.setLEDBrightness(LED_BRIGHTNESS);     
    }
  }
  else {
    leftLedStick.setLEDBrightness(0);
    rightLedStick.setLEDBrightness(0);
  }

  ledsOn = on;
}

void animate(int animationDirection, int delayTime) {
  if (millis() > timer + delayTime) {
    timer = millis();
    
    leftLedStick.setLEDBrightness(0);
    rightLedStick.setLEDBrightness(0);

    if (animationDirection == 1) {
      leftLedStick.setLEDBrightness(9 - i, LED_BRIGHTNESS); 
    }
    else if (animationDirection == 2) {
      rightLedStick.setLEDBrightness(9 - i, LED_BRIGHTNESS);
    }
    else if (animationDirection == 3) {
      leftLedStick.setLEDBrightness(9 - i, LED_BRIGHTNESS);
      rightLedStick.setLEDBrightness(9 - i, LED_BRIGHTNESS); 
    } 

    i++;
    if (i > 9) {
      i = 0;
    }
  }
}

void loop() {
  static char command;

  command = Serial.read();

  switch (command) {
    case '0':
      setLEDsOn(false);
      break;
    case '1':
      setLEDsOn(true);
      break;
    case 'R':
      animationDirection = 2;
      break;
    case 'L':
      animationDirection = 1;
      break;
    case 'B':
      animationDirection = 3;
      break;
  }

  if (ledsOn) {
    animate(animationDirection, animationDelayMs);   
  }
}
