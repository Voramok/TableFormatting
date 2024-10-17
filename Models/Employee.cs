using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFormatting.Models
{
    internal class Employee
    {
        public required string Name { get; set; }
        public int Code { get; set; }
        public required string Position { get; set; }
    }
}
