// File Cabinet version 1.0 completed afternoon, Monday, November 6, 2017.
// Program copyright (c) 2017, Douglas Michael Singer.  All rights reserved.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
namespace FileCabinet
{
    public partial class Form1 : Form
    {
        // variables to load a SQL database file
        static string defaultDbName = "SQLFileCabinetDB.mdf";
        string dbPath;
        static string dbPathFile = "SQLFileCabinetDefaultPath.txt";
        string dbPathFileErrorCode = "001";
        bool editing = false;
        bool editingRecord = false;

        // variables for SQL database connection
        SqlConnection fcConnection;
        SqlCommand recordsCommand;
        SqlCommand categoriesCommand;
        SqlCommand producersCommand;
        SqlCommand tagsCommand;
        SqlDataAdapter recordsAdapter;
        SqlDataAdapter categoriesAdapter;
        SqlDataAdapter producersAdapter;
        SqlDataAdapter tagsAdapter;

        // strings to help with tag generation
        static string tCharsLeft = "0123456789abcdefgh";
        static string tCharsRight = "ijklmnopqrstuvwxyz";
        static string tagFailCode = "000";
        static string firstTag = "0i";
        static string lastTag = "hz";
        static int maxTags = 10;

        // string to aid calendar control
        string calendarFillsWhich;

        // to remember current position when editing
        int curPos = 0;

        // string to help with logging
        string debugQuery;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            log("Program launched");
            SetState("Lock");
            // try to load the default file.  Fail?  Try to prompt user for file.  Fail?  Lock most user controls.
            string tempPath = getDbDefaultPath();
            dbPath = tempPath != dbPathFileErrorCode ? tempPath : "C:\\Program Files\\FileCabinet\\" + defaultDbName;
            lblCurrentPath.Text = "Database path = " + dbPath;
            connectToDb();
        }
        private void connectToDb()
        {
            try
            {
                // connect to FileCabinet database`
                fcConnection = new SqlConnection($"Data Source=.\\SQLEXPRESS; AttachDbFilename={dbPath}; User Instance=True; Integrated Security=True; Connection Timeout=10;");
                fcConnection.Open();
                log("Connected to database");

                // load tables and bind controls
                loadRecords();
                loadProducers();
                loadCategories();
                loadTags();

                log("Data bindings succeeded.");
                SetState("View");
                doChangeSelection();
                grdRecords.Focus();
            }
            catch (Exception ex)
            {
                SetState("Lock");
                log("Database connection error: " + ex.Message);
                if (MessageBox.Show("There was a problem connecting to the database! You may need to resolve one of the following issues." + Environment.NewLine + Environment.NewLine + "1. Make sure the database file is located in the default directory path" + Environment.NewLine + "2. Wait for SQL Server to spin up and try again" + Environment.NewLine + Environment.NewLine + "The program generated this error message:" + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine + "Would you like to reattempt the connection?", "Error connecting to database!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    connectToDb();
                }
                else
                {
                    // perform tasks when database connection fails -- for now, nothing
                }
            }
        }
        private void closeDb()
        {
            try
            {
                fcConnection.Close();
                fcConnection.Dispose();
                recordsCommand.Dispose();
                categoriesCommand.Dispose();
                producersCommand.Dispose();
                tagsCommand.Dispose();
                recordsAdapter.Dispose();
                categoriesAdapter.Dispose();
                producersAdapter.Dispose();
                tagsAdapter.Dispose();
            }
            catch (Exception ex)
            {
                log("Couldn't close database because " + ex.Message);
            }
        }
        private void loadRecords(string qry = "SELECT * FROM Records")
        {
            recordsCommand = new SqlCommand(qry, fcConnection);
            recordsAdapter = new SqlDataAdapter();
            recordsAdapter.SelectCommand = recordsCommand;
            DataTable recordsTable = new DataTable();
            recordsAdapter.Fill(recordsTable);
            txtDate.DataBindings.Add("Text", recordsTable, "RecordDate");
            txtRecordTitle.DataBindings.Add("Text", recordsTable, "Title");
            txtRecordText.DataBindings.Add("Text", recordsTable, "RecordText");
            txtRecordSum.DataBindings.Add("Text", recordsTable, "RecordSum");
            picRecords.DataBindings.Add("Tag", recordsTable, "ImagePath");
            grdRecords.DataSource = recordsTable;

            // make some columns invisible so we don't see every bit of data
            // and set widths of others
            grdRecords.Columns[0].HeaderText = "ID";
            grdRecords.Columns[0].Width = 60;
            grdRecords.Columns[1].Visible = false;
            grdRecords.Columns[2].Visible = false;
            grdRecords.Columns[3].Visible = false;
            grdRecords.Columns[4].HeaderText = "Title";
            grdRecords.Columns[4].Width = 70;
            grdRecords.Columns[5].HeaderText = "Sum";
            grdRecords.Columns[5].Width = 60;
            grdRecords.Columns[6].HeaderText = "Text";
            grdRecords.Columns[6].Width = 65;
            grdRecords.Columns[7].HeaderText = "Date";
            grdRecords.Columns[7].Width = 56;
            grdRecords.Columns[8].Visible = false;

            // recordsManager = (CurrencyManager)this.BindingContext[recordsTable];
        }
        private void unloadRecords()
        {
            recordsAdapter = new SqlDataAdapter();
            recordsAdapter.Dispose();
            DataTable recordsTable = new DataTable();
            recordsTable.Dispose();
            txtRecordTitle.DataBindings.Clear();
            txtRecordSum.DataBindings.Clear();
            txtRecordText.DataBindings.Clear();
            txtDate.DataBindings.Clear();
            picRecords.DataBindings.Clear();
        }
        private void loadProducers()
        {
            producersCommand = new SqlCommand("SELECT * FROM Producers ORDER BY Name", fcConnection);
            producersAdapter = new SqlDataAdapter();
            producersAdapter.SelectCommand = producersCommand;
            DataTable producersTable = new DataTable();
            producersAdapter.Fill(producersTable);
            grdProducers.DataSource = producersTable;
             setProducersButtons(); // activate edit buttons if we have data in the checkboxlist

            // set column visibility and width
            grdProducers.Columns[0].Visible = false;
            grdProducers.Columns[1].Visible = false;
            grdProducers.Columns[2].Width = 225;
            grdProducers.Columns[3].Visible = false;
        }
        private void unloadProducers()
        {
            producersAdapter = new SqlDataAdapter();
            producersAdapter.Dispose();
            DataTable producersTable = new DataTable();
            producersTable.Dispose();
        }
        private void loadCategories()
        {
            categoriesCommand = new SqlCommand("SELECT * FROM Categories ORDER BY Name", fcConnection);
            categoriesAdapter = new SqlDataAdapter();
            categoriesAdapter.SelectCommand = categoriesCommand;
            DataTable categoriesTable = new DataTable();
            categoriesAdapter.Fill(categoriesTable);
            grdCategories.DataSource = categoriesTable;
            setCategoriesButtons(); // activate edit buttons if we have data in the checkboxlist

            // set column visibility and width
            grdCategories.Columns[0].Visible = false;
            grdCategories.Columns[1].Width = 225;
            grdCategories.Columns[2].Visible = false;
        }
        private void unloadCategories()
        {
            categoriesAdapter = new SqlDataAdapter();
            categoriesAdapter.Dispose();
            DataTable categoriesTable = new DataTable();
            categoriesTable.Dispose();
        }
        private void loadTags()
        {
            tagsCommand = new SqlCommand("SELECT * FROM Tags ORDER BY Name", fcConnection);
            tagsAdapter = new SqlDataAdapter();
            tagsAdapter.SelectCommand = tagsCommand;
            DataTable tagsTable = new DataTable();
            tagsAdapter.Fill(tagsTable);
            grdTags.DataSource = tagsTable;
            setTagsButtons(); // activate edit buttons if we have data in the checkboxlist

            // set column visibility and width
            grdTags.Columns[0].Visible = false;
            grdTags.Columns[1].Width = 225;
            grdTags.Columns[2].Visible = false;
        }
        private void unloadTags()
        {
            tagsAdapter = new SqlDataAdapter();
            tagsAdapter.Dispose();
            DataTable tagsTable = new DataTable();
            tagsTable.Dispose();
        }
        private DataTable formatQuery(string query, SqlConnection connection) // common SQL procedure turned into a simple function
        {
            // establish command object
            SqlCommand tempCommand = new SqlCommand(query, connection);

            // create adapter and table to execute command
            SqlDataAdapter tempAdapter = new SqlDataAdapter();
            tempAdapter.SelectCommand = tempCommand;
            DataTable tempTable = new DataTable();
            tempAdapter.Fill(tempTable);

            // return table
            return tempTable;
        }
        private void SetState(string state)
        {
            switch (state)
            {
                case "Record Edit":
                    // state "Record Edit" allows user access to some necessary buttons and text fiels
                    SetState("Edit");

                    // buttons
                    btnCancel.Enabled = true;
                    btnImagePath.Enabled = true;
                    btnSave.Enabled = true;
                    btnUnlinkImage.Enabled = (string)picRecords.Tag == "" ? false : true;
                    btnCategoriesClear.Enabled = true;
                    btnProducersClear.Enabled = true;
                    btnTagsClear.Enabled = true;
                    

                    // drag drop
                    this.AllowDrop = true;

                    // grid
                    grdProducers.Enabled = true;
                    grdCategories.Enabled = true;
                    grdTags.Enabled = true;

                    // image
                    picRecords.Enabled = false;

                    // text boxes
                    txtDate.Enabled = true;
                    txtRecordSum.Enabled = true;
                    txtRecordText.Enabled = true;
                    txtRecordTitle.Enabled = true;
                    break;
                case "Edit":
                    // state "Edit" restricts everything except editing controls
                    // first "Lock"
                    SetState("Lock");

                    // then make a few changes from "Lock" state
                    // buttons
                    btnExit.Enabled = false;
                    btnLoadFile.Enabled = false;
                    break;
                case "Lock":
                    // state "Lock" restricts everything except the Exit and Load Database File buttons

                    // buttons
                    btnAdd.Enabled = false;
                    btnCancel.Enabled = false;
                    btnCategoriesAdd.Enabled = false;
                    btnCategoriesAll.Enabled = false;
                    btnCategoriesClear.Enabled = false;
                    btnCategoriesDelete.Enabled = false;
                    btnCategoriesEdit.Enabled = false;
                    btnCategoriesView.Enabled = false;
                    btnDate.Enabled = false;
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                    btnExit.Enabled = true;
                    btnImagePath.Enabled = false;
                    btnLimit.Enabled = false;
                    btnLoadFile.Enabled = true;
                    btnProducersAdd.Enabled = false;
                    btnProducersAll.Enabled = false;
                    btnProducersClear.Enabled = false;
                    btnProducersDelete.Enabled = false;
                    btnProducersEdit.Enabled = false;
                    btnProducersView.Enabled = false;
                    btnSave.Enabled = false;
                    btnSearch.Enabled = false;
                    btnSetDefault.Enabled = false;
                    btnTagsAdd.Enabled = false;
                    btnTagsAll.Enabled = false;
                    btnTagsClear.Enabled = false;
                    btnTagsDelete.Enabled = false;
                    btnTagsEdit.Enabled = false;
                    btnTagsView.Enabled = false;
                    btnTotal.Enabled = false;
                    btnUnlinkImage.Enabled = false;
                    btnViewAll.Enabled = false;

                    // check box
                    chkQuickDelete.Enabled = false;
                    chkAutoClear.Enabled = false;

                    // combo box
                    cboView.Enabled = false;
                  
                    // drag drop
                    this.AllowDrop = false;

                    // grid
                    grdCategories.Enabled = false;
                    grdProducers.Enabled = false;
                    grdRecords.Enabled = false;
                    grdTags.Enabled = false;

                    // image
                    picRecords.Enabled = false;

                    // text boxes
                    txtDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    txtRecordSum.Enabled = false;
                    txtRecordText.Enabled = false;
                    txtRecordTitle.Enabled = false;
                    txtSearch.Enabled = false;
                    txtStartDate.Enabled = false;
                    break;
                default:
                case "View":
                    // "View" is the most lenient state, allowing access to most features except edit features
                    setProducersButtons();
                    setCategoriesButtons();
                    setTagsButtons();
                    setEnvironmentButtons(); 
                    break;
            }
        }
        private bool displayDescriptionBox(string name, string description)
        {
            return MessageBox.Show(description, "Description of " + name, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK ? true: false;
        }
        private bool displayDeleteBox(string table, string name)
        {
            return MessageBox.Show("Really delete entry " + name + " from " + table + "?" + Environment.NewLine + "This action cannot be reversed.", "About to delete " + name + " from " + table + "!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK ? true : false;
        }
        private void log(string txt)
        {
            // program log
            txtProgramOutput.AppendText(Environment.NewLine);
            txtProgramOutput.AppendText(DateTime.Now.ToString() + "\t" + txt);
        }
        private void startEdit()
        {
            // common editor startup tasks
            SetState("Edit");
            log("Initialized editor task " + EditorWindow.Table + " " + EditorWindow.Task);
        }
        private void endEdit()
        {
            // common editor close tasks
            SetState("View");
            log("Completed editor task with code " + EditorWindow.ReturnState + " (editor task " + EditorWindow.Table + " " + EditorWindow.Task + ")"); 
        }
        private string pullId(string table, string id)
        {
            DataGridViewRow row;
            switch (table)
            { 
                // credit to Habib on Stackoverflow for LINQ function

                case "Producers":
                   row = grdProducers.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells[0].Value.ToString().Equals(id))
                        .First();
                    return grdProducers.Rows[row.Index].Cells[2].Value.ToString();
                case "Categories":
                    row = grdCategories.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells[0].Value.ToString().Equals(id))
                        .First();
                    return grdCategories.Rows[row.Index].Cells[1].Value.ToString();
                case "Tags":
                     row = grdTags.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells[0].Value.ToString().Equals(id))
                        .First();
                    return grdTags.Rows[row.Index].Cells[1].Value.ToString();
                default:
                    return "";
            }
        } 

