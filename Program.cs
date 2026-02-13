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
            // Read entire CSV file into memory
            string[] file = File.ReadAllLines("Chevrolet Detroit Grand Prix_Practice 2.csv");

            // Parse file rows into LapRow objects
            List<LapRow> rows = LoadRows(file);

            // Execute each step
            RunStep4(rows);
            RunStep5(rows);
            RunStep6(rows);
        }

        // Converts raw CSV lines into a list of LapRow objects
        static List<LapRow> LoadRows(string[] file)
        {
            List<LapRow> rows = new List<LapRow>();

            for (int i = 1; i < file.Length; i++) // Skip header row
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

        // Step 4:
        // Filter "LAP" rows and determine fastest lap per car
        static void RunStep4(List<LapRow> rows)
        {
            Dictionary<string, double> laps = new Dictionary<string, double>();
            double minTime;

            Console.WriteLine("\nStep 4:");
            foreach(var r in rows)
            {
                if(r.Sector.Equals("LAP"))
                {
                    // If laps dict doesn't contain car number, store it
                    if(!laps.ContainsKey(r.CarNumber))
                    {
                        laps.Add(r.CarNumber, r.Time);
                    }
                    else
                    {
                        // Update if a faster lap time is found
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

            // Update if a faster lap time is found
            List<KeyValuePair<string, double>> lapList = laps.ToList();

            lapList.Sort((a, b) => a.Value.CompareTo(b.Value));

            foreach (var l in lapList)
            {
                Console.WriteLine($"Car {l.Key,-2} | Fastest Lap: " + l.Value + "s");
            }
        }

        // Step 5:
        // For Car 2 only, determine fastest time per sector (S1–S9)
        static void RunStep5(List<LapRow> rows)
        {
            double minTime; 
            Dictionary<string, double> laps = new Dictionary<string, double>();

            Console.WriteLine("\nStep 5:");
            foreach(var r in rows)
            {
                // Ignore LAP rows and filter to Car 2
                if(!r.Sector.Equals("LAP") && r.CarNumber.Equals("2"))
                {
                    // If laps dict doesn't contain sector, store it
                    if(!laps.ContainsKey(r.Sector))
                    {
                        laps.Add(r.Sector, r.Time);
                    }
                    else
                    {
                        // Update if faster sector time found
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

            //sort laps by sectors (S1-S9)
            List<KeyValuePair<string, double>> lapList = laps.ToList(); //convert dictionary to list

            lapList.Sort((a, b) => a.Key.CompareTo(b.Key)); //sort list in ascending order by sector times

            foreach (var l in lapList)
            {
                Console.WriteLine("Car 2 | " + l.Key +  " | " + l.Value + "s");
            }
        }

        // Step 6:
        // Compute theoretical optimal lap per car by summing
        // the best sector times (S1–S9) for each car
        static void RunStep6(List<LapRow> rows)
        {
            double optTime;
            // Outer dictionary: CarNumber → (Sector → Best Sector Time)
            Dictionary<string,  Dictionary<string, double>> bestCar = new Dictionary<string,  Dictionary<string, double>>();
            Console.WriteLine("\nStep 6:");
            
            foreach(var r in rows)
            {
                // Skip "LAP" rows
                if(r.Sector.Equals("LAP"))
                {
                    continue;
                }
                // Create inner dictionary for each car to track sector mins
                else if(!bestCar.ContainsKey(r.CarNumber))
                {
                    bestCar[r.CarNumber] = new Dictionary<string, double>();
                }
                else
                {
                    var sectorDict = bestCar[r.CarNumber];

                    // Store first sector time
                    if (!sectorDict.ContainsKey(r.Sector))
                    {
                        sectorDict[r.Sector] = r.Time;
                    }

                     // Update if faster sector time found
                    else if (r.Time < sectorDict[r.Sector])
                    {
                        sectorDict[r.Sector] = r.Time;
                    }
                }
            }

            // Compute optimal lap per car by summing sector minimums
            Dictionary<string, double> optLap = new Dictionary<string, double>();

            foreach(var b in bestCar)
            {
                var sectorDict = b.Value;
                optTime = 0;

                foreach(var s in sectorDict)
                {
                    optTime += s.Value;
                }
                optLap[b.Key] = optTime;
            }

            // Sort cars by optimal lap time ascending
            List<KeyValuePair<string, double>> lapList = optLap.ToList();

            lapList.Sort((a, b) => a.Value.CompareTo(b.Value));

            foreach (var l in lapList)
            {
                Console.WriteLine($"Car {l.Key,-2} | Optimal Lap Time: " + l.Value.ToString("F3") + "s");
            }
        }
    }
}
