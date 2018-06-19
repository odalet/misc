using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KillNicelyCmdProg
{
    public partial class Form1 : Form
    {
        private bool _experimentRunning;
        private int PID;
        private Timer DelayTimer;

        public Form1()
        {
            InitializeComponent();
        }

        private void TestOne_Click(object sender, EventArgs e)
        {
            //This method is a mix of .Net and pinvoke. It stops the process by closing its command window, similar to clicking
            //the 'X' button in the upper right corner. This generates a Ctrl-C event to all programs, registered with that 
            //console.
            //The downside of this method is that it briefly shows the commend window when starting and when stopping the process.

            EnableButtons(false);

            Process proc = Experiments.StartProgramUsingDotNet(Output, false);
            IntPtr windowHandle = Experiments.FindWindowHandleFromProcessObjectWithVisibleWindow(proc);
            Experiments.HideCommandWindowUsingPInvoke(windowHandle);

            PID = proc.Id;

            DelayTimer = new Timer();
            DelayTimer.Interval = 6000;
            DelayTimer.Tick += (o, args) =>
                {
                    DelayTimer.Enabled = false;

                    Output.AppendText("Stopping..." + Environment.NewLine);
                    Experiments.ShowCommandWindowUsingPInvoke(windowHandle);
                    Experiments.StopProgramUsingProcessObjectWithVisibleMainWindow(proc);

                    EnableButtons(true);
                };
            DelayTimer.Enabled = true;
        }

        private void TestTwo_Click(object sender, EventArgs e)
        {
            EnableButtons(false);

            int pid = Experiments.StartProgramWithoutWindowUsingPinvoke(Output);
            IntPtr windowHandle = Experiments.FindWindowHandleFromPid(pid);

            PID = pid;

            DelayTimer = new Timer();
            DelayTimer.Interval = 6000;
            DelayTimer.Tick += (o, args) =>
                {
                    DelayTimer.Enabled = false;

                    Output.AppendText("Stopping..." + Environment.NewLine);
                    Experiments.StopProgramWithInvisibleWindowUsingPinvoke(windowHandle);

                    EnableButtons(true);
                };
            DelayTimer.Enabled = true;
        }

        private void TestThree_Click(object sender, EventArgs e)
        {
            //This is the most brutal method, which relies entirely on .Net functionality. When stopping the process, it simply kills
            //it, without giving it any chance to clean up. Note that "ping" command does not display any summary when it is stopped
            //in this fashion.

            EnableButtons(false);

            Process proc = Experiments.StartProgramUsingDotNet(Output, true);

            PID = proc.Id;

            DelayTimer = new Timer();
            DelayTimer.Interval = 6000;
            DelayTimer.Tick += (o, args) =>
                {
                    DelayTimer.Enabled = false;

                    Output.AppendText("Stopping..." + Environment.NewLine);
                    Experiments.StopProgramByKillingIt(proc);

                    EnableButtons(true);
                };
            DelayTimer.Enabled = true;
        }

        private void TestFour_Click(object sender, EventArgs e)
        {
            EnableButtons(false);

            Process proc = Experiments.StartProgramUsingDotNet(Output, true);

            PID = proc.Id;

            DelayTimer = new Timer();
            DelayTimer.Interval = 6000;
            DelayTimer.Tick += (o, args) =>
                {
                    DelayTimer.Enabled = false;

                    Output.AppendText("Stopping..." + Environment.NewLine);

                    Experiments.StopProgramByAttachingToItsConsoleAndIssuingCtrlCEvent(proc);

                    EnableButtons(true);
                };
            DelayTimer.Enabled = true;
        }

        private void EnableButtons(bool state)
        {
            TestOne.Enabled = state;
            TestTwo.Enabled = state;
            TestThree.Enabled = state;
            TestFour.Enabled = state;
            _experimentRunning = !state;
        }

        private void panic_Click(object sender, EventArgs e)
        {
            try
            {
                DelayTimer.Stop();
                DelayTimer.Dispose();
                DelayTimer = null;
                Process p = Process.GetProcessById(PID);
                p.Kill();
                EnableButtons(true);
            }
            catch { }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _experimentRunning;
        }
    }
}
