using DOAN_BUIVANDAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_BUIVANDAT.DAO
{
    internal class NguoiDungDAO
    {
        QLBDContext db = null;
        public NguoiDungDAO()
        {
            db = new QLBDContext();
        }
        public NguoiDung getRow(string tendn, string matkhau)
        {
            return db.NguoiDungs.Where(m => m.TaiKhoan == tendn && m.MatKhau == matkhau).FirstOrDefault();

        }
    }
}
