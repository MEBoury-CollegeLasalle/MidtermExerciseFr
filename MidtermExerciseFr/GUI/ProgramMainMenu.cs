using MidtermExerciseFr.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidtermExerciseFr.GUI {
    public partial class ProgramMainMenu : Form {

        private ProgramManager progManager;

        public ProgramMainMenu(ProgramManager pManager) {
            this.progManager = pManager;
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e) {
            this.progManager.ExitProgram();
        }

        private void buttonOpenCourses_Click(object sender, EventArgs e) {
            this.progManager.OpenCoursesWindow();
        }

        private void buttonStudentsWindow_Click(object sender, EventArgs e) {
            this.progManager.OpenStudentsWindow();
        }
    }
}
