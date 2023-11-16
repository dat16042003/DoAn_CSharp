using DOAN_BUIVANDAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_BUIVANDAT.DAO
{
    internal class NhanVienDAO
    {
        QLBDContext db = null;
        public NhanVienDAO()
        {
            db = new QLBDContext();
        }
        public List<NhanVien> getList()
        {
            return db.NhanViens.ToList();
        }
        public NhanVien getRow(int MaNV)
        {
            return db.NhanViens.Find(MaNV);
        }
        public int getCount()
        {
            return db.NhanViens.Count();
        }
        public void Insert(NhanVien nhanvien)
        {
            db.NhanViens.Add(nhanvien);
            db.SaveChanges();
        }
        public void Update(NhanVien nhanvien)
        {
            db.Entry(nhanvien).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(NhanVien nhanvien)
        {
            db.NhanViens.Remove(nhanvien);
            db.SaveChanges();
        }
        public void Delete(int MaNV)
        {
            NhanVien nhanvien = db.NhanViens.Find(MaNV);
            db.NhanViens.Remove(nhanvien);
            db.SaveChanges();
        }
        public List<NhanVien> LocTheoGioiTinh(string GioiTinh)
        {
            var LocGT = getList().Where(e => e.GioiTinh == GioiTinh).ToList();
            return LocGT;
        }
        public List<NhanVien> TimKiemNhanVien(int id, string name)
        {
            using (var context = new QLBDContext())
            {
                var query = context.NhanViens.AsQueryable();

                if (id != 0)
                {
                    query = query.Where(p => p.MaNV == id);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(p => p.TenNV.Contains(name));
                }


                return query.ToList();
            }
        }
    }
}
