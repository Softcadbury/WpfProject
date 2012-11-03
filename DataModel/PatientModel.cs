using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class PatientModel
    {
        public string Name { get; set; }

        public string Firstname { get; set; }

        public DateTime Birthday { get; set; }

        public int Id { get; set; }

        public List<ObservationModel> Observations { get; set; }
    }
}