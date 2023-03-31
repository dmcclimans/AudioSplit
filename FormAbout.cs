using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CenteredMessageBox;

namespace AudioSplit
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text = String.Format("Version {0}", AssemblyVersion);
            LinkLabel.Link link = new LinkLabel.Link
            {
                LinkData = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "License.txt")
            };
            linkLabel1.Links.Add(link);
            CenterToParent();
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Send the URL to the operating system.
                Process.Start(e.Link.LinkData as string);
            }
            catch (Exception)
            {
                // Might be that no app is associated with .txt. 
                // Sometimes Windows will display a dialog asking you to select the program to use.
                // Sometimes I get an exception. Don't really understand this.
                // Anyway, try running notepad.
                try
                {
                    Process.Start("notepad.exe", e.Link.LinkData as string);
                }
                catch (Exception exc)
                {
                    MessageBoxEx.Show(this, exc.Message);
                }
            }
        }
    }
}
