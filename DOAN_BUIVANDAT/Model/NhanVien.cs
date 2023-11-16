namespace DOAN_BUIVANDAT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNV { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNV { get; set; }

        [Required]
        [StringLength(50)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(50)]
        public string GioiTinh { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }
    }
}
