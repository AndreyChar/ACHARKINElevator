using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    class Program
    {
        private static Floor[] floors;
        static void Main(string[] args)
        {
            floors = new Floor[20];
            for (int i = 1; i < 21; i++)
            {
                floors[i - 1] = new Floor(i);
                //floors[i - 1].Users = 0;
            }
            //floors[0].Users = 1;
            //floors[14].Users = 1;
            Test();
            Console.ReadKey();
        }

        static void Test()
        {
            Cabin c = floors[0].ElevatorCall();
            c.CloseDoorButtonPress();
            c.FloorButtonPress(14);
            if (c.IsReady())
            {
                floors[0].ElevatorMove(c);
            }
            Cabin c2 = floors[14].ElevatorCall();
            //Console.WriteLine(floors[14].FloorNumber.ToString());
            c2.CloseDoorButtonPress();
            c2.FloorButtonPress(1);
            if (c2.IsReady())
            {
                floors[14].ElevatorMove(c);
            }
        }
    }
}
