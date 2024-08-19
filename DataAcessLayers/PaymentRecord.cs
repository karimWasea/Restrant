using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static C_Utilities.Enumes;

namespace DataAcessLayers
{
    public class PaymentRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;

        public decimal? TotalPaidAmount { get; set; }

        public decimal? TotalUnpaidAmount { get; set; }

        public string? UserId { get; set; }

        [NotMapped]
        public Applicaionuser? User { get; set; }

        public int UnpaidMoneyId { get; set; }

        public virtual NotPayedmoney UnpaidMoney { get; set; }

  
 
        public string? Notes { get; set; }  // Any additional information or remarks regarding the payment

     }

}
