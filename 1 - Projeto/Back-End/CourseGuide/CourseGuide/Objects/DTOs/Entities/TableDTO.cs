using CourseGuide.Objects.Generics;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseGuide.Objects.DTOs.Entities
{
    public class TableDTO : IEntityInt
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O número é requerido!")]
        [MaxLength(20)]
        public string CodeTable { get; set; }

        [Required(ErrorMessage = "A capacidade é requerida!")]
        [MaxLength(2)]
        public string CapacityTable { get; set; }

        [Required(ErrorMessage = "A localização é requerida!")]
        [MaxLength(50)]
        public string LocationTable { get; set; }

        [Required(ErrorMessage = "O valor é requerido!")]
        public decimal ValueTable
        {
            get => _valueTable;
            set => _valueTable = Math.Round(value, 2);
        }
        private decimal _valueTable;

        public bool AvailableTable { get; set; }

        [Required(ErrorMessage = "O restaurante é requerido!")]
        public int IdRestaurant { get; set; }



        [JsonIgnore]
        public RestaurantDTO? RestaurantDTO { get; set; }

        [JsonIgnore]
        public ICollection<ReservationDTO>? ReservationsDTO { get; set; }
    }
}
