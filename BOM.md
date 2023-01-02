# MoPeDT - Bill of materials (BOM)

## Basic components

| Component    | Quantity | Link | Comment |
|--------------|----------|------|---------|
| PLA filament | >= 1 spool (1 kg) | |
| M3x5.7 threaded insert | >= 12 | https://amzn.eu/d/7vHdi4a |
| M3 screws    | >= 12   | https://amzn.eu/d/j1Tz68p | Used throughout the hole headset |
| M2x4 threaded insert | >= 12 | https://amzn.eu/d/aU0sRlk | Used throughout the hole headset |
| SparkFun Qwiic Cable Kit | 2 | https://www.sparkfun.com/products/15081 | Required when using Arduino-based modules | 
| USB extension cable | 3 | | Recommended to use shorter cables on the headset itself for easier handling and use extension when necessary |

## Tracking

## SteamVR tracking module

| Component | Quantity | Link | Comment | 
|-----------|----------|------|---------|
| Vive Tracker 3.0 or similar | 1 | https://www.vive.com/de/accessory/tracker3/ | For SteamVR tracking |

## Intel RealSense T265 module

| Component | Quantity | Link | Comment | 
|-----------|----------|------|---------|
| Intel RealSense T265 | 1 | https://www.intelrealsense.com/tracking-camera-t265/ | For usage without SteamVR or outdoor usage |
| Angled micro USB 3.0 cable | 1 | https://amzn.eu/d/ewdhcyj | Recommended for strain relief of the RealSense cable |

## IMU tracking module (rotation only)

| Component | Quantity | Link | Comment | 
|-----------|----------|------|---------|
| Arduino Nano 33 BLE Sense | 1 | https://store.arduino.cc/products/arduino-nano-33-ble-sense | IMU tracking (without position) |
| SparkFun VR IMU Breakout - BNO080 (Qwiic) | 1 | https://www.sparkfun.com/products/14686 | Improved IMU accuracy due to integrated sensor fusion |

## Display modules

### 3.1" LCD module

| Component    | Quantity | Link | Comment |
|--------------|----------|------|---------|
| 3.1" 720x720 display with HDMI adapter board | 1 | https://de.aliexpress.com/item/32975602421.html | 90 Hz |
| HDMI cable | 1 |
| Micro USB cable | 1 |


### See-through AR module

| Component    | Quantity | Link | Comment |
|--------------|----------|------|---------|
| 2.9" 1440x1440 with HDMI adapter board | 1 | https://de.aliexpress.com/item/4000010166124.html | 90 Hz maximum. Print the dual HDMI board holder |
| 2x 2.9" 1440x1440 with DisplayPort adapter board | 1 | https://de.aliexpress.com/item/32976735089.html | 120 Hz. Print the DP board holder. |
| Mini DisplayPort cable | 1 |
| Micro USB cable | 1 |

## Arduino-based modules using Qwiic

Require IMU tracking module that houses Arduino.

### LED strip module

| Component    | Quantity | Link | Comment |
|--------------|----------|------|---------|
| SparkFun Qwiic LED Stick - APA102C | 1 | https://www.sparkfun.com/products/18354 |

### LED matrix module

| Component    | Quantity | Link | Comment |
|--------------|----------|------|---------|
| Adafruit IS31FL3741 13x9 PWM RGB LED Matrix Driver - STEMMA QT / Qwiic | 1 | https://www.adafruit.com/product/5201 |

### OLED module

| Component    | Quantity | Link | Comment |
|--------------|----------|------|---------|
| SparkFun Transparent Graphical OLED Breakout (Qwiic) | 1 | https://www.sparkfun.com/products/15173 |
