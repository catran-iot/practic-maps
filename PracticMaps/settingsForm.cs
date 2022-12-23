using System;
using System.Windows.Forms;

namespace PracticMapsProject
{
    public partial class settings_Form : Form
    {
        private readonly CPMapUISettings config;
        public bool IsChanged;

        internal settings_Form(CPMapUISettings cfg)
        {
            InitializeComponent();
            config = cfg;
        }

        private void settings_Form_Shown(object sender, EventArgs e)
        {
            btMinDepthColor.BackColor = config.pointMinDepthColor;
            btMaxDepthColor.BackColor = config.pointMaxDepthColor;
            btPlaceColor.BackColor = config.placeColor;
            btSelectionColor.BackColor = config.selectionColor;
            
            btBadLowDepthColor.BackColor = config.pointBadLowDepthColor;
            btBadGreatDepthColor.BackColor = config.pointBadGreatDepthColor;

            tbMinDepth.Text = config.pointMinDepth.ToString();
            tbMaxDepth.Text = config.pointMaxDepth.ToString();
            tbPlaceSize.Text = config.placeSize.ToString();
            tbPointSize.Text = config.pointSize.ToString();
            tbSelectionWidth.Text = config.selectionWidth.ToString();
            tbBadLowDepth.Text = config.pointBadLowDepth.ToString();
            tbBadGreatDepth.Text = config.pointBadGreatDepth.ToString();

            if (config.showBadLowDepth)
            {
                cbLowDepth.CheckState = CheckState.Checked;
            }
            else
            {
                cbLowDepth.CheckState = CheckState.Unchecked;
            }

            if (config.showBadGreatDepth)
            {
                cbGreatDepth.CheckState = CheckState.Checked;
            }
            else
            {
                cbGreatDepth.CheckState = CheckState.Unchecked;
            }

            IsChanged = false;
        }

        private void btMinDepthColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = config.pointMinDepthColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btMinDepthColor.BackColor = colorDialog1.Color;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            config.pointMinDepthColor = btMinDepthColor.BackColor;
            config.pointMaxDepthColor = btMaxDepthColor.BackColor;
            config.placeColor = btPlaceColor.BackColor;
            config.selectionColor = btSelectionColor.BackColor;
            config.pointBadLowDepthColor = btBadLowDepthColor.BackColor;
            config.pointBadGreatDepthColor = btBadGreatDepthColor.BackColor;

            double d;
            int i;

            if (!double.TryParse(tbMinDepth.Text, out d))
            {
                MessageBox.Show("Некорректное значение мин. глубины");
                return;
            }
            if (!double.TryParse(tbMaxDepth.Text, out d))
            {
                MessageBox.Show("Некорректное значение макс. глубины");
                return;
            }

            if (double.Parse(tbMinDepth.Text) >= double.Parse(tbMaxDepth.Text))
            {
                MessageBox.Show("Минимальная глубина должна быть меньше максимальной");
                return;
            }

            if (!int.TryParse(tbPointSize.Text, out i))
            {
                MessageBox.Show("Некорректное значение размера точки");
                return;
            } else
                if (int.Parse(tbPointSize.Text) <= 0)
                {
                    MessageBox.Show("Некорректное значение размера точки");
                    return;
                }

            if (!int.TryParse(tbSelectionWidth.Text, out i))
            {
                MessageBox.Show("Некорректное значение толщины выделения");
                return;
            } else
                if (int.Parse(tbSelectionWidth.Text) <= 0)
                {
                    MessageBox.Show("Некорректная толщина выделения");
                    return;
                }

            if (!double.TryParse(tbBadLowDepth.Text, out d))
            {
                MessageBox.Show("Некорректное отмечаемой малой глубины");
                return;
            }

            if (!double.TryParse(tbBadGreatDepth.Text, out d))
            {
                MessageBox.Show("Некорректное отмечаемой большой глубины");
                return;
            }

            if (!int.TryParse(tbPlaceSize.Text, out i))
            {
                MessageBox.Show("Некорректное значение размера места");
                return;
            } else
                if (int.Parse(tbPlaceSize.Text) <= 0)
                {
                    MessageBox.Show("Некорректный размер места");
                    return;
                }

            config.pointMinDepth = double.Parse(tbMinDepth.Text);
            config.pointMaxDepth = double.Parse(tbMaxDepth.Text);
            config.pointSize = int.Parse(tbPointSize.Text);
            config.placeSize = int.Parse(tbPlaceSize.Text);
            config.selectionWidth = int.Parse(tbSelectionWidth.Text);
            config.showBadLowDepth = cbLowDepth.Checked;
            config.showBadGreatDepth = cbGreatDepth.Checked;
            config.showBadLowDepth = cbLowDepth.Checked;
            config.pointBadLowDepth = double.Parse(tbBadLowDepth.Text);
            config.pointBadGreatDepth = double.Parse(tbBadGreatDepth.Text);


            IsChanged = true;

            Close();
        }

        private void btMaxDepthColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = config.pointMaxDepthColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btMaxDepthColor.BackColor = colorDialog1.Color;
            }

        }

        private void btSelectionColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = config.selectionColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btSelectionColor.BackColor = colorDialog1.Color;
            }
        }

        private void btPlaceColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = config.placeColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btPlaceColor.BackColor = colorDialog1.Color;
            }
        }

        private void btBadLowDepthColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = config.pointBadLowDepthColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btBadLowDepthColor.BackColor = colorDialog1.Color;
            }

        }

        private void btBadGreatDepthColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = config.pointBadGreatDepthColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btBadGreatDepthColor.BackColor = colorDialog1.Color;
            }

        }
    }
}
