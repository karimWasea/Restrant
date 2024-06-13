using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static C_Utilities.Enumes;

namespace DataAcessLayers
{
    public class NotPayedmoney
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
         public decimal? TotalPayedAmount { get; set; }
        public decimal? TotalNotpayedAmount { get; set; }
        public string? ChangedByUserId { get; set; }
 
        public string ?SystemUserId { get; set; }
        public string? SystemUserName { get; set; }


        public int PaymentStatus { get; set; }
   
        public ICollection<NotPayedmoneyHistory> NotPayedmoneyHistory { get; set; }

       }

}
