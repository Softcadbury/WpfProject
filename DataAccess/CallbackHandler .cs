using DataAccess.ServiceLive;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DataAccess
{
    public class CallbackHandler : IServiceLiveCallback
    {
        public CallbackHandler(ObservableCollection<KeyValuePair<DateTime, double>> heartQueue,
                               ObservableCollection<KeyValuePair<DateTime, double>> tempQueue)
        {
            _heartQueue = heartQueue;
            _tempQueue = tempQueue;
        }

        private ObservableCollection<KeyValuePair<DateTime, double>> _heartQueue;
        private ObservableCollection<KeyValuePair<DateTime, double>> _tempQueue;

        public void PushDataHeart(double requestData)
        {
            _heartQueue.Add(new KeyValuePair<DateTime, double>(DateTime.Now, requestData));
            if (_heartQueue.Count > 40) _heartQueue.RemoveAt(0);
        }

        public void PushDataTemp(double requestData)
        {
            _tempQueue.Add(new KeyValuePair<DateTime, double>(DateTime.Now, requestData));
            if (_tempQueue.Count > 20) _tempQueue.RemoveAt(0);
        }
    }
}