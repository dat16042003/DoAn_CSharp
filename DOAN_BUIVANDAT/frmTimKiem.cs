using DOAN_BUIVANDAT.DAO;
using DOAN_BUIVANDAT.Model;
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

namespace DOAN_BUIVANDAT
{
    public partial class frmTimKiem : Form
    {
       
        QLBDContext db = new QLBDContext();
        SanPhamDAO sanPhamDAO = new SanPhamDAO();
     
        public frmTimKiem()
        {
            InitializeComponent();
        }

        private void frmTimKiem_Load(object sender, EventArgs e)
        {
            loadSanPham();
        }
        private void loadSanPham()
        {
            dgvDanhSachTimSP.DataSource = db.SanPhams.Select(p => new { p.MaSP, p.TenSP, p.MaLoaiHang, p.DonGiaNhap, p.DonGiaBan, p.SoLuong, p.MoTaSP, p.Anh }).ToList();
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
            string input = txtTimMaSP.Text;
           
            if (int.TryParse(input, out int number))
            {
                SanPhamDAO sanPhamDAO = new SanPhamDAO();
                List<SanPham> foundProducts = sanPhamDAO.TimKiemSanPham(number, string.Empty);
                if (foundProducts.Count > 0)
                {
                    dgvDanhSachTimSP.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm .", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                SanPhamDAO sanPhamDAO = new SanPhamDAO();
                List<SanPham> foundProducts = sanPhamDAO.TimKiemSanPham(0, input);

                if (foundProducts.Count > 0)
                {
                    dgvDanhSachTimSP.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm với tên đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void dgvDanhSachTimSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txtTimMaSP_TextChanged(object sender, EventArgs e)
        {
         
        }

       
    }
}
