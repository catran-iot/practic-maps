namespace PracticMapsProject
{
    partial class main_Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MainMenu_FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_File_ImportTarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_File_ImportCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_File_LoadDirPTSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_File_LoadPlaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_File_ReloadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_File_ClearProjectDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_File_ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Map_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Map_BaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Map_SetGoogleHibrydToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Map_SetYandexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Map_SetOSMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Map_MarkersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Map_ClearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Map_MoveRMBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Map_SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Help_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Help_AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Help_DebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.gmapMain = new GMap.NET.WindowsForms.GMapControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSessions = new System.Windows.Forms.TabPage();
            this.treePoints = new System.Windows.Forms.TreeView();
            this.tabPlaces = new System.Windows.Forms.TabPage();
            this.treePlaces = new System.Windows.Forms.TreeView();
            this.contextMenuStrip_points = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenu_PointsTree_GotoPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_MoveObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_EditObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_DelObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenu_PointsTree_DepthCorrectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_DepthTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_DepthSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenu_PointsTree_SelectPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_SelDupeInTracktoolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_SelBadDepthInTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_SelClosePointsInTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenu_PointsTree_SelDupeInSessiontoolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_SelBadDepthInSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_SelClosePointsInSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_SelectionClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_MoveSelectionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_DelPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenu_PointsTree_SaveSessionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_ExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_ExportSessionTarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_ExportSessionToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PointsTree_SaveSessionToDirPTSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenu_PointsTree_ExportTrackToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_marker = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenu_Marker_EditPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Marker_MovePointStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Marker_DeletePointtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStrip_places = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenu_PlacesTree_GoToPlaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PlacesTree_MovePlaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PlacesTree_EditPlaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PlacesTree_SavePlaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PlacesTree_ExportPlaceToPLCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_PlacesTree_DelPlaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabSessions.SuspendLayout();
            this.tabPlaces.SuspendLayout();
            this.contextMenuStrip_points.SuspendLayout();
            this.contextMenuStrip_marker.SuspendLayout();
            this.contextMenuStrip_places.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 24);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(49, 19);
            this.toolStripStatusLabel1.Text = "Zoom: ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(138, 19);
            this.toolStripStatusLabel2.Text = "Нет активного действия";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_FileToolStripMenuItem,
            this.MainMenu_Map_ToolStripMenuItem,
            this.MainMenu_Help_ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MainMenu_FileToolStripMenuItem
            // 
            this.MainMenu_FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File_ImportTarToolStripMenuItem,
            this.MainMenu_File_ImportCSVToolStripMenuItem,
            this.MainMenu_File_LoadDirPTSToolStripMenuItem,
            this.toolStripMenuItem3,
            this.MainMenu_File_LoadPlaceToolStripMenuItem,
            this.toolStripMenuItem6,
            this.MainMenu_File_ReloadProjectToolStripMenuItem,
            this.MainMenu_File_ClearProjectDirToolStripMenuItem,
            this.toolStripMenuItem1,
            this.MainMenu_File_ExitToolStripMenuItem});
            this.MainMenu_FileToolStripMenuItem.Name = "MainMenu_FileToolStripMenuItem";
            this.MainMenu_FileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.MainMenu_FileToolStripMenuItem.Text = "Файл";
            // 
            // MainMenu_File_ImportTarToolStripMenuItem
            // 
            this.MainMenu_File_ImportTarToolStripMenuItem.Name = "MainMenu_File_ImportTarToolStripMenuItem";
            this.MainMenu_File_ImportTarToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.MainMenu_File_ImportTarToolStripMenuItem.Text = "Загрузить файл экспорта (tar)";
            this.MainMenu_File_ImportTarToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_File_ImportTar_Click);
            // 
            // MainMenu_File_ImportCSVToolStripMenuItem
            // 
            this.MainMenu_File_ImportCSVToolStripMenuItem.Name = "MainMenu_File_ImportCSVToolStripMenuItem";
            this.MainMenu_File_ImportCSVToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.MainMenu_File_ImportCSVToolStripMenuItem.Text = "Загрузить файл (csv)";
            this.MainMenu_File_ImportCSVToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_File_ImportCSV_Click);
            // 
            // MainMenu_File_LoadDirPTSToolStripMenuItem
            // 
            this.MainMenu_File_LoadDirPTSToolStripMenuItem.Name = "MainMenu_File_LoadDirPTSToolStripMenuItem";
            this.MainMenu_File_LoadDirPTSToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.MainMenu_File_LoadDirPTSToolStripMenuItem.Text = "Загрузить папку с файлами (pts)";
            this.MainMenu_File_LoadDirPTSToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_File_LoadDirPTS_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(250, 6);
            // 
            // MainMenu_File_LoadPlaceToolStripMenuItem
            // 
            this.MainMenu_File_LoadPlaceToolStripMenuItem.Name = "MainMenu_File_LoadPlaceToolStripMenuItem";
            this.MainMenu_File_LoadPlaceToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.MainMenu_File_LoadPlaceToolStripMenuItem.Text = "Загрузить место (plc)";
            this.MainMenu_File_LoadPlaceToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_File_LoadPlace_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(250, 6);
            // 
            // MainMenu_File_ReloadProjectToolStripMenuItem
            // 
            this.MainMenu_File_ReloadProjectToolStripMenuItem.Name = "MainMenu_File_ReloadProjectToolStripMenuItem";
            this.MainMenu_File_ReloadProjectToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.MainMenu_File_ReloadProjectToolStripMenuItem.Text = "Сбросить изменения в проекте";
            this.MainMenu_File_ReloadProjectToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_File_ReloadProject_Click);
            // 
            // MainMenu_File_ClearProjectDirToolStripMenuItem
            // 
            this.MainMenu_File_ClearProjectDirToolStripMenuItem.Name = "MainMenu_File_ClearProjectDirToolStripMenuItem";
            this.MainMenu_File_ClearProjectDirToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.MainMenu_File_ClearProjectDirToolStripMenuItem.Text = "Удалить все данные проекта";
            this.MainMenu_File_ClearProjectDirToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_File_ClearProjectDir_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(250, 6);
            // 
            // MainMenu_File_ExitToolStripMenuItem
            // 
            this.MainMenu_File_ExitToolStripMenuItem.Name = "MainMenu_File_ExitToolStripMenuItem";
            this.MainMenu_File_ExitToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.MainMenu_File_ExitToolStripMenuItem.Text = "Выход";
            this.MainMenu_File_ExitToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_File_Exit_Click);
            // 
            // MainMenu_Map_ToolStripMenuItem
            // 
            this.MainMenu_Map_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Map_BaseToolStripMenuItem,
            this.MainMenu_Map_MarkersToolStripMenuItem,
            this.MainMenu_Map_MoveRMBToolStripMenuItem,
            this.MainMenu_Map_SettingsToolStripMenuItem});
            this.MainMenu_Map_ToolStripMenuItem.Name = "MainMenu_Map_ToolStripMenuItem";
            this.MainMenu_Map_ToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.MainMenu_Map_ToolStripMenuItem.Text = "Карта";
            // 
            // MainMenu_Map_BaseToolStripMenuItem
            // 
            this.MainMenu_Map_BaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Map_SetGoogleHibrydToolStripMenuItem,
            this.MainMenu_Map_SetYandexToolStripMenuItem,
            this.MainMenu_Map_SetOSMToolStripMenuItem});
            this.MainMenu_Map_BaseToolStripMenuItem.Name = "MainMenu_Map_BaseToolStripMenuItem";
            this.MainMenu_Map_BaseToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.MainMenu_Map_BaseToolStripMenuItem.Text = "Подложка";
            // 
            // MainMenu_Map_SetGoogleHibrydToolStripMenuItem
            // 
            this.MainMenu_Map_SetGoogleHibrydToolStripMenuItem.Name = "MainMenu_Map_SetGoogleHibrydToolStripMenuItem";
            this.MainMenu_Map_SetGoogleHibrydToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.MainMenu_Map_SetGoogleHibrydToolStripMenuItem.Text = "Карта Google (гибридная)";
            this.MainMenu_Map_SetGoogleHibrydToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_Map_SetGoogleHibryd_Click);
            // 
            // MainMenu_Map_SetYandexToolStripMenuItem
            // 
            this.MainMenu_Map_SetYandexToolStripMenuItem.Name = "MainMenu_Map_SetYandexToolStripMenuItem";
            this.MainMenu_Map_SetYandexToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.MainMenu_Map_SetYandexToolStripMenuItem.Text = "Карта Yandex (спутник)";
            this.MainMenu_Map_SetYandexToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_Map_SetYandex_Click);
            // 
            // MainMenu_Map_SetOSMToolStripMenuItem
            // 
            this.MainMenu_Map_SetOSMToolStripMenuItem.Name = "MainMenu_Map_SetOSMToolStripMenuItem";
            this.MainMenu_Map_SetOSMToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.MainMenu_Map_SetOSMToolStripMenuItem.Text = "Карта OSM (схема)";
            this.MainMenu_Map_SetOSMToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_Map_SetOSM_Click);
            // 
            // MainMenu_Map_MarkersToolStripMenuItem
            // 
            this.MainMenu_Map_MarkersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Map_ClearSelectionToolStripMenuItem});
            this.MainMenu_Map_MarkersToolStripMenuItem.Name = "MainMenu_Map_MarkersToolStripMenuItem";
            this.MainMenu_Map_MarkersToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.MainMenu_Map_MarkersToolStripMenuItem.Text = "Объекты";
            // 
            // MainMenu_Map_ClearSelectionToolStripMenuItem
            // 
            this.MainMenu_Map_ClearSelectionToolStripMenuItem.Name = "MainMenu_Map_ClearSelectionToolStripMenuItem";
            this.MainMenu_Map_ClearSelectionToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.MainMenu_Map_ClearSelectionToolStripMenuItem.Text = "Убрать все отметки";
            this.MainMenu_Map_ClearSelectionToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_Map_ClearSelection_Click);
            // 
            // MainMenu_Map_MoveRMBToolStripMenuItem
            // 
            this.MainMenu_Map_MoveRMBToolStripMenuItem.Checked = true;
            this.MainMenu_Map_MoveRMBToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MainMenu_Map_MoveRMBToolStripMenuItem.Name = "MainMenu_Map_MoveRMBToolStripMenuItem";
            this.MainMenu_Map_MoveRMBToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.MainMenu_Map_MoveRMBToolStripMenuItem.Text = "Перемещать правой кнопкой";
            this.MainMenu_Map_MoveRMBToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_Map_MoveByRMB_Click);
            // 
            // MainMenu_Map_SettingsToolStripMenuItem
            // 
            this.MainMenu_Map_SettingsToolStripMenuItem.Name = "MainMenu_Map_SettingsToolStripMenuItem";
            this.MainMenu_Map_SettingsToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.MainMenu_Map_SettingsToolStripMenuItem.Text = "Настройки";
            this.MainMenu_Map_SettingsToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_Map_Settings_Click);
            // 
            // MainMenu_Help_ToolStripMenuItem
            // 
            this.MainMenu_Help_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Help_AboutToolStripMenuItem,
            this.MainMenu_Help_DebugToolStripMenuItem});
            this.MainMenu_Help_ToolStripMenuItem.Name = "MainMenu_Help_ToolStripMenuItem";
            this.MainMenu_Help_ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.MainMenu_Help_ToolStripMenuItem.Text = "Справка";
            // 
            // MainMenu_Help_AboutToolStripMenuItem
            // 
            this.MainMenu_Help_AboutToolStripMenuItem.Name = "MainMenu_Help_AboutToolStripMenuItem";
            this.MainMenu_Help_AboutToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.MainMenu_Help_AboutToolStripMenuItem.Text = "О программе";
            this.MainMenu_Help_AboutToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_Help_About_Click);
            // 
            // MainMenu_Help_DebugToolStripMenuItem
            // 
            this.MainMenu_Help_DebugToolStripMenuItem.Enabled = false;
            this.MainMenu_Help_DebugToolStripMenuItem.Name = "MainMenu_Help_DebugToolStripMenuItem";
            this.MainMenu_Help_DebugToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.MainMenu_Help_DebugToolStripMenuItem.Text = "Режим отладки";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.gmapMain);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer.Size = new System.Drawing.Size(800, 402);
            this.splitContainer.SplitterDistance = 642;
            this.splitContainer.TabIndex = 3;
            // 
            // gmapMain
            // 
            this.gmapMain.Bearing = 0F;
            this.gmapMain.CanDragMap = true;
            this.gmapMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gmapMain.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmapMain.GrayScaleMode = false;
            this.gmapMain.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmapMain.LevelsKeepInMemory = 5;
            this.gmapMain.Location = new System.Drawing.Point(0, 0);
            this.gmapMain.MarkersEnabled = true;
            this.gmapMain.MaxZoom = 20;
            this.gmapMain.MinZoom = 2;
            this.gmapMain.MouseWheelZoomEnabled = true;
            this.gmapMain.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.gmapMain.Name = "gmapMain";
            this.gmapMain.NegativeMode = false;
            this.gmapMain.PolygonsEnabled = false;
            this.gmapMain.RetryLoadTile = 0;
            this.gmapMain.RoutesEnabled = false;
            this.gmapMain.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmapMain.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmapMain.ShowTileGridLines = false;
            this.gmapMain.Size = new System.Drawing.Size(642, 402);
            this.gmapMain.TabIndex = 0;
            this.gmapMain.Zoom = 3D;
            this.gmapMain.OnMapClick += new GMap.NET.WindowsForms.MapClick(this.gmapMain_OnMapClick);
            this.gmapMain.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmapMain_OnMarkerClick);
            this.gmapMain.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.gmapMain_OnMapZoomChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSessions);
            this.tabControl1.Controls.Add(this.tabPlaces);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(154, 402);
            this.tabControl1.TabIndex = 2;
            // 
            // tabSessions
            // 
            this.tabSessions.Controls.Add(this.treePoints);
            this.tabSessions.Location = new System.Drawing.Point(4, 22);
            this.tabSessions.Name = "tabSessions";
            this.tabSessions.Padding = new System.Windows.Forms.Padding(3);
            this.tabSessions.Size = new System.Drawing.Size(146, 376);
            this.tabSessions.TabIndex = 0;
            this.tabSessions.Text = "Сессии";
            this.tabSessions.UseVisualStyleBackColor = true;
            // 
            // treePoints
            // 
            this.treePoints.CheckBoxes = true;
            this.treePoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePoints.FullRowSelect = true;
            this.treePoints.HideSelection = false;
            this.treePoints.Location = new System.Drawing.Point(3, 3);
            this.treePoints.Name = "treePoints";
            this.treePoints.Size = new System.Drawing.Size(140, 370);
            this.treePoints.TabIndex = 0;
            this.treePoints.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treePoints_AfterCheck);
            this.treePoints.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treePoints_NodeMouseClick);
            // 
            // tabPlaces
            // 
            this.tabPlaces.Controls.Add(this.treePlaces);
            this.tabPlaces.Location = new System.Drawing.Point(4, 22);
            this.tabPlaces.Name = "tabPlaces";
            this.tabPlaces.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlaces.Size = new System.Drawing.Size(146, 376);
            this.tabPlaces.TabIndex = 1;
            this.tabPlaces.Text = "Места";
            this.tabPlaces.UseVisualStyleBackColor = true;
            // 
            // treePlaces
            // 
            this.treePlaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePlaces.Location = new System.Drawing.Point(3, 3);
            this.treePlaces.Name = "treePlaces";
            this.treePlaces.Size = new System.Drawing.Size(140, 370);
            this.treePlaces.TabIndex = 1;
            this.treePlaces.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treePlaces_NodeMouseClick);
            // 
            // contextMenuStrip_points
            // 
            this.contextMenuStrip_points.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenu_PointsTree_GotoPointToolStripMenuItem,
            this.ContextMenu_PointsTree_MoveObjectToolStripMenuItem,
            this.ContextMenu_PointsTree_EditObjectToolStripMenuItem,
            this.ContextMenu_PointsTree_DelObjectToolStripMenuItem,
            this.toolStripMenuItem5,
            this.ContextMenu_PointsTree_DepthCorrectionToolStripMenuItem,
            this.toolStripMenuItem2,
            this.ContextMenu_PointsTree_SelectPointsToolStripMenuItem,
            this.ContextMenu_PointsTree_SelectionClearToolStripMenuItem,
            this.ContextMenu_PointsTree_MoveSelectionToolStripMenuItem1,
            this.ContextMenu_PointsTree_DelPointsToolStripMenuItem,
            this.toolStripMenuItem4,
            this.ContextMenu_PointsTree_SaveSessionToolStripMenuItem1,
            this.ContextMenu_PointsTree_ExportToolStripMenuItem});
            this.contextMenuStrip_points.Name = "contextMenuStrip1";
            this.contextMenuStrip_points.Size = new System.Drawing.Size(254, 264);
            // 
            // ContextMenu_PointsTree_GotoPointToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_GotoPointToolStripMenuItem.Name = "ContextMenu_PointsTree_GotoPointToolStripMenuItem";
            this.ContextMenu_PointsTree_GotoPointToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.ContextMenu_PointsTree_GotoPointToolStripMenuItem.Text = "Перейти к объекту";
            this.ContextMenu_PointsTree_GotoPointToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_GotoPoint_Click);
            // 
            // ContextMenu_PointsTree_MoveObjectToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_MoveObjectToolStripMenuItem.Name = "ContextMenu_PointsTree_MoveObjectToolStripMenuItem";
            this.ContextMenu_PointsTree_MoveObjectToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.ContextMenu_PointsTree_MoveObjectToolStripMenuItem.Text = "Переместить точку";
            this.ContextMenu_PointsTree_MoveObjectToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_MoveObject_Click);
            // 
            // ContextMenu_PointsTree_EditObjectToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_EditObjectToolStripMenuItem.Name = "ContextMenu_PointsTree_EditObjectToolStripMenuItem";
            this.ContextMenu_PointsTree_EditObjectToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.ContextMenu_PointsTree_EditObjectToolStripMenuItem.Text = "Редактировать объект";
            this.ContextMenu_PointsTree_EditObjectToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_EditObject_Click);
            // 
            // ContextMenu_PointsTree_DelObjectToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_DelObjectToolStripMenuItem.Name = "ContextMenu_PointsTree_DelObjectToolStripMenuItem";
            this.ContextMenu_PointsTree_DelObjectToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.ContextMenu_PointsTree_DelObjectToolStripMenuItem.Text = "Удалить объект";
            this.ContextMenu_PointsTree_DelObjectToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_DeleteSelectedObject_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(250, 6);
            // 
            // ContextMenu_PointsTree_DepthCorrectionToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_DepthCorrectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenu_PointsTree_DepthTrackToolStripMenuItem,
            this.ContextMenu_PointsTree_DepthSessionToolStripMenuItem});
            this.ContextMenu_PointsTree_DepthCorrectionToolStripMenuItem.Name = "ContextMenu_PointsTree_DepthCorrectionToolStripMenuItem";
            this.ContextMenu_PointsTree_DepthCorrectionToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.ContextMenu_PointsTree_DepthCorrectionToolStripMenuItem.Text = "Коррекция данных";
            // 
            // ContextMenu_PointsTree_DepthTrackToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_DepthTrackToolStripMenuItem.Name = "ContextMenu_PointsTree_DepthTrackToolStripMenuItem";
            this.ContextMenu_PointsTree_DepthTrackToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.ContextMenu_PointsTree_DepthTrackToolStripMenuItem.Text = "Глубины точек трека";
            this.ContextMenu_PointsTree_DepthTrackToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_CorrectionTrackDepth_Click);
            // 
            // ContextMenu_PointsTree_DepthSessionToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_DepthSessionToolStripMenuItem.Name = "ContextMenu_PointsTree_DepthSessionToolStripMenuItem";
            this.ContextMenu_PointsTree_DepthSessionToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.ContextMenu_PointsTree_DepthSessionToolStripMenuItem.Text = "Глубины точек сессии";
            this.ContextMenu_PointsTree_DepthSessionToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_CorrectionSessionDepth_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(250, 6);
            // 
            // ContextMenu_PointsTree_SelectPointsToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_SelectPointsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenu_PointsTree_SelDupeInTracktoolStripMenuItem7,
            this.ContextMenu_PointsTree_SelBadDepthInTrackToolStripMenuItem,
            this.ContextMenu_PointsTree_SelClosePointsInTrackToolStripMenuItem,
            this.toolStripMenuItem7,
            this.ContextMenu_PointsTree_SelDupeInSessiontoolStripMenuItem9,
            this.ContextMenu_PointsTree_SelBadDepthInSessionToolStripMenuItem,
            this.ContextMenu_PointsTree_SelClosePointsInSessionToolStripMenuItem});
            this.ContextMenu_PointsTree_SelectPointsToolStripMenuItem.Name = "ContextMenu_PointsTree_SelectPointsToolStripMenuItem";
            this.ContextMenu_PointsTree_SelectPointsToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.ContextMenu_PointsTree_SelectPointsToolStripMenuItem.Text = "Выделить точки...";
            // 
            // ContextMenu_PointsTree_SelDupeInTracktoolStripMenuItem7
            // 
            this.ContextMenu_PointsTree_SelDupeInTracktoolStripMenuItem7.Name = "ContextMenu_PointsTree_SelDupeInTracktoolStripMenuItem7";
            this.ContextMenu_PointsTree_SelDupeInTracktoolStripMenuItem7.Size = new System.Drawing.Size(256, 22);
            this.ContextMenu_PointsTree_SelDupeInTracktoolStripMenuItem7.Text = "Дубликаты в треке";
            this.ContextMenu_PointsTree_SelDupeInTracktoolStripMenuItem7.Click += new System.EventHandler(this.ContextMenu_PointsTree_SelectDupsInTrack_Click);
            // 
            // ContextMenu_PointsTree_SelBadDepthInTrackToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_SelBadDepthInTrackToolStripMenuItem.Name = "ContextMenu_PointsTree_SelBadDepthInTrackToolStripMenuItem";
            this.ContextMenu_PointsTree_SelBadDepthInTrackToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.ContextMenu_PointsTree_SelBadDepthInTrackToolStripMenuItem.Text = "Некорректные глубины в треке";
            this.ContextMenu_PointsTree_SelBadDepthInTrackToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_SelectBadDepthInTrack_Click);
            // 
            // ContextMenu_PointsTree_SelClosePointsInTrackToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_SelClosePointsInTrackToolStripMenuItem.Name = "ContextMenu_PointsTree_SelClosePointsInTrackToolStripMenuItem";
            this.ContextMenu_PointsTree_SelClosePointsInTrackToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.ContextMenu_PointsTree_SelClosePointsInTrackToolStripMenuItem.Text = "Близкие точки в треке";
            this.ContextMenu_PointsTree_SelClosePointsInTrackToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_SelectClosePointsInTrack_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(253, 6);
            // 
            // ContextMenu_PointsTree_SelDupeInSessiontoolStripMenuItem9
            // 
            this.ContextMenu_PointsTree_SelDupeInSessiontoolStripMenuItem9.Name = "ContextMenu_PointsTree_SelDupeInSessiontoolStripMenuItem9";
            this.ContextMenu_PointsTree_SelDupeInSessiontoolStripMenuItem9.Size = new System.Drawing.Size(256, 22);
            this.ContextMenu_PointsTree_SelDupeInSessiontoolStripMenuItem9.Text = "Дубликаты в сессии";
            this.ContextMenu_PointsTree_SelDupeInSessiontoolStripMenuItem9.Click += new System.EventHandler(this.ContextMenu_PointsTree_SelectDupsInSession_Click);
            // 
            // ContextMenu_PointsTree_SelBadDepthInSessionToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_SelBadDepthInSessionToolStripMenuItem.Name = "ContextMenu_PointsTree_SelBadDepthInSessionToolStripMenuItem";
            this.ContextMenu_PointsTree_SelBadDepthInSessionToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.ContextMenu_PointsTree_SelBadDepthInSessionToolStripMenuItem.Text = "Некорректные глубины в сессии";
            this.ContextMenu_PointsTree_SelBadDepthInSessionToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_SelectBadDepthInSession_Click);
            // 
            // ContextMenu_PointsTree_SelClosePointsInSessionToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_SelClosePointsInSessionToolStripMenuItem.Name = "ContextMenu_PointsTree_SelClosePointsInSessionToolStripMenuItem";
            this.ContextMenu_PointsTree_SelClosePointsInSessionToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.ContextMenu_PointsTree_SelClosePointsInSessionToolStripMenuItem.Text = "Близкие точки в сессии";
            this.ContextMenu_PointsTree_SelClosePointsInSessionToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_SelectClosePointsInSession_Click);
            // 
            // ContextMenu_PointsTree_SelectionClearToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_SelectionClearToolStripMenuItem.Name = "ContextMenu_PointsTree_SelectionClearToolStripMenuItem";
            this.ContextMenu_PointsTree_SelectionClearToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.ContextMenu_PointsTree_SelectionClearToolStripMenuItem.Text = "Снять выделение";
            this.ContextMenu_PointsTree_SelectionClearToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_SelectionClear_Click);
            // 
            // ContextMenu_PointsTree_MoveSelectionToolStripMenuItem1
            // 
            this.ContextMenu_PointsTree_MoveSelectionToolStripMenuItem1.Name = "ContextMenu_PointsTree_MoveSelectionToolStripMenuItem1";
            this.ContextMenu_PointsTree_MoveSelectionToolStripMenuItem1.Size = new System.Drawing.Size(253, 22);
            this.ContextMenu_PointsTree_MoveSelectionToolStripMenuItem1.Text = "Скопировать отмеченные точки";
            this.ContextMenu_PointsTree_MoveSelectionToolStripMenuItem1.Click += new System.EventHandler(this.ContextMenu_PointsTree_MoveSelection_Click);
            // 
            // ContextMenu_PointsTree_DelPointsToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_DelPointsToolStripMenuItem.Name = "ContextMenu_PointsTree_DelPointsToolStripMenuItem";
            this.ContextMenu_PointsTree_DelPointsToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.ContextMenu_PointsTree_DelPointsToolStripMenuItem.Text = "Удалить отмеченные точки";
            this.ContextMenu_PointsTree_DelPointsToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_DeleteSelectedPoints_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(250, 6);
            // 
            // ContextMenu_PointsTree_SaveSessionToolStripMenuItem1
            // 
            this.ContextMenu_PointsTree_SaveSessionToolStripMenuItem1.Name = "ContextMenu_PointsTree_SaveSessionToolStripMenuItem1";
            this.ContextMenu_PointsTree_SaveSessionToolStripMenuItem1.Size = new System.Drawing.Size(253, 22);
            this.ContextMenu_PointsTree_SaveSessionToolStripMenuItem1.Text = "Сохранить изменения в сессии";
            this.ContextMenu_PointsTree_SaveSessionToolStripMenuItem1.Click += new System.EventHandler(this.ContextMenu_PointsTree_SaveSession_Click);
            // 
            // ContextMenu_PointsTree_ExportToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_ExportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenu_PointsTree_ExportSessionTarToolStripMenuItem,
            this.ContextMenu_PointsTree_ExportSessionToCSVToolStripMenuItem,
            this.ContextMenu_PointsTree_SaveSessionToDirPTSToolStripMenuItem,
            this.toolStripMenuItem8,
            this.ContextMenu_PointsTree_ExportTrackToCSVToolStripMenuItem});
            this.ContextMenu_PointsTree_ExportToolStripMenuItem.Name = "ContextMenu_PointsTree_ExportToolStripMenuItem";
            this.ContextMenu_PointsTree_ExportToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.ContextMenu_PointsTree_ExportToolStripMenuItem.Text = "Экспорт";
            // 
            // ContextMenu_PointsTree_ExportSessionTarToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_ExportSessionTarToolStripMenuItem.Name = "ContextMenu_PointsTree_ExportSessionTarToolStripMenuItem";
            this.ContextMenu_PointsTree_ExportSessionTarToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.ContextMenu_PointsTree_ExportSessionTarToolStripMenuItem.Text = "Сессия в TAR";
            this.ContextMenu_PointsTree_ExportSessionTarToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_ExportSessionToTAR_Click);
            // 
            // ContextMenu_PointsTree_ExportSessionToCSVToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_ExportSessionToCSVToolStripMenuItem.Name = "ContextMenu_PointsTree_ExportSessionToCSVToolStripMenuItem";
            this.ContextMenu_PointsTree_ExportSessionToCSVToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.ContextMenu_PointsTree_ExportSessionToCSVToolStripMenuItem.Text = "Сессия в CSV";
            this.ContextMenu_PointsTree_ExportSessionToCSVToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_ExportSessionToCSV_Click);
            // 
            // ContextMenu_PointsTree_SaveSessionToDirPTSToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_SaveSessionToDirPTSToolStripMenuItem.Name = "ContextMenu_PointsTree_SaveSessionToDirPTSToolStripMenuItem";
            this.ContextMenu_PointsTree_SaveSessionToDirPTSToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.ContextMenu_PointsTree_SaveSessionToDirPTSToolStripMenuItem.Text = "Сессия в папку (PTS)";
            this.ContextMenu_PointsTree_SaveSessionToDirPTSToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_SaveSessionToDirPTS_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(184, 6);
            // 
            // ContextMenu_PointsTree_ExportTrackToCSVToolStripMenuItem
            // 
            this.ContextMenu_PointsTree_ExportTrackToCSVToolStripMenuItem.Name = "ContextMenu_PointsTree_ExportTrackToCSVToolStripMenuItem";
            this.ContextMenu_PointsTree_ExportTrackToCSVToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.ContextMenu_PointsTree_ExportTrackToCSVToolStripMenuItem.Text = "Трек в CSV";
            this.ContextMenu_PointsTree_ExportTrackToCSVToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PointsTree_ExportTrackToCSV_Click);
            // 
            // contextMenuStrip_marker
            // 
            this.contextMenuStrip_marker.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenu_Marker_EditPointToolStripMenuItem,
            this.ContextMenu_Marker_MovePointStripMenuItem,
            this.ContextMenu_Marker_DeletePointtoolStripMenuItem});
            this.contextMenuStrip_marker.Name = "contextMenuStrip2";
            this.contextMenuStrip_marker.Size = new System.Drawing.Size(155, 70);
            // 
            // ContextMenu_Marker_EditPointToolStripMenuItem
            // 
            this.ContextMenu_Marker_EditPointToolStripMenuItem.Name = "ContextMenu_Marker_EditPointToolStripMenuItem";
            this.ContextMenu_Marker_EditPointToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.ContextMenu_Marker_EditPointToolStripMenuItem.Text = "Редактировать";
            this.ContextMenu_Marker_EditPointToolStripMenuItem.Click += new System.EventHandler(this.ContextMenuMarker_EditPoint_Click);
            // 
            // ContextMenu_Marker_MovePointStripMenuItem
            // 
            this.ContextMenu_Marker_MovePointStripMenuItem.Name = "ContextMenu_Marker_MovePointStripMenuItem";
            this.ContextMenu_Marker_MovePointStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.ContextMenu_Marker_MovePointStripMenuItem.Text = "Переместить";
            this.ContextMenu_Marker_MovePointStripMenuItem.Click += new System.EventHandler(this.ContextMenu_Marker_MovePoint_Click);
            // 
            // ContextMenu_Marker_DeletePointtoolStripMenuItem
            // 
            this.ContextMenu_Marker_DeletePointtoolStripMenuItem.Name = "ContextMenu_Marker_DeletePointtoolStripMenuItem";
            this.ContextMenu_Marker_DeletePointtoolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.ContextMenu_Marker_DeletePointtoolStripMenuItem.Text = "Удалить";
            this.ContextMenu_Marker_DeletePointtoolStripMenuItem.Click += new System.EventHandler(this.ContextMenuMarker_DeletePoint_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "tar";
            this.saveFileDialog1.Filter = "Файлы импорта Практик (*.tar)|*.tar";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // contextMenuStrip_places
            // 
            this.contextMenuStrip_places.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenu_PlacesTree_GoToPlaceToolStripMenuItem,
            this.ContextMenu_PlacesTree_MovePlaceToolStripMenuItem,
            this.ContextMenu_PlacesTree_EditPlaceToolStripMenuItem,
            this.ContextMenu_PlacesTree_SavePlaceToolStripMenuItem,
            this.ContextMenu_PlacesTree_ExportPlaceToPLCToolStripMenuItem,
            this.ContextMenu_PlacesTree_DelPlaceToolStripMenuItem});
            this.contextMenuStrip_places.Name = "contextMenuStrip_place";
            this.contextMenuStrip_places.Size = new System.Drawing.Size(197, 158);
            // 
            // ContextMenu_PlacesTree_GoToPlaceToolStripMenuItem
            // 
            this.ContextMenu_PlacesTree_GoToPlaceToolStripMenuItem.Name = "ContextMenu_PlacesTree_GoToPlaceToolStripMenuItem";
            this.ContextMenu_PlacesTree_GoToPlaceToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ContextMenu_PlacesTree_GoToPlaceToolStripMenuItem.Text = "Перейти к месту";
            this.ContextMenu_PlacesTree_GoToPlaceToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PlacesTree_GoToPlace_Click);
            // 
            // ContextMenu_PlacesTree_MovePlaceToolStripMenuItem
            // 
            this.ContextMenu_PlacesTree_MovePlaceToolStripMenuItem.Name = "ContextMenu_PlacesTree_MovePlaceToolStripMenuItem";
            this.ContextMenu_PlacesTree_MovePlaceToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ContextMenu_PlacesTree_MovePlaceToolStripMenuItem.Text = "Переместить";
            this.ContextMenu_PlacesTree_MovePlaceToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PlacesTree_MovePlace_Click);
            // 
            // ContextMenu_PlacesTree_EditPlaceToolStripMenuItem
            // 
            this.ContextMenu_PlacesTree_EditPlaceToolStripMenuItem.Name = "ContextMenu_PlacesTree_EditPlaceToolStripMenuItem";
            this.ContextMenu_PlacesTree_EditPlaceToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ContextMenu_PlacesTree_EditPlaceToolStripMenuItem.Text = "Редактировать";
            this.ContextMenu_PlacesTree_EditPlaceToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PlacesTree_EditPlace_Click);
            // 
            // ContextMenu_PlacesTree_SavePlaceToolStripMenuItem
            // 
            this.ContextMenu_PlacesTree_SavePlaceToolStripMenuItem.Name = "ContextMenu_PlacesTree_SavePlaceToolStripMenuItem";
            this.ContextMenu_PlacesTree_SavePlaceToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ContextMenu_PlacesTree_SavePlaceToolStripMenuItem.Text = "Сохранить изменения";
            this.ContextMenu_PlacesTree_SavePlaceToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PlacesTree_SavePlace_Click);
            // 
            // ContextMenu_PlacesTree_ExportPlaceToPLCToolStripMenuItem
            // 
            this.ContextMenu_PlacesTree_ExportPlaceToPLCToolStripMenuItem.Name = "ContextMenu_PlacesTree_ExportPlaceToPLCToolStripMenuItem";
            this.ContextMenu_PlacesTree_ExportPlaceToPLCToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ContextMenu_PlacesTree_ExportPlaceToPLCToolStripMenuItem.Text = "Экспортировать";
            this.ContextMenu_PlacesTree_ExportPlaceToPLCToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PlacesTree_ExportPlace_Click);
            // 
            // ContextMenu_PlacesTree_DelPlaceToolStripMenuItem
            // 
            this.ContextMenu_PlacesTree_DelPlaceToolStripMenuItem.Name = "ContextMenu_PlacesTree_DelPlaceToolStripMenuItem";
            this.ContextMenu_PlacesTree_DelPlaceToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ContextMenu_PlacesTree_DelPlaceToolStripMenuItem.Text = "Удалить";
            this.ContextMenu_PlacesTree_DelPlaceToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_PlacesTree_DeletePlace_Click);
            // 
            // main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(200, 400);
            this.Name = "main_Form";
            this.Text = "Practic Maps";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_Form_FormClosing);
            this.Load += new System.EventHandler(this.main_Form_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabSessions.ResumeLayout(false);
            this.tabPlaces.ResumeLayout(false);
            this.contextMenuStrip_points.ResumeLayout(false);
            this.contextMenuStrip_marker.ResumeLayout(false);
            this.contextMenuStrip_places.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private GMap.NET.WindowsForms.GMapControl gmapMain;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_FileToolStripMenuItem;
        private System.Windows.Forms.TreeView treePoints;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Map_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Map_BaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Map_SetGoogleHibrydToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Map_SetYandexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Map_SetOSMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Map_MarkersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Map_ClearSelectionToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_points;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_GotoPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File_ClearProjectDirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_DelObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File_ReloadProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File_ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File_ImportTarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Help_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Help_AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_SaveSessionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File_LoadDirPTSToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_MoveSelectionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_marker;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Marker_EditPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Marker_MovePointStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Marker_DeletePointtoolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File_ImportCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_EditObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_MoveObjectToolStripMenuItem;
        private System.Windows.Forms.TreeView treePlaces;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSessions;
        private System.Windows.Forms.TabPage tabPlaces;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_places;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PlacesTree_EditPlaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PlacesTree_MovePlaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PlacesTree_DelPlaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File_LoadPlaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Map_MoveRMBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PlacesTree_GoToPlaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PlacesTree_SavePlaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PlacesTree_ExportPlaceToPLCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_DepthCorrectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_DepthTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_DepthSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_ExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_ExportSessionTarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_ExportSessionToCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_SaveSessionToDirPTSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_ExportTrackToCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Map_SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_SelectionClearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_DelPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_SelectPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_SelDupeInTracktoolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_SelDupeInSessiontoolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_SelBadDepthInTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_SelBadDepthInSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_SelClosePointsInTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_PointsTree_SelClosePointsInSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Help_DebugToolStripMenuItem;
    }
}

