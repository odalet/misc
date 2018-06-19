namespace KillNicelyCmdProg
{
    partial class Form1
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
            this.Output = new System.Windows.Forms.RichTextBox();
            this.TestOne = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TestTwo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TestThree = new System.Windows.Forms.Button();
            this.panic = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TestFour = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(0, 148);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(605, 297);
            this.Output.TabIndex = 1;
            this.Output.Text = "";
            // 
            // TestOne
            // 
            this.TestOne.BackColor = System.Drawing.Color.Lime;
            this.TestOne.ForeColor = System.Drawing.Color.Black;
            this.TestOne.Location = new System.Drawing.Point(0, 3);
            this.TestOne.Name = "TestOne";
            this.TestOne.Size = new System.Drawing.Size(76, 23);
            this.TestOne.TabIndex = 2;
            this.TestOne.Text = "Scenario 1 - ";
            this.TestOne.UseVisualStyleBackColor = false;
            this.TestOne.Click += new System.EventHandler(this.TestOne_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(515, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start with visible window using .Net, hide with pinvoke, run for 6 seconds, show " +
    "with pinvoke, stop with .Net";
            // 
            // TestTwo
            // 
            this.TestTwo.BackColor = System.Drawing.Color.Red;
            this.TestTwo.Location = new System.Drawing.Point(0, 32);
            this.TestTwo.Name = "TestTwo";
            this.TestTwo.Size = new System.Drawing.Size(76, 23);
            this.TestTwo.TabIndex = 4;
            this.TestTwo.Text = "Scenario 2 - ";
            this.TestTwo.UseVisualStyleBackColor = false;
            this.TestTwo.Click += new System.EventHandler(this.TestTwo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(508, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Pinvoke only: Start with invisible window, get window handle, run for 6 seconds, " +
    "stop using window handle";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(376, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Brutal .Net: Start without window; run for 6 seconds; stop by killing the process" +
    "";
            // 
            // TestThree
            // 
            this.TestThree.BackColor = System.Drawing.Color.Lime;
            this.TestThree.Location = new System.Drawing.Point(0, 61);
            this.TestThree.Name = "TestThree";
            this.TestThree.Size = new System.Drawing.Size(76, 23);
            this.TestThree.TabIndex = 6;
            this.TestThree.Text = "Scenario 3 - ";
            this.TestThree.UseVisualStyleBackColor = false;
            this.TestThree.Click += new System.EventHandler(this.TestThree_Click);
            // 
            // panic
            // 
            this.panic.BackColor = System.Drawing.Color.Yellow;
            this.panic.Location = new System.Drawing.Point(0, 119);
            this.panic.Name = "panic";
            this.panic.Size = new System.Drawing.Size(196, 23);
            this.panic.TabIndex = 8;
            this.panic.Text = "Panic!";
            this.panic.UseVisualStyleBackColor = false;
            this.panic.Click += new System.EventHandler(this.panic_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(82, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(511, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Start without window using .Net, run for 6 seconds, stop by attaching console and" +
    " issuing ConsoleCtrlEvent";
            // 
            // TestFour
            // 
            this.TestFour.BackColor = System.Drawing.Color.Lime;
            this.TestFour.ForeColor = System.Drawing.Color.Black;
            this.TestFour.Location = new System.Drawing.Point(0, 90);
            this.TestFour.Name = "TestFour";
            this.TestFour.Size = new System.Drawing.Size(76, 23);
            this.TestFour.TabIndex = 9;
            this.TestFour.Text = "Scenario 4 - ";
            this.TestFour.UseVisualStyleBackColor = false;
            this.TestFour.Click += new System.EventHandler(this.TestFour_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 448);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TestFour);
            this.Controls.Add(this.panic);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TestThree);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TestTwo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TestOne);
            this.Controls.Add(this.Output);
            this.Name = "Form1";
            this.Text = "Killing It Softly";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Output;
        private System.Windows.Forms.Button TestOne;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button TestTwo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button TestThree;
        private System.Windows.Forms.Button panic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button TestFour;
    }
}

