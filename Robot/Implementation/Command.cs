using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    class Command : ICommand
    {
        public string Verb { get; }

        public Vector Position { get; }

        public Vector Direction { get; }

        public Command(string verb)
        {
            Verb = verb;
        }
    }
}
