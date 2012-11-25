using DataAccess.ServiceObservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class ServiceObservationHelper
    {
        public static bool AddObservation(int idPatient, Observation obs)
        {
            ServiceObservationClient serviceObs = new ServiceObservationClient();
            return serviceObs.AddObservation(idPatient, obs);
        }
    }
}