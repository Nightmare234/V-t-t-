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
using System.Configuration;
using System.Linq.Expressions;

namespace Vật_tư
{
    public partial class Thongtinnhacungcap : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        public Thongtinnhacungcap()
        {
            InitializeComponent();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string p_Manhacungcap = txtManhacungcap.Text.Trim();
            
            SqlConnection con = new SqlConnection("Data Source = DESKTOP - FIH9LQ8\\SQLEXPRESS;Initial Catalog = QLVT;Intergrated Security=True");
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string sql = "Delete Thongtinnhacungcap where Manhacungcap= '" + p_Manhacungcap + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Xóa thành công");
            Load_dgvThongtinncc();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string p_Manhacungcap = txtManhacungcap.Text.Trim();
            string p_Tennhacungcap = txtTennhacungcap.Text.Trim();
            string p_Sodienthoai = txtSodienthoai.Text.Trim();
            string p_Diachi = txtDiachi.Text.Trim();
            string p_Ghichu = txtGhichu.Text.Trim();

            SqlConnection con = new SqlConnection("Data Source = DESKTOP - FIH9LQ8\\SQLEXPRESS;Initial Catalog = QLVT;Intergrated Security=True");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string sql = "Update Thongtinnhacungcap set Tennhacungcap= N'" + p_Tennhacungcap + "', Sodienthoai='" + p_Sodienthoai + "',Diachi= N'" + p_Diachi + "', Ghichu =N'" + p_Ghichu + "' where Manhacungcap = '" + p_Manhacungcap + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Sửa thành công");
            Load_dgvThongtinncc();
            txtManhacungcap.Enabled = true;
        }

        private void Thongtinnhacungcap_Load(object sender, EventArgs e)
        {
            Load_dgvThongtinncc();
        }

        private void dgvThongtinncc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtManhacungcap.Text = dgvThongtinncc.Rows[i].Cells[0].Value.ToString();
            txtTennhacungcap.Text = dgvThongtinncc.Rows[i].Cells[1].Value.ToString();
            txtSodienthoai.Text = dgvThongtinncc.Rows[i].Cells[2].Value.ToString();
            txtDiachi.Text = dgvThongtinncc.Rows[i].Cells[3].Value.ToString();
            txtGhichu.Text = dgvThongtinncc.Rows[i].Cells[4].Value.ToString();
        }
        private void Load_dgvThongtinncc()
        {
            SqlConnection con = new SqlConnection("Data Source = DESKTOP - FIH9LQ8\\SQLEXPRESS;Initial Catalog = QLVT;Intergrated Security=True");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string sql = "select * from Thongtinnhacungcap";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();
            dgvThongtinncc.DataSource = tb;
            dgvThongtinncc.Refresh();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string p_Manhacungcap = txtManhacungcap.Text.Trim();
                string p_Tennhacungcap = txtTennhacungcap.Text.Trim();
                string p_Sodienthoai = txtSodienthoai.Text.Trim();
                string p_Diachi = txtDiachi.Text.Trim();
                string p_Ghichu = txtGhichu.Text.Trim();

                if (p_Manhacungcap == "")
                {
                    MessageBox.Show("Mã nhà cung cấp không được để trống ");
                    txtManhacungcap.Focus();
                    return;
                }

                if(checktrungma(p_Manhacungcap) == true)
                {
                    MessageBox.Show("Mã nhà cung cấp đã tồn tại ");
                    txtManhacungcap.Focus();
                    return;
                }

                SqlConnection con = new SqlConnection("Data Source = DESKTOP - FIH9LQ8\\SQLEXPRESS;Initial Catalog = QLVT;Intergrated Security=True");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string sql = "INSERT Thongtinnhacungcap values('" + p_Manhacungcap + "',N'" + p_Tennhacungcap + "',N'" + p_Sodienthoai + "',N'" + p_Diachi + "',N'" + p_Ghichu + "',N'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                MessageBox.Show("Thêm mới thành công ");
                Load_dgvThongtinncc();
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống. Liên hệ với quản trị viên");
            }
        }
        private bool checktrungma(string p_Manhacungcap)
        {
            SqlConnection con = new SqlConnection("Data Source = DESKTOP - FIH9LQ8\\SQLEXPRESS;Initial Catalog = QLVT;Intergrated Security=True");
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string sql = "select count(*) From Thongtinnhacungcap where Manhacungcap = '" + p_Manhacungcap+"'";
            SqlCommand cmd = new SqlCommand(sql, con);
            int kq = (int)cmd.ExecuteScalar();
            cmd.Dispose();
            con.Close();
            if (kq > 0) return true;
            else return false;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string p_Manhacungcap = txtManhacungcap.Text.Trim();
            string p_Tennhacungcap = txtTennhacungcap.Text.Trim();
            string p_Sodienthoai = txtSodienthoai.Text.Trim();
            string p_Diachi = txtDiachi.Text.Trim();
            string p_Ghichu = txtGhichu.Text.Trim();

            SqlConnection con = new SqlConnection("Data Source = DESKTOP - FIH9LQ8\\SQLEXPRESS;Initial Catalog = QLVT;Intergrated Security=True");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string sql = "Select * From Thongtinnhacungcap where Manhacungcap like '%" + p_Manhacungcap + "%' and Tennhacungcap like N'" + p_Tennhacungcap + "' and Sodienthoai like '%" + p_Sodienthoai + "%' and Ghichu like 'N" + p_Ghichu + "%' and Diachi like N'%" + p_Diachi + "%'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();
            dgvThongtinncc.DataSource = tb;
            dgvThongtinncc.Refresh();
        }
    }
}
