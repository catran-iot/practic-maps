using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticMapsProject
{
    public partial class about_Form : Form
    {
        public about_Form()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void about_Form_Shown(object sender, EventArgs e)
        {
            //string name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            lbVersion.Text = ver;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/practicmaps");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.rusfishing.ru/forum/threads/ehxolot-praktik.63063");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://rusonar.ru");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/judero01col/GMap.NET");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://7-zip.org");
        }
    }
}
