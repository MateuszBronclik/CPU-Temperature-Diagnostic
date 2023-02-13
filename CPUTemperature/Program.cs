using System;
using System.Linq;
using System.Management;
using System.Threading;
using System.Collections.Generic;
using Figgle;
using System.Xml.Serialization;
using System.Linq.Expressions;

namespace CPUTemperature
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FiggleFonts.Larry3d.Render("cpu thermometer"));
            Console.WriteLine("CPU Temperature checker |\nPlease select duration of Temperature Test\n");
            Console.WriteLine("1. 5 minutes");
            Console.WriteLine("2. 10 minutes");
            Console.WriteLine("3. 15 minutes");
            Console.WriteLine("4. 30 minutes");
            Console.WriteLine("5. 1 hour");
            Console.WriteLine("\nInput coresponding number and confirm with [Enter] key");

            int choice = int.Parse(Console.ReadLine());

            double duration = 0;

            switch (choice)
            {
                case 1:
                    duration = 5;
                    break;
                case 2:
                    duration = 10;
                    break;
                case 3:
                    duration = 15;
                    break;
                case 4:
                    duration = 30;
                    break;
                case 5:
                    duration = 60;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    return;
            }

            List<double> temperatures = new List<double>();
            TimeSpan endTime = TimeSpan.FromMinutes(duration);
            TimeSpan elapsedTime = TimeSpan.Zero;
            DateTime startTime = DateTime.Now;
            try
            {
                while (elapsedTime < endTime)
                {
                    double temperature = GetCPUTemperature();
                    Console.WriteLine("Current CPU temperature: " + temperature + "°C");
                    Console.WriteLine($"Time remaining until completion: {endTime - elapsedTime:mm\\:ss}");

                    temperatures.Add(temperature);

                    Thread.Sleep(1000);
                    elapsedTime = DateTime.Now - startTime;
                    Console.Clear();
                }

                Console.ForegroundColor = temperatures.Average() >= 75 ? ConsoleColor.Red : ConsoleColor.Green;
                double averageTemperature = temperatures.Average();
                double maxTemperature = temperatures.Max();
                Console.WriteLine($"Average CPU temperature in the last {duration} minutes: {averageTemperature}°C");
                Console.WriteLine($"Highest recorded temperature: {maxTemperature}");

                if (averageTemperature >= 75)
                {
                    Console.WriteLine("A temperature higher than 75° is considered potentially dangerous for the CPU.");
                }
                else
                {
                    Console.WriteLine("Everything from 40° to 75° is consider as a normal temperature");
                }
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
            catch (ManagementException ex)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Access denied. Please exit and run Visual Studio as administrator.");
                return;
            }


        }

        private static double GetCPUTemperature()
        {
            {
                var query = "SELECT * FROM MSAcpi_ThermalZoneTemperature";
                double temperature = 0;
                double absoluteZero = -273.15;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", query);
                foreach (ManagementObject obj in searcher.Get())
                {
                    temperature = (double)((UInt32)obj["CurrentTemperature"] / 10 + absoluteZero);
                    break;
                }
                Console.ForegroundColor = temperature <= 75 ? ConsoleColor.Green : ConsoleColor.Red;
                return temperature;
            }
            
        }

        private static void DisplayResult(double averageTemperature, double maxTemperature, int duration)
        {
            Console.WriteLine($"Average CPU temperature in the last: {duration} minutes: {averageTemperature}°C");
            Console.WriteLine($"Highest recorded temperature: {maxTemperature}°C");

            if (averageTemperature > 75)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("A temperature higher than 75°C is considered potentially dangerous for the CPU.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Everything from 40°C to 75°C is consider as a normal temperature.");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}

