/**
 * This file is part of TwitchPlays.
 * 
 * Copyright (C) 2014 Ash. Katzenbaer
 * All Rights Reserved.
 * 
 * @github im420blaziken
 *  
 * TwitchPlays is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * TwitchPlays is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with TwitchPlays.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysTimers = System.Timers;

using System.Diagnostics;
using System.Runtime.InteropServices;

using vJoyInterfaceWrap;

namespace TwitchPlays
{
    public partial class LogForm : Form
    {
        

        // Declaring one joystick (Device id 1) and a position structure. 
        static public vJoy joystick;
        static public vJoy.JoystickState iReport;
        static public uint id = 1;

        bool _canSetGoal = true;
        bool _LagNoticeEnabled = false;
        private int voteCount = 0;
        public int VoteCount
        {
            get
            {
                return voteCount;
            }
        }

        string _defaultSaveDirectory;
        public LogForm(string defaultSaveDirectory)
        {
            InitializeComponent();
            _defaultSaveDirectory = defaultSaveDirectory;
            IntPtr _temp = this.Handle; // force creation of handle
        }

        private void AttachPokemonParty(ref PokemonParty f)
        {
            if (f != null)
            {
                f.Location = new Point(this.Location.X - f.Width, this.Location.Y);
                f.BringToFront();
            }
        }

        private PokemonParty module_party;
        private void LogForm_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(400, 100);
            // Load Modules
            //// PokemonParty
            module_party = new PokemonParty(_defaultSaveDirectory);
            module_party.Show();
            AttachPokemonParty(ref module_party);

            Initialize_vJoy(); // Create Virtual Joystick

            // Defaults
            voteBox.Items.Clear();
            chatBox.Items.Clear();
            double _maxdelay;
            if (double.TryParse(txtMaxDelay.Text, out _maxdelay)) Program.MaxDelay = _maxdelay;
            this.LagNoticeEnabled = false;
            this.InputEnabled = true;
            this.lblVoteResult.Text = "";

            // Timespan
            Thread clock_thread = new Thread(() => Tick());
            clock_thread.IsBackground = true;
            clock_thread.Start();

            Thread mode_thread = new Thread(() => CheckModeVotes());
            mode_thread.IsBackground = true;
            mode_thread.Start();

            Thread nominee_thread = new Thread(() => SortNominees());
            nominee_thread.IsBackground = true;
            nominee_thread.Start();

            // Cache Timer
            Thread cache_thread = new Thread(() => CacheTick());
            cache_thread.IsBackground = true;
            cache_thread.Start();

            // Vote listener
            Thread thread = new Thread(() => TwitchPlays.Program.ConnectIRC(this));
            thread.IsBackground = true;
            thread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program._shouldExit = true;
            joystick.RelinquishVJD(id);
        }

        public bool LagNoticeEnabled
        {
            get
            {
                return _LagNoticeEnabled;
            }
            set
            {
                _LagNoticeEnabled = value;
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(
                        () =>
                        {
                            if (lblAlert.Visible != value || pbAlert32.Visible != value)
                            {
                                lblAlert.Visible = value;
                                pbAlert32.Visible = value;
                            }
                        })
                    );
                }
                else if (Program.IsMainThread)
                {
                    if (lblAlert.Visible != value || pbAlert32.Visible != value)
                    {
                        lblAlert.Visible = value;
                        pbAlert32.Visible = value;
                    }
                }
            }
        }

        private bool _inputEnabled = true;
        public bool InputEnabled
        {
            get
            {
                return _inputEnabled;
            }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(
                        () =>
                        {
                            _inputEnabled = value;
                            if (lblInputAlert.Visible == value || pbInputAlert32.Visible == value || cbDisableVoting.Checked == value)
                            {
                                lblInputAlert.Visible = !value;
                                pbInputAlert32.Visible = !value;
                                cbDisableVoting.Checked = !value;
                            }
                        })
                    );
                }
                else if (Program.IsMainThread)
                {
                    _inputEnabled = value;
                    if (lblInputAlert.Visible == value || pbInputAlert32.Visible == value || cbDisableVoting.Checked == value)
                    {
                        lblInputAlert.Visible = !value;
                        pbInputAlert32.Visible = !value;
                        cbDisableVoting.Checked = !value;
                    }
                }
            }
        }

        private bool _isVotingOpen = false;
        public void StartVote()
        {
            double durationOffset = 1.0;
            Double.TryParse(txtDurationOffset.Text, out durationOffset);
            Program.DurationOffset = durationOffset;
            Program.TallyTime = DateTime.Now.AddSeconds(Program.VoteDuration + durationOffset);
            _isVotingOpen = false;
            _wasTallySet = true;
        }

        private bool _wasTallySet = false;
        private void Tick()
        {
            while (true)
            {
                Thread.Sleep(75);

                // UserCount
                //SetUserCount(Users.All.Count);

                // Tallying
                bool tallied = false;
                bool winnerBool = false;
                bool isPopularVote = false;
                Nominee winner = null;
                if ((Program.TallyTime - DateTime.Now).TotalMilliseconds < 0 && _wasTallySet) // Perform Nominee
                {
                    _wasTallySet = false;
                    tallied = true;

                    List <Nominee> nominees = Program.SortNominees();
                    winner = (nominees.Count >= 1) ? nominees[0] : null;
                    Nominee runnerup = (nominees.Count >= 2) ? nominees[1] : null;
                    if (winner == null) goto _goto_not_voting;

                    if (winner.Votes > 0)
                    {
                        winnerBool = true;
                        if (runnerup != null && winner.Votes == runnerup.Votes) isPopularVote = false;
                        else
                        {
                            isPopularVote = true;

                            Thread vJoyThread = new Thread(() =>  vJoySendNominee(winner));
                            vJoyThread.IsBackground = true;
                            vJoyThread.Start();
                        }
                        Program.ClearNominees();
                    }
                    else
                    {
                        if ((DateTime.Now - Program.LastInteraction).TotalSeconds > 3.0) // Check for Twitch Lag
                            LagNoticeEnabled = true;
                    }
                }

            _goto_not_voting:
                if (this.InvokeRequired)
                {
                    try
                    {
                        this.Invoke(new Action(
                            () =>
                            {
                                lblTimespan.Text = (Program.RunStart - DateTime.Now).ToString(@"dh':'mm':'ss");
                                //lblTimespan.Text = (DateTime.Now - Program.RunStart).ToString(@"d'd 'h'h 'm'm 's's'");
                                if (Program.TallyTime > DateTime.Now) // Counting down
                                {
                                    lblVoteClock.Text = Program.VoteSpan.ToString(@"ss\.fff's left'");

                                    double voting_time = Program.VoteDuration + Program.DurationOffset - Program.LagPropagationDelay;
                                    if (Program.IsVotingClosed)
                                    {
                                        if (lblVoteClock.ForeColor != Color.Silver)
                                        {
                                            lblVoteClock.ForeColor = Color.Silver;
                                            voteBox.ForeColor = Color.Silver;
                                        }
                                    }
                                    else if (Program.VoteSpan.TotalSeconds <= voting_time * 1.0 / 3.0)
                                    {
                                        if (lblVoteClock.ForeColor != Color.Crimson) lblVoteClock.ForeColor = Color.Crimson;
                                    }
                                    else if (Program.VoteSpan.TotalSeconds <= voting_time * 2.0 / 3.0)
                                    {
                                        if (lblVoteClock.ForeColor != Color.Tomato) lblVoteClock.ForeColor = Color.Tomato;
                                    }
                                    else
                                    {
                                        if (lblVoteClock.ForeColor != Color.White)
                                        {
                                            lblVoteClock.ForeColor = Color.White;
                                            voteBox.ForeColor = Color.White;
                                        }
                                    }

                                    if (!_isVotingOpen && !Program.IsVotingClosed)
                                    {
                                        _isVotingOpen = true;
                                        //Program.SendIRCMessage("++ " + lblPollNum.Text + " is now open!");
                                    }
                                }
                                else // Zero
                                {
                                    lblVoteClock.Text = "--.---s left";
                                    lblVoteClock.ForeColor = Color.White;
                                    if (Program.GameMode == User.ModeType.DEMOCRACY) StartVote();
                                }

                                if (tallied && Program.GameMode == User.ModeType.DEMOCRACY)
                                {
                                    if (winnerBool)
                                    {
                                        if (isPopularVote) lblVoteResult.Text = "WINNER: " + winner.Name;
                                        else lblVoteResult.Text = "TIE, NO ACTION";
                                    }
                                    else lblVoteResult.Text = "NO VOTES";
                                    Program.SendIRCNotice(lblPollNum.Text + " " + lblVoteResult.Text);

                                    if (voteCount == 999) voteCount = -1;
                                    lblPollNum.Text = "Poll #" + (++voteCount).ToString().PadLeft(3, '0');
                                }
                            })
                        );
                    }
                    catch (Exception) { }
                }
                else if (Program.IsMainThread)
                {
                    lblTimespan.Text = (Program.RunStart - DateTime.Now).ToString(@"dh':'mm':'ss");
                    //lblTimespan.Text = (DateTime.Now - Program.RunStart).ToString(@"d'd 'h'h 'm'm 's's'");
                    if (Program.TallyTime > DateTime.Now) // Counting down
                    {
                        lblVoteClock.Text = Program.VoteSpan.ToString(@"ss\.fff's left'");

                        double voting_time = Program.VoteDuration + Program.DurationOffset - Program.LagPropagationDelay;
                        if (Program.IsVotingClosed)
                        {
                            if (lblVoteClock.ForeColor != Color.Silver)
                            {
                                lblVoteClock.ForeColor = Color.Silver;
                                voteBox.ForeColor = Color.Silver;
                            }
                        }
                        else if (Program.VoteSpan.TotalSeconds <= voting_time * 1.0 / 3.0)
                        {
                            if (lblVoteClock.ForeColor != Color.Crimson) lblVoteClock.ForeColor = Color.Crimson;
                        }
                        else if (Program.VoteSpan.TotalSeconds <= voting_time * 2.0 / 3.0)
                        {
                            if (lblVoteClock.ForeColor != Color.Tomato) lblVoteClock.ForeColor = Color.Tomato;
                        }
                        else
                        {
                            if (lblVoteClock.ForeColor != Color.White)
                            {
                                lblVoteClock.ForeColor = Color.White;
                                voteBox.ForeColor = Color.White;
                            }
                        }

                        if (!_isVotingOpen && !Program.IsVotingClosed)
                        {
                            _isVotingOpen = true;
                            //Program.SendIRCMessage("++ " + lblPollNum.Text + " is now open!");
                        }
                    }
                    else // Zero
                    {
                        lblVoteClock.Text = "--.---s left";
                        lblVoteClock.ForeColor = Color.White;
                        if (Program.GameMode == User.ModeType.DEMOCRACY) StartVote();
                    }

                    if (tallied && Program.GameMode == User.ModeType.DEMOCRACY)
                    {
                        if (winnerBool)
                        {
                            if (isPopularVote) lblVoteResult.Text = "WINNER: " + winner.Name;
                            else lblVoteResult.Text = "TIE, NO ACTION";
                        }
                        else lblVoteResult.Text = "NO VOTES";
                        Program.SendIRCNotice(lblPollNum.Text + " " + lblVoteResult.Text);

                        if (voteCount == 999) voteCount = -1;
                        lblPollNum.Text = "Poll #" + (++voteCount).ToString().PadLeft(3, '0');
                    }
                }
            }
        }
        public void OnGameMode(User.ModeType mode)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(
                        () =>
                        {
                            switch (mode)
                            {
                                case User.ModeType.ANARCHY:
                                    {
                                        lblVoteResult.Text = "ANARCHY";
                                        lblVoteResult.ForeColor = Color.Crimson;
                                        lblVoteResult.Location = new Point(7, 138);
                                        chatBox.Location = new Point(11, 158);
                                        chatBox.Size = new Size(306, 308);
                                        pbModeForeground.Image = global::TwitchPlays.Properties.Resources.pikachu_left;
                                        chatBox.Items.Clear();
                                        Program.TallyTime = DateTime.Now;
                                    }
                                    break;
                                case User.ModeType.DEMOCRACY:
                                    {
                                        lblVoteResult.Text = "DEMOCRACY";
                                        lblVoteResult.ForeColor = Color.DodgerBlue;
                                        lblVoteResult.Location = new Point(7, 179);
                                        chatBox.Location = new Point(11, 256);
                                        chatBox.Size = new Size(306, 210);
                                        pbModeForeground.Image = global::TwitchPlays.Properties.Resources.pikachu_right;
                                        chatBox.Items.Clear();
                                    }
                                    break;
                            }
                        })
                    );
            }
            else if (Program.IsMainThread)
            {
                switch (mode)
                {
                    case User.ModeType.ANARCHY:
                        {
                            lblVoteResult.Text = "ANARCHY";
                            lblVoteResult.ForeColor = Color.Crimson;
                            lblVoteResult.Location = new Point(7, 138);
                            chatBox.Location = new Point(11, 158);
                            chatBox.Size = new Size(306, 308);
                            pbModeForeground.Image = global::TwitchPlays.Properties.Resources.pikachu_left;
                            chatBox.Items.Clear();
                            Program.TallyTime = DateTime.Now;
                        }
                        break;
                    case User.ModeType.DEMOCRACY:
                        {
                            lblVoteResult.Text = "DEMOCRACY";
                            lblVoteResult.ForeColor = Color.DodgerBlue;
                            lblVoteResult.Location = new Point(7, 179);
                            chatBox.Location = new Point(11, 256);
                            chatBox.Size = new Size(306, 210);
                            pbModeForeground.Image = global::TwitchPlays.Properties.Resources.pikachu_right;
                            chatBox.Items.Clear();
                        }
                        break;
                }
            }
        }
        private void CheckModeVotes()
        {
            while (true)
            {
                Thread.Sleep(75);
                // Check Mode Votes
                // TODO Filter out votes from inactive users
                List<User> _users = null;

                if (Monitor.TryEnter(Users.All))
                {
                    try
                    {
                        _users = new List<User>(Users.All);
                    }
                    finally
                    {
                        Monitor.Exit(Users.All);
                    }
                }

                if (_users == null) continue;

                double anarchyCount = _users.Count(x => x.GameMode == User.ModeType.ANARCHY);
                double democracyCount = _users.Count(x => x.GameMode == User.ModeType.DEMOCRACY);
                double totalCount = anarchyCount + democracyCount;
                double percentage = 0.0;

                if (Program.GameMode == User.ModeType.ANARCHY)
                {
                    percentage = democracyCount / totalCount;
                    if (percentage >= 0.75) goto _goto_switch_to_democracy;
                }
                else if (Program.GameMode == User.ModeType.DEMOCRACY)
                {
                    percentage = anarchyCount / totalCount;
                    if (percentage >= 0.75) goto _goto_switch_to_anarchy;
                }
                else
                {
                    if (anarchyCount > democracyCount) goto _goto_switch_to_anarchy;
                    else if (democracyCount > anarchyCount) goto _goto_switch_to_democracy;
                }

                goto _goto_noswitch;

            _goto_switch_to_democracy:
                Program.GameMode = User.ModeType.DEMOCRACY;
                Program.SendIRCNotice("From the people, democracy has emerged.");
                goto _goto_noswitch;

            _goto_switch_to_anarchy:
                Program.GameMode = User.ModeType.ANARCHY;
                Program.SendIRCNotice("Anarchy has fallen upon this land...");
                Program.TallyTime = DateTime.Now; // Cancel current poll
                goto _goto_noswitch;

            _goto_noswitch:
                try
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(
                            () =>
                            {
                                if (totalCount == 0)
                                {
                                    pbModeForeground.Left = pbModeBackground.Left + (int)((double)(pbModeBackground.Width - pbModeForeground.Width) * (0.5));
                                    lblDemocracyCount.Text = "--";
                                    lblAnarchyCount.Text = "--";
                                }
                                else
                                {
                                    pbModeForeground.Left = pbModeBackground.Left + (int)((double)(pbModeBackground.Width - pbModeForeground.Width) * (anarchyCount / totalCount));
                                    lblDemocracyCount.Text = democracyCount.ToString();
                                    lblAnarchyCount.Text = anarchyCount.ToString();
                                }
                            })
                        );
                    }
                    else if (Program.IsMainThread)
                    {
                        if (totalCount == 0)
                        {
                            pbModeForeground.Left = pbModeBackground.Left + (int)((double)(pbModeBackground.Width - pbModeForeground.Width) * (0.5));
                            lblDemocracyCount.Text = "--";
                            lblAnarchyCount.Text = "--";
                        }
                        else
                        {
                            pbModeForeground.Left = pbModeBackground.Left + (int)((double)(pbModeBackground.Width - pbModeForeground.Width) * (democracyCount / totalCount));
                            lblDemocracyCount.Text = democracyCount.ToString();
                            lblAnarchyCount.Text = anarchyCount.ToString();
                        }
                    }
                }
                catch (ObjectDisposedException) { }
            }
        }
        /*public void SetUserCount(int d)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(
                        () =>
                        {
                            if (d == 1) lblTwitchPlays.Text = "Twitch Plays";
                            else lblTwitchPlays.Text = d.ToString() + " Twitch Users Play";
                        })
                );
            }
            else if (Program.IsMainThread)
            {
                if (d == 1) lblTwitchPlays.Text = "Twitch Plays";
                else lblTwitchPlays.Text = d.ToString() + " Twitch Users Play";
            }
        }*/
        private void SortNominees()
        {
            while (true)
            {
                Thread.Sleep(50);
                try
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(
                                () =>
                                {
                                    Program.SortNominees();
                                    voteBox.Items.Clear();
                                    foreach (Nominee nominee in Program.Nominees)
                                    {
                                        string _name = nominee.Name;
                                        if (_name.Length > 27) _name = _name.Substring(0, 27);
                                        if (nominee.Votes != 0) voteBox.Items.Add(_name + nominee.Votes.ToString().PadLeft(27 - _name.Length, ' '));
                                    }
                                })
                            );
                    }
                    else if (Program.IsMainThread)
                    {
                        Program.SortNominees();
                        voteBox.Items.Clear();
                        foreach (Nominee nominee in Program.Nominees)
                        {
                            string _name = nominee.Name;
                            if (_name.Length > 27) _name = _name.Substring(0, 27);
                            if (nominee.Votes != 0) voteBox.Items.Add(_name + nominee.Votes.ToString().PadLeft(27 - _name.Length, ' '));
                        }
                    }
                }
                catch (ObjectDisposedException) { };
            }
        }

        private void Initialize_vJoy()
        {
            // Create one joystick object and a position structure.
            joystick = new vJoy();
            iReport = new vJoy.JoystickState();

            // Get the driver attributes (Vendor ID, Product ID, Version Number)
            if (!joystick.vJoyEnabled())
            {
                MessageBox.Show("vJoy driver not enabled: Failed Getting vJoy attributes.\n");
                Program._shouldExit = true;
                System.Environment.Exit(0);
            }

            // Get the state of the requested device
            VjdStat status = joystick.GetVJDStatus(id);
            switch (status)
            {
                case VjdStat.VJD_STAT_OWN:
                    break;
                case VjdStat.VJD_STAT_FREE:
                    break;
                case VjdStat.VJD_STAT_BUSY:
                    MessageBox.Show("vJoy Device is already owned by another feeder\nInput feeding will not occur.");
                    //System.Environment.Exit(0);
                    return;
                case VjdStat.VJD_STAT_MISS:
                    MessageBox.Show("vJoy Device is not installed or disabled\nInput feeding will not occur.");
                    //System.Environment.Exit(0);
                    return;
                default:
                    MessageBox.Show("vJoy Device general error\nInput feeding will not occur.");
                    //System.Environment.Exit(0);
                    return;
            };

            // Get the number of buttons and POV Hat switchessupported by this vJoy device
            int nButtons = joystick.GetVJDButtonNumber(id);
            int ContPovNumber = joystick.GetVJDContPovNumber(id);
            int DiscPovNumber = joystick.GetVJDDiscPovNumber(id);

            // Acquire the target
            if ((status == VjdStat.VJD_STAT_OWN) || ((status == VjdStat.VJD_STAT_FREE) && (!joystick.AcquireVJD(id))))
            {
                MessageBox.Show("Failed to acquire vJoy device number");
                Program._shouldExit = true;
                System.Environment.Exit(0);
            }

            joystick.ResetVJD(id);
        }

        public void vJoySendNominee(Nominee n)
        {
            for (int i = 0; i < n.Keys.Count; i++)
            {
                if (n.Keys[i] == Program.KeyType.HOLD) Thread.Sleep(500);
                else vJoySendButton((uint)n.Keys[i], n.Counts[i]);
                Thread.Sleep(100);
            }
        }

        private void vJoySendButton(uint btn, uint cnt)
        {
            for (int i = 0; i < cnt; i++)
            {
                joystick.SetBtn(true, id, btn + 1);
                Thread.Sleep(150);
                joystick.SetBtn(false, id, btn + 1);
                Thread.Sleep(500);
            }
        }

        private string _logBuffer = "";
        private string _logBuffer2 = "";
        private bool _bufferLock = false;
        private bool _textBoxLock = false;
        public void Log(string msg) {
            string _line = "[" + lblTimespan.Text + "] " + msg + "\r\n";
            if (!_bufferLock) _logBuffer += _line;
            else _logBuffer2 += _line;

            int count = Users.TotalUsers;
            if (count < 500)
            {
                logBox.AppendText(_line);
                _textBoxLock = false;
            }
            else
            {
                if (_textBoxLock == false)
                {
                    _textBoxLock = true;
                    logBox.Text = "Active user count is over 500. Real-time chat log has been disabled.";
                }
            }
        }

        public void SetLastMsg(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(
                    () =>
                    {
                        while (chatBox.Items.Count >= chatBox.Size.Height / 14) chatBox.Items.RemoveAt(0);
                        chatBox.Items.Add(msg);
                    }
                    )
                );

                if (LagNoticeEnabled) LagNoticeEnabled = false;
            }
            else if (Program.IsMainThread)
            {
                while (chatBox.Items.Count >= chatBox.Size.Height / 14) chatBox.Items.RemoveAt(0);
                chatBox.Items.Add(msg);
            }
        }

        private string _goalsetter;
        public string GoalSetter
        {
            get
            {
                return _goalsetter;
            }
            set
            {
                _goalsetter = value;
            }
        }
        public string Goal
        {
            get
            {
                if (this.InvokeRequired)
                {
                    string temp = "";
                    this.Invoke(new Action(
                       () =>
                       {
                           temp = lblGoal.Text;
                       }
                       )
                   );
                   return temp;
                }
                else
                {
                    return lblGoal.Text;
                }
            }
        }
        private void ResetGoal(int minutes)
        {
            try
            {
                Thread.Sleep(minutes * 60000);
                _canSetGoal = true;
            }
            catch (ThreadInterruptedException) { };
        }

        Thread goal_thread;
        public void SetAdminGoal(User u, string msg)
        {
            if (_canSetGoal)
            {
                _canSetGoal = false;
                goal_thread = new Thread(() => ResetGoal(5));
                goal_thread.Start();
            }
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(
                   () =>
                   {
                       lblGoal.Text = msg;
                   }
                   )
               );
            }
            else if (Program.IsMainThread)
            {
                lblGoal.Text = msg;
            }
            GoalSetter = u.Name;
            Program.SendIRCNotice(GoalSetter + " has set the goal to: '" + msg + "'");
        }
        public bool SetGoal(User u, string msg)
        {
            if (_canSetGoal)
            {
                SetAdminGoal(u, msg);
                return true;
            }
            return false;
        }

        public void ClearAdminGoal(User u, string msg)
        {
            lock (Users.ClearGoal)
            {
                Users.ClearGoal.Clear();
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(
                    () =>
                    {
                        lblGoal.Text = msg;
                    }
                    )
                );
            }
            else if (Program.IsMainThread)
            {
                lblGoal.Text = msg;
            }

            GoalSetter = u.Name;
            Program.SendIRCNotice(GoalSetter + " has cleared the goal.");

            if (goal_thread != null && goal_thread.IsAlive)
            {
                goal_thread.Interrupt();
                goal_thread = new Thread(() => ResetGoal(1));
                goal_thread.Start();
            }
        }
        public bool ClearGoal(User u, string msg)
        {
            if (!_canSetGoal)
            {
                int _cleargoal_count = 0;
                lock (Users.ClearGoal) {
                    _cleargoal_count = Users.ClearGoal.Count;
                }
                int _usersactive_count = 0;
                lock (Users.All)
                {
                    _usersactive_count = Users.All.Count;
                }
                if (_cleargoal_count * 2 > _usersactive_count)
                {
                    ClearAdminGoal(u, msg);
                    return true;
                }
            }
            return false;
        }

        private void btnTestDelay_Click(object sender, EventArgs e)
        {
            /*int testcount;
            if (Int32.TryParse(txtDelayTest.Text, out testcount)) SetUserCount(testcount);
            else MessageBox.Show("Unable to parse Test Delay.");*/
        }

        private void txtMaxDelay_Leave(object sender, EventArgs e)
        {
            int d;
            if (Int32.TryParse(txtMaxDelay.Text, out d))
            {
                Program.MaxDelay = d;
            }
            else MessageBox.Show("Unable to parse max delay");
        }

        private void btnSetAdminGoal_Click(object sender, EventArgs e)
        {
            User u = new User("ADMIN CONSOLE");
            SetAdminGoal(u, txtAdminGoal.Text);
        }

        private void cbDisableVoting_CheckedChanged(object sender, EventArgs e)
        {
            InputEnabled = !cbDisableVoting.Checked;
        }

        private void btnSendTestButton_Click(object sender, EventArgs e)
        {
            int btn;
            if (Int32.TryParse(txtTestButton.Text, out btn))
            {
                vJoySendButton((uint)btn, 1);
            }
            else MessageBox.Show("Unable to send vJoy test button");
        }

        private static string workingDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private static string chatlogDirectory = workingDirectory + @"\chatlogs";
        public void DumpLog()
        {
            string log = (_bufferLock) ? _logBuffer2 : _logBuffer;

            // Switch buffers
            _bufferLock = !_bufferLock;

            string dumpfileName = DateTime.Now.ToLocalTime().ToString("yyyy_MM_dd") + ".txt";
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(
                       () =>
                       {
                           try
                           {
                               if (!Directory.Exists(chatlogDirectory))
                               {
                                   Console.WriteLine("Chatlog directory does not exist. Creating one...");
                                   Directory.CreateDirectory(chatlogDirectory);
                               }
                               using (System.IO.StreamWriter file = new System.IO.StreamWriter(chatlogDirectory + @"\" + dumpfileName, true))
                               {

                                   file.Write(log);
                                   log = "";
                                   if (_textBoxLock == false) logBox.Clear();
                               }
                           }
                           catch (Exception e)
                           {
                               Log("Unable to save dump: " + e.Message);
                           }
                       }
                       )
                   );
            }
            else if (Program.IsMainThread)
            {
                try
                {
                    if (!Directory.Exists(chatlogDirectory)) Directory.CreateDirectory(chatlogDirectory);
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(chatlogDirectory + @"\" + dumpfileName, true))
                    {

                        file.Write(log);
                        log = "";
                        if (_textBoxLock == false) logBox.Clear();
                    }
                }
                catch (Exception e)
                {
                    Log("Unable to save dump: " + e.Message);
                }
            }
        }
        private void CacheTick()
        {
            int sleep_time = (1000 * 60 * 5) / 2;
            DumpLog(); // Set initial TextBox & buffer data bindings
            while (true)
            {
                Thread.Sleep(sleep_time);
                SaveGame();
                DumpLog();
                Console.WriteLine("CacheTick!");
                Users.RemoveInactive();
                Thread.Sleep(sleep_time);
            }
        }
        private void btnDumpLog_Click(object sender, EventArgs e)
        {
            DumpLog();
        }

        private void LogForm_LocationChanged(object sender, EventArgs e)
        {
            AttachPokemonParty(ref module_party);
        }

        private void btnAdminUp_Click(object sender, EventArgs e)
        {
            vJoySendButton((uint)(Program.KeyType.UP), 1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            vJoySendButton((uint)(Program.KeyType.DOWN), 1);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            vJoySendButton((uint)(Program.KeyType.LEFT), 1);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            vJoySendButton((uint)(Program.KeyType.RIGHT), 1);
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private void ActivateApp(string processName)
        {
            Process[] p = Process.GetProcessesByName(processName);

            // Activate the first application we find with this name
            if (p.Count() > 0)
                SetForegroundWindow(p[0].MainWindowHandle);
        }
        public void SaveGame()
        {
            if (!Program.ShouldAutosave) return;
            ActivateApp("VisualBoyAdvance-M");
            Thread.Sleep(5000);
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(
                    () =>
                    {
                        System.Windows.Forms.SendKeys.Send("+{F1}");
                    })
                );
            }
            else if (Program.IsMainThread) System.Windows.Forms.SendKeys.Send("+{F1}");
        }
        public void ReloadGame()
        {
            ActivateApp("VisualBoyAdvance-M");
            Thread.Sleep(5000);
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(
                    () =>
                    {
                        System.Windows.Forms.SendKeys.Send("^R");
                    })
                );
            }
            else if (Program.IsMainThread) System.Windows.Forms.SendKeys.Send("^R");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => SaveGame());
            thread.Start();
        }

        private void lblTwitchPlays_Click(object sender, EventArgs e)
        {
            if (this.Width == 342 && this.Height == 520) this.Width = 954;
            else
            {
                this.Width = 342;
                this.Height = 520;
            }
        }
    }
}
