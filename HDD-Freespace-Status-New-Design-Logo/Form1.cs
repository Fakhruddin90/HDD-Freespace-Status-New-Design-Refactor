using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HDD_Freespace_Status_Design_1
{
    public partial class Form1 : Form
    {
        #region Drop shadow code
        /// Drop Shadow Code

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        ////////////////////////////////////////////////////////////////////////////////////
        #endregion
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void ceLearningToggle1_Click(object sender, EventArgs e)
        {
            if (ceLearningToggle1.IsOn == false)
            {
                this.Opacity = 1.0;
                Console.WriteLine(this.Opacity);
            }
            else if (ceLearningToggle1.IsOn == true)
            {
                this.Opacity = 0.7;
                Console.WriteLine(this.Opacity);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                this.TopMost = true;
            }
            else if (checkBox1.CheckState == CheckState.Unchecked)
            {
                this.TopMost = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            label1.Text = dt.ToString("yyyy-MM-dd");
            label2.Text = dt.ToString("hh:mm:ss tt");

            Storage.StorageDrive("C", CStorage);
            Storage.StorageDrive("D", DStorage);
            Storage.StorageDrive("E", EStorage);
            Storage.StorageDrive("F", FStorage);

            Console.WriteLine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DriveInfo dDrive = new DriveInfo("D");
            string C = "";
            if (dDrive.IsReady)
            {
                double freeSpacePerc = (dDrive.AvailableFreeSpace / (float)dDrive.TotalSize) * 100;
                C = String.Format("{0:0.00}%", freeSpacePerc);
            }
            else
            {
                C = "NA";
            }

            Logs.CSVLogs(C);
        }
    }
}