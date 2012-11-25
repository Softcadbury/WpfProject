using DataAccess.ServicePatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class ServicePatientHelper
    {
        public static Patient[] GetListPatient()
        {
            ServicePatientClient servicePatient = new ServicePatientClient();
            return servicePatient.GetListPatient();
        }

        public static Patient GetPatient(int id)
        {
            ServicePatientClient servicePatient = new ServicePatientClient();
            return servicePatient.GetPatient(id);
        }

        public static bool AddPatient(Patient patient)
        {
            ServicePatientClient servicePatient = new ServicePatientClient();
            return servicePatient.AddPatient(patient);
        }

        public static bool DeletePatient(int id)
        {
            ServicePatientClient servicePatient = new ServicePatientClient();
            return servicePatient.DeletePatient(id);
        }
    }
}