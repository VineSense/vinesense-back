namespace Nickel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("viticulture.aldo_station")]
    public partial class AldoStation : ISensorRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        [Column("Date_Time")]
        public string DateTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        [Column("P1_Humidity")]
        public float? P1Humidity { get; set; }
        [Column("P1_Temp")]
        public float? P1Temp { get; set; }
        [Column("P2_Temp")]
        public float? P2Temp { get; set; }
        [Column("P2_Water_Potential")]
        public float? P2WaterPotential { get; set; }
        [Column("P3_Temp")]
        public float? P3Temp { get; set; }
        [Column("P3_Moisture")]
        public float? P3Moisture { get; set; }
        [Column("P4_Temp")]
        public float? P4Temp { get; set; }
        [Column("P4_Moisture")]
        public float? P4Moisture { get; set; }
        [Column("P5_Temp")]
        public float? P5Temp { get; set; }
        [Column("P5_Moisture")]
        public float? P5Moisture { get; set; }

        public IEnumerable<float?> Temperatues
        {
            get { return new[] { P1Temp, P2Temp, P3Temp, P4Temp, P5Temp }; }
        }

        public IEnumerable<float?> Moistures
        {
            get { return new[] { P1Humidity, P2WaterPotential, P3Moisture, P4Moisture, P5Moisture }; }
        }
    }
}
