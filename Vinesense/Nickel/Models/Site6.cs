namespace Nickel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("viticulture.site_6")]
    public partial class Site6 : ISensorRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        [Column("Date_Time")]
        public string DateTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }
        [Column("S1_Temp")]
        public float? S1Temp { get; set; }
        [Column("S1_Moist")]
        public float? S1Moist { get; set; }
        [Column("S2_Temp")]
        public float? S2Temp { get; set; }
        [Column("S2_Moist")]
        public float? S2Moist { get; set; }
        [Column("S3_Temp")]
        public float? S3Temp { get; set; }
        [Column("S3_Moist")]
        public float? S3Moist { get; set; }
        [Column("S4_Temp")]
        public float? S4Temp { get; set; }
        [Column("S4_Moist")]
        public float? S4Moist { get; set; }
        [Column("S5_Temp")]
        public float? S5Temp { get; set; }
        [Column("S5_Moist")]
        public float? S5Moist { get; set; }

        public IEnumerable<float?> Temperatues
        {
            get { return new[] { S1Temp, S2Temp, S3Temp, S4Temp, S5Temp }; }
        }

        public IEnumerable<float?> Moistures
        {
            get { return new[] { S1Moist, S2Moist, S3Moist, S4Moist, S5Moist }; }
        }
    }
}
