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
    partial class PokemonParty
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
            this.btnCheckSave = new System.Windows.Forms.Button();
            this.txtSavPath = new System.Windows.Forms.TextBox();
            this.grpTools = new System.Windows.Forms.GroupBox();
            this.btnBrowseDirectory = new System.Windows.Forms.Button();
            this.btnWatch = new System.Windows.Forms.Button();
            this.lblFrameDimensions = new System.Windows.Forms.Label();
            this.pbSlotSix = new System.Windows.Forms.PictureBox();
            this.pbSlotFive = new System.Windows.Forms.PictureBox();
            this.pbSlotFour = new System.Windows.Forms.PictureBox();
            this.pbSlotThree = new System.Windows.Forms.PictureBox();
            this.pbSlotTwo = new System.Windows.Forms.PictureBox();
            this.pbSlotOne = new System.Windows.Forms.PictureBox();
            this.pbFrame = new System.Windows.Forms.PictureBox();
            this.grpTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotSix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotFive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotFour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotThree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotTwo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotOne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCheckSave
            // 
            this.btnCheckSave.ForeColor = System.Drawing.Color.Black;
            this.btnCheckSave.Location = new System.Drawing.Point(87, 47);
            this.btnCheckSave.Name = "btnCheckSave";
            this.btnCheckSave.Size = new System.Drawing.Size(107, 33);
            this.btnCheckSave.TabIndex = 1;
            this.btnCheckSave.Text = "Check Save";
            this.btnCheckSave.UseVisualStyleBackColor = true;
            this.btnCheckSave.Click += new System.EventHandler(this.btnCheckSave_Click);
            // 
            // txtSavPath
            // 
            this.txtSavPath.Location = new System.Drawing.Point(6, 19);
            this.txtSavPath.Name = "txtSavPath";
            this.txtSavPath.Size = new System.Drawing.Size(269, 16);
            this.txtSavPath.TabIndex = 2;
            // 
            // grpTools
            // 
            this.grpTools.Controls.Add(this.btnBrowseDirectory);
            this.grpTools.Controls.Add(this.btnWatch);
            this.grpTools.Controls.Add(this.lblFrameDimensions);
            this.grpTools.Controls.Add(this.txtSavPath);
            this.grpTools.Controls.Add(this.btnCheckSave);
            this.grpTools.Font = new System.Drawing.Font("codefont1", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTools.ForeColor = System.Drawing.Color.White;
            this.grpTools.Location = new System.Drawing.Point(86, 12);
            this.grpTools.Name = "grpTools";
            this.grpTools.Size = new System.Drawing.Size(281, 103);
            this.grpTools.TabIndex = 3;
            this.grpTools.TabStop = false;
            this.grpTools.Text = "Tools";
            // 
            // btnBrowseDirectory
            // 
            this.btnBrowseDirectory.ForeColor = System.Drawing.Color.Black;
            this.btnBrowseDirectory.Location = new System.Drawing.Point(200, 47);
            this.btnBrowseDirectory.Name = "btnBrowseDirectory";
            this.btnBrowseDirectory.Size = new System.Drawing.Size(75, 33);
            this.btnBrowseDirectory.TabIndex = 11;
            this.btnBrowseDirectory.Text = "Browse...";
            this.btnBrowseDirectory.UseVisualStyleBackColor = true;
            this.btnBrowseDirectory.Click += new System.EventHandler(this.btnBrowseDirectory_Click);
            // 
            // btnWatch
            // 
            this.btnWatch.ForeColor = System.Drawing.Color.Black;
            this.btnWatch.Location = new System.Drawing.Point(6, 47);
            this.btnWatch.Name = "btnWatch";
            this.btnWatch.Size = new System.Drawing.Size(75, 33);
            this.btnWatch.TabIndex = 10;
            this.btnWatch.Text = "Watch";
            this.btnWatch.UseVisualStyleBackColor = true;
            this.btnWatch.Click += new System.EventHandler(this.btnWatch_Click);
            // 
            // lblFrameDimensions
            // 
            this.lblFrameDimensions.AutoSize = true;
            this.lblFrameDimensions.Location = new System.Drawing.Point(48, 83);
            this.lblFrameDimensions.Name = "lblFrameDimensions";
            this.lblFrameDimensions.Size = new System.Drawing.Size(227, 9);
            this.lblFrameDimensions.TabIndex = 3;
            this.lblFrameDimensions.Text = "The frame dimensions are 80 x 479 px.";
            // 
            // pbSlotSix
            // 
            this.pbSlotSix.Location = new System.Drawing.Point(0, 373);
            this.pbSlotSix.Name = "pbSlotSix";
            this.pbSlotSix.Size = new System.Drawing.Size(64, 64);
            this.pbSlotSix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSlotSix.TabIndex = 9;
            this.pbSlotSix.TabStop = false;
            // 
            // pbSlotFive
            // 
            this.pbSlotFive.Location = new System.Drawing.Point(0, 310);
            this.pbSlotFive.Name = "pbSlotFive";
            this.pbSlotFive.Size = new System.Drawing.Size(64, 64);
            this.pbSlotFive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSlotFive.TabIndex = 8;
            this.pbSlotFive.TabStop = false;
            // 
            // pbSlotFour
            // 
            this.pbSlotFour.Location = new System.Drawing.Point(0, 247);
            this.pbSlotFour.Name = "pbSlotFour";
            this.pbSlotFour.Size = new System.Drawing.Size(64, 64);
            this.pbSlotFour.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSlotFour.TabIndex = 7;
            this.pbSlotFour.TabStop = false;
            // 
            // pbSlotThree
            // 
            this.pbSlotThree.Location = new System.Drawing.Point(0, 184);
            this.pbSlotThree.Name = "pbSlotThree";
            this.pbSlotThree.Size = new System.Drawing.Size(64, 64);
            this.pbSlotThree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSlotThree.TabIndex = 6;
            this.pbSlotThree.TabStop = false;
            // 
            // pbSlotTwo
            // 
            this.pbSlotTwo.Location = new System.Drawing.Point(0, 121);
            this.pbSlotTwo.Name = "pbSlotTwo";
            this.pbSlotTwo.Size = new System.Drawing.Size(64, 64);
            this.pbSlotTwo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSlotTwo.TabIndex = 5;
            this.pbSlotTwo.TabStop = false;
            // 
            // pbSlotOne
            // 
            this.pbSlotOne.Location = new System.Drawing.Point(0, 59);
            this.pbSlotOne.Name = "pbSlotOne";
            this.pbSlotOne.Size = new System.Drawing.Size(64, 64);
            this.pbSlotOne.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSlotOne.TabIndex = 4;
            this.pbSlotOne.TabStop = false;
            // 
            // pbFrame
            // 
            this.pbFrame.ErrorImage = global::TwitchPlays.Properties.Resources.pkmn_frame;
            this.pbFrame.Image = global::TwitchPlays.Properties.Resources.pkmn_frame;
            this.pbFrame.Location = new System.Drawing.Point(287, 121);
            this.pbFrame.Name = "pbFrame";
            this.pbFrame.Size = new System.Drawing.Size(80, 479);
            this.pbFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbFrame.TabIndex = 0;
            this.pbFrame.TabStop = false;
            this.pbFrame.Visible = false;
            // 
            // PokemonParty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(377, 480);
            this.Controls.Add(this.pbSlotSix);
            this.Controls.Add(this.pbSlotFive);
            this.Controls.Add(this.pbSlotFour);
            this.Controls.Add(this.pbSlotThree);
            this.Controls.Add(this.pbSlotTwo);
            this.Controls.Add(this.pbSlotOne);
            this.Controls.Add(this.grpTools);
            this.Controls.Add(this.pbFrame);
            this.Name = "PokemonParty";
            this.Text = "PokemonParty";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PokemonParty_FormClosed);
            this.Load += new System.EventHandler(this.PokemonParty_Load);
            this.Shown += new System.EventHandler(this.PokemonParty_Shown);
            this.grpTools.ResumeLayout(false);
            this.grpTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotSix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotFive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotFour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotThree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotTwo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSlotOne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFrame;
        private System.Windows.Forms.Button btnCheckSave;
        private System.Windows.Forms.TextBox txtSavPath;
        private System.Windows.Forms.GroupBox grpTools;
        private System.Windows.Forms.Label lblFrameDimensions;
        private System.Windows.Forms.PictureBox pbSlotOne;
        private System.Windows.Forms.PictureBox pbSlotTwo;
        private System.Windows.Forms.PictureBox pbSlotThree;
        private System.Windows.Forms.PictureBox pbSlotFour;
        private System.Windows.Forms.PictureBox pbSlotSix;
        private System.Windows.Forms.PictureBox pbSlotFive;
        private System.Windows.Forms.Button btnWatch;
        private System.Windows.Forms.Button btnBrowseDirectory;
    }
}