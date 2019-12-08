using System;
using System.Collections.Generic;
using System.Text;

namespace TF2.ViewModels
{
    public class LecOrSubView : Tuple<string, string>
    {
        public LecOrSubView(string item1, string item2) : base(item1, item2 ?? CreateEmptyModel()) { }

        private static string CreateEmptyModel()
        {
            return "";
        }
    }
}
