using MidtermExerciseFr.GUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidtermExerciseFr.Business {
    public class ProgramManager {

        private ProgramMainMenu menuWindow;
        private DataSet applicationDataSet;
        private SqlConnection connection;
        private CoursesManager coursesManager;
        private StudentsManager studentsManager;

        public ProgramManager() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            this.menuWindow = new ProgramMainMenu(this);
            this.applicationDataSet = new DataSet();
            this.connection = new SqlConnection("Server=.\\SQL2019EXPRESS;Integrated security=true;Database=db_demo1;");
            this.coursesManager = new CoursesManager(this.connection, this.applicationDataSet);
            this.studentsManager = new StudentsManager(this.connection, this.applicationDataSet);
        }

        public void OpenMainMenu() {
            Application.Run(this.menuWindow);
        }

        public void ExitProgram() {
            Application.Exit();
        }

        public void OpenCoursesWindow() {
            this.coursesManager.OpenCoursesWindow();
        }

        public void OpenStudentsWindow() {
            this.studentsManager.OpenStudentsWindow();
        }
    }
}
