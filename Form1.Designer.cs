namespace NetworkWinApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private string hex = "#235688";

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgt = new System.Windows.Forms.DataGridView();
            this.Add = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCurrentIp = new System.Windows.Forms.Button();
            this.serverName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accessType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firewallPermission = new System.Windows.Forms.DataGridViewImageColumn();
            this.serverAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgt)).BeginInit();
            this.SuspendLayout();
            // 
            // dgt
            // 
            this.dgt.AllowUserToAddRows = false;
            this.dgt.AllowUserToDeleteRows = false;
            this.dgt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgt.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgt.ColumnHeadersHeight = 30;
            this.dgt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serverName,
            this.accessType,
            this.pcAddress,
            this.pcPort,
            this.firewallPermission,
            this.serverAddress,
            this.serverPort});
            this.dgt.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgt.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgt.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgt.Location = new System.Drawing.Point(43, 98);
            this.dgt.Name = "dgt";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgt.RowTemplate.Height = 30;
            this.dgt.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgt.Size = new System.Drawing.Size(1097, 288);
            this.dgt.TabIndex = 3;
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Add.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Add.Location = new System.Drawing.Point(1065, 60);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 32);
            this.Add.TabIndex = 8;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this._add_Click);
            // 
            // Save
            // 
            this.Save.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Save.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Save.Location = new System.Drawing.Point(1065, 393);
            this.Save.Margin = new System.Windows.Forms.Padding(4);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 32);
            this.Save.TabIndex = 9;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 22);
            this.label2.TabIndex = 11;
            this.label2.Text = "Windows Port Forwarder";
            // 
            // btnCurrentIp
            // 
            this.btnCurrentIp.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnCurrentIp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCurrentIp.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCurrentIp.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCurrentIp.Location = new System.Drawing.Point(351, 60);
            this.btnCurrentIp.Name = "btnCurrentIp";
            this.btnCurrentIp.Size = new System.Drawing.Size(113, 32);
            this.btnCurrentIp.TabIndex = 12;
            this.btnCurrentIp.Text = "Set Current IP";
            this.btnCurrentIp.UseVisualStyleBackColor = false;
            this.btnCurrentIp.Click += new System.EventHandler(this.btnCurrentIp_Click);
            // 
            // serverName
            // 
            this.serverName.HeaderText = "Server Name";
            this.serverName.Name = "serverName";
            // 
            // accessType
            // 
            this.accessType.HeaderText = "Access Type";
            this.accessType.Name = "accessType";
            // 
            // pcAddress
            // 
            this.pcAddress.HeaderText = "PC Address";
            this.pcAddress.Name = "pcAddress";
            // 
            // pcPort
            // 
            this.pcPort.HeaderText = "PC Port";
            this.pcPort.Name = "pcPort";
            // 
            // firewallPermission
            // 
            this.firewallPermission.HeaderText = "Firewall Permission";
            this.firewallPermission.Name = "firewallPermission";
            this.firewallPermission.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.firewallPermission.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // serverAddress
            // 
            this.serverAddress.HeaderText = "Server Address";
            this.serverAddress.Name = "serverAddress";
            // 
            // serverPort
            // 
            this.serverPort.HeaderText = "Server Port";
            this.serverPort.Name = "serverPort";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 450);
            this.Controls.Add(this.btnCurrentIp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.dgt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Windows Port Forwarder";
            ((System.ComponentModel.ISupportInitialize)(this.dgt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgt;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCurrentIp;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverName;
        private System.Windows.Forms.DataGridViewTextBoxColumn accessType;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcPort;
        private System.Windows.Forms.DataGridViewImageColumn firewallPermission;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverPort;

    }
}

