using AutoMapper;
using DataAccess.ServiceObservation;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class ObservationService
    {
        public static bool AddObservation(int idPatient, ObservationModel obs)
        {
            ServiceObservationClient serviceObs = new ServiceObservationClient();
            Mapper.CreateMap<ObservationModel, Observation>();
            return serviceObs.AddObservation(idPatient, Mapper.Map<ObservationModel, Observation>(obs));
        }
    }
}