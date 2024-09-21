using CourseGuide.Objects.Generics;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseGuide.Objects.Models.Entities
{
    [Table("restaurants")]
    public class RestaurantModel : IEntityInt
    {
        [Column("idrestaurant")]
        public int Id { get; set; }

        [Column("namerestaurant")]
        public string NameRestaurant { get; set; }

        [Column("emailrestaurant")]
        public string EmailRestaurant { get; set; }

        [Column("phonerestaurant")]
        public string PhoneRestaurant { get; set; }

        [Column("iduser")]
        public int IdOwner { get; set; }


        public UserModel? OwnerModel { get; set; }
    }
}
