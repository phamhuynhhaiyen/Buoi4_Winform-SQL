using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5951071125_PhamHuynhHaiYen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-OCUUTDBF\SQLEXPRESS;Initial Catalog=DemoCRUB;Integrated Security=True");
        private void GetStudentRecord()
        {
            
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();
            dgvSV.DataSource = dt;
        }
        private bool IsValidData()
        {
            if (txtHo.Text == string.Empty|| txtTen.Text == string.Empty || string.IsNullOrEmpty(txtSBD.Text)
                ||string.IsNullOrEmpty(txtSDT.Text)  || txtDiaChi.Text == string.Empty)
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu!","Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }          
            return true;
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                string s = string.Format("INSERT INTO StudentsTb VALUES ( N'{0}', N'{1}', N'{2}', N'{3}', '{4}')",txtHo.Text, txtTen.Text, txtSBD.Text, txtDiaChi.Text, txtSDT.Text);
                SqlCommand cmd = new SqlCommand(s, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
                ResetData();
            }
        }
        public int studentID;
        private void dgvSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvSV.Rows[e.RowIndex];
            studentID = int.Parse((row.Cells[0]).Value.ToString());
            txtHo.Text = row.Cells[1].Value.ToString();
            txtTen.Text = row.Cells[2].Value.ToString();
            txtSBD.Text = row.Cells[3].Value.ToString();
            txtDiaChi.Text = row.Cells[4].Value.ToString();
            txtSDT.Text = row.Cells[5].Value.ToString();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (studentID > 0)
            {
                string s = string.Format("UPDATE StudentsTb SET Name = N'{0}', FatherName= N'{1}', RollNumber= N'{2}', Address= N'{3}', Mobile= '{4}' WHERE StudentID = {5}", txtHo.Text, txtTen.Text, txtSBD.Text, txtDiaChi.Text, txtSDT.Text, studentID);
                SqlCommand cmd = new SqlCommand(s, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Cập nhật lỗi!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ResetData()
        {
            txtHo.Text = txtTen.Text = txtSBD.Text = txtDiaChi.Text = txtSDT.Text = "";
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (studentID > 0)
            {
                string s = string.Format("DELETE StudentsTb WHERE StudentID = {0}",studentID);
                SqlCommand cmd = new SqlCommand(s, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Cập nhật lỗi!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXacLap_Click(object sender, EventArgs e)
        {
            ResetData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
