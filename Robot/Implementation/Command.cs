using Common;
using Robot;

namespace Implementation.Robot
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

        public Command(string verb, Vector position, Vector direction) : this(verb)
        {
            Position = position;
            Direction = direction;
        }
    }
}
