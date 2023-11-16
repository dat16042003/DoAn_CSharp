using DOAN_BUIVANDAT.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_BUIVANDAT
{
    public partial class frmMain : Form
    {
        //public static string tentaikhoan = null;
        public frmMain()
        {
            InitializeComponent();
        }
        private Form currentFromChild;
        private void openChildForm(Form childForm)
        {
            if(currentFromChild!=null )
            {
                currentFromChild.Close();
            }
            currentFromChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnMain.Controls.Add(childForm);
            pnMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        public static NguoiDung nguoidung = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (nguoidung == null)
            {
                Form frmdn = new frmDangNhap();
                frmdn.ShowDialog();
               

            }
            if (nguoidung != null)
            {
                lblTenTaiKhoan.Text = nguoidung.TaiKhoan;
            }
        }

        private void btnNhanvien_Click(object sender, EventArgs e)
        {
            openChildForm(new frmNhanVien());
            
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            openChildForm(new frmSanPham());
         
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmTimKiem());
          
        }

        private void btnKhachhang_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKhachHang());
          
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            openChildForm(new frmHoaDonBanHang());
            
        }

        private void btnLoaiHang_Click(object sender, EventArgs e)
        {
            openChildForm(new frmLoaiSP());
        }
    }
}
