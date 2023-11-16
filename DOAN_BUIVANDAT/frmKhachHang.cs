using DOAN_BUIVANDAT.DAO;
using DOAN_BUIVANDAT.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_BUIVANDAT
{
    public partial class frmKhachHang : Form
    {
        int rowindex = -1;
        QLBDContext db = new QLBDContext();
        KhachHangDAO khachHangDAO = new KhachHangDAO();
        private string AddOrEdit = null;



        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnXoaKH.Enabled = false;
            ShowAndHidden(false);
            loadKhachHang();
            //loadLoaiSP();
        }
        private void loadKhachHang()
        {
            dgvDanhSachKH.DataSource = db.KhachHangs.Select(p => new { p.MaKH, p.TenKH, p.DiaChi, p.SDT }).ToList();
        }
        private void ShowAndHidden(bool show)
        {
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
        }
        public void ResetText1()
        {
            txtMaKH.Clear();
            txtDiaChi.Clear();
            txtTenKH.Clear();
            mtDienThoai.Clear();
        }
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            ShowAndHidden(true);
            AddOrEdit = "Add";
            btnLuu.Enabled = true;
            ResetText1();
        }



        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            txtMaKH.Enabled = false;
            btnLuu.Enabled = true;
            AddOrEdit = "Edit";
          
        }
        public bool checkKH(string makhachhang)
        {
            if (dgvDanhSachKH.Rows.Count == 0)
            {
                return true;
            }
            for (int row = 0; row < dgvDanhSachKH.Rows.Count - 1; row++)
            {
                if (dgvDanhSachKH.Rows[row].Cells["makhachhang"].Value.ToString() == makhachhang)
                {
                    return false;
                }
            }
            return true;
        }

        private void dgvDanhSachKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKH.Enabled = false;
            btnXoaKH.Enabled = true;
            btnSuaKH.Enabled = true;
            btnLuu.Enabled = false;
            rowindex = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvDanhSachKH.Rows.Count)
            {
                int index = e.RowIndex;
                txtMaKH.Text = dgvDanhSachKH.Rows[rowindex].Cells["makhachhang"].Value.ToString();
                txtTenKH.Text = dgvDanhSachKH.Rows[rowindex].Cells["tenkhachhang"].Value.ToString();
                txtDiaChi.Text = dgvDanhSachKH.Rows[rowindex].Cells["diachi"].Value.ToString();
                mtDienThoai.Text = dgvDanhSachKH.Rows[rowindex].Cells["sodienthoai"].Value.ToString();
            }

        }
        public bool checkMaKH(string manv)
        {
            if (dgvDanhSachKH.Rows.Count == 0)
            {
                return true;
            }
            for (int row = 0; row < dgvDanhSachKH.Rows.Count - 1; row++)
            {
                if (dgvDanhSachKH.Rows[row].Cells["makhachhang"].Value.ToString() == manv)
                {
                    return false;
                }
            }
            return true;
        }
        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            btnXoaKH.Enabled = false;
            int maSP = int.Parse(txtMaKH.Text.Trim());
            KhachHang lh = khachHangDAO.getRow(maSP);
            khachHangDAO.Delete(lh);
            loadKhachHang();
            ResetText1();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (!checkMaKH(txtMaKH.Text))
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }*/
                if (txtMaKH.Text.Length.Equals(0))
                {
                    throw new Exception("Mã khách hàng không được để trống");
                }
                if (txtTenKH.Text.Length.Equals(0))
                {
                    throw new Exception("Tên khách hàng không được để trống");
                }
                if (txtDiaChi.Text.Length.Equals(0))
                {
                    throw new Exception("Địa chỉ  không được để trống");
                }
                if (mtDienThoai.Text.Length.Equals(0))
                {
                    throw new Exception("SDT không được để trống");
                }
                if (AddOrEdit == "Add")
                {
                    //Luu vào CSDL
                    KhachHangDAO khachHangDAO = new KhachHangDAO();
                    KhachHang kh = new KhachHang();
                    kh.MaKH = int.Parse(txtMaKH.Text.Trim());
                    kh.TenKH = txtTenKH.Text.Trim();

                    kh.SDT = mtDienThoai.Text.Trim();
                    kh.DiaChi = txtDiaChi.Text.Trim();

                    khachHangDAO.Insert(kh);
                    db.SaveChanges();
                    loadKhachHang();
                }
                if (AddOrEdit == "Edit")
                {
                    //Update
                    int maKH = int.Parse(txtMaKH.Text.Trim());
                    KhachHang kh = khachHangDAO.getRow(maKH);
                    kh.MaKH = int.Parse(txtMaKH.Text.Trim());
                    kh.TenKH = txtTenKH.Text.Trim();             
                    kh.SDT = mtDienThoai.Text.Trim();
                    kh.DiaChi = txtDiaChi.Text.Trim();
                    khachHangDAO.Update(kh);
                    dgvDanhSachKH.DataSource = khachHangDAO.getList();
                }
                txtMaKH.Clear();
                txtDiaChi.Clear();
                txtTenKH.Clear();
                mtDienThoai.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string input = txtTimKiem.Text;
            string TenKH = txtTenKH.Text;
            if (int.TryParse(input, out int number))
            {
                KhachHangDAO khachHangDAO = new KhachHangDAO();
                List<KhachHang> foundProducts = khachHangDAO.TimKiemKhachHang(number, string.Empty);
                if (foundProducts.Count > 0)
                {
                    dgvDanhSachKH.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                KhachHangDAO khachHangDAO = new KhachHangDAO();
                List<KhachHang> foundProducts = khachHangDAO.TimKiemKhachHang(0, input);

                if (foundProducts.Count > 0)
                {
                    dgvDanhSachKH.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng với tên đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

       

        private void btnAll_Click(object sender, EventArgs e)
        {
            loadKhachHang();
        }
    }
}
