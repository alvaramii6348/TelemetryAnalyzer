// Step 5 Plan:
// 1. Filter rows where Sector != "LAP"
// 2. For Car 2, group by Sector (S1..S9)
// 3. For each sector, compute min time
// 4. Sort by sector number
// 5. Print results

using TelemetryAnalyzer;

string[] file = File.ReadAllLines("Chevrolet Detroit Grand Prix_Practice 2.csv");

List<LapRow> rows = new List<LapRow>(); //declare list of rows using LapRow class

string[] col; //String array to hold csv rows
double minTime; //double holding the minimum sector time for cars

Dictionary<string, double> laps = new Dictionary<string, double>(); //dictionary holding a key, value pair of the cars and their sector times


//loop through entire csv
for(int i = 1; i < file.Length; i++)
{
    col = file[i].Split(','); //remove commas in each row of file

    //Create instance of LapRow class to hold car#, Sector, Sector Time
    LapRow row = new LapRow 
    {
        CarNumber = col[0],
        Sector = col[2],
        Time = double.Parse(col[3])
    };

    if(!row.Sector.Equals("LAP") && row.CarNumber.Equals("2"))
    {
        if(!laps.ContainsKey(row.Sector))
        {
            laps.Add(row.Sector, row.Time);
        }
        else
        {
            if(laps.TryGetValue(row.Sector, out minTime))
            {
                if(minTime > row.Time)
                {
                    minTime = row.Time;
                    laps[row.Sector] = minTime;
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

