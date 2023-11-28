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
    public partial class AdminForm : Form
    {
        private DBClass dbClass;

        public AdminForm()
        {
            InitializeComponent();
            dbClass = new DBClass();

        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            // DB 연결
            if (dbClass.ConnectToDatabase())
            {
                // USERINFO 테이블을 DataGridView에 표시
                DisplayUserInfoTable();

                // USERINFO 테이블의 열 헤더 설정
                USERINFO_header();

                // Inquiries 테이블을 DataGridView에 표시
                DisplayInquiriesTable();

                // Inquiries 테이블의 열 헤더 설정
                Inquiries_header();

                // DataGridView3의 CellClick 이벤트 핸들러 등록
                dataGridView3.CellClick += dataGridView3_CellClick;
            }
            else
            {
                MessageBox.Show("DB 연결에 실패했습니다.");
                this.Close();
            }
            USERINFO_counter();
            Orders_counter();
            Inquiries_counter();
        }

        public void USERINFO_counter()
        {
            int i;
            i = dataGridView1.RowCount;
            Label1.Text = "총 " + i + " 명";
        }

        private void DisplayUserInfoTable()
        {
            // USERINFO 테이블을 DataGridView에 표시
            string sqlstr = "SELECT userId, name, phone, address, SignUpDate FROM USERINFO ORDER BY userId ASC";
            dbClass.DCom.CommandText = sqlstr;

            // USERINFO 테이블에 데이터를 채움
            dbClass.DS.Tables["USERINFO"].Clear(); // 중복 데이터 제거
            dbClass.DA.SelectCommand = dbClass.DCom;
            dbClass.DA.Fill(dbClass.DS, "USERINFO");

            // DataGridView에 데이터 소스 설정
            dataGridView1.DataSource = dbClass.DS.Tables["USERINFO"].DefaultView;
        }

        private void USERINFO_header()
        {
            // USERINFO 테이블의 열 헤더 설정
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "이름";
            dataGridView1.Columns[2].HeaderText = "전화번호";
            dataGridView1.Columns[3].HeaderText = "주소";
            dataGridView1.Columns[4].HeaderText = "가입 날짜"; // 수정된 부분

            // 각 열의 너비 설정
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 90;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                // 선택한 행의 첫 번째 열 값을 가져와서 텍스트 박스에 표시
                string selectedId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBoxUserId.Text = selectedId;
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxUserId.Text))
            {
                // 선택한 ID에 해당하는 주문 목록을 가져와서 표시
                DisplayUserOrders(textBoxUserId.Text);

                // Orders_counter 호출하여 주문 횟수 갱신
                Orders_counter();
            }
        }

        public void Orders_counter()
        {
            int i = dataGridView2.RowCount;
            Lable2.Text = $"주문횟수: {i} 번";
        }

        private void DisplayUserOrders(string userId)
        {
            // ORDERS 테이블이 없으면 생성
            if (dbClass.DS.Tables["Orders"] == null)
            {
                dbClass.DS.Tables.Add("Orders");
            }

            // Orders 테이블을 사용하여 주문 목록을 가져오는 쿼리
            string orderSql = $"SELECT * FROM Orders WHERE userId = '{userId}'";
            dbClass.DCom.CommandText = orderSql;

            try
            {
                // ORDERS 테이블을 지우고, 주문 목록을 다시 채움
                dbClass.DS.Tables["Orders"].Clear();
                dbClass.DA.SelectCommand = dbClass.DCom;
                dbClass.DA.Fill(dbClass.DS, "Orders");

                // DataGridView에 주문 목록 표시
                dataGridView2.DataSource = dbClass.DS.Tables["Orders"].DefaultView;

                // Orders 테이블의 열 헤더 설정
                Orders_header();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"주문 목록 표시 오류: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        public void Orders_header()
        {
            // Orders 테이블의 열 헤더 설정
            dataGridView2.Columns[0].HeaderText = "주문번호";
            dataGridView2.Columns[1].HeaderText = "주소";
            dataGridView2.Columns[2].HeaderText = "주문상태";
            dataGridView2.Columns[3].HeaderText = "총액";
            dataGridView2.Columns[4].HeaderText = "ID";

            // 각 열의 너비 설정
            dataGridView2.Columns[0].Width = 80;
            dataGridView2.Columns[1].Width = 150;
            dataGridView2.Columns[2].Width = 90;
            dataGridView2.Columns[3].Width = 70;
            dataGridView2.Columns[4].Width = 70;
        }


        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 폼이 닫힐 때 DB 연결 해제
            dbClass.DisconnectFromDatabase();
        }

        // 고객 계정 삭제 기능
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            // 사용자가 선택한지 확인
            if (!string.IsNullOrEmpty(textBoxUserId.Text))
            {
                // 사용자에게 삭제 여부 확인
                DialogResult result = MessageBox.Show("해당 고객의 계정을 삭제하시겠습니까?", "계정 삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string userIdToDelete = textBoxUserId.Text;

                    // Inquiries, Orders, USERINFO 테이블에서 레코드 삭제
                    DeleteInquiries(userIdToDelete);
                    DeleteOrders(userIdToDelete);
                    DeleteUserInfo(userIdToDelete);

                    // DataGridView 새로고침
                    DisplayUserInfoTable();
                    DisplayUserOrders(userIdToDelete);
                    DisplayInquiriesTable();

                    // 카운터 업데이트
                    USERINFO_counter();
                    Orders_counter();
                    Inquiries_counter();
                }
            }
            else
            {
                MessageBox.Show("삭제할 유저를 선택하세요.");
            }
        }

        private void DeleteInquiries(string userId)
        {
            // Inquiries 테이블에서 해당 유저의 레코드 삭제
            string deleteInquiriesSql = $"DELETE FROM Inquiries WHERE userId = '{userId}'";
            dbClass.DCom.CommandText = deleteInquiriesSql;
            dbClass.DCom.ExecuteNonQuery();
        }

        private void DeleteOrders(string userId)
        {
            // Orders 테이블에서 해당 유저의 레코드 삭제
            string deleteOrdersSql = $"DELETE FROM Orders WHERE userId = '{userId}'";
            dbClass.DCom.CommandText = deleteOrdersSql;
            dbClass.DCom.ExecuteNonQuery();
        }

        private void DeleteUserInfo(string userId)
        {
            // USERINFO 테이블에서 해당 유저의 레코드 삭제
            string deleteUserInfoSql = $"DELETE FROM USERINFO WHERE userId = '{userId}'";
            dbClass.DCom.CommandText = deleteUserInfoSql;
            dbClass.DCom.ExecuteNonQuery();
        }

        // 고객 문의 기능
        public void Inquiries_counter()
        {
            int i;
            i = dataGridView3.RowCount;
            Label3.Text = "총 " + i + " 개";
        }

        private void DisplayInquiriesTable()
        {
            // Inquiries 테이블을 DataGridView에 표시
            string sqlstr = "SELECT inqId, orderId, userId, title, createDate, hasReply FROM Inquiries ORDER BY inqId ASC";
            dbClass.DCom.CommandText = sqlstr;

            // Inquiries 테이블에 데이터를 채움
            dbClass.DS.Tables["Inquiries"].Clear(); // 중복 데이터 제거
            dbClass.DA.SelectCommand = dbClass.DCom;
            dbClass.DA.Fill(dbClass.DS, "Inquiries");

            // DataGridView에 데이터 소스 설정
            dataGridView3.DataSource = dbClass.DS.Tables["Inquiries"].DefaultView;
        }
        public void RefreshInquiriesTable()
        {
            DisplayInquiriesTable();
        }
        private void Inquiries_header()
        {
            // Inquiries 테이블의 열 헤더 설정
            dataGridView3.Columns[0].HeaderText = "번호";
            dataGridView3.Columns[1].HeaderText = "주문번호";
            dataGridView3.Columns[2].HeaderText = "아이디";
            dataGridView3.Columns[3].HeaderText = "제목";
            dataGridView3.Columns[4].HeaderText = "작성일자";
            dataGridView3.Columns[5].HeaderText = "답글 여부";

            // 각 열의 너비 설정
            dataGridView3.Columns[0].Width = 80;
            dataGridView3.Columns[1].Width = 80;
            dataGridView3.Columns[2].Width = 80;
            dataGridView3.Columns[3].Width = 80;
            dataGridView3.Columns[4].Width = 80;
            dataGridView3.Columns[5].Width = 80;
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView3.Rows.Count)
            {
                // 선택한 행의 첫 번째 열 값을 가져와서 텍스트 박스에 표시
                var cellValue = dataGridView3.Rows[e.RowIndex].Cells[0].Value;
                if (cellValue != null)
                {
                    string selectedId = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
                    textBox1.Text = selectedId;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 선택한 행의 inqId 값을 가져오기
            if (!string.IsNullOrEmpty(textBox1.Text) && int.TryParse(textBox1.Text, out int inqId))
            {
                // inqId에 해당하는 Inquiries 테이블의 정보 가져오기
                string sqlstr = $"SELECT userId, orderId, title, content FROM Inquiries WHERE inqId = {inqId}";
                dbClass.DCom.CommandText = sqlstr;

                try
                {
                    // DataTable이 없으면 생성
                    if (dbClass.DS.Tables["InquiryDetails"] == null)
                    {
                        dbClass.DS.Tables.Add("InquiryDetails");
                    }

                    dbClass.DS.Tables["InquiryDetails"].Clear();
                    dbClass.DA.SelectCommand = dbClass.DCom;
                    dbClass.DA.Fill(dbClass.DS, "InquiryDetails");

                    // DataTable에서 해당 정보 가져오기
                    string userId = dbClass.DS.Tables["InquiryDetails"].Rows[0]["userId"].ToString();
                    string orderId = dbClass.DS.Tables["InquiryDetails"].Rows[0]["orderId"].ToString();
                    string title = dbClass.DS.Tables["InquiryDetails"].Rows[0]["title"].ToString();
                    string content = dbClass.DS.Tables["InquiryDetails"].Rows[0]["content"].ToString();

                    // InquiryAdminForm 열기
                    InquiryAdminForm inquiryAdminForm = new InquiryAdminForm(inqId, userId, orderId, title, content, this);
                    inquiryAdminForm.ShowDialog();

                    // 고객 문의 테이블 새로고침
                    DisplayInquiriesTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"문의 정보 불러오기 오류: {ex.Message}\nStackTrace: {ex.StackTrace}");
                }
            }
            else
            {
                MessageBox.Show("문의를 선택해주세요.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 선택한 행의 inqId 값을 가져오기
            if (!string.IsNullOrEmpty(textBox1.Text) && int.TryParse(textBox1.Text, out int inqId))
            {
                // inqId에 해당하는 Inquiries 테이블의 정보 가져오기
                string sqlstr = $"SELECT userId, orderId, title, content, reply FROM Inquiries WHERE inqId = {inqId}";
                dbClass.DCom.CommandText = sqlstr;

                try
                {
                    // DataTable이 없으면 생성
                    if (dbClass.DS.Tables["InquiryDetails"] == null)
                    {
                        dbClass.DS.Tables.Add("InquiryDetails");
                    }

                    dbClass.DS.Tables["InquiryDetails"].Clear();
                    dbClass.DA.SelectCommand = dbClass.DCom;
                    dbClass.DA.Fill(dbClass.DS, "InquiryDetails");

                    // DataTable에서 해당 정보 가져오기
                    string userId = dbClass.DS.Tables["InquiryDetails"].Rows[0]["userId"].ToString();
                    string orderId = dbClass.DS.Tables["InquiryDetails"].Rows[0]["orderId"].ToString();
                    string title = dbClass.DS.Tables["InquiryDetails"].Rows[0]["title"].ToString();
                    string content = dbClass.DS.Tables["InquiryDetails"].Rows[0]["content"].ToString();
                    string reply = dbClass.DS.Tables["InquiryDetails"].Rows[0]["reply"].ToString();

                    // InquiryAdminForm 열기
                    InquiryAdminForm inquiryAdminForm = new InquiryAdminForm(inqId, userId, orderId, title, content, reply, this);
                    inquiryAdminForm.ShowDialog();

                    // 고객 문의 테이블 새로고침
                    DisplayInquiriesTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"문의 정보 불러오기 오류: {ex.Message}\nStackTrace: {ex.StackTrace}");
                }
            }
            else
            {
                MessageBox.Show("문의를 선택해주세요.");
            }
        }
    }
}