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
        public string CodeTable { get; set; }

        [Column("capacitytable")]
        public string CapacityTable { get; set; }

        [Column("locationtable")]
        public string LocationTable { get; set; }

        [Column("valuetable")]
        public decimal ValueTable { get; set; }

        [Column("availabletable")]
        public bool AvailableTable { get; set; }

        [Column("idrestaurant")]
        public int IdRestaurant { get; set; }


        public RestaurantModel? RestaurantModel { get; set; }
        public ICollection<ReservationModel>? ReservationsModel { get; set; }
    }
}
