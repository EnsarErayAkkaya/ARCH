using System;
using System.Collections.Generic;

[Serializable]
public class Floor 
{
    public string floorName;
    public int floorIndex;
    public int startingX,startingY,endingX,endingY;
    public List<Room> rooms;
    public List<Station> stations = new List<Station>();
    public Floor(int index)
    {
        rooms = new List<Room>();
        
        floorIndex = index;

        Random R = new Random();
        char c = (char)('A' + R.Next (0,26));

        uint num = (uint)R.Next(100,1000);

        floorName = c + "-" + num;
    }
}
