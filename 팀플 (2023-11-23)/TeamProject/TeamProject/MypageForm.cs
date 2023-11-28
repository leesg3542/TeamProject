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
    public partial class MypageForm : Form
    {
        private DBClass dbClass;
        private string userId; // userId를 저장할 변수 추가
        private string orderId; // orderId를 저장할 변수 추가
        private string InqId; // InqId를 저장할 변수 추가

        public string SelectedOrderId { get; set; }
        public string SelectedInqId { get; set; }

        public MypageForm(string loggedInUserId)
        {
            InitializeComponent();
            dbClass = new DBClass();

            // 생성자에서 userId 초기화
            userId = loggedInUserId;

            // TextBox1 텍스트 업데이트
            UpdateTextBox1();

            LoadDataToGridView(); // 데이터 로드 메서드 호출
        }

        // SetLoggedInUserId 메서드 정의 추가
        public void SetLoggedInUserId(string loggedInUserId)
        {
            userId = loggedInUserId;
            // TextBox1 텍스트 업데이트
            UpdateTextBox1();
        }

        private void UpdateTextBox1()
        {
            textBox1.Text = $"{userId}";
        }

        public void ResetMypage()
        {
            // userId 초기화
            userId = null;

            // TextBox1 초기화
            UpdateTextBox1();

            // DataGridView 초기화
            dataGridView1.DataSource = null;

            // 주문 횟수 초기화
            Label2.Text = "주문횟수: 0 번";
        }

        private void MypageForm_Load(object sender, EventArgs e)
        {
            // DB 연결
            if (dbClass.ConnectToDatabase())
            {
                // Orders 테이블을 DataGridView에 표시
                DisplayOrdersTable();

                // Orders 테이블의 열 헤더 설정
                Orders_header();

                // Orders 테이블의 행 수를 표시
                Orders_counter();

                // Inquiries 테이블을 DataGridView에 표시
                DisplayInquiriesTable();

                // DataGridView2의 CellClick 이벤트 핸들러 등록
                dataGridView2.CellClick += dataGridView2_CellClick;

                // Inquiries 테이블의 열 헤더 설정
                Inquiries_header();

                // Inquiries 테이블의 행 수를 표시
                Inquiries_counter();
            }
            else
            {
                MessageBox.Show("DB 연결에 실패했습니다.");
                this.Close();
            }
        }

        private void LoadInquiriesData()
        {
            try
            {
                // DB 연결
                if (!dbClass.ConnectToDatabase())
                {
                    MessageBox.Show("DB 연결에 실패했습니다.");
                    return;
                }

                // SQL 쿼리를 생성하여 데이터 가져오기
                string selectQuery = $"SELECT * FROM Inquiries WHERE userId = '{userId}'";
                OracleDataAdapter dataAdapter = new OracleDataAdapter(selectQuery, dbClass.DCom.Connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Inquiries");

                // 그리드뷰 데이터 초기화
                dataGridView2.DataSource = dataSet.Tables["Inquiries"];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로드 중 오류 발생: {ex.Message}");
            }
            finally
            {
                // DB 연결 종료
                dbClass.DisconnectFromDatabase();
            }
        }

        private void LoadDataToGridView()
        {
            try
            {
                // DB 연결
                if (!dbClass.ConnectToDatabase())
                {
                    MessageBox.Show("DB 연결에 실패했습니다.");
                    return;
                }

                // DataGridView2 초기화
                dataGridView2.Columns.Clear();
                dataGridView2.Rows.Clear();

                // SQL 쿼리를 생성하여 데이터 가져오기
                string selectQuery = "SELECT * FROM Inquiries";
                OracleDataAdapter dataAdapter = new OracleDataAdapter(selectQuery, dbClass.DCom.Connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Inquiries");

                // 그리드뷰 데이터 초기화
                dataGridView2.DataSource = dataSet.Tables["Inquiries"];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로드 중 오류 발생: {ex.Message}");
            }
            finally
            {
                // DB 연결 종료
                dbClass.DisconnectFromDatabase();
            }
        }

        public void Orders_counter()
        {
            int i = dataGridView1.RowCount;
            Label2.Text = $"주문횟수: {i} 번";
        }

        private void DisplayOrdersTable()
        {
            // Orders 테이블을 DataGridView에 표시
            string orderSql = $"SELECT orderId, DeliveryAddress, OrderStatus, TotalAmount FROM Orders WHERE userId = '{userId}'";
            dbClass.DCom.CommandText = orderSql;

            // Orders 테이블에 데이터를 채움
            dbClass.DA.SelectCommand = dbClass.DCom;
            dbClass.DA.Fill(dbClass.DS, "Orders");

            // DataGridView에 데이터 소스 설정
            dataGridView1.DataSource = dbClass.DS.Tables["Orders"].DefaultView;
        }
        public void Orders_header()
        {
            // Orders 테이블의 열 헤더 설정
            dataGridView1.Columns[0].HeaderText = "주문번호";
            dataGridView1.Columns[1].HeaderText = "주소";
            dataGridView1.Columns[2].HeaderText = "주문상태";
            dataGridView1.Columns[3].HeaderText = "총액";

            // 각 열의 너비 설정
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].Width = 70;
        }

        // MypageForm 클래스에서 DataGridView의 CellClick 이벤트 핸들러로 변경
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                // 선택한 행의 첫 번째 열 값을 가져와서 textBox2에 표시
                SelectedOrderId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                // orderId에 선택된 주문 ID 설정
                orderId = SelectedOrderId;

                // textBox2에 주문번호 표시
                textBox2.Text = $"{SelectedOrderId}";
            }
        }

        // MypageForm 클래스에서 문의 버튼 클릭 이벤트 핸들러
        private void button1_Click(object sender, EventArgs e)
        {
            // textBox2에 주문번호가 표시되지 않았을 경우 메시지를 표시하고 함수 종료
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("주문을 선택하세요.");
                return;
            }

            // textBox2에 표시된 주문번호를 가져와서 고객 문의 폼을 생성하고 열기
            string selectedOrderId = textBox2.Text.Replace("주문번호 : ", "").Trim();
            InquriyUserForm inquiryForm = new InquriyUserForm(userId, orderId);

            // inquiryForm을 열도록 추가
            inquiryForm.ShowDialog();

            // DataGridView2를 새로 고침
            dataGridView2.Refresh();
        }


        public void Inquiries_counter()
        {
            int i = dataGridView2.RowCount;
            Label3.Text = $"작성한 문의 글: {i} 개";
        }

        // DisplayInquiriesTable 메서드 추가
        private void DisplayInquiriesTable()
        {
            // Inquiries 테이블을 DataGridView에 표시
            string inquirySql = $"SELECT inqId, title, createDate, hasReply FROM Inquiries WHERE userId = '{userId}'";
            dbClass.DCom.CommandText = inquirySql;

            // Inquiries 테이블에 데이터를 채움
            dbClass.DA.SelectCommand = dbClass.DCom;
            dbClass.DA.Fill(dbClass.DS, "Inquiries");


            // DataGridView에 데이터 소스 설정
            dataGridView2.DataSource = dbClass.DS.Tables["Inquiries"].DefaultView;
        }

        private void Inquiries_header()
        {
            // Inquiries 테이블의 열 헤더 설정
            dataGridView2.Columns[0].HeaderText = "문의 번호";
            dataGridView2.Columns[1].HeaderText = "제목";
            dataGridView2.Columns[2].HeaderText = "작성일";
            dataGridView2.Columns[3].HeaderText = "답변 여부";

            // 각 열의 너비 설정
            dataGridView2.Columns[0].Width = 90;
            dataGridView2.Columns[1].Width = 150;
            dataGridView2.Columns[2].Width = 80;
            dataGridView2.Columns[3].Width = 90;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < dataGridView2.Rows.Count)
            {
                // 선택한 행의 첫 번째 열 값을 가져와서 텍스트 박스에 표시
                string selectedId = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox3.Text = selectedId;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 선택한 행의 inqId 값을 가져오기
            if (!string.IsNullOrEmpty(textBox3.Text) && int.TryParse(textBox3.Text, out int inqId))
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

                    // InquriyUserForm 열기
                    InquriyUserForm inquriyUserForm = new InquriyUserForm(userId, orderId, title, content);
                    // inquiryForm을 열도록 추가
                    if (inquriyUserForm.ShowDialog() == DialogResult.OK)
                    {
                        // 고객 문의 테이블 새로 고침
                        dataGridView2.Refresh();
                    }
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