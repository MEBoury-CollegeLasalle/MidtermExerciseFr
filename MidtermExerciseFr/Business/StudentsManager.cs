using MidtermExerciseFr.DataAccess;
using MidtermExerciseFr.GUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermExerciseFr.Business {
    public class StudentsManager {

        private StudentsForm studentsWindow;
        private StudentsDAO studentsDAO;
        private DataSet applicationDataSet;

        public StudentsManager(SqlConnection conn, DataSet dataset) { 
            this.applicationDataSet = dataset;
            this.studentsWindow = new StudentsForm(this);
            this.studentsDAO = new StudentsDAO(conn, this);

        }

        public void OpenStudentsWindow() {
            this.studentsWindow.ShowDialog();
        }

        public void CloseStudentsWindow() {
            this.studentsWindow.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public void LoadAndDisplayStudentsData() {
            this.studentsDAO.FillDataSet();
            this.studentsWindow.BindTableToGridView(this.applicationDataSet.Tables[this.studentsDAO.datasetTableName]);
        }

        public void SaveModificationsToData() {
            this.studentsDAO.UpdateDataSet();
        }

        public DataSet GetDataSet() {
            return this.applicationDataSet;
        }

    }
}
