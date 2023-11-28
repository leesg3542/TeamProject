
namespace TeamProject
{
    partial class RecycleBinForm
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
            this.listBoxRecycleBin = new System.Windows.Forms.ListBox();
            this.txtid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPermanentlyDelete = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxRecycleBin
            // 
            this.listBoxRecycleBin.FormattingEnabled = true;
            this.listBoxRecycleBin.ItemHeight = 12;
            this.listBoxRecycleBin.Location = new System.Drawing.Point(28, 23);
            this.listBoxRecycleBin.Name = "listBoxRecycleBin";
            this.listBoxRecycleBin.Size = new System.Drawing.Size(400, 244);
            this.listBoxRecycleBin.TabIndex = 0;
            // 
            // txtid
            // 
            this.txtid.Location = new System.Drawing.Point(174, 301);
            this.txtid.Name = "txtid";
            this.txtid.Size = new System.Drawing.Size(112, 21);
            this.txtid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 304);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID : ";
            // 
            // btnPermanentlyDelete
            // 
            this.btnPermanentlyDelete.Location = new System.Drawing.Point(70, 354);
            this.btnPermanentlyDelete.Name = "btnPermanentlyDelete";
            this.btnPermanentlyDelete.Size = new System.Drawing.Size(125, 54);
            this.btnPermanentlyDelete.TabIndex = 3;
            this.btnPermanentlyDelete.Text = "완전 삭제";
            this.btnPermanentlyDelete.UseVisualStyleBackColor = true;
            this.btnPermanentlyDelete.Click += new System.EventHandler(this.btnPermanentlyDelete_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(260, 354);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(125, 54);
            this.btnRestore.TabIndex = 4;
            this.btnRestore.Text = "복구";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // RecycleBinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 446);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnPermanentlyDelete);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtid);
            this.Controls.Add(this.listBoxRecycleBin);
            this.Name = "RecycleBinForm";
            this.Text = "RecycleBinForm";
            this.Load += new System.EventHandler(this.RecycleBinForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxRecycleBin;
        private System.Windows.Forms.TextBox txtid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPermanentlyDelete;
        private System.Windows.Forms.Button btnRestore;
    }
}