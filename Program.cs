using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TelemetryAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("Chevrolet Detroit Grand Prix_Practice 2.csv");

            List<LapRow> rows = LoadRows(file);

            RunStep4(rows);
            RunStep5(rows);
            RunStep6(rows);
        }


        static List<LapRow> LoadRows(string[] file)
        {
            List<LapRow> rows = new List<LapRow>();

            for (int i = 1; i < file.Length; i++)
            {
                string[] col = file[i].Split(',');

                LapRow row = new LapRow
                {
                    CarNumber = col[0],
                    Sector = col[2],
                    Time = double.Parse(col[3])
                };

                rows.Add(row);
            }

            return rows;
        }

        static void RunStep4(List<LapRow> rows)
        {
            Dictionary<string, double> laps = new Dictionary<string, double>();
            double minTime;

            Console.WriteLine("\nStep 4:");
            foreach(var r in rows)
            {
                if(r.Sector.Equals("LAP"))
                {
                    if(!laps.ContainsKey(r.CarNumber))
                    {
                        laps.Add(r.CarNumber, r.Time);
                    }
                    else
                    {
                        if(laps.TryGetValue(r.CarNumber, out minTime))
                        {
                            if(minTime > r.Time)
                            {
                                minTime = r.Time;
                                laps[r.CarNumber] = minTime;
                            }
                        }
                    }
                }


            }

            List<KeyValuePair<string, double>> lapList = laps.ToList();

            lapList.Sort((a, b) => a.Value.CompareTo(b.Value));

            foreach (var l in lapList)
            {
                Console.WriteLine("Car " + l.Key + " | Fastest Lap: " + l.Value + "s");
            }
        }

        static void RunStep5(List<LapRow> rows)
        {
            double minTime; 
            Dictionary<string, double> laps = new Dictionary<string, double>();

            Console.WriteLine("\nStep 5:");
            foreach(var r in rows)
            {
                if(!r.Sector.Equals("LAP") && r.CarNumber.Equals("2"))
                {
                    if(!laps.ContainsKey(r.Sector))
                    {
                        laps.Add(r.Sector, r.Time);
                    }
                    else
                    {
                        if(laps.TryGetValue(r.Sector, out minTime))
                        {
                            if(minTime > r.Time)
                            {
                                minTime = r.Time;
                                laps[r.Sector] = minTime;
                            }
                        }
                    }
                }
            }

            List<KeyValuePair<string, double>> lapList = laps.ToList(); //convert dictionary to list

            lapList.Sort((a, b) => a.Key.CompareTo(b.Key)); //sort list in ascending order by sector times

            foreach (var l in lapList)
            {
                Console.WriteLine("Car 2 | " + l.Key +  " | Fastest Lap: " + l.Value + "s");
            }
        }

        static void RunStep6(List<LapRow> rows)
        {
            Console.WriteLine("Step 6 Running..");
        }

    }
}
