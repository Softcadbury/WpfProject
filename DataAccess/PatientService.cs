using AutoMapper;
using DataAccess.ServicePatient;
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
            ServicePatientClient servicePatient = new ServicePatientClient();

            Patient[] patients = servicePatient.GetListPatient();
            if (patients == null)
                return null;

            Mapper.CreateMap<Patient, PatientModel>();
            Mapper.CreateMap<Observation, ObservationModel>();
            return Mapper.Map<List<Patient>, List<PatientModel>>(patients.ToList());
        }

        public static PatientModel GetPatient(int id)
        {
            ServicePatientClient servicePatient = new ServicePatientClient();
            Mapper.CreateMap<Patient, PatientModel>();
            Mapper.CreateMap<Observation, ObservationModel>();
            return Mapper.Map<Patient, PatientModel>(servicePatient.GetPatient(id));
        }

        public static bool AddPatient(PatientModel patient)
        {
            ServicePatientClient servicePatient = new ServicePatientClient();
            Mapper.CreateMap<PatientModel, Patient>();
            Mapper.CreateMap<ObservationModel, Observation>();
            return servicePatient.AddPatient(Mapper.Map<PatientModel, Patient>(patient));
        }

        public static bool DeletePatient(int id)
        {
            ServicePatientClient servicePatient = new ServicePatientClient();
            return servicePatient.DeletePatient(id);
        }
    }
}