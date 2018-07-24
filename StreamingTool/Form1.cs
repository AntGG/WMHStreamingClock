using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Timers;
using System.Diagnostics;
using System.Threading;

namespace Streaming_Tool
{
    public partial class Form1 : Form
    {
        System.Timers.Timer OneSec;
        TimeSpan clockOne;
        TimeSpan clockTwo;
        Stopwatch watchOne;
        Stopwatch watchTwo;
        bool playerOneActive;
        bool clockPaused;
        object actionLock;
        int playerOneScore;
        int playerTwoScore;
        System.Timers.Timer FiveSec;
        bool boxConnected = false;
        string serialPort = "";

        SerialPort box;
        bool readyForInit = false;

        public Form1()
        {
            InitializeComponent();

            CreateLibrary();
            clockPaused = true;
            pauseButton.Text = "Start";
            ResetScores();
            actionLock = new object();
            SetUpPlayers();
            StartOneSecondTimer();
            StartBoxTimer();

            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                SwapClock();
            }
        }

        private void CreateLibrary()
        {
            Directory.CreateDirectory(@"C:\Library");
        }

        private void StartBoxTimer()
        {
            FiveSec = new System.Timers.Timer();
            FiveSec.SynchronizingObject = this;
            FiveSec.Interval = 5000; //Five seconds.
            FiveSec.AutoReset = false;
            FiveSec.Elapsed += FiveSec_Tick;
            FiveSec.Enabled = true;
        }

        private void FiveSec_Tick(object sender, EventArgs e)
        {
            FiveSec.Enabled = false;
            Thread setupThread = new Thread(new ThreadStart(SetUpBoxLink));
            setupThread.Start();
        }

        // the Serial Port detection routine 
        private void testSerialPort(object obj)
        {
            if (!(obj is string))
                return;
            string spName = obj as string;
            SerialPort sp = new SerialPort(spName);
            sp.BaudRate = 9600;
            sp.Parity = Parity.None;
            sp.StopBits = StopBits.One;
            sp.DataBits = 8;
            sp.Handshake = Handshake.None;
            sp.RtsEnable = true;

            try
            {
                sp.Open();
            }
            catch (Exception)
            {
                // users don't want to experience this
                return;
            }

            if (sp.IsOpen)
            {
                sp.Write("TEST");
                isSerialPortValid = true;

            }
            sp.Close();

        }

        // validity of serial port        
        private bool isSerialPortValid;

        // the callback function of button checks the serial ports
        private bool serialTest(string portName)
        {
            isSerialPortValid = false;
            Thread t = new Thread(new ParameterizedThreadStart(testSerialPort));
            t.Start(portName);
            Thread.Sleep(500); // wait and trink a tee for 500 ms
            t.Abort();

            return isSerialPortValid;
        }


