using System.Collections.Generic;

namespace Robot
{
    public interface IParser
    {
        ICommand Parse(string input);

        List<ICommand> ParseFile(string fileName);
    }
}
