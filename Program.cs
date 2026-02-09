using ConsoleApp1;

string[] file = File.ReadAllLines("Chevrolet Detroit Grand Prix_Practice 2.csv");

Console.WriteLine("File headers: " + file[0]);

List<Class> rows = new List<Class>();




string[] col;
int length = file.Length-1;

for(int i = 1; i < file.Length; i++)
{
    col = file[i].Split(',');

    Class row = new Class
    {
        CarNumber = col[0],
        Sector = col[2],
        Time = double.Parse(col[3])
    };

    rows.Add(row);

    Console.WriteLine(row.CarNumber);
    // if(col[])
    // if (col[3].Equals("LAPS"))
    // {
    //     continue;
    // }
    // else
    // {
    //     Console.WriteLine(col[3]);
    // }
}

Dictionary<string, List<Class>> cars = new Dictionary<string, List<Class>>;

foreach(Class r in rows)
{
    
}