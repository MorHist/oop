using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lr1
{
    internal class StationContainer
    {
        private Stack<Station> _stations;

        public delegate void StationEventHendler(object sender, StationEventArgs e);

        public class StationEventArgs : EventArgs
        { 
            public Station Station { get; set;}
        }

        public event StationEventHendler StationAdded;

        public event StationEventHendler StationRemoved;

        public void AddStation(Station station) 
        { 
            _stations.Push(station);
            OnStationAdded(new StationEventArgs { Station = station });
           
        }

        public void RemoveStation(Station station)
        {
            _stations.Pop();
            OnStationRemoved(new StationEventArgs { Station = station });
        }

        protected virtual void OnStationAdded(StationEventArgs e)
        {
            StationAdded?.Invoke(this, e);
        }

        protected virtual void OnStationRemoved(StationEventArgs e) 
        {
            StationRemoved?.Invoke(this, e);
        }

        public Stack<Station> GetAllStations() 
        {
            return new Stack<Station>(_stations);
        }
    }
}
