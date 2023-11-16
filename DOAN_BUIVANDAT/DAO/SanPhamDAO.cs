using DOAN_BUIVANDAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_BUIVANDAT.DAO
{
    internal class SanPhamDAO
    {
   
        QLBDContext db = null;
        public SanPhamDAO()
        {
            db = new QLBDContext();
        }
        public List<SanPham> getList()
        {
            return db.SanPhams.ToList();
        }
        public SanPham getRow(int MaSP)
        {
            return db.SanPhams.Find(MaSP);
        }
        public int getCount()
        {
            return db.SanPhams.Count();
        }
        public void Insert(SanPham sanpham)
        {
            db.SanPhams.Add(sanpham);
            db.SaveChanges();
        }
        public void Update(SanPham sanpham)
        {
            db.Entry(sanpham).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(SanPham sanpham)
        {
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
        }
        public void Delete(int MaSP)
        {
            SanPham sanpham = db.SanPhams.Find(MaSP);
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
        }
        public List<SanPham> LocTheLoaiSP(string MaLoaiHang)
        {
            var LocSP = getList().Where(e => e.MaLoaiHang == MaLoaiHang).ToList();
            return LocSP;
        }
        public List<SanPham> TimKiemSanPham(int id, string name)
        {
            using (var context = new QLBDContext())
            {
                // Thực hiện tìm kiếm sản phẩm dựa trên mã sản phẩm và tên sản phẩm
                var query = context.SanPhams.AsQueryable();

                if (id != 0)
                {
                    query = query.Where(p => p.MaSP == id);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(p => p.TenSP.Contains(name));
                }


                return query.ToList();
            }
        }
    }
}
