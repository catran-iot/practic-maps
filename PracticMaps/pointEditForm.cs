using System;
using System.Windows.Forms;

namespace PracticMapsProject
{
    public partial class pointEdit_Form : Form
    {
        double pm_latitude, pm_longitude, pm_depth, pm_correction;
        public pointEdit_Form()
        {
            InitializeComponent();
        }
        public bool IsPointEdited { get; set; }
        public double Latitude
        {
            get { return pm_latitude; }
            set { pm_latitude = value; }
        }
        public double Longitude
        {
            get { return pm_longitude; }
            set { pm_longitude = value; }
        }
        public double Depth
        {
            get { return pm_depth; }
            set { pm_depth = value; }
        }
        public double Correction
        {
            get { return pm_correction; }
            set { pm_correction = value; }
        }
        public string Time
        {
            get { return tbTime.Text; }
            set { tbTime.Text = value; }
        }
        public string Session
        {
            get { return tbSession.Text; }
            set { tbSession.Text = value; }
        }
        public string Track
        {
            get { return tbTrack.Text; }
            set { tbTrack.Text = value; }
        }

        private void pointEdit_Form_Shown(object sender, EventArgs e)
        {
            tbLatitude.Text = pm_latitude.ToString();
            tbLongitude.Text = pm_longitude.ToString();
            tbDepth.Text = pm_depth.ToString();
            tbCorrection.Text = pm_correction.ToString();

            IsPointEdited = false;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (double.TryParse(tbLatitude.Text, out double dbl))
                pm_latitude = dbl;
            else
            {
                MessageBox.Show("Некорректное значение широты", "Ошибка");
                return;
            }
            if (double.TryParse(tbLongitude.Text, out dbl))
                pm_longitude = dbl;
            else
            {
                MessageBox.Show("Некорректное значение долготы", "Ошибка");
                return;
            }
            if (double.TryParse(tbDepth.Text, out dbl))
                pm_depth = dbl;
            else
            {
                MessageBox.Show("Некорректное значение глубины", "Ошибка");
                return;
            }

            Latitude = pm_latitude;
            Longitude = pm_longitude;
            Depth = pm_depth;

            IsPointEdited = true;

            Close();

        }
    }
}
