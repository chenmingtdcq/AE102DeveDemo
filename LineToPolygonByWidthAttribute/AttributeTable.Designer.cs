namespace LineToPolygonByWidthAttribute
{
    partial class AttributeTable
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
            this.dgAttribute = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgAttribute)).BeginInit();
            this.SuspendLayout();
            // 
            // dgAttribute
            // 
            this.dgAttribute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAttribute.Location = new System.Drawing.Point(10, 8);
            this.dgAttribute.Name = "dgAttribute";
            this.dgAttribute.RowTemplate.Height = 23;
            this.dgAttribute.Size = new System.Drawing.Size(703, 365);
            this.dgAttribute.TabIndex = 0;
            // 
            // AttributeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 376);
            this.Controls.Add(this.dgAttribute);
            this.Name = "AttributeTable";
            this.Text = "AttributeTable";
            ((System.ComponentModel.ISupportInitialize)(this.dgAttribute)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgAttribute;
    }
}