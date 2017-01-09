# Introduction 
Activity monitor developed for Windows 10 IoT Core running on a Raspberry Pi 2 or 3. Detects motion using passive infrared sensor and blinks Phillips Hue lights when motion is detected.

Blog Post: http://hunter-dev.com/activity-monitor-using-raspberry-pi-with-windows-10-iot-core/
YouTube Video: https://www.youtube.com/watch?v=Qh0cJH1ecH8

# Getting Started
1.	Follow steps at https://developer.microsoft.com/en-us/windows/iot/GetStarted to setup your Raspberry Pi with IoT Core. Wire up the motion sensor to GPIO pin 26 on the Pi.
2.	Register your Hue App by following the steps at https://www.developers.meethue.com/documentation/getting-started. 
3.  Pass your Hue app key into the HueLightsNotification constructor.
4.  Build and deploy app onto Raspberry Pi. Enjoy!