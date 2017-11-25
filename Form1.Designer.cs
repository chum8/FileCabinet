namespace FileCabinet
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtProgramOutput = new System.Windows.Forms.TextBox();
            this.grdRecords = new System.Windows.Forms.DataGridView();
            this.picRecords = new System.Windows.Forms.PictureBox();
            this.btnProducersAll = new System.Windows.Forms.Button();
            this.btnProducersClear = new System.Windows.Forms.Button();
            this.btnTotal = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnProducersDelete = new System.Windows.Forms.Button();
            this.btnProducersEdit = new System.Windows.Forms.Button();
            this.btnProducersAdd = new System.Windows.Forms.Button();
            this.btnCategoriesAdd = new System.Windows.Forms.Button();
            this.btnCategoriesEdit = new System.Windows.Forms.Button();
            this.btnCategoriesDelete = new System.Windows.Forms.Button();
            this.btnCategoriesClear = new System.Windows.Forms.Button();
            this.btnCategoriesAll = new System.Windows.Forms.Button();
            this.btnTagsAdd = new System.Windows.Forms.Button();
            this.btnTagsEdit = new System.Windows.Forms.Button();
            this.btnTagsDelete = new System.Windows.Forms.Button();
            this.btnTagsClear = new System.Windows.Forms.Button();
            this.btnTagsAll = new System.Windows.Forms.Button();
            this.btnProducersView = new System.Windows.Forms.Button();
            this.btnCategoriesView = new System.Windows.Forms.Button();
            this.btnTagsView = new System.Windows.Forms.Button();
            this.btnUnlinkImage = new System.Windows.Forms.Button();
            this.btnImagePath = new System.Windows.Forms.Button();
            this.txtRecordSum = new System.Windows.Forms.TextBox();
            this.txtRecordTitle = new System.Windows.Forms.TextBox();
            this.txtRecordText = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDate = new System.Windows.Forms.Button();
            this.txtStartDate = new System.Windows.Forms.TextBox();
            this.txtEndDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.grdProducers = new System.Windows.Forms.DataGridView();
            this.grdCategories = new System.Windows.Forms.DataGridView();
            this.grdTags = new System.Windows.Forms.DataGridView();
            this.grpProgramOptions = new System.Windows.Forms.GroupBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.btnSetDefault = new System.Windows.Forms.Button();
            this.lblCurrentPath = new System.Windows.Forms.Label();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.chkQuickDelete = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnLimit = new System.Windows.Forms.Button();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.cboView = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProducers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTags)).BeginInit();
            this.grpProgramOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Records";
            // 
            // txtProgramOutput
            // 
            this.txtProgramOutput.BackColor = System.Drawing.Color.White;
            this.txtProgramOutput.Location = new System.Drawing.Point(16, 597);
            this.txtProgramOutput.Multiline = true;
            this.txtProgramOutput.Name = "txtProgramOutput";
            this.txtProgramOutput.ReadOnly = true;
            this.txtProgramOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtProgramOutput.Size = new System.Drawing.Size(965, 83);
            this.txtProgramOutput.TabIndex = 70;
            this.txtProgramOutput.Text = "Program Output";
            // 
            // grdRecords
            // 
            this.grdRecords.AllowUserToAddRows = false;
            this.grdRecords.AllowUserToDeleteRows = false;
            this.grdRecords.AllowUserToOrderColumns = true;
            this.grdRecords.BackgroundColor = System.Drawing.Color.White;
            this.grdRecords.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdRecords.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRecords.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdRecords.Location = new System.Drawing.Point(15, 125);
            this.grdRecords.MultiSelect = false;
            this.grdRecords.Name = "grdRecords";
            this.grdRecords.ReadOnly = true;
            this.grdRecords.RowHeadersVisible = false;
            this.grdRecords.RowHeadersWidth = 16;
            this.grdRecords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdRecords.ShowEditingIcon = false;
            this.grdRecords.Size = new System.Drawing.Size(330, 349);
            this.grdRecords.TabIndex = 4;
            this.grdRecords.SelectionChanged += new System.EventHandler(this.grdRecords_SelectionChanged);
            // 
            // picRecords
            // 
            this.picRecords.Location = new System.Drawing.Point(608, 230);
            this.picRecords.Name = "picRecords";
            this.picRecords.Size = new System.Drawing.Size(373, 358);
            this.picRecords.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picRecords.TabIndex = 13;
            this.picRecords.TabStop = false;
            this.picRecords.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.picRecords_LoadCompleted);
            // 
            // btnProducersAll
            // 
            this.btnProducersAll.Location = new System.Drawing.Point(354, 228);
            this.btnProducersAll.Name = "btnProducersAll";
            this.btnProducersAll.Size = new System.Drawing.Size(48, 25);
            this.btnProducersAll.TabIndex = 17;
            this.btnProducersAll.Text = "All";
            this.btnProducersAll.UseVisualStyleBackColor = true;
            this.btnProducersAll.Click += new System.EventHandler(this.btnProducersAll_Click);
            // 
            // btnProducersClear
            // 
            this.btnProducersClear.Location = new System.Drawing.Point(403, 228);
            this.btnProducersClear.Name = "btnProducersClear";
            this.btnProducersClear.Size = new System.Drawing.Size(48, 25);
            this.btnProducersClear.TabIndex = 18;
            this.btnProducersClear.Text = "Clear";
            this.btnProducersClear.UseVisualStyleBackColor = true;
            this.btnProducersClear.Click += new System.EventHandler(this.btnProducersClear_Click);
            // 
            // btnTotal
            // 
            this.btnTotal.Location = new System.Drawing.Point(206, 538);
            this.btnTotal.Name = "btnTotal";
            this.btnTotal.Size = new System.Drawing.Size(139, 25);
            this.btnTotal.TabIndex = 10;
            this.btnTotal.Text = "Total Current View Sums";
            this.btnTotal.UseVisualStyleBackColor = true;
            this.btnTotal.Click += new System.EventHandler(this.btnTotal_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Location = new System.Drawing.Point(79, 543);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTotal.Size = new System.Drawing.Size(121, 17);
            this.lblTotal.TabIndex = 31;
            this.lblTotal.Text = "--";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnProducersDelete
            // 
            this.btnProducersDelete.Location = new System.Drawing.Point(452, 228);
            this.btnProducersDelete.Name = "btnProducersDelete";
            this.btnProducersDelete.Size = new System.Drawing.Size(48, 25);
            this.btnProducersDelete.TabIndex = 19;
            this.btnProducersDelete.Text = "Delete";
            this.btnProducersDelete.UseVisualStyleBackColor = true;
            this.btnProducersDelete.Click += new System.EventHandler(this.btnProducersDelete_Click);
            // 
            // btnProducersEdit
            // 
            this.btnProducersEdit.Location = new System.Drawing.Point(501, 228);
            this.btnProducersEdit.Name = "btnProducersEdit";
            this.btnProducersEdit.Size = new System.Drawing.Size(48, 25);
            this.btnProducersEdit.TabIndex = 20;
            this.btnProducersEdit.Text = "Edit";
            this.btnProducersEdit.UseVisualStyleBackColor = true;
            this.btnProducersEdit.Click += new System.EventHandler(this.btnProducersEdit_Click);
            // 
            // btnProducersAdd
            // 
            this.btnProducersAdd.Location = new System.Drawing.Point(550, 228);
            this.btnProducersAdd.Name = "btnProducersAdd";
            this.btnProducersAdd.Size = new System.Drawing.Size(48, 25);
            this.btnProducersAdd.TabIndex = 21;
            this.btnProducersAdd.Text = "Add";
            this.btnProducersAdd.UseVisualStyleBackColor = true;
            this.btnProducersAdd.Click += new System.EventHandler(this.btnProducersAdd_Click);
            // 
            // btnCategoriesAdd
            // 
            this.btnCategoriesAdd.Location = new System.Drawing.Point(550, 396);
            this.btnCategoriesAdd.Name = "btnCategoriesAdd";
            this.btnCategoriesAdd.Size = new System.Drawing.Size(48, 25);
            this.btnCategoriesAdd.TabIndex = 30;
            this.btnCategoriesAdd.Text = "Add";
            this.btnCategoriesAdd.UseVisualStyleBackColor = true;
            this.btnCategoriesAdd.Click += new System.EventHandler(this.btnCategoriesAdd_Click);
            // 
            // btnCategoriesEdit
            // 
            this.btnCategoriesEdit.Location = new System.Drawing.Point(501, 396);
            this.btnCategoriesEdit.Name = "btnCategoriesEdit";
            this.btnCategoriesEdit.Size = new System.Drawing.Size(48, 25);
            this.btnCategoriesEdit.TabIndex = 29;
            this.btnCategoriesEdit.Text = "Edit";
            this.btnCategoriesEdit.UseVisualStyleBackColor = true;
            this.btnCategoriesEdit.Click += new System.EventHandler(this.btnCategoriesEdit_Click);
            // 
            // btnCategoriesDelete
            // 
            this.btnCategoriesDelete.Location = new System.Drawing.Point(452, 396);
            this.btnCategoriesDelete.Name = "btnCategoriesDelete";
            this.btnCategoriesDelete.Size = new System.Drawing.Size(48, 25);
            this.btnCategoriesDelete.TabIndex = 28;
            this.btnCategoriesDelete.Text = "Delete";
            this.btnCategoriesDelete.UseVisualStyleBackColor = true;
            this.btnCategoriesDelete.Click += new System.EventHandler(this.btnCategoriesDelete_Click);
            // 
            // btnCategoriesClear
            // 
            this.btnCategoriesClear.Location = new System.Drawing.Point(403, 396);
            this.btnCategoriesClear.Name = "btnCategoriesClear";
            this.btnCategoriesClear.Size = new System.Drawing.Size(48, 25);
            this.btnCategoriesClear.TabIndex = 27;
            this.btnCategoriesClear.Text = "Clear";
            this.btnCategoriesClear.UseVisualStyleBackColor = true;
            this.btnCategoriesClear.Click += new System.EventHandler(this.btnCategoriesClear_Click);
            // 
            // btnCategoriesAll
            // 
            this.btnCategoriesAll.Location = new System.Drawing.Point(354, 396);
            this.btnCategoriesAll.Name = "btnCategoriesAll";
            this.btnCategoriesAll.Size = new System.Drawing.Size(48, 25);
            this.btnCategoriesAll.TabIndex = 26;
            this.btnCategoriesAll.Text = "All";
            this.btnCategoriesAll.UseVisualStyleBackColor = true;
            this.btnCategoriesAll.Click += new System.EventHandler(this.btnCategoriesAll_Click);
            // 
            // btnTagsAdd
            // 
            this.btnTagsAdd.Location = new System.Drawing.Point(550, 566);
            this.btnTagsAdd.Name = "btnTagsAdd";
            this.btnTagsAdd.Size = new System.Drawing.Size(48, 25);
            this.btnTagsAdd.TabIndex = 39;
            this.btnTagsAdd.Text = "Add";
            this.btnTagsAdd.UseVisualStyleBackColor = true;
            this.btnTagsAdd.Click += new System.EventHandler(this.btnTagsAdd_Click);
            // 
            // btnTagsEdit
            // 
            this.btnTagsEdit.Location = new System.Drawing.Point(501, 566);
            this.btnTagsEdit.Name = "btnTagsEdit";
            this.btnTagsEdit.Size = new System.Drawing.Size(48, 25);
            this.btnTagsEdit.TabIndex = 38;
            this.btnTagsEdit.Text = "Edit";
            this.btnTagsEdit.UseVisualStyleBackColor = true;
            this.btnTagsEdit.Click += new System.EventHandler(this.btnTagsEdit_Click);
            // 
            // btnTagsDelete
            // 
            this.btnTagsDelete.Location = new System.Drawing.Point(452, 566);
            this.btnTagsDelete.Name = "btnTagsDelete";
            this.btnTagsDelete.Size = new System.Drawing.Size(48, 25);
            this.btnTagsDelete.TabIndex = 37;
            this.btnTagsDelete.Text = "Delete";
            this.btnTagsDelete.UseVisualStyleBackColor = true;
            this.btnTagsDelete.Click += new System.EventHandler(this.btnTagsDelete_Click);
            // 
            // btnTagsClear
            // 
            this.btnTagsClear.Location = new System.Drawing.Point(403, 566);
            this.btnTagsClear.Name = "btnTagsClear";
            this.btnTagsClear.Size = new System.Drawing.Size(48, 25);
            this.btnTagsClear.TabIndex = 36;
            this.btnTagsClear.Text = "Clear";
            this.btnTagsClear.UseVisualStyleBackColor = true;
            this.btnTagsClear.Click += new System.EventHandler(this.btnTagsClear_Click);
            // 
            // btnTagsAll
            // 
            this.btnTagsAll.Location = new System.Drawing.Point(354, 566);
            this.btnTagsAll.Name = "btnTagsAll";
            this.btnTagsAll.Size = new System.Drawing.Size(48, 25);
            this.btnTagsAll.TabIndex = 35;
            this.btnTagsAll.Text = "All";
            this.btnTagsAll.UseVisualStyleBackColor = true;
            this.btnTagsAll.Click += new System.EventHandler(this.btnTagsAll_Click);
            // 
            // btnProducersView
            // 
            this.btnProducersView.Location = new System.Drawing.Point(428, 96);
            this.btnProducersView.Name = "btnProducersView";
            this.btnProducersView.Size = new System.Drawing.Size(170, 25);
            this.btnProducersView.TabIndex = 15;
            this.btnProducersView.Text = "View Description(s) of Selected";
            this.btnProducersView.UseVisualStyleBackColor = true;
            this.btnProducersView.Click += new System.EventHandler(this.btnProducersView_Click);
            // 
            // btnCategoriesView
            // 
            this.btnCategoriesView.Location = new System.Drawing.Point(428, 264);
            this.btnCategoriesView.Name = "btnCategoriesView";
            this.btnCategoriesView.Size = new System.Drawing.Size(170, 25);
            this.btnCategoriesView.TabIndex = 24;
            this.btnCategoriesView.Text = "View Description(s) of Selected";
            this.btnCategoriesView.UseVisualStyleBackColor = true;
            this.btnCategoriesView.Click += new System.EventHandler(this.btnCategoriesView_Click);
            // 
            // btnTagsView
            // 
            this.btnTagsView.Location = new System.Drawing.Point(428, 434);
            this.btnTagsView.Name = "btnTagsView";
            this.btnTagsView.Size = new System.Drawing.Size(170, 25);
            this.btnTagsView.TabIndex = 33;
            this.btnTagsView.Text = "View Description(s) of Selected";
            this.btnTagsView.UseVisualStyleBackColor = true;
            this.btnTagsView.Click += new System.EventHandler(this.btnTagsView_Click);
            // 
            // btnUnlinkImage
            // 
            this.btnUnlinkImage.Enabled = false;
            this.btnUnlinkImage.Location = new System.Drawing.Point(795, 138);
            this.btnUnlinkImage.Name = "btnUnlinkImage";
            this.btnUnlinkImage.Size = new System.Drawing.Size(92, 40);
            this.btnUnlinkImage.TabIndex = 57;
            this.btnUnlinkImage.Text = "&Unlink Image";
            this.btnUnlinkImage.UseVisualStyleBackColor = true;
            this.btnUnlinkImage.Click += new System.EventHandler(this.btnUnlinkImage_Click);
            // 
            // btnImagePath
            // 
            this.btnImagePath.Location = new System.Drawing.Point(889, 138);
            this.btnImagePath.Name = "btnImagePath";
            this.btnImagePath.Size = new System.Drawing.Size(92, 40);
            this.btnImagePath.TabIndex = 58;
            this.btnImagePath.Text = "Change &Image Path...";
            this.btnImagePath.UseVisualStyleBackColor = true;
            this.btnImagePath.Click += new System.EventHandler(this.btnImagePath_Click);
            // 
            // txtRecordSum
            // 
            this.txtRecordSum.BackColor = System.Drawing.Color.White;
            this.txtRecordSum.Location = new System.Drawing.Point(841, 31);
            this.txtRecordSum.MaxLength = 9;
            this.txtRecordSum.Name = "txtRecordSum";
            this.txtRecordSum.Size = new System.Drawing.Size(140, 20);
            this.txtRecordSum.TabIndex = 48;
            this.txtRecordSum.TextChanged += new System.EventHandler(this.txtRecordSum_TextChanged);
            this.txtRecordSum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRecordSum_KeyPress);
            // 
            // txtRecordTitle
            // 
            this.txtRecordTitle.BackColor = System.Drawing.Color.White;
            this.txtRecordTitle.Location = new System.Drawing.Point(608, 31);
            this.txtRecordTitle.MaxLength = 45;
            this.txtRecordTitle.Name = "txtRecordTitle";
            this.txtRecordTitle.Size = new System.Drawing.Size(227, 20);
            this.txtRecordTitle.TabIndex = 45;
            this.txtRecordTitle.TextChanged += new System.EventHandler(this.txtRecordTitle_TextChanged);
            this.txtRecordTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRecordTitle_KeyPress);
            // 
            // txtRecordText
            // 
            this.txtRecordText.BackColor = System.Drawing.Color.White;
            this.txtRecordText.Location = new System.Drawing.Point(608, 70);
            this.txtRecordText.MaxLength = 255;
            this.txtRecordText.Multiline = true;
            this.txtRecordText.Name = "txtRecordText";
            this.txtRecordText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRecordText.Size = new System.Drawing.Size(227, 62);
            this.txtRecordText.TabIndex = 46;
            this.txtRecordText.TextChanged += new System.EventHandler(this.txtRecordText_TextChanged);
            this.txtRecordText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRecordText_KeyPress);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(889, 181);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(92, 40);
            this.btnExit.TabIndex = 62;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(795, 181);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 40);
            this.btnAdd.TabIndex = 61;
            this.btnAdd.Text = "&Add New Record";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(701, 181);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(92, 40);
            this.btnEdit.TabIndex = 60;
            this.btnEdit.Text = "&Edit Current Record";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(607, 181);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(92, 40);
            this.btnDelete.TabIndex = 59;
            this.btnDelete.Text = "&Delete Current Record";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(18, 485);
            this.txtSearch.MaxLength = 45;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(182, 20);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(206, 482);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(139, 25);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search in Titles";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDate
            // 
            this.btnDate.Location = new System.Drawing.Point(206, 510);
            this.btnDate.Name = "btnDate";
            this.btnDate.Size = new System.Drawing.Size(139, 25);
            this.btnDate.TabIndex = 9;
            this.btnDate.Text = "Limit by Date Range";
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
            // 
            // txtStartDate
            // 
            this.txtStartDate.BackColor = System.Drawing.Color.White;
            this.txtStartDate.Location = new System.Drawing.Point(18, 513);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(76, 20);
            this.txtStartDate.TabIndex = 7;
            this.txtStartDate.Enter += new System.EventHandler(this.txtStartDate_Enter);
            // 
            // txtEndDate
            // 
            this.txtEndDate.BackColor = System.Drawing.Color.White;
            this.txtEndDate.Location = new System.Drawing.Point(124, 513);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(76, 20);
            this.txtEndDate.TabIndex = 8;
            this.txtEndDate.Enter += new System.EventHandler(this.txtEndDate_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 69;
            this.label4.Text = "Producers";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(351, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 70;
            this.label5.Text = "Categories";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(351, 445);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 71;
            this.label6.Text = "Tags";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 518);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 72;
            this.label7.Text = "to";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(605, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 13);
            this.label8.TabIndex = 73;
            this.label8.Text = "Title (45 characters remaining)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(838, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 74;
            this.label9.Text = "Sum (9 remaining)";
            // 
            // grdProducers
            // 
            this.grdProducers.AllowUserToAddRows = false;
            this.grdProducers.AllowUserToDeleteRows = false;
            this.grdProducers.AllowUserToOrderColumns = true;
            this.grdProducers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdProducers.Location = new System.Drawing.Point(354, 125);
            this.grdProducers.Name = "grdProducers";
            this.grdProducers.ReadOnly = true;
            this.grdProducers.RowHeadersVisible = false;
            this.grdProducers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdProducers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdProducers.Size = new System.Drawing.Size(244, 99);
            this.grdProducers.TabIndex = 16;
            // 
            // grdCategories
            // 
            this.grdCategories.AllowUserToAddRows = false;
            this.grdCategories.AllowUserToDeleteRows = false;
            this.grdCategories.AllowUserToOrderColumns = true;
            this.grdCategories.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdCategories.Location = new System.Drawing.Point(354, 293);
            this.grdCategories.Name = "grdCategories";
            this.grdCategories.ReadOnly = true;
            this.grdCategories.RowHeadersVisible = false;
            this.grdCategories.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdCategories.Size = new System.Drawing.Size(244, 99);
            this.grdCategories.TabIndex = 25;
            // 
            // grdTags
            // 
            this.grdTags.AllowUserToAddRows = false;
            this.grdTags.AllowUserToDeleteRows = false;
            this.grdTags.AllowUserToOrderColumns = true;
            this.grdTags.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdTags.Location = new System.Drawing.Point(354, 463);
            this.grdTags.Name = "grdTags";
            this.grdTags.ReadOnly = true;
            this.grdTags.RowHeadersVisible = false;
            this.grdTags.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdTags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTags.Size = new System.Drawing.Size(244, 99);
            this.grdTags.TabIndex = 34;
            // 
            // grpProgramOptions
            // 
            this.grpProgramOptions.Controls.Add(this.chkAutoClear);
            this.grpProgramOptions.Controls.Add(this.btnSetDefault);
            this.grpProgramOptions.Controls.Add(this.lblCurrentPath);
            this.grpProgramOptions.Controls.Add(this.btnLoadFile);
            this.grpProgramOptions.Controls.Add(this.chkQuickDelete);
            this.grpProgramOptions.Location = new System.Drawing.Point(16, 12);
            this.grpProgramOptions.Name = "grpProgramOptions";
            this.grpProgramOptions.Size = new System.Drawing.Size(582, 76);
            this.grpProgramOptions.TabIndex = 0;
            this.grpProgramOptions.TabStop = false;
            this.grpProgramOptions.Text = "Program Options";
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Checked = true;
            this.chkAutoClear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoClear.Location = new System.Drawing.Point(396, 25);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(180, 17);
            this.chkAutoClear.TabIndex = 2;
            this.chkAutoClear.Text = "Clear Fields on Add New Record";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // btnSetDefault
            // 
            this.btnSetDefault.Location = new System.Drawing.Point(206, 20);
            this.btnSetDefault.Name = "btnSetDefault";
            this.btnSetDefault.Size = new System.Drawing.Size(180, 25);
            this.btnSetDefault.TabIndex = 1;
            this.btnSetDefault.Text = "Set Current &File as Default";
            this.btnSetDefault.UseVisualStyleBackColor = true;
            this.btnSetDefault.Click += new System.EventHandler(this.btnSetDefault_Click);
            // 
            // lblCurrentPath
            // 
            this.lblCurrentPath.AutoSize = true;
            this.lblCurrentPath.Location = new System.Drawing.Point(18, 51);
            this.lblCurrentPath.Name = "lblCurrentPath";
            this.lblCurrentPath.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentPath.TabIndex = 83;
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(18, 19);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(180, 25);
            this.btnLoadFile.TabIndex = 0;
            this.btnLoadFile.Text = "&Load a Database File...";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // chkQuickDelete
            // 
            this.chkQuickDelete.AutoSize = true;
            this.chkQuickDelete.Location = new System.Drawing.Point(452, 50);
            this.chkQuickDelete.Name = "chkQuickDelete";
            this.chkQuickDelete.Size = new System.Drawing.Size(124, 17);
            this.chkQuickDelete.TabIndex = 3;
            this.chkQuickDelete.Text = "Enable Quick Delete";
            this.chkQuickDelete.UseVisualStyleBackColor = true;
            this.chkQuickDelete.CheckedChanged += new System.EventHandler(this.chkQuickDelete_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(606, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(194, 13);
            this.label10.TabIndex = 84;
            this.label10.Text = "Record Text (255 characters remaining)";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select the folder that contains the database file";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(607, 138);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 40);
            this.btnSave.TabIndex = 55;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(701, 138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 40);
            this.btnCancel.TabIndex = 56;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.White;
            this.txtDate.Location = new System.Drawing.Point(841, 70);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(140, 20);
            this.txtDate.TabIndex = 49;
            this.txtDate.Enter += new System.EventHandler(this.txtDate_Enter);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(838, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 86;
            this.label11.Text = "Date";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.BackColor = System.Drawing.Color.White;
            this.monthCalendar1.Location = new System.Drawing.Point(682, 230);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 75;
            this.monthCalendar1.Visible = false;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Title = "Select an Image to Link to the Record";
            // 
            // btnLimit
            // 
            this.btnLimit.Location = new System.Drawing.Point(64, 566);
            this.btnLimit.Name = "btnLimit";
            this.btnLimit.Size = new System.Drawing.Size(139, 25);
            this.btnLimit.TabIndex = 11;
            this.btnLimit.Text = "Apply Limitations";
            this.btnLimit.UseVisualStyleBackColor = true;
            this.btnLimit.Click += new System.EventHandler(this.btnLimit_Click);
            // 
            // btnViewAll
            // 
            this.btnViewAll.Location = new System.Drawing.Point(206, 566);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(139, 25);
            this.btnViewAll.TabIndex = 12;
            this.btnViewAll.Text = "View All Records";
            this.btnViewAll.UseVisualStyleBackColor = true;
            this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);
            // 
            // cboView
            // 
            this.cboView.FormattingEnabled = true;
            this.cboView.Location = new System.Drawing.Point(842, 110);
            this.cboView.Name = "cboView";
            this.cboView.Size = new System.Drawing.Size(139, 21);
            this.cboView.TabIndex = 50;
            this.cboView.Enter += new System.EventHandler(this.cboView_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(838, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 88;
            this.label2.Text = "View Relations";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 689);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboView);
            this.Controls.Add(this.btnViewAll);
            this.Controls.Add(this.btnLimit);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.grpProgramOptions);
            this.Controls.Add(this.grdTags);
            this.Controls.Add(this.grdCategories);
            this.Controls.Add(this.grdProducers);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEndDate);
            this.Controls.Add(this.txtStartDate);
            this.Controls.Add(this.btnDate);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.txtRecordSum);
            this.Controls.Add(this.txtRecordTitle);
            this.Controls.Add(this.txtRecordText);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnImagePath);
            this.Controls.Add(this.btnUnlinkImage);
            this.Controls.Add(this.btnTagsView);
            this.Controls.Add(this.btnCategoriesView);
            this.Controls.Add(this.btnProducersView);
            this.Controls.Add(this.btnTagsAdd);
            this.Controls.Add(this.btnTagsEdit);
            this.Controls.Add(this.btnTagsDelete);
            this.Controls.Add(this.btnTagsClear);
            this.Controls.Add(this.btnTagsAll);
            this.Controls.Add(this.btnCategoriesAdd);
            this.Controls.Add(this.btnCategoriesEdit);
            this.Controls.Add(this.btnCategoriesDelete);
            this.Controls.Add(this.btnCategoriesClear);
            this.Controls.Add(this.btnCategoriesAll);
            this.Controls.Add(this.btnProducersAdd);
            this.Controls.Add(this.btnProducersEdit);
            this.Controls.Add(this.btnProducersDelete);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnTotal);
            this.Controls.Add(this.btnProducersClear);
            this.Controls.Add(this.btnProducersAll);
            this.Controls.Add(this.picRecords);
            this.Controls.Add(this.grdRecords);
            this.Controls.Add(this.txtProgramOutput);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Cabinet Database";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.grdRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProducers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTags)).EndInit();
            this.grpProgramOptions.ResumeLayout(false);
            this.grpProgramOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProgramOutput;
        private System.Windows.Forms.DataGridView grdRecords;
        private System.Windows.Forms.PictureBox picRecords;
        private System.Windows.Forms.Button btnProducersAll;
        private System.Windows.Forms.Button btnProducersClear;
        private System.Windows.Forms.Button btnTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnProducersDelete;
        private System.Windows.Forms.Button btnProducersEdit;
        private System.Windows.Forms.Button btnProducersAdd;
        private System.Windows.Forms.Button btnCategoriesAdd;
        private System.Windows.Forms.Button btnCategoriesEdit;
        private System.Windows.Forms.Button btnCategoriesDelete;
        private System.Windows.Forms.Button btnCategoriesClear;
        private System.Windows.Forms.Button btnCategoriesAll;
        private System.Windows.Forms.Button btnTagsAdd;
        private System.Windows.Forms.Button btnTagsEdit;
        private System.Windows.Forms.Button btnTagsDelete;
        private System.Windows.Forms.Button btnTagsClear;
        private System.Windows.Forms.Button btnTagsAll;
        private System.Windows.Forms.Button btnProducersView;
        private System.Windows.Forms.Button btnCategoriesView;
        private System.Windows.Forms.Button btnTagsView;
        private System.Windows.Forms.Button btnUnlinkImage;
        private System.Windows.Forms.Button btnImagePath;
        private System.Windows.Forms.TextBox txtRecordSum;
        private System.Windows.Forms.TextBox txtRecordTitle;
        private System.Windows.Forms.TextBox txtRecordText;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDate;
        private System.Windows.Forms.TextBox txtStartDate;
        private System.Windows.Forms.TextBox txtEndDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView grdProducers;
        private System.Windows.Forms.DataGridView grdCategories;
        private System.Windows.Forms.DataGridView grdTags;
        private System.Windows.Forms.GroupBox grpProgramOptions;
        private System.Windows.Forms.CheckBox chkQuickDelete;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCurrentPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnSetDefault;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnLimit;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.ComboBox cboView;
        private System.Windows.Forms.Label label2;
    }
}

