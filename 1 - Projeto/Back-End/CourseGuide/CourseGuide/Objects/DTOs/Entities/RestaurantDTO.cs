using CourseGuide.Objects.Generics;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseGuide.Objects.DTOs.Entities
{
    public class RestaurantDTO : IEntityInt
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é requerido!")]
        [MaxLength(100)]
        public string NameRestaurant { get; set; }

        [Required(ErrorMessage = "O e-mail é requerido!")]
        [MinLength(10)]
        [MaxLength(200)]
        public string EmailRestaurant
        {
            get => _emailRestaurant;
            set => _emailRestaurant = value.ToLower();
        }
        private string _emailRestaurant;

        [Required(ErrorMessage = "O telefone é requerido!")]
        [MinLength(14)]
        [MaxLength(15)]
        public string PhoneRestaurant { get; set; }

        public int IdOwner { get; set; }


        [JsonIgnore]
        public UserDTO? OwnerDTO { get; set; }

        [JsonIgnore]
        public ICollection<TableDTO>? TablesDTO { get; set; }
    }
}
