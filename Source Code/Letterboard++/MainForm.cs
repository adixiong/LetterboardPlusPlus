using Bunifu.Framework.UI;
using Letterboard__.Helpers;
using Letterboard__.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Letterboard__
{
    public partial class MainForm : Form
    {
        private BunifuThinButton2 CurrentKeyboardButton;

        public MainForm()
        {
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnKeyboard_MouseHover(object sender, EventArgs e)
        {
            if (CurrentKeyboardButton != null)
            {
                if (CurrentKeyboardButton.Tag == "suggestion")
                {
                    CurrentKeyboardButton.Font = new Font("Century Gothic", 12, FontStyle.Regular);
                }
                else
                {
                    CurrentKeyboardButton.Font = new Font("Century Gothic", 18, FontStyle.Regular);
                }
                tmrKeyboard.Stop();
                timer1.Stop();
            }

            BunifuThinButton2 button = sender as BunifuThinButton2;
            button.Font = new Font("Century Gothic", 18, FontStyle.Bold);
            CurrentKeyboardButton = button;
            pbCommon.Value = 0;
            tmrKeyboard.Start();
            timer1.Start();
        }

        private void tmrKeyboard_Tick(object sender, EventArgs e)
        {
            if (CurrentKeyboardButton.ButtonText == "123")
            {
                pnlAlphabets.Visible = false;
                pnlNumbersAndSymbols.Visible = true;
            }
            else if (CurrentKeyboardButton.ButtonText == "Abc")
            {
                pnlAlphabets.Visible = true;
                pnlNumbersAndSymbols.Visible = false;
            }
            else if (CurrentKeyboardButton.ButtonText == "[ SPACE ]")
            {
                txtBoard.Text += " ";
            }
            else if (CurrentKeyboardButton.ButtonText == "[ BACK ]")
            {
                if (txtBoard.Text.Length > 0)
                {
                    txtBoard.Text = txtBoard.Text.Remove(txtBoard.Text.Length - 1, 1);
                }
            }
            else if (CurrentKeyboardButton.ButtonText == "sqrt")
            {
                txtBoard.Text += "√";
            }
            else if (CurrentKeyboardButton.Tag == "suggestion")
            {
                var words = txtBoard.Text.Split(' ').ToList();
                words.Remove(words.LastOrDefault());
                words.Add(CurrentKeyboardButton.ButtonText);
                txtBoard.Text = string.Join(" ", words);
            }
            else
            {
                txtBoard.Text += CurrentKeyboardButton.ButtonText.ToLower();
                LoadSuggestions();
            }

            pbCommon.Value = 0;
            tmrKeyboard.Stop();
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbCommon.Value++;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            DialogResult dialogResult = settingsForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                LoadSettings();
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = saveFileDialog1.ShowDialog();
            if(dialogResult==DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName + ".txt", txtBoard.Text);
                MessageBox.Show("Text has been successfully saved in file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadSettings()
        {
            Settings settings = SettingsHelper.GetSetting();
            tmrKeyboard.Interval = settings.KeyboardDelayTime;
            txtBoard.ForeColor = Color.FromName(settings.Color);
            foreach(var control in pnlAlphabets.Controls)
            {
                (control as BunifuThinButton2).ForeColor = Color.FromName(settings.Color);
                (control as BunifuThinButton2).ActiveForecolor = Color.FromName(settings.Color);
                (control as BunifuThinButton2).IdleForecolor = Color.FromName(settings.Color);
            }
            foreach (var control in pnlNumbersAndSymbols.Controls)
            {
                (control as BunifuThinButton2).ForeColor = Color.FromName(settings.Color);
                (control as BunifuThinButton2).ActiveForecolor = Color.FromName(settings.Color);
                (control as BunifuThinButton2).IdleForecolor = Color.FromName(settings.Color);
            }
            foreach (var control in pnlSuggestions.Controls)
            {
                (control as BunifuThinButton2).ForeColor = Color.FromName(settings.Color);
                (control as BunifuThinButton2).ActiveForecolor = Color.FromName(settings.Color);
                (control as BunifuThinButton2).IdleForecolor = Color.FromName(settings.Color);
            }
            pbCommon.ProgressColor = Color.FromName(settings.Color);
        }

        private async Task LoadSuggestions()
        {
            string partialWord = txtBoard.Text.Split(' ').LastOrDefault();
            var suggestions = await SuggestionsHelper.GetSuggestions(partialWord.ToLower());
            for (int i = 0; i < suggestions.Count; i++)
            {
                (pnlSuggestions.Controls[i] as BunifuThinButton2).ButtonText = suggestions[i];
            }
        }
    }
}
