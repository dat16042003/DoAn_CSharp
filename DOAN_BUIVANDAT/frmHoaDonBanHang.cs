using DOAN_BUIVANDAT.DAO;
using DOAN_BUIVANDAT.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_BUIVANDAT
{
    public partial class frmHoaDonBanHang : Form
    {
        int rowindex = -1;
        QLBDContext db = new QLBDContext();
        HoaDonDAO hoaDonDAO = new HoaDonDAO();
        private string AddOrEdit = null;
        public frmHoaDonBanHang()
        {
            InitializeComponent();
        }
        public void ResetText()
        {
            txtSoLuong.Text = "";
            txtDiaChi.Text = "";
            txtDonGia.Text = "";
            txtMaHD.Text = "";
            txtMaKH.Text = ""; ;
            cbTenKH.Text = "";
            txtTimKiem.Text = "";
            txtTongTien.Text = "";
            cbTenNV.Text = "";
            cbTenSP.Text = "";
            mkDienThoai.Text = "";
            mkNgayBan.Text = "";
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            int maHD = int.Parse(txtMaHD.Text.Trim());
            HoaDon hd = hoaDonDAO.getRow(maHD);
            hoaDonDAO.Delete(hd);
            loadHoaDon();
            ResetText();
        }
        private void loadHoaDon()
        {
            dgvDanhSachHD.DataSource = db.HoaDons.Select(p => new { p.MaHD, p.TenSP, p.NgayLapHD, p.TenNV, p.DonGiaBan, p.SoLuongSP, p.MaKH, p.TenKH, p.DiaChi, p.SDT, p.TongTien }).ToList();
        }
        private void loadSanPham()
        {
            cbTenSP.DataSource = db.SanPhams.ToList();
            cbTenSP.ValueMember = "TenSP";
            cbTenSP.DisplayMember = "TenSP"; 
            var sanPhams = db.SanPhams.ToList();
            cbTenSP.DataSource = sanPhams;

        }
        private void loadKhachHang()
        {
            cbTenKH.DataSource = db.KhachHangs.ToList();
            cbTenKH.ValueMember = "MaKH";
            cbTenKH.DisplayMember = "TenKH";
        }
        private void loadNhanVien()
        {
            cbTenNV.DataSource = db.NhanViens.ToList();
            cbTenNV.ValueMember = "TenNV";

        }

     
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaKH.Enabled = true;
            txtMaHD.Enabled = true;
            AddOrEdit = "Add";
            btnLuu.Enabled = true;
            ResetText();
            // Gán ngày bán là ngày hiện tại
            // mkNgayBan.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void frmHoaDonBanHang_Load(object sender, EventArgs e)
        {
            cbTenSP.SelectedIndexChanged += cbTenSP_SelectedIndexChanged;
            txtSoLuong.TextChanged += txtSoLuong_TextChanged;
            txtDonGia.TextChanged += txtDonGia_TextChanged;
            cbTenKH.SelectedIndexChanged += cbTenKH_SelectedIndexChanged;
            dgvDanhSachHD.DataSource = hoaDonDAO.getList();
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;          
            loadSanPham();
            loadHoaDon();
            loadNhanVien();
            loadKhachHang();
        }

        private void dgvDanhSachHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaHD.Enabled = false;
                txtMaKH.Enabled = false;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
                rowindex = e.RowIndex;
                
                if (e.RowIndex >= 0 && e.RowIndex < dgvDanhSachHD.Rows.Count)
                {
                    int index = e.RowIndex;
                    txtMaHD.Text = dgvDanhSachHD.Rows[rowindex].Cells["MaHD"].Value.ToString();
                    mkNgayBan.Text = dgvDanhSachHD.Rows[rowindex].Cells["NgayLapHD"].Value.ToString();
                    cbTenNV.Text = dgvDanhSachHD.Rows[rowindex].Cells["TenNV"].Value.ToString();
                    txtMaKH.Text = dgvDanhSachHD.Rows[rowindex].Cells["MaKH"].Value.ToString();
                    cbTenKH.Text = dgvDanhSachHD.Rows[rowindex].Cells["TenKH"].Value.ToString();
                    txtDiaChi.Text = dgvDanhSachHD.Rows[rowindex].Cells["DiaChi"].Value.ToString();
                    mkDienThoai.Text = dgvDanhSachHD.Rows[rowindex].Cells["SDT"].Value.ToString();
                    cbTenSP.Text = dgvDanhSachHD.Rows[rowindex].Cells["TenSP"].Value.ToString();
                    txtDonGia.Text = dgvDanhSachHD.Rows[rowindex].Cells["DonGiaBan"].Value.ToString();
                    txtSoLuong.Text = dgvDanhSachHD.Rows[rowindex].Cells["SoLuongSP"].Value.ToString();
                    txtTongTien.Text = dgvDanhSachHD.Rows[rowindex].Cells["TongTien"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            try
            {
                int gia, soluong;

                if (txtMaHD.Text.Length.Equals(0))
                {
                    throw new Exception("Mã hóa đơn không được để trống");
                }
                if (mkNgayBan.Text.Length.Equals(0))
                {
                    throw new Exception("Ngày bán không được để trống");
                }

                if (txtDonGia.Text.Length.Equals(0))
                {
                    throw new Exception("Giá bán không được để trống");
                }
                if (!int.TryParse(txtDonGia.Text.Trim(), out gia))
                {
                    throw new Exception("Giá bán không phải số");
                }

                if (!int.TryParse(txtSoLuong.Text.Trim(), out soluong))
                {
                    throw new Exception("Số lượng không phải số");
                }
                if (cbTenSP.Text.Length.Equals(0))
                {
                    throw new Exception("Chon sản phẩm không được để trống");
                }
                if (txtMaKH.Text.Length.Equals(0))
                {
                    throw new Exception("Mã khách hàng  không được để trống");
                }
                /* // Kiểm tra mã khách hàng đã tồn tại trong CSDL hay chưa
                 int maKH = int.Parse(txtMaKH.Text.Trim());
                 KhachHang existingKhachHang = db.KhachHangs.FirstOrDefault(kh => kh.MaKH == maKH);

                 // Nếu mã khách hàng chưa tồn tại, thêm mới vào CSDL
                 if (existingKhachHang == null)
                 {
                     KhachHang khachHangMoi = new KhachHang
                     {
                         MaKH = maKH,
                         //Thêm thông tin khách hàng từ các controls khác vào đây
                     };
                     db.KhachHangs.Add(khachHangMoi);
                     db.SaveChanges();
                 }*/
                if (AddOrEdit == "Add")
                {
                    //Luu vào CSDL
                    HoaDonDAO hoaDonDAO = new HoaDonDAO();
                    HoaDon hd = new HoaDon();
                    hd.MaHD = int.Parse(txtMaHD.Text.Trim());
                    hd.NgayLapHD = DateTime.Parse(mkNgayBan.Text);                 
                    hd.TenNV = cbTenNV.Text.Trim();
                    hd.MaKH = int.Parse(txtMaKH.Text.Trim());
                    hd.TenKH = cbTenKH.Text.Trim();
                    hd.SDT = mkDienThoai.Text.Trim();
                    hd.DiaChi = txtDiaChi.Text.Trim();
                    hd.TenSP = cbTenSP.Text.Trim();
                    hd.DonGiaBan = Convert.ToInt32(txtDonGia.Text);
                    hd.SoLuongSP = Convert.ToInt32(txtSoLuong.Text);
                    hd.TongTien = Convert.ToInt32(txtTongTien.Text);
                    hoaDonDAO.Insert(hd);
                    db.SaveChanges();
                    loadHoaDon();                    
                }
                if (AddOrEdit == "Edit")
                {
                    //Update
                    int maHD = int.Parse(txtMaHD.Text.Trim());
                    HoaDon hd = hoaDonDAO.getRow(maHD);
                    hd.MaHD = int.Parse(txtMaHD.Text.Trim());
                    hd.NgayLapHD = DateTime.Parse(mkNgayBan.Text);                   
                    hd.TenNV = cbTenNV.Text.Trim();
                    hd.MaKH = int.Parse(txtMaKH.Text.Trim());
                    hd.TenKH = cbTenKH.Text.Trim();
                    hd.SDT = mkDienThoai.Text.Trim();
                    hd.TenSP = cbTenSP.Text.Trim();
                    hd.DiaChi = txtDiaChi.Text.Trim();
                    hd.DonGiaBan = Convert.ToInt32(txtDonGia.Text);
                    hd.SoLuongSP = Convert.ToInt32(txtSoLuong.Text);
                    hd.TongTien = Convert.ToInt32(txtTongTien.Text);
                    hoaDonDAO.Update(hd);
                    dgvDanhSachHD.DataSource = hoaDonDAO.getList();
                }
                txtSoLuong.Text = "";
                txtDiaChi.Text = "";
                txtDonGia.Text = "";
                txtMaHD.Text = "";
                txtMaKH.Text = "";
                txtDiaChi.Text = "";
                mkDienThoai.Text = ""; 
                cbTenKH.Text = "";
                txtTimKiem.Text = "";
                txtTongTien.Text = "";
                cbTenNV.Text = "";
                cbTenSP.Text = "";            
                mkNgayBan.Text = "";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMaHD.Enabled = false;
            txtMaKH.Enabled = false;
            btnLuu.Enabled = true;
            AddOrEdit = "Edit";
            // Gán ngày bán là ngày hiện tại
           // mkNgayBan.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void TinhTongTien()
        {
            int soLuong, donGia;

            // Kiểm tra xem có thể chuyển đổi sang số nguyên không
            if (int.TryParse(txtSoLuong.Text.Trim(), out soLuong) && int.TryParse(txtDonGia.Text.Trim(), out donGia))
            {
                // Tính toán tổng tiền và cập nhật vào txtTongTien
                int tongTien = soLuong * donGia;
                txtTongTien.Text = tongTien.ToString();
            }
            else
            {
                // Xử lý trường hợp không thể chuyển đổi sang số nguyên
                txtTongTien.Text = "";
            }
        }
        private void cbTenSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xử lý sự kiện SelectedIndexChanged
            string tenSanPhamDuocChon = cbTenSP.Text;

            // Lấy sản phẩm được chọn từ cơ sở dữ liệu
            SanPham sanPhamDuocChon = db.SanPhams.FirstOrDefault(p => p.TenSP == tenSanPhamDuocChon);

            // Cập nhật textbox giá bán
            if (sanPhamDuocChon != null)
            {
                txtDonGia.Text = sanPhamDuocChon.DonGiaBan.ToString();
            }
            else
            {
                // Xử lý trường hợp khi không tìm thấy sản phẩm được chọn
                txtDonGia.Text = "";
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

       

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        private void cbTenKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbTenKH.Text = "";
            if (cbTenKH.SelectedItem != null)
            {            
                // Lấy đối tượng KhachHang từ ComboBox
                KhachHang selectedKhachHang = (KhachHang)cbTenKH.SelectedItem;
                // Hiển thị thông tin vào các ô thông tin tương ứng
                txtMaKH.Text = selectedKhachHang.MaKH.ToString();
                mkDienThoai.Text = selectedKhachHang.SDT;
                txtDiaChi.Text = selectedKhachHang.DiaChi;
            }
           /* else
            {
                // Nếu không tìm thấy thông tin khách hàng, xóa dữ liệu trong các ô thông tin
               
                mkDienThoai.Text = "";
                txtMaKH.Text = "";
                txtDiaChi.Text = "";
            }*/
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string input = txtTimKiem.Text;
           // string TenKH = txtTenKH.Text;
            if (int.TryParse(input, out int number))
            {
                HoaDonDAO hoaDonDAO = new HoaDonDAO();
                List<HoaDon> foundProducts = hoaDonDAO.TimKiemHoaDon(number, string.Empty);
                if (foundProducts.Count > 0)
                {
                    dgvDanhSachHD.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                HoaDonDAO hoaDonDAO = new HoaDonDAO();
                List<HoaDon> foundProducts = hoaDonDAO.TimKiemHoaDon(0, input);

                if (foundProducts.Count > 0)
                {
                    dgvDanhSachHD.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn với mã đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnAll_Click_1(object sender, EventArgs e)
        {
            loadHoaDon();
        }
    }
}
