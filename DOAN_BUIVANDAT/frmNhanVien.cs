using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using DOAN_BUIVANDAT.DAO;
using DOAN_BUIVANDAT.Model;

namespace DOAN_BUIVANDAT
{

    public partial class frmNhanVien : Form
    {
        int rowindex = -1;
        QLBDContext db = new QLBDContext();
        NhanVienDAO nhanVienDAO = new NhanVienDAO();
        private string AddOrEdit = null;
        public frmNhanVien()
        {
            InitializeComponent();

        }
        public void ResetText1()
        {
            txtMaNhanVien.Clear();
            txtTenNhanVien.Clear();
            txtDiaChi.Clear();
            mskNgaySinh.Clear();
            mtbDienThoai.ResetText();
            rbNam.Checked = false;
            rbNu.Checked = false;

        }
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnXoaNv.Enabled = false;
            ShowAndHidden(false);
            loadNhanVien();

        }
        private void loadNhanVien()
        {
            dgvDanhSachNv.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.TenNV, p.DiaChi, p.GioiTinh, p.SDT, p.NgaySinh }).ToList();
        }

        private void ShowAndHidden(bool show)
        {
            txtMaNhanVien.Enabled = true;
            txtTenNhanVien.Enabled = true;
        }
        private void dgvNhanVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {


        }
        private void btnThemNv_Click(object sender, EventArgs e)
        {
            ShowAndHidden(true);
            AddOrEdit = "Add";
            btnLuu.Enabled = true;
            ResetText1();
        }
        public bool checkMaNV(string manv)
        {
            if (dgvDanhSachNv.Rows.Count == 0)
            {
                return true;
            }
            for (int row = 0; row < dgvDanhSachNv.Rows.Count - 1; row++)
            {
                if (dgvDanhSachNv.Rows[row].Cells["manv"].Value.ToString() == manv)
                {
                    return false;
                }
            }
            return true;
        }

        private void dgvDanhSachNv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNhanVien.Enabled = false;
            btnXoaNv.Enabled = true;
            btnSuaNv.Enabled = true;
            btnLuu.Enabled = false;
            rowindex = e.RowIndex;
            rowindex = e.RowIndex;
            txtMaNhanVien.Text = dgvDanhSachNv.Rows[rowindex].Cells["manv"].Value.ToString();
            txtTenNhanVien.Text = dgvDanhSachNv.Rows[rowindex].Cells["tennv"].Value.ToString();
            txtDiaChi.Text = dgvDanhSachNv.Rows[rowindex].Cells["diachi"].Value.ToString();
            string gioiTinh = dgvDanhSachNv.Rows[rowindex].Cells["gioitinh"].Value.ToString();
            if (gioiTinh == "Nam")
            {
                rbNam.Checked = true;
                rbNu.Checked = false;
            }
            else if (gioiTinh == "Nữ")
            {
                rbNam.Checked = false;
                rbNu.Checked = true;
            }
            mtbDienThoai.Text = dgvDanhSachNv.Rows[rowindex].Cells["sodienthoai"].Value.ToString();
            mskNgaySinh.Text = dgvDanhSachNv.Rows[rowindex].Cells["ngaysinh"].Value.ToString();
        }

        private void btnSuaNv_Click(object sender, EventArgs e)
        {
            txtMaNhanVien.Enabled = false;
            btnLuu.Enabled = true;
            AddOrEdit = "Edit";

        }

        private void btnXoaNv_Click(object sender, EventArgs e)
        {
            btnXoaNv.Enabled = false;
            int maNV = int.Parse(txtMaNhanVien.Text.Trim());
            NhanVien nv = nhanVienDAO.getRow(maNV);
            nhanVienDAO.Delete(nv);
            loadNhanVien();
            ResetText1();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            txtDiaChi.Text = "";
            mskNgaySinh.Text = "";
            mtbDienThoai.Text = "";
            rbNam.Checked = false;
            rbNu.Checked = false;

        }

        private void rbNam_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNam.Checked)
            {
                // Nếu checkbox "Nam" được chọn, loại bỏ lựa chọn ở checkbox "Nữ"
                rbNu.Checked = false;
            }
        }

        private void rbNu_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNu.Checked)
            {
                // Nếu checkbox "Nữ" được chọn, loại bỏ lựa chọn ở checkbox "Nam"
                rbNam.Checked = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
                {
                    MessageBox.Show("Vui lòng điền Mã nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTenNhanVien.Text))
                {
                    MessageBox.Show("Vui lòng điền Tên nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
                {
                    MessageBox.Show("Vui lòng điền Địa chỉ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!rbNam.Checked && !rbNu.Checked)
                {
                    MessageBox.Show("Vui lòng chọn Giới tính.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(mtbDienThoai.Text))
                {
                    MessageBox.Show("Vui lòng điền Số điện thoại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(mskNgaySinh.Text))
                {
                    MessageBox.Show("Vui lòng điền Ngày sinh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (AddOrEdit == "Add")
                {
                    //Luu vào CSDL
                    NhanVienDAO nhanVienDAO = new NhanVienDAO();
                    NhanVien nv = new NhanVien();
                    nv.MaNV = int.Parse(txtMaNhanVien.Text.Trim());
                    nv.TenNV = txtTenNhanVien.Text.Trim();
                    nv.DiaChi = txtDiaChi.Text.Trim();
                    nv.GioiTinh = rbNam.Checked ? "Nam" : "Nữ";
                    nv.SDT = mtbDienThoai.Text.Trim();

                    nv.NgaySinh = DateTime.Parse(mskNgaySinh.Text);

                    nhanVienDAO.Insert(nv);
                    db.SaveChanges();
                    loadNhanVien();
                }
                if (AddOrEdit == "Edit")
                {
                    //Update
                    int maSP = int.Parse(txtMaNhanVien.Text.Trim());
                    NhanVien nv = nhanVienDAO.getRow(maSP);
                    nv.MaNV = int.Parse(txtMaNhanVien.Text.Trim());

                    nv.TenNV = txtTenNhanVien.Text.Trim();
                    nv.DiaChi = txtDiaChi.Text.Trim();
                    nv.GioiTinh = rbNam.Checked ? "Nam" : "Nữ";
                    nv.SDT = mtbDienThoai.Text.Trim();
                    nv.NgaySinh = DateTime.Parse(mskNgaySinh.Text);

                    nhanVienDAO.Update(nv);
                    dgvDanhSachNv.DataSource = nhanVienDAO.getList();
                }
                txtMaNhanVien.Text = "";
                txtTenNhanVien.Text = "";
                txtDiaChi.Text = "";
                mskNgaySinh.Text = "";
                mtbDienThoai.Text = "";
                rbNam.Checked = false;
                rbNu.Checked = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string input = txtTimKiem.Text;
            string TenNV = txtTenNhanVien.Text;
            if (int.TryParse(input, out int number))
            {
                NhanVienDAO nhanVienDAO = new NhanVienDAO();
                List<NhanVien> foundProducts = nhanVienDAO.TimKiemNhanVien(number, string.Empty);
                if (foundProducts.Count > 0)
                {
                    dgvDanhSachNv.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên .", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                NhanVienDAO nhanVienDAO = new NhanVienDAO();
                List<NhanVien> foundProducts = nhanVienDAO.TimKiemNhanVien(0, input);

                if (foundProducts.Count > 0)
                {
                    dgvDanhSachNv.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên với tên đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

       

        private void btnAll_Click(object sender, EventArgs e)
        {
            loadNhanVien();
        }
    }
}
