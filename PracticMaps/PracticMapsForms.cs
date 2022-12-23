using System;
using System.Drawing;
using System.Windows.Forms;

namespace PracticMapsProject
{
    public partial class main_Form : Form

    {
        private readonly CPMapProject pmProject;

        /// <summary>
        /// Очищает каталог данных проекта и обрабатывает возникшие исключения
        /// </summary>
        public void PM_ClearProjectDirectory()
        {
            try
            {
                pmProject.Data.ClearProjectDirectory();
            }
            catch (PmException_DataSourceAccessError ex)
            {
                MessageBox.Show($"Ошибка при очистке каталога проекта ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"Ошибка при очистке каталога проекта ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"Неизвестная ошибка при очистке каталога проекта ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
        }

        /// <summary>
        /// Загружает данные проекта и обрабатывает возникшие исключения
        /// </summary>
        public void PM_LoadProject()
        {
            try
            {
                pmProject.Data.LoadProject();
            }
            catch (PmException_DataSourceAccessError ex)
            {
                MessageBox.Show($"Ошибка чтения данных проекта ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"Ошибка чтения данных проекта '{ex.Path}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
            catch (PmException_DataSourceIsEmpty ex)
            {
                MessageBox.Show($"В каталоге проекта нет данных для загрузки ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"В каталоге проекта нет данных для загрузки '{ex.Path}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
            catch (PmException_DataIsPartiallyIncorrect ex)
            {
                MessageBox.Show($"В каталоге проекта найдены некорректные данные\n{ex.Message}", "Ошибка");
                pmProject.Log.Error($"В каталоге проекта найдены некорректные данные '{ex.Path}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка в данных проекта ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"Неизвестная ошибка в данных проекта ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
        }


        /// <summary>
        /// Подготавливает и вызывает форму редактирования активного объекта
        /// </summary>
        public void PM_EditActiveObject()
        {
            // ссылка для сокращения кода
            CPMapObject mapObject = pmProject.Data.State.MapObject;
            // флаг - определяет, было ли в результате изменен объект и нужно ли обновлять объекты на карте
            bool refresh = false;

            // что редактируем?
            switch (mapObject)
            {
                case CPMapPoint point:

                    // подготавливаем и открываем диалог редактирования точки
                    pointEditForm.Track = point.ParentTrack.Name;
                    pointEditForm.Session = point.ParentSession.RootTrackRef.Name;
                    pointEditForm.Latitude = point.Latitude;
                    pointEditForm.Longitude = point.Longitude;
                    pointEditForm.Depth = point.Depth;
                    pointEditForm.Correction = point.ParentTrack.CorrectionValue;

                    // время хранится как ticks, может сбиваться часовой пояс
                    DateTime init_date = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    DateTime time_stamp = init_date.AddMilliseconds(point.Time).ToLocalTime();
                    pointEditForm.Time = time_stamp.ToString("yyyy-MM-dd_HH-mm-ss");

                    pointEditForm.ShowDialog();

                    // если были изменения, принимаем
                    if (pointEditForm.IsPointEdited)
                    {
                        point.Latitude = pointEditForm.Latitude;
                        point.Longitude = pointEditForm.Longitude;
                        point.Depth = pointEditForm.Depth;

                        point.ParentSession.IsChanged = true;
                        refresh = true;
                    }
                    break;

                case CPMapPlace place:

                    // подготавливаем и открываем диалог редактирования места
                    placeEditForm.Latitude = place.Latitude;
                    placeEditForm.Longitude = place.Longitude;
                    placeEditForm.PlaceName = place.Name;
                    placeEditForm.ShowDialog();

                    // если были изменения, принимаем
                    if (placeEditForm.IsPlaceEdited)
                    {
                        place.Latitude = placeEditForm.Latitude;
                        place.Longitude = placeEditForm.Longitude;
                        place.Name = placeEditForm.PlaceName;
                        refresh = true;
                    }

                    break;

                case CPMapSession:
                case CPMapTrack:

                    // подготавливаем и открываем диалог редактирования треков/сессии
                    inputboxForm.QueryText = "Новое имя:";
                    inputboxForm.FormCaptionText = "Переименовать";
                    inputboxForm.ValueText = mapObject.Name;
                    inputboxForm.ShowDialog();

                    if (inputboxForm.TextValueChanged)
                    {
                        mapObject.Name = inputboxForm.ValueText;
                        refresh = true;
                    }
                    break;

                default:

                    // тип объекта не распознан
                    pmProject.Log.Warning($"Редактирование данного объекта не поддерживается '{mapObject.GetType()}'");
                    MessageBox.Show("Редактирование данного объекта не поддерживается");
                    break;
            }

            if (refresh)
            {
                pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
                pmProject.UI.PointsTreeRefreshNodes();
                pmProject.UI.PlacesTreeRefreshNodes();
            }

            pmProject.Data.State.ActionCompleted();
        }

        /// <summary>
        /// Иницирует диалог процесса удаления активного объекта
        /// </summary>
        public void PM_DeleteActiveObject()
        {
            CPMapObject obj = pmProject.Data.GetFinalDeletionObject(pmProject.Data.State.MapObject);

            string message;
            switch (obj)
            {
                case CPMapSession session:
                    message = $"Удалить сессию '{session.Name}'?";
                    break;
                case CPMapTrack track:
                    message = $"Удалить трек '{track.Name}'?";
                    break;
                case CPMapPoint:
                    message = "Удалить точку?";
                    break;
                case CPMapPlace place:
                    message = $"Удалить место '{place.Name}'?";
                    break;
                default:
                    MessageBox.Show("Выбран объект неизвестного типа", "Внутренняя ошибка");
                    pmProject.Log.Error($"Попытка удалить объект неизвестного типа '{obj.Name}' ({obj})");
                    pmProject.Log.Debug($"PM_DeleteActiveObject, GetFinalDeletionObject = '{obj}'");
                    pmProject.Data.State.ActionCompleted();
                    return;
            }

            if (MessageBox.Show(message, "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    pmProject.Data.DeleteActiveObject();
                }
                catch (PmException_DataSourceAccessError ex)
                {
                    MessageBox.Show("Ошибка удаления данных с диска");
                    pmProject.Log.Error($"Ошибка удаления данных с диска '{ex.Path}' ({obj})");
                    pmProject.Log.Debug($"{ex}");
                }
                catch (PmException_EmptyParentAfterDeletion ex)
                {
                    MessageBox.Show("После удаления остается пустой родительский элемент", "Внутренняя ошибка");
                    pmProject.Log.Error($"Внутренняя ошибка - после удаления остается пустой родительский элемент '{obj}' ({ex.Message})");
                    pmProject.Log.Debug($"{ex}");
                    return;
                }
                catch (PmException_NotSupportedObjectType ex)
                {
                    MessageBox.Show("Попытка удалить объект неизвестного типа", "Внутренняя ошибка");
                    pmProject.Log.Error($"Попытка удалить объект неизвестного типа ({obj})");
                    pmProject.Log.Debug($"{ex}");
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления объекта", "Внутренняя ошибка");
                    pmProject.Log.Error($"Ошибка удаления объекта '{obj.Name}' ({ex.Message}): '{obj}'");
                    pmProject.Log.Debug($"{ex}");
                    return;
                }
                finally
                {
                    pmProject.Data.State.ActionCompleted();
                }

                pmProject.UI.MapDrawMarkers(EPMapObjectType.None);
                pmProject.UI.PointsTreeRefreshNodes();
                pmProject.UI.PlacesTreeRefreshNodes();
            }
        }

        /// <summary>
        /// Сохраняет сессию или трек в файл формата CSV и обрабатывает возникшие исключения
        /// </summary>
        /// <param name="pPath"></param>
        /// <param name="pSession"></param>
        /// <param name="pTrack"></param>
        public void PM_SaveSessionToCSV(string pPath, CPMapSession pSession, CPMapTrack pTrack)
        {
            try
            {
                pmProject.Data.SaveSessionToCSV(pPath, pSession, pTrack);
            }
            catch (PmException_DataSourceAccessError ex)
            {
                MessageBox.Show($"Не удалось сохранить данные ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"Не удалось сохранить данные в формате CSV '{pSession}' '{pTrack}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сохранить данные ({ex.Message})", "Внутренняя ошибка");
                pmProject.Log.Error($"Не удалось сохранить данные в формате CSV '{pSession}' '{pTrack}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
        }

        /// <summary>
        /// Сохраняет место в файл формата PLC и обрабатывает возникшие исключения
        /// </summary>
        public void PM_SavePlaceToPLC(string pPath, CPMapPlace pPlace, bool keepOldID)
        {
            try
            {
                pmProject.Data.SavePlaceToPLC(pPath, pPlace, keepOldID);
            }
            catch (PmException_DataSourceAccessError ex)
            {
                MessageBox.Show($"Не удалось сохранить место ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"Не удалось сохранить место в формате PLC '{pPlace}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сохранить место ({ex.Message})", "Внутренняя ошибка");
                pmProject.Log.Error($"Не удалось сохранить место в формате PLC '{pPlace}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
        }

        /// <summary>
        /// Сохраняет сессию в файл в экспортном формате "Практик 7" TAR и обрабатывает возникшие исключения
        /// </summary>
        /// <param name="pPath">Путь к сохраняемому файлу</param>
        /// <param name="pSession">Ссылка на сессию</param>
        public void PM_SaveSessionToTar(string pPath, CPMapSession pSession)
        {
            try
            {
                pmProject.Data.SaveSessionToTar(pPath, pSession);
            }
            catch (PmException_DataSourceAccessError ex)
            {
                MessageBox.Show($"Не удалось сохранить сессию ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"Не удалось сохранить сессию в формате TAR '{pSession}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
            catch (PmException_ExternalUtilityError ex)
            {
                MessageBox.Show($"Ошибка упаковки данных ({ex.Message})", "Внутренняя ошибка");
                pmProject.Log.Error($"Ошибка упаковки данных в формат TAR '{pSession}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
            catch (PmException_DataIsPartiallyIncorrect ex)
            {
                MessageBox.Show($"Итоговый файл содержит ошибки ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"Итоговый файл TAR содержит ошибки '{pSession}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сохранить сессию ({ex.Message})", "Внутренняя ошибка");
                pmProject.Log.Error($"Не удалось сохранить сессию в формате TAR '{pSession}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
        }
        /// <summary>
        /// Сохраняет сессию в папку в виде набора файлов PTS (папка предварительно полностью очищается) и обрабатывает возникцие исключения
        /// </summary>
        /// <param name="pPath">Путь к папке для сохранения данных</param>
        /// <param name="pSession">Ссылка на сессию проекта</param>
        /// <param name="keepOldId">Сохранять старый идентификатор сессии?</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PmException_DataSourceAccessError"></exception>
        /// <exception cref="PmException_DataIsPartiallyIncorrect"></exception>
        public void PM_SaveSessionToDirPTS(string pPath, CPMapSession pSession, bool keepOldId)
        {
            try
            {
                pmProject.Data.SaveSessionToDirPTS(pPath, pSession, keepOldId);
            }
            catch (PmException_DataSourceAccessError ex)
            {
                MessageBox.Show($"Не удалось сохранить сессию ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"Не удалось сохранить сессию '{pSession}' в формате PTS в каталог '{pPath}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
            catch (PmException_DataIsPartiallyIncorrect ex)
            {
                MessageBox.Show($"Данные сохранены не полностью ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"Данные сессии '{pSession}' сохранены не полностью в формате PTS в каталог '{pPath}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сохранить сессию ({ex.Message})", "Внутренняя ошибка");
                pmProject.Log.Error($"Не удалось сохранить сессию '{pSession}' в формате PTS в каталог '{pPath}' ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
        }

        /// <summary>
        /// Удаляет выделенные точки и обрабатывает возникшие исключения
        /// </summary>
        public void PM_DeleteSelectedPoints()
        {
            try
            {
                pmProject.Data.DeleteSelectedPoints();
            }

            catch (PmException_DataSourceAccessError ex)
            {
                MessageBox.Show($"Ошибка удаления данных с диска ({ex.Message})", "Ошибка");
                pmProject.Log.Error($"Ошибка удаления данных с диска ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }

            catch (PmException_EmptyParentAfterDeletion ex)
            {
                MessageBox.Show($"Выявлен пустой элемент после удаления данных ({ex.Message})", "Внутренняя ошибка");
                pmProject.Log.Error($"Выявлен пустой элемент после удаления данных ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }

            catch (PmException_NotSupportedObjectType ex)
            {
                MessageBox.Show($"Выявлен неподдерживаемый объект ({ex.Message})", "Внутренняя ошибка");
                pmProject.Log.Error($"Выявлен неподдерживаемый объект ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении точек ({ex.Message})", "Внутренняя ошибка");
                pmProject.Log.Error($"Ошибка при удалении точек ({ex.Message})");
                pmProject.Log.Debug($"{ex}");
            }
        }

        /// <summary>
        /// Устанавливает параметры элементов интерфейса
        /// </summary>
        public void PM_SetInterfaceElements()
        {
            // отображение величины зума карты
            toolStripStatusLabel1.Text = $"Zoom: {gmapMain.Zoom}";

            // индикация режима отладки
            MainMenu_Help_DebugToolStripMenuItem.Checked = pmProject.Log.DebugMode;

            // индикация текущего действия
            string text = "Нет активного действия";
            Color color = SystemColors.ControlText;

            switch (pmProject.Data.State.MapAction)
            {
                case EPMapAction.MoveObject:
                    switch (pmProject.Data.State.MapObject)
                    {
                        case CPMapPoint:
                            text = "Перенос точки (правый клик)";
                            color = Color.OrangeRed;
                            break;

                        case CPMapPlace:
                            text = "Перенос места (правый клик)";
                            color = Color.OrangeRed;
                            break;
                    }
                    break;

                default:
                    break;
            }
            toolStripStatusLabel2.Text = text;
            toolStripStatusLabel2.ForeColor = color;

        }

    }

}