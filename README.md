
# CPU Temperature Diagnostic Tool

This C# program provides real-time temperature monitoring of the CPU on the host machine. The user has the option to choose between five measurement durations: 
+ 5 minutes
+ 10 minutes
+ 15 minutes
+ 30 minutes
+ 1 hour

The temperature is recorded at one second intervals and displayed on the console. The program tracks the maximum and average temperature, and at the end of the measurement period, it reports the average temperature, highest recorded temperature, and whether it falls within the safe range. In case the temperature exceeds > 75°C, the program issues a warning, indicating potential danger to the CPU.

Note: This program was created as a solution for the lack of CPU temperature monitoring support in Windows 11 and has only been tested on this operating system. 

## Important Notice
To run the program, you must open the solution as **administrator**. 

Program uses Nugget packages, including:
+ Figgle (https://www.nuget.org/packages/Figgle)
+ System.Managment (https://www.nuget.org/packages/System.Management/)

Instructions:

+ Choose the desired measurement duration.
+ Minimize the program and perform regular tasks on your PC as usual.
+ After the chosen duration has passed, the program will display the average temperature of the CPU.

## Screenshots 
+ Main menu:
![App Screenshot](https://github.com/MateuszBronclik/CPU-Temperature-Diagnostic/blob/main/CPUTemperature/screenshots/cpumenu.png?raw=true ) 
+ Cpu test running:
![App Screenshot](https://github.com/MateuszBronclik/CPU-Temperature-Diagnostic/blob/main/CPUTemperature/screenshots/cputest.png?raw=true ) 
