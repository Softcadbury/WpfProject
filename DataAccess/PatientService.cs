using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class PatientService
    {
        public static List<PatientModel> GetListPatient()
        {
            return new List<PatientModel>();
        }

        public static PatientModel GetPatient(int id)
        {
            return new PatientModel()
            {
                Id = 1,
                Name = "parage",
                Firstname = "romain",
                Birthday = new DateTime(),
                Observations = new List<ObservationModel>()
            };
        }

        public static bool AddPatient(PatientModel patient)
        {
            return true;
        }

        public static bool DeletePatient(int id)
        {
            return true;
        }
    }
}