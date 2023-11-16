namespace DOAN_BUIVANDAT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayLapHD { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNV { get; set; }

        public int MaKH { get; set; }

        [Required]
        [StringLength(50)]
        public string TenKH { get; set; }

        [Required]
        [StringLength(50)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(50)]
        public string SDT { get; set; }

        [Required]
        [StringLength(50)]
        public string TenSP { get; set; }

        public int DonGiaBan { get; set; }

        public int SoLuongSP { get; set; }

        public int TongTien { get; set; }
    }
}
