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
    public partial class StudentsForm : Form {

        private StudentsManager studentsManager;

        public StudentsForm(StudentsManager stManager) {
            this.studentsManager = stManager;
            InitializeComponent();
        }

        public void BindTableToGridView(DataTable table) {
            this.dataGridView1.DataSource = table;
            this.dataGridView1.Refresh();
        }

        private void buttonLoad_Click(object sender, EventArgs e) {
            this.studentsManager.LoadAndDisplayStudentsData();
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            this.studentsManager.SaveModificationsToData();
        }

        private void buttonFermer_Click(object sender, EventArgs e) {
            this.studentsManager.CloseStudentsWindow();
        }
    }


}
