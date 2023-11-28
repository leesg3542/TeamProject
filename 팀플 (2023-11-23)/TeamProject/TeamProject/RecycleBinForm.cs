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
    public partial class RecycleBinForm : Form
    {
        private DBClass dbClass;

        /*
        public RecycleBinForm(DBClass dbClass)
        {
            InitializeComponent();
            this.dbClass = dbClass;
        }
        */
        private void RecycleBinForm_Load(object sender, EventArgs e)
        {
            DisplayRecycleBin();
        }

        private void DisplayRecycleBin()
        {
            listBoxRecycleBin.Items.Clear(); // 기존 아이템 지우기

            string sqlstr = "SELECT * FROM RecycleBin ORDER BY actionDate DESC";
            dbClass.DCom.CommandText = sqlstr;

            using (OracleDataReader reader = dbClass.DCom.ExecuteReader())
            {
                while (reader.Read())
                {
                    // ListBox에 관련 정보를 표시
                    string item = $"{reader["recordId"]} - {reader["tableName"]} - {reader["columnName"]} - {reader["columnValue"]} - {reader["actionType"]}";
                    listBoxRecycleBin.Items.Add(item);
                }
            }
        }

        private void btnPermanentlyDelete_Click(object sender, EventArgs e)
        {
            if (listBoxRecycleBin.SelectedIndex >= 0)
            {
                string selectedItem = listBoxRecycleBin.SelectedItem.ToString();
                string[] itemParts = selectedItem.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                if (itemParts.Length >= 4)
                {
                    string recordId = itemParts[0].Trim();
                    string tableName = itemParts[1].Trim();

                    // 완전 삭제할 데이터를 휴지통에서 삭제
                    string deleteSql = $"DELETE FROM RecycleBin WHERE recordId = '{recordId}' AND tableName = '{tableName}'";
                    dbClass.DCom.CommandText = deleteSql;
                    dbClass.DCom.ExecuteNonQuery();

                    // 완전 삭제된 데이터를 휴지통 테이블에서 완전 삭제
                    PermanentlyDeleteFromRecycleBin(recordId, tableName);

                    // 갱신된 휴지통 테이블을 ListBox에 반영
                    DisplayRecycleBin();
                }
            }
        }
        private void PermanentlyDeleteFromRecycleBin(string recordId, string tableName)
        {
            // 완전 삭제된 데이터를 휴지통 테이블에서 완전 삭제
            string deleteSql = $"DELETE FROM RecycleBin WHERE recordId = '{recordId}' AND tableName = '{tableName}'";
            dbClass.DCom.CommandText = deleteSql;
            dbClass.DCom.ExecuteNonQuery();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (listBoxRecycleBin.SelectedIndex >= 0)
            {
                string selectedItem = listBoxRecycleBin.SelectedItem.ToString();
                string[] itemParts = selectedItem.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                if (itemParts.Length >= 4)
                {
                    string recordId = itemParts[0].Trim();
                    string tableName = itemParts[1].Trim();

                    // 복구할 데이터를 휴지통에서 삭제
                    string deleteSql = $"DELETE FROM RecycleBin WHERE recordId = '{recordId}' AND tableName = '{tableName}'";
                    dbClass.DCom.CommandText = deleteSql;
                    dbClass.DCom.ExecuteNonQuery();

                    // 복구할 데이터를 원래 테이블로 이동
                    MoveToOriginalTable(recordId, tableName);

                    // 갱신된 휴지통 테이블을 ListBox에 반영
                    DisplayRecycleBin();
                }
            }
        }

        private void MoveToOriginalTable(string recordId, string tableName)
        {
            // 휴지통에서 원래 테이블로 데이터 이동
            string selectSql = $"SELECT * FROM RecycleBin WHERE recordId = '{recordId}' AND tableName = '{tableName}'";
            dbClass.DCom.CommandText = selectSql;

            using (OracleDataReader reader = dbClass.DCom.ExecuteReader())
            {
                if (reader.Read())
                {
                    // 원래 테이블로 데이터 추가
                    string insertSql = $"INSERT INTO {tableName} ({reader["columnName"]}) VALUES ('{reader["columnValue"]}')";

                    dbClass.DCom.CommandText = insertSql;
                    dbClass.DCom.ExecuteNonQuery();
                }
            }
        }
    }
}