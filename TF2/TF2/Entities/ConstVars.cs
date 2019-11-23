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
        public static int AuthStatus { get; set; } = 0;
        //0 - not logged in, other values can mean different user groups
        public static User currentUser;
    }
}
