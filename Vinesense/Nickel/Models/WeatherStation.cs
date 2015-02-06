namespace Nickel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("viticulture.weather_station")]
    public partial class WeatherStation : IRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        [Column("Date_Time")]
        public string DateTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        [Column("Wind_Direction")]
        public int? WindDirection { get; set; }

        [Column("Wind_Gust")]
        public float? WindGust { get; set; }

        [Column("Wind_Speed")]
        public float? WindSpeed { get; set; }

        [Column("Solar_Radiation")]
        public float? SolarRadiation { get; set; }

        [Column("Relative_Humidity")]
        public float? RelativeHumidity { get; set; }

        public float? Temperature { get; set; }

        public float? Precipitation { get; set; }

        [Column("Leaf_wetness_counts")]
        public float? LeafWetnessCounts { get; set; }

        [Column("Leaf_Wetness_Minutes")]
        public float? LeafWetnessMinutes { get; set; }
    }
}