        private void addRow(string table, string tag = "")
        {
            SqlCommand tempCommand;
            if (tag == "")
            {
                tempCommand = new SqlCommand("insert into " + table + "(Name, Description) values (@title, @description)", fcConnection); 
                tempCommand.Parameters.Add(new SqlParameter("title", EditorWindow.Title));
                tempCommand.Parameters.Add(new SqlParameter("description", EditorWindow.Description));
            }
            else
            {
                tempCommand = new SqlCommand("insert into " + table + "(TagID, Name, Description) values (@tag, @title, @description)", fcConnection); 
                tempCommand.Parameters.Add(new SqlParameter("tag", tag)); 
                tempCommand.Parameters.Add(new SqlParameter("title", EditorWindow.Title));
                tempCommand.Parameters.Add(new SqlParameter("description", EditorWindow.Description));
            }
            debugQuery = tempCommand.CommandText;
            try
            {
                tempCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding record!", "Unable to perform database update!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log("Database error adding row: " + ex.Message + Environment.NewLine + "Using query: " + debugQuery);
            }
        }
        private void addRecordRow(string producer, string category, string taghash, decimal sum)
        {
            SqlCommand tempCommand = new SqlCommand("insert into Records(ProducerID, CategoryID, TagHash, Title, RecordSum, RecordText, RecordDate, ImagePath) values (@producer, @category, @taghash, @title, @sum, @text, @date, @imagepath)", fcConnection);
            tempCommand.Parameters.Add(new SqlParameter("producer", producer));
            tempCommand.Parameters.Add(new SqlParameter("category", category));
            tempCommand.Parameters.Add(new SqlParameter("taghash", taghash));
            tempCommand.Parameters.Add(new SqlParameter("title", txtRecordTitle.Text));
            tempCommand.Parameters.Add(new SqlParameter("sum", sum));
            tempCommand.Parameters.Add(new SqlParameter("text", txtRecordText.Text));
            tempCommand.Parameters.Add(new SqlParameter("date", txtDate.Text));
            tempCommand.Parameters.Add(new SqlParameter("imagepath", picRecords.Tag));

            debugQuery = tempCommand.CommandText;
            try
            {
                tempCommand.ExecuteNonQuery();
                MessageBox.Show("The record was successfully added to the database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                log("Successfully added record" + Environment.NewLine + "Using query: " + debugQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding record!", "Unable to perform database update!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log("Database error adding row: " + ex.Message + Environment.NewLine + "Using query: " + debugQuery);
            }
        }
        private void deleteRow(string table, string column, string matchValue, string tempString = "")
        {
            SqlCommand tempCommand;
            if (table == "Tags")
            {
                // need a slightly different query
                tempCommand = new SqlCommand("DELETE FROM " + table + " WHERE " + column + " = " + "'" + matchValue + "'", fcConnection);
            }
            else
            {
                tempCommand = new SqlCommand("DELETE FROM " + table + " WHERE " + column + " = " + matchValue, fcConnection);
            }
            debugQuery = tempCommand.CommandText;
            try
            {
                tempCommand.ExecuteNonQuery();
                log("Successfully deleted '" + tempString + "' from '" + table + "'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record!", "Unable to perform database update!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log("Database error deleting row: " + ex.Message + Environment.NewLine + "Using query: " + debugQuery);
            }
        }
        private void editRow(string table, string name, string description, string column, string matchValue)
        {
            SqlCommand tempCommand = new SqlCommand("UPDATE " + table + " SET Name = @name, Description = @description WHERE " + column + " = " + matchValue, fcConnection);
            tempCommand.Parameters.Add(new SqlParameter("name", name));
            tempCommand.Parameters.Add(new SqlParameter("description", description));

            debugQuery = tempCommand.CommandText;
            try
            {
                tempCommand.ExecuteNonQuery();
                log("Successfully edited '" + name + "' in '" + table + "'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing record!", "Unable to perform database update!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log("Database error editing row: " + ex.Message + Environment.NewLine + "Using query: " + debugQuery);
            }
        }
        private void editRecordRow(string id, string producer, string category, string taghash, decimal sum)
        {
            SqlCommand tempCommand = new SqlCommand("UPDATE Records SET ProducerID = @producer, CategoryID = @category, TagHash = @taghash, RecordSum = @sum, Title = @title, RecordText = @text, RecordDate = @date, ImagePath = @imagepath WHERE RecordID = @id", fcConnection);
            tempCommand.Parameters.Add(new SqlParameter("id", id));
            tempCommand.Parameters.Add(new SqlParameter("producer", producer));
            tempCommand.Parameters.Add(new SqlParameter("category", category));
            tempCommand.Parameters.Add(new SqlParameter("taghash", taghash));
            tempCommand.Parameters.Add(new SqlParameter("title", txtRecordTitle.Text));
            tempCommand.Parameters.Add(new SqlParameter("sum", sum));
            tempCommand.Parameters.Add(new SqlParameter("text", txtRecordText.Text));
            tempCommand.Parameters.Add(new SqlParameter("date", txtDate.Text));
            tempCommand.Parameters.Add(new SqlParameter("imagepath", picRecords.Tag));
            debugQuery = tempCommand.CommandText;
            try
            {
                tempCommand.ExecuteNonQuery();
                log("Successfully edited '" + txtRecordTitle.Text + "' in 'Records'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing record!", "Unable to perform database update!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log("Database error editing row: " + ex.Message + Environment.NewLine + "Using query: " + debugQuery);
            }
        }
        private void setEnvironmentButtons()
        {
            // always active when not editing
            btnExit.Enabled = true;
            btnLoadFile.Enabled = true;

            // always inactive when not editing
            btnCancel.Enabled = false;
            btnImagePath.Enabled = false;
            btnSave.Enabled = false;
            btnUnlinkImage.Enabled = false;
            monthCalendar1.Visible = false;

            // activate if there is a database in the path
            if (dbPath != "")
            {
                // buttons
                btnAdd.Enabled = true;
                btnSetDefault.Enabled = true;

                // checkboxes
                chkQuickDelete.Enabled = true;
                chkAutoClear.Enabled = true;

                // drag drop
                this.AllowDrop = true;

                // image
                picRecords.Enabled = true;
            }

            if (grdRecords.Rows.Count > 0)
            {
                // buttons
                btnDate.Enabled = true;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
                btnLimit.Enabled = true;
                btnSearch.Enabled = true;
                btnTotal.Enabled = true;
                btnViewAll.Enabled = true;

                // combo
                cboView.Enabled = true;

                // grid
                grdRecords.Enabled = true;

                // text boxes
                txtEndDate.Enabled = true;
                txtRecordSum.Enabled = false;
                txtRecordText.Enabled = false;
                txtRecordTitle.Enabled = false;
                txtSearch.Enabled = true;
                txtStartDate.Enabled = true;

                // need to focus on each gridview
                grdProducers.Rows[0].Selected = true;
                grdCategories.Rows[0].Selected = true;
                grdTags.Rows[0].Selected = true;
            }
            else
            {
                // buttons
                btnDate.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                btnLimit.Enabled = false;
                btnImagePath.Enabled = false;
                btnSearch.Enabled = false;
                btnTotal.Enabled = false;

                // combo
                cboView.Enabled = false;

                // grid
                grdRecords.Enabled = false;

                // image
                picRecords.Enabled = false;

                // text boxes
                txtDate.Enabled = false;
                txtEndDate.Enabled = false;
                txtRecordSum.Enabled = false;
                txtRecordText.Enabled = false;
                txtRecordTitle.Enabled = false;
                txtSearch.Enabled = false;
                txtStartDate.Enabled = false;
            }
        }
        private void setProducersButtons()
        {
            // activate if there is a database in the path
            if (dbPath != "")
            {
                btnProducersAdd.Enabled = true;
            }

            if (grdProducers.Rows.Count > 0)
            {
                // activate buttons
                btnProducersView.Enabled = true;
                btnProducersAll.Enabled = true;
                btnProducersClear.Enabled = true;
                btnProducersDelete.Enabled = true;
                btnProducersEdit.Enabled = true;

                // activate grid
                grdProducers.Enabled = true;
            }
            else
            {
                // there is nothing to select, so disabled these buttons
                btnProducersView.Enabled = false;
                btnProducersAll.Enabled = false;
                btnProducersClear.Enabled = false;
                btnProducersDelete.Enabled = false;
                btnProducersEdit.Enabled = false;
                
                // deactivate grid
                grdProducers.Enabled = false;
            }
        }
        private void setCategoriesButtons()
        {
            // activate if there is a database in the path
            if (dbPath != "")
            {
                btnCategoriesAdd.Enabled = true;
            }

            if (grdCategories.Rows.Count > 0)
            {
                // activate buttons
                btnCategoriesView.Enabled = true;
                btnCategoriesAll.Enabled = true;
                btnCategoriesClear.Enabled = true;
                btnCategoriesDelete.Enabled = true;
                btnCategoriesEdit.Enabled = true;
                
                // activate grid
                grdCategories.Enabled = true;
            }
            else
            {
                // there is nothing to select, so disabled these buttons
                btnCategoriesView.Enabled = false;
                btnCategoriesAll.Enabled = false;
                btnCategoriesClear.Enabled = false;
                btnCategoriesDelete.Enabled = false;
                btnCategoriesEdit.Enabled = false;
                
                // deactivate grid
                grdCategories.Enabled = false;
            }
        }
        private void setTagsButtons()
        {
            // activate if there is a database in the path
            if (dbPath != "")
            {
                btnTagsAdd.Enabled = true;
            }

            if (grdTags.Rows.Count > 0)
            {
                // activate buttons
                btnTagsView.Enabled = true;
                btnTagsAll.Enabled = true;
                btnTagsClear.Enabled = true;
                btnTagsDelete.Enabled = true;
                btnTagsEdit.Enabled = true;
                
                // activate grid
                grdTags.Enabled = true;
            }
            else
            {
                // there is nothing to select, so disabled these buttons
                btnTagsView.Enabled = false;
                btnTagsAll.Enabled = false;
                btnTagsClear.Enabled = false;
                btnTagsDelete.Enabled = false;
                btnTagsEdit.Enabled = false;
                
                // deactivate grid
                grdTags.Enabled = false;
            }
        }
        private string generateTag(string tag)
        {
            if (tag != lastTag) // in which case we would have no tags left to assign!
            {
                char c1 = tag[0];
                char c2 = tag[1];

                // if the second character has reached the end of the string, increment first character and reset second
                // if not, increment second character and leave first alone
                c1 = tCharsRight.IndexOf(c2) == tCharsRight.Length - 1 ? tCharsLeft[tCharsLeft.IndexOf(c1) + 1] : c1;
                c2 = tCharsRight.IndexOf(c2) == tCharsRight.Length - 1 ? tCharsRight[0] : tCharsRight[tCharsRight.IndexOf(c2) + 1];

                return c1.ToString() + c2.ToString();
            }
            else
            {
                return tagFailCode; // code to warn calling function that we are out of tags!
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                string debugQuery = "SELECT * from RECORDS WHERE Title LIKE '" + txtSearch.Text + "%'";
                log("Initialized search query");
                try
                {
                    unloadRecords();
                    loadRecords(debugQuery);
                }
                catch (Exception Ex)
                {
                    log("Search query failed." + Environment.NewLine + "Using query: " + debugQuery + Environment.NewLine + "Message: " + Ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter a search string to perform search.", "Unable to complete operation.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log("Could not search records because no search string was entered."); 
            }
            
        }
        private void btnDate_Click(object sender, EventArgs e)
        {
            string debugQuery = "";
            string taskFlag = txtStartDate.Text == "" ? (txtEndDate.Text == "" ? "No" : "End") : (txtEndDate.Text == "" ? "Start" : "Both");
            switch (taskFlag)
            {
                case "End":
                    debugQuery = "SELECT * from Records WHERE RecordDate < '" + DateTime.Parse(txtEndDate.Text).Date + "'";
                    break;
                case "Start":
                    debugQuery = "SELECT * from Records WHERE RecordDate > '" + DateTime.Parse(txtStartDate.Text).Date + "'";
                    break;
                case "Both":
                    // compare dates to check range
                    if (DateTime.Parse(txtStartDate.Text).Date > DateTime.Parse(txtEndDate.Text).Date)
                    {
                        txtStartDate.Text = txtEndDate.Text;
                    }
                    debugQuery = "SELECT * from Records WHERE RecordDate > '" + DateTime.Parse(txtStartDate.Text).Date + "' AND RecordDate < '" + DateTime.Parse(txtEndDate.Text).Date + "'";
                   break;
                default: // "No" do nothing
                    break;
            }
            if (taskFlag != "No")
            {
                // attempt to limit records
                log("Initialized limit to date range");
                try
                {
                    unloadRecords();
                    loadRecords(debugQuery);
                }
                catch (Exception Ex)
                {
                    log("Date limit failed." + Environment.NewLine + "Using query: " + debugQuery + Environment.NewLine + "Message: " + Ex.Message);
                }
            }
            else 
            {
                MessageBox.Show("Please enter a date in one or both fields to limit record selection by date.", "Unable to complete operation.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log("Could not limit records because no date was entered."); 
            }
       }
        private void btnTotal_Click(object sender, EventArgs e)
        {
            if (grdRecords.Rows.Count <= 0)
            {
                MessageBox.Show("Impossible to total sums because no records exist!", "Operation unsuccessful.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                decimal s, sum;
                sum = 0;
                for (int i = 0; i < grdRecords.Rows.Count; i++)
                {
                    decimal.TryParse(grdRecords.Rows[i].Cells[5].Value.ToString(), out s);
                    sum += s;
                }
                lblTotal.Text = sum.ToString();
            }
        }
        private void btnProducersAll_Click(object sender, EventArgs e)
        {
            grdProducers.SelectAll();
        }
        private void btnProducersClear_Click(object sender, EventArgs e)
        {
            grdProducers.ClearSelection();
        }
        private void btnProducersView_Click(object sender, EventArgs e)
        {
            bool did = false; // hack to prepare to display a message if no items were checked

            for (int i = grdProducers.SelectedRows.Count - 1; i > -1; i--)
            {
                did = true;
                if (displayDescriptionBox(grdProducers.Rows[grdProducers.SelectedRows[i].Index].Cells[2].Value.ToString(), grdProducers.Rows[grdProducers.SelectedRows[i].Index].Cells[3].Value.ToString()))
                {
                    // do nothing -- let loop continue 
                }
                else
                {
                    // user canceled description view, break out of loop
                    break;
                }
            }
            if (!did)
            {
                MessageBox.Show("Impossible to view description because no items were selected!", "Unable to complete request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnProducersDelete_Click(object sender, EventArgs e)
        {
            log("Initialized action Delete 'Producers'.");

            if (grdProducers.SelectedRows.Count < 0)
            { 
                // there is nothing to delete because nothing was selected
                MessageBox.Show("Impossible to delete entries from 'Producers' because no items were selected!", "Unable to complete request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                for (int i = grdProducers.SelectedRows.Count - 1; i > -1; i--)
                {   
                    string temp = grdProducers.Rows[grdProducers.SelectedRows[i].Index].Cells[2].Value.ToString();

                    // if quick delete enabled, skip delete confirmation and just delete
                    if (chkQuickDelete.Checked == true)
                    {
                        // remove the record and log
                        deleteRow("Producers", "ProducerID", grdProducers.Rows[grdProducers.SelectedRows[i].Index].Cells[0].Value.ToString(), temp);
                    }
                    else
                    {
                        // confirm that user wants to delete record
                        if (displayDeleteBox("'Producers'", "'" + temp + "'"))
                        {
                            // remove the record and log
                            deleteRow("Producers", "ProducerID", grdProducers.Rows[grdProducers.SelectedRows[i].Index].Cells[0].Value.ToString(), temp);
                        }
                        else
                        {
                            // user canceled delete, break out of loop
                            log("User canceled action Delete 'Producers.'");
                            break;
                        }
                    }
                }
                // clean up tasks 
                log("Exited action Delete 'Producers'.");
                unloadProducers();
                loadProducers();
                setProducersButtons();
            }
        }
        private void btnProducersEdit_Click(object sender, EventArgs e)
        {
            log("Initialized action Edit 'Producers'.");

            // first, make sure something is selected or checked at all
            if (grdProducers.SelectedRows.Count < 0)
            {
                // there is nothing to edit because nothing was selected
                MessageBox.Show("Impossible to edit entries from 'Producers' because no items were selected!", "Unable to complete request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                EditorWindow.Task = grdProducers.SelectedRows.Count > 1 ? "Multiple" : "Single";
                EditorWindow.Table = "Producers";
                startEdit();
                for (int i = grdProducers.SelectedRows.Count - 1; i > -1; i--)
                {
                    int j = grdProducers.SelectedRows[i].Index; 
                    EditorWindow.Title = grdProducers.Rows[j].Cells[2].Value.ToString();
                    EditorWindow.Description = grdProducers.Rows[j].Cells[3].Value.ToString();
                    EditorWindow.Display();
                    if (EditorWindow.CloseState == "Save")
                    {
                        editRow("Producers", EditorWindow.Title, EditorWindow.Description, "ProducerID", grdProducers.Rows[j].Cells[0].Value.ToString());
                    }
                    else
                    {
                        // editor window was canceled, break out of loop
                        log("User canceled action Edit 'Producers.'");
                        break;
                    }
                }
                // clean up tasks
                log("Exited action Edit 'Producers'."); 
                unloadProducers();
                loadProducers();
                endEdit();
            }
        }
        private void btnProducersAdd_Click(object sender, EventArgs e)
        {
            log("Initialized action Add 'Producers'.");
            EditorWindow.Task = "Add";
            EditorWindow.Table = "Producers";
            EditorWindow.RemainingRecs = 1;
            startEdit();
            while (EditorWindow.RemainingRecs > 0)
            {
                EditorWindow.Title = "";
                EditorWindow.Description = "";
                EditorWindow.Display();
                if (EditorWindow.CloseState == "Save")
                {
                    addRow("Producers");
                }
                else
                {
                    // editor window was canceled, do nothing
                }
            }
            // clean up tasks
            log("Exited action Add 'Producers'."); 
            unloadProducers();
            loadProducers();
            endEdit();
            setProducersButtons();
        }
        private void btnCategoriesAll_Click(object sender, EventArgs e)
        {
            grdCategories.SelectAll();
        }
        private void btnCategoriesClear_Click(object sender, EventArgs e)
        {
            grdCategories.ClearSelection();
        }
        private void btnCategoriesView_Click(object sender, EventArgs e)
        {
            bool did = false; // hack to prepare to display a message if no items were checked

            for (int i = grdCategories.SelectedRows.Count - 1; i > -1; i--)
            {
                did = true;
                if (displayDescriptionBox(grdCategories.Rows[grdCategories.SelectedRows[i].Index].Cells[1].Value.ToString(), grdCategories.Rows[grdCategories.SelectedRows[i].Index].Cells[2].Value.ToString()))
                {
                    // do nothing -- let loop continue 
                }
                else
                {
                    // user canceled description view, break out of loop
                    break;
                }
            }
            if (!did)
            {
                MessageBox.Show("Impossible to view description because no items were selected!", "Unable to complete request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnCategoriesDelete_Click(object sender, EventArgs e)
        {
            log("Initialized action Delete 'Categories'.");

            if (grdCategories.SelectedRows.Count < 0)
            {
                // there is nothing to delete because nothing was selected
                MessageBox.Show("Impossible to delete entries from 'Categories' because no items were selected!", "Unable to complete request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                for (int i = grdCategories.SelectedRows.Count - 1; i > -1; i--)
                {
                    string temp = grdCategories.Rows[grdCategories.SelectedRows[i].Index].Cells[2].Value.ToString();

                    // if quick delete enabled, skip delete confirmation and just delete
                    if (chkQuickDelete.Checked == true)
                    {
                        // remove the record and log
                        deleteRow("Categories", "CategoryID", grdCategories.Rows[grdCategories.SelectedRows[i].Index].Cells[0].Value.ToString(), temp);
                    }
                    else
                    {
                        // confirm that user wants to delete record
                        if (displayDeleteBox("'Categories'", "'" + temp + "'"))
                        {
                            // remove the record and log
                            deleteRow("Categories", "CategoryID", grdCategories.Rows[grdCategories.SelectedRows[i].Index].Cells[0].Value.ToString(), temp);
                        }
                        else
                        {
                            // user canceled delete, break out of loop
                            log("User canceled action Delete 'Categories.'");
                            break;
                        }
                    }
                }
                // clean up tasks 
                log("Exited action Delete 'Categories'.");
                grdCategories.ClearSelection();
                unloadCategories();
                loadCategories();
                setCategoriesButtons();
            }
        }
        private void btnCategoriesEdit_Click(object sender, EventArgs e)
        {
            log("Initialized action Edit 'Categories'.");

            // first, make sure something is selected or checked at all
            if (grdCategories.SelectedRows.Count < 0)
            {
                // there is nothing to edit because nothing was selected
                MessageBox.Show("Impossible to edit entries from 'Categories' because no items were selected!", "Unable to complete request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                EditorWindow.Task = grdCategories.SelectedRows.Count > 1 ? "Multiple" : "Single";
                EditorWindow.Table = "Categories";
                startEdit();
                for (int i = grdCategories.SelectedRows.Count - 1; i > -1; i--)
                {
                    int j = grdCategories.SelectedRows[i].Index;
                    EditorWindow.Title = grdCategories.Rows[j].Cells[1].Value.ToString();
                    EditorWindow.Description = grdCategories.Rows[j].Cells[2].Value.ToString();
                    EditorWindow.Display();
                    if (EditorWindow.CloseState == "Save")
                    {
                        editRow("Categories", EditorWindow.Title, EditorWindow.Description, "CategoryID", grdCategories.Rows[j].Cells[0].Value.ToString());
                    }
                    else
                    {
                        // editor window was canceled, break out of loop
                        log("User canceled action Edit 'Categories.'");
                        break;
                    }
                }
                // clean up tasks
                log("Exited action Edit 'Categories'.");
                unloadCategories();
                loadCategories();
                endEdit();
            }
        }
        private void btnCategoriesAdd_Click(object sender, EventArgs e)
        {
            log("Initialized action Add 'Categories'.");
            EditorWindow.Task = "Add";
            EditorWindow.Table = "Categories";
            EditorWindow.RemainingRecs = 1;
            startEdit();
            while (EditorWindow.RemainingRecs > 0)
            {
                EditorWindow.Title = "";
                EditorWindow.Description = "";
                EditorWindow.Display();
                if (EditorWindow.CloseState == "Save")
                {
                    addRow("Categories");
                }
                else
                {
                    // editor window was canceled, do nothing
                }
            }
            // clean up tasks
            log("Exited action Add 'Categories'.");
            unloadCategories();
            loadCategories();
            endEdit();
            setCategoriesButtons();
        }
        private void btnTagsAll_Click(object sender, EventArgs e)
        {
            grdTags.SelectAll();
        }
        private void btnTagsClear_Click(object sender, EventArgs e)
        {
            grdTags.ClearSelection();
        }
        private void btnTagsView_Click(object sender, EventArgs e)
        {
            bool did = false; // hack to prepare to display a message if no items were checked

            for (int i = grdTags.SelectedRows.Count - 1; i > -1; i--)
            {
                did = true;
                if (displayDescriptionBox(grdTags.Rows[grdTags.SelectedRows[i].Index].Cells[1].Value.ToString(), grdTags.Rows[grdTags.SelectedRows[i].Index].Cells[2].Value.ToString()))
                {
                    // do nothing -- let loop continue 
                }
                else
                {
                    // user canceled description view, break out of loop
                    break;
                }
            }
            if (!did)
            {
                MessageBox.Show("Impossible to view description because no items were selected!", "Unable to complete request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnTagsDelete_Click(object sender, EventArgs e)
        {
            log("Initialized action Delete 'Tags'.");

            if (grdTags.SelectedRows.Count < 0)
            {
                // there is nothing to delete because nothing was selected
                MessageBox.Show("Impossible to delete entries from 'Tags' because no items were selected!", "Unable to complete request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                for (int i = grdTags.SelectedRows.Count - 1; i > -1; i--)
                {
                    string temp = grdTags.Rows[grdTags.SelectedRows[i].Index].Cells[2].Value.ToString();

                    // if quick delete enabled, skip delete confirmation and just delete
                    if (chkQuickDelete.Checked == true)
                    {
                        // remove the record and log
                        deleteRow("Tags", "TagID", grdTags.Rows[grdTags.SelectedRows[i].Index].Cells[0].Value.ToString(), temp);
                    }
                    else
                    {
                        // confirm that user wants to delete record
                        if (displayDeleteBox("'Tags'", "'" + temp + "'"))
                        {
                            // remove the record and log
                            deleteRow("Tags", "TagID", grdTags.Rows[grdTags.SelectedRows[i].Index].Cells[0].Value.ToString(), temp);
                        }
                        else
                        {
                            // user canceled delete, break out of loop
                            log("User canceled action Delete 'Tags.'");
                            break;
                        }
                    }
                }
                // clean up tasks 
                log("Exited action Delete 'Tags'.");
                grdTags.ClearSelection();
                unloadTags();
                loadTags();
                setTagsButtons();
            }
        }
        private void btnTagsEdit_Click(object sender, EventArgs e)
        {
            log("Initialized action Edit 'Tags'.");

            // first, make sure something is selected or checked at all
            if (grdTags.SelectedRows.Count < 0)
            {
                // there is nothing to edit because nothing was selected
                MessageBox.Show("Impossible to edit entries from 'Tags' because no items were selected!", "Unable to complete request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                EditorWindow.Task = grdTags.SelectedRows.Count > 1 ? "Multiple" : "Single";
                EditorWindow.Table = "Tags";
                startEdit();
                for (int i = grdTags.SelectedRows.Count - 1; i > -1; i--)
                {
                    int j = grdTags.SelectedRows[i].Index;
                    EditorWindow.Title = grdTags.Rows[j].Cells[1].Value.ToString();
                    EditorWindow.Description = grdTags.Rows[j].Cells[2].Value.ToString();
                    EditorWindow.Display();
                    if (EditorWindow.CloseState == "Save")
                    {
                        editRow("Tags", EditorWindow.Title, EditorWindow.Description, "TagID", grdTags.Rows[j].Cells[0].Value.ToString());
                    }
                    else
                    {
                        // editor window was canceled, break out of loop
                        log("User canceled action Edit 'Tags.'");
                        break;
                    }
                }
                // clean up tasks
                log("Exited action Edit 'Tags'.");
                unloadTags();
                loadTags();
                endEdit();
            }
        }
        private void btnTagsAdd_Click(object sender, EventArgs e)
        {
            log("Initialized action Add 'Tags'.");
            EditorWindow.Task = "Add";
            EditorWindow.Table = "Tags";
            EditorWindow.RemainingRecs = 1;
            startEdit();

            // important to hold current tag in a variable so we don't have to reload database when adding multiple tags
            string currentTag;

            // see if we are adding the first tag and set to current record tag if not
            currentTag = grdTags.Rows.Count == 0 ? "" : grdTags.Rows[grdTags.Rows.Count - 1].Cells[0].Value.ToString();
           
            // if we cannot assign a new tag, exit function and display warning message
            if (currentTag == lastTag)
            {
                doTagsFull();
            }
            else
            {
                while (EditorWindow.RemainingRecs > 0)
                {       
                    if (currentTag == lastTag)
                    {
                        doTagsFull();
                        break;
                    }
                    EditorWindow.Title = "";
                    EditorWindow.Description = "";
                    EditorWindow.Display();
                    if (EditorWindow.CloseState == "Save")
                    {
                        // /if we are adding the first tag, make currentTag the first tag.  Otherwise, generate the next one.
                        currentTag = currentTag == "" ? firstTag : generateTag(currentTag);
                        if (currentTag == tagFailCode)
                        {
                            doTagsFull();
                            break;
                        }
                        else
                        {
                            addRow("Tags", currentTag);
                        }
                    }
                    else
                    {
                        // editor window was canceled, do nothing
                    }
                }
                // clean up tasks
                log("Exited action Add 'Tags'.");
                unloadTags();
                loadTags();
                endEdit();
                setTagsButtons();
            }
        }
        private void doTagsFull()
        {
            // we cannot add any more tags, the database is full
            MessageBox.Show("The tag database is full, unable to add another tag!", "Error adding tag", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            log("The tag database is full.");
            log("Exited action Add 'Tags'.");
            unloadTags();
            loadTags();
            endEdit();
            setTagsButtons();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            log("Initialized action Delete 'Records'.");

            // first, make sure something is selected or checked at all
            if (grdRecords.CurrentCell.RowIndex < 0 || grdRecords.Rows.Count == 0)
            {
                // there is nothing to delete because nothing was selected
                MessageBox.Show("Impossible to delete entry from 'Records' because no item is selected, or grid is empty!", "Unable to complete request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // if so, initialize delete process 
            else
            {
                int j = grdRecords.CurrentCell.RowIndex;
                string temp = grdRecords.Rows[j].Cells[4].Value.ToString();

                // if quick delete enabled, skip delete confirmation and just delete
                if (chkQuickDelete.Checked == true)
                {
                    // remove the record and log
                    deleteRow("Records", "RecordID", grdRecords.Rows[j].Cells[0].Value.ToString(), temp);
                }
                else
                {
                    // confirm that user wants to delete record
                    if (displayDeleteBox("'Records'", "'" + temp + "'"))
                    {
                        // remove the record and log
                        deleteRow("Records", "RecordID", grdRecords.Rows[j].Cells[0].Value.ToString(), temp);
                    }
                    else
                    {
                        // user canceled delete, don't delete
                        log("User canceled action Delete 'Records.'");
                    }
                }
                
                // clean up tasks 
                unloadRecords();
                loadRecords();
                int scrollTo = j > grdRecords.Rows.Count - 1 ? grdRecords.Rows.Count - 1 : j;
                log("Exited action Delete 'Records'.");
                SetState("View");
                grdRecords.FirstDisplayedScrollingRowIndex = scrollTo;
                grdRecords.Rows[scrollTo].Selected = true;
                grdRecords.Focus();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            log("Initialized edit record.");
            // first, make sure something is selected or checked at all
            if (grdRecords.CurrentCell.RowIndex < 0 || grdRecords.Rows.Count == 0)
            {
                // there is nothing to edit because nothing was selected
                MessageBox.Show("Impossible to edit entry from 'Records' because no item is selected, or grid is empty!", "Unable to complete request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // if so, initialize edit process 
            else
            {
                curPos = grdRecords.CurrentCell.RowIndex; // remember current position
                SetState("Record Edit");
                editing = true;
                editingRecord = true;
                txtRecordTitle.Focus();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            log("Initialized add record.");
            windowAddRecord();
        }
        private void windowAddRecord()
        {
            SetState("Record Edit");
            curPos = grdRecords.CurrentCell.RowIndex > -1 ? grdRecords.CurrentCell.RowIndex : 0;
            editing = true;
            if (chkAutoClear.Checked == true)
            {
                // clear fields
                txtRecordTitle.Text = "";
                txtRecordSum.Text = "";
                txtRecordText.Text = "";
                txtDate.Text = "";
            }
            txtRecordTitle.Focus();
        }
        private string makeHash()
        {
            if (grdTags.SelectedRows.Count > 0)
            {
                string hashBuilder = "";
                for (int i = grdTags.SelectedRows.Count - 1; i > -1; i--)
                {
                    hashBuilder += grdTags.Rows[grdTags.SelectedRows[i].Index].Cells[0].Value.ToString();
                }
                return hashBuilder;
            }
            else  
            {
                // nothing was selected in tags checklistbox, return an empty string
                return "";
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnUnlinkImage_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Unlink the image from the record?  This action cannot be undone!", "About to unlink image", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                picRecords.Image = null;
                picRecords.Tag = "";
            }
        }
        private void btnImagePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg|*.jpeg|*.jpe|*.jfif|*.png|*.gif|*.tif|*.tiff|*.bmp|*.dib|*.rle|All Files|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                picRecords.LoadAsync(dialog.FileName);
                picRecords.Tag = dialog.FileName;
            }
        }
        private void chkQuickDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQuickDelete.Checked == true)
            {
                if (MessageBox.Show("Warning: Enabling quick delete means that the program will not ask you to confirm"
                + " record deletion! Only enable this option if you are an advanced user.  You could accidentally delete your data!  Proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    chkQuickDelete.Checked = false;
                }
            }
        }
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            string tempPath = getDbPath();
            string oldPath = dbPath;
            dbPath = tempPath != dbPathFileErrorCode ? tempPath : dbFileLoadError(dbPath); 
            SetState(dbPath == "" ? "Lock" : "View");
            if (dbPath == "")
            {
                MessageBox.Show("There was no database file in the selected folder. You may want to try again.", "Error locating file!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (dbPath == oldPath)
            {
                MessageBox.Show("There was no database file in the selected folder. You may want to try again.", "Error locating file!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // if the program is loading a new file, reconnect to database

                // close the old connection if needed
                if (oldPath != "")
                {
                    closeDb();
                }
                connectToDb();
            }
            lblCurrentPath.Text = "Database path = " + dbPath;
        }
        private string dbFileLoadError(string path = "")
        {
            log("Failed to open database file because no database file exists at that path.");
            return path; // this allows the function to return either "" or current file path in case of a load fail
        }
        private string getDbPath()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var newPath = folderBrowserDialog1.SelectedPath;
                log("Attempting to load file " + newPath + "\\" + defaultDbName);

                // if the file exists, return the path, if not, return an error code
                return File.Exists(newPath + "\\" + defaultDbName) ? newPath + "\\" + defaultDbName : dbPathFileErrorCode;
            }
            else
            {
                return dbPathFileErrorCode;
            }
        }
        private string getDbDefaultPath()
        {
            if (File.Exists(dbPathFile))
            {
                using (StreamReader sr = File.OpenText(dbPathFile))
                {
                    string newPath = sr.ReadLine();
                    log("Attempting to load file " + newPath); 
                    // if the file exists, return the path, if not, return an error code
                    return File.Exists(newPath) ? newPath : dbPathFileErrorCode;
                }
            }
            else
            {
                return dbPathFileErrorCode;
            }
        }
        private void setDefaultDbPath(string newPath)
        {
            if (File.Exists(dbPathFile))
            {
                File.Delete(dbPathFile); 
            }
            using (FileStream fs = File.Create(dbPathFile))
            {
                Byte[] line = new UTF8Encoding(true).GetBytes(newPath);
                fs.Write(line, 0, line.Length);
            }
        }
        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            log("Initialized set default path to current path.");
            try
            {
                setDefaultDbPath(dbPath);
                log("Successfully set current path as default."); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to set the default path!", "Error changing path.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log("Set default path failed!" + Environment.NewLine + "Message: " + ex.Message); 
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editing) // see if the user really meant to close window and lose edits
            {
                if (doCancelEdit())
                {
                    if (dbPath != "")
                    {
                        closeDb();
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (dbPath != "")
                {
                    closeDb();
                }
            }
        }
        private bool returnKey(KeyPressEventArgs e)
        {
            return (e.KeyChar == 13);
        }
        private void txtRecordText_TextChanged(object sender, EventArgs e)
        {
            label10.Text = "Record Text (" + (txtRecordText.MaxLength - txtRecordText.TextLength) + " characters remaining)"; 
        }
        private void txtRecordText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (returnKey(e))
            {
                txtRecordSum.Focus();
            }
        }
        private void txtRecordSum_TextChanged(object sender, EventArgs e)
        {
            if (txtRecordSum.Text.Contains("."))
            {
                txtRecordSum.MaxLength = 10;
            }
            else
            {
                txtRecordSum.MaxLength = 9;
            }
            label9.Text = "Sum (" + (txtRecordSum.MaxLength - txtRecordSum.TextLength) + " digits remaining)"; 
        }
        private void txtRecordSum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (returnKey(e))
            {
                txtDate.Focus();
            }
            Regex regexSumString;
            if (txtRecordSum.Text.Contains("."))
            {
                // allow no more than two decimals, prevent a second '.'
                regexSumString = (txtRecordSum.Text.IndexOf(".") < txtRecordSum.TextLength - 2) ? new Regex(@"^[\b]$") : new Regex(@"^[0-9\b]$");
                // vestige
                // allow no more than two decimals, prevent a second '.', and prevent '.' as last character of string
                // regexSumString = ((txtRecordSum.Text.IndexOf(".") < txtRecordSum.TextLength - 2) && (txtRecordSum.TextLength == txtRecordSum.MaxLength - 1)) ? new Regex(@"^$") : new Regex(@"^[0-9\b\d]$");
            }
            else
            {
                // if we don't have a '.' yet, use "lenient" regular expression
                regexSumString = new Regex(@"^[0-9\b.]$");
            }
            if (regexSumString.IsMatch(Convert.ToString(e.KeyChar)))
            {
                // acceptable keystrokes
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                Console.Beep();
            }
        }
        private void txtRecordTitle_TextChanged(object sender, EventArgs e)
        {
            label8.Text = "Title (" + (txtRecordTitle.MaxLength - txtRecordTitle.TextLength) + " remaining)"; 
        }
        private void txtRecordTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (returnKey(e))
            {
                txtRecordText.Focus();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (doCancelEdit())
            {
                if (!editingRecord) // get rid of picture 
                {
                    picRecords.Image = null;
                    picRecords.Tag = "";
                }
                grdRecords.Focus();
                editingRecord = false;
                editing = false;
                SetState("View");
            }
            else
            {
                // do nothing, let editing continue
            }
        }
        private bool doCancelEdit()
        {
            return (MessageBox.Show("You will lose unsaved edits!  Really cancel?", "Cancel edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes); 
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            bool ableToAdd = true;
            if (grdProducers.SelectedRows.Count > 1)
            {
                ableToAdd = false;
                message = "Unable to add a new record because each record is allowed to link to only one Producer. Make sure you have 1 or 0 boxes selected in the Producers list and try again." + Environment.NewLine;
            }
            if (grdCategories.SelectedRows.Count > 1)
            {
                ableToAdd = false;
                message += "Unable to add a new record because each record is allowed to link to only one Category. Make sure you have 1 or 0 boxes selected in the Categories list and try again." + Environment.NewLine;
            }
            if (grdTags.SelectedRows.Count > maxTags)
            {
                ableToAdd = false;
                message = "Unable to add a new record because each record is allowed to link to only ten Tags. Make sure you have 10 or less boxes selected in the Tags list and try again.";
            }
            if (ableToAdd)
            {
                // validate sum box to make sure the '.' didn't end up at the end
                if (txtRecordSum.TextLength > 0 && (txtRecordSum.Text[txtRecordSum.TextLength - 1] == '.'))
                {
                    txtRecordSum.Text = txtRecordSum.Text.TrimEnd('.');
                }
                // make sure picture tag path is not null
                if (picRecords.Image == null)
                {
                    picRecords.Tag = "";
                }
                Decimal d;
                switch (editingRecord)
                {
                    case true:
                        // find out if we have producers, categories or tags, and pass to addRecordRow
                        if (!decimal.TryParse(txtRecordSum.Text, out d))
                        {
                            d = 0;
                        }
                        editRecordRow(grdRecords.Rows[grdRecords.SelectedRows[0].Index].Cells[0].Value.ToString(),
                            grdProducers.SelectedRows.Count > 0 ? grdProducers.Rows[grdProducers.SelectedRows[0].Index].Cells[0].Value.ToString() : "-1",
                            grdCategories.SelectedRows.Count > 0 ? grdCategories.Rows[grdCategories.SelectedRows[0].Index].Cells[0].Value.ToString() : "-1",
                            makeHash(), d);

                        // done editing, clean up
                        unloadRecords();
                        loadRecords();
                        editing = false;
                        editingRecord = false;
                        log("Ended action edit record");
                        grdRecords.FirstDisplayedScrollingRowIndex = curPos;
                        grdRecords.Rows[curPos].Selected = true;
                        grdRecords.Focus();
                        SetState("View");

                        curPos = 0;
                        break;
                    default: // case false
                             // find out if we have producers, categories or tags, and pass to addRecordRow
                        if (!decimal.TryParse(txtRecordSum.Text, out d))
                        {
                            d = 0;
                        }
                        // this complicated statement is a workaround for a bug that was happening when user tried to add a record before clicking in one of the grid boxes
                        addRecordRow(grdProducers.SelectedRows.Count > 0 ? grdProducers.Rows[grdProducers.SelectedRows[0].Index].Cells[0].Value.ToString() : "-1",
                            grdCategories.SelectedRows.Count > 0 ? grdCategories.Rows[grdCategories.SelectedRows[0].Index].Cells[0].Value.ToString() : "-1",
                            makeHash(), d);
                        // see if the user would like to keep adding records
                        if (MessageBox.Show("Would you like to add another record?", "Add another?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            windowAddRecord();
                        }
                        else
                        {
                            // done editing, clean up
                            unloadRecords();
                            loadRecords();
                            editing = false;
                            log("Ended action add records");
                            grdRecords.FirstDisplayedScrollingRowIndex = curPos;
                            grdRecords.Rows[curPos].Selected = true;
                            grdRecords.Focus();
                            curPos = 0;
                            SetState("View");
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Could not add a new record for the following reason(s):" + Environment.NewLine + message, "Unable to add a new record!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log("Failed to add new record.");
                SetState("View");
            }
        }
        private void txtDate_Enter(object sender, EventArgs e)
        {
            // temporarily disable these buttons
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            monthCalendar1.Location = new Point(txtDate.Location.X + (txtDate.Width - monthCalendar1.Width), txtDate.Location.Y);
            monthCalendar1.Visible = true;
            monthCalendar1.BringToFront();

            calendarFillsWhich = "txtDate";
        }
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            switch (calendarFillsWhich)
            {
                case "txtDate":
                txtDate.Text = monthCalendar1.SelectionRange.Start.ToString();
                monthCalendar1.Visible = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnSave.Focus();
                break;

                case "txtStartDate":
                txtStartDate.Text = monthCalendar1.SelectionRange.Start.ToShortDateString();
                monthCalendar1.Visible = false;
                SetState("View");
                break;
                    
                default: // txtEndDate
                txtEndDate.Text = monthCalendar1.SelectionRange.Start.ToShortDateString();
                monthCalendar1.Visible = false;
                SetState("View");
                break;
            }
            calendarFillsWhich = "";
        }
        private void picRecords_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // activate button to allow user to unlink a newly loaded picture if in edit mode
            if (editing)
            {
                btnUnlinkImage.Enabled = (string)picRecords.Tag == "" ? false : true;
            }
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            log("Attempting image drag and drop.");
            e.Effect = DragDropEffects.Copy;
            try
            {
                picRecords.Image = null;
                string[] filename = (string[])e.Data.GetData(DataFormats.FileDrop);
                picRecords.LoadAsync(filename[0]);
                picRecords.Tag = filename[0];
                this.Focus();
                if (editing)
                {
                    log("Image drag and drop succeeded.");
                }
                else
                {
                    windowAddRecord(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to drag and drop new image.", "Operation unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log("Image drag and drop failed." + Environment.NewLine + "Message: " + ex.Message);
            }
        }
        private void grdRecords_SelectionChanged(object sender, EventArgs e)
        {
            doChangeSelection();
        }
        private void doChangeSelection()
        {        // make sure something is selected or program will crash when trying to re-sort
            if (grdRecords.SelectedRows.Count > 0)
            {
                // open picture
                if ((string)picRecords.Tag != "")
                {
                    picRecords.LoadAsync((string)picRecords.Tag);
                }
                else
                {
                    picRecords.Image = null;
                }

                // update producers, categories, tags to reflect current selection
                string value = "";
                int c = grdRecords.CurrentCell.RowIndex;

                // producers`
                grdProducers.ClearSelection();
                value = grdRecords.Rows[c].Cells[2].Value.ToString();
                if (value != null && value != "")
                {
                    try
                    {
                        DataGridViewRow row = grdProducers.Rows.Cast<DataGridViewRow>()
                            .Where(r => r.Cells[0].Value.ToString().Equals(value)).First();
                        grdProducers.Rows[row.Index].Selected = true;
                        grdProducers.FirstDisplayedScrollingRowIndex = row.Index;
                    }
                    catch
                    {
                        // log("Cannot generate link to Producers grid view" + Environment.NewLine + "Message: " + ex.Message);
                        // link broken
                    }
                }

                // categories
                grdCategories.ClearSelection();
                value = grdRecords.Rows[c].Cells[1].Value.ToString();
                if (value != null && value != "")
                {
                    try
                    {
                        DataGridViewRow row = grdCategories.Rows.Cast<DataGridViewRow>()
                            .Where(r => r.Cells[0].Value.ToString().Equals(value)).First();
                        grdCategories.Rows[row.Index].Selected = true;
                        grdCategories.FirstDisplayedScrollingRowIndex = row.Index;
                    }
                    catch
                    {
                        // log("Cannot generate link to Categories grid view" + Environment.NewLine + "Message: " + ex.Message);
                        // link broken
                    }
                }

                // tags
                grdTags.ClearSelection();
                value = grdRecords.Rows[c].Cells[3].Value.ToString();
                if (value != null && value != "")
                {
                    bool firstOne = true;
                    foreach (string tag in breakHash(value))
                    {
                        try
                        {
                            DataGridViewRow row = grdTags.Rows.Cast<DataGridViewRow>()
                                .Where(r => r.Cells[0].Value.ToString().Equals(tag)).First();
                            grdTags.Rows[row.Index].Selected = true;
                            if (firstOne) { grdTags.FirstDisplayedScrollingRowIndex = row.Index; }
                            firstOne = false;
                        }
                        catch
                        {
                            // log("Cannot generate link to Tags grid view" + Environment.NewLine + "Message: " + ex.Message);
                            // link broken
                        }
                    }
                }
            }
        }
        private void btnViewAll_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtStartDate.Clear();
            txtEndDate.Clear();
            unloadRecords();
            loadRecords();
        }
        private void txtStartDate_Enter(object sender, EventArgs e)
        {
            SetState("Edit");

            monthCalendar1.Location = new Point(txtStartDate.Location.X, txtStartDate.Location.Y);
            monthCalendar1.Visible = true;
            monthCalendar1.BringToFront();
            calendarFillsWhich = "txtStartDate";
        }
        private void txtEndDate_Enter(object sender, EventArgs e)
        {
            SetState("Edit");

            monthCalendar1.Location = new Point(txtEndDate.Location.X, txtEndDate.Location.Y);
            monthCalendar1.Visible = true;
            monthCalendar1.BringToFront();
            calendarFillsWhich = "txtEndDate";
        }

        private void cboView_Enter(object sender, EventArgs e)
        {
            // clear old list
            cboView.Items.Clear();

            // populate list
            int c = grdRecords.CurrentCell.RowIndex;
            string tempId;

            // Producers
            tempId = grdRecords.Rows[c].Cells[2].Value.ToString();
            cboView.Items.Add("-------Producer-------");
            if (tempId != null && tempId != "")
            {
                try
                {
                    cboView.Items.Add(pullId("Producers", tempId));
                }
                catch
                {
                    cboView.Items.Add("Link broken");
                }
            }
            else
            {
                cboView.Items.Add("(none)"); 
            }
            
            // Categories
            tempId = grdRecords.Rows[c].Cells[1].Value.ToString();
            cboView.Items.Add("-------Category-------");
            if (tempId != null && tempId != "")
            {
                try
                {
                    cboView.Items.Add(pullId("Categories", tempId));
                }
                catch
                {
                    cboView.Items.Add("Link broken");
                }
            }
            else
            {
                cboView.Items.Add("(none)"); 
            }
            
            // tags
            tempId = grdRecords.Rows[c].Cells[3].Value.ToString();
            cboView.Items.Add("----------Tags----------");
            if (tempId != null && tempId != "")
            {
                foreach (string tag in breakHash(tempId))
                {
                    try
                    {
                        cboView.Items.Add(pullId("Tags", tag));
                    }
                    catch
                    {
                        cboView.Items.Add("Link broken");
                    }
                }
            }
            else
            {
                cboView.Items.Add("(none)"); 
            }
        }
        private string[] breakHash(string taghash)
        {
            string[] broken = new string[taghash.Length / 2];
            int j = 0;
            for (int i = 0; i < taghash.Length; i = i + 2)
            {
                broken[j] = taghash.Substring(i, 2);
                j++; 
            }
            return broken;
        }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (returnKey(e))
            {
                btnSearch.Focus(); 
            }
        }
        private void btnLimit_Click(object sender, EventArgs e)
        {
            log("Initialized apply limitations.");

            // build queries with id #s for each database
            bool needOr = false; // prevents the word 'AND' from coming in after word 'WHERE' query, and also lets us know if we should execute
            string debugQuery = "SELECT * FROM Records WHERE ";
            for (int i = 0; i < grdProducers.SelectedRows.Count; i++)
            {
                if (needOr) { debugQuery += "OR "; }
                debugQuery += "ProducerID = '" +
                    grdProducers.Rows[grdProducers.SelectedRows[i].Index].Cells[0].Value.ToString() + "' ";
                needOr = true;
            }
            for (int i = 0; i < grdCategories.SelectedRows.Count; i++)
            {
                if (needOr) { debugQuery += "OR "; }
                debugQuery += "CategoryID = '" +
                    grdCategories.Rows[grdCategories.SelectedRows[i].Index].Cells[0].Value.ToString() + "' ";
                needOr = true;
            }
            for (int i = 0; i < grdTags.SelectedRows.Count; i++)
            {
                if (needOr) { debugQuery += "OR "; }
                debugQuery += "TagHash LIKE '%" +
                    grdTags.Rows[grdTags.SelectedRows[i].Index].Cells[0].Value.ToString() + "%' ";
                needOr = true;
            }
            try
            {
                if (needOr)
                {
                    unloadRecords();
                    loadRecords(debugQuery);
                    grdProducers.ClearSelection();
                    grdCategories.ClearSelection();
                    grdTags.ClearSelection();
                }
                else
                {
                    MessageBox.Show("Please select at least one 'Producer', 'Category', or 'Tag' to perform limitation!", "Unable to complete operation.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    log("Could not limit records because no limitation was selected."); 
                }
            }
            catch (Exception Ex)
            {
                log("Search query failed." + Environment.NewLine + "Using query: " + debugQuery + Environment.NewLine + "Message: " + Ex.Message);
            }
        }
    }
}
