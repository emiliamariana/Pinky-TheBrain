using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkyAndTheBrain.Models
{
    public class RatPlayer
    {
        public int LineIndex { get; set; }
        public int ColumnIndex { get; set; }
        public int Score { get; set; }
        public char LetterOfIdentification { get; set; }
    }
}
