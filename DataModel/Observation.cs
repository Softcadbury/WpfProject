using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Observation
    {
        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public string[] Prescription { get; set; }

        public Byte[][] Pictures { get; set; }

        public int Weight { get; set; }

        public int BloodPressure { get; set; }
    }
}