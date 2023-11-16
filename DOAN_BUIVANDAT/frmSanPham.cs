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
    public partial class frmSanPham : Form
    {
        int rowindex = -1;
        QLBDContext db = new QLBDContext();
        SanPhamDAO sanPhamDAO = new SanPhamDAO();
        private string AddOrEdit = null;
        private byte[] selectedImageBytes;

        public frmSanPham()
        {
            InitializeComponent();

        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {

            btnLuu.Enabled = false;
            btnXoaSp.Enabled = false;
            ShowAndHidden(false);
            loadSanPham();
            loadLoaiSP();
        }
        private void loadSanPham()
        {
            dgvDanhSachSp.DataSource = db.SanPhams.Select(p => new { p.MaSP, p.TenSP, p.MaLoaiHang, p.DonGiaNhap, p.DonGiaBan, p.SoLuong, p.MoTaSP, p.Anh }).ToList();
        }
        private void loadLoaiSP()
        {
            cbLoaiSanPham.DataSource = db.LoaiHangs.ToList();
            cbLoaiSanPham.ValueMember = "MaLoaiHang";
            cbLoaiSanPham.DisplayMember = "TenLoaiHang";
        }
        private void ShowAndHidden(bool show)
        {

            txtMaSanPham.Enabled = true;
            txtTenSanPham.Enabled = true;
        }
        private void btnThemSp_Click(object sender, EventArgs e)
        {

            ShowAndHidden(true);
            AddOrEdit = "Add";
            btnLuu.Enabled = true;
            ResetText1();

        }
        private void btnSuaSp_Click(object sender, EventArgs e)
        {
            txtMaSanPham.Enabled = false;
            btnLuu.Enabled = true;
            AddOrEdit = "Edit";
        }


        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pbAnh.SizeMode = PictureBoxSizeMode.StretchImage;
                    string imagePath = openFileDialog.FileName;
                    pbAnh.Image = Image.FromFile(imagePath);
                    selectedImageBytes = File.ReadAllBytes(imagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        public bool checkMaSP(string masanpham)
        {
            if (dgvDanhSachSp.Rows.Count == 0)
            {
                return true;
            }
            for (int row = 0; row < dgvDanhSachSp.Rows.Count - 1; row++)
            {
                if (dgvDanhSachSp.Rows[row].Cells["masanpham"].Value.ToString() == masanpham)
                {
                    return false;
                }
            }
            return true;
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                int gia, soluong;
                /* if (!checkMaSP(txtMaSanPham.Text))
                 {
                     MessageBox.Show("Mã sản phẩm đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }*/
                if (txtMaSanPham.Text.Length.Equals(0))
                {
                    throw new Exception("Mã sản phẩm không được để trống");
                }
                if (txtTenSanPham.Text.Length.Equals(0))
                {
                    throw new Exception("Tên sản phẩm không được để trống");
                }
                if (txtDonGiaNhap.Text.Length.Equals(0))
                {
                    throw new Exception("Giá nhập không được để trống");
                }
                if (txtDonGiaBan.Text.Length.Equals(0))
                {
                    throw new Exception("Giá bán không được để trống");
                }
                if (!int.TryParse(txtDonGiaBan.Text.Trim(), out gia))
                {
                    throw new Exception("Giá bán không phải số");
                }
                if (!int.TryParse(txtDonGiaNhap.Text.Trim(), out gia))
                {
                    throw new Exception("Giá nhập không phải số");
                }
                if (!int.TryParse(txtSoLuong.Text.Trim(), out soluong))
                {
                    throw new Exception("Số lượng không phải số");
                }
                if (cbLoaiSanPham.Text.Length.Equals(0))
                {
                    throw new Exception("Chon loại sản phẩm không được để trống");
                }
                if (txtMoTaSP.Text.Length.Equals(0))
                {
                    throw new Exception("Mô tả sp không được để trống");
                }

                if (AddOrEdit == "Add")
                {
                    //Luu vào CSDL
                    SanPhamDAO sanPhamDAO = new SanPhamDAO();
                    SanPham sp = new SanPham();
                    sp.MaSP = int.Parse(txtMaSanPham.Text.Trim());
                    sp.TenSP = txtTenSanPham.Text.Trim();
                    sp.MaLoaiHang = cbLoaiSanPham.Text.Trim();

                    sp.DonGiaBan = Convert.ToInt32(txtDonGiaBan.Text);
                    sp.DonGiaNhap = Convert.ToInt32(txtDonGiaNhap.Text);
                    sp.SoLuong = Convert.ToInt32(txtSoLuong.Text);
                    sp.MoTaSP = txtMoTaSP.Text.Trim();
                    sp.Anh = selectedImageBytes;

                    sanPhamDAO.Insert(sp);
                    db.SaveChanges();
                    loadSanPham();

                }
                if (AddOrEdit == "Edit")
                {
                    //Update
                    int maSP = int.Parse(txtMaSanPham.Text.Trim());
                    SanPham sp = sanPhamDAO.getRow(maSP);
                    sp.MaSP = int.Parse(txtMaSanPham.Text.Trim());
                    sp.TenSP = txtTenSanPham.Text.Trim();
                    sp.MaLoaiHang = cbLoaiSanPham.Text.Trim();
                    sp.DonGiaNhap = Convert.ToInt32(txtDonGiaNhap.Text);
                    sp.DonGiaBan = Convert.ToInt32(txtDonGiaBan.Text);

                    sp.SoLuong = Convert.ToInt32(txtSoLuong.Text);
                    sp.MoTaSP = txtMoTaSP.Text.Trim();
                    // sp.Anh = selectedImageBytes;
                    byte[] currentImage = sp.Anh;
                    if (selectedImageBytes != null)
                    {
                        sp.Anh = selectedImageBytes; // Cập nhật thông tin ảnh mới
                    }
                    else
                    {
                        sp.Anh = currentImage; // Giữ nguyên thông tin ảnh cũ
                    }
                    sanPhamDAO.Update(sp);
                    dgvDanhSachSp.DataSource = sanPhamDAO.getList();
                }
                txtSoLuong.Text = "";
                txtTenSanPham.Text = "";
                txtMaSanPham.Text = "";
                txtDonGiaNhap.Text = "";
                txtDonGiaBan.Text = ""; ;
                txtMoTaSP.Text = "";
                cbLoaiSanPham.Text = "";
                pbAnh.Image = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void dgvDanhSachSp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSanPham.Enabled = false;
            btnXoaSp.Enabled = true;
            btnSuaSp.Enabled = true;
            btnLuu.Enabled = false;
            rowindex = e.RowIndex;

            if (e.RowIndex >= 0 && e.RowIndex < dgvDanhSachSp.Rows.Count)
            {
                int index = e.RowIndex;
                txtMaSanPham.Text = dgvDanhSachSp.Rows[rowindex].Cells["MaSP"].Value.ToString();
                txtTenSanPham.Text = dgvDanhSachSp.Rows[rowindex].Cells["TenSP"].Value.ToString();
                cbLoaiSanPham.Text = dgvDanhSachSp.Rows[rowindex].Cells["MaLoaiHang"].Value.ToString();
                txtDonGiaNhap.Text = dgvDanhSachSp.Rows[rowindex].Cells["DonGiaNhap"].Value.ToString();
                txtDonGiaBan.Text = dgvDanhSachSp.Rows[rowindex].Cells["DonGiaBan"].Value.ToString();
                txtSoLuong.Text = dgvDanhSachSp.Rows[rowindex].Cells["SoLuong"].Value.ToString();
                txtMoTaSP.Text = dgvDanhSachSp.Rows[rowindex].Cells["MoTaSP"].Value.ToString();

                if (dgvDanhSachSp.Rows[rowindex].Cells["Anh"].Value != null)
                {
                    byte[] imageData = (byte[])dgvDanhSachSp.Rows[rowindex].Cells["Anh"].Value;
                    if (imageData != null && imageData.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            pbAnh.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbAnh.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pbAnh.Image = null; // Nếu không có hình ảnh, PictureBox sẽ hiển thị rỗng.
                    }
                }
                else
                {
                    pbAnh.Image = null; // Nếu không có hình ảnh, PictureBox sẽ hiển thị rỗng.
                }

            }


        }
        private void btnXoaSp_Click(object sender, EventArgs e)
        {
            btnXoaSp.Enabled = false;
            int maSP = int.Parse(txtMaSanPham.Text.Trim());
            SanPham sp = sanPhamDAO.getRow(maSP);
            sanPhamDAO.Delete(sp);
            loadSanPham();
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

        public void ResetText1()
        {

            txtSoLuong.Clear();
            txtTenSanPham.Clear();
            txtMaSanPham.Clear();
            txtDonGiaNhap.Clear();
            txtDonGiaBan.Clear();
            txtMoTaSP.Clear();
            cbLoaiSanPham.ResetText();
            pbAnh.Image = null;
        }
        private void cbLoaiSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            // cbLoaiSanPham.Text = "";
        }
        private void pbAnh_Click(object sender, EventArgs e)
        {

        }

        private void txtDonGiaNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDonGiaBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cbLoaiSanPham.Text))
                {
                    throw new Exception("Vui lòng chọn tên loại sản phẩm cần lọc");
                }

                string loaiSanPham = cbLoaiSanPham.Text.Trim();

               /* if (string.IsNullOrEmpty(loaiSanPham))
                {
                    throw new Exception("Vui lòng chọn loại sản phẩm cần lọc");
                }*/

                dgvDanhSachSp.DataSource = sanPhamDAO.LocTheLoaiSP(loaiSanPham);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            loadSanPham();
        }
    }
}

