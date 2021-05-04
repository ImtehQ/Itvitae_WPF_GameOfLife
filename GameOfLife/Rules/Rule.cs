using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Rules
{
    public abstract class Rule
    {
        public abstract bool NewState(bool currentState, int neighborsAlive);
    }
}
