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
namespace TwitchPlays
{
    partial class LogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logBox = new System.Windows.Forms.TextBox();
            this.voteBox = new System.Windows.Forms.ListBox();
            this.lblGoal = new System.Windows.Forms.Label();
            this.btnTestDelay = new System.Windows.Forms.Button();
            this.txtDelayTest = new System.Windows.Forms.TextBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.lblDurationOffset = new System.Windows.Forms.Label();
            this.txtDurationOffset = new System.Windows.Forms.TextBox();
            this.btnRight = new System.Windows.Forms.Button();
            this.cbDisableVoting = new System.Windows.Forms.CheckBox();
            this.btnLeft = new System.Windows.Forms.Button();
            this.lblMaxDelay = new System.Windows.Forms.Label();
            this.txtMaxDelay = new System.Windows.Forms.TextBox();
            this.btnAdminUp = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSendTestButton = new System.Windows.Forms.Button();
            this.txtTestButton = new System.Windows.Forms.TextBox();
            this.txtAdminGoal = new System.Windows.Forms.TextBox();
            this.btnSetAdminGoal = new System.Windows.Forms.Button();
            this.lblTwitchPlays = new System.Windows.Forms.Label();
            this.lblYellow = new System.Windows.Forms.Label();
            this.lblPokemon = new System.Windows.Forms.Label();
            this.lblTimespan = new System.Windows.Forms.Label();
            this.lblVoteClock = new System.Windows.Forms.Label();
            this.lblAlert = new System.Windows.Forms.Label();
            this.chatBox = new System.Windows.Forms.ListBox();
            this.lblPollNum = new System.Windows.Forms.Label();
            this.lblVoteResult = new System.Windows.Forms.Label();
            this.lblInputAlert = new System.Windows.Forms.Label();
            this.btnDumpLog = new System.Windows.Forms.Button();
            this.lblDemocracyCount = new System.Windows.Forms.Label();
            this.lblAnarchyCount = new System.Windows.Forms.Label();
            this.pbModeForeground = new System.Windows.Forms.PictureBox();
            this.pbModeBackground = new System.Windows.Forms.PictureBox();
            this.pbInputAlert32 = new System.Windows.Forms.PictureBox();
            this.pbClock32 = new System.Windows.Forms.PictureBox();
            this.pbAlert32 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeForeground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInputAlert32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClock32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAlert32)).BeginInit();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(371, 12);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(557, 240);
            this.logBox.TabIndex = 0;
            // 
            // voteBox
            // 
            this.voteBox.BackColor = System.Drawing.Color.Black;
            this.voteBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.voteBox.Font = new System.Drawing.Font("codefont1", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.voteBox.ForeColor = System.Drawing.Color.White;
            this.voteBox.FormattingEnabled = true;
            this.voteBox.ItemHeight = 17;
            this.voteBox.Items.AddRange(new object[] {
            "START",
            "SELECT",
            "A",
            "B",
            "LEFT",
            "RIGHT",
            "UP",
            "DOWN"});
            this.voteBox.Location = new System.Drawing.Point(10, 202);
            this.voteBox.Name = "voteBox";
            this.voteBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.voteBox.Size = new System.Drawing.Size(353, 51);
            this.voteBox.TabIndex = 1;
            this.voteBox.TabStop = false;
            // 
            // lblGoal
            // 
            this.lblGoal.BackColor = System.Drawing.Color.Black;
            this.lblGoal.Font = new System.Drawing.Font("codefont1", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoal.ForeColor = System.Drawing.Color.Gold;
            this.lblGoal.Location = new System.Drawing.Point(10, 78);
            this.lblGoal.Name = "lblGoal";
            this.lblGoal.Size = new System.Drawing.Size(305, 30);
            this.lblGoal.TabIndex = 7;
            this.lblGoal.Text = "Welcome";
            this.lblGoal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnTestDelay
            // 
            this.btnTestDelay.ForeColor = System.Drawing.Color.Black;
            this.btnTestDelay.Location = new System.Drawing.Point(6, 40);
            this.btnTestDelay.Name = "btnTestDelay";
            this.btnTestDelay.Size = new System.Drawing.Size(154, 22);
            this.btnTestDelay.TabIndex = 8;
            this.btnTestDelay.Text = "Test Delay";
            this.btnTestDelay.UseVisualStyleBackColor = true;
            this.btnTestDelay.Click += new System.EventHandler(this.btnTestDelay_Click);
            // 
            // txtDelayTest
            // 
            this.txtDelayTest.Location = new System.Drawing.Point(6, 18);
            this.txtDelayTest.Name = "txtDelayTest";
            this.txtDelayTest.Size = new System.Drawing.Size(154, 16);
            this.txtDelayTest.TabIndex = 9;
            this.txtDelayTest.Text = "300";
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.btnSave);
            this.grpOptions.Controls.Add(this.btnDown);
            this.grpOptions.Controls.Add(this.lblDurationOffset);
            this.grpOptions.Controls.Add(this.txtDurationOffset);
            this.grpOptions.Controls.Add(this.btnRight);
            this.grpOptions.Controls.Add(this.cbDisableVoting);
            this.grpOptions.Controls.Add(this.btnLeft);
            this.grpOptions.Controls.Add(this.lblMaxDelay);
            this.grpOptions.Controls.Add(this.txtMaxDelay);
            this.grpOptions.Controls.Add(this.btnAdminUp);
            this.grpOptions.Font = new System.Drawing.Font("codefont1", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOptions.ForeColor = System.Drawing.Color.White;
            this.grpOptions.Location = new System.Drawing.Point(651, 286);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(277, 180);
            this.grpOptions.TabIndex = 10;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(149, 79);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 22);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(49, 137);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(32, 32);
            this.btnDown.TabIndex = 8;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // lblDurationOffset
            // 
            this.lblDurationOffset.AutoSize = true;
            this.lblDurationOffset.Location = new System.Drawing.Point(6, 40);
            this.lblDurationOffset.Name = "lblDurationOffset";
            this.lblDurationOffset.Size = new System.Drawing.Size(137, 9);
            this.lblDurationOffset.TabIndex = 4;
            this.lblDurationOffset.Text = "Duration Offset (secs)";
            // 
            // txtDurationOffset
            // 
            this.txtDurationOffset.Location = new System.Drawing.Point(149, 37);
            this.txtDurationOffset.Name = "txtDurationOffset";
            this.txtDurationOffset.Size = new System.Drawing.Size(48, 16);
            this.txtDurationOffset.TabIndex = 3;
            this.txtDurationOffset.Text = "30";
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(87, 97);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(32, 32);
            this.btnRight.TabIndex = 7;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // cbDisableVoting
            // 
            this.cbDisableVoting.AutoSize = true;
            this.cbDisableVoting.Location = new System.Drawing.Point(149, 59);
            this.cbDisableVoting.Name = "cbDisableVoting";
            this.cbDisableVoting.Size = new System.Drawing.Size(108, 14);
            this.cbDisableVoting.TabIndex = 2;
            this.cbDisableVoting.Text = "Disable Voting";
            this.cbDisableVoting.UseVisualStyleBackColor = true;
            this.cbDisableVoting.CheckedChanged += new System.EventHandler(this.cbDisableVoting_CheckedChanged);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(11, 97);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(32, 32);
            this.btnLeft.TabIndex = 6;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // lblMaxDelay
            // 
            this.lblMaxDelay.AutoSize = true;
            this.lblMaxDelay.Location = new System.Drawing.Point(6, 18);
            this.lblMaxDelay.Name = "lblMaxDelay";
            this.lblMaxDelay.Size = new System.Drawing.Size(101, 9);
            this.lblMaxDelay.TabIndex = 1;
            this.lblMaxDelay.Text = "Max Delay (secs)";
            // 
            // txtMaxDelay
            // 
            this.txtMaxDelay.Location = new System.Drawing.Point(149, 15);
            this.txtMaxDelay.Name = "txtMaxDelay";
            this.txtMaxDelay.Size = new System.Drawing.Size(48, 16);
            this.txtMaxDelay.TabIndex = 0;
            this.txtMaxDelay.Text = "8";
            this.txtMaxDelay.Leave += new System.EventHandler(this.txtMaxDelay_Leave);
            // 
            // btnAdminUp
            // 
            this.btnAdminUp.Location = new System.Drawing.Point(49, 59);
            this.btnAdminUp.Name = "btnAdminUp";
            this.btnAdminUp.Size = new System.Drawing.Size(32, 32);
            this.btnAdminUp.TabIndex = 5;
            this.btnAdminUp.UseVisualStyleBackColor = true;
            this.btnAdminUp.Click += new System.EventHandler(this.btnAdminUp_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSendTestButton);
            this.groupBox1.Controls.Add(this.txtTestButton);
            this.groupBox1.Controls.Add(this.txtAdminGoal);
            this.groupBox1.Controls.Add(this.btnSetAdminGoal);
            this.groupBox1.Controls.Add(this.txtDelayTest);
            this.groupBox1.Controls.Add(this.btnTestDelay);
            this.groupBox1.Font = new System.Drawing.Font("codefont1", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox1.Location = new System.Drawing.Point(371, 286);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 180);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test Bench";
            // 
            // btnSendTestButton
            // 
            this.btnSendTestButton.ForeColor = System.Drawing.Color.Black;
            this.btnSendTestButton.Location = new System.Drawing.Point(6, 140);
            this.btnSendTestButton.Name = "btnSendTestButton";
            this.btnSendTestButton.Size = new System.Drawing.Size(154, 22);
            this.btnSendTestButton.TabIndex = 13;
            this.btnSendTestButton.Text = "Send Test Button";
            this.btnSendTestButton.UseVisualStyleBackColor = true;
            this.btnSendTestButton.Click += new System.EventHandler(this.btnSendTestButton_Click);
            // 
            // txtTestButton
            // 
            this.txtTestButton.Location = new System.Drawing.Point(6, 118);
            this.txtTestButton.Name = "txtTestButton";
            this.txtTestButton.Size = new System.Drawing.Size(154, 16);
            this.txtTestButton.TabIndex = 12;
            this.txtTestButton.Text = "0";
            // 
            // txtAdminGoal
            // 
            this.txtAdminGoal.Location = new System.Drawing.Point(6, 68);
            this.txtAdminGoal.Name = "txtAdminGoal";
            this.txtAdminGoal.Size = new System.Drawing.Size(154, 16);
            this.txtAdminGoal.TabIndex = 11;
            this.txtAdminGoal.Text = "Think 20s ahead";
            // 
            // btnSetAdminGoal
            // 
            this.btnSetAdminGoal.ForeColor = System.Drawing.Color.Black;
            this.btnSetAdminGoal.Location = new System.Drawing.Point(6, 90);
            this.btnSetAdminGoal.Name = "btnSetAdminGoal";
            this.btnSetAdminGoal.Size = new System.Drawing.Size(154, 22);
            this.btnSetAdminGoal.TabIndex = 10;
            this.btnSetAdminGoal.Text = "Set Admin Goal";
            this.btnSetAdminGoal.UseVisualStyleBackColor = true;
            this.btnSetAdminGoal.Click += new System.EventHandler(this.btnSetAdminGoal_Click);
            // 
            // lblTwitchPlays
            // 
            this.lblTwitchPlays.BackColor = System.Drawing.Color.Black;
            this.lblTwitchPlays.Font = new System.Drawing.Font("codefont1", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTwitchPlays.ForeColor = System.Drawing.Color.White;
            this.lblTwitchPlays.Location = new System.Drawing.Point(9, 9);
            this.lblTwitchPlays.Name = "lblTwitchPlays";
            this.lblTwitchPlays.Size = new System.Drawing.Size(306, 18);
            this.lblTwitchPlays.TabIndex = 12;
            this.lblTwitchPlays.Text = "Twitch Plays";
            this.lblTwitchPlays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTwitchPlays.Click += new System.EventHandler(this.lblTwitchPlays_Click);
            // 
            // lblYellow
            // 
            this.lblYellow.AutoSize = true;
            this.lblYellow.BackColor = System.Drawing.Color.Transparent;
            this.lblYellow.Font = new System.Drawing.Font("codefont1", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYellow.ForeColor = System.Drawing.Color.Gold;
            this.lblYellow.Location = new System.Drawing.Point(176, 27);
            this.lblYellow.Name = "lblYellow";
            this.lblYellow.Size = new System.Drawing.Size(94, 21);
            this.lblYellow.TabIndex = 13;
            this.lblYellow.Text = "Yellow";
            this.lblYellow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPokemon
            // 
            this.lblPokemon.AutoSize = true;
            this.lblPokemon.BackColor = System.Drawing.Color.Black;
            this.lblPokemon.Font = new System.Drawing.Font("codefont1", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPokemon.ForeColor = System.Drawing.Color.White;
            this.lblPokemon.Location = new System.Drawing.Point(62, 27);
            this.lblPokemon.Name = "lblPokemon";
            this.lblPokemon.Size = new System.Drawing.Size(108, 21);
            this.lblPokemon.TabIndex = 14;
            this.lblPokemon.Text = "Pokemon";
            this.lblPokemon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTimespan
            // 
            this.lblTimespan.BackColor = System.Drawing.Color.Black;
            this.lblTimespan.Font = new System.Drawing.Font("codefont1", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimespan.ForeColor = System.Drawing.Color.White;
            this.lblTimespan.Location = new System.Drawing.Point(8, 55);
            this.lblTimespan.Name = "lblTimespan";
            this.lblTimespan.Size = new System.Drawing.Size(309, 18);
            this.lblTimespan.TabIndex = 15;
            this.lblTimespan.Text = "0d 00h 00m 00s";
            this.lblTimespan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVoteClock
            // 
            this.lblVoteClock.BackColor = System.Drawing.Color.Transparent;
            this.lblVoteClock.Font = new System.Drawing.Font("codefont1", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoteClock.ForeColor = System.Drawing.Color.White;
            this.lblVoteClock.Location = new System.Drawing.Point(48, 152);
            this.lblVoteClock.Name = "lblVoteClock";
            this.lblVoteClock.Size = new System.Drawing.Size(122, 20);
            this.lblVoteClock.TabIndex = 17;
            this.lblVoteClock.Text = "00.00s left";
            this.lblVoteClock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAlert
            // 
            this.lblAlert.Font = new System.Drawing.Font("codefont1", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlert.ForeColor = System.Drawing.Color.Orange;
            this.lblAlert.Location = new System.Drawing.Point(12, 78);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(305, 30);
            this.lblAlert.TabIndex = 19;
            this.lblAlert.Text = "TwitchChat lag detected";
            this.lblAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chatBox
            // 
            this.chatBox.BackColor = System.Drawing.Color.Black;
            this.chatBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatBox.Font = new System.Drawing.Font("codefont1", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatBox.ForeColor = System.Drawing.Color.White;
            this.chatBox.FormattingEnabled = true;
            this.chatBox.ItemHeight = 14;
            this.chatBox.Items.AddRange(new object[] {
            "(00m:01s) <blank>: <blank>",
            "(00m:02s) <blank>: <blank>",
            "(00m:03s) <blank>: <blank>",
            "(00m:04s) <blank>: <blank>",
            "(00m:05s) <blank>: <blank>",
            "(00m:06s) <blank>: <blank>",
            "(00m:07s) <blank>: <blank>",
            "(00m:08s) <blank>: <blank>",
            "(00m:09s) <blank>: <blank>",
            "(00m:10s) <blank>: <blank>",
            "(00m:11s) <blank>: <blank>",
            "(00m:12s) <blank>: <blank>",
            "(00m:13s) <blank>: <blank>",
            "(00m:14s) <blank>: <blank>",
            "(00m:15s) <blank>: <blank>",
            "(00m:16s) <blank>: <blank>",
            "(00m:17s) <blank>: <blank>",
            "(00m:18s) <blank>: <blank>",
            "(00m:19s) <blank>: <blank>",
            "(00m:20s) <blank>: <blank>"});
            this.chatBox.Location = new System.Drawing.Point(11, 256);
            this.chatBox.Name = "chatBox";
            this.chatBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.chatBox.Size = new System.Drawing.Size(306, 210);
            this.chatBox.TabIndex = 20;
            this.chatBox.TabStop = false;
            // 
            // lblPollNum
            // 
            this.lblPollNum.BackColor = System.Drawing.Color.Transparent;
            this.lblPollNum.Font = new System.Drawing.Font("codefont1", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPollNum.ForeColor = System.Drawing.Color.White;
            this.lblPollNum.Location = new System.Drawing.Point(200, 152);
            this.lblPollNum.Name = "lblPollNum";
            this.lblPollNum.Size = new System.Drawing.Size(117, 20);
            this.lblPollNum.TabIndex = 22;
            this.lblPollNum.Text = "Poll #00000";
            this.lblPollNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVoteResult
            // 
            this.lblVoteResult.BackColor = System.Drawing.Color.Transparent;
            this.lblVoteResult.Font = new System.Drawing.Font("codefont1", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoteResult.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblVoteResult.Location = new System.Drawing.Point(7, 179);
            this.lblVoteResult.Name = "lblVoteResult";
            this.lblVoteResult.Size = new System.Drawing.Size(310, 20);
            this.lblVoteResult.TabIndex = 23;
            this.lblVoteResult.Text = "DEMOCRACY";
            this.lblVoteResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInputAlert
            // 
            this.lblInputAlert.BackColor = System.Drawing.Color.Transparent;
            this.lblInputAlert.Font = new System.Drawing.Font("codefont1", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputAlert.ForeColor = System.Drawing.Color.Crimson;
            this.lblInputAlert.Location = new System.Drawing.Point(6, 202);
            this.lblInputAlert.Name = "lblInputAlert";
            this.lblInputAlert.Size = new System.Drawing.Size(306, 32);
            this.lblInputAlert.TabIndex = 25;
            this.lblInputAlert.Text = "Input Disabled";
            this.lblInputAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDumpLog
            // 
            this.btnDumpLog.ForeColor = System.Drawing.Color.Black;
            this.btnDumpLog.Location = new System.Drawing.Point(774, 258);
            this.btnDumpLog.Name = "btnDumpLog";
            this.btnDumpLog.Size = new System.Drawing.Size(154, 22);
            this.btnDumpLog.TabIndex = 14;
            this.btnDumpLog.Text = "Dump Log";
            this.btnDumpLog.UseVisualStyleBackColor = true;
            this.btnDumpLog.Click += new System.EventHandler(this.btnDumpLog_Click);
            // 
            // lblDemocracyCount
            // 
            this.lblDemocracyCount.BackColor = System.Drawing.Color.Transparent;
            this.lblDemocracyCount.Font = new System.Drawing.Font("codefont1", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemocracyCount.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblDemocracyCount.Location = new System.Drawing.Point(10, 111);
            this.lblDemocracyCount.Name = "lblDemocracyCount";
            this.lblDemocracyCount.Size = new System.Drawing.Size(45, 28);
            this.lblDemocracyCount.TabIndex = 27;
            this.lblDemocracyCount.Text = "0";
            this.lblDemocracyCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAnarchyCount
            // 
            this.lblAnarchyCount.BackColor = System.Drawing.Color.Transparent;
            this.lblAnarchyCount.Font = new System.Drawing.Font("codefont1", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnarchyCount.ForeColor = System.Drawing.Color.Crimson;
            this.lblAnarchyCount.Location = new System.Drawing.Point(269, 110);
            this.lblAnarchyCount.Name = "lblAnarchyCount";
            this.lblAnarchyCount.Size = new System.Drawing.Size(45, 28);
            this.lblAnarchyCount.TabIndex = 28;
            this.lblAnarchyCount.Text = "0";
            this.lblAnarchyCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbModeForeground
            // 
            this.pbModeForeground.BackColor = System.Drawing.Color.Silver;
            this.pbModeForeground.Image = global::TwitchPlays.Properties.Resources.pikachu_left;
            this.pbModeForeground.Location = new System.Drawing.Point(139, 111);
            this.pbModeForeground.Name = "pbModeForeground";
            this.pbModeForeground.Size = new System.Drawing.Size(27, 27);
            this.pbModeForeground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbModeForeground.TabIndex = 29;
            this.pbModeForeground.TabStop = false;
            // 
            // pbModeBackground
            // 
            this.pbModeBackground.BackColor = System.Drawing.Color.Silver;
            this.pbModeBackground.Location = new System.Drawing.Point(61, 125);
            this.pbModeBackground.Name = "pbModeBackground";
            this.pbModeBackground.Size = new System.Drawing.Size(202, 3);
            this.pbModeBackground.TabIndex = 26;
            this.pbModeBackground.TabStop = false;
            // 
            // pbInputAlert32
            // 
            this.pbInputAlert32.BackColor = System.Drawing.Color.Transparent;
            this.pbInputAlert32.Image = global::TwitchPlays.Properties.Resources.alert32;
            this.pbInputAlert32.Location = new System.Drawing.Point(50, 202);
            this.pbInputAlert32.Name = "pbInputAlert32";
            this.pbInputAlert32.Size = new System.Drawing.Size(32, 32);
            this.pbInputAlert32.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbInputAlert32.TabIndex = 24;
            this.pbInputAlert32.TabStop = false;
            // 
            // pbClock32
            // 
            this.pbClock32.Image = global::TwitchPlays.Properties.Resources.clock32;
            this.pbClock32.Location = new System.Drawing.Point(10, 144);
            this.pbClock32.Name = "pbClock32";
            this.pbClock32.Size = new System.Drawing.Size(32, 32);
            this.pbClock32.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbClock32.TabIndex = 16;
            this.pbClock32.TabStop = false;
            // 
            // pbAlert32
            // 
            this.pbAlert32.Image = global::TwitchPlays.Properties.Resources.alert32;
            this.pbAlert32.Location = new System.Drawing.Point(10, 76);
            this.pbAlert32.Name = "pbAlert32";
            this.pbAlert32.Size = new System.Drawing.Size(32, 32);
            this.pbAlert32.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbAlert32.TabIndex = 18;
            this.pbAlert32.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("codefont1", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Aquamarine;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 39);
            this.label1.TabIndex = 30;
            this.label1.Text = "It\'s a whole new world we live in...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(326, 481);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblVoteResult);
            this.Controls.Add(this.pbInputAlert32);
            this.Controls.Add(this.lblInputAlert);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.lblDemocracyCount);
            this.Controls.Add(this.pbModeForeground);
            this.Controls.Add(this.lblAnarchyCount);
            this.Controls.Add(this.pbModeBackground);
            this.Controls.Add(this.btnDumpLog);
            this.Controls.Add(this.lblVoteClock);
            this.Controls.Add(this.pbClock32);
            this.Controls.Add(this.lblTimespan);
            this.Controls.Add(this.lblPokemon);
            this.Controls.Add(this.lblYellow);
            this.Controls.Add(this.lblTwitchPlays);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.pbAlert32);
            this.Controls.Add(this.lblPollNum);
            this.Controls.Add(this.voteBox);
            this.Controls.Add(this.lblAlert);
            this.Controls.Add(this.lblGoal);
            this.Name = "LogForm";
            this.Text = "Log";
            this.Shown += new System.EventHandler(this.LogForm_Shown);
            this.LocationChanged += new System.EventHandler(this.LogForm_LocationChanged);
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeForeground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInputAlert32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClock32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAlert32)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.ListBox voteBox;
        private System.Windows.Forms.Label lblGoal;
        private System.Windows.Forms.Button btnTestDelay;
        private System.Windows.Forms.TextBox txtDelayTest;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Label lblMaxDelay;
        private System.Windows.Forms.TextBox txtMaxDelay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAdminGoal;
        private System.Windows.Forms.Button btnSetAdminGoal;
        private System.Windows.Forms.CheckBox cbDisableVoting;
        private System.Windows.Forms.Button btnSendTestButton;
        private System.Windows.Forms.TextBox txtTestButton;
        private System.Windows.Forms.Label lblTwitchPlays;
        private System.Windows.Forms.Label lblYellow;
        private System.Windows.Forms.Label lblPokemon;
        private System.Windows.Forms.Label lblTimespan;
        private System.Windows.Forms.PictureBox pbClock32;
        private System.Windows.Forms.Label lblVoteClock;
        private System.Windows.Forms.PictureBox pbAlert32;
        private System.Windows.Forms.Label lblAlert;
        private System.Windows.Forms.ListBox chatBox;
        private System.Windows.Forms.Label lblPollNum;
        private System.Windows.Forms.Label lblVoteResult;
        private System.Windows.Forms.PictureBox pbInputAlert32;
        private System.Windows.Forms.Label lblInputAlert;
        private System.Windows.Forms.Button btnDumpLog;
        private System.Windows.Forms.Label lblDurationOffset;
        private System.Windows.Forms.TextBox txtDurationOffset;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnAdminUp;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pbModeBackground;
        private System.Windows.Forms.Label lblDemocracyCount;
        private System.Windows.Forms.Label lblAnarchyCount;
        private System.Windows.Forms.PictureBox pbModeForeground;
        private System.Windows.Forms.Label label1;

    }
}

