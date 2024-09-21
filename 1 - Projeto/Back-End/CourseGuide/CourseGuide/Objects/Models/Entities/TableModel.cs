using CourseGuide.Objects.Generics;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseGuide.Objects.Models.Entities
{
    [Table("tables")]
    public class TableModel : IEntityInt
    {
        [Column("idtable")]
        public int Id { get; set; }

        [Column("numbertable")]
        public string NumberTable { get; set; }

        [Column("capacitytable")]
        public string CapacityTable { get; set; }

        [Column("locationtable")]
        public string LocationTable { get; set; }

        [Column("valuetable", TypeName = "decimal(18, 2)")]
        public decimal ValueTable { get; set; }

        [Column("idrestaurant")]
        public int IdRestaurant { get; set; }


        public RestaurantModel? RestaurantModel { get; set; }
    }
}
