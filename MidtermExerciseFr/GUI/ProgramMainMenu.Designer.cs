namespace MidtermExerciseFr.GUI {
    partial class ProgramMainMenu {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonOpenCourses = new System.Windows.Forms.Button();
            this.buttonStudentsWindow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(300, 254);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(207, 31);
            this.buttonExit.TabIndex = 0;
            this.buttonExit.Text = "Quitter Application";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonOpenCourses
            // 
            this.buttonOpenCourses.Location = new System.Drawing.Point(300, 119);
            this.buttonOpenCourses.Name = "buttonOpenCourses";
            this.buttonOpenCourses.Size = new System.Drawing.Size(207, 31);
            this.buttonOpenCourses.TabIndex = 1;
            this.buttonOpenCourses.Text = "Gestion des cours";
            this.buttonOpenCourses.UseVisualStyleBackColor = true;
            this.buttonOpenCourses.Click += new System.EventHandler(this.buttonOpenCourses_Click);
            // 
            // buttonStudentsWindow
            // 
            this.buttonStudentsWindow.Location = new System.Drawing.Point(300, 156);
            this.buttonStudentsWindow.Name = "buttonStudentsWindow";
            this.buttonStudentsWindow.Size = new System.Drawing.Size(207, 31);
            this.buttonStudentsWindow.TabIndex = 2;
            this.buttonStudentsWindow.Text = "Gestion des étudiants";
            this.buttonStudentsWindow.UseVisualStyleBackColor = true;
            this.buttonStudentsWindow.Click += new System.EventHandler(this.buttonStudentsWindow_Click);
            // 
            // ProgramMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonStudentsWindow);
            this.Controls.Add(this.buttonOpenCourses);
            this.Controls.Add(this.buttonExit);
            this.Name = "ProgramMainMenu";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonOpenCourses;
        private System.Windows.Forms.Button buttonStudentsWindow;
    }
}

