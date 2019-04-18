using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioSplit
{
    public partial class FormTemplateHelp : Form
    {
        public Settings Settings { get; set; }

        public FormTemplateHelp()
        {
            InitializeComponent();
        }

        private void FormTemplateHelp_Load(object sender, EventArgs e)
        {
            if (Settings != null)
            {
                if (Settings.FormTemplateHelpLocation.X != 0 ||
                      Settings.FormTemplateHelpLocation.Y != 0)
                {
                    this.Location = Settings.FormTemplateHelpLocation;
                }
                else
                {
                    // No starting location. Try to position to the right of the main form.
                     this.Location = new Point( Settings.FormMainLocation.X + Settings.FormMainSize.Width,
                        Settings.FormMainLocation.Y);
                 }
                if (Settings.FormTemplateHelpSize.Height != 0 ||
                      Settings.FormTemplateHelpSize.Width != 0)
                {
                    this.Size = Settings.FormTemplateHelpSize;
                }
                // If form is not entirely visible, move the window to the center of the main form.
                // There is no parent of the form (you can't set it), but this works, so apparently it
                // uses owner.
                if (!IsOnScreen(this))
                {
                    CenterToParent();
                }
            }

            richText.SetInnerMargins(10, 10, 10, 10);
            richText.Rtf = AudioSplit.Properties.Resources.FileNameTemplateHelp;
        }

        private bool IsOnScreen(Form form)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                Rectangle formRectangle = new Rectangle(form.Left, form.Top,
                                                         form.Width, form.Height);

                if (screen.WorkingArea.Contains(formRectangle))
                {
                    return true;
                }
            }

            return false;
        }
        private void FormTemplateHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings != null)
            {
                Settings.FormTemplateHelpLocation = this.Location;
                Settings.FormTemplateHelpSize = this.Size;
            }
        }
    }
}
