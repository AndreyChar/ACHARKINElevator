using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    public enum State
    {
        idle = 0, idle_opened = 1,
        goes_up = 2, goes_down = 3,
        opens = 4, closes = 5
    }
    class Cabin
    {
        public int Floor { get; set; }

        private bool FBPressed = false;
        private bool CDBPressed = false;
        public State state = 0;
        public int FloorDest { get; set; }

        public void FloorButtonPress(int floor)
        {
            if (floor > 0 && floor <= 20)
            {
                FBPressed = true;
                Console.WriteLine("Отправление к этажу " + floor + "!");

                if (CDBPressed)
                {
                    Console.WriteLine("Лифт готов!");
                    //return true;
                }
                else
                {
                    Console.WriteLine("Дверь не закрыта!");
                    //return false;
                }
                FloorDest = floor;
            }
            else
            {
                Console.WriteLine("Такого этажа нет!");
                //return false;
            }
        }

        public bool IsReady()
        {
            return FBPressed && CDBPressed;
        }

        public void FBReset()
        {
            FBPressed = false;
        }

        public void OpenDoorButtonPress()
        {
            if (state == State.idle)
            {
                state = State.idle_opened;
                Console.WriteLine("Дверь открыта!");
            }
            else if (state == State.idle_opened)
            {
                Console.WriteLine("Дверь была открыта!");
            }
            CDBPressed = false;
        }

        public void CloseDoorButtonPress()
        {
            if (state == State.idle_opened)
            {
                state = State.idle;
                Console.WriteLine("Дверь закрыта!");
            }
            else if (state == State.idle)
            {
                Console.WriteLine("Дверь была закрыта!");
            }
            CDBPressed = true;
            if (FBPressed)
            {
                Console.WriteLine("Лифт готов!");
            }
        }

        public void HelpButtonPress()
        {
            Console.WriteLine("Диспетчер вызван!");
        }

        public bool MovementFound()
        {
            return (state == State.idle_opened);
        }

        public bool NoMovementFound()
        {
            return !MovementFound();
        }
    }
}
