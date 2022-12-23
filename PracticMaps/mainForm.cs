using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using PracticMapsProject.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace PracticMapsProject
{
    public partial class main_Form : Form
    {
        private readonly pointEdit_Form pointEditForm;
        private readonly moveSelection_Form moveSelectionForm;
        private readonly inputbox_Form inputboxForm;
        private readonly placeEdit_Form placeEditForm;
        private readonly about_Form aboutForm;
        private readonly settings_Form settingsForm;

        /// <summary>
        /// Инициализация экземпляра класса главной формы
        /// </summary>
        public main_Form()
        {
            // встроенный системный метод
            InitializeComponent();

            // подготовка данных
            string logFilename = Environment.CurrentDirectory + Path.DirectorySeparatorChar + 
                                 Resources.LOG_SUBDIR + Path.DirectorySeparatorChar + 
                                 DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".log";



            // ЛОГГЕР
            // пытаемся инициализировать экземпляр класса логгера

            CPMapLoggerSerilog Log;
            try
            {
                Log = new (logFilename);
            }
            // логгер не создался - логирование невозможно, будет создан логгер-заглушка
            catch
            {
                MessageBox.Show("Ошибка инициализации журнала.\nЛогирование не производится.");
                Log = new (null);
            }

            // ГРАФИЧЕСКИЙ ИНТЕРФЕЙС
            // пытаемся инициализировать экземпляр класса GUI
            CPMapUIWin UI;
            try
            {
                UI = new (gmapMain, treePoints, treePlaces, Log);
            }
            // GUI не проинициализирован, приложение будет закрыто
            catch (Exception ex)
            {
                Log?.Fatal($"Ошибка инициализации графического интерфейса приложения ({ex.Message}) - запуск приложения невозможен.");
                Log?.Debug($"{ex}");

                MessageBox.Show("Ошибка инициализации графического интерфейса приложения.\nЗапуск приложения невозможен.");
                Close();
                return;
            }

            // ПРОЕКТ
            // пытаемся инициализировать экземпляр данных проекта
            try
            {
                pmProject = new (UI, Log);
            }
            // ошибка при доступе к данным проекта на диске, приложение будет закрыто
            catch (PmException_DataSourceAccessError ex)
            {
                Log?.Fatal($"Ошибка при доступе к данным проекта '{ex.Path}' ({ex.Message}) - запуск приложения невозможен.");
                Log?.Debug($"{ex}");
                
                MessageBox.Show("Ошибка при доступе к данным проекта!\nЗапуск приложения невозможен.");
                Close();
                return;
            }
            // проект не проинициализирован, приложение будет закрыто
            catch (Exception ex)
            {
                Log?.Fatal($"Ошибка инициализации данных проекта ({ex.Message}) - запуск приложения невозможен");
                Log?.Debug($"{ex}");

                MessageBox.Show("Ошибка инициализации данных проекта\nЗапуск приложения невозможен");
                Close();
                return;
            }

            // обрабатываем некритичные события
            if (pmProject.LastMessage != "")
                MessageBox.Show(pmProject.LastMessage);

            // создаем вспомогательные формы
            settingsForm = new settings_Form(pmProject.UI.Settings);
            pointEditForm = new pointEdit_Form();
            moveSelectionForm = new moveSelection_Form();
            inputboxForm = new inputbox_Form();
            placeEditForm = new placeEdit_Form();
            aboutForm = new about_Form();

            // отображаем данные проекта в UI
            pmProject.UI.MapDrawMarkers(EPMapObjectType.Session);
            pmProject.UI.PointsTreeRefreshNodes();
            pmProject.UI.PlacesTreeRefreshNodes();

            // инициализация компонента карты
            gmapMain.ShowCenter = false;
            gmapMain.DragButton = MouseButtons.Right;
            gmapMain.MapProvider = GMapProviders.GoogleHybridMap;

            // отображение прочих данных в UI
            PM_SetInterfaceElements();

        }

        /// <summary>
        /// Обработчик - карта, клик мышью по маркеру объекта
        /// </summary>
        /// <param name="item"></param>
        /// <param name="e"></param>
        private void gmapMain_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            CPMapMarker marker = item as CPMapMarker;

            switch(e.Button)
            { 
                case MouseButtons.Left:
                    // клик левой кнопки мыши
                    switch (marker.PM_MapObjectRef)
                    {
                        // по точке - инвертируем отметку точки
                        case CPMapPoint point:
                            pmProject.Data.State.SetSelectionToObject(point, !pmProject.Data.State.SelectionPointIsSelected(point));
                            pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
                            pmProject.UI.PointsTreeRefreshNodeCheck();
                            break;

                        // по месту - заглушка
                        case CPMapPlace place:
                            MessageBox.Show($"Место '{place.Name}'");
                            break;

                        default:
                            // по умолчанию - игнорируем
                            break;
                    }
                    break;

                case MouseButtons.Right:
                    // клик правой кнопки мыши
                    if (pmProject.Data.State.SetActiveObject(marker.PM_MapObjectRef))
                    // выделенный объект успешно установлен активным, отображаем контекстное меню
                    {
                        contextMenuStrip_marker.Show(MousePosition);
                    }
                    // выделенный объект отброшен - предыдущее действие не завершено
                    else
                    {
                        MessageBox.Show("Закончите активное действие", "Внимание");
                    }
                    break;

                default:
                    // нажатие других кнопок мыши игнорируем
                    break;
            }
        }

        /// <summary>
        /// Обработчик - дерево точек, отметка/снятие отметки узла дерева
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treePoints_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!pmProject.UI.DontRefreshTreeView)
            {
                CPMapObject obj;

                try
                {
                    obj = pmProject.UI.PointsTreeGetCheckedObject(e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при отметке элемента дерева точек", "Внутренняя ошибка");
                    pmProject.Log.Error($"Внутренняя ошибка при отметке элемента дерева точек {ex.Message}");
                    pmProject.Log.Debug($"{ex}");
                    return;
                }

                pmProject.Data.State.SetSelectionToObject(obj, e.Node.Checked);
                pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
                pmProject.UI.PointsTreeRefreshNodeCheck();
            }
        }

        /// <summary>
        /// Обработчик - карта, установка подложки Google
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Map_SetGoogleHibryd_Click(object sender, EventArgs e)
        {
            gmapMain.MapProvider = GMapProviders.GoogleHybridMap;
            gmapMain.MaxZoom = 20;
        }

        /// <summary>
        /// Обработчик - карта, установка подложки Yandex
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Map_SetYandex_Click(object sender, EventArgs e)
        {
            gmapMain.MapProvider = GMapProviders.YandexHybridMap;
            gmapMain.MaxZoom = 17;
        }

        /// <summary>
        /// Обработчик - карта, установка подложки OpenStreetMap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Map_SetOSM_Click(object sender, EventArgs e)
        {
            gmapMain.MapProvider = GMapProviders.OpenStreetMap;
            gmapMain.MaxZoom = 20;
        }

        /// <summary>
        /// Обработчик - главное меню, снятие выделения объектов карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Map_ClearSelection_Click(object sender, EventArgs e)
        {
            pmProject.Data.State.SelectionClear();
            pmProject.UI.PointsTreeRefreshNodeCheck();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, переход к активному объекту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_GotoPoint_Click(object sender, EventArgs e)
        {
            pmProject.UI.MapMoveToActiveObject();
        }

        /// <summary>
        /// Обработчик - главное меню, удаление текущего проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_File_ClearProjectDir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Проект и cодержимое папки\r\n" + pmProject.Data.ProjectDirectory + "\r\nбудут полностью удалены!\r\n\r\nПродолжить?", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PM_ClearProjectDirectory();
                PM_LoadProject();

                pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
                pmProject.UI.PointsTreeRefreshNodes();
                pmProject.UI.PlacesTreeRefreshNodes();
                
            }
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, удаление выделенной точки/трека/сессии 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_DeleteSelectedObject_Click(object sender, EventArgs e)
        {
            PM_DeleteActiveObject();
        }

        /// <summary>
        /// Обработчик - главное меню, файл, перезагрузка данных проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_File_ReloadProject_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Проект будет загружен заново из каталога\n" + pmProject.Data.ProjectDirectory.ToUpper() + "\nНесохраненные изменения будут утеряны!\r\n\r\nПродолжить?", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PM_LoadProject();

                pmProject.UI.MapDrawMarkers(EPMapObjectType.Session);
                pmProject.UI.PointsTreeRefreshNodes();
                pmProject.UI.PlacesTreeRefreshNodes();

            }
        }
        
        /// <summary>
        /// Обработчик - главное меню, файл, выход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_File_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик - главное меню, файл, импорт сессии из TAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_File_ImportTar_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = pmProject.Data.ProjectDirectory;
            openFileDialog1.DefaultExt = "tar";
            openFileDialog1.Filter = "Файлы экспорта Практик (*.tar)|*.tar";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pmProject.Data.LoadFromFile_TAR(openFileDialog1.FileName);
                }
                catch (PmException_DataIsPartiallyIncorrect ex)
                {
                    MessageBox.Show($"Данные в файле частично некорректны ({ex.Message})");
                    pmProject.Log.Warning($"Данные в файле частично некорректны '{ex.Path}' ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                }
                catch (PmException_DataSourceAccessError ex)
                {
                    MessageBox.Show($"Ошибка доступа к файлу ({ex.Message})");
                    pmProject.Log.Error($"Ошибка доступа к файлу '{ex.Path}' ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                }
                catch (PmException_DataSourceIsEmpty ex)
                {
                    MessageBox.Show($"В файле отсутствуют корректные данные ({ex.Message})");
                    pmProject.Log.Warning($"В файле отсутствуют корректные данные '{ex.Path}' ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                }
                catch (PmException_DataSourceIsNotExists ex)
                {
                    MessageBox.Show($"Файл не найден ({ex.Message})");
                    pmProject.Log.Error($"Файл не найден '{ex.Path}' ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                }
                catch (PmException_ExternalUtilityError ex)
                {
                    MessageBox.Show($"Ошибка обработки файла ({ex.Message})");
                    pmProject.Log.Error($"Ошибка обработки файла '{openFileDialog1.FileName}' ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Неизвестная ошибка ({ex.Message})");
                    pmProject.Log.Error($"Неизвестная ошибка при открытии файла '{openFileDialog1.FileName}' ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                }

                pmProject.UI.MapDrawMarkers(EPMapObjectType.Session);
                pmProject.UI.PointsTreeRefreshNodes();
                
            }
        }

        /// <summary>
        /// Обработчик - главное меню, Справка, "О программе"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Help_About_Click(object sender, EventArgs e)
        {
            aboutForm.ShowDialog();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, экспорт сессии в TAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_ExportSessionToTAR_Click(object sender, EventArgs e)
        {
            CPMapObject mapObject = pmProject.Data.State.MapObject;

            switch (mapObject)
            {
                case CPMapSession:
                case CPMapTrack:
                case CPMapPoint:

                    saveFileDialog1.DefaultExt = "tar";
                    saveFileDialog1.Filter = "Файлы экспорта Практик (*.tar)|*.tar";
                    saveFileDialog1.InitialDirectory = pmProject.Data.ProjectDirectory;
                    saveFileDialog1.FileName = pmProject.Data.State.Session.Name;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        PM_SaveSessionToTar(saveFileDialog1.FileName, pmProject.Data.State.Session);
                    }

                    break;

                default:
                    MessageBox.Show("Данный объект нельзя сохранить в формате TAR", "Внутренняя ошибка");
                    pmProject.Log.Warning("Попытка сохранить в TAR неподдерживаемый объект");
                    pmProject.Log.Debug($"ContextMenu_PointsTree_ExportSessionToTAR_Click '{mapObject}'");
                    break;
            }
        }

        /// <summary>
        /// Обработчик - дерево точек, клик мышью по элементу дерева
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treePoints_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treePoints.SelectedNode = e.Node;
                CPMapObject obj = pmProject.UI.PointsTreeGetSelectedObject();
                
                if (obj == null)
                // не удалось получить ссылку на выделенный объект
                {
                    MessageBox.Show("Нет выделенного объекта", "Внутренняя ошибка");
                    pmProject.Log.Debug("treeSessions_NodeMouseClick - нет выделенного объекта");
                    return;
                }

                if (pmProject.Data.State.SetActiveObject(obj))
                // выделенный объект установлен как активный 
                {
                    contextMenuStrip_points.Show(MousePosition);
                }
                else
                // выделенный объект отброшен - предыдущее действие не завершено
                {
                    MessageBox.Show("Закончите активное действие", "Внимание");
                }
            }
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, сохранение изменений в выбранной сессии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_SaveSession_Click(object sender, EventArgs e)
        {
            if (pmProject.Data.State.Session == null)
            {
                MessageBox.Show("Сессия не найдена", "Внутренняя ошибка");
                pmProject.Log.Error("Попытка сохранить сессию из контекстного меню дерева точек, но сессию найти не удалось");
                pmProject.Log.Debug("ContextMenu_PointsTree_SaveSession_Click");
                return;
            }

            // объект карты выделен
            // выделены точка, трек или сессия
            string session_name = pmProject.Data.State.Session.Name;
            string session_dir = pmProject.Data.State.Session.DataDirectory;

            if (MessageBox.Show($"Сохранить изменения в сессии '{session_name}'?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PM_SaveSessionToDirPTS(session_dir, pmProject.Data.State.Session, true);
                pmProject.UI.PointsTreeRefreshNodes();
            }
        }

        /// <summary>
        /// Обработчик - главное меню, загрузка сессии из файлов PTS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_File_LoadDirPTS_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + pmProject.Data.ProjectDirectory;
            folderBrowserDialog1.ShowNewFolderButton = false;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pmProject.Data.LoadSessionFromDir_PTS(folderBrowserDialog1.SelectedPath);
                }
                catch (PmException_DataSourceIsEmpty ex)
                {
                    MessageBox.Show($"Корректные данные отсутствуют");
                    pmProject.Log.Error($"В каталоге '{ex.Path}' отсутствуют корректные данные ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                    return;
                }
                catch (PmException_DataSourceAccessError ex)
                {
                    MessageBox.Show($"Ошибка чтения данных");
                    pmProject.Log.Error($"Ошибка чтения данных из каталога '{ex.Path}' '({ex.Message})'");
                    pmProject.Log.Debug($"{ex}");
                    return;
                }
                catch (PmException_DataIsPartiallyIncorrect ex)
                {
                    MessageBox.Show("Данные загружены частично");
                    pmProject.Log.Warning($"Данные из каталога '{ex.Path}' загружены частично ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Неизвестная ошибка при загрузке данных.");
                    pmProject.Log.Error($"Неизвестная ошибка: {ex.Message}");
                    pmProject.Log.Debug($"{ex}");
                    return;
                }
                
                pmProject.UI.MapDrawMarkers(EPMapObjectType.Session);
                pmProject.UI.PointsTreeRefreshNodes();

            }
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, сохранение сессии в набор файлов PTS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_SaveSessionToDirPTS_Click(object sender, EventArgs e)
        {
            if (pmProject.Data.State.Session == null)
            {
                MessageBox.Show("Сессия не найдена", "Внутренняя ошибка");
                pmProject.Log.Error("Попытка сохранить сессию в файлы PTS из контекстного меню дерева точек, но сессию найти не удалось");
                pmProject.Log.Debug("ContextMenu_PointsTree_SaveSessionToDirPTS_Click");
                return;
            }

            folderBrowserDialog1.SelectedPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + pmProject.Data.ProjectDirectory;
            folderBrowserDialog1.ShowNewFolderButton = true;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
                string[] dirs = Directory.GetDirectories(folderBrowserDialog1.SelectedPath);

                if ((files.Length > 0)||(dirs.Length > 0))
                {
                    if (MessageBox.Show("Выбранная папка не пуста, ее содержимое будет очищено!\nПродолжить?", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }

                PM_SaveSessionToDirPTS(folderBrowserDialog1.SelectedPath, pmProject.Data.State.Session, false);
            }
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, экспорт сессию в файл CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_ExportSessionToCSV_Click(object sender, EventArgs e)
        {
            if (pmProject.Data.State.Session == null)
            {
                MessageBox.Show("Сессия не найдена", "Внутренняя ошибка");
                pmProject.Log.Error("Попытка сохранить сессию в файл CSV из контекстного меню дерева точек, но сессию найти не удалось");
                pmProject.Log.Debug("ContextMenu_PointsTree_ExportSessionToCSV_Click");
                return;
            }

            saveFileDialog1.DefaultExt = "csv";
                saveFileDialog1.Filter = "Файлы обмена CSV (*.csv)|*.csv";
                saveFileDialog1.InitialDirectory = pmProject.Data.ProjectDirectory;
                saveFileDialog1.FileName = pmProject.Data.State.Session.Name;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PM_SaveSessionToCSV(saveFileDialog1.FileName, pmProject.Data.State.Session, null);
                }

        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, копирование отмеченных точек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_MoveSelection_Click(object sender, EventArgs e)
        {
            moveSelectionForm.PM_Names = pmProject.Data.Names;
            moveSelectionForm.ShowDialog();

            if ((moveSelectionForm.SelectedSession != moveSelection_Form.NothingSelected) && (moveSelectionForm.SelectedTrack != moveSelection_Form.NothingSelected))
            {

                CPMapSession session = pmProject.Data.Sessions[moveSelectionForm.SelectedSession];
                CPMapTrack track;
                if (session == null)
                    track = null;
                else
                    if (moveSelectionForm.SelectedTrack == moveSelection_Form.NewSelected)
                    track = null;
                else
                    track = session.Tracks[moveSelectionForm.SelectedTrack];


                pmProject.Data.SelectionCopyTo(session, track);
                pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
                pmProject.UI.PointsTreeRefreshNodes();
            }
        }

        /// <summary>
        /// Обработчик - карта, изменение масштаба (зума)
        /// </summary>
        private void gmapMain_OnMapZoomChanged()
        {
            toolStripStatusLabel1.Text = "Zoom: " + gmapMain.Zoom.ToString();
        }

        /// <summary>
        /// Обработчик - контекстное меню маркера карты, редактирование выбранного объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuMarker_EditPoint_Click(object sender, EventArgs e)
        {
            PM_EditActiveObject();
        }

        /// <summary>
        /// Обработчик - контекстное меню маркера карты, активация действия "перемещение объекта"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_Marker_MovePoint_Click(object sender, EventArgs e)
        {
            switch (pmProject.Data.State.MapObject)
            {
                case CPMapPoint:
                case CPMapPlace:
                    break;

                default:
                    // выбран неподходящий для перемещения объект, игнорируем
                    MessageBox.Show("Объект не найден", "Внутренняя ошибка");
                    pmProject.Log.Error($"Попытка перемещения объекта, перемещение которого не подерживается '{pmProject.Data.State.MapObject}'");
                    pmProject.Log.Debug($"ContextMenu_Marker_MovePoint_Click");
                    return;
            }

            pmProject.Data.State.SetActiveAction(EPMapAction.MoveObject);
            PM_SetInterfaceElements();
        }

        /// <summary>
        /// Обработчик - контекстное меню маркера карты, удаляем объект.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuMarker_DeletePoint_Click(object sender, EventArgs e)
        {
            PM_DeleteActiveObject();
        }

        /// <summary>
        /// Обработчик - карта, клик мышью (реагирует только на правый)
        /// </summary>
        /// <param name="pointClick"></param>
        /// <param name="e"></param>
        private void gmapMain_OnMapClick(GMap.NET.PointLatLng pointClick, MouseEventArgs e)
        {
            CPMapObject mapObject = pmProject.Data.State.MapObject;

            // проверяем, какое действие выполняется
            switch (pmProject.Data.State.MapAction)
            {
                // выполняется перемещение объекта
                case EPMapAction.MoveObject:
                    string text;

                    // смотрим, какой объект перемещаем
                    switch (mapObject)
                    {
                        case CPMapPoint:
                            text = "Переместить точку?\n\nДа - переместить точку\nНет - перевыбрать\nОтмена - не перемещать точку";
                            break;
                        case CPMapPlace:
                            text = "Переместить место?\n\nДа - переместить место\nНет - перевыбрать\nОтмена - не перемещать место";
                            break;
                        default:
                            // ошибка - выбрано перемещение неперемещаемого объекта, сбрасываем действие выходим
                            MessageBox.Show("Сессия не найдена", "Внутренняя ошибка");
                            pmProject.Log.Error($"Попытка перемещения объекта, перемещение которого не подерживается '{mapObject}'");
                            pmProject.Log.Debug($"gmapMain_OnMapClick");
                            pmProject.Data.State.ActionCompleted();

                            return;
                    }

                    // перемещаем объект
                    switch (MessageBox.Show(text, "Подтверждение", MessageBoxButtons.YesNoCancel))
                    {
                        // перемещаем объект в указанное место
                        case DialogResult.Yes:
                            mapObject.Latitude = pointClick.Lat;
                            mapObject.Longitude = pointClick.Lng;
                            pmProject.Data.State.ActionCompleted();

                            if (pmProject.Data.State.Session != null)
                                pmProject.Data.State.Session.IsChanged = true;

                            if (pmProject.Data.State.MapObject is CPMapPlace)
                                pmProject.UI.PlacesTreeRefreshNodes();
                            else
                                pmProject.UI.PointsTreeRefreshNodes();

                            pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
                            break;

                        // отменяем перемещение 
                        case DialogResult.Cancel:

                            pmProject.Data.State.ActionCompleted();

                            break;

                        // выбираем новое место 
                            case DialogResult.No:
                                break;
                    }

                    PM_SetInterfaceElements();

                    break;

                // никакое не выполняется действие, игнорируем клик
                case EPMapAction.None:
                    break;

                // не нашлось обрабочика для активного действия - это ошибка
                default:
                    MessageBox.Show("Активное действие не завершено", "Внутренняя ошибка");
                    pmProject.Log.Error($"Внутренняя ошибка при обработке активного действия '{pmProject.Data.State.MapAction}'");
                    pmProject.Log.Debug($"gmapMain_OnMapClick");
                    break;
            }    
        }

        /// <summary>
        /// Обработчик - главная форма, установка заголовка окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_Form_Load(object sender, EventArgs e)
        {
            string name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            Text =  $"{name} {ver}";
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, экспорт трека в файл CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_ExportTrackToCSV_Click(object sender, EventArgs e)
        {
            if (pmProject.Data.State.Track == null)
            {
                MessageBox.Show("Выберите трек или точку трека.");
                return;
            }

            saveFileDialog1.DefaultExt = "csv";
            saveFileDialog1.Filter = "Файлы обмена CSV (*.csv)|*.csv";
            saveFileDialog1.InitialDirectory = pmProject.Data.ProjectDirectory;
            saveFileDialog1.FileName = pmProject.Data.State.Track.Name;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PM_SaveSessionToCSV(saveFileDialog1.FileName, pmProject.Data.State.Track.ParentSession, pmProject.Data.State.Track);
            }

        }

        /// <summary>
        /// Обработчик - главное меню, файл, импорт сессии из файла CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_File_ImportCSV_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = pmProject.Data.ProjectDirectory;
            openFileDialog1.DefaultExt = "csv";
            openFileDialog1.Filter = "Файлы обмена CSV (*.csv)|*.csv";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    pmProject.Data.LoadFromFile_CSV(openFileDialog1.FileName);
                }
                catch (PmException_DataSourceIsEmpty ex)
                {
                    MessageBox.Show("Файл не содержит точек глубин", "Предупреждение");
                    pmProject.Log.Warning($"Файл не содержит точек глубин '{openFileDialog1.FileName}'");
                    pmProject.Log.Debug($"{ex}");
                    return;
                }
                catch (PmException_DataSourceAccessError ex)
                {
                    MessageBox.Show($"Ошибка открытия файла ({ex.Message})", "Ошибка");
                    pmProject.Log.Error($"Ошибка открытия файла '{openFileDialog1.FileName} ({ex.Message})'");
                    pmProject.Log.Debug($"{ex}");
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Неожиданная ошибка при открытии файла ({ex.Message})", "Ошибка");
                    pmProject.Log.Error($"Неожиданная ошибка при открытии файла '{openFileDialog1.FileName} ({ex.Message})'");
                    pmProject.Log.Debug($"{ex}");
                    return;
                }

                pmProject.UI.MapDrawMarkers(EPMapObjectType.Session);
                pmProject.UI.PointsTreeRefreshNodes();
                tabControl1.SelectedIndex = 0;
            }

        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, перемещение точки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_MoveObject_Click(object sender, EventArgs e)
        {
            // выходим из обработчика - выбран объект, не являющийся точкой 
            if (pmProject.Data.State.MapObject is not CPMapPoint)
                return;

            // точка выбрана, устанавливаем действие "перемещение"
            if (!pmProject.Data.State.SetActiveAction(EPMapAction.MoveObject))
            {
                MessageBox.Show($"Предыдущее действие не завершено", "Внутренняя ошибка");
                pmProject.Log.Error($"Внутренняя ошибка - предыдущее действие не завершено");
                pmProject.Log.Debug("ContextMenu_PointsTree_MoveObject_Click");
                return;
            }

            PM_SetInterfaceElements();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, редактирование объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_EditObject_Click(object sender, EventArgs e)
        // 
        {
            PM_EditActiveObject();
        }

        /// <summary>
        /// Обработчик - главное меню, файл, загрузка места
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_File_LoadPlace_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = pmProject.Data.ProjectDirectory;
            openFileDialog1.DefaultExt = "plc";
            openFileDialog1.Filter = "Файлы мест Практик (*.plc)|*.plc";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    pmProject.Data.LoadFromFile_PLC(openFileDialog1.FileName, true);
                }
                catch (PmException_DataSourceAccessError ex)
                {
                    MessageBox.Show($"Ошибка доступа к файлу ({ex.Message})");
                    pmProject.Log.Error($"Ошибка доступа к файлу '{ex.Path}' ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                }
                catch (PmException_DataSourceIsEmpty ex)
                {
                    MessageBox.Show($"В файле отсутствуют корректные данные ({ex.Message})");
                    pmProject.Log.Warning($"В файле отсутствуют корректные данные '{ex.Path}' ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                }
                catch (PmException_DataSourceIsNotExists ex)
                {
                    MessageBox.Show($"Файл не найден ({ex.Message})");
                    pmProject.Log.Error($"Файл не найден '{ex.Path}' ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Неизвестная ошибка ({ex.Message})");
                    pmProject.Log.Error($"Неизвестная ошибка при открытии файла '{openFileDialog1.FileName}' ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                }

                pmProject.UI.PlacesTreeRefreshNodes();
                pmProject.UI.MapDrawMarkers(EPMapObjectType.Place);

            }
        }

        /// <summary>
        /// Обработчик - главное меню, карта, "перемещать правой кнопкой мыши"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Map_MoveByRMB_Click(object sender, EventArgs e)
        {
            MainMenu_Map_MoveRMBToolStripMenuItem.Checked = !MainMenu_Map_MoveRMBToolStripMenuItem.Checked;

            switch (MainMenu_Map_MoveRMBToolStripMenuItem.Checked)
            {
                case false:
                    gmapMain.DragButton = MouseButtons.Left;
                    break;

                case true:
                    gmapMain.DragButton = MouseButtons.Right;
                    break;
            }
        }

        /// <summary>
        /// Обработчик - дерево мест, клик мышью на элементе дерева
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treePlaces_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treePlaces.SelectedNode = e.Node;

                CPMapObject obj = pmProject.UI.PlacesTreeGetSelectedObject();

                if (obj == null)
                // не удалось получить ссылку на выделенный объект
                {
                    MessageBox.Show("Нет выделенного объекта", "Внутренняя ошибка");
                    pmProject.Log.Error("Нет выделенного объекта");
                    pmProject.Log.Debug("treePlaces_NodeMouseClick");
                    return;
                }

                if (pmProject.Data.State.SetActiveObject(obj))
                // выделенный объект успешно установлен как активный
                {
                    contextMenuStrip_places.Show(MousePosition);
                }
                else
                // выделенный объект отброшен - есть незавершенное действие
                {
                    MessageBox.Show("Закончите предыдущее действие", "Внимание");
                }
            }
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева мест, перемещение места
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PlacesTree_MovePlace_Click(object sender, EventArgs e)
        {
            if (pmProject.Data.State.MapObject is not CPMapPlace)
            {
                MessageBox.Show("Выбранный объект - не 'место'", "Внутренняя ошибка");
                pmProject.Log.Error($"Попытка переместить место, но выбран объект типа '{pmProject.Data.State.MapObject}'");
                pmProject.Log.Debug($"ContextMenu_PlacesTree_MovePlace_Click");
                return;
            }

            pmProject.Data.State.SetActiveAction(EPMapAction.MoveObject);
            pmProject.UI.PlacesTreeRefreshNodes();
            PM_SetInterfaceElements();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева мест, центрирует карту по выбранному месту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PlacesTree_GoToPlace_Click(object sender, EventArgs e)
        {
            if (pmProject.Data.State.MapObject is not CPMapPlace)
            {
                MessageBox.Show("Выбранный объект - не 'место'", "Внутренняя ошибка");
                pmProject.Log.Error($"Попытка перейти к месту, но выбран объект типа '{pmProject.Data.State.MapObject}'");
                pmProject.Log.Debug($"ContextMenu_PlacesTree_GoToPlace_Click");
                return;
            }

            pmProject.UI.MapMoveToActiveObject();
            pmProject.Data.State.ActionCompleted();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева мест, удаление места
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PlacesTree_DeletePlace_Click(object sender, EventArgs e)
        {
            PM_DeleteActiveObject();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева мест, сохранение изменений в месте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PlacesTree_SavePlace_Click(object sender, EventArgs e)
        {
            if (pmProject.Data.State.MapObject is not CPMapPlace)
            {
                MessageBox.Show("Выбранный объект - не 'место'", "Внутренняя ошибка");
                pmProject.Log.Error($"Попытка сохранить изменения места на диск, но выбран объект типа '{pmProject.Data.State.MapObject}'");
                pmProject.Log.Debug($"ContextMenu_PlacesTree_SavePlace_Click");
                return;
            }

            if (MessageBox.Show($"Сохранить изменения в месте '{pmProject.Data.State.MapObject.Name}'?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PM_SavePlaceToPLC(((CPMapPlace)pmProject.Data.State.MapObject).PLC_filename, (CPMapPlace)pmProject.Data.State.MapObject, true);

                pmProject.Data.State.ActionCompleted();
                pmProject.UI.PlacesTreeRefreshNodes();
            }
        }

        /// <summary>
        /// Обработчик - дерево мест, экспорт места в файл PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PlacesTree_ExportPlace_Click(object sender, EventArgs e)
        {

            if (pmProject.Data.State.MapObject is not CPMapPlace)
            {
                MessageBox.Show("Выбранный объект - не 'место'", "Внутренняя ошибка");
                pmProject.Log.Error($"Попытка экспортировать место в файл PLC, но выбран объект типа '{pmProject.Data.State.MapObject}'");
                pmProject.Log.Debug($"ContextMenu_PlacesTree_EditPlace_Click");
                return;
            }

            saveFileDialog1.DefaultExt = "plc";
            saveFileDialog1.Filter = "Файл места Практик (*.plc)|*.plc";

            saveFileDialog1.InitialDirectory = pmProject.Data.ProjectDirectory;
            saveFileDialog1.FileName = Path.GetFileName(((CPMapPlace)pmProject.Data.State.MapObject).PLC_filename);

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PM_SavePlaceToPLC(saveFileDialog1.FileName, (CPMapPlace)pmProject.Data.State.MapObject, false);

                pmProject.Data.State.ActionCompleted();
                pmProject.UI.PlacesTreeRefreshNodes();
            }
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, коррекция глубин трека
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_CorrectionTrackDepth_Click(object sender, EventArgs e)
        {
            CPMapTrack track = pmProject.Data.State.Track;

            if (track == null)
            {
                MessageBox.Show("Нужно выбрать трек или точку трека");
                return;
            }

            bool correction = false;

            DialogResult action = MessageBox.Show
                ("Внести поправку глубины?\n" +
                "Да - установится величина коррекции глубины трека\n" +
                "Нет - пересчитаются глубины всех точек трека\n" +
                "Отмена - не корректировать глубину\n",
                "Внимание",
                MessageBoxButtons.YesNoCancel);

            switch (action)
            {
                case DialogResult.Yes:
                    correction = true;
                    break;
                case DialogResult.No:
                    correction = false;
                    break;
                case DialogResult.Cancel:
                    return;
            }

            inputboxForm.QueryText = "Величина корреции (м):";
            inputboxForm.FormCaptionText = "Трек '" + track.Name + "'";
            inputboxForm.ValueText = track.CorrectionValue.ToString();
            inputboxForm.ValueType = 0.0.GetType();
            inputboxForm.ShowDialog();

            if (inputboxForm.TextValueChanged)
            {
                // не проверяем результат, было проверено в форме
                double.TryParse(inputboxForm.ValueText, out double result);

                if (!track.ApplyCorrection(result, correction))
                    MessageBox.Show("Появились точки с отрицательной глубиной!");

                pmProject.UI.PointsTreeRefreshNodes();
                pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
            }
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, коррекция глубин сессии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_CorrectionSessionDepth_Click(object sender, EventArgs e)
        {
            {
                bool correction = false;

                DialogResult action = MessageBox.Show
                    ("Внести поправку глубины?\n" +
                    "Да - установится величина коррекции глубины на все треки сессии\n" +
                    "Нет - пересчитаются глубины точек всех треков сессии" +
                    "Отмена - не корректировать глубину\n",
                    "Внимание", 
                    MessageBoxButtons.YesNoCancel);
                
                switch (action)
                {
                    case DialogResult.Yes:
                        correction = true;
                        break;
                    case DialogResult.No:
                        correction = false;
                        break;
                    case DialogResult.Cancel:
                        return;
                }

                CPMapSession session = pmProject.Data.State.Session;
                if (session == null)
                    //exception
                    return;

                inputboxForm.QueryText = "Величина корреции (м):";
                inputboxForm.FormCaptionText = "Сессия '" + session.Name + "'";
                inputboxForm.ValueText = "0";
                inputboxForm.ValueType = 0.0.GetType();
                inputboxForm.ShowDialog();

                if (inputboxForm.TextValueChanged)
                {
                    // не проверяем результат, было проверено в форме
                    double.TryParse(inputboxForm.ValueText, out double result);

                    if (!session.ApplyCorrection(result, correction))
                        MessageBox.Show("Появились точки с отрицательной глубиной!");

                    pmProject.UI.PointsTreeRefreshNodes();
                    pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
                }
            }

        }

        // Сводка:
        // контекстное меню дерева мест
        // редактируем место
        private void ContextMenu_PlacesTree_EditPlace_Click(object sender, EventArgs e)
        {
            PM_EditActiveObject();
        }

        /// <summary>
        /// Обработчик - главное меню, карта, настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Map_Settings_Click(object sender, EventArgs e)
        {
            settingsForm.ShowDialog();
            if (settingsForm.IsChanged)
            {
                pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
                pmProject.UI.SaveSettings();
            }
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, снять выделение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_SelectionClear_Click(object sender, EventArgs e)
        {
            pmProject.Data.State.SelectionClear();
            pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
            pmProject.UI.PointsTreeRefreshNodeCheck();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, выделить дубликаты точек в треке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_SelectDupsInTrack_Click(object sender, EventArgs e)
        {
            pmProject.Data.State.SelectDuplicatedPointsInActiveObject(EPMapObjectType.Track);
            pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
            pmProject.UI.PointsTreeRefreshNodeCheck();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, выделить дубликаты точек в сессии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_SelectDupsInSession_Click(object sender, EventArgs e)
        {
            pmProject.Data.State.SelectDuplicatedPointsInActiveObject(EPMapObjectType.Session);
            pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
            pmProject.UI.PointsTreeRefreshNodeCheck();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, выделение точек с некорректными глубинами в треке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_SelectBadDepthInTrack_Click(object sender, EventArgs e)
        {
            pmProject.Data.State.SelectBadDepthPointsInActiveObject
                (EPMapObjectType.Track, 
                pmProject.UI.Settings.pointBadLowDepth, 
                pmProject.UI.Settings.showBadLowDepth,
                pmProject.UI.Settings.pointBadGreatDepth,
                pmProject.UI.Settings.showBadGreatDepth);
            
            pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
            pmProject.UI.PointsTreeRefreshNodeCheck();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, выделение точек с некорректными глубинами в сессии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_SelectBadDepthInSession_Click(object sender, EventArgs e)
        {
            pmProject.Data.State.SelectBadDepthPointsInActiveObject
                (EPMapObjectType.Session,
                pmProject.UI.Settings.pointBadLowDepth,
                pmProject.UI.Settings.showBadLowDepth,
                pmProject.UI.Settings.pointBadGreatDepth,
                pmProject.UI.Settings.showBadGreatDepth);
            pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
            pmProject.UI.PointsTreeRefreshNodeCheck();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, удаление выделенных точек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_DeleteSelectedPoints_Click(object sender, EventArgs e)
        {
            if (pmProject.Data.State.SelectedPoints.Count == 0)
                return;

            if (MessageBox.Show("Удалить отмеченные точки?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            PM_DeleteSelectedPoints();

            pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
            pmProject.UI.PointsTreeRefreshNodes();
        }

        /// <summary>
        /// Обработчик - главная форма, закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Закрыть приложение?\nВсе несохраненные изменения будут потеряны.", "Выход", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, выделение близких точек в треке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_SelectClosePointsInTrack_Click(object sender, EventArgs e)
        {
            inputboxForm.QueryText = "Расстояние в метрах:";
            inputboxForm.FormCaptionText = "Близкие точки";
            inputboxForm.ValueText = "0";
            inputboxForm.ValueType = 0.0.GetType();
            inputboxForm.ShowDialog();

            double result = 0;

            if (inputboxForm.TextValueChanged)
            {
                // не проверяем результат, было проверено в форме
                double.TryParse(inputboxForm.ValueText, out result);
            }

            pmProject.Data.State.SelectClosePointsInActiveObject(EPMapObjectType.Track, result);
            pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
            pmProject.UI.PointsTreeRefreshNodeCheck();
        }

        /// <summary>
        /// Обработчик - контекстное меню дерева точек, выделение близких точек в сессии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_PointsTree_SelectClosePointsInSession_Click(object sender, EventArgs e)
        {
            inputboxForm.QueryText = "Расстояние в метрах:";
            inputboxForm.FormCaptionText = "Близкие точки";
            inputboxForm.ValueText = "0";
            inputboxForm.ValueType = 0.0.GetType();
            inputboxForm.ShowDialog();

            double result = 0;

            if (inputboxForm.TextValueChanged)
            {
                // не проверяем результат, было проверено в форме
                double.TryParse(inputboxForm.ValueText, out result);
            }

            pmProject.Data.State.SelectClosePointsInActiveObject(EPMapObjectType.Session, result);
            pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
            pmProject.UI.PointsTreeRefreshNodeCheck();

        }

    }
}
