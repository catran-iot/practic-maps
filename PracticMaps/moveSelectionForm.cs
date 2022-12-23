using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PracticMapsProject
{

    public partial class moveSelection_Form : Form
    {
        public const int NothingSelected = -2;
        public const int NewSelected = -1;


        private List<List<string>> pm_Names;
        public int SelectedSession { get; set; }
        public int SelectedTrack { get; set; }

        public moveSelection_Form()
        {
            InitializeComponent();
            pm_Names = new List<List<string>>();
        }


        public List<List<string>> PM_Names
        {
            set 
            { 
                pm_Names = value;
            }
        }

        private void moveSelection_Form_Shown(object sender, EventArgs e)
        {
            cbSession.Items.Clear();
            cbSession.Items.Add("<Новая сессия>");
            foreach (List<string> name in pm_Names)
            {
                cbSession.Items.Add(name[0]);
            }
            //cbSession.SelectedIndex = 0;

            cbTrack.Enabled = false;
            cbTrack.Items.Clear();

            btMove.Enabled = false;

            // признак, что ничего не было выбрано
            SelectedSession = NothingSelected;
            SelectedTrack = NothingSelected;
        }

        private void cbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTrack.Enabled = true;
            cbTrack.Items.Clear();
            cbTrack.Items.Add("<Новый трек>");
            if (cbSession.SelectedIndex > 0)
                foreach (string track_name in pm_Names[cbSession.SelectedIndex - 1])
                    cbTrack.Items.Add(track_name);

            cbTrack.SelectedIndex = 0;

            btMove.Enabled = true;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btMove_Click(object sender, EventArgs e)
        {
            // -1 соотвествует значению <новая сессия>, <новый трек>
            SelectedSession = cbSession.SelectedIndex - 1;
            SelectedTrack = cbTrack.SelectedIndex - 1;
            Close();
        }
    }
}
