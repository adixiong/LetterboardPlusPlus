using Letterboard__.Helpers;
using Letterboard__.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Settings = Letterboard__.Models.Settings;

namespace Letterboard__
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            foreach (Color color in new ColorConverter().GetStandardValues())
            {
                cmbColor.Items.Add(color.Name);
            }

            Settings settings = SettingsHelper.GetSetting();
            cmbKeyboardDelayTime.SelectedItem = settings.KeyboardDelayTime.ToString() + "/ms";
            cmbColor.SelectedItem = settings.Color;
            pnlColor.BackColor = Color.FromName(settings.Color);
            btnSave.BackColor = Color.FromName(settings.Color);
            btnSave.IdleFillColor = Color.FromName(settings.Color);
            btnSave.IdleBorderColor = Color.FromName(settings.Color);
            btnSave.onHoverState.FillColor = Color.FromName(settings.Color);
            btnSave.onHoverState.BorderColor = Color.FromName(settings.Color);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings()
            {
                KeyboardDelayTime = Convert.ToInt32(cmbKeyboardDelayTime.SelectedItem.ToString().Replace("/ms", "")),
                Color = cmbColor.SelectedItem.ToString()
            };
            SettingsHelper.Save(settings);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlColor.BackColor = Color.FromName(cmbColor.SelectedItem.ToString());
        }
    }
}
