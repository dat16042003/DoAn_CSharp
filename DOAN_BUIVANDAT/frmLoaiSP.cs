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
    public partial class frmLoaiSP : Form
    {
        int rowindex = -1;
        QLBDContext db = new QLBDContext();
        LoaiHangDAO loaihangDAO = new LoaiHangDAO();
        public frmLoaiSP()
        {
            InitializeComponent();
        }
        private string AddOrEdit = null;
        private void btnThem_Click(object sender, EventArgs e)
        {
            ShowAndHidden(true);
            AddOrEdit = "Add";
            btnLuu.Enabled = true;
            ResetText();
        }
        public void ResetText()
        {
            txtMaLoai.Clear();
            txtTenLoai.Clear();
 
        }
        private void ShowAndHidden(bool show)
        {
            txtMaLoai.Enabled = true;
            txtTenLoai.Enabled = true;
        }
        private void loadLoaiSP()
        {
            dvgLoaiSP.DataSource = db.LoaiHangs.Select(p => new { p.MaLoaiHang, p.TenLoaiHang }).ToList();
        }
        private void frmLoaiSP_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            ShowAndHidden(false);
            loadLoaiSP();
        }
        public bool checkMaLoaiSP(string masanpham)
        {
            if (dvgLoaiSP.Rows.Count == 0)
            {
                return true;
            }
            for (int row = 0; row < dvgLoaiSP.Rows.Count - 1; row++)
            {
                if (dvgLoaiSP.Rows[row].Cells["MaLoaiHang"].Value.ToString() == masanpham)
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
                //kt mã loại sp 
               /* if (!checkMaLoaiSP(txtMaLoai.Text))
                {
                    MessageBox.Show("Mã loại sp đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }*/
                if (txtTenLoai.Text.Length.Equals(0))
                {
                    throw new Exception("Tên loại  không được để trống");
                }
               
                if (AddOrEdit == "Add")
                {
                    //Luu vào CSDL
                    LoaiHangDAO laoihangDAO = new LoaiHangDAO();
                    LoaiHang lh = new LoaiHang();
                    lh.MaLoaiHang = int.Parse(txtMaLoai.Text.Trim());
                    lh.TenLoaiHang = txtTenLoai.Text.Trim();

                    loaihangDAO.Insert(lh);
                    db.SaveChanges();
                    loadLoaiSP();
                }
                if (AddOrEdit == "Edit")
                {
                    //Update
                    int malh = int.Parse(txtMaLoai.Text.Trim());
                    LoaiHang lh = loaihangDAO.getRow(malh);
                    lh.MaLoaiHang = int.Parse(txtMaLoai.Text.Trim());
                    lh.TenLoaiHang = txtTenLoai.Text.Trim();


                    loaihangDAO.Update(lh);
                    dvgLoaiSP.DataSource = loaihangDAO.getList();
                }
        
                txtTenLoai.Text = "";
                txtMaLoai.Text = "";
              


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMaLoai.Enabled = false;
            btnLuu.Enabled = true;
            AddOrEdit = "Edit";
           
        }

        private void dvgLoaiSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaLoai.Enabled = false;

            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            rowindex = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dvgLoaiSP.Rows.Count)
            {
                int index = e.RowIndex;
                txtMaLoai.Text = dvgLoaiSP.Rows[rowindex].Cells["MaLoaiHang"].Value.ToString();
                txtTenLoai.Text = dvgLoaiSP.Rows[rowindex].Cells["TenLoaiHang"].Value.ToString();           
            }
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

            txtMaLoai.Clear();
            txtTenLoai.Clear();

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            int maSP = int.Parse(txtMaLoai.Text.Trim());
            LoaiHang lh = loaihangDAO.getRow(maSP);
            loaihangDAO.Delete(lh);
            loadLoaiSP();
            ResetText1();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string input = txtTimKiem.Text;
            string TenNV = txtTenLoai.Text;
            if (int.TryParse(input, out int number))
            {
                LoaiHangDAO laoiHangDAO = new LoaiHangDAO();
                List<LoaiHang> foundProducts = laoiHangDAO.TimKiemLoaiHang(number, string.Empty);
                if (foundProducts.Count > 0)
                {
                    dvgLoaiSP.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại sản phẩm .", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                LoaiHangDAO nhanVienDAO = new LoaiHangDAO();
                List<LoaiHang> foundProducts = nhanVienDAO.TimKiemLoaiHang(0, input);

                if (foundProducts.Count > 0)
                {
                    dvgLoaiSP.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại sản phẩm với tên đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            loadLoaiSP();
        }
    }
}
