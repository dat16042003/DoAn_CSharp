using DOAN_BUIVANDAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_BUIVANDAT.DAO
{
    internal class HoaDonDAO
    {
        QLBDContext db = null;
        public HoaDonDAO()
        {
            db = new QLBDContext();
        }
        public List<HoaDon> getList()
        {
            return db.HoaDons.ToList();
        }
        public HoaDon getRow(int MaHD)
        {
            return db.HoaDons.Find(MaHD);
        }
        public int getCount()
        {
            return db.HoaDons.Count();
        }
        public void Insert(HoaDon thongTinHD)
        {
            db.HoaDons.Add(thongTinHD);
            db.SaveChanges();
        }
        public void Update(HoaDon thongTinHD)
        {
            db.Entry(thongTinHD).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(HoaDon thongTinHD)
        {
            db.HoaDons.Remove(thongTinHD);
            db.SaveChanges();
        }
        public void Delete(int MaKH)
        {
            HoaDon thongTinHD = db.HoaDons.Find(MaKH);
            db.HoaDons.Remove(thongTinHD);
            db.SaveChanges();
        }
        public List<HoaDon> TimKiemHoaDon(int id, string name)
        {
            using (var context = new QLBDContext())
            {

                var query = context.HoaDons.AsQueryable();

                if (id != 0)
                {
                    query = query.Where(p => p.MaHD == id);
                }

                /*if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(p => p.TenKH.Contains(name));
                }*/


                return query.ToList();
            }
        }
    }
}
