using System;
using System.Collections.Generic;
using System.Linq;

namespace Lr1
{
    /// <summary>
    /// Фасад для управления железнодорожными станциями
    /// Предоставляет упрощенный интерфейс для работы со станцими
    /// </summary>
    public class StationFacade
    {
        private readonly StationContainer _stationContainer;
        private readonly SationListner _stationListener;

        /// <summary>
        /// Событие при изменении коллекции станций
        /// </summary>
        public event EventHandler<StationCollectionChangedEventArgs> CollectionChanged;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public StationFacade()
        {
            _stationContainer = new StationContainer();
            _stationListener = new SationListner();

            // Подписываемся на события контейнера
            _stationContainer.StationAdded += OnStationAddedInternal;
            _stationContainer.StationRemoved += OnStationRemovedInternal;
        }

        #region ОСНОВНЫЕ ОПЕРАЦИИ (упрощенный интерфейс)

        /// <summary>
        /// Создать новую станцию с минимальными данными
        /// </summary>
        public Station CreateStation(string name, string address, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название станции обязательно");

            var station = new Station(name)
            {
                Address = address,
                Number = phone,
                DateOfOpening = DateTime.Now
            };

            AddStation(station);
            return station;
        }

        /// <summary>
        /// Создать станцию с полными данными
        /// </summary>
        public Station CreateFullStation(string name, int seats, int soldTickets,
                                       string phone, double attendance, DateTime openingDate, string address)
        {
            var station = new Station(name, seats, soldTickets, phone, attendance, openingDate, address);
            AddStation(station);
            return station;
        }

        /// <summary>
        /// Добавить существующую станцию
        /// </summary>
        public void AddStation(Station station)
        {
            if (station == null)
                throw new ArgumentNullException(nameof(station));

            _stationContainer.AddStation(station);
        }

        /// <summary>
        /// Удалить последнюю добавленную станцию
        /// </summary>
        public bool RemoveLastStation()
        {
            if (!_stationContainer.AnyStations())
                return false;

            var station = _stationContainer.Peek();
            _stationContainer.RemoveStation(station);
            return true;
        }

        /// <summary>
        /// Получить текущую (последнюю) станцию
        /// </summary>
        public Station GetCurrentStation()
        {
            return _stationContainer.AnyStations()
                ? _stationContainer.Peek()
                : null;
        }

        /// <summary>
        /// Получить статистику по всем станциям
        /// </summary>
        public StationStats GetStatistics()
        {
            var stations = GetAllStations().ToList();

            if (!stations.Any())
                return new StationStats();

            return new StationStats
            {
                TotalStations = stations.Count,
                TotalSeats = stations.Sum(s => s.NumberOfSeats ?? 0),
                TotalTicketsSold = stations.Sum(s => s.SoldTickets ?? 0),
                AverageAttendance = stations.Average(s => s.AverageAttendace ?? 0),
                OldestStation = stations.OrderBy(s => s.DateOfOpening).FirstOrDefault(),
                NewestStation = stations.OrderByDescending(s => s.DateOfOpening).FirstOrDefault()
            };
        }

        /// <summary>
        /// Поиск станций по названию (регистронезависимый)
        /// </summary>
        public IEnumerable<Station> FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return GetAllStations();

            return GetAllStations()
                .Where(s => s.Title?.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(s => s.Title);
        }

        /// <summary>
        /// Поиск станций с вместимостью больше указанной
        /// </summary>
        public IEnumerable<Station> FindByMinCapacity(int minSeats)
        {
            return GetAllStations()
                .Where(s => s.NumberOfSeats >= minSeats)
                .OrderByDescending(s => s.NumberOfSeats);
        }

        /// <summary>
        /// Получить станции, отсортированные по дате открытия
        /// </summary>
        public IEnumerable<Station> GetStationsByDate(bool ascending = true)
        {
            return ascending
                ? GetAllStations().OrderBy(s => s.DateOfOpening)
                : GetAllStations().OrderByDescending(s => s.DateOfOpening);
        }

        /// <summary>
        /// Получить станции, отсортированные по названию
        /// </summary>
        public IEnumerable<Station> GetStationsByName()
        {
            return GetAllStations().OrderBy(s => s.Title);
        }

        /// <summary>
        /// Получить общее количество станций
        /// </summary>
        public int GetTotalCount()
        {
            return _stationContainer.CountOfStation();
        }

        /// <summary>
        /// Проверить, есть ли станции
        /// </summary>
        public bool HasStations()
        {
            return _stationContainer.AnyStations();
        }

        /// <summary>
        /// Очистить все станции
        /// </summary>
        public void ClearAll()
        {
            while (_stationContainer.AnyStations())
            {
                RemoveLastStation();
            }
        }

