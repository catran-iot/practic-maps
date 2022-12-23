using GMap.NET;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Serilog;
using PracticMapsProject.Properties;

using System.ComponentModel;
using static System.Collections.Specialized.BitVector32;
using PracticMaps.Exceptions;
using static GMap.NET.Entity.OpenStreetMapGraphHopperGeocodeEntity;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class IsExternalInit { }
}
//

namespace PracticMaps
{
    /// <summary>
    /// Базовый класс картографических объектов
    /// </summary>
    public abstract class CPMapObject
    {
        /// <summary>
        /// Широта
        /// </summary>
        public abstract double Latitude { get; set; }
        /// <summary>
        /// Долгота
        /// </summary>
        public abstract double Longitude { get; set; }
        /// <summary>
        /// Название объекта
        /// </summary>
        public abstract string Name { get; set; }
    }
    /// <summary>
    /// Класс текущего состояния проекта - активный объект проекта и производимое над ним действие
    /// </summary>
    public class CPMapDataState
    {
        /// <summary>
        /// Ссылка на активный объект
        /// </summary>
        private CPMapObject _mapObject;
        /// <summary>
        /// Текущее действие с активным объектом
        /// </summary>
        private EPMapAction _mapAction;
        /// <summary>
        /// Список выделенных точек
        /// </summary>
        private readonly List<CPMapPoint> _selectedPoints;

        /// <summary>
        /// Устанавливает активный объект
        /// </summary>
        /// <param name="obj">Ссылка на объект карты</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>true - если успешно установлен новый активный объект, false - если предыдущее действие не было завершено</returns>
        public bool SetActiveObject(CPMapObject obj)
        {
            if (_mapAction != EPMapAction.None)
                return false;

            _mapObject = obj ?? throw new ArgumentNullException();
            return true;
        }

        /// <summary>
        /// Устанавливает действие над активным объектом 
        /// </summary>
        /// <param name="action"></param>
        /// <returns>true - если успешно установлено новое действие, false - если предыдущее действие не было завершено</returns>
        public bool SetActiveAction(EPMapAction action)
        {
            // нельзя прерывать незаконченное действие другим действием, сначала нужно завершить активное 
            if (_mapAction != EPMapAction.None)
                return false;
            
            _mapAction = action;
            return true;
        }

        // 
        /// <summary>
        /// Завершает текущее действие и сбрасывает ссылку на активный объект
        /// </summary>
        public void ActionCompleted()
        {
            _mapObject = null;
            _mapAction = EPMapAction.None;
        }

        /// <summary>
        /// Список выделенных точек
        /// </summary>
        public List<CPMapPoint> SelectedPoints
        {
            get => _selectedPoints;
        }

        /// <summary>
        /// Возвращает ссылку на активный объект
        /// </summary>
        public CPMapObject MapObject
        {
            get => _mapObject;
        }

        /// <summary>
        /// Возвращает действие над активным объектом
        /// </summary>
        public EPMapAction MapAction
        {
            get => _mapAction;
        }

