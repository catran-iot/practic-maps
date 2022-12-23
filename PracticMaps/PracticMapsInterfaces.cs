using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PracticMaps
{
    /// <summary>
    /// Интерфейс логгера
    /// </summary>
    public interface IPMapLogger
    {
        bool DebugMode { get; set; }
        void Debug(string message);
        void Information(string message);
        void Error(string message);
        void Warning(string message);
        void Fatal(string message);
    }

    /// <summary>
         /// Интерфейс графического пользовательского интерфейса. Подразумевает отображение данных на карте, в дереве точек и дереве мест
         /// </summary>
    public interface IPMapUI
    {
        void MapDrawMarkers(EPMapObjectType type);
        void MapMoveToActiveObject();
        CPMapObject PlacesTreeGetSelectedObject();
        void PlacesTreeRefreshNodes();
        CPMapObject PointsTreeGetCheckedObject(object e);
        CPMapObject PointsTreeGetSelectedObject();
        void PointsTreeRefreshNodeCheck();
        void PointsTreeRefreshNodes();
        void DefaultSettings();
        void LoadSettings(string cfgFilename);
        void SaveSettings();
        void SaveSettings(string cfgFilename);

        CPMapProject ParentProject { get; set; }

        CPMapUISettings Settings { get; }
        bool DontRefreshTreeView { get; }
    }

}