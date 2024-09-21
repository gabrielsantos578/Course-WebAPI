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
        public string NumberTable { get; set; }

        [Required(ErrorMessage = "A capacidade é requerida!")]
        [MaxLength(2)]
        public string CapacityTable { get; set; }

        [Required(ErrorMessage = "A localização é requerida!")]
        [MaxLength(50)]
        public string LocationTable { get; set; }

        [Required(ErrorMessage = "O valor é requerido!")]
        [Range(0, 999999999999.99, ErrorMessage = "O valor deve ter até 2 casas decimais!")]
        public decimal ValueTable { get; set; }

        public int IdRestaurant { get; set; }



        [JsonIgnore]
        public RestaurantDTO? RestaurantDTO { get; set; }
    }
}
