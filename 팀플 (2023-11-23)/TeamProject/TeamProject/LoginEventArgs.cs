using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    public class LoginEventArgs : EventArgs
    {
        public string LoggedInUserId { get; private set; }

        public LoginEventArgs(string loggedInUserId)
        {
            LoggedInUserId = loggedInUserId;
        }
    }
}
