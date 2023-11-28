using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;


namespace TeamProject
{
    public partial class SignUpForm : Form
    {
        private DBClass dbClass;

        public SignUpForm()
        {
            InitializeComponent();
            dbClass = new DBClass();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dbClass.ConnectToDatabase())
            {
                if (dbClass.CheckDuplicateUserId(txtid.Text))
                {
                    MessageBox.Show("이미 존재하는 사용자 ID입니다. 다른 ID를 선택해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (dbClass.RegisterUser(txtid.Text, txtpwd.Text, txtname.Text, txtphone.Text, txtaddress.Text, txtemail.Text))
                    {
                        MessageBox.Show("회원가입 완료");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("회원가입 실패");
                    }
                }

                dbClass.DisconnectFromDatabase();
            }
        }
    }
}