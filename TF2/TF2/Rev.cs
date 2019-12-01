using System;
using System.Collections.Generic;
using System.Text;

namespace TF2
{
    class Rev
    {
        public string Reviewerusername { get; set; }
        public int LecSubScore { get; set; }
        public string Comment { get; set; }

        public Rev(string Reviewerusername, int LecSubScore, string comment)
        {
            this.Reviewerusername = Reviewerusername;
            this.LecSubScore = LecSubScore;
            Comment = comment;
        }
    }
}
