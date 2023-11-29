using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Azkar
{
    public partial class frmMain : Form
    {
        struct stNotiData
        {
            public string Message;
            public int TimeMS;
        }
        stNotiData NotiData = new stNotiData();
        public frmMain()
        {
            InitializeComponent();
        }

        private void cbUserChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            NotiData.Message = cbUserChoice.Text;
            btnDone.Enabled = true;

        }

        private void nudNumberOfMinutes_ValueChanged(object sender, EventArgs e)
        {
            NotiData.TimeMS = Convert.ToInt32(nudNumberOfMinutes.Value) * 60 * 1000;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            NotiData.Message = "";
            nudNumberOfMinutes.Value = 1;
            nudNumberOfMinutes.Enabled = true;

            cbUserChoice.SelectedIndex = -1;
            cbUserChoice.Enabled = true;

            lblTimeDown.Text = "00:00:00";
            timerTimeDown.Enabled = false;
            timer1.Enabled = false;
        }
        void FillTimeSpan()
        {
            int Hours = Convert.ToInt32(nudNumberOfMinutes.Value) / 60;
            int Minutes = Convert.ToInt32(nudNumberOfMinutes.Value) % 60;
            ts = new TimeSpan(Hours, Minutes, 0);
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            if (cbUserChoice.SelectedIndex == -1)
            {
                MessageBox.Show("You have to chose an option", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            nudNumberOfMinutes.Enabled = false;
            cbUserChoice.Enabled = false;
            
            FillTimeSpan();
            timerTimeDown.Enabled = true;

            timer1.Enabled = true;
            timer1.Interval = NotiData.TimeMS;
            
        }

        string NotificationMessage()
        {
            if(NotiData.Message != "عشوائي")
                return NotiData.Message;

            Random rd = new Random();

            string Message = cbUserChoice.Items[rd.Next(0, cbUserChoice.Items.Count - 1)].ToString();

            return Message;

        }

        void SendNotifaction()
        {
            notifyIcon1.Icon = SystemIcons.Information;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Azkar";
            notifyIcon1.BalloonTipText = NotificationMessage();
            notifyIcon1.ShowBalloonTip(1000);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendNotifaction();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            NotiData.TimeMS = 60 * 1000;
        }




        //Time Down


        TimeSpan ts;
        void UpdateTimeDown()
        {
            lblTimeDown.Text = ts.ToString();
            ts -= new TimeSpan(0,0, 1);
            if (ts == new TimeSpan(0, 0, 0))
                FillTimeSpan();
        }

        private void timerTimeDown_Tick(object sender, EventArgs e)
        {
            UpdateTimeDown();
        }
    }
}
