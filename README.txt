VisAR Navigation Aid README

Overview:
VisAR is a navigation aid for the visually impaired developed using augmented reality (AR) technology. This README provides information on the platform, compilation process, and usage instructions for the VisAR application.

Platform:
VisAR is developed using Unity 2022.3.11f1, a cross-platform game engine. The application is designed to run on the Meta Quest 3, providing users with a seamless navigation experience in their physical environment.

Compilation:
To compile VisAR, follow these steps:
1.Install the apk file at the top of the webpage
2.Install Meta Quest Developer Hub
3.Connect the Headset to a computer using USB
4.If you are using Windows, download the OEM USB driver. If you are using macOS, skip the next step as you do not need any additional USB drivers.
5.Extract the oculus-adb-driver-2.0 zip file, go to the /oculus-go-adb-driver-2.0/usb_driver/ folder, and double-click the android_winusb.inf file.
6.Open Terminal on your computer and run the following command to install the app: adb install -r [path of the apk file]
7.Put on the headset, go to Library > Unknown Sources, and then run the app.
8.For more precise details, please check the oculus documentation: Set Up Development Environment and Headset   

Usage:
Once installed on the Meta Quest headset, users can launch the VisAR application and follow these instructions to navigate their surroundings:
1. Put on the Meta Quest headset and ensure it is properly calibrated.
2. Navigate to Settings > Physical Space > Space Setup to capture the environment obstacles.
3. Navigate to the VisAR application in the "Unknown Sources" section of the Library and select it to launch.
4. Follow the audio and visual cues provided by VisAR to navigate obstacles and hazards in your environment.
