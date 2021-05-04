using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Rules
{
    public class Default : Rule
    {
        public override bool NewState(bool currentState, int neighborsAlive)
        {
            if (currentState == false)           //Poor cell currently death
            {
                if (neighborsAlive == 3)           //He will be born
                    return true;
            }
            else if (currentState)               //Cell is currently alive and well
            {
                if (neighborsAlive <= 1)           //He dies a lonly death
                    return false;
                else if (neighborsAlive >= 4)      //He dies by crushed to death
                    return false;
                else if (neighborsAlive >= 2)      //He lives with his friends
                    return true;
            }

            return false;
        }
    }
}
