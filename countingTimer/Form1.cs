using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace countingTimer
{
    public partial class countdownTimer : Form
    {
        // VARIABLES
        DateTime stasisTime = DateTime.Now; // Gets the current time immediately
        int timeLeft = 60; 
        int timeSaved = 0; // For saving & loading
        int errorMessageCountdown = -1; // Error handling message screen time
        bool Error = false;

        public countdownTimer()
        {
            InitializeComponent();
            lblDay.Text = stasisTime.ToString("d/MM/yy");
            lblTime.Text = stasisTime.ToString("HH:mm:ss");
            timer1.Start();
            // Gets the immediate time values so the displays have something & starts the SECOND timer
            // .. So the latter can update in real time
        }
        private void timer1_Tick(object sender, EventArgs e)// Note: 1000 Ticks is a second
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                progressBar.Value++;
                timerLabel.Text = timeLeft + " SECONDS";
            }
            else {
                timer.Stop();
                timerLabel.Text = "TIME IS UP";
            }
        }
        private void timer1_Tick_1(object sender, EventArgs e)
            // timer number 2, one that doesn't need the "start" button to tick down.
        {
            DateTime constantTime = DateTime.Now;
            lblTime.Text = constantTime.ToString("HH:mm:ss"); // it will constantly be updating
            if (Error == true)
            {
                lblError.Text = "AN ERROR HAS OCCURED - THE TIMER HAS BEEN RESET";
                Thread.Sleep(5500);
                lblError.Text = null;
                Error = false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            timer.Stop();
            timeLeft = 60;
            timerLabel.Text = timeLeft + " SECONDS";

            progressBar.Value = 0;
            progressBar.Maximum = 60;

            // Reset button. Doesn't impact the save feature.
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer.Start(); // Visual studio accounts for a lot of the logic required for time
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void minus_Click(object sender, EventArgs e)
        {
            try // Time can't deal with negatives /This error handling catches it
            {
                timeLeft -= 5;
                progressBar.Maximum = timeLeft;
                progressBar.Value = 0;

                if (timeLeft <= 5)
                {
                    timeLeft = 5;
                    progressBar.Maximum = 5;
                }
                timerLabel.Text = timeLeft.ToString() + " SECONDS";
            }
            catch {
                // Reset...
                timer.Stop();
                timeLeft = 60;
                timerLabel.Text = timeLeft + " SECONDS";
                progressBar.Value = 0;
                progressBar.Maximum = 60;
                // Error message...
                Error = true;
                lblError.Text = "AN ERROR HAS OCCURED - THE TIMER HAS BEEN RESET";

            }
        }

        private void plus_Click(object sender, EventArgs e)
        {
            timeLeft += 5;
            progressBar.Maximum = timeLeft;
            progressBar.Value = 0;
            if (timeLeft >= 1000) { 
                timeLeft = 1000;
            }
            timerLabel.Text = timeLeft.ToString() + " SECONDS";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            timeSaved = timeLeft;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            timer.Stop();
            progressBar.Value = 0;
            progressBar.Maximum = timeSaved;
            timeLeft = timeSaved;
            timerLabel.Text = timeLeft.ToString() + " SECONDS";
        }
    }
}
