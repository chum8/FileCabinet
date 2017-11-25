using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileCabinet
{
    public partial class Form2 : Form
    {
        // the purpose of this form is only to retrieve and validate user input to return to the parent.
        // database tasks are handled by parent.

        public Form2()
        {
            InitializeComponent();

            // initialize text fields when adding a new record
            txtTitle.Text = EditorWindow.Title;
            txtDescription.Text = EditorWindow.Description;
            EditorWindow.CloseState = ""; // default state 
            string f;
            switch (EditorWindow.Table)
            {
                case "Categories":
                    f = "Categor";
                    break;
                case "Producers":
                    f = "Producer";
                    break;
                default:
                case "Tags":
                    f = "Tag";
                    break;
            }
            switch (EditorWindow.Task)
            {
                case "Single":
                    this.Text = "Editing Single " + f;
                    if (EditorWindow.Table == "Categories")
                    {
                        this.Text += "y";
                    }
                    break;
                case "Multiple":
                    if (EditorWindow.Table == "Categories")
                    {
                        this.Text = "Editing Multiple " + f + "ies ";
                    }
                    else
                    {
                        this.Text = "Editing Multiple " + f + "s ";
                    }
                    this.Text += "(" + ((EditorWindow.TotalRecs - EditorWindow.RemainingRecs) + 1).ToString() + "\\" + EditorWindow.TotalRecs.ToString() + ")"; 
                    break;
                default:
                case "Add":
                    this.Text = "Adding New Record";
                    break;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EditorWindow.CloseState = "Cancel";
            this.Close();
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (returnKey(e)) 
            {
                btnSave.Focus();
            }
        }

        private void txtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (returnKey(e))
            {
                txtDescription.Focus();
            }
        }

        private bool returnKey(KeyPressEventArgs e)
        {
            return (e.KeyChar == 13);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            EditorWindow.CloseState = "Save";
            this.Close();
        }

        private bool doWindowCancel()
        {
            if (MessageBox.Show("You will lose unsaved edits!  Really cancel?", "Cancel edit",  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                EditorWindow.ReturnState = "Cancel";
                EditorWindow.RemainingRecs = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // as the form closes, peek at the EditorWindow.CloseState variable to see if the user is cancelling or saving

            switch (EditorWindow.CloseState)
            {
                case "Save":
                    EditorWindow.Title = txtTitle.Text;
                    EditorWindow.Description = txtDescription.Text;
                    EditorWindow.ReturnState = "Save";
                    switch (EditorWindow.Task)
                    {
                        case "Add":
                            EditorWindow.RemainingRecs = (MessageBox.Show("Would you like to add another record?", "Add another?",  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes) ? 1 : 0;
                            break;
                        default:
                        case "Multiple":
                        case "Single":
                            EditorWindow.RemainingRecs--; // parent form will call this form again if remainingRecs > 0
                            break;
                    }
                    break;
                default:
                case "Cancel":
                    if (doWindowCancel())
                    {
                        // do nothing -- i.e. let closing event run its course
                    }
                    else
                    {
                        e.Cancel = true; // cancel event and return to form
                    }
                    break;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            label1.Text = "Title (" + (txtTitle.MaxLength - txtTitle.TextLength) + " characters remaining)"; 
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            label2.Text = "Description (" + (txtDescription.MaxLength - txtDescription.TextLength) + " characters remaining)"; 
        }
    }
}
