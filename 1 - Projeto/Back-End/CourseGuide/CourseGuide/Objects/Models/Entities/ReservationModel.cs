using CourseGuide.Objects.Enums;
using CourseGuide.Objects.Enums.State;
using CourseGuide.Objects.Generics;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseGuide.Objects.Models.Entities
{
    [Table("reservations")]
    public class ReservationModel : IEntityInt
    {
        [Column("idreservation")]
        public int Id { get; set; }

        [Column("datereservation")]
        public string DateReservation { get; set; }

        [Column("datefinish")]
        public string DateFinish { get; set; }

        [Column("timeduration")]
        public string TimeDuration { get; set; }

        [Column("valuereservation")]
        public decimal ValueReservation { get; set; }

        [Column("statusreservation")]
        public StatusReservation Status { get; set; }

        [ForeignKey("iduser")]
        public int IdUser { get; set; }

        [ForeignKey("idtable")]
        public int IdTable { get; set; }

        [Column("createat")]
        public string CreateAt { get; set; }

        [Column("updateat")]
        public string UpdateAt { get; set; }


        public UserModel? UserModel { get; set; }
        public TableModel? TableModel { get; set; }



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
