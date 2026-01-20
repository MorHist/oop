using System;
using System.Collections.Generic;
using System.Linq;
using Lr1.Models;

namespace Lr1.Service
{
    public class StationContainer
    {
        public Stack<StationBase> _stations = new Stack<StationBase>();

        public delegate void StationEventHandler(object sender, StationEventArgs e);

        public class StationEventArgs : EventArgs
        {
            public StationBase Station { get; set; }
        }

        public event StationEventHandler StationAdded;
        public event StationEventHandler StationRemoved;

        public void AddStation(StationBase station)
        {
            _stations.Push(station);
            OnStationAdded(new StationEventArgs { Station = station });
        }

        public StationBase RemoveStation()
        {
            if (!_stations.Any())
                return null;
            StationBase.DecreaseTotalStations();
            var station = _stations.Pop();
            OnStationRemoved(new StationEventArgs { Station = station });
            return station;
        }

        public int CountOfStation()
        {
            return _stations.Count();
        }

        public bool AnyStations()
        {
            return _stations.Any();
        }

        public StationBase Peek()
        {
            return _stations.Any() ? _stations.Peek() : null;
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

        public Stack<StationBase> GetAllStations()
        {
            return _stations;
        }

        /// <summary>
        /// Получить статистику по типам станций
        /// </summary>
        public Dictionary<string, int> GetStationTypesStatistics()
        {
            return _stations
                .GroupBy(s => s.StationType)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// Получить станции определенного типа
        /// </summary>
        public IEnumerable<StationBase> GetStationsByType(string stationType)
        {
            return _stations.Where(s => s.StationType == stationType);
        }

        /// <summary>
        /// Поиск станций по критерию
        /// </summary>
        public IEnumerable<StationBase> FindStations(Predicate<StationBase> predicate)
        {
            return _stations.Where(s => predicate(s));
        }
    }
}