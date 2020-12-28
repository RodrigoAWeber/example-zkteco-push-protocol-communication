namespace ExampleZKPush
{
    partial class frmMAIN
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSTART = new System.Windows.Forms.Button();
            this.btnSTOP = new System.Windows.Forms.Button();
            this.lblCOMMUNICATION_OPTIONS = new System.Windows.Forms.Label();
            this.btnSEND_DATE_AND_TIME = new System.Windows.Forms.Button();
            this.btnSEND_USERS = new System.Windows.Forms.Button();
            this.btnRECEIVE_TRANSACTIONS = new System.Windows.Forms.Button();
            this.lblSERVER_OPTIONS = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEVENTS = new System.Windows.Forms.TextBox();
            this.btnRECEIVE_USERS = new System.Windows.Forms.Button();
            this.lstDEVICES = new System.Windows.Forms.ListBox();
            this.btnRECEIVE_TEMPLATES = new System.Windows.Forms.Button();
            this.btnRECEIVE_BIOPHOTOS = new System.Windows.Forms.Button();
            this.btnGET_OPTIONS = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSTART
            // 
            this.btnSTART.Location = new System.Drawing.Point(17, 52);
            this.btnSTART.Name = "btnSTART";
            this.btnSTART.Size = new System.Drawing.Size(159, 23);
            this.btnSTART.TabIndex = 0;
            this.btnSTART.Text = "Start";
            this.btnSTART.UseVisualStyleBackColor = true;
            this.btnSTART.Click += new System.EventHandler(this.btnSTART_Click);
            // 
            // btnSTOP
            // 
            this.btnSTOP.Location = new System.Drawing.Point(17, 81);
            this.btnSTOP.Name = "btnSTOP";
            this.btnSTOP.Size = new System.Drawing.Size(159, 23);
            this.btnSTOP.TabIndex = 1;
            this.btnSTOP.Text = "Stop";
            this.btnSTOP.UseVisualStyleBackColor = true;
            this.btnSTOP.Click += new System.EventHandler(this.btnSTOP_Click);
            // 
            // lblCOMMUNICATION_OPTIONS
            // 
            this.lblCOMMUNICATION_OPTIONS.AutoSize = true;
            this.lblCOMMUNICATION_OPTIONS.Location = new System.Drawing.Point(17, 123);
            this.lblCOMMUNICATION_OPTIONS.Name = "lblCOMMUNICATION_OPTIONS";
            this.lblCOMMUNICATION_OPTIONS.Size = new System.Drawing.Size(139, 15);
            this.lblCOMMUNICATION_OPTIONS.TabIndex = 3;
            this.lblCOMMUNICATION_OPTIONS.Text = "Communication Options";
            // 
            // btnSEND_DATE_AND_TIME
            // 
            this.btnSEND_DATE_AND_TIME.Location = new System.Drawing.Point(17, 143);
            this.btnSEND_DATE_AND_TIME.Name = "btnSEND_DATE_AND_TIME";
            this.btnSEND_DATE_AND_TIME.Size = new System.Drawing.Size(159, 23);
            this.btnSEND_DATE_AND_TIME.TabIndex = 4;
            this.btnSEND_DATE_AND_TIME.Text = "Send Date and Time";
            this.btnSEND_DATE_AND_TIME.UseVisualStyleBackColor = true;
            this.btnSEND_DATE_AND_TIME.Click += new System.EventHandler(this.btnSEND_DATE_AND_TIME_Click);
            // 
            // btnSEND_USERS
            // 
            this.btnSEND_USERS.Location = new System.Drawing.Point(17, 172);
            this.btnSEND_USERS.Name = "btnSEND_USERS";
            this.btnSEND_USERS.Size = new System.Drawing.Size(159, 23);
            this.btnSEND_USERS.TabIndex = 4;
            this.btnSEND_USERS.Text = "Send Users";
            this.btnSEND_USERS.UseVisualStyleBackColor = true;
            this.btnSEND_USERS.Click += new System.EventHandler(this.btnSEND_USERS_Click);
            // 
            // btnRECEIVE_TRANSACTIONS
            // 
            this.btnRECEIVE_TRANSACTIONS.Location = new System.Drawing.Point(17, 230);
            this.btnRECEIVE_TRANSACTIONS.Name = "btnRECEIVE_TRANSACTIONS";
            this.btnRECEIVE_TRANSACTIONS.Size = new System.Drawing.Size(159, 23);
            this.btnRECEIVE_TRANSACTIONS.TabIndex = 4;
            this.btnRECEIVE_TRANSACTIONS.Text = "Receive Transactions";
            this.btnRECEIVE_TRANSACTIONS.UseVisualStyleBackColor = true;
            this.btnRECEIVE_TRANSACTIONS.Click += new System.EventHandler(this.btnRECEIVE_TRANSACTIONS_Click);
            // 
            // lblSERVER_OPTIONS
            // 
            this.lblSERVER_OPTIONS.AutoSize = true;
            this.lblSERVER_OPTIONS.Location = new System.Drawing.Point(17, 32);
            this.lblSERVER_OPTIONS.Name = "lblSERVER_OPTIONS";
            this.lblSERVER_OPTIONS.Size = new System.Drawing.Size(84, 15);
            this.lblSERVER_OPTIONS.TabIndex = 3;
            this.lblSERVER_OPTIONS.Text = "Server Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 363);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Devices Connected";
            // 
            // txtEVENTS
            // 
            this.txtEVENTS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEVENTS.Location = new System.Drawing.Point(192, 32);
            this.txtEVENTS.Multiline = true;
            this.txtEVENTS.Name = "txtEVENTS";
            this.txtEVENTS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEVENTS.Size = new System.Drawing.Size(747, 563);
            this.txtEVENTS.TabIndex = 6;
            // 
            // btnRECEIVE_USERS
            // 
            this.btnRECEIVE_USERS.Location = new System.Drawing.Point(17, 201);
            this.btnRECEIVE_USERS.Name = "btnRECEIVE_USERS";
            this.btnRECEIVE_USERS.Size = new System.Drawing.Size(159, 23);
            this.btnRECEIVE_USERS.TabIndex = 4;
            this.btnRECEIVE_USERS.Text = "Receive Users";
            this.btnRECEIVE_USERS.UseVisualStyleBackColor = true;
            this.btnRECEIVE_USERS.Click += new System.EventHandler(this.btnRECEIVE_USERS_Click);
            // 
            // lstDEVICES
            // 
            this.lstDEVICES.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstDEVICES.FormattingEnabled = true;
            this.lstDEVICES.ItemHeight = 15;
            this.lstDEVICES.Location = new System.Drawing.Point(17, 381);
            this.lstDEVICES.Name = "lstDEVICES";
            this.lstDEVICES.Size = new System.Drawing.Size(159, 214);
            this.lstDEVICES.TabIndex = 5;
            // 
            // btnRECEIVE_TEMPLATES
            // 
            this.btnRECEIVE_TEMPLATES.Location = new System.Drawing.Point(17, 259);
            this.btnRECEIVE_TEMPLATES.Name = "btnRECEIVE_TEMPLATES";
            this.btnRECEIVE_TEMPLATES.Size = new System.Drawing.Size(159, 23);
            this.btnRECEIVE_TEMPLATES.TabIndex = 4;
            this.btnRECEIVE_TEMPLATES.Text = "Receive Templates";
            this.btnRECEIVE_TEMPLATES.UseVisualStyleBackColor = true;
            this.btnRECEIVE_TEMPLATES.Click += new System.EventHandler(this.btnRECEIVE_TEMPLATES_Click);
            // 
            // btnRECEIVE_BIOPHOTOS
            // 
            this.btnRECEIVE_BIOPHOTOS.Location = new System.Drawing.Point(17, 288);
            this.btnRECEIVE_BIOPHOTOS.Name = "btnRECEIVE_BIOPHOTOS";
            this.btnRECEIVE_BIOPHOTOS.Size = new System.Drawing.Size(159, 23);
            this.btnRECEIVE_BIOPHOTOS.TabIndex = 4;
            this.btnRECEIVE_BIOPHOTOS.Text = "Receive Biophotos";
            this.btnRECEIVE_BIOPHOTOS.UseVisualStyleBackColor = true;
            this.btnRECEIVE_BIOPHOTOS.Click += new System.EventHandler(this.btnRECEIVE_BIOPHOTOS_Click);
            // 
            // btnGET_OPTIONS
            // 
            this.btnGET_OPTIONS.Location = new System.Drawing.Point(17, 317);
            this.btnGET_OPTIONS.Name = "btnGET_OPTIONS";
            this.btnGET_OPTIONS.Size = new System.Drawing.Size(159, 23);
            this.btnGET_OPTIONS.TabIndex = 4;
            this.btnGET_OPTIONS.Text = "Get Options";
            this.btnGET_OPTIONS.UseVisualStyleBackColor = true;
            this.btnGET_OPTIONS.Click += new System.EventHandler(this.btnGET_OPTIONS_Click);
            // 
            // frmMAIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 607);
            this.Controls.Add(this.btnGET_OPTIONS);
            this.Controls.Add(this.btnRECEIVE_BIOPHOTOS);
            this.Controls.Add(this.btnRECEIVE_TEMPLATES);
            this.Controls.Add(this.btnRECEIVE_USERS);
            this.Controls.Add(this.txtEVENTS);
            this.Controls.Add(this.lstDEVICES);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSERVER_OPTIONS);
            this.Controls.Add(this.btnRECEIVE_TRANSACTIONS);
            this.Controls.Add(this.btnSEND_USERS);
            this.Controls.Add(this.btnSEND_DATE_AND_TIME);
            this.Controls.Add(this.lblCOMMUNICATION_OPTIONS);
            this.Controls.Add(this.btnSTOP);
            this.Controls.Add(this.btnSTART);
            this.Name = "frmMAIN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZK Push Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSTART;
        private System.Windows.Forms.Button btnSTOP;
        private System.Windows.Forms.Label lblCOMMUNICATION_OPTIONS;
        private System.Windows.Forms.Button btnSEND_DATE_AND_TIME;
        private System.Windows.Forms.Button btnSEND_USERS;
        private System.Windows.Forms.Button btnRECEIVE_TRANSACTIONS;
        private System.Windows.Forms.Label lblSERVER_OPTIONS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEVENTS;
        private System.Windows.Forms.Button btnRECEIVE_USERS;
        private System.Windows.Forms.ListBox lstDEVICES;
        private System.Windows.Forms.Button btnRECEIVE_TEMPLATES;
        private System.Windows.Forms.Button btnRECEIVE_BIOPHOTOS;
        private System.Windows.Forms.Button btnGET_OPTIONS;
    }
}

