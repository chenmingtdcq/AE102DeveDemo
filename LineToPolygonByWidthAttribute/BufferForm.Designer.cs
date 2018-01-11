namespace LineToPolygonByWidthAttribute
{
    partial class BufferForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.cbUnit = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFields = new System.Windows.Forms.ComboBox();
            this.btnUnion = new System.Windows.Forms.Button();
            this.cbFieldValues = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cbDictanceField = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbInputDLTB = new System.Windows.Forms.TextBox();
            this.btnImputDLTB = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "缓冲距离";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "线转面路径";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(106, 83);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(258, 21);
            this.tbPath.TabIndex = 1;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(371, 82);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(81, 23);
            this.btnSelectPath.TabIndex = 2;
            this.btnSelectPath.Text = "选择";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // cbUnit
            // 
            this.cbUnit.FormattingEnabled = true;
            this.cbUnit.Location = new System.Drawing.Point(370, 33);
            this.cbUnit.Name = "cbUnit";
            this.cbUnit.Size = new System.Drawing.Size(82, 20);
            this.cbUnit.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(266, 255);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(92, 21);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "执行线转面";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(382, 255);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 21);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "面合并";
            // 
            // cbFields
            // 
            this.cbFields.FormattingEnabled = true;
            this.cbFields.Location = new System.Drawing.Point(371, 130);
            this.cbFields.Name = "cbFields";
            this.cbFields.Size = new System.Drawing.Size(81, 20);
            this.cbFields.TabIndex = 6;
            this.cbFields.SelectionChangeCommitted += new System.EventHandler(this.cbFields_SelectionChangeCommitted);
            // 
            // btnUnion
            // 
            this.btnUnion.Location = new System.Drawing.Point(151, 255);
            this.btnUnion.Name = "btnUnion";
            this.btnUnion.Size = new System.Drawing.Size(87, 21);
            this.btnUnion.TabIndex = 7;
            this.btnUnion.Text = "执行面合并";
            this.btnUnion.UseVisualStyleBackColor = true;
            this.btnUnion.Click += new System.EventHandler(this.btnUnion_Click);
            // 
            // cbFieldValues
            // 
            this.cbFieldValues.Location = new System.Drawing.Point(106, 130);
            this.cbFieldValues.Name = "cbFieldValues";
            this.cbFieldValues.Properties.AllowMultiSelect = true;
            this.cbFieldValues.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbFieldValues.Properties.DropDownRows = 15;
            this.cbFieldValues.Size = new System.Drawing.Size(257, 20);
            this.cbFieldValues.TabIndex = 17;
            // 
            // cbDictanceField
            // 
            this.cbDictanceField.FormattingEnabled = true;
            this.cbDictanceField.Location = new System.Drawing.Point(102, 32);
            this.cbDictanceField.Name = "cbDictanceField";
            this.cbDictanceField.Size = new System.Drawing.Size(261, 20);
            this.cbDictanceField.TabIndex = 8;
            this.cbDictanceField.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_SelectionChangeCommitted);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 255);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 20);
            this.button1.TabIndex = 10;
            this.button1.Text = "执行擦除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "输入面要素";
            // 
            // tbInputDLTB
            // 
            this.tbInputDLTB.Location = new System.Drawing.Point(105, 190);
            this.tbInputDLTB.Name = "tbInputDLTB";
            this.tbInputDLTB.Size = new System.Drawing.Size(258, 21);
            this.tbInputDLTB.TabIndex = 12;
            // 
            // btnImputDLTB
            // 
            this.btnImputDLTB.Location = new System.Drawing.Point(371, 188);
            this.btnImputDLTB.Name = "btnImputDLTB";
            this.btnImputDLTB.Size = new System.Drawing.Size(83, 23);
            this.btnImputDLTB.TabIndex = 13;
            this.btnImputDLTB.Text = "选择";
            this.btnImputDLTB.UseVisualStyleBackColor = true;
            this.btnImputDLTB.Click += new System.EventHandler(this.btnImputDLTB_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(239, 260);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "<—";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(129, 260);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "<—";
            // 
            // BufferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 310);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnImputDLTB);
            this.Controls.Add(this.tbInputDLTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbDictanceField);
            this.Controls.Add(this.cbFieldValues);
            this.Controls.Add(this.btnUnion);
            this.Controls.Add(this.cbFields);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbUnit);
            this.Controls.Add(this.btnSelectPath);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "BufferForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线转面";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.ComboBox cbUnit;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFields;
        private System.Windows.Forms.Button btnUnion;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbFieldValues;
        private System.Windows.Forms.ComboBox cbDictanceField;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbInputDLTB;
        private System.Windows.Forms.Button btnImputDLTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}