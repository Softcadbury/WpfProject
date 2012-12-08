using DataAccess.ServiceLive;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tools;

namespace DataAccess
{
    public static class ServiceLiveHelper
    {
        public static void Subscribe(ObservableCollection<KeyValuePair<DateTime, double>> heartQueue,
                                     ObservableCollection<KeyValuePair<DateTime, double>> tempQueue)
        {
            try
            {
                _callBack = new InstanceContext(new CallbackHandler(heartQueue, tempQueue));
                _serviceLive = new ServiceLiveClient(_callBack);
                _serviceLive.Subscribe();
            }
            catch (Exception)
            {
                _callBack = null;
                _serviceLive = null;
            }
        }

        private static InstanceContext _callBack;
        private static ServiceLiveClient _serviceLive;

        public static void UnSubscribe()
        {
            if (_callBack == null || _serviceLive == null)
                return;

            try
            {
                _serviceLive.Close();
                _callBack.Close();
            }
            catch { }
        }
    }
}