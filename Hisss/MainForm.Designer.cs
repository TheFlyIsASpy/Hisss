﻿namespace Hisss
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            axFiScn1 = new AxFiScnLib.AxFiScn();
            ((System.ComponentModel.ISupportInitialize)axFiScn1).BeginInit();
            SuspendLayout();
            // 
            // axFiScn1
            // 
            axFiScn1.Enabled = true;
            axFiScn1.Location = new Point(364, 192);
            axFiScn1.Name = "axFiScn1";
            axFiScn1.OcxState = (AxHost.State)resources.GetObject("axFiScn1.OcxState");
            axFiScn1.Size = new Size(48, 48);
            axFiScn1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(axFiScn1);
            Name = "Form1";
            Text = "Form1";
            Load += On_Form_Load;
            ((System.ComponentModel.ISupportInitialize)axFiScn1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private AxFiScnLib.AxFiScn axFiScn1;
    }
}
