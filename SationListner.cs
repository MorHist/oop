using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lr1.Station;

namespace Lr1
{
    internal class SationListner
    {
        public void OnStationAdded(object sender, StationContainer.StationEventArgs e)
        {
            Console.WriteLine($"Добавлена станция: {e.Station._title}");
        }

        public void OnStationRemoved(object sender, StationContainer.StationEventArgs e)
        {
            Console.WriteLine($"Удаленана станция: {e.Station._title}");
        }
        
    }
}
