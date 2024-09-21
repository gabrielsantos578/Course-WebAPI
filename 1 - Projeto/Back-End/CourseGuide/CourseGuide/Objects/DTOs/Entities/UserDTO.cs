using CourseGuide.Objects.Generics;
using CourseGuide.Objects.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseGuide.Objects.DTOs.Entities
{
    public class UserDTO : IEntityInt
    {
        public int Id { get; set; }

        public string? ImageProfile { get; set; }

        [Required(ErrorMessage = "O nome é requerido!")]
        [MaxLength(100)]
        public string NameUser { get; set; }

        [Required(ErrorMessage = "O e-mail é requerido!")]
        [MinLength(10)]
        [MaxLength(200)]
        public string EmailUser
        {
            get => _emailUser;
            set => _emailUser = value.ToLower();
        }
        private string _emailUser;

        [Required(ErrorMessage = "A senha é requerida!")]
        [MinLength(8)]
        public string PasswordUser { get; set; }

        [Required(ErrorMessage = "O telefone é requerido!")]
        [MinLength(14)]
        [MaxLength(15)]
        public string PhoneUser { get; set; }


        [JsonIgnore]
        public ICollection<RestaurantDTO>? RestaurantsDTO { get; set; }

        [JsonIgnore]
        public ICollection<ReservationDTO>? ReservationsDTO { get; set; }
    }
}
