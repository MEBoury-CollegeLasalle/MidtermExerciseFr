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
    public partial class CoursesForm : Form {

        private CoursesManager coursesManager;

        public CoursesForm(CoursesManager coursesManager) {
            this.coursesManager = coursesManager;
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e) {
            this.coursesManager.CloseCoursesWindow();
        }

        private void buttonLoad_Click(object sender, EventArgs e) {
            this.coursesManager.LoadAndDisplayCoursesData();
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            this.coursesManager.UpdateCoursesInDatabase();
        }

        public void BindTableToGridView(DataTable courseTable) {
            this.dataGridView1.DataSource = courseTable;
            this.dataGridView1.Refresh();
        }
    }
}
