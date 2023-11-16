using DOAN_BUIVANDAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_BUIVANDAT.DAO
{
    internal class LoaiHangDAO
    {
        QLBDContext db = null;
        public LoaiHangDAO()
        {
            db = new QLBDContext();
        }
        public List<LoaiHang> getList()
        {
            return db.LoaiHangs.ToList();
        }
        public LoaiHang getRow(int MaLoai)
        {
            return db.LoaiHangs.Find(MaLoai);
        }
        public int getCount()
        {
            return db.LoaiHangs.Count();
        }
        public void Insert(LoaiHang thongTinLoai)
        {
            db.LoaiHangs.Add(thongTinLoai);
            db.SaveChanges();
        }
        public void Update(LoaiHang thongTinLoai)
        {
            db.Entry(thongTinLoai).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(LoaiHang thongTinLoai)
        {
            db.LoaiHangs.Remove(thongTinLoai);
            db.SaveChanges();
        }
        public void Delete(int MaLoai)
        {
            LoaiHang thongTinLoai = db.LoaiHangs.Find(MaLoai);
            db.LoaiHangs.Remove(thongTinLoai);
            db.SaveChanges();
        }
       
        public List<LoaiHang> TimKiemLoaiHang(int id, string name)
        {
            using (var context = new QLBDContext())
            {
                var query = context.LoaiHangs.AsQueryable();

                if (id != 0)
                {
                    query = query.Where(p => p.MaLoaiHang == id);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(p => p.TenLoaiHang.Contains(name));
                }


                return query.ToList();
            }
        }
    }
}