        private void SetUpBoxLink()
        {
            if (!boxConnected)
            {
                bool boxFound = false;
                readyForInit = false;
                foreach (string portName in SerialPort.GetPortNames())
                {
                    try
                    {
                        serialPort = portName;
                        boxLabel.Text = "Looking on " + serialPort;

                        if (serialTest(serialPort))
                        {

                            box = new SerialPort(serialPort);

                            box.BaudRate = 9600;
                            box.Parity = Parity.None;
                            box.StopBits = StopBits.One;
                            box.DataBits = 8;
                            box.Handshake = Handshake.None;
                            box.RtsEnable = true;

                            //Give it half a second.
                            System.Threading.Thread.Sleep(1000);

                            box.Open();
                            System.Threading.Thread.Sleep(1000);

                            if (box.IsOpen)
                            {
                                connectBoxUpdate();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //Whatever.
                    }

                }
            }
            if (boxConnected)
            {
                //Link read event here.
                box.DataReceived += Box_DataReceived;
            }
            else
            {
                boxLabel.Text = "Box not found.";
                FiveSec.Enabled = true;
            }
        }

        private void connectBoxUpdate()
        {
            FiveSec.Enabled = false;
            try
            {
                //while (!readyForInit)
                //{
                //    box.Write("INIT");
                //    System.Threading.Thread.Sleep(100);
                //}

                //Initialise box.
                box.Write("SETUP");
                System.Threading.Thread.Sleep(200);

                //Send times.
                SendPlayerOneTime(clockOne.ClockDisplay(), true);
                System.Threading.Thread.Sleep(200);
                SendPlayerTwoTime(clockTwo.ClockDisplay(), true);
                System.Threading.Thread.Sleep(200);

                //Send score.
                SendPlayerOneScore(true);
                SendPlayerTwoScore(true);

                //Send active.
                SendActivePlayer(true);
                System.Threading.Thread.Sleep(200);
                //Send CP here.

                boxConnected = true;
                boxLabel.Text = "Box connected on " + serialPort + ".";

            }
            catch
            {
                disconnectBoxUpdate();
            }
        }

        private void disconnectBoxUpdate()
        {
            boxLabel.Text = "Box disconnected.";
            boxConnected = false;
            readyForInit = false;
            FiveSec.Enabled = true;
        }

        private void Box_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine();
            indata = indata.Trim();
            if (indata == "LEFT") { BoxPlayerOne(); }
            else if (indata == "RIGHT") { BoxPlayerTwo(); }
            else if (indata == "PAUSE") { BoxPause(); }
            else if (indata == "START") { BoxStart(); }
            else if (indata.StartsWith("LSCORE")) { BoxLeftScore(indata); }
            else if (indata.StartsWith("STARTING")) { readyForInit = true; }

            /*if (pauseButton.InvokeRequired)
            {
                this.pauseButton.BeginInvoke((MethodInvoker)delegate () { pauseButton.Text = indata; ; });
            }
            else
            {
                pauseButton.Text = indata;
            }*/
        }

        private void BoxLeftScore(string indata)
        {
            if (pauseButton.InvokeRequired)
            {
                this.pauseButton.BeginInvoke((MethodInvoker)delegate () { pauseButton.Text = indata.Substring(6); ; });
            }
            else
            {
                pauseButton.Text = indata;
            }
            //resetButton.Text = indata;
            //playerOneScore = int.Parse(indata.Substring(5));
            SendPlayerOneScore(false);
        }

        private void BoxPause()
        {
            if (!clockPaused)
            {
                PauseClock();
            }
        }

        private void BoxStart()
        {
            if (clockPaused)
            {
                StartClock();
            }
        }

        private void BoxPlayerTwo()
        {
            if (!playerOneActive)
            {
                SwapClock();
            }
        }

        private void BoxPlayerOne()
        {
            if (playerOneActive)
            {
                SwapClock();
            }
        }

        private void SetUpPlayers()
        {
            ResetClocks();
            playerOneActive = true;
        }

        private void ResetScores()
        {
            playerOneScore = 0;
            playerTwoScore = 0;
            using (System.IO.StreamWriter file =
    new System.IO.StreamWriter(@"C:\Library\score1.txt", false))
            {
                file.WriteLine(scoreLabel1.Text);
            }
            using (System.IO.StreamWriter file =
    new System.IO.StreamWriter(@"C:\Library\score2.txt", false))
            {
                file.WriteLine(scoreLabel1.Text);
            }
            scoreLabel1.Text = playerOneScore.ToString();
            scoreLabel2.Text = playerTwoScore.ToString();

            SendPlayerOneScore(false);
            SendPlayerTwoScore(false);
        }

        private void ResetClocks()
        {
            clockOne = new TimeSpan(0, 60, 0);
            clockTwo = new TimeSpan(0, 60, 0);
            watchOne = new Stopwatch();
            watchTwo = new Stopwatch();
            PauseClock();

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Library\clock.txt", false))
            {
                file.WriteLine(clockOne.ClockDisplay());
                clockLabel1.Text = clockOne.ClockDisplay();
                SendPlayerOneTime(clockOne.ClockDisplay(), false);
            }
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Library\clock2.txt", false))
            {
                file.WriteLine(clockTwo.ClockDisplay());
                clockLabel2.Text = clockOne.ClockDisplay();
                SendPlayerTwoTime(clockTwo.ClockDisplay(), false);
            }
        }

        private void StartOneSecondTimer()
        {
            OneSec = new System.Timers.Timer();
            OneSec.SynchronizingObject = this;
            OneSec.Interval = 100; //One second.
            OneSec.AutoReset = true;
            OneSec.Elapsed += OneSec_Tick;
        }

        private void SwapClock()
        {
            lock (actionLock)
            {
                playerOneActive = !playerOneActive;
            }
            if (playerOneActive)
            {
                if (clockLabel1.InvokeRequired)
                {
                    this.clockLabel1.BeginInvoke((MethodInvoker)delegate ()
                    {
                        watchTwo.Stop();
                        if (!clockPaused) { watchOne.Start(); }
                        clockLabel1.ForeColor = System.Drawing.Color.Black;
                        clockLabel2.ForeColor = System.Drawing.Color.Gray; ;
                    });
                }
                else
                {
                    watchTwo.Stop();
                    if (!clockPaused) { watchOne.Start(); }
                    clockLabel1.ForeColor = System.Drawing.Color.Black;
                    clockLabel2.ForeColor = System.Drawing.Color.Gray; ;
                }
            }
            else
            {
                if (clockLabel1.InvokeRequired)
                {
                    this.clockLabel1.BeginInvoke((MethodInvoker)delegate ()
                    {
                        watchOne.Stop();
                        if (!clockPaused) { watchTwo.Start(); }
                        clockLabel1.ForeColor = System.Drawing.Color.Gray;
                        clockLabel2.ForeColor = System.Drawing.Color.Black; ;
                    });
                }
                else
                {
                    watchOne.Stop();
                    if (!clockPaused) { watchTwo.Start(); }
                    clockLabel1.ForeColor = System.Drawing.Color.Gray;
                    clockLabel2.ForeColor = System.Drawing.Color.Black; ;
                }
            }
            SendActivePlayer(false);
        }

        private void SendActivePlayer(bool manual)
        {
            if (boxConnected || manual)
            {
                try
                {
                    if (playerOneActive)
                    {
                        box.Write("1P/");
                    }
                    else
                    {
                        box.Write("2P/");
                    }
                }
                catch
                {
                    disconnectBoxUpdate();
                }
            }
        }

        private void SendPlayerOneScore(bool manual)
        {
            if (boxConnected || manual)
            {
                try
                {
                    box.Write("1S-" + playerOneScore + "/");
                }
                catch
                {
                    disconnectBoxUpdate();
                }
            }
        }

        private void SendPlayerTwoScore(bool manual)
        {
            if (boxConnected || manual)
            {
                try
                {
                    box.Write("2S-" + playerTwoScore + "/");
                }
                catch
                {
                    disconnectBoxUpdate();
                }
            }
        }

        private void SendBeep()
        {
            if (boxConnected)
            {
                try
                {
                    box.Write("BEEP/");
                }
                catch
                {
                    disconnectBoxUpdate();
                }
            }
        }

        private void SendPlayerOneTime(string time, bool manual)
        {
            if (boxConnected || manual)
            {
                try
                {
                    string serialTime = "1T-" + time + "/";
                    box.Write(serialTime);
                }
                catch
                {
                    disconnectBoxUpdate();
                }
            }
        }

        private void SendPlayerTwoTime(string time, bool manual)
        {
            if (boxConnected || manual)
            {
                try
                {
                    string serialTime = "2T-" + time + "/";
                    box.Write(serialTime);
                }
                catch
                {
                    disconnectBoxUpdate();
                }
            }
        }

        private void OneSec_Tick(object sender, EventArgs e)
        {
            lock (actionLock)
            {
                if (playerOneActive)
                {
                    int clockSec = clockOne.Seconds;
                    clockOne = new TimeSpan(0, 60, 0).Subtract(watchOne.Elapsed);
                    if (clockSec != clockOne.Seconds)
                    {
                        using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@"C:\Library\clock.txt", false))
                        {
                            file.WriteLine(clockOne.ClockDisplay());
                            clockLabel1.Text = clockOne.ClockDisplay();
                            SendPlayerOneTime(clockOne.ClockDisplay(), false);
                        }
                    }
                }
                else
                {
                    int clockSec = clockTwo.Seconds;
                    clockTwo = new TimeSpan(0, 60, 0).Subtract(watchTwo.Elapsed);
                    if (clockSec != clockTwo.Seconds)
                    {
                        using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@"C:\Library\clock2.txt", false))
                        {
                            file.WriteLine(clockTwo.ClockDisplay());
                            clockLabel2.Text = clockTwo.ClockDisplay();
                            SendPlayerTwoTime(clockTwo.ClockDisplay(), false);
                        }
                    }
                }
                if (clockOne.Equals(new TimeSpan(0)) || clockTwo.Equals(new TimeSpan(0)))
                {
                    PauseClock();
                }
            }
        }

        private void StopMatch()
        {
            //TODO: Stuff.
            //Stop timers.
            //Disable buttons.
            //Enable reset.
            PauseClock();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SwapClock();
            flipClockButton.Select();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void StopwatchPause()
        {
            watchOne.Stop();
            watchTwo.Stop();
        }

        private void StopwatchStart()
        {
            if (playerOneActive)
            {
                watchOne.Start();
            }
            else
            {
                watchTwo.Start();
            }
        }

        private void PauseClock()
        {
            if (!clockPaused)
            {
                if (pauseButton.InvokeRequired)
                {
                    this.pauseButton.BeginInvoke((MethodInvoker)delegate ()
                    {
                        StopwatchPause();
                        pauseButton.Text = "Resume";
                        OneSec.Enabled = false; ;
                        resetButton.Enabled = true;
                    });
                }
                else
                {
                    StopwatchPause();
                    pauseButton.Text = "Resume";
                    OneSec.Enabled = false;
                    resetButton.Enabled = true;
                }
                clockPaused = true;
            }
        }

        private void StartClock()
        {
            if (clockPaused)
            {
                if (pauseButton.InvokeRequired)
                {
                    this.pauseButton.BeginInvoke((MethodInvoker)delegate ()
                    {
                        StopwatchStart();
                        pauseButton.Text = "Pause"; OneSec.Enabled = true; resetButton.Enabled = false; ;
                    });
                }
                else
                {
                    StopwatchStart();
                    pauseButton.Text = "Pause";
                    OneSec.Enabled = true;
                    resetButton.Enabled = false;
                }
                clockPaused = false;
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (clockPaused)
            {
                StartClock();
            }
            else
            {
                PauseClock();
            }
            flipClockButton.Select();
        }

        private void scoreButton1_Click(object sender, EventArgs e)
        {
            if (playerOneScore > 0) { playerOneScore--; }
            scoreLabel1.Text = playerOneScore.ToString();
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Library\score1.txt", false))
            {
                file.WriteLine(scoreLabel1.Text);
            }
            SendPlayerOneScore(false);
            flipClockButton.Select();
        }

        private void scoreButton2_Click(object sender, EventArgs e)
        {
            playerTwoScore++;
            scoreLabel2.Text = playerTwoScore.ToString();
            using (System.IO.StreamWriter file =
    new System.IO.StreamWriter(@"C:\Library\score2.txt", false))
            {
                file.WriteLine(scoreLabel2.Text);
            }
            SendPlayerTwoScore(false);
            flipClockButton.Select();
        }

        private void scoreButton1Plus_Click(object sender, EventArgs e)
        {
            playerOneScore++;
            scoreLabel1.Text = playerOneScore.ToString();
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Library\score1.txt", false))
            {
                file.WriteLine(scoreLabel1.Text);
            }
            SendPlayerOneScore(false);
            flipClockButton.Select();
        }

        private void scoreButton2Minus_Click(object sender, EventArgs e)
        {
            if (playerTwoScore > 0) { playerTwoScore--; }
            scoreLabel2.Text = playerTwoScore.ToString();
            using (System.IO.StreamWriter file =
    new System.IO.StreamWriter(@"C:\Library\score2.txt", false))
            {
                file.WriteLine(scoreLabel2.Text);
            }
            SendPlayerTwoScore(false);
            flipClockButton.Select();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetClocks();
            ResetScores();
            flipClockButton.Select();
        }
    }
}
