
using System;

namespace TeamProject
{
    partial class AgreementForm
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
            this.chkAllAgree = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRequired1 = new System.Windows.Forms.CheckBox();
            this.chkRequired2 = new System.Windows.Forms.CheckBox();
            this.chkOptional1 = new System.Windows.Forms.CheckBox();
            this.chkOptional2 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkAllAgree
            // 
            this.chkAllAgree.AutoSize = true;
            this.chkAllAgree.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkAllAgree.Location = new System.Drawing.Point(28, 32);
            this.chkAllAgree.Name = "chkAllAgree";
            this.chkAllAgree.Size = new System.Drawing.Size(107, 16);
            this.chkAllAgree.TabIndex = 0;
            this.chkAllAgree.Text = "전체 동의하기";
            this.chkAllAgree.UseVisualStyleBackColor = true;
            this.chkAllAgree.CheckedChanged += new System.EventHandler(this.chkAllAgree_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(51, 278);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(370, 84);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(29, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(455, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "위치기반서비스 이용약관(선택),  이벤트 ・혜택 정보 수신(선택) 동의를 포함합니다.";
            // 
            // chkRequired1
            // 
            this.chkRequired1.AutoSize = true;
            this.chkRequired1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkRequired1.Location = new System.Drawing.Point(28, 118);
            this.chkRequired1.Name = "chkRequired1";
            this.chkRequired1.Size = new System.Drawing.Size(76, 16);
            this.chkRequired1.TabIndex = 3;
            this.chkRequired1.Text = "이용약관";
            this.chkRequired1.UseVisualStyleBackColor = true;
            this.chkRequired1.CheckedChanged += new System.EventHandler(this.chkRequired_CheckedChanged);
            // 
            // chkRequired2
            // 
            this.chkRequired2.AutoSize = true;
            this.chkRequired2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkRequired2.Location = new System.Drawing.Point(28, 256);
            this.chkRequired2.Name = "chkRequired2";
            this.chkRequired2.Size = new System.Drawing.Size(156, 16);
            this.chkRequired2.TabIndex = 4;
            this.chkRequired2.Text = "개인정보 수집 및 이용";
            this.chkRequired2.UseVisualStyleBackColor = true;
            this.chkRequired2.CheckedChanged += new System.EventHandler(this.chkRequired_CheckedChanged);
            // 
            // chkOptional1
            // 
            this.chkOptional1.AutoSize = true;
            this.chkOptional1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkOptional1.Location = new System.Drawing.Point(28, 397);
            this.chkOptional1.Name = "chkOptional1";
            this.chkOptional1.Size = new System.Drawing.Size(172, 16);
            this.chkOptional1.TabIndex = 5;
            this.chkOptional1.Text = "위치기반서비스 이용약관";
            this.chkOptional1.UseVisualStyleBackColor = true;
            // 
            // chkOptional2
            // 
            this.chkOptional2.AutoSize = true;
            this.chkOptional2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkOptional2.Location = new System.Drawing.Point(28, 523);
            this.chkOptional2.Name = "chkOptional2";
            this.chkOptional2.Size = new System.Drawing.Size(156, 16);
            this.chkOptional2.TabIndex = 6;
            this.chkOptional2.Text = "개인정보 수집 및 이용";
            this.chkOptional2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(51, 140);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(370, 84);
            this.textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(51, 419);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(370, 84);
            this.textBox3.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 555);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "이벤트 ・혜택 정보 수신";
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnNext.Location = new System.Drawing.Point(31, 590);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(421, 47);
            this.btnNext.TabIndex = 10;
            this.btnNext.Text = "다음";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // AgreementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 662);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.chkOptional2);
            this.Controls.Add(this.chkOptional1);
            this.Controls.Add(this.chkRequired2);
            this.Controls.Add(this.chkRequired1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chkAllAgree);
            this.Name = "AgreementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConditionsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ChkRequired_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.CheckBox chkAllAgree;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRequired1;
        private System.Windows.Forms.CheckBox chkRequired2;
        private System.Windows.Forms.CheckBox chkOptional1;
        private System.Windows.Forms.CheckBox chkOptional2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNext;
    }
}