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
    public partial class LoginForm : Form
    {
        private DBClass dbClass;
        private Form1 form1;

        public bool LoginSuccess { get; private set; }
        public string LoggedInUserId { get; private set; }
        public string LoggedInAdminId { get; private set; }
        public bool IsAdmin { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            dbClass = new DBClass();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HandleLogin();
        }

        private void HandleLogin() // 로그인 폼 2번 열리는거 수정 코드
        {
            if (dbClass.ConnectToDatabase())
            {
                TryUserAuthentication();

                dbClass.DisconnectFromDatabase();
            }

            CloseLoginForm();
        }

        private void TryUserAuthentication()
        {
            if (dbClass.AuthenticateUser(txtid.Text, txtpswd.Text))
            {
                SetUserLoginSuccess(txtid.Text);
            }
            else if (dbClass.AuthenticateAdmin(txtid.Text, txtpswd.Text))
            {
                SetAdminLoginSuccess(txtid.Text);
            }
            else
            {
                HandleAuthenticationFailure();
            }
        }

        private void SetUserLoginSuccess(string userId)
        {
            LoginSuccess = true;
            LoggedInUserId = userId;
        }

        private void SetAdminLoginSuccess(string adminId)
        {
            LoginSuccess = true;
            LoggedInUserId = adminId;
            LoggedInAdminId = adminId;
            IsAdmin = true;
            Console.WriteLine("관리자로 로그인하였습니다."); // 로그 추가
        }

        private void HandleAuthenticationFailure()
        {
            LoginSuccess = false;
            MessageBox.Show("아이디/패스워드가 틀렸습니다.");
        }

        private void CloseLoginForm()
        {
            this.Close(); // 로그인 폼을 닫음
        }
    }
}