using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinesense.Legacy
{
    [Table("viticulture.site_6")]
    public class Site6 : ISiteRecord
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
    }
}
