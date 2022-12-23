using System;
using System.Windows.Forms;

namespace PracticMapsProject
{
    public partial class inputbox_Form : Form
    {
        Type valueType;
        string valueText;
        public bool TextValueChanged;
        public string ValueText
        {
            get
            {
                return valueText;
            }

            set
            {
                valueText = value;
            }

        }
        public Type ValueType
        {
            get
            {
                return valueType;
            }

            set
            {
                valueType = value;
            }

        }
        public string QueryText
        {
            set
            {
                lbText.Text = value;
            }

        }
        public string FormCaptionText
        {
            set
            {
                this.Text = value;
            }

        }
        public inputbox_Form()
        {
            InitializeComponent();
            valueType = null;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        // нажата кнопка ОК
        {
            // проверяем тип
            // если нужен числовой, проверяем корректность введенного значения
            if ((valueType == 0.0.GetType()) || (valueType == 0.GetType()))
            {
                if (!double.TryParse(tbText.Text, out double result))
                {
                    MessageBox.Show("Введено некорректное значение","Ошибка");
                    return;
                }
            }
            
            valueText = tbText.Text;
            TextValueChanged = true;
            Close();
        }

        private void inputbox_Form_Shown(object sender, EventArgs e)
        {
            tbText.Text = valueText;
            TextValueChanged = false;
        }
    }
}
