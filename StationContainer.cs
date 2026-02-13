using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lr1
{
    public class StationContainer
    {
        public Stack<Station> _stations = new Stack<Station>();

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

        public int CountOfStation(Station station)
        {
            return _stations.Count();
        }

        public bool AnyStations()
        { 
            return _stations.Any(); 
        }

        public Station Peek()
        {
            return _stations.Peek();
        }

        public void Reverse()
        {
            _stations.Reverse(); 
        }

        public bool EnableEvents { get; set; } = true;

        protected virtual void OnStationAdded(StationEventArgs e)
        {
            if (EnableEvents)
            {
                StationAdded?.Invoke(this, e);
            }
        }

        protected virtual void OnStationRemoved(StationEventArgs e)
        {
            if (EnableEvents)
            {
                StationRemoved?.Invoke(this, e);
            }
        }

        public Stack<Station> GetAllStations() 
        {

            return _stations;
        }


    }
}
