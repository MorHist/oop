using System.Collections.Generic;

namespace Lr1.Interpreter
{
    public class StationQueryInterpreter
    {
        private readonly QueryParser _parser = new QueryParser();

        public IEnumerable<Station> ExecuteQuery(StationContainer container, string query)
        {
            var expression = _parser.Parse(query);
            var result = new List<Station>();

            foreach (var station in container.GetAllStations())
            {
                if (expression.Interpret(station))
                    result.Add(station);
            }

            return result;
        }
    }
}