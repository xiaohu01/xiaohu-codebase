using CarFinder;
using System;
using System.Collections.Generic;
using System.IO;

namespace CarFinderApp
{
    class Program
    {
        static int count;
        static object obj = new object();

        static void Main(string[] args)
        {
            CarSetting[] settings = ReadSetting(@"..\..\Files\CarSetup.txt");
            if(settings != null && settings.Length > 0)
            {
                for (int i = 0; i < settings.Length; i++)
                {
                    Car car = new Car(settings[i].InitialPosition, settings[i].Velocity);

                    var finders = new List<CarFinderBase>() { new BinaryCarFinder(car), new GoldenSectionCarFinder(car) };

                    foreach (var finder in finders)
                        finder.CarHasBeenFound += Finder_CarHasBeenFound;

                    count = 2;
                    car.Start();

                    foreach (var finder in finders)
                        finder.Find();

                    while (count > 0)
                        System.Threading.Thread.Sleep(100);

                    foreach (var finder in finders)
                        finder.Cancel();
                    car.Stop();
                }             
            }

            Console.WriteLine("Batch run has completed. Please press any key to finish...");
            Console.Read();
        }

        private static void Finder_CarHasBeenFound(object sender, EventArgs e)
        {
            lock (obj)
            {
                CarFinderBase finder = (CarFinderBase)sender;
                if (finder != null)
                    WriteResult(finder);
                count--; 
            }
        }

        static CarSetting[] ReadSetting(string path)
        {
            if(File.Exists(path))
            {
                List<CarSetting> carSettings = new List<CarSetting>();
                using (StreamReader reader = File.OpenText(path))
                {
                    string input = reader.ReadLine();
                    while((input = reader.ReadLine()) != null)
                    {
                        carSettings.Add(new CarSetting { InitialPosition = int.Parse(input.Split(';')[0]), Velocity = int.Parse(input.Split(';')[1]) });
                    }
                }

                return carSettings.ToArray();
            }
            return null;
        }

        static void WriteResult(CarFinderBase finder)
        {
            string path = @"..\..\Files\Result.txt";

            if(File.Exists(path))
            {
                string line = finder.FinderName + ";" + finder.CarFound.ToString() + ";" + finder.InitialPosition.ToString()
                    + ";" + finder.Velocity.ToString() + ";" + finder.Position.ToString() + ";" + finder.Ticks.ToString();

                using (StreamWriter writer = File.AppendText(path))
                {
                    writer.WriteLine(line);
                }
            }
            else
            {
                string header = "FinderName;CarFound;InitialPosition;Velocity;Position;Time";
                string line = finder.FinderName + ";" + finder.CarFound.ToString() + ";" + finder.InitialPosition.ToString()
                    + ";" + finder.Velocity.ToString() + ";" + finder.Position.ToString() + ";" + finder.Ticks.ToString();

                using (StreamWriter writer = File.AppendText(path))
                {
                    writer.WriteLine(header);
                    writer.WriteLine(line);
                }
            }
        }
    }

    class CarSetting
    {
        public int InitialPosition { get; set; }
        public int Velocity { get; set; }
    }
}
