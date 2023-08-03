using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ElevatorSim
{
    class Floor
    {
        private static int C1F = 1;
        private static int C2F = 1;
        private static State C1S = State.idle;
        private static State C2S = State.idle;
        private static Cabin C1 = new Cabin();
        private static Cabin C2 = new Cabin();
        private static Cabin[] Cs = { C1, C2 };

        public int FloorNumber { get; }
        public int Users { get; set; }

        public Floor(int number)
        {
            FloorNumber = number;
        }

        public Cabin ElevatorCall()
        {
            Cabin c = null;
            int relativePosition1 = Math.Abs(FloorNumber - C1F);
            int relativePosition2 = Math.Abs(FloorNumber - C2F);
            if (relativePosition1 <= relativePosition2)
            {
                if (C1.state == State.idle || (C1.Floor > FloorNumber && C1.state == State.goes_down)
                    || (C1.Floor < FloorNumber && C1.state == State.goes_up))
                {
                    EMove(C1, FloorNumber);
                    Console.WriteLine("Лифт 1 готов!");
                    c = C1;
                    c.OpenDoorButtonPress();
                    C1S = c.state;
                    Thread.Sleep(1 * 1000);
                    Console.WriteLine("Пассажир зашел!");
                }
                else
                {
                    EMove(C2, FloorNumber);
                    Console.WriteLine("Лифт 2 готов!");
                    c = C2;
                    c.OpenDoorButtonPress();
                    C2S = c.state;
                    Thread.Sleep(1 * 1000);
                    Console.WriteLine("Пассажир зашел!");
                }
            }
            else
            {
                if (C2.state == State.idle || (C2.Floor > FloorNumber && C2.state == State.goes_down)
                    || (C2.Floor < FloorNumber && C2.state == State.goes_up))
                {
                    EMove(C2, FloorNumber);
                    Console.WriteLine("Лифт 2 готов!");
                    c = C2;
                    c.OpenDoorButtonPress();
                    C2S = c.state;
                    Thread.Sleep(1 * 1000);
                    Console.WriteLine("Пассажир зашел!");
                }
                else
                {
                    EMove(C1, FloorNumber);
                    Console.WriteLine("Лифт 1 готов!");
                    c = C1;
                    c.OpenDoorButtonPress();
                    C1S = c.state;
                    Thread.Sleep(1 * 1000);
                    Console.WriteLine("Пассажир зашел!");
                }
            }
            //if (C1F == FloorNumber)
            //{
            //    Console.WriteLine("Лифт 1 готов!");
            //    c = C1;
            //    c.OpenDoorButtonPress();
            //    C1S = c.state;
            //    Thread.Sleep(1 * 1000);
            //    Console.WriteLine("Пассажир зашел!");
            //}
            return c;
        }

        private void EMove(Cabin c, int floor)
        {
            int elevatorNumber = 0;
            if (c == Cs[0])
                elevatorNumber = 1;
            else
                elevatorNumber = 2;
            int summand = 1;
            if (floor < FloorNumber)
            {
                c.state = State.goes_down;
                summand = -1;
            }
            else
            {
                c.state = State.goes_up;
            }
            while (c.Floor != floor)
            {
                //Thread.Sleep(1 * 1000);
                c.Floor += summand;
                Console.WriteLine("Лифт " + elevatorNumber + " перешел на этаж " + c.Floor);
            }
            c.FBReset();
            Console.WriteLine("Лифт прибыл!");
            if (elevatorNumber == 1)
            {
                C1F = c.Floor;
            }
            else
            {
                C2F = c.Floor;
            }
        }

        //public void ElevatorMove(Cabin c)
        //{
        //    //if (Cs[elevator].state != State.idle) return;
        //    //Cabin c = Cs[elevator];
        //    int floor = c.FloorDest;
        //    int elevatorNumber = 0;
        //    if (c == Cs[0])
        //        elevatorNumber = 1;
        //    else
        //        elevatorNumber = 2;
        //    int summand = 1;
        //    if (floor < FloorNumber)
        //    {
        //        c.state = State.goes_down;
        //        summand = -1;
        //    }
        //    else
        //    {
        //        c.state = State.goes_up;
        //    }
        //    while (c.Floor != floor)
        //    {
        //        Thread.Sleep(1 * 1000);
        //        c.Floor += summand;
        //        Console.WriteLine("Лифт " + elevatorNumber + " перешел на этаж " + c.Floor);
        //    }
        //    c.FBReset();
        //    if (elevatorNumber == 1)
        //    {
        //        C1F = c.Floor;
        //    }
        //    else
        //    {
        //        C2F = c.Floor;
        //    }
        //}

        public void ElevatorMove(Cabin c)
        {
            EMove(c, c.FloorDest);
        }
    }
}
