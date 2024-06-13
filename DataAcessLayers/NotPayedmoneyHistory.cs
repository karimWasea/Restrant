using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static C_Utilities.Enumes;

namespace DataAcessLayers
{
    public class NotPayedmoneyHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool ishospital { get; set; } = false;
        public int? Qantity { get; set; }

        public string? SystemUserId { get; set; }
        public string ?SystemUserName { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
         public decimal? NotpayedAmount { get; set; }
         public decimal? payedAmount { get; set; }
        public DateTime ? ChangeDate { get; set; }
        public string? ChangedByUserId { get; set; }
         public string ?Description { get; set; }
        public int PaymentStatus { get; set; }
        public int HospitalaoOrprationtyp { get; set; }
        public string? UserNotPayedmoneyId { get; set; }
        public Applicaionuser UserNotPayedmoney { get; set; }
        public int NotPayedmoneyId { get; set; }
        public virtual ICollection<NotPayedmoneyHistoryPriceProductebytypes> NotPayedmoneyHistoryPriceProductebytypes { get; set; }

        public virtual NotPayedmoney NotPayedmoneys { get; set; }
    }


}
