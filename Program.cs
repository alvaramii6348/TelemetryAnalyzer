using TelemetryAnalyzer;

string[] file = File.ReadAllLines("Chevrolet Detroit Grand Prix_Practice 2.csv");

List<LapRow> rows = new List<LapRow>(); //declare list of rows using LapRow class

string[] col; //String array to hold csv rows
double minTime;

Dictionary<string, double> laps = new Dictionary<string, double>();


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

    if(row.Sector.Equals("LAP"))
    {
        if(!laps.ContainsKey(row.CarNumber))
        {
            laps.Add(row.CarNumber, row.Time);
        }
        else
        {
            if(laps.TryGetValue(row.CarNumber, out minTime))
            {
                if(minTime > row.Time)
                {
                    minTime = row.Time;
                    laps[row.CarNumber] = minTime;
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

