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
    public partial class Form1 : Form
    {
        private MainForm main;
        private TestForm test;
        private AdminForm admin;
        private LoginForm loginForm;
        private MypageForm mypage;


        public Form1()
        {
            InitializeComponent();
            IsMdiContainer = true; // 이 부분을 추가
            HideAdminTab();
        }

        public void ShowAdminTab()
        {
            관리자탭ToolStripMenuItem.Visible = true;
        }
        public void HideAdminTab()
        {
            관리자탭ToolStripMenuItem.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            main = new MainForm(this);
            main.MdiParent = this;
            main.Show();
        }

        private void 사용자탭1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (test == null || test.IsDisposed)
            {
                test = new TestForm();
                test.MdiParent = this;
                test.Show();
            }
        }

        private void 관리자탭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (admin == null || admin.IsDisposed)
            {
                admin = new AdminForm();
                admin.MdiParent = this;
                admin.Show();
            }
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 마이페이지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mypage == null || mypage.IsDisposed)
            {
                // loggedInUserId를 생성자를 통해 전달
                mypage = new MypageForm(main.LoggedInUserId);
                mypage.MdiParent = this;
                mypage.Show();
            }
        }
    }
}