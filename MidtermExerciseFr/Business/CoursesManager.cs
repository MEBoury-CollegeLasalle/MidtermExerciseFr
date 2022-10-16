using MidtermExerciseFr.DataAccess;
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
    public class CoursesManager {

        private CoursesForm coursesWindow;
        private CoursesDAO coursesDAO;
        private DataSet applicationDataSet;

        public CoursesManager(SqlConnection connection, DataSet dataset) {
            this.coursesWindow = new CoursesForm(this);
            this.coursesDAO = new CoursesDAO(connection, this);
            this.applicationDataSet = dataset;
        }

        public void OpenCoursesWindow() {
            this.coursesWindow.ShowDialog();
        }

        public void CloseCoursesWindow() {
            this.coursesWindow.DialogResult = DialogResult.Cancel;
        }

        public void LoadAndDisplayCoursesData() {
            this.coursesDAO.FillDataSet();
            DataTable courseTable = this.applicationDataSet.Tables[this.coursesDAO.datasetTableName];
            this.coursesWindow.BindTableToGridView(courseTable);
        }

        public void UpdateCoursesInDatabase() {
            this.coursesDAO.UpdateDataSet();
        }

        public DataSet GetDataSet() {
            return this.applicationDataSet;
        }
    }
}
