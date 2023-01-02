#include <Adafruit_IS31FL3741.h>

// Color definitions
#define BLACK    0x0000
#define BLUE     0x001F
#define RED      0xF800
#define GREEN    0x07E0
#define CYAN     0x07FF
#define MAGENTA  0xF81F
#define YELLOW   0xFFE0 
#define WHITE    0xFFFF

#define ANIMATION_DELAY_MS 100

Adafruit_IS31FL3741_QT_buffered leftMatrix;
Adafruit_IS31FL3741_QT_buffered rightMatrix;

TwoWire *i2c = &Wire;

long timer = 0;
int vertL = 1;
int horL = 1;
int vertR = 1;
int horR = 1;
int xL = 6;
int yL = 4;
int xR = 6;
int yR = 4;
int rL = 2;
int rR = 2;
float speed = 0;

const byte numChars = 32;
char receivedChars[numChars]; // an array to store the received data

boolean newData = false;

void recvWithEndMarker() {
  static byte ndx = 0;
  char endMarker = '\n';
  char rc;

  while (Serial.available() > 0 && newData == false) {
    rc = Serial.read();

    if (rc != endMarker) {
      receivedChars[ndx] = rc;
      ndx++;
      if (ndx >= numChars) {
        ndx = numChars - 1;
      }
    }
    else {
      receivedChars[ndx] = '\0'; // terminate the string
      ndx = 0;
      newData = true;
    }
  }
}


void setLEDsOn(bool on) {
  leftMatrix.enable(on);
  rightMatrix.enable(on);
}

void draw() {
  leftMatrix.fill();
  rightMatrix.fill();

  leftMatrix.fillTriangle(xL, yL, xL + rL, yL + rL, xL - rL, yL + rL, RED);
  rightMatrix.fillTriangle(xR, yR, xR + rR, yR + rR, xR - rR, yR + rR, RED);
  
  leftMatrix.show();
  rightMatrix.show();
}

void setup() {
  Serial.begin(115200);

  if (!leftMatrix.begin(IS3741_ADDR_DEFAULT, i2c)) {
    Serial.println("Matrix 1 not found on 0x30");
    while (1);
  }
  if (!rightMatrix.begin(0x31, i2c)) {
    Serial.println("Matrix 2 not found on 0x31");
    while (1);
  }


  i2c->setClock(400000);

  // Set brightness to max and bring controller out of shutdown state
  leftMatrix.setLEDscaling(0x60);
  leftMatrix.setGlobalCurrent(0xFF);
  leftMatrix.enable(true);
  rightMatrix.setLEDscaling(0x60);
  rightMatrix.setGlobalCurrent(0xFF);
  rightMatrix.enable(true);

  draw();
}

void animate(int delayTime) {
  if (millis() > timer + delayTime) {
    timer = millis();

    draw();
    
    yL = (yL - vertL) % 9;
    if (yL < -2 * rL) {
      yL = 8;
    }

    yR = (yR - vertR) % 9;
    if (yR < -2 * rR) {
      yR = 8;
    }
  }
}

void loop() {
  recvWithEndMarker();
  if (newData) {
    String command = receivedChars;

    if (command.equals("on")) {
      setLEDsOn(true);
    }
    else if (command.equals("off")) {
      setLEDsOn(false);
    }
    else {
      speed = command.toFloat();
    }

    newData = false;
  }

  leftMatrix.setRotation(speed >= 0 ? 0 : 2);
  rightMatrix.setRotation(speed >= 0 ? 0 : 2);

  if (speed != 0.0f) {
    animate(ANIMATION_DELAY_MS / abs(speed));
  }
}
