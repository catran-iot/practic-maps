using System;
using System.Windows.Forms;

namespace PracticMapsProject
{
    public partial class placeEdit_Form: Form
    {
        double pm_latitude, pm_longitude;
        string pm_name;
        public bool IsPlaceEdited { get; set; }
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
        public string PlaceName
        {
            get { return pm_name; }
            set { pm_name = value; }
        }
        public placeEdit_Form()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void placeEdit_Form_Shown(object sender, EventArgs e)
        {
            tbLatitude.Text = pm_latitude.ToString();
            tbLongitude.Text = pm_longitude.ToString();
            tbName.Text = pm_name;

            IsPlaceEdited = false;
        }

        public void btSave_Click(object sender, EventArgs e)
        {
            // нажата кнопка ОК
            if (!double.TryParse(tbLatitude.Text, out double lat) || !double.TryParse(tbLongitude.Text, out double lng))
            {
                MessageBox.Show("Некорректное значение координат");
                return;
            }

            pm_latitude = lat;
            pm_longitude = lng;
            pm_name = tbName.Text;
            IsPlaceEdited = true;

            Close();
        }

    }
}
