using MidtermExerciseFr.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermExerciseFr.DataAccess {
    public class StudentsDAO {

        private string databaseTableName = "dbo.Students";
        public string datasetTableName = "Students";
        private SqlDataAdapter dataAdapter;
        private SqlConnection connection;
        private StudentsManager manager;

        public StudentsDAO(SqlConnection connection, StudentsManager manager) {
            this.connection = connection;
            this.dataAdapter = CreateDataAdapter();
            this.manager = manager;
        }


        public void FillDataSet() {
            DataSet dataset = this.manager.GetDataSet();
            if (dataset.Tables.Contains(this.datasetTableName)) {
                dataset.Tables.Remove(this.datasetTableName);
            }

            this.connection.Open();
            this.dataAdapter.Fill(dataset, this.datasetTableName);
            this.connection.Close();

            DataTable studentsTable = dataset.Tables[this.datasetTableName];
            studentsTable.Columns["studentCode"].AutoIncrementSeed = 0;
            studentsTable.Columns["studentCode"].AutoIncrementStep = -1;
            studentsTable.Columns["studentCode"].ReadOnly = true;
            studentsTable.Columns["firstName"].AllowDBNull = false;
            studentsTable.Columns["lastName"].AllowDBNull = false;
            studentsTable.Columns["dateCreated"].AllowDBNull = true;
            studentsTable.Columns["dateUpdated"].AllowDBNull = true;
            studentsTable.Columns["dateDeleted"].AllowDBNull = true;
            studentsTable.Columns["dateCreated"].ReadOnly = true;
            studentsTable.Columns["dateUpdated"].ReadOnly = true;
            studentsTable.Columns["dateDeleted"].ReadOnly = true;
        }

        public void UpdateDataSet() {

            DataSet dataset = this.manager.GetDataSet();
            this.connection.Open();
            this.dataAdapter.Update(dataset, this.datasetTableName);
            this.connection.Close();
            DataTable studentsTable = dataset.Tables[this.datasetTableName];
            studentsTable.AcceptChanges();
        }


        private SqlDataAdapter CreateDataAdapter() {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            SqlCommand selectCommand = new SqlCommand($"SELECT * FROM {this.databaseTableName};", this.connection);

            SqlCommand insertCommand = new SqlCommand($"INSERT INTO {this.databaseTableName} " +
                $"(firstName, lastName, dateCreated, dateUpdated) " +
                $"VALUES (@firstName, @lastName, @dateCreated, @dateUpdated); " +
                $"SELECT * FROM {this.databaseTableName} WHERE studentCode = SCOPE_IDENTITY();", this.connection);
            insertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
            insertCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 50, "firstName");
            insertCommand.Parameters.Add("@lastName", SqlDbType.NVarChar, 50, "lastName");
            insertCommand.Parameters.Add("@dateCreated", SqlDbType.DateTime);
            insertCommand.Parameters.Add("@dateUpdated", SqlDbType.DateTime);

            SqlCommand updateCommand = new SqlCommand($"UPDATE {this.databaseTableName} SET " +
                $"firstName = @firstName, " +
                $"lastName = @lastName, " +
                $"dateUpdated = @dateUpdated " +
                $"WHERE studentCode = @studentCode AND dateUpdated = @oldDateUpdated;", this.connection);
            updateCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 50, "firstName");
            updateCommand.Parameters.Add("@lastName", SqlDbType.NVarChar, 50, "lastName");
            updateCommand.Parameters.Add("@dateUpdated", SqlDbType.DateTime);
            updateCommand.Parameters.Add("@studentCode", SqlDbType.Int, 4, "studentCode");
            updateCommand.Parameters.Add("@oldDateUpdated", SqlDbType.DateTime, 6, "dateUpdated").SourceVersion = DataRowVersion.Original;

            SqlCommand deleteCommand = new SqlCommand($"DELETE FROM {this.databaseTableName} WHERE studentCode = @studentCode;", this.connection);
            deleteCommand.Parameters.Add("@studentCode", SqlDbType.Int, 4, "studentCode");

            dataAdapter.SelectCommand = selectCommand;
            dataAdapter.InsertCommand = insertCommand;
            dataAdapter.UpdateCommand = updateCommand;
            dataAdapter.DeleteCommand = deleteCommand;

            dataAdapter.RowUpdating += new SqlRowUpdatingEventHandler(OnRowUpdating);
            dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(OnRowUpdated);


            return dataAdapter;
        }

        private void OnRowUpdating(object sender, SqlRowUpdatingEventArgs arguments) {
            if (arguments.StatementType == StatementType.Insert) {
                DateTime maintenant = DateTime.Now;
                arguments.Command.Parameters["@dateCreated"].Value = maintenant;
                arguments.Command.Parameters["@dateUpdated"].Value = maintenant;

            } else if (arguments.StatementType == StatementType.Update) {
                arguments.Command.Parameters["@dateUpdated"].Value = DateTime.Now;
            }
        }

        private void OnRowUpdated(object sender, SqlRowUpdatedEventArgs arguments) {
            if (arguments.StatementType == StatementType.Insert) {
                arguments.Status = UpdateStatus.SkipCurrentRow;

            } else if (arguments.StatementType == StatementType.Update) {
                if (arguments.RecordsAffected == 0) {
                    throw new Exception("Modification concurrente détectée!");
                }
            }
        }
    }
}
