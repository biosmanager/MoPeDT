# MoPeDT - Assembly and setup

1. See CAD files and BOM to determine the desired configuration of MoPeDT.
2. Print the necessary parts. All parts except the alternative wider base frame can be printed on a Prusa i3 MK3(S/S+) with a 25×21×21 cm print volume.
3. Insert threaded inserts in the base frame.
4. Assemble tracking and display modules. If unsure about how to assemble a specific module, refer to the CAD model.

## Software setup

### PC-based display modules

1. Plug display cables in graphics card.
2. Set display configuration in Windows so that all MoPeDT displays are right to the normal desktop displays.

### Arduino-based modules

1. See `Software/MoPeDT Arduino SDK` for Arduino sketches.
2. Flash the firmware using Arduino IDE.

Afterwards, start the Arduino Unity SDK in Unity and open the main menu scene. Output on the peripheral PC-based display modules only works in a build. Select one of the example scenes.
