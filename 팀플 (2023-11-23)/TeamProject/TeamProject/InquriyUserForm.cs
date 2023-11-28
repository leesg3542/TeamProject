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
    public partial class InquriyUserForm : Form
    {
        private DBClass dbClass;
        private string userId;
        private string orderId;

        // 생성자 오버로딩: 새로운 문의 작성
        public InquriyUserForm(string userId, string orderId)
        {
            InitializeComponent();
            dbClass = new DBClass();
            this.userId = userId;
            this.orderId = orderId;

            // 레이블에 값을 설정
            textBox1.Text = $"{userId}";
            textBox2.Text = $"{this.orderId}"; // 클래스 멤버 변수 사용
        }

        // 생성자 오버로딩: 문의 수정
        public InquriyUserForm(string userId, string orderId, string title, string content)
        {
            InitializeComponent();
            dbClass = new DBClass();
            this.userId = userId;
            this.orderId = orderId;

            // 레이블에 값을 설정
            textBox1.Text = $"{userId}";
            textBox2.Text = $"{this.orderId}"; // 클래스 멤버 변수 사용

            // title과 content를 텍스트 박스에 설정
            textBox3.Text = title;
            textBox4.Text = content;

            // 답변 정보를 불러오고 텍스트 박스에 표시
            UpdateInquiryReplyTextBox();
        }


        // "작성" 버튼 클릭 시 실행되는 이벤트 핸들러
        private void createbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // DB 연결
                if (!dbClass.ConnectToDatabase())
                {
                    MessageBox.Show("DB 연결에 실패했습니다.");
                    return;
                }

                // 이미 해당 주문번호로 작성한 고객문의가 있는지 확인
                string checkExistingQuery = $"SELECT COUNT(*) FROM Inquiries WHERE orderId = '{orderId}' AND userId = '{userId}'";
                dbClass.DCom.CommandText = checkExistingQuery;

                int existingCount = Convert.ToInt32(dbClass.DCom.ExecuteScalar());

                if (existingCount > 0)
                {
                    MessageBox.Show("이미 해당 주문번호로 작성한 고객문의가 있습니다. 수정 기능을 이용해주세요.");
                    return;
                }

                // TextTrim 사용하여 공백 제거
                string title = TextTrim.TrimWhitespace(textBox3.Text);
                string content = TextTrim.TrimWhitespace(textBox4.Text);

                // createDate를 현재 날짜로 설정
                DateTime createDate = DateTime.Now;

                // SQL 쿼리를 생성하여 DB에 추가
                string insertQuery = $"INSERT INTO Inquiries (inqId, orderId, userId, title, content, createDate, hasReply) " +
                                    $"VALUES (inqId_sequence.NEXTVAL, '{orderId}', '{userId}', '{title}', '{content}', " +
                                    $"TO_TIMESTAMP('{createDate:yyyy-MM-dd HH:mm}', 'YYYY-MM-DD HH24:MI'), 'N')";

                dbClass.DCom.CommandText = insertQuery;
                dbClass.DCom.ExecuteNonQuery();

                MessageBox.Show("문의가 성공적으로 작성되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"문의 작성 중 오류 발생: {ex.Message}");
            }
            finally
            {
                // DB 연결 종료
                dbClass.DisconnectFromDatabase();
                // 폼 닫기
                this.Close();
            }
        }

        // "수정" 버튼 클릭 시 실행되는 이벤트 핸들러
        private void uploadbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // DB 연결
                if (!dbClass.ConnectToDatabase())
                {
                    MessageBox.Show("DB 연결에 실패했습니다.");
                    return;
                }

                // TextTrim 사용하여 공백 제거
                string title = TextTrim.TrimWhitespace(textBox3.Text);
                string content = TextTrim.TrimWhitespace(textBox4.Text);

                // SQL 쿼리를 생성하여 DB에 수정
                string updateQuery = $"UPDATE Inquiries SET title = '{title}', content = '{content}' WHERE orderId = '{orderId}' AND userId = '{userId}'";

                dbClass.DCom.CommandText = updateQuery;
                dbClass.DCom.ExecuteNonQuery();

                MessageBox.Show("문의가 성공적으로 수정되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"문의 수정 중 오류 발생: {ex.Message}");
            }
            finally
            {
                // DB 연결 종료
                dbClass.DisconnectFromDatabase();
                // 폼 닫기
                this.Close();
            }
        }

        // "삭제" 버튼 클릭 시 실행되는 이벤트 핸들러
        private void deletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                // DB 연결
                if (!dbClass.ConnectToDatabase())
                {
                    MessageBox.Show("DB 연결에 실패했습니다.");
                    return;
                }

                // 사용자에게 삭제 확인 메시지 표시
                DialogResult result = MessageBox.Show("문의 글을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // SQL 쿼리를 생성하여 DB에서 삭제 (orderId를 기준으로 삭제)
                    string deleteQuery = $"DELETE FROM Inquiries WHERE orderId = '{orderId}'";

                    dbClass.DCom.CommandText = deleteQuery;
                    dbClass.DCom.ExecuteNonQuery();

                    MessageBox.Show("문의가 성공적으로 삭제되었습니다.");

                    // DB 연결 종료
                    dbClass.DisconnectFromDatabase();

                    // 폼 닫기
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                // 아니오 버튼을 클릭한 경우
                else if (result == DialogResult.No)
                {
                    // 아무 동작 없이 종료
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"문의 삭제 중 오류 발생: {ex.Message}");
            }
            finally
            {
                // DB 연결 종료
                dbClass.DisconnectFromDatabase();
            }
        }

        private void UpdateInquiryReplyTextBox()
        {
            try
            {
                // DB 연결
                if (!dbClass.ConnectToDatabase())
                {
                    MessageBox.Show("DB 연결에 실패했습니다.");
                    return;
                }

                // Inquiries 테이블에서 reply 값 가져오기 (inqId를 기준으로 수정)
                string getReplyQuery = $"SELECT reply FROM Inquiries WHERE orderId = '{orderId}' AND userId = '{userId}'";
                dbClass.DCom.CommandText = getReplyQuery;

                object replyObj = dbClass.DCom.ExecuteScalar();
                string reply = (replyObj != null && replyObj != DBNull.Value) ? replyObj.ToString() : string.Empty;

                // reply 값이 null이 아니면 텍스트박스에 표시
                if (!string.IsNullOrEmpty(reply))
                {
                    textBox5.Text = reply;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"답변 정보 불러오기 오류: {ex.Message}");
            }
            finally
            {
                // DB 연결 종료
                dbClass.DisconnectFromDatabase();
            }
        }
    }
}
