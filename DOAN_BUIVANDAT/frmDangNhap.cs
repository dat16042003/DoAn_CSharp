using DOAN_BUIVANDAT.DAO;
using DOAN_BUIVANDAT.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_BUIVANDAT
{
    public partial class frmDangNhap : Form
    {

        public frmDangNhap()
        {
            InitializeComponent();

        }


        private void bntDangNhap_Click(object sender, EventArgs e)
        {
            string tendn = txtTenDN.Text.Trim();
            string matkhau = txtMatkhau.Text.Trim();
            NguoiDungDAO tvDAO = new NguoiDungDAO();
            NguoiDung nguoidung = tvDAO.getRow(tendn, matkhau);
            if (nguoidung == null)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác.Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                frmMain.nguoidung = nguoidung;
                this.Close();
                /*Form frmmain = new frmMain();
                  frmmain.ShowDialog();
                  this.Close();*/
            }
        }

        private void btnTh_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
           
        }     
        private void frmDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }
    }


}


