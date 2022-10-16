using MidtermExerciseFr.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermExerciseFr.DataAccess {
    public class CoursesDAO {

        private string databaseTableName = "dbo.Courses";
        public string datasetTableName = "Courses";
        private SqlDataAdapter coursesAdapter;
        private SqlConnection connection;
        private CoursesManager manager;

        public CoursesDAO(SqlConnection connection, CoursesManager manager) {
            this.connection = connection;
            this.coursesAdapter = CreateDataAdapter();
            this.manager = manager;
        }

        public void FillDataSet() {
            DataSet dataset = this.manager.GetDataSet();
            if (dataset.Tables.Contains(this.datasetTableName)) {
                dataset.Tables.Remove(this.datasetTableName);
            }

            this.connection.Open();
            this.coursesAdapter.Fill(dataset, this.datasetTableName);
            this.connection.Close();

            DataTable table = dataset.Tables[this.datasetTableName];
            table.Columns["id"].AutoIncrementSeed = 0;
            table.Columns["id"].AutoIncrementStep = -1;
            table.Columns["id"].ReadOnly = true;
            table.Columns["courseCode"].AllowDBNull = false;
            table.Columns["courseCode"].MaxLength = 50;
            table.Columns["name"].AllowDBNull = false;
            table.Columns["description"].AllowDBNull = true;
            table.Columns["dateCreated"].AllowDBNull = true;
            table.Columns["dateUpdated"].AllowDBNull = true;
            table.Columns["dateDeleted"].AllowDBNull = true;
            table.Columns["dateCreated"].ReadOnly = true;
            table.Columns["dateUpdated"].ReadOnly = true;
            table.Columns["dateDeleted"].ReadOnly = true;

        }

        public void UpdateDataSet() {
            DataSet dataset = this.manager.GetDataSet();
            this.connection.Open();
            this.coursesAdapter.Update(dataset, this.datasetTableName);
            this.connection.Close();
            DataTable table = dataset.Tables[this.datasetTableName];
            table.AcceptChanges();

        }

        private SqlDataAdapter CreateDataAdapter() {

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            SqlCommand selectCommand = new SqlCommand($"SELECT * FROM {this.databaseTableName};", this.connection);

            SqlCommand insertCommand = new SqlCommand($"INSERT INTO {this.databaseTableName} " +
                $"(courseCode, name, description, dateCreated, dateUpdated) " +
                $"VALUES (@courseCode, @name, @description, @dateCreated, @dateUpdated); " +
                $"SELECT * FROM {this.databaseTableName} WHERE id = SCOPE_IDENTITY();", this.connection);
            insertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
            insertCommand.Parameters.Add("@courseCode", SqlDbType.NVarChar, 50, "courseCode");
            insertCommand.Parameters.Add("@name", SqlDbType.Text, -1, "name");
            insertCommand.Parameters.Add("@description", SqlDbType.Text, -1, "description");
            insertCommand.Parameters.Add("@dateCreated", SqlDbType.DateTime);
            insertCommand.Parameters.Add("@dateUpdated", SqlDbType.DateTime);

            SqlCommand updateCommand = new SqlCommand($"UPDATE {this.databaseTableName} SET " +
                $"courseCode = @courseCode, " +
                $"name = @name, " +
                $"description = @description, " +
                $"dateUpdated = @dateUpdated " +
                $"WHERE id = @whereId AND dateUpdated = @oldDataUpdated;", this.connection);
            updateCommand.Parameters.Add("@courseCode", SqlDbType.NVarChar, 50, "courseCode");
            updateCommand.Parameters.Add("@name", SqlDbType.Text, -1, "name");
            updateCommand.Parameters.Add("@description", SqlDbType.Text, -1, "description");
            updateCommand.Parameters.Add("@dateUpdated", SqlDbType.DateTime);
            updateCommand.Parameters.Add("@whereId", SqlDbType.Int, 4, "id");
            updateCommand.Parameters.Add("@oldDataUpdated", SqlDbType.DateTime, 6, "dateUpdated").SourceVersion = DataRowVersion.Original;

            SqlCommand deleteCommand = new SqlCommand($"DELETE FROM {this.databaseTableName} WHERE id = @id;", this.connection);
            deleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id");

            dataAdapter.SelectCommand = selectCommand;
            dataAdapter.InsertCommand = insertCommand;
            dataAdapter.UpdateCommand = updateCommand;
            dataAdapter.DeleteCommand = deleteCommand;

            dataAdapter.RowUpdating += new SqlRowUpdatingEventHandler(OnRowUpdating);
            dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(OnRowUpdated);

            return dataAdapter;
        }


        private void OnRowUpdating(object sender, SqlRowUpdatingEventArgs args) {
            if (args.StatementType == StatementType.Insert) {
                DateTime maintenant = DateTime.Now;
                args.Command.Parameters["@dateCreated"].Value = maintenant;
                args.Command.Parameters["@dateUpdated"].Value = maintenant;

            } else if (args.StatementType == StatementType.Update) {
                args.Command.Parameters["@dateUpdated"].Value = DateTime.Now;

            }
        }

        private void OnRowUpdated(object sender, SqlRowUpdatedEventArgs args) {
            if (args.StatementType == StatementType.Insert) {
                args.Status = UpdateStatus.SkipCurrentRow;

            } else if (args.StatementType == StatementType.Update) {
                if (args.RecordsAffected == 0) {
                    throw new Exception("Modification concurrente détectée!");
                }
            }
        }
    }
}
