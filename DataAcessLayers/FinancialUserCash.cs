namespace DataAcessLayers
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using static C_Utilities.Enumes;

    public class FinancialUserCash
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
         public decimal? PayedTotalAmount { get; set; } 
 
         public string? SystemUserId { get; set; }
        public string? SystemUserName { get; set; }
        public int PaymentStatus { get; set; }
          public ICollection<FinancialUserCashHistory> FinancialUserCashHistory { get; set; }
 
    }


}
