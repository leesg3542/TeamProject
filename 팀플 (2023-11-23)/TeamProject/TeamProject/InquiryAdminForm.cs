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
    public partial class InquiryAdminForm : Form
    {
        private int inqId;
        private DBClass dbClass;
        private AdminForm adminForm;  // AdminForm에 대한 참조 추가
        // 생성자 오버로딩 문의 답글
        public InquiryAdminForm(int inqId, string userId, string orderId, string title, string content, AdminForm adminForm)
        {
            InitializeComponent();
            // DB 연결
            dbClass = new DBClass();
            if (dbClass.ConnectToDatabase())
            {
                this.inqId = inqId;

                // 텍스트 박스에 전달받은 값 할당
                textBox1.Text = userId;
                textBox2.Text = orderId;
                textBox3.Text = title;
                textBox4.Text = content;

                // AdminForm 참조 설정
                this.adminForm = adminForm;
            }
            else
            {
                MessageBox.Show("DB 연결에 실패했습니다.");
                this.Close();
            }
        }
        // 생성자 오버로딩 문의 확인
        public InquiryAdminForm(int inqId, string userId, string orderId, string title, string content, string reply,  AdminForm adminForm)
        {
            InitializeComponent();
            // DB 연결
            dbClass = new DBClass();
            if (dbClass.ConnectToDatabase())
            {
                this.inqId = inqId;

                // 텍스트 박스에 전달받은 값 할당
                textBox1.Text = userId;
                textBox2.Text = orderId;
                textBox3.Text = title;
                textBox4.Text = content;
                textBox5.Text = reply;

                // AdminForm 참조 설정
                this.adminForm = adminForm;
            }
            else
            {
                MessageBox.Show("DB 연결에 실패했습니다.");
                this.Close();
            }
        }

        private void createbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // 텍스트 박스5의 내용을 DB에 저장하고 hasReply 값을 변경하는 코드 추가
                string reply = textBox5.Text;
                SaveToDatabase(reply);

                // InquiryAdminForm 닫기
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }
        }

        private void InquiryAdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 폼이 닫힐 때 DB 연결 해제
            dbClass.DisconnectFromDatabase();

            // 부모 폼의 DataGridView 갱신
            adminForm.RefreshInquiriesTable();
        }


        // InquiryAdminForm.cs 파일
        private void SaveToDatabase(string reply)
        {
            try
            {
                // inqId를 사용하여 Inquiries 테이블에 reply 저장
                string sqlstr = $"UPDATE Inquiries SET reply = '{reply}', hasReply = 'Y' WHERE inqId = {inqId}";
                dbClass.DCom.CommandText = sqlstr;
                dbClass.DCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터베이스 저장 오류: {ex.Message}");
                // 예외가 발생하면 여기에서 리턴하거나 적절한 조치를 취하세요.
            }
        }

        // InquiryAdminForm.cs 파일
        private void DeleteToDatabase(string reply)
        {
            try
            {
                // inqId를 사용하여 Inquiries 테이블에 reply 저장
                string sqlstr = $"UPDATE Inquiries SET reply = NULL, hasReply = 'N' WHERE inqId = {inqId}";
                dbClass.DCom.CommandText = sqlstr;
                dbClass.DCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터베이스 저장 오류: {ex.Message}");
                // 예외가 발생하면 여기에서 리턴하거나 적절한 조치를 취하세요.
            }
        }

        private void uploadbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // 텍스트 박스5의 내용을 DB에 저장하고 hasReply 값을 변경하는 코드 추가
                string reply = textBox5.Text;
                SaveToDatabase(reply);

                // InquiryAdminForm 닫기
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                // 텍스트 박스5의 내용을 DB에서 삭제하고 hasReply 값을 변경하는 코드 추가
                string reply = textBox5.Text;
                DeleteToDatabase(reply);

                // InquiryAdminForm 닫기
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }
        }
    }
}
