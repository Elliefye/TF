using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Entities;

namespace TF2
{
    //a class for constants/global variables
    static class ConstVars
    {
        //0 - not logged in, other values can mean different user groups
        public static int AuthStatus { get; set; } = 0;
        //password is encrypted
        public static User currentUser;
        public static bool DarkMode { get; set; } = false;
    }
}