        /// <summary>
        /// Возвращает ссылку на сессию, которой принадлежит активный объект 
        /// </summary>
        public CPMapSession Session
        {
            get
            {
                if (_mapObject == null)
                    return null;

                switch (_mapObject)
                {
                    case CPMapPoint point:
                        return point.ParentSession;
                    case CPMapTrack track:
                        return track.ParentSession;
                    case CPMapSession session:
                        return session;
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Возвращает ссылку на трек, которому принадлежит активный объект 
        /// </summary>
        public CPMapTrack Track
        {
            get
            {
                if (_mapObject == null)
                    return null;

                switch (_mapObject)
                {
                    case CPMapPoint point:
                        return point.ParentTrack;
                    case CPMapTrack track:
                        return track;
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Инициализирует экземпляр класса состояния данных проекта
        /// </summary>
        public CPMapDataState()
        {
            _mapObject = null;
            _mapAction = EPMapAction.None;
            _selectedPoints = new List<CPMapPoint>();
        }

        /// <summary>
        /// Устанавливает или снимает выделение объекта проекта
        /// </summary>
        /// <param name="mapObject"></param>
        /// <param name="state">true - выделить, false - снять выделение</param>
        /// <exception cref="PmException_NotSupportedObjectType"></exception>
        public void SetSelectionToObject(CPMapObject mapObject, bool state)
        {
            switch (mapObject)
            {
                case CPMapPoint point:
                    if (state)
                    {
                        if (_selectedPoints.IndexOf(point) == -1)
                            _selectedPoints.Add(point);
                    }
                    else
                    {
                        _selectedPoints.Remove(point);
                    }
                    break;

                case CPMapTrack track:
                    foreach (CPMapPoint pnt in track.Points)
                        SetSelectionToObject(pnt, state);
                    break;

                case CPMapSession session:
                    foreach (CPMapTrack trk in session.Tracks)
                        SetSelectionToObject(trk, state);
                    break;

                default:
                    throw new PmException_NotSupportedObjectType("Тип объекта не поддерживается");
            }
        }

        /// <summary>
        /// Выделяет дубликаты точек в сессии или треке, которому принадлежит активный объект
        /// </summary>
        /// <param name="type">Задает диапазон поиска - сессия или трек</param>
        /// <exception cref="PmException_ParentObjectNotFound"></exception>
        /// <exception cref="PmException_NotSupportedObjectType"></exception>
        public void SelectDuplicatedPointsInActiveObject(EPMapObjectType type)
        {
            List<CPMapPoint> points;
            switch (type)
            {
                case EPMapObjectType.Session:
                    _ = Session ?? throw new PmException_ParentObjectNotFound("Родительская сессия не найдена");
                    points = Session.FindDuplicatePoints();
                    SelectionCreate(points);
                    break;

                case EPMapObjectType.Track:
                    _ = Track ?? throw new PmException_ParentObjectNotFound("Родительский трек не найден");
                    points = Track.FindDuplicatePoints();
                    SelectionCreate(points);
                    break;

                default:
                    throw new PmException_NotSupportedObjectType("Тип объекта не поддерживается");
            }
        }

        /// <summary>
        /// Выделяет точки с некорректными глубинами в сессии или треке, которому принадлежит активный объект
        /// </summary>
        /// <param name="type">Задает диапазон поиска - сессия или трек</param>
        /// <param name="lowDepth">Глубина, меньшие значения которой считаются некорректными</param>
        /// <param name="useLowDepth">True - считать точки, с глубинами меньше заданной, некорректными; иначе - false</param>
        /// <param name="greatDepth">Глубина, большие значения которой считаются некорректными</param>
        /// <param name="useGreatDepth">True - считать точки, с глубинами больше заданной, некорректными; иначе - false</param>
        /// <exception cref="PmException_ParentObjectNotFound"></exception>
        /// <exception cref="PmException_NotSupportedObjectType"></exception>
        public void SelectBadDepthPointsInActiveObject(EPMapObjectType type, double lowDepth, bool useLowDepth, double greatDepth, bool useGreatDepth)
        {
            List<CPMapPoint> points;
            switch (type)
            {
                case EPMapObjectType.Session:
                    _ = Session ?? throw new PmException_ParentObjectNotFound("Родительская сессия не найдена");
                    points = Session.FindBadDepthPoints(lowDepth, useLowDepth, greatDepth, useGreatDepth);
                    SelectionCreate(points);
                    break;

                case EPMapObjectType.Track:
                    _ = Track ?? throw new PmException_ParentObjectNotFound("Родительский трек не найден");
                    points = Track.FindBadDepthPoints(lowDepth, useLowDepth, greatDepth, useGreatDepth);
                    SelectionCreate(points);
                    break;

                default:
                    throw new PmException_NotSupportedObjectType("Тип объекта не поддерживается");
            }
        }

        /// <summary>
        /// Выделяет слишком близко расположенные точки, которому принадлежит активный объект
        /// </summary>
        /// <param name="type">Задает диапазон поиска - сессия или трек</param>
        /// <param name="distance">Задает минимально допустимое расстоянием между точками</param>
        /// <exception cref="PmException_ParentObjectNotFound"></exception>
        /// <exception cref="PmException_NotSupportedObjectType"></exception>
        public void SelectClosePointsInActiveObject(EPMapObjectType type, double distance)
        {

            List<CPMapPoint> points;
            switch (type)
            {
                case EPMapObjectType.Session:
                    _ = Session ?? throw new PmException_ParentObjectNotFound("Родительская сессия не найдена");
                    points = Session.FindClosePoints(distance);
                    SelectionCreate(points);
                    break;

                case EPMapObjectType.Track:
                    _ = Track ?? throw new PmException_ParentObjectNotFound("Родительский трек не найден");
                    points = Track.FindClosePoints(distance);
                    SelectionCreate(points);
                    break;

                default:
                    throw new PmException_NotSupportedObjectType("Тип объекта не поддерживается");
            }
        }

        /// <summary>
        /// Проверяет, выделена ли данная точка
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool SelectionPointIsSelected(CPMapPoint point)
        {
            _ = point ?? throw new ArgumentNullException();

            return _selectedPoints.IndexOf(point) != -1;
        }

        /// <summary>
        /// Очищает список выделенных точек
        /// </summary>
        public void SelectionClear()
        {
            _selectedPoints.Clear();
        }

        /// <summary>
        /// Делает точки из списка выделенными, предыдущее выделение сбрасывается
        /// </summary>
        /// <param name="points"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SelectionCreate(List<CPMapPoint> points)
        {
            _ = points ?? throw new ArgumentNullException();

            _selectedPoints.Clear();
            _selectedPoints.AddRange(points);
        }

    }

    /// <summary>
    /// Класс параметров стиля маркера карты
    /// </summary>
    public class CPMapMarkerStyle
    {
        /// <summary>
        /// Цвет линии обводки маркера
        /// </summary>
        public Color SelectionColor;

        /// <summary>
        /// Толщина линии обводки маркера в пикселах
        /// </summary>
        public int SelectionWidth;

        /// <summary>
        /// Цвет маркера
        /// </summary>
        public Color MarkerColor;

        /// <summary>
        /// Размер маркера в пикселах
        /// </summary>
        public int Size;
    }

    /// <summary>
    /// Класс маркера объекта карта
    /// </summary>
    public class CPMapMarker : GMapMarker
    {
        private readonly CPMapObject _pmMapObjectRef;
        private readonly CPMapMarkerStyle _style;
        public CPMapObject PM_MapObjectRef
        {
            get => _pmMapObjectRef;
        }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CPMapMarker"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="st"></param>
        public CPMapMarker(CPMapObject obj, CPMapMarkerStyle st) : base(new PointLatLng(obj.Latitude, obj.Longitude))
        {
            _pmMapObjectRef = obj;
            _style = st;

            Size = new Size(_style.Size, _style.Size);
            Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
        }

        /// <summary>
        /// Обработчик - событие OnRender, отображает кастомные маркеры объектов на карте
        /// </summary>
        /// <param name="g"></param>
        public override void OnRender(Graphics g)
        {
            Rectangle rect = new (LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);
            Brush brush = new SolidBrush(_style.MarkerColor);
            Pen pen = new (_style.SelectionColor, _style.SelectionWidth);

            switch (_pmMapObjectRef)
            {
                // рисуем маркер точки (круг)
                case CPMapPoint:
                    g.FillEllipse(brush, rect);
                    g.DrawEllipse(pen, rect);
                    break;

                // рисуем маркер места (ромб)
                case CPMapPlace:
                    PointF[] points = { new(LocalPosition.X, LocalPosition.Y + Size.Height / 2),
                                        new(LocalPosition.X + Size.Width/2, LocalPosition.Y + Size.Height),
                                        new(LocalPosition.X + Size.Width, LocalPosition.Y + Size.Height / 2),
                                        new(LocalPosition.X + Size.Width / 2, LocalPosition.Y),
                                        new(LocalPosition.X, LocalPosition.Y + Size.Height / 2)};
                    g.FillPolygon(brush, points);
                    g.DrawPolygon(pen, points);
                    break;

                // рисуем объект неизвестного типа (красный крестик)
                default:
                    pen = new(Color.Red, 3);
                    g.DrawLine(pen, LocalPosition.X, LocalPosition.Y, LocalPosition.X + Size.Width, LocalPosition.Y + Size.Height);
                    g.DrawLine(pen, LocalPosition.X, LocalPosition.Y + Size.Height, LocalPosition.X + Size.Width, LocalPosition.Y);
                    break;
            }
        }

    }


    // {"places":[{"placeId":"c877c9f2-d3f5-4b38-968a-4c04b7072b53",
    // "name":"Снежная баба",
    // "latitude":54.363121,
    // "longitude":36.2023864,
    // iconId":2131230939,
    // "iconName":"ru.rusonar.praktik:drawable/ic_fishing_spot_p_gr"}]}

    /// <summary>
    /// Класс картографического объекта "Место"
    /// </summary>
    public class CPMapPlace : CPMapObject
    {
        [JsonIgnore]
        bool _isChanged;
        [JsonIgnore]
        string _name;
        [JsonIgnore]
        double _latitude;
        [JsonIgnore]
        double _longitude;

        /// <summary>
        /// Идентификатор места в формате UUID
        /// </summary>
        [JsonProperty(Order = 1, PropertyName = "placeId")]
        public string PlaceId { get; set; }

        /// <summary>
        /// Название места. Изменение устанавливает флаг isChanged
        /// </summary>
        [JsonProperty(Order = 2, PropertyName = "name")]
        public override string Name
        {
            get => _name;

            set
            {
                _name = value;
                _isChanged = true;
            }
        }

        /// <summary>
        /// Широта места. Изменение устанавливает флаг isChanged
        /// </summary>
        [JsonProperty(Order = 3, PropertyName = "latitude")]
        public override double Latitude
        {
            get => _latitude;

            set
            {
                _latitude = value;
                _isChanged = true;
            }
        }

        /// <summary>
        /// Долгота места. Изменение устанавливает флаг isChanged
        /// </summary>
        [JsonProperty(Order = 4, PropertyName = "longitude")]
        public override double Longitude
        {
            get => _longitude;

            set
            {
                _longitude = value;
                _isChanged = true;
            }
        }

        [JsonProperty(Order = 5, PropertyName = "iconId")]
        public int IconId { get; set; }

        [JsonProperty(Order = 6, PropertyName = "iconName")]
        public string IconName { get; set; }

        /// <summary>
        /// Имя файла с данными трека
        /// </summary>
        [JsonIgnore]
        public string PLC_filename { get; set; }

        [JsonIgnore]
        public bool IsChanged
        {
            get
            {
                return _isChanged;
            }
        }

        /// <summary>
        /// Сбрасывает флаг isChanged
        /// </summary>
        public void WasSaved()
        {
            _isChanged = false;
        }

        public override string ToString()
        {
            return $"{base.ToString()} '{Name}': ({Latitude}, {Longitude}) '{PLC_filename}'";
        }
    }

    /// <summary>
    /// Класс списка мест. Для совместимости с форматом обмена Практик 7
    /// </summary>
    public class CPMapPlaces
    {
        [JsonProperty(Order = 1, PropertyName = "places")]
        public List<CPMapPlace> Places;
    }

    /// <summary>
    /// Класс картографического объекта "Трек"
    /// </summary>
    public class CPMapTrack : CPMapObject
    {
        /// <summary>
        /// Назначение неясно. В данных эхолота значение равно 0
        /// </summary>
        [JsonProperty(Order = 1, PropertyName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Идентификатор трека в формате UUID
        /// </summary>
        [JsonProperty(Order = 2, PropertyName = "sessionId")]
        public string SessionId { get; set; }

        /// <summary>
        /// Название трека
        /// </summary>
        [JsonProperty(Order = 3, PropertyName = "name")]
        public override string Name { get; set; }

        /// <summary>
        /// Дата создания трека
        /// </summary>
        [JsonProperty(Order = 4, PropertyName = "date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Список точек трека
        /// </summary>
        [JsonProperty(Order = 5, PropertyName = "points")]
        public List<CPMapPoint> Points { get; set; }

        /// <summary>
        /// Идентификатор родительской сессии в формате UUID
        /// </summary>
        [JsonProperty(Order = 6, PropertyName = "parentSessionId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentSessionId { get; set; }

        /// <summary>
        /// Значение коррекции глубины, применяемое ко всем точкам данного трека
        /// </summary>
        [JsonProperty(Order = 7, PropertyName = "correctionValue")]
        public double CorrectionValue { get; set; }

        /// <summary>
        /// Путь к файлу эхограммы, соответствующей данному треку
        /// </summary>
        [JsonProperty(Order = 8, PropertyName = "fileName")]
        public string BNSCAP_filename { get; set; }

        /// <summary>
        /// Назначение неясно. В данных эхолота значение равно 2
        /// </summary>
        [JsonProperty(Order = 9, PropertyName = "paletteId")]
        public int PaletteId { get; set; }

        [JsonIgnore]
        public CPMapSession ParentSession;

        /// <summary>
        /// Возвращает true, если данный трек является корневым в сессии
        /// </summary>
        [JsonIgnore]
        public bool IsRoot
        {
            get => Index == 0;
        }

        /// <summary>
        /// Возвращает широту первой точки трека
        /// </summary>
        [JsonIgnore]
        public override double Latitude
        {
            get => Points[0].Latitude;
            set { }
        }

        /// <summary>
        /// Возвращает долготу первой точки трека
        /// </summary>
        [JsonIgnore]
        public override double Longitude
        {
            get => Points[0].Longitude;
            set { }
        }

        /// <summary>
        /// Возвращает индекс данного трека в родительской сессии
        /// </summary>
        [JsonIgnore]
        public int Index
        {
            get => ParentSession.Tracks.IndexOf(this);
        }

        /// <summary>
        /// Путь к файлу, содержащему данные трека
        /// </summary>
        [JsonIgnore]
        public string PTS_fileName { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр трека
        /// </summary>
        /// <param name="session">Ссылка на родительскую сессию трека</param>
        /// <param name="track_name">Имя нового трека</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CPMapTrack(CPMapSession session, string track_name)
        {
            //ParentSession = session ?? throw new ArgumentNullException("Трек должен принадлежать сессии");

            // в переданной сессии уже есть треки?
            if (session?.RootTrackRef != null)
            // есть, новый трек - обычный, он будет ссылаться на имеющийся корневой трек сессии
            {
                ParentSessionId = session.RootTrackRef.SessionId;
            }
            // нет, новый трек - корневой, сессия ссылается на него как на корневой
            else
            {
                ParentSessionId = null;
            }
            session?.Tracks.Add(this);

            Points = new List<CPMapPoint>();
            Name = track_name;
            PTS_fileName = track_name + ".pts";
            ID = 0;
            SessionId = Guid.NewGuid().ToString();
            Date = DateTime.Now;
            CorrectionValue = 0;
            BNSCAP_filename = "";
            PaletteId = 2;
        }

        /// <summary>
        /// Ищет дубликаты точек в треке. Сравниваются только идущие подряд точки
        /// </summary>
        /// <returns>Список дубликатов точек</returns>
        public List<CPMapPoint> FindDuplicatePoints()
        {
            List<CPMapPoint> foundPoints = new();

            // если точек 2 и больше - продолжаем
            if (Points.Count >= 2)
            {
                // проверяем подряд все точки трека
                // for (int i = 0; i < Points.Count; i++)
                foreach (CPMapPoint point in Points)
                {
                    // CPMapPoint point = Points[i];

                    // если эта точка уже есть в списке дубликатов - пропускаем эту точку
                    if (foundPoints.Contains(point))
                        continue;

                    // проверяем все остальные точки на равенство с текущей. 
                    // отсекаем сравнение точки с самой собой
                    foreach (CPMapPoint otherPoint in Points)
                    {
                        if (!point.Equals(otherPoint)
                            && ((point.Latitude == otherPoint.Latitude)
                            && (point.Longitude == otherPoint.Longitude)))
                        {
                            foundPoints.Add(otherPoint);
                        }
                    }
                }
            }

            return foundPoints;
        }

        /// <summary>
        /// Ищет точки с некорректной глубиной
        /// </summary>
        /// <param name="lowDepth"></param>
        /// <param name="useLowDepth"></param>
        /// <param name="greatDepth"></param>
        /// <param name="useGreatDepth"></param>
        /// <returns>Список точек с некорректной глубиной</returns>
        public List<CPMapPoint> FindBadDepthPoints(double lowDepth, bool useLowDepth, double greatDepth, bool useGreatDepth)
        {
            List<CPMapPoint> found_points = new ();

            foreach (CPMapPoint point in Points)
            {
                double depth = point.Depth + point.ParentTrack.CorrectionValue;
                if (((depth < lowDepth) && useLowDepth) || ((depth > greatDepth) && useGreatDepth))
                    found_points.Add(point);
            }

            return found_points;
        }

        public List<CPMapPoint> FindClosePoints(double distance)
        {
            List<CPMapPoint> found_points = new List<CPMapPoint>();

            if (Points.Count > 1)
            {
                for (int i = 1; i < Points.Count; i++)
                {
                    var points = new List<PointLatLng>()
                    {
                        new PointLatLng(Points[i].Latitude, Points[i].Longitude),
                        new PointLatLng(Points[i - 1].Latitude, Points[i - 1].Longitude)
                    };
                    var route = new GMapRoute(points, "");

                    if (route.Distance <= distance)
                    {
                        found_points.Add(Points[i]);
                    }
                }
            }

            return found_points;
        }

        public bool ApplyCorrection(double depth, bool soft_correction)
        // применяем коррекцию ко всем точкам трека
        // если soft_correction == true, то изменяем величину коррекции трека
        // если soft_correction == false, то пересчитываем глубину каждой точки трека
        // возвращает false, если хоть одна точка оказалась с отрицательной глубиной
        {
            bool result = true;

            if (soft_correction)
            {
                CorrectionValue = depth;
                foreach (CPMapPoint point in Points)
                    if ((point.Depth + CorrectionValue) < 0)
                    {
                        result = false;
                        break;
                    }
            }
            else
                if (Points.Count > 0)
            {
                foreach (CPMapPoint point in Points)
                {
                    if ((point.Depth += depth) < 0)
                        result = false;
                }
            }

            ParentSession.IsChanged = true;
            return result;
        }

        public object Clone() => MemberwiseClone();

        public override string ToString()
        {
            return $"{base.ToString()} '{ParentSession}' '{Name}' '{PTS_fileName}'";
        }

    }
    /// <summary>
    /// Класс картографического объекта "Точка"
    /// </summary>
    public class CPMapPoint : CPMapObject
    {
        [JsonProperty(Order = 1, PropertyName = "depth")]
        public double Depth { get; set; }

        [JsonProperty(Order = 2, PropertyName = "latitude")]
        public override double Latitude { get; set; }

        [JsonProperty(Order = 3, PropertyName = "longitude")]
        public override double Longitude { get; set; }

        [JsonProperty(Order = 4, PropertyName = "timePosition")]
        public long Time { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public override string Name
        {
            get => "";

            set
            {
            }
        }
        [JsonIgnore]
        public bool IsVisible { get; set; }

        [JsonIgnore]
        internal CPMapTrack ParentTrack { get; set; }

        [JsonIgnore]
        internal CPMapSession ParentSession { get; set; }

        public object Clone() => MemberwiseClone();
        public CPMapPoint()
        {
            Depth = -1;
            Latitude = -1;
            Longitude = -1;
            Time = 0;
        }

        public override string ToString()
        {
            return $"{base.ToString()} '{ParentSession?.Name}'-'{ParentTrack?.Name}': {Latitude}, {Longitude}, {Depth}";
        }
    }

    /// <summary>
    /// Класс, обеспечивающий взаимодействие пользователя с данными картографического проекта <see cref="CPMapProject"/>
    /// </summary>
    public class CPMapUIWin : IPMapUI
    {
        private CPMapProject _parentProject;
        private CPMapUISettings _settings;
        private readonly GMapControl _mapMain;
        private readonly TreeView _treePoints;
        private readonly TreeView _treePlaces;
        private readonly GMapOverlay _overlay;
        private bool _dontRefreshTreeView;
        private readonly string _const_SETTINGS_FILENAME;

        private readonly IPMapLogger _log;

        public IPMapLogger Log
        {
            get => _ = (_parentProject == null) ? _log : _parentProject.Log;
        }


        public CPMapUISettings Settings
        {
            get => _settings;
        }

        public CPMapProject ParentProject
        {
            get => _parentProject;
            set
            {
                if (_parentProject != null)
                    throw new InvalidOperationException("Ссылка на проект уже инициализирована");

                _parentProject = value ?? throw new ArgumentNullException("Пустая ссылка на проект недопустима");
            }
            
        }

        /// <summary>
        /// Сбрасывает настройки приложения на значения по умолчанию
        /// </summary>
        public void DefaultSettings()
        {
            _settings = new CPMapUISettings
            {
                selectionColor = Color.Lime,
                selectionWidth = 4,

                placeColor = Color.GreenYellow,
                placeSize = 20,

                pointBadLowDepthColor = Color.Red,
                pointBadGreatDepthColor = Color.Red,

                pointBadLowDepth = 0,
                pointBadGreatDepth = 100,

                showBadLowDepth = true,
                showBadGreatDepth = true,

                pointMinDepthColor = Color.White,
                pointMaxDepthColor = Color.Blue,
                pointSize = 20,

                pointMinDepth = 0,
                pointMaxDepth = 5
            };
        }

        /// <summary>
        /// Загружает файл настроек приложения. Если формат или данные файла некорректны, файл удаляется
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PmException_DataSourceIsNotExists">Файл настроек отсутствует</exception>
        /// <exception cref="PmException_DataSourceAccessError">Ошибка доступа к файлу настроек</exception>
        public void LoadSettings(string cfgFilename)
        {
            StreamReader jsonFile;

            // пытаемся открыть файл настроек
            try
            {
                jsonFile = new StreamReader(cfgFilename);
            }
            // файл отсутствует
            catch (FileNotFoundException ex)
            {
                throw new PmException_DataSourceIsNotExists("Файл не найден", cfgFilename, ex);
            }
            // проблема при открытии файла
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Ошибка доступа к файлу", cfgFilename, ex);
            }

            // пытаемся читать содержимое файла
            string jsonStr;
            try
            {
                jsonStr = jsonFile.ReadToEnd();
            }
            // проблема с чтением содержимого файла
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Ошибка чтения файла", cfgFilename, ex);
            }
            finally
            {
                jsonFile?.Close();
            }

            // пытаемся преобразовать содержимое файла в экземпляр класса JObject
            JObject jsonObj;
            try
            {
                jsonObj = JObject.Parse(jsonStr);
            }
            // содержимое файла не соответствует формату json, удаляем некорректный файл
            catch (Exception ex)
            {
                try
                {
                    File.Delete(cfgFilename);
                }
                catch
                {
                    throw new PmException_DataSourceAccessError("Содержимое файл настроек некорректно, удалить его не удалось", cfgFilename, ex);
                }
                
                Log.Debug($"Некорректное содержимое файла настроек: '{jsonStr}'");
                throw new PmException_DataSourceIsNotExists($"Файл настроек удален, некорректное содержимое", cfgFilename, ex);
            }

                // удалось считать информацию из файла
                _settings = jsonObj.ToObject<CPMapUISettings>();
        }

        /// <summary>
        /// Сохраняет настройки приложения на диск
        /// </summary>
        /// <param name="cfgFilename">Путь к файлу настроек</param>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        public void SaveSettings(string cfgFilename)
        {
            string json_str = JsonConvert.SerializeObject(_settings);
            StreamWriter json_file = null;
            try
            {
                json_file = new StreamWriter(cfgFilename, false);
                json_file?.Write(json_str);
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Ошибка сохранения настроек", cfgFilename, ex);
            }
            finally
            {
                json_file?.Close();
            }
        }

        /// <summary>
        /// Сохраняет настройки приложения на диск в файл CFG_FILE_NAME
        /// </summary>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        public void SaveSettings()
        {
            SaveSettings(_const_SETTINGS_FILENAME);
        }

        public bool DontRefreshTreeView
        {
            get
            {
                return _dontRefreshTreeView;
            }
        }

        public CPMapUISettings Config;

        List<CPMapSession> Sessions
        {
            get
            {
                return _parentProject.Data.Sessions;
            }
        }

        List<CPMapPlace> Places
        {
            get
            {
                return _parentProject.Data.Places;
            }
        }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CPMapUIWin"/>
        /// </summary>
        /// <param name="gMap"></param>
        /// <param name="pointsTreeView"></param>
        /// <param name="placesTreeView"></param>
        /// <exception cref="NullReferenceException"></exception>
        public CPMapUIWin(GMapControl gMap, TreeView pointsTreeView, TreeView placesTreeView, IPMapLogger log)
        {
            if (gMap == null || pointsTreeView == null || placesTreeView == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                _mapMain = gMap;
                _treePoints = pointsTreeView;
                _treePlaces = placesTreeView;
            }

            _overlay = new GMapOverlay();
            _settings = new CPMapUISettings();
            _parentProject = null;
            _dontRefreshTreeView = false;
            _log = log;

            _const_SETTINGS_FILENAME = Environment.CurrentDirectory + Path.DirectorySeparatorChar + Resources.SETTINGS_FILENAME;
            // пытаемся считать настройки из файла
            try
            {
                LoadSettings(_const_SETTINGS_FILENAME);
            }
            // файл настроек отсутствует, создаем и сохраняем файл настроек по умолчанию
            catch (PmException_DataSourceIsNotExists ex)
            {
                // создаем настройки по-умолчанию
                DefaultSettings();

                // пытаемся сохранить настройки по умолчанию
                try
                {
                    SaveSettings(_const_SETTINGS_FILENAME);
                }
                // сохранить настройки по умолчанию не удалось, логируем
                catch (Exception ex1)
                {
                    Log.Error($"Файл настроек '{ex.Path}' не найден ({ex.Message}). Пересоздать не удалось ({ex1.Message}), используются настройки по умолчанию");
                    Log.Debug($"{ex1}");
                }

                Log.Information($"Сформирован новый файл настроек '{ex.Path}' ({ex.Message})");
            }
            // ошибка доступа к файлу настроек, невозможно пересоздать файл с настройками по умолчанию
            catch (PmException_DataSourceAccessError ex)
            {
                DefaultSettings();
                Log.Error($"Ошибка доступа к файлу настроек, проигнорирован '{ex.Path}' ({ex.Message}), используются настройки по умолчанию.");
                Log.Debug($"{ex}");
            }
            // 
            catch (Exception ex)
            {
                DefaultSettings();
                Log.Error($"Неизвестная ошибка при чтении файла настроек, проигнорирован '{_const_SETTINGS_FILENAME}' ({ex.Message}), используются настройки по умолчанию.");
                Log.Debug($"{ex}");
            }
        }

        /// <summary>
        /// Возвращает стиль маркера карты для объекта проекта
        /// </summary>
        /// <param name="mapObject">Ссылка на объект проекта</param>
        /// <returns>Ссылка на экземпляр объекта стиля маркера</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PmException_NotSupportedObjectType"></exception>
        private CPMapMarkerStyle GetMarkerStyle(CPMapObject mapObject)
        {
            // проверяем корректность параметра
            _ = mapObject ?? throw new ArgumentNullException();

            CPMapMarkerStyle style = new ();

            switch (mapObject)
            {
                // маркер для точки
                case CPMapPoint point:
                    // вычисляем рабочую глубину. если глубина точки выходит за пределы глубин - присваиваем соответствующий предел
                    double depth = point.Depth + point.ParentTrack.CorrectionValue;

                    // считаем цвет точки, сначала окрашиваем некорректные точки 
                    // если слишком малая глубина, то...
                    if ((depth < Settings.pointBadLowDepth) && (Settings.showBadLowDepth))
                    {
                        style.MarkerColor = Settings.pointBadLowDepthColor;
                    } else

                    // если слишком большая глубинаглубина, то...
                    if ((depth > Settings.pointBadGreatDepth) && (Settings.showBadGreatDepth))
                    {
                        style.MarkerColor = Settings.pointBadGreatDepthColor;
                    }

                    // если глубина в норме, раскрашиваем согласно настройкам
                    else
                    {
                        if (point.Depth < Settings.pointMinDepth)
                            depth = Settings.pointMinDepth;
                        else
                        if (point.Depth > Settings.pointMaxDepth)
                            depth = Settings.pointMaxDepth;
                        else
                            depth = point.Depth;

                        // вычисляем относительное "расстояние" глубины от начала диапазона глубин 
                        double d = (depth - Settings.pointMinDepth) / (Settings.pointMaxDepth - Settings.pointMinDepth);

                        // интерполируем компоненты цветов
                        int r_st = Settings.pointMinDepthColor.R;
                        int r_fin = Settings.pointMaxDepthColor.R;
                        int g_st = Settings.pointMinDepthColor.G;
                        int g_fin = Settings.pointMaxDepthColor.G;
                        int b_st = Settings.pointMinDepthColor.B;
                        int b_fin = Settings.pointMaxDepthColor.B;

                        int r = r_st + (int)((r_fin - r_st) * d);
                        int g = g_st + (int)((g_fin - g_st) * d);
                        int b = b_st + (int)((b_fin - b_st) * d);

                        style.MarkerColor = Color.FromArgb(r, g, b);
                    }

                    // если точка не выделена, то точку обводим прозрачным цветом
                    if (_parentProject.Data.State.SelectionPointIsSelected(point))
                        style.SelectionColor = Settings.selectionColor;
                    else
                        style.SelectionColor = Color.Transparent;

                    style.SelectionWidth = Settings.selectionWidth;

                    // размер точки
                    style.Size = Settings.pointSize;

                    break;

                // маркера для места
                case CPMapPlace:
                    style.MarkerColor = Settings.placeColor;
                    style.SelectionColor = Color.Transparent;
                    style.Size = Settings.placeSize;
                    break;

                default:
                    throw new PmException_NotSupportedObjectType("Для данного типа объекта не предусмотрен стиль маркера");

            }
            return style;
        }

        /// <summary>
        /// Отображает на карте маркеры всех объектов проекта, при необходимости карта сдвигается к самому новому объекту
        /// </summary>
        /// <param name="type">Тип объекта, к которому сдвигается карта</param>
        public void MapDrawMarkers(EPMapObjectType type)
        {
            _mapMain.Overlays.Clear();
            _overlay.Clear();

            // отображаем маркеры точек
            if (Sessions.Count > 0)
            {
                for (int isession = 0; isession < Sessions.Count; isession++)
                {
                    CPMapSession session = Sessions[isession];
                    for (int itrack = 0; itrack < session.Tracks.Count; itrack++)
                    {
                        CPMapTrack track = session.Tracks[itrack];
                        for (int ipoint = 0; ipoint < track.Points.Count; ipoint++)
                        {
                            CPMapPoint point = track.Points[ipoint];

                            CPMapMarkerStyle style = GetMarkerStyle(point);
                            CPMapMarker marker = new (point, style);

                            marker.ToolTip = new GMapToolTip(marker);
                            double depth = point.Depth + track.CorrectionValue;

                            string st = (track.CorrectionValue == 0) ? "" : "[к] ";
                            marker.ToolTipText = $"{depth:#.##}м {st}({track.Name})";

                            //добавляем маркер
                            _overlay.Markers.Add(marker);
                        }
                    }
                }
            }

            // отображаем маркеры мест
            if (Places.Count > 0)
            {
                foreach (CPMapPlace place in Places)
                {
                    CPMapMarkerStyle style = GetMarkerStyle(place);
                    CPMapMarker marker = new CPMapMarker(place, style);

                    marker.ToolTip = new GMapToolTip(marker);
                    marker.ToolTipText = place.Name;

                    _overlay.Markers.Add(marker);
                }
            }

            _mapMain.Overlays.Add(_overlay);

            // если нарисовали хоть что-то, пытаемся сдвинуть карту
            if ((Sessions.Count > 0) || (Places.Count > 0))
            {

                switch (type)
                {

                    // в случае варианта "сессия" центрируем карту по первой точке последней сессии
                    case EPMapObjectType.Session:
                        if (Sessions.Count > 0)
                        {
                            _mapMain.Zoom = 17;
                            int isession = Sessions.Count - 1;
                            _mapMain.Position = new PointLatLng(Sessions[isession].Tracks[0].Points[0].Latitude, Sessions[isession].Tracks[0].Points[0].Longitude);
                        }

                        break;

                    // в случае варианта "место" центрируем карту по последнему месту в списке
                    case EPMapObjectType.Place:
                        if (Places.Count > 0)
                        {
                            _mapMain.Zoom = 17;
                            int iplace = Places.Count - 1;
                            _mapMain.Position = new PointLatLng(Places[iplace].Latitude, Places[iplace].Longitude);
                        }
                        break;

                    // другие варианты игнорируем
                    default:
                        break;
                }
            }    
        }

        /// <summary>
        /// Центрирует карту по активному объекту, если он выбран
        /// </summary>
        public void MapMoveToActiveObject()
        {
            CPMapObject obj;

            if ((obj = _parentProject.Data.State.MapObject) == null) 
                return;

            _mapMain.Position = new PointLatLng(obj.Latitude, obj.Longitude);
        }

        /// <summary>
        /// Возвращает объект, выделенный в дереве точек 
        /// </summary>
        /// <returns>Ссылка на выделенный объект, null - если объект не выделен</returns>
        public CPMapObject PointsTreeGetSelectedObject()
        {
            CPMapObject obj = null;

            int isession, itrack, ipoint;

            if (_treePoints.SelectedNode != null)
            {
                if (_treePoints.SelectedNode.Parent != null)
                {
                    //выделена точка
                    if (_treePoints.SelectedNode.Parent.Parent != null)
                    {
                        isession = _treePoints.SelectedNode.Parent.Parent.Index;
                        itrack = _treePoints.SelectedNode.Parent.Index;
                        ipoint = _treePoints.SelectedNode.Index;
                        obj = Sessions[isession].Tracks[itrack].Points[ipoint];
                    }
                    else
                    //выделен трек
                    {
                        isession = _treePoints.SelectedNode.Parent.Index;
                        itrack = _treePoints.SelectedNode.Index;
                        obj = Sessions[isession].Tracks[itrack];
                    }
                }
                else
                //выделена сессия
                {
                    isession = _treePoints.SelectedNode.Index;
                    obj = Sessions[isession];
                }
            }

            return obj;
        }

        /// <summary>
        /// Ищет объект, выделенный в дереве точек
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Ссылка на выделенный объект</returns>
        /// <exception cref="ArgumentException"></exception>
        public CPMapObject PointsTreeGetCheckedObject(object e)
        {
            if (e is not TreeViewEventArgs)
                throw new ArgumentException($"Событие имеет недопустимый тип '{e.GetType()}'");

            CPMapObject mapObject;
            TreeViewEventArgs tvEvent = (TreeViewEventArgs)e;

            if (tvEvent.Node.Parent != null)
            {
                //выбрана точка
                if (tvEvent.Node.Parent.Parent != null)
                {
                    mapObject = Sessions[tvEvent.Node.Parent.Parent.Index].Tracks[tvEvent.Node.Parent.Index].Points[tvEvent.Node.Index];
                }
                // выбран трек
                else
                {
                    mapObject = Sessions[tvEvent.Node.Parent.Index].Tracks[tvEvent.Node.Index];
                }
            }
            // выбрана сессия
            else
            {
                mapObject = Sessions[tvEvent.Node.Index];
            }

            return mapObject;
        }

        /// <summary>
        /// Устанавливает актуальные отметки узлов дерева точек
        /// </summary>
        public void PointsTreeRefreshNodeCheck()
        {
            // запрещаем обновление дерева точек, чтобы избежать рекурсивного вызова метода
            _dontRefreshTreeView = true;

            // пробегаемся по всем сессиям проекта
            for (int isession = 0; isession < Sessions.Count; isession++)
            {
                CPMapSession session = Sessions[isession];
                // пробегаемся по всем трекам сессии
                for (int itrack = 0; itrack < session.Tracks.Count; itrack++)
                {
                    CPMapTrack track = session.Tracks[itrack];
                    // пробегаемся по всем точкам трека
                    for (int ipoint = 0; ipoint < track.Points.Count; ipoint++)
                    {
                        CPMapPoint point = track.Points[ipoint];
                        _treePoints.Nodes[isession].Nodes[itrack].Nodes[ipoint].Checked = _parentProject.Data.State.SelectionPointIsSelected(point);
                    }
                }
            }
            _dontRefreshTreeView = false;
        }

        /// <summary>
        /// Формирует содержимое дерева точек - узлы вместе с отметками
        /// </summary>
        public void PointsTreeRefreshNodes()
        {
            TreeNode node;

            // запрещаем обновление дерева точек, чтобы избежать рекурсивного вызова метода
            _dontRefreshTreeView = true;

            // очищаем дерево точек
            _treePoints.Nodes.Clear();

            // пробегаемся по всем сессиям
            for (int isession = 0; isession < Sessions.Count; isession++)
            {
                CPMapSession session = Sessions[isession];
                string name = session.RootTrackRef.Name;

                if (session.IsChanged)
                    name = $"[правка] {name}";

                _treePoints.Nodes.Add(name);

                for (int itrack = 0; itrack < session.Tracks.Count; itrack++)
                {
                    CPMapTrack track = session.Tracks[itrack];
                    string node_text = track.Name;
                    if (track.CorrectionValue != 0)
                        node_text += $" (корр. = {track.CorrectionValue} м)";

                    _treePoints.Nodes[isession].Nodes.Add(node_text);

                    for (int ipoint = 0; ipoint < track.Points.Count; ipoint++)
                    {
                        CPMapPoint point = track.Points[ipoint];
                        node_text = $"{point.Latitude}; {point.Longitude} ({(point.Depth + track.CorrectionValue):#.##}м)";

                        if (track.CorrectionValue != 0)
                            node_text += " [к]";
                        node = _treePoints.Nodes[isession].Nodes[itrack].Nodes.Add(node_text);
                        node.Checked = _parentProject.Data.State.SelectionPointIsSelected(point);
                    }
                }
            }

            _treePoints.ExpandAll();
            _dontRefreshTreeView = false;
        }

        /// <summary>
        /// Формирует содержимое дерева мест
        /// </summary>
        public void PlacesTreeRefreshNodes()
        {
            // очищаем дерево мест
            _treePlaces.Nodes.Clear();

            // пробегаемся по всем сессиям
            foreach (CPMapPlace place in Places)
            {
                string node_label = $"{place.Name} [{place.Latitude}; {place.Longitude}]";
                if (place.IsChanged)
                    node_label = "[правка] " + node_label;
                _treePlaces.Nodes.Add(node_label);
            }

            _treePlaces.ExpandAll();
        }

        /// <summary>
        /// Возвращает объект, выделенный в дереве мест 
        /// </summary>
        /// <returns>Ссылка на выделенный объект, null - если объект не выделен</returns>
        public CPMapObject PlacesTreeGetSelectedObject()
        {
            CPMapObject place = null;

            if (_treePlaces.SelectedNode != null)
            {
                int iplace = _treePlaces.SelectedNode.Index;

                if ((iplace >= 0) && (iplace < Places.Count))
                {
                    place = Places[iplace];
                } else
                {
                    throw new PmException_ObjectNotFound("Выделенное место не найдено в проекте");
                }
            }
            return place;
        }
    }

    /// <summary>
    /// Класс картографического проекта "Практик"
    /// </summary>
    public class CPMapProject
    {
        private string _lastMessage;

        /// <summary>
        /// Ссылка на данные проекта
        /// </summary>
        public readonly CPMapData Data;

        /// <summary>
        /// Ссылка на обработчик пользовательского интерфейса
        /// </summary>
        public readonly IPMapUI UI;
        
        /// <summary>
        /// Ссылка на логгер проекта
        /// </summary>
        public readonly IPMapLogger Log;

        /// <summary>
        /// Сообщение о некритичных ошибках в работе проекта
        /// </summary>
        // TODO потом прикрутить
        public string LastMessage
        {
            get => _lastMessage;
        }

        /// <summary>
        /// Инициализирует экземпляр класса картографического проекта "Практик"
        /// </summary>
        /// <param name="ui">Экземпляр интерфейса <see cref="IPMapUI"/></param>
        /// <param name="log">Экземпляр интерфейса <see cref="IPMapLogger"/></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        public CPMapProject(IPMapUI ui, IPMapLogger log)
        {
            _ = ui ?? throw new NullReferenceException("Пустая ссылка на обработчик пользовательского интерфейса");
            _ = log ?? throw new NullReferenceException("Пустая ссылка на логгер");

            Log = log;
            Log.DebugMode = File.Exists(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "debug.txt");

            UI = ui;
            UI.ParentProject = this;

            _lastMessage = "";

            // инициализируем экземпляры внутренних классов
            // ДАННЫЕ
            // подготавливаем хранилище данных проекта и временный каталог
            // не обрабатываем исключения - их обработают снаружи
            Data = new CPMapData(this);

            // ищем и загружаем имеющиеся данные проекта
            // проверяем некритичные исключения - пишем в лог и фиксируем описание в _lastMessage
            try
            {
                Data.LoadProject();
            }
            catch (PmException_DataSourceIsEmpty ex)
            {
                _lastMessage = "Проект не содержит данных";

                Log.Information($"Каталог с данными проекта не содержит данных ({ex.Message}) '{ex.Path}'");
                Log.Debug($"{ex}");
            }
            catch (PmException_DataIsPartiallyIncorrect ex)
            {
                _lastMessage = "Проект содержит некорректные данные";

                Log.Warning($"Данные проекта загружены не полностью ({ex.Message})");
                Log.Debug($"{ex}");
            }
        }
    }
    /// <summary>
    /// Класс набора данных картографического проекта
    /// </summary>
    public class CPMapData
    {
        private readonly List<CPMapSession> _sessions;
        private readonly List<CPMapPlace> _places;
        private readonly CPMapDataState _state;

        private readonly string _projectDirectory;
        private readonly CPMapProject _parentProject;

        /// <summary>
        /// Логгер родительского проекта
        /// </summary>
        public IPMapLogger Log
        {
            get => _parentProject.Log;
        }

        /// <summary>
        /// Ссылка на родительский проект
        /// </summary>
        public CPMapProject ParentProject
        {
            get => _parentProject;
        }

        /// <summary>
        /// Возвращает ссылку на данные текущего состояния проекта
        /// </summary>
        public CPMapDataState State
        {
            get => _state;
        }

        /// <summary>
        /// Возвращает ссылку на список сессий проекта
        /// </summary>
        public List<CPMapSession> Sessions
        {
            get => _sessions;
        }

        /// <summary>
        /// Возвращает ссылку на список мест проекта
        /// </summary>
        public List<CPMapPlace> Places
        {
            get => _places;
        }

        /// <summary>
        /// Возвращает полное имя каталога, в котором находятся данные проекта
        /// </summary>
        public string ProjectDirectory
        {
            get => _projectDirectory;
        }

        /// <summary>
        /// Возвращает список списков имен треков в сессиях проекта 
        /// </summary>
        public List<List<string>> Names
        {
            get
            {
                List<List<string>> all_names = new();

                foreach (CPMapSession session in _sessions)
                {
                    List<string> names = new();
                    foreach (CPMapTrack track in session.Tracks)
                        names.Add(track.Name);
                    all_names.Add(names);
                }
                return all_names;
            }
        }

        /// <summary>
        /// Инициализирует экземпляр класса данных картографического проекта
        /// </summary>
        /// <param name="parentProject">Ссылка на родительский проект</param>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        public CPMapData(CPMapProject parentProject)
        {
            _parentProject = parentProject;
            _sessions = new ();
            _places = new ();
            _state = new ();
            _projectDirectory = Environment.CurrentDirectory + Path.DirectorySeparatorChar + Resources.DATA_SUBDIR;

            string tempDirectory = Environment.CurrentDirectory + Path.DirectorySeparatorChar + Resources.TEMP_SUBDIR;

            try
            // очищаем временный каталог проекта
            {
                if (Directory.Exists(tempDirectory))
                    Directory.Delete(tempDirectory, true);
                Directory.CreateDirectory(tempDirectory);
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Не удалось подготовить временный каталог", tempDirectory, ex);
            }
        }

        /// <summary>
        /// Копирует выделенные точки в заданную сессию/трек
        /// </summary>
        /// <param name="p_session">Ссылка на сессию, если null, то создается новая</param>
        /// <param name="p_track">Ссылка на трек, если = null, то создается новый</param>
        public void SelectionCopyTo(CPMapSession p_session, CPMapTrack p_track)
        {

            // если нет выделеных точек - выход
            if (State.SelectedPoints.Count == 0)
                return;

            CPMapSession _session;
            CPMapTrack _track;

            // если передана сессия == null, создаем новую пустую сессию, точки копируем в корневой трек
            if (p_session == null)
            {
                _session = AddNewSession();
                _track = _session.RootTrackRef;
            }
            // работаем с существующей сессией
            else
            {
                _session = p_session;

                // если передан трек == null, добавляем новый пустой трек к сессии
                if (p_track == null)
                {
                    _track = AddNewTrack(_session);
                }
                else
                    _track = p_track;
            }

            // копируем выделенные точки в обозначенное место
            foreach (CPMapPoint _point in State.SelectedPoints)
            {
                CPMapPoint _newpoint = (CPMapPoint)_point.Clone();
                _newpoint.ParentTrack = _track;
                _newpoint.ParentSession = _session;

                _track.Points.Add(_newpoint);
            }

            _session.IsChanged = true;
        }

        /// <summary>
        /// Добавляет новую сессию с пустым корневым треком
        /// </summary>
        /// <returns>Ссылка на добавленную сессию</returns>
        private CPMapSession AddNewSession()
        {
            DateTime now = DateTime.Now;
            string name = "Сессия " + now.ToString("yyyy-MM-dd_HH-mm-ss");
            string dir = ProjectDirectory + Path.DirectorySeparatorChar + now.ToString("yyyy-MM-dd_HH-mm-ss");

            CPMapSession session = new (dir, name);

            // добавляем пустую сессию в список сессий проекта
            _sessions.Add(session);
            return session;
        }

        /// <summary>
        /// Добавляет пустой трек к указанной сессии
        /// </summary>
        /// <param name="session">Ссылка на сессию, в которую будет добавлен трек</param>
        /// <returns>Ссылка на добавленный трек</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private CPMapTrack AddNewTrack(CPMapSession session)
        {
            _ = session ?? throw new ArgumentNullException("Ссылка на сессию не должна быть пустой");

            string name = "Трек " + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            CPMapTrack track = new (session, name);
            session.IsChanged = true;

            return track;
        }

        /// <summary>
        /// Добавляет точку к указанному треку
        /// </summary>
        /// <param name="track">Ссылка на трек, в который будет добавлена точка</param>
        /// <param name="point">Добавляемая точка</param>
        private void AddNewPoint(CPMapTrack track, CPMapPoint point)
        {
            point.ParentTrack = track;
            point.ParentSession = track.ParentSession;

            track.Points.Add(point);
            track.ParentSession.IsChanged = true;
        }

        /// <summary>
        /// Очищает директорию данных проекта
        /// </summary>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        public void ClearProjectDirectory()
        {
            try
            {
                Directory.Delete(ProjectDirectory, true);
            }
            catch (DirectoryNotFoundException) { }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Проблема при удалении каталога данных", ProjectDirectory, ex);
            }

            try
            {
                Directory.CreateDirectory(ProjectDirectory);
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Проблема при создании каталога данных", ProjectDirectory, ex);
            }
        }

        /// <summary>
        /// Удаляет указанный картографический объект
        /// </summary>
        /// <param name="mapObject">Ссылка на картографический объект</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        /// <exception cref="PmException_EmptyParentAfterDeletion"></exception>
        /// <exception cref="PmException_NotSupportedObjectType"></exception>
        private void DeleteObject(CPMapObject mapObject)
        {
            switch (mapObject) 
            {
                case CPMapSession session:
                    DeleteObject(session);
                    break;

                case CPMapTrack track:
                    DeleteObject(track);
                    break;

                case CPMapPoint point:
                    DeleteObject(point);
                    break;

                case CPMapPlace place:
                    DeleteObject(place);
                    break;

                default:
                    throw new PmException_NotSupportedObjectType("Не опознан тип удаляемого объекта");
            }
        }

        /// <summary>
        /// Удаляет сессию проекта
        /// </summary>
        /// <param name="session">Ссылка на сессию проекта</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        private void DeleteObject(CPMapSession session)
        {
            if (session == null)
                throw new ArgumentNullException();
            if (_sessions.IndexOf(session) == -1)
                throw new ArgumentException("Сессия не принадлежит текущему проекту");

            try
            {
                Directory.Delete(session.DataDirectory, true);
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Ошибка удаления каталога сессии", session.DataDirectory, ex);
            }
            finally
            {
                _sessions.Remove(session);
            }
        }

        /// <summary>
        /// Удаляет трек проекта
        /// </summary>
        /// <param name="track">Ссылка на трек проекта</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PmException_EmptyParentAfterDeletion"></exception>
        private void DeleteObject(CPMapTrack track)
        {
            // передали null
            if (track == null)
                throw new ArgumentNullException();

            // передали трек, у которого родительская сессия == null
            if (track.ParentSession == null)
                throw new ArgumentException("Трек не принадлежит сессии");

            // в списке треков родительской сессии данный трек отсутствует
            if (_sessions.IndexOf(track.ParentSession) == -1)
                throw new ArgumentException("Трек принадлежит сессии, которая не принадлежит текущему проекту");

            // последний трек удалить нельзя, нужно удалить родительскую сессию
            if (track.ParentSession.Tracks.Count == 1)
                throw new PmException_EmptyParentAfterDeletion("Последний трек сессии удалить нельзя");

            // какой трек в итоге удаляем?
            if (track.IsRoot)
            {
                // удаляем корневой трек
                // трек, следующий за корневым [1], станет корневым, его родительская ссылка обнуляется,..
                track.ParentSession.Tracks[1].ParentSessionId = null;

                // ...а остальным трекам нужно прописать на него ссылку
                for (int i = 2; i < track.ParentSession.Tracks.Count; i++)
                {
                    track.ParentSession.Tracks[i].ParentSessionId = track.ParentSession.Tracks[1].SessionId;
                }

            }
            track.ParentSession.Tracks.Remove(track);
            track.ParentSession.IsChanged = true;
        }

        /// <summary>
        /// Удаляет точку проекта
        /// </summary>
        /// <param name="point">Ссылка на точку проекта</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PmException_EmptyParentAfterDeletion"></exception>
        private void DeleteObject(CPMapPoint point)
        {
            // попытка удалить точку == null
            _ = point ?? throw new ArgumentNullException("Пустая ссылка на точку");

            // попытка удалить точку без ссылки на корневой трек
            _ = point.ParentTrack ?? throw new ArgumentException("Точка не принадлежит треку");

            // попытка удалить точку без ссылки на сессию
            _ = point.ParentSession ?? throw new ArgumentException("Точка не принадлежит сессии");

            // попытка удалить точку, но либо она сама, либо ее корневые трек/сессия не принадлежат проекту
            if ((point.ParentSession.Tracks.IndexOf(point.ParentTrack) == -1) || 
                (_sessions.IndexOf(point.ParentSession) == -1) ||
                (point.ParentTrack.Points.IndexOf(point) == -1))
                    throw new ArgumentException("Точка не принадлежит проекту");

            // последняя точка в треке
            if (point.ParentTrack.Points.Count == 1)
                throw new PmException_EmptyParentAfterDeletion("Последнюю точку трека удалить нельзя");

            point.ParentTrack.Points.Remove(point);
            point.ParentSession.IsChanged = true;

        }

        /// <summary>
        /// Удаляет место проекта
        /// </summary>
        /// <param name="place">Ссылка на место проекта</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        private void DeleteObject(CPMapPlace place)
        {
            // попытка удалить место == null
            if (place == null)
                throw new ArgumentNullException();
            
            // попытка удалить место, не принадлежащее проекту
            if (_places.IndexOf(place) == -1)
                throw new ArgumentException("Место не принадлежит проекту");

            _places.Remove(place);

            try
            {
                File.Delete(place.PLC_filename);
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Ошибка удаления файла места", place.PLC_filename, ex);
            } 
        }

        /// <summary>
        /// Возвращает ссылку на объект, который нужно фактически удалять при удалении данного объекта
        /// </summary>
        /// <param name="mapObject">Ссылка на удаляемый объект</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CPMapObject GetFinalDeletionObject(CPMapObject mapObject)
        // 1. если удаляем последний трек - удаляется сессия,
        // 2. если удаляем последнюю точку в треке, то удаляем трек,
        // 3. если же этот трек последний, то см. п.1
        {
            _ = mapObject ?? throw new ArgumentNullException();

            CPMapObject result;

            switch (mapObject)
            {
                // пытаемся удалить трек
                case CPMapTrack track:
                    // проверяем, этот трек - единственный?
                    if (track.ParentSession.Tracks.Count == 1)
                    {
                        // трек - единственный, нужно удалять сессию, возвращаем ссылку на сессию
                        result = track.ParentSession;
                    }
                    else
                    {
                        // трек - не единственный, возвращаем ссылку на сам трек
                        result = track;
                    }
                    break;

                // пытаемся удалить точку
                case CPMapPoint point:

                    // проверяем, данная точка - единственная в треке?
                    // если единственная - проверяем на удаление ее родительский трек
                    if (point.ParentTrack.Points.Count == 1)
                    {
                        result = GetFinalDeletionObject(point.ParentTrack);
                    }
                    else
                    // данная точка - не единственная, возвращаем ссылку на саму точку
                    {
                        result = point;
                    }
                    break;

                default:
                    result = mapObject;

                    break;
            }
            return result;
        }

        /// <summary>
        /// Удаляет выделенный (активный) объект проекта
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        /// <exception cref="PmException_EmptyParentAfterDeletion"></exception>
        /// <exception cref="PmException_NotSupportedObjectType"></exception>
        public void DeleteActiveObject()
        {
            CPMapObject obj = GetFinalDeletionObject(State.MapObject);
            DeleteObject(obj);
        }

        /// <summary>
        /// Удаляет выделенные точки
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        /// <exception cref="PmException_EmptyParentAfterDeletion"></exception>
        /// <exception cref="PmException_NotSupportedObjectType"></exception>
        public void DeleteSelectedPoints()
        {
            foreach (CPMapPoint point in State.SelectedPoints)
            {
                CPMapObject obj = GetFinalDeletionObject(point);
                DeleteObject(obj);
            }
        }

        /// <summary>
        /// Считывает сессию - картографическую информацию из файлов *.PTS в указанной директории
        /// </summary>
        /// <param name="dir"></param>
        /// <exception cref="PmException_DataSourceIsNotExists"></exception>
        /// <exception cref="PmException_DataSourceIsEmpty"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        /// <exception cref="PmException_DataIsPartiallyIncorrect"></exception>
        public void LoadSessionFromDir_PTS(string dir)
        {
            string[] ptsFilenames;

            try
            // пытаемся найти файлы треков из указанного каталога сессии
            {
                ptsFilenames = Directory.GetFiles(dir, "*.pts");
            }
            catch (DirectoryNotFoundException ex)
            // каталог сессии не найден
            {
                throw new PmException_DataSourceIsNotExists("Каталог не найден", dir, ex);
            }
            catch (Exception ex)
            // другая неприятная ошибка
            {
                throw new PmException_DataSourceAccessError("Ошибка доступа к каталогу", dir, ex);
            }

            // в каталоге сессии нет данных
            if (ptsFilenames?.Length == 0)
                throw new PmException_DataSourceIsEmpty("Файлы *.pts отсутствуют", dir);

            // каталог с данными существует, читаем содержимое
            bool isBadPTShere = false;
            CPMapSession session = new (dir);

            foreach (string ptsFilename in ptsFilenames)
            {
                // считываем содержимое всех найденных файлов *.pts по очереди
                StreamReader jsonPtsFile = null;
                string jsonString;
                try
                // пытаемся прочитать файл
                {
                    jsonPtsFile = new StreamReader(ptsFilename);
                    jsonString = jsonPtsFile?.ReadToEnd();
                }
                catch (Exception ex)
                // ошибка открытия/чтения *.pts - пропускаем файл, переходим к следующему
                {
                    Log.Error($"Ошибка чтения файла '{ptsFilename}' ({ex.Message})");
                    Log.Debug($"{ex}");
                    isBadPTShere = true;
                    continue;
                }
                finally
                {
                    jsonPtsFile?.Close();
                }

                JObject jsonObject;
                try
                // файл прочитался, пытаемся преобразовать в объект json
                {
                    jsonObject = JObject.Parse(jsonString);
                }
                catch (Exception ex)
                // не получилось из-за ошибки формата - пропускаем файл, переходим к следующему
                {
                    Log.Error($"Пропущен файл '{ptsFilename}', некорректное JSON-содержимое: '{jsonString}'");
                    Log.Debug($"{ex}");
                    isBadPTShere = true;
                    continue;
                }

                CPMapTrack currentTrack = jsonObject.ToObject<CPMapTrack>();
                currentTrack.PTS_fileName = Path.GetFileName(ptsFilename);

                // проверяем корректность считанных точек, нет ли пропущенных полей
                // если найдется хоть одна битая точка - весь трек будет отброшен
                bool isBadPointHere = false;
                foreach (CPMapPoint point in currentTrack.Points)
                {
                    // ошибка - считана некорректная точка, прекращаем проверку трека
                    if ((point.Longitude == -1) || (point.Latitude == -1) || (point.Depth == -1))
                    {
                        isBadPointHere = true;
                        break;
                    }
                    point.ParentTrack = currentTrack;
                    point.ParentSession = session;
                }

                // в треке была найдена некорректная точка, текущий трек отбрасываем и переходим к следующему треку
                if (isBadPointHere)
                {
                    Log.Debug($"Пропущен некорректный файл: '{ptsFilename}': '{jsonString}'");
                    isBadPTShere = true;
                    continue;
                }

                currentTrack.ParentSession = session;
                // файл с корневым треком вставляем в начало списка
                if (currentTrack.ParentSessionId == null)
                {
                    session.Tracks.Insert(0, currentTrack);
                }
                // файл с некорневым треком - вставляем в конец списка
                else
                {
                    session.Tracks.Add(currentTrack);
                }
            }
            // проверям, есть ли в итоге корректные треки в сессии?
            if (session.Tracks.Count == 0)
            // нет - исключение
            {
                throw new PmException_DataSourceIsEmpty("Отсутствуют корректные файлы *.pts", dir);
            }
            else
            // корректные данные есть, добавляем считанную сессию в список сессий проекта
            {
                session.IsChanged = false;
                _sessions.Add(session);
            }

            // были ли найдены ошибки в процессе считывания сессии?
            if (isBadPTShere)
            // ошибки были - выбрасываем исключение, сессия остается
            {
                throw new PmException_DataIsPartiallyIncorrect("Некоторые файлы *.pts некорректны", dir);
            }
        }

        /// <summary>
        /// Загружает файл места в проект
        /// </summary>
        /// <param name="filename">Путь к файлу места</param>
        /// <param name="copyToProject">Нужно ли скопировать файл в проект?</param>
        /// <exception cref="PmException_DataSourceIsNotExists"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        /// <exception cref="PmException_DataSourceIsEmpty"></exception>
        public void LoadFromFile_PLC(string filename, bool copyToProject)
        {
            StreamReader plcJsonFile = null;
            string jsonString;

            try
            {
                plcJsonFile = new StreamReader(filename);
                jsonString = plcJsonFile?.ReadToEnd();
            }
            catch (FileNotFoundException ex)
            {
                throw new PmException_DataSourceIsNotExists("Файл не найден", filename, ex);
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Ошибка при открытии", filename, ex);
            }
            finally
            { 
                plcJsonFile?.Close(); 
            }

            JObject jsonObject;

            try
            {
                jsonObject = JObject.Parse(jsonString);
            }
            catch
            // ошибка - некорректное содержимое файла
            {
                Log.Debug($"Некорректный формат JSON: '{jsonString}'");
                throw new PmException_DataSourceIsEmpty("Ошибка в JSON", filename);
            }

            CPMapPlaces json_places = jsonObject.ToObject<CPMapPlaces>();
            if ((json_places.Places == null) || (json_places.Places.Count == 0))
            // преобразование прошло успешно, но:
            // 1. json корректен, но поле Places отсутствовало
            // 2. json корректен, поле Places пустое
            {
                throw new PmException_DataSourceIsEmpty("В файле отсутствуют данные о местах", filename);
            }

            string newFilename = filename;

            bool placeTransferError = false;

            if (copyToProject)
            // если требуется скопировать файл с местом в проект
            {
                newFilename = ProjectDirectory + Path.DirectorySeparatorChar + Path.GetFileName(filename);
                // проверяем, есть ли уже такой файл в проекте. если есть, то дописываем "_1" к имени
                while (File.Exists(newFilename))
                {
                    newFilename = ProjectDirectory + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(newFilename) + "_1" + Path.GetExtension(filename);
                }
                try
                // пытаемся скопировать файл места в проект
                {
                    File.Copy(filename, newFilename);
                }
                catch (Exception ex)
                // не получилось, прописываем в экземпляре месте путь к оригинальному файлу
                {
                    placeTransferError = true;
                    newFilename = filename;

                    Log.Debug($"{ex}");
                }
            }

            // формат файла PLC подразумевает, что в одном файле может быть несколько мест.
            // на практике такого не встречалось, но честно обрабатываем потенциальный массив мест

            // всем загруженным местам прописываем имя файла, в котором они находятся
            // сбрасываем месту признак внесения изменений
            foreach (CPMapPlace place in json_places.Places)
            {
                place.PLC_filename = newFilename;
                place.WasSaved();
            }

            // добавляем места в проект
            _places.AddRange(json_places.Places);

            if (copyToProject && placeTransferError)
            // Файл не удалось скопировать в проект, место не сохранится в проекте после перезапуска приложения
            {
                throw new PmException_DataSourceAccessError("Места загружены, но файл не сохранен в проект", filename);
            }
        }

        /// <summary>
        /// Считывает сессию из файла CSV и сохраняет в проекте
        /// </summary>
        /// <param name="filename">Путь к файлу CSV</param>
        /// <returns></returns>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        /// <exception cref="PmException_DataSourceIsEmpty"></exception>
        public CPMapSession LoadFromFile_CSV(string filename)
        {
            // открываем файл для чтения
            StreamReader csv_file;
            try
            {
                csv_file = new StreamReader(filename);
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Ошибка открытия файла", filename, ex);
            }

            // создаем новую директорию в директории проекта
            string now = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string newdir = ProjectDirectory + Path.DirectorySeparatorChar + now;

            try
            {
                Directory.CreateDirectory(newdir);
            }
            catch (Exception ex) 
            {
                csv_file.Close();
                throw new PmException_DataSourceAccessError("Не удалось создать каталог для данных сессии", newdir, ex);
            }

            CPMapSession session = AddNewSession();
            CPMapTrack track = session.RootTrackRef;
            int points_count = 0;

            // читаем содержимое файла построчно
            string csv_line;

            do
            {
                try
                {
                    csv_line = csv_file.ReadLine();
                }
                catch (Exception ex)
                {
                    csv_file.Close();
                    throw new PmException_DataSourceAccessError("Ошибка чтения данных из файла", filename, ex);
                }

                // достигнут конец файла
                if (csv_line == null)
                    continue;
                // предполагаем, что в каждой строке 3 элемента, разделенных ';'
                string[] point_strings = csv_line.Split(';');

                // строка не разделилась на 3 элемента, пропускаем
                if (point_strings.Length != 3)
                    continue;

                // строка разделилась, но в ней нет корректных чисел
                if ((!double.TryParse(point_strings[0], out double lat)) || (!double.TryParse(point_strings[1], out double lon)) || (!double.TryParse(point_strings[2], out double dep)))
                    continue;

                // числа корректные, но отрицательные
                if ((lat < 0) || (lon < 0) || (dep < 0))
                    continue;

                // данные корректные, сохраняем точку
                CPMapPoint point = new() { Latitude = lat, Longitude = lon, Depth = dep };

                AddNewPoint(track, point);
                points_count++;

            } while (csv_line != null);

            csv_file.Close();

            // сколько точек удалось прочитать из файла?
            if (points_count > 0)
            // больше нуля
            {
                try
                {
                    SaveSessionToDirPTS(newdir, session, true);
                }
                catch (Exception ex)
                {
                    Log.Error($"Ошибка при сохранении новой сессии '{session}' в каталог '{newdir}' ({ex.Message})");
                    Log.Debug($"{ex}");
                }
                return session;
            }
            else
            // ноль точек считано, удаляем созданную пустую сессию
            {
                DeleteObject(session);
                throw new PmException_DataSourceIsEmpty("Ни одной точки прочитать не удалось", filename);
            }
        }
        
        /// <summary>
        /// Загружает сессию из файла экспорта TAR
        /// </summary>
        /// <param name="filename">Путь к файлу TAR</param>
        /// <exception cref="PmException_DataSourceIsNotExists"></exception>
        /// <exception cref="PmException_DataSourceIsEmpty"></exception>
        /// <exception cref="PmException_DataIsPartiallyIncorrect"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        /// <exception cref="PmException_ExternalUtilityError"></exception>
        public void LoadFromFile_TAR(string filename)
        {
            if (!File.Exists(filename))
                throw new PmException_DataSourceIsNotExists("Файл не найден", filename);

            // создаем новую директорию в директории проекта
            string now = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string newDir = ProjectDirectory + Path.DirectorySeparatorChar + now;

            // 7z.exe x "file.tar" -o"Output_directory"
            string archivatorPath = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}7z.exe";

            if (!File.Exists(archivatorPath))
                throw new PmException_ExternalUtilityError("Архиватор 7z.exe отсутствует");

            try
            {
                Directory.CreateDirectory(newDir);
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Не удалось создать папку для сессии", filename, ex);
            }

            string argumentsString = $"x \"{filename}\" -o\"{newDir}\"";

            Process proc = new ();
            proc.StartInfo.FileName = archivatorPath;
            proc.StartInfo.Arguments = argumentsString;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                throw new PmException_ExternalUtilityError("Ошибка при работе архиватора", ex);
            }

            // все исключения обрабатываем выше
            LoadSessionFromDir_PTS(newDir);
        }

        /// <summary>
        /// Загружает проект из рабочей директории ProjectDirectory
        /// </summary>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        /// <exception cref="PmException_DataSourceIsEmpty"></exception>
        /// <exception cref="PmException_DataIsPartiallyIncorrect"></exception>
        public void LoadProject()
        {
            // проверяем, существует ли каталог с рабочим проектом?
            if (!Directory.Exists(ProjectDirectory))
            {
                // нет рабочей директории, создаем пустую
                try
                {
                    Directory.CreateDirectory(ProjectDirectory);
                }
                catch (Exception ex)
                {
                    throw new PmException_DataSourceAccessError("Каталог проекта не существует, создать не удалось.", ProjectDirectory, ex);
                }
                // создали пустой каталог - загружать нечего, выходим
                throw new PmException_DataSourceIsEmpty("Каталог проекта отсутствовал, создан пустой", ProjectDirectory);
            }

            // загружаем сессии - каждая сессия в отдельном подкаталоге
            string[] sessionsDirectories;

            try
            // пытаемся найти каталоги сессий в каталоге проекта
            {
                sessionsDirectories = Directory.GetDirectories(ProjectDirectory);
            }
            catch (Exception ex)
            // возникла ошибка
            {
                throw new PmException_DataSourceAccessError("Ошибка при чтении каталога проекта", ProjectDirectory, ex);
            }

            int corruptedSessionsCount = 0;

            if (sessionsDirectories.Length == 0)
            // в каталоге проекта нет подкаталогов сессий
            {
                Log.Information($"Каталог проекта '{ProjectDirectory}' не содержит подкаталогов сессий");
            }
            else
            {
                _sessions.Clear();

                foreach (string session_dir in sessionsDirectories)
                {

                    try
                    // пытаемся загрузить данные сессий
                    {
                        LoadSessionFromDir_PTS(session_dir);
                    }
                    catch (PmException_DataSourceAccessError ex)
                    // ошибка доступа к каталогу сессии, пропускаем
                    {
                        corruptedSessionsCount++;
                        Log.Error($"Ошибка доступа к каталогу сессии ({ex.Message}): '{ex.Path}'");
                        Log.Debug($"{ex}");
                        continue;
                    }
                    catch (PmException_DataSourceIsEmpty ex)
                    // каталог сессии не содержит корректных данных, пропускаем
                    {
                        corruptedSessionsCount++;
                        Log.Error($"Сессия не содержит данных ({ex.Message}): '{ex.Path}'");
                        Log.Debug($"{ex}");
                        continue;
                    }
                    catch (PmException_DataIsPartiallyIncorrect ex)
                    // 
                    {
                        corruptedSessionsCount++;
                        Log.Error($"Сессия загружена не полностью ({ex.Message}): '{ex.Path}'");
                        Log.Debug($"{ex}");
                        continue;
                    }
                }
            }

            // загружаем все места
            string[] plcFiles;
            try
            {
                plcFiles = Directory.GetFiles(ProjectDirectory, "*.plc");
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка при получении списка файлов мест *.plc в каталоге проекта '{ProjectDirectory}' ({ex.Message})");
                Log.Debug($"{ex}");
                plcFiles = null;
            }

            int corruptedPlacesCount = 0;

            if ((plcFiles?.Length == 0) || (plcFiles == null))
            // в каталоге проекта нет файлов мест *.plc
            {
                Log.Information($"Каталог проекта '{ProjectDirectory}' не содержит файлов мест *.plc");
            } else
            // файлы мест найдены, пытаемся их загрузить
            {
                _places.Clear();

                foreach (string placeFile in plcFiles)
                {
                    try
                    // пытаемся загрузить места из файлов
                    // в проект копировать не нужно, они уже там
                    {
                        LoadFromFile_PLC(placeFile, copyToProject: false);
                    }
                    catch (PmException_DataSourceIsNotExists ex)
                    {
                        corruptedPlacesCount++;

                        Log.Warning($"Файл места {placeFile} не найден ({ex.Message})");
                        Log.Debug($"{ex}");
                    }
                    catch (PmException_DataSourceIsEmpty ex)
                    {
                        corruptedPlacesCount++;

                        Log.Warning($"Файл места {placeFile} не содержит данных ({ex.Message})");
                        Log.Debug($"{ex}");
                    }
                    catch (PmException_DataSourceAccessError ex)
                    {
                        corruptedPlacesCount++;

                        Log.Error($"Ошибка при открытии файла места '{placeFile}' ({ex.Message})");
                        Log.Debug($"{ex}");
                    }
                }
            }

            if ((corruptedSessionsCount > 0) || (corruptedPlacesCount > 0))
            {
                int sessionCount;
                int placesCount;

                sessionCount = (sessionsDirectories == null) ? 0 : sessionsDirectories.Length;
                placesCount = (plcFiles == null) ? 0 : plcFiles.Length;

                throw new PmException_DataIsPartiallyIncorrect
                    ($"Не загружено сессий {corruptedSessionsCount} из {sessionCount}, мест {corruptedPlacesCount} из {placesCount}", 
                        ProjectDirectory);
            }
        }

        /// <summary>
        /// Сохраняет все сессию или отдельный ее трек в файл формата CSV
        /// </summary>
        /// <param name="pPath">Путь к файлу для сохранения данных</param>
        /// <param name="pSession">Ссылка на сессию</param>
        /// <param name="pTrack">Ссылка на трек. Если ссылка пустая, сохраняется вся сессия, иначе - только этот трек</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        public void SaveSessionToCSV(string pPath, CPMapSession pSession, CPMapTrack pTrack)
        {
            _ = pSession ?? throw new ArgumentNullException("Ссылка на сессию не может быть пустой");
            
            StreamWriter csv_file;

            try
            {
                csv_file = new StreamWriter(pPath, true);
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError($"Ошибка при открытии файла '{ex.Message}'", pPath, ex);
            }

            try
            {
                // сохраняем только указанный трек
                if (pTrack != null)
                {
                    foreach (CPMapPoint point in pTrack.Points)
                        csv_file.WriteLine($"{point.Latitude};{point.Longitude};{point.Depth}");
                }
                // сохраняем всю сессию
                else
                {
                    // пробегаемся по всем трекам сессии
                    foreach (CPMapTrack track in pSession.Tracks)
                        foreach (CPMapPoint point in track.Points)
                            csv_file.WriteLine($"{point.Latitude};{point.Longitude};{point.Depth}");
                }
            } 
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError($"Ошибка при записи в файл '{ex.Message}'", pPath, ex);
            }
            finally
            {
                csv_file.Close();
            }
        }

        /// <summary>
        /// Сохраняет сессию в папку в виде набора файлов PTS. Папка предварительно полностью очищается
        /// </summary>
        /// <param name="pPath">Путь к папке для сохранения данных</param>
        /// <param name="pSession">Ссылка на сессию проекта</param>
        /// <param name="keepOldId">Сохранять старый идентификатор сессии?</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        /// <exception cref="PmException_DataIsPartiallyIncorrect"></exception>
        public void SaveSessionToDirPTS(string pPath, CPMapSession pSession, bool keepOldId)
        // keepOldId - false = сгенерировать новый session_id (использовать для дальнейшего экспорта, иначе
        //                     будет перезаписывать старую сессию при импорте в Практик Актив, возможны глюки)
        //             true  = оставить старый session_id (использовать для сохранения текущей сессии)
        {
            // проверяем корректность сессии
            _ = pSession ?? throw new ArgumentNullException("Ссылка на сессию не может быть пустой");

            // очищаем каталог для сессии, если он существует
            // создаем каталог для сессии, если он не существует
            try
            {
                if (Directory.Exists(pPath))
                    Directory.Delete(pPath, true);
                Directory.CreateDirectory(pPath);
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Не удалось очистить каталог назначения", pPath, ex);
            }

            // если требуется создать копию сессии, генерим ей новый ID
            string new_session_id = keepOldId ? "" : Guid.NewGuid().ToString();
            int badSavedTracks = 0;

            // пробегаемся по всем трекам сессии
            for (int iTrack = 0; iTrack < pSession.Tracks.Count; iTrack++)
            {
                CPMapTrack track;
                // нужно создать копию сессии
                if (!keepOldId)
                {
                    // создаем копию трека, чтобы не повредить данные
                    track = (CPMapTrack) pSession.Tracks[iTrack].Clone();

                    // обновляем ID
                    if (iTrack == 0)
                    {
                        // трек - корневой, то модифицируем ему имя
                        track.Name += "_1";
                        track.ParentSessionId = null;
                        track.SessionId = new_session_id;
                    }
                    else
                    {
                        // трек - не корневой
                        track.ParentSessionId = new_session_id;
                        track.SessionId = Guid.NewGuid().ToString();
                    }
                }
                else
                    // обновляется существующая сессия, берем данные трека
                    track = pSession.Tracks[iTrack];

                // сериализуем трек и пишем в файл
                string jsonString = JsonConvert.SerializeObject(track);
                StreamWriter ptsFile;

                try
                {
                    ptsFile = new StreamWriter(pPath + Path.DirectorySeparatorChar + pSession.Tracks[iTrack].PTS_fileName, true);
                }
                catch (Exception ex)
                {
                    badSavedTracks++;
                    Log.Error($"Сохранение трека '{track}' - не удалось создать файл '{pPath + Path.DirectorySeparatorChar + pSession.Tracks[iTrack].PTS_fileName}'");
                    Log.Debug($"{ex}");
                    continue;
                }

                try
                {
                    ptsFile.Write(jsonString);
                }
                catch (Exception ex)
                {
                    badSavedTracks++;
                    Log.Error($"Сохранение трека '{track}' - не удалось записать файл '{pPath + Path.DirectorySeparatorChar + pSession.Tracks[iTrack].PTS_fileName}'");
                    Log.Debug($"{ex}");
                    continue;
                }
                finally
                {
                    ptsFile.Close();
                }
            }

            if (badSavedTracks > 0)
                throw new PmException_DataIsPartiallyIncorrect($"Cохранено {pSession.Tracks.Count - badSavedTracks} из {pSession.Tracks.Count} треков", pPath);

            // обновили существующую сессию из рабочего проекта, сбрасываем ей флаг наличия изменений
            if (keepOldId)
                pSession.IsChanged = false;
        }

        /// <summary>
        /// Сохраняет место проекта в файл формата PLC
        /// </summary>
        /// <param name="pPath">Путь к файлу назначения</param>
        /// <param name="pPlace">Ссылка на место проекта</param>
        /// <param name="keepOldId">Сохранять старый идентификатор места?</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        public void SavePlaceToPLC(string pPath, CPMapPlace pPlace, bool keepOldId)
        {

            _ = pPlace ?? throw new ArgumentNullException("Пустая ссылка на место");

            string json_str;
            StreamWriter plc_file;

            // создаем объект места для записи в файл
            CPMapPlaces out_Place = new() { Places = new() { pPlace } };

            // если нужен новый Id, создаем его
            if (!keepOldId)
            {
                out_Place.Places[0].PlaceId = Guid.NewGuid().ToString();
            }

            // сериализуем место и пишем в файл
            json_str = JsonConvert.SerializeObject(out_Place);
            try
            {
                plc_file = new StreamWriter(pPlace.PLC_filename, false);
                plc_file.Write(json_str);
                plc_file.Close();
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Не удалось сохранить место", pPath, ex);
            }

            // если заданный путь к файлу места совпал с указанным в месте, то сбрасываем признак наличия изменений
            if (pPath.ToLower() == pPlace.PLC_filename.ToLower())
                pPlace.WasSaved();
        }

        /// <summary>
        /// Сохраняет сессию в файл в экспортном формате "Практик 7" TAR
        /// </summary>
        /// <param name="pPath">Путь к сохраняемому файлу</param>
        /// <param name="pSession">Ссылка на сессию</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PmException_ExternalUtilityError"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        /// <exception cref="PmException_DataIsPartiallyIncorrect"></exception>
        public void SaveSessionToTar(string pPath, CPMapSession pSession)
        {

            _ = pSession ?? throw new ArgumentNullException("Ссылка на сессию не может быть пустой");
            
            string arc = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}7z.exe";
            if (!File.Exists(arc))
                throw new PmException_ExternalUtilityError("Не найден архиватор 7z.exe");

            string srcSessionPath = pSession.DataDirectory;
            string tmpSessionPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + Resources.TEMP_SUBDIR + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            try
            {
                // сохраняем сессию во временную папку
                SaveSessionToDirPTS(tmpSessionPath, pSession, false);
            }
            catch (Exception ex)
            {
                throw new PmException_DataSourceAccessError("Ошибка сохранения промежуточных данных", tmpSessionPath, ex);
            }

            bool BNSCAP_savedOK = true;
            // копируем файлы эхограмм в ту же папку
            foreach (CPMapTrack track in pSession.Tracks)
            {
                string srcBNSCAP = srcSessionPath + Path.DirectorySeparatorChar + track.BNSCAP_filename;
                string destBNSCAP = tmpSessionPath + Path.DirectorySeparatorChar + track.BNSCAP_filename;

                try
                {
                    if (File.Exists(srcBNSCAP))
                        File.Copy(srcBNSCAP, destBNSCAP);
                }
                catch (Exception ex)
                {
                    BNSCAP_savedOK = false;
                    Log.Error($"Не удалось скопировать файл эхограммы '{srcBNSCAP}' в '{destBNSCAP}'");
                    Log.Debug($"{ex}");
                    continue;
                }
            }
            
            // a -ttar -scsUTF-8 "<ARC_NAME>" "<DIR\*.pts>" "<DIR\*.bnscap>";
            string arg = $"a -ttar -scsUTF-8 \"{pPath}\" \"{tmpSessionPath}{Path.DirectorySeparatorChar}*.pts\" \"{tmpSessionPath}{Path.DirectorySeparatorChar}*.bnscap\"";

            Process proc = new ();
            proc.StartInfo.FileName = arc;
            proc.StartInfo.Arguments = arg;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            try
            {
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                throw new PmException_ExternalUtilityError("Ошибка при работе архиватора", ex);
            }

            if (!BNSCAP_savedOK)
                throw new PmException_DataIsPartiallyIncorrect("Файлы эхограмм не скопированы", pPath);
        }

    }


    /// <summary>
    /// Класс картографического объекта "Сессия"
    /// </summary>
    public class CPMapSession : CPMapObject
    {
        /// <summary>
        /// Есть несохраненные изменения?
        /// </summary>
        public bool IsChanged;
        /// <summary>
        /// Каталог на диске с данными сессмм
        /// </summary>
        public string DataDirectory;
        /// <summary>
        /// Список треков
        /// </summary>
        public List<CPMapTrack> Tracks;
        /// <summary>
        /// Ссылка на корневой трек
        /// </summary>
        public CPMapTrack RootTrackRef
        {
            get
            {
                if (Tracks.Count == 0)
                {
                    return null;
                }

                return Tracks[0];
            }
        }
        /// <summary>
        /// Название сессии
        /// </summary>
        public override string Name
        {
            get
            {
                if (RootTrackRef == null)
                {
                    //exception
                }
                return RootTrackRef.Name;
            }
            set
            {
                if (RootTrackRef == null)
                {
                    //exception
                }
                RootTrackRef.Name = value;
            }

        }

        public override double Latitude
        {
            get => Tracks[0].Points[0].Latitude;

            set { }
        }
        public override double Longitude
        {
            get => Tracks[0].Points[0].Longitude;

            set { }
        }

        //
        // сессия создается без корневого трека
        public CPMapSession(string dir)
        {
            DataDirectory = dir;
            IsChanged = true;
            Tracks = new List<CPMapTrack>();
        }

        // сессия создается с корневым треком
        public CPMapSession(string dir, string name)
        {
            DataDirectory = dir;
            IsChanged = true;
            Tracks = new List<CPMapTrack>();

            new CPMapTrack(this, name);
        }

        /// <summary>
        /// Корректирует глубины всех точек сессии
        /// </summary>
        /// <param name="depth">величина коррекции</param>
        /// <param name="soft_correction">true: изменяется величина коррекции, false: изменяются глубины точек</param>
        /// <returns>false, если хоть одна точка оказалась с отрицательной глубиной</returns>
        public bool ApplyCorrection(double depth, bool soft_correction)
        {
            bool result = true;

            if (Tracks.Count > 0)
                foreach (CPMapTrack track in Tracks)
                {
                    if (!track.ApplyCorrection(depth, soft_correction))
                        result = false;
                }

            return result;
        }

        public List<CPMapPoint> FindDuplicatePoints()
        {

            List<CPMapPoint> points = new List<CPMapPoint>();

            foreach (CPMapTrack track in Tracks)
                points.AddRange(track.FindDuplicatePoints());

            return points;
        }
        public List<CPMapPoint> FindBadDepthPoints(double lowDepth, bool useLowDepth, double greatDepth, bool useGreatDepth)
        {

            List<CPMapPoint> points = new List<CPMapPoint>();

            foreach (CPMapTrack track in Tracks)
                points.AddRange(track.FindBadDepthPoints(lowDepth, useLowDepth, greatDepth, useGreatDepth));

            return points;
        }
        public List<CPMapPoint> FindClosePoints(double distance)
        {

            List<CPMapPoint> points = new List<CPMapPoint>();

            foreach (CPMapTrack track in Tracks)
                points.AddRange(track.FindClosePoints(distance));

            return points;
        }

        public override string ToString()
        {
            return $"{base.ToString()} '{Name}' '{DataDirectory}'";
        }
    }

    /// <summary>
    /// Класс-реализация логгера на базе Serilog (интерфейс IPMapLogger)
    /// </summary>
    public class CPMapLoggerSerilog : IPMapLogger
    {
        private readonly Serilog.Core.Logger _log;
        /// <summary>
        /// Инициализирует экземпляр класса логгера
        /// </summary>
        /// <param name="logFilename">null создает логгер-заглушку</param>
        public CPMapLoggerSerilog(string logFilename)
        {
            if (logFilename == null)
            {
                _log = null;
            } else
            {
                _log = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(logFilename)
                    .CreateLogger();
            }
        }

        public bool DebugMode { get; set; }
        public void Debug(string message)
        {
            if (DebugMode)
                _log?.Debug(message);
        }
        public void Information(string message)
        {
            _log?.Information(message);
        }
        public void Warning(string message)
        {
            _log?.Warning(message);
        }
        public void Error(string message)
        {
            _log?.Error(message);
        }
        public void Fatal(string message)
        {
            _log?.Fatal(message);
        }
    }

    public class CPMapUISettings
    {
        public Color selectionColor, placeColor, pointMinDepthColor, pointMaxDepthColor, pointBadLowDepthColor, pointBadGreatDepthColor;
        public double pointMinDepth, pointMaxDepth, pointBadLowDepth, pointBadGreatDepth;
        public int placeSize, pointSize, selectionWidth;
        public bool showBadLowDepth, showBadGreatDepth;
    }

}
