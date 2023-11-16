using DOAN_BUIVANDAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_BUIVANDAT.DAO
{
    internal class KhachHangDAO
    {
        QLBDContext db = null;
        public KhachHangDAO()
        {
            db = new QLBDContext();
        }
        public List<KhachHang> getList()
        {
            return db.KhachHangs.ToList();
        }
        public KhachHang getRow(int MaKH)
        {
            return db.KhachHangs.Find(MaKH);
        }
        public int getCount()
        {
            return db.KhachHangs.Count();
        }
        public void Insert(KhachHang thongTinKH)
        {
            db.KhachHangs.Add(thongTinKH);
            db.SaveChanges();
        }
        public void Update(KhachHang thongTinKH)
        {
            db.Entry(thongTinKH).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(KhachHang thongTinKH)
        {
            db.KhachHangs.Remove(thongTinKH);
            db.SaveChanges();
        }
        public void Delete(int MaKH)
        {
            KhachHang thongTinKH = db.KhachHangs.Find(MaKH);
            db.KhachHangs.Remove(thongTinKH);
            db.SaveChanges();
        }
        public List<KhachHang> LocTheoDiaChi(string DiaChi)
        {
            var LocKH = getList().Where(e => e.DiaChi == DiaChi).ToList();
            return LocKH;
        }
        public List<KhachHang> TimKiemKhachHang(int id, string name)
        {
            using (var context = new QLBDContext())
            {

                var query = context.KhachHangs.AsQueryable();

                if (id != 0)
                {
                    query = query.Where(p => p.MaKH == id);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(p => p.TenKH.Contains(name));
                }


                return query.ToList();
            }
        }
    }
}
