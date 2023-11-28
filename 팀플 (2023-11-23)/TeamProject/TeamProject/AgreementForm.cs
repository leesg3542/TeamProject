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
    public partial class AgreementForm : Form
    {
        public AgreementForm()
        {
            InitializeComponent();
        }

        private void chkAllAgree_CheckedChanged(object sender, EventArgs e)
        {
            // 전체 동의 체크박스가 변경되었을 때
            if (chkAllAgree.Checked)
            {
                chkRequired1.Checked = true;
                chkRequired2.Checked = true;
                chkOptional1.Checked = true;
                chkOptional2.Checked = true;
            }
            else
            {
                chkRequired1.Checked = false;
                chkRequired2.Checked = false;
                chkOptional1.Checked = true;
                chkOptional2.Checked = true;
            }

            UpdateNextButtonStatus();
        }

        private void chkRequired_CheckedChanged(object sender, EventArgs e)
        {
            // 필수 동의 체크박스가 변경되었을 때
            UpdateNextButtonStatus();
        }

        private void UpdateNextButtonStatus()
        {
            // 필수 동의 체크박스 상태에 따라 다음 버튼 활성화 여부를 업데이트
            btnNext.Enabled = chkRequired1.Checked && chkRequired2.Checked;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // 다음 버튼 클릭 시 처리 로직
            if (btnNext.Enabled)
            {
                SignUpForm signUpForm = new SignUpForm();
                signUpForm.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("필수 동의 항목을 모두 체크해주세요.", "경고", MessageBoxButtons.OK);
            }
        }
    }
}
