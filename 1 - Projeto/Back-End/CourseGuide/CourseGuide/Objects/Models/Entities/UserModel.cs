using System.ComponentModel.DataAnnotations.Schema;

namespace CourseGuide.Objects.Models.Entities;
[Table("users")]
public class UserModel
{
    [Column("iduser")]
    public int Id { get; set; }

    [Column("imageprofile")]
    public string ImageProfile { get; set; }

    [Column("nameuser")]
    public string NameUser { get; set; }

    [Column("emailuser")]
    public string EmailUser { get; set; }

    [Column("passworduser")]
    public string PasswordUser { get; set; }

    [Column("phoneuser")]
    public string PhoneUser { get; set; }
}