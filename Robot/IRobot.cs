using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    /// <summary>
    /// A robot abstraction
    /// </summary>
    ///
    public interface IRobot
    {
        /// <summary>
        /// The only interface between the robot and the outside world. Used to submit commands and received output, if any.
        /// </summary>
        ///
        public string SubmitCommand(ICommand command);
    }
}
