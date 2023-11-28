using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;//ADO.Net 개체 참조
using System.Data; //DataSet 개체 참조
using System.Windows.Forms; //MessageBox 개체 참조

namespace TeamProject
{

    public static class TextTrim
    {
        public static string TrimWhitespace(string input)
        {
            // 앞뒤의 여백을 제거합니다.
            return input?.Trim();
        }
    }

    class DBClass
    {
        private OracleConnection connection;
        private OracleCommand dCom;
        private OracleDataAdapter dA;
        private DataSet dS;

        private const string connectionString = "User Id=hong1; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
        private const string authenticateUserQuery = "SELECT COUNT(*) FROM USERINFO WHERE UserId = :UserId AND Password = :Password";
        private const string authenticateAdminQuery = "SELECT COUNT(*) FROM ADMININFO WHERE AdminId = :AdminId AND APassword = :APassword";
        private const string checkDuplicateUserIdQuery = "SELECT COUNT(*) FROM USERINFO WHERE UserId = :UserId";
        private const string registerUserQuery = "INSERT INTO USERINFO (UserId, Password, Name, Phone, Address, Email, SignUpDate) VALUES (:UserId, :Password, :Name, :Phone, :Address, :Email, :SignUpDate)";


        public OracleCommand DCom { get { return dCom; } set { dCom = value; } }
        public OracleDataAdapter DA { get { return dA; } set { dA = value; } }
        public DataSet DS { get { return dS; } set { dS = value; } }

        public bool IsAdmin { get; private set; }

        public DBClass()
        {
            connection = new OracleConnection(connectionString);
            dCom = new OracleCommand();
            dCom.Connection = connection;
            dA = new OracleDataAdapter();
            DS = new DataSet();
            DS.Tables.Add("USERINFO");
            DS.Tables.Add("ORDERS");
            DS.Tables.Add("INQUIRIES");
        }

        public bool ConnectToDatabase()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB 연결 오류: " + ex.Message);
                return false;
            }
        }

        private int ExecuteQuery(string query, OracleParameter[] parameters)
        {
            using (OracleCommand command = new OracleCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            }
        }

        private int ExecuteScalarQuery(string query, OracleParameter[] parameters)
        {
            using (OracleCommand command = new OracleCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void DisconnectFromDatabase()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        private void HandleException(string errorMessage, Exception ex)
        {
            // 예외 던지기 대신 호출자에서 처리하도록 변경
            throw new Exception(errorMessage, ex);
        }

        public bool AuthenticateUser(string userId, string password)
        {
            try
            {
                // TextTrimdmf 사용하여 공백 제거
                userId = TextTrim.TrimWhitespace(userId);
                password = TextTrim.TrimWhitespace(password);

                OracleParameter[] parameters = {
                new OracleParameter(":UserId", OracleDbType.Varchar2) { Value = userId },
                new OracleParameter(":Password", OracleDbType.Varchar2) { Value = password }
            };

                return ExecuteScalarQuery(authenticateUserQuery, parameters) > 0;
            }
            catch (Exception ex)
            {
                HandleException("인증 오류", ex);
                return false;
            }
        }

        public bool AuthenticateAdmin(string adminId, string aPassword)
        {
            // TextTrim를 사용하여 공백 제거
            adminId = TextTrim.TrimWhitespace(adminId);
            aPassword = TextTrim.TrimWhitespace(aPassword);

            try
            {
                OracleParameter[] parameters = {
                new OracleParameter(":AdminId", OracleDbType.Varchar2) { Value = adminId },
                new OracleParameter(":APassword", OracleDbType.Varchar2) { Value = aPassword }
            };

                // 여기서 AuthenticateAdmin 메서드의 반환값이 true이면서 IsAdmin 속성을 true로 설정
                IsAdmin = ExecuteScalarQuery(authenticateAdminQuery, parameters) > 0;
                return IsAdmin;
            }
            catch (Exception ex)
            {
                HandleException("인증 오류", ex);
                return false;
            }
        }

        public bool CheckDuplicateUserId(string userId)
        {
            try
            {
                OracleParameter[] parameters = {
                new OracleParameter(":UserId", OracleDbType.Varchar2) { Value = userId }
            };

                return ExecuteScalarQuery(checkDuplicateUserIdQuery, parameters) > 0;
            }
            catch (Exception ex)
            {
                HandleException("중복 체크 오류", ex);
                return false;
            }
        }

        public bool RegisterUser(string userId, string password, string name, string phone, string address, string email)
        {
            // 여백을 제거하여 사용자가 입력한 값을 저장
            userId = TextTrim.TrimWhitespace(userId);
            password = TextTrim.TrimWhitespace(password);
            name = TextTrim.TrimWhitespace(name);
            phone = TextTrim.TrimWhitespace(phone);
            address = TextTrim.TrimWhitespace(address);
            email = TextTrim.TrimWhitespace(email);

            try
            {
                OracleParameter[] parameters = {
                new OracleParameter(":UserId", OracleDbType.Varchar2) { Value = userId },
                new OracleParameter(":Password", OracleDbType.Varchar2) { Value = password },
                new OracleParameter(":Name", OracleDbType.Varchar2) { Value = name },
                new OracleParameter(":Phone", OracleDbType.Varchar2) { Value = phone },
                new OracleParameter(":Address", OracleDbType.Varchar2) { Value = address },
                new OracleParameter(":Email", OracleDbType.Varchar2) { Value = email },
                new OracleParameter(":SignUpDate", OracleDbType.Date) { Value = DateTime.Now }
            };

                return ExecuteScalarQuery(registerUserQuery, parameters) > 0;
            }
            catch (Exception ex)
            {
                HandleException("회원가입 오류", ex);
                return false;
            }
        }

        // DBClass.cs 파일
        public void SaveReplyToDatabase(int inqId, string reply)
        {
            try
            {
                // inqId를 사용하여 Inquiries 테이블의 해당 레코드를 찾아 업데이트
                string updateQuery = "UPDATE Inquiries SET reply = :Reply, hasReply = 'Y' WHERE inqId = :InqId";
                OracleParameter[] parameters = {
            new OracleParameter(":Reply", OracleDbType.Varchar2) { Value = reply },
            new OracleParameter(":InqId", OracleDbType.Int32) { Value = inqId }
        };

                // ExecuteQuery 메서드를 호출
                ExecuteQuery(updateQuery, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB 저장 오류: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

    }
}