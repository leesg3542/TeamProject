using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamProject
{
    public partial class MainForm : Form
    {
        private GroupBox userControlsGroupBox;
        private TextBox loggedInTextBox;
        private Button logoutButton;
        private Button loginButton;
        private Form1 form1;
        private MypageForm mypage;

        public string LoggedInUserId { get; private set; } // 추가

        public MainForm(Form1 form1)
        {
            InitializeComponent();
            userControlsGroupBox = LoginGroup;
            this.form1 = form1;

            loginButton = LoginButton;
            loginButton.Click -= LoginButton_Click;
            loginButton.Click += LoginButton_Click;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            if (loginForm.LoginSuccess)
            {
                LoggedInUserId = loginForm.LoggedInUserId;

                UpdateUIOnLogin(loginForm.LoggedInUserId, loginForm.IsAdmin);
            }
            else
            {
                MessageBox.Show("로그인에 실패했습니다.");
            }
        }

        private void UpdateUIOnLogin(string loggedInUserId, bool isAdmin)
        {
            if (loggedInTextBox == null)
            {
                loggedInTextBox = CreateTextBox();
                userControlsGroupBox.Controls.Add(loggedInTextBox);
            }

            loggedInTextBox.Text = "ID: " + loggedInUserId;
            loggedInTextBox.Visible = true;

            if (logoutButton == null)
            {
                logoutButton = CreateLogoutButton();
                userControlsGroupBox.Controls.Add(logoutButton);
            }

            logoutButton.Visible = true;

            LoginButton.Visible = false;
            SignUpButton.Visible = false;

            if (isAdmin)
            {
                form1.ShowAdminTab();
            }

            ShowLoginSuccessMessage();
        }

        private TextBox CreateTextBox()
        {
            TextBox textBox = new TextBox
            {
                ReadOnly = true,
                Location = new Point(17, 30)
            };

            this.Controls.Add(textBox);
            return textBox;
        }

        private Button CreateLogoutButton()
        {
            Button button = new Button
            {
                Text = "로그아웃",
                Location = new Point(112, 30)
            };

            button.Click += LogoutButton_Click;
            this.Controls.Add(button);
            return button;
        }

        private void ShowLoginSuccessMessage()
        {
            MessageBox.Show("로그인 성공");
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            AgreementForm agreementForm = new AgreementForm();
            agreementForm.ShowDialog();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("로그아웃 되었습니다.");

            loggedInTextBox.Visible = false;
            logoutButton.Visible = false;

            LoginButton.Visible = true;
            SignUpButton.Visible = true;

            form1.HideAdminTab();

            // mypage 객체 초기화
            mypage = null;
        }
    }
}
