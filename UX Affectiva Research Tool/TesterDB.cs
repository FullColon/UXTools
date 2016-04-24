using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using CsvHelper;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary.BinaryDrawingFormat;
using ExcelLibrary.BinaryFileFormat;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using TopSoft.ExcelExport;
using TopSoft.ExcelExport.Entity;
using TopSoft.ExcelExport.Attributes;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;


namespace UX_Affectiva_Research_Tool
{
    public class TesterDB
    {
        //SQLite objects
        Microsoft.Office.Interop.Excel.Application objApp;
        Microsoft.Office.Interop.Excel._Workbook objBook;
        System.Data.SQLite.SQLiteConnection sqlite_conn;
        System.Data.SQLite.SQLiteCommand sqlite_cmd;
        System.Data.SQLite.SQLiteDataReader sqlite_datareader;
        SQLiteDataAdapter sqlite_dataAdapter;
        public string filepath;
        string inputFile = "C:\\Users\\Wesley Osborn\\Desktop\\MyTextDB\\Teststuff.db";
        String connString;

        public string Emotion { get; set; }
        public int TimeStamp { get; set; }

        public void CreateNewDBConnection(String inputFile)
        {
            this.inputFile = inputFile;
            connString = String.Format("Data Source={0}", inputFile);
            sqlite_conn = new SQLiteConnection(connString);
            // sqlite_conn.Open();
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void NewTableCommand()
        {
            try
            {
                string createsql = "CREATE TABLE IF NOT EXISTS Emotions (emotion VARCHAR(20), Xvalue FLOAT, Yvalue FLOAT)";
                connString = String.Format("Data Source={0}", inputFile);
                SQLiteConnection newConn = new SQLiteConnection(connString);
                newConn.Open();
                SQLiteCommand newCom;

                using (SQLiteTransaction sqlTransaction = newConn.BeginTransaction())
                {
                    newCom = new SQLiteCommand(createsql, newConn);
                    newCom.ExecuteNonQuery();
                    newCom.Dispose();

                    sqlTransaction.Commit();
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }

        }
        //--------------------------------------------------------------------------------
        public void LoadChartFromDB(string inputFile )
        {
          //  inputFile = "C:\\Users\\Wesley Osborn\\Desktop\\MyTextDB\\Teststuff.db";
            connString = String.Format("Data Source={0}", inputFile);
            SQLiteConnection newConn = new SQLiteConnection(connString);
            newConn.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            DataSet dt = new DataSet();
            SQLiteDataAdapter sda = new SQLiteDataAdapter("SELECT * FROM Emotions", newConn);
            sda.Fill(dt);
            DataView dv = new DataView(dt.Tables[0]);
            //replace chart1 with name of chart trying to display to
            //chart1.Series[0].Points.DataBindXY(dv, "Xvalue", dv, "Yvalue");
        }
        //--------------------------------------------------------------------------------------------
        public void LoadToChart(ref Chart _chart, string inputFile)//from a series
        {

            connString = String.Format("Data Source={0}", inputFile);
            SQLiteConnection newConn = new SQLiteConnection(connString);
            SQLiteCommand newComm = new SQLiteCommand("Select * From Emotions", newConn);
            SQLiteDataReader sdr;
            try
            {
                newConn.Open();
                sdr = newComm.ExecuteReader();
                _chart.Series.Add(new Series("0"));
                _chart.Series.Add(new Series("1"));
                _chart.Series.Add(new Series("2"));
                _chart.Series.Add(new Series("3"));
                _chart.Series.Add(new Series("4"));
                _chart.Series.Add(new Series("5"));
                int i = 0;
                while (sdr.Read() && i < 6 )
                {          
                    //query out emotion column, xvalue column, yvalue column
                    string name = sdr.GetString(0);
                    if (name != "6")
                        _chart.Series[name].Points.AddXY(sdr.GetFloat(1), sdr.GetFloat(2));
                    else
                        name = "0";
                    i++;           
                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }


        }
        //-------------------------------------------------------------------------------------------------------------------
        public void PopulateNewTable(List<System.Windows.Forms.DataVisualization.Charting.Series> _newData)
        {
            connString = String.Format("Data Source={0}", inputFile);
            sqlite_conn = new SQLiteConnection(connString);
            sqlite_cmd = new SQLiteCommand(sqlite_conn);
            string sql;
            sqlite_conn.Open();
            for (var i = 0; i < _newData.Count; i++)
            {
                for (var j = 0; j < _newData[i].Points.Count; j++)
                {
                    sql = "INSERT INTO Emotions (emotion, Xvalue, Yvalue) VALUES ('" + i.ToString() + "','" + _newData[i].Points[j].XValue + "','" + _newData[i].Points[j].YValues[0] + "')";
                    sqlite_cmd.CommandText = sql;//"INSERT INTO Emotions (emotion, Xvalue, Yvalue) VALUES ('" + i.ToString() + "','" + _newData[i].Points[j].XValue + "','" + _newData[i].Points[j].YValues[0] + "')";
                    int rowsUpdated = sqlite_cmd.ExecuteNonQuery();
                }
            }
            System.Windows.Forms.MessageBox.Show("Saved");
        }
        //-------------------------------------------------------------------------------------------------------------------
        //prints out the whole table in meesage box/ can do console output
        public void ReadOutTable()//read out the whole table
        {
            sqlite_cmd.CommandText = "SELECT * FROM Emotions";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                string myReader = sqlite_datareader.GetString(0);
                System.Windows.MessageBox.Show(myReader);
            }
        }
        //----------------------------------------------------------------------
        //should be fastest bulk update
        //pass in the DB, and two variables to update with it (string, float , float)
        public void UpdateDB(string _emotion, float _xval, float _yval)//how do we want passed?
        {
            SQLiteTransaction trans;
            string SQL = "INSERT INTO Emotions (emotion, Xvalue, Yvalue) VALUES (emotions='" + _emotion + "',Xvalue='" + _xval + "', Yvalue='" + _yval + "',";
            SQLiteCommand myCMD = new SQLiteCommand(SQL);
            myCMD.Connection = sqlite_conn;
            sqlite_conn.Open();
            trans = sqlite_conn.BeginTransaction();
            int retval = 0;
            try
            {
                retval = myCMD.ExecuteNonQuery();
                if (retval == 1)
                {
                    System.Windows.Forms.MessageBox.Show("Row Inserted");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Row NOT Inserted");
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
            }
            finally
            {
                trans.Commit();
                myCMD.Dispose();
                sqlite_conn.Close();
            }
        }
        //-------------------------------------------------------------
        public void DeleteFromDB(string _toDelete)
        {  
            string delQuery = "DELETE FROM Emotions WHERE emotion='" + _toDelete + "'";

            using (sqlite_conn = new SQLiteConnection(connString))
            {
                sqlite_conn.Open();
                sqlite_cmd.CommandText = delQuery;
                sqlite_cmd.ExecuteNonQuery();
            }

        }
        //---------------------------------------------------------------------------------
        //can use without passing in a DGV
        public void TESTEXPORT(ref DataGridView _DVG) //chart passed in (database of info)
        {
            SQLiteCommand myCMD = new SQLiteCommand("SELECT * FROM Emotions", sqlite_conn);
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter();
                sda.SelectCommand = myCMD;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                _DVG.DataSource = bSource;
                sda.Update(dbdataset);


                //export to exxel file
                DataSet ds = new DataSet("New_DataSet");
                ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                sda.Fill(dbdataset);
                ds.Tables.Add(dbdataset);
                ExcelLibrary.DataSetHelper.CreateWorkbook("MyExcelFile.xls", ds);
                //end of export to excel file

            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            // end of DGV---------------------------------------------------------------------
        }
        //------------------------------------------------------------------------------------------------
        public void ExportingToCSV(DataGridView source)
        {
            SQLiteCommand myCommand = new SQLiteCommand();

            myCommand.CommandText = "SELECT * FROM Emotions";
            System.Data.DataTable data = new System.Data.DataTable();

            SQLiteDataAdapter myAdapter = new SQLiteDataAdapter(myCommand);
            myAdapter.Fill(data);
            source.DataSource = data;
            source.Refresh();

            string value = "";
            DataGridViewRow dr = new DataGridViewRow();
            StreamWriter swOut = new StreamWriter("C: \\Users\\Wesley Osborn\\Desktop\\DBtest");

            for (int i = 0; i <= source.Columns.Count - 1; i++)
            {
                if (i > 0)
                {
                    swOut.Write(",");
                }
                swOut.Write(source.Columns[i].HeaderText);
            }

            swOut.WriteLine();

            for (int j = 0; j <= source.Rows.Count - 1; j++)
            {
                if (j > 0)
                {
                    swOut.WriteLine();
                }
                dr = source.Rows[j];

                for (int i = 0; i <= source.Columns.Count - 1; i++)
                {
                    if (i > 0)
                    {
                        swOut.Write(",");
                    }

                    value = dr.Cells[i].Value.ToString();
                    value = value.Replace(',', ' ');
                    value = value.Replace(Environment.NewLine, " ");

                    swOut.Write(value);
                }
            }
            swOut.Close();
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void SaveToExcel(List<Microsoft.Office.Interop.Excel.Series> _series)//include another param for table name & filepath
        {
            System.Data.SQLite.SQLiteCommand cmdDataBase = new System.Data.SQLite.SQLiteCommand(" SELECT * FROM Emotions ;", sqlite_conn);
         
            try
            {
                System.Data.SQLite.SQLiteDataAdapter sda = new System.Data.SQLite.SQLiteDataAdapter();
                sda.SelectCommand = cmdDataBase;
                System.Data.DataTable dbdataset = new System.Data.DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;

                // _DGV.DataSource = bSource;

                sda.Update(dbdataset);

                //export to Excel
                DataSet ds = new DataSet("New_DataSet");
                ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                sda.Fill(dbdataset);
                ds.Tables.Add(dbdataset);
                ExcelLibrary.DataSetHelper.CreateWorkbook("MyExcelFile.xls", ds);

            }
            catch (Exception)
            {
                throw;
            }
        }
        //---------------------------------------------------------------------------------------------------------------
        //select specific items in the DataBase for viewing (console of MessageBox)
        public static DataTable ReadOut(string connectionString, string selectQuery)//Returns a DataTable
        {
            // string selectqueer = "SELECT'" + _lookFor + "'FROM '" + _readFrom + "'";
            var returnVal = new DataTable();
            var conn = new SQLiteConnection(connectionString);
            try
            {
                conn.Open();
                var command = new SQLiteCommand(selectQuery);

                using (var adapter = new SQLiteDataAdapter(command))
                {
                    adapter.Fill(returnVal);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return returnVal;
        }
        //--------------------------------------------------------------------------------------------------------
        public void ReadSpecific()//Reads out the whole database on console
        {
            string sql = "SELECT * FROM Emotions";
            SQLiteCommand command = new SQLiteCommand(sql, sqlite_conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Emotion: " + reader["emotion"] + "\txvalue: " + reader["Xvalue"] + "\tyvalue: " + reader["Yvalue"]);
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public static void WriteToCSV(DataTable _dataSource, string _fileOutPath, bool firstRowIsCoulmnHeader = false, string seperator = ";")
        {
            var sw = new StreamWriter(_fileOutPath, false);

            int icolcount = _dataSource.Columns.Count;
            if (!firstRowIsCoulmnHeader)
            {
                for (int i = 0; i < icolcount; i++)
                {
                    sw.Write(_dataSource.Columns[i]);
                    if (i < icolcount - 1)
                    {
                        sw.Write(seperator);
                    }
                }
                sw.Write(sw.NewLine);
            }

            foreach (DataRow drow in _dataSource.Rows)
            {
                for (int i = 0; i < icolcount; i++)
                {
                    if (!Convert.IsDBNull(drow[i]))
                    {
                        sw.Write(drow[i].ToString());
                    }
                    if (i < icolcount - 1)
                    {
                        sw.Write(seperator);
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
        public void CloseConnection()
        {
            sqlite_conn.Close();
        }
    }
}

