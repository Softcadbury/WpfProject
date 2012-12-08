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

            try
            {
                return servicePatient.GetListPatient();
            }
            catch (Exception)
            {
                return new Patient[0];
            }
        }

        public static Patient GetPatient(int id)
        {
            ServicePatientClient servicePatient = new ServicePatientClient();

            try
            {
                return servicePatient.GetPatient(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool AddPatient(Patient patient)
        {
            ServicePatientClient servicePatient = new ServicePatientClient();

            try
            {
                return servicePatient.AddPatient(patient);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeletePatient(int id)
        {
            ServicePatientClient servicePatient = new ServicePatientClient();

            try
            {
                return servicePatient.DeletePatient(id);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}