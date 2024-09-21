using CourseGuide.Objects.Contracts;
using CourseGuide.Objects.Enums;
using CourseGuide.Objects.Enums.State;
using CourseGuide.Objects.Generics;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseGuide.Objects.DTOs.Entities
{
    public class ReservationDTO : IEntityInt
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data é requerida!")]
        public string DateReservation { get; set; }

        public string? DateFinish { get; set; }

        public string? TimeDuration { get; set; }

        [Required(ErrorMessage = "O valor é requerido!")]
        public decimal ValueReservation
        {
            get => _valueReservation;
            set => _valueReservation = Math.Round(value, 2);
        }
        private decimal _valueReservation;

        public StatusReservation Status { get; set; }

        [Required(ErrorMessage = "O usuário é requerido!")]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "A mesa é requerida!")]
        public int IdTable { get; set; }

        public string? CreateAt { get; set; }

        public string? UpdateAt { get; set; }


        [JsonIgnore]
        public UserDTO? UserDTO { get; set; }

        [JsonIgnore]
        public TableDTO? TableDTO { get; set; }



        public void DefaultState() => Status = StatusReservationExtensions.DefaultState();
        public void PutInProgress() => Status = StatusReservationExtensions.PutInProgress();
        public void Finish() => Status = StatusReservationExtensions.Finish();
        public void Block() => Status = StatusReservationExtensions.Block();

        public string GetState() => IStatusStateExtensions.GetState(this.Status);
        public bool CanDefaultState() => IStatusStateExtensions.CanDefaultState(this.Status);
        public bool CanInProgress() => IStatusStateExtensions.CanInProgress(this.Status);
        public bool CanFinish() => IStatusStateExtensions.CanFinish(this.Status);
        public bool CanBlock() => IStatusStateExtensions.CanBlock(this.Status);
    }
}