using Common;

namespace Robot
{
    /// <summary>
    /// A robot command, structured as a verb with zero or more parameters
    /// </summary>
    ///
    public interface ICommand
    {
        string Verb { get; }
        Vector Position { get; }
        Vector Direction { get; }
    }
}