        /// <summary>
        /// Получить все станции
        /// </summary>
        public IEnumerable<Station> GetAllStations()
        {
            return _stationContainer.GetAllStations();
        }

        public string ExportToText()
        {
            var stations = GetAllStations().ToList();
            if (!stations.Any())
                return "Нет данных о станциях";

            var result = new System.Text.StringBuilder();
            result.AppendLine("=== СПИСОК ЖЕЛЕЗНОДОРОЖНЫХ СТАНЦИЙ ===");
            result.AppendLine($"Всего станций: {stations.Count}");
            result.AppendLine($"Дата экспорта: {DateTime.Now:dd.MM.yyyy HH:mm}");
            result.AppendLine("======================================");

            int index = 1;
            foreach (var station in stations)
            {
                result.AppendLine($"\n--- Станция #{index++} ---");
                result.AppendLine($"Название: {station.Title}");
                result.AppendLine($"Адрес: {station.Address}");
                result.AppendLine($"Телефон: {station.Number}");
                result.AppendLine($"Мест: {station.NumberOfSeats}");
                result.AppendLine($"Дата открытия: {station.DateOfOpening:dd.MM.yyyy}");
                if (station.AverageAttendace.HasValue)
                    result.AppendLine($"Средняя посещаемость: {station.AverageAttendace:F1}");
            }

            return result.ToString();
        }

        #endregion

        #region ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ

        /// <summary>
        /// Получить информацию о станции в формате строки
        /// </summary>
        public string GetStationInfo(Station station)
        {
            if (station == null)
                return "Станция не найдена";

            return station.ToString();
        }

        /// <summary>
        /// Получить станцию по индексу (0 - последняя, 1 - предпоследняя и т.д.)
        /// </summary>
        public Station GetStationByIndex(int index)
        {
            var stations = GetAllStations().ToList();

            if (index < 0 || index >= stations.Count)
                return null;

            // В стеке последний добавленный - первый, поэтому реверсируем
            return stations[stations.Count - 1 - index];
        }

        /// <summary>
        /// Перевернуть порядок станций
        /// </summary>
        public void ReverseOrder()
        {
            _stationContainer.Reverse();
            OnCollectionChanged(CollectionChangeType.Reordered);
        }

        #endregion

        #region ВНУТРЕННИЕ МЕТОДЫ И ОБРАБОТЧИКИ СОБЫТИЙ

        private void OnStationAddedInternal(object sender, StationContainer.StationEventArgs e)
        {
            // Дополнительная обработка при добавлении
            _stationListener.OnStationAdded(sender, e);
            OnCollectionChanged(CollectionChangeType.StationAdded, e.Station);
        }

        private void OnStationRemovedInternal(object sender, StationContainer.StationEventArgs e)
        {
            // Дополнительная обработка при удалении
            _stationListener.OnStationRemoved(sender, e);
            OnCollectionChanged(CollectionChangeType.StationRemoved, e.Station);
        }

        private void OnCollectionChanged(CollectionChangeType changeType, Station station = null)
        {
            CollectionChanged?.Invoke(this,
                new StationCollectionChangedEventArgs(changeType, station, GetTotalCount()));
        }

        #endregion

        #region ВСПОМОГАТЕЛЬНЫЕ КЛАССЫ

        /// <summary>
        /// Статистика по станциям
        /// </summary>
        public class StationStats
        {
            public int TotalStations { get; set; }
            public int TotalSeats { get; set; }
            public int TotalTicketsSold { get; set; }
            public double AverageAttendance { get; set; }
            public Station OldestStation { get; set; }
            public Station NewestStation { get; set; }

            public override string ToString()
            {
                return $"Станций: {TotalStations}, Мест: {TotalSeats}, " +
                       $"Билетов продано: {TotalTicketsSold}";
            }
        }

        /// <summary>
        /// Тип изменения коллекции
        /// </summary>
        public enum CollectionChangeType
        {
            StationAdded,
            StationRemoved,
            Reordered,
            Cleared
        }

        /// <summary>
        /// Аргументы события изменения коллекции
        /// </summary>
        public class StationCollectionChangedEventArgs : EventArgs
        {
            public CollectionChangeType ChangeType { get; }
            public Station Station { get; }
            public int TotalCount { get; }
            public DateTime ChangeTime { get; }

            public StationCollectionChangedEventArgs(CollectionChangeType changeType,
                                                   Station station, int totalCount)
            {
                ChangeType = changeType;
                Station = station;
                TotalCount = totalCount;
                ChangeTime = DateTime.Now;
            }
        }

        #endregion
    }
}