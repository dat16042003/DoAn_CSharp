namespace DOAN_BUIVANDAT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        [Required]
        [StringLength(50)]
        public string TenSP { get; set; }

        [Required]
        [StringLength(50)]
        public string MaLoaiHang { get; set; }

        public int SoLuong { get; set; }

        public int DonGiaNhap { get; set; }

        public int DonGiaBan { get; set; }

        [StringLength(200)]
        public string MoTaSP { get; set; }

        [Column(TypeName = "image")]
        public byte[] Anh { get; set; }
    }
}
