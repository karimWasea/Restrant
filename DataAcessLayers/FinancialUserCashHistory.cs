namespace DataAcessLayers
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using static C_Utilities.Enumes;
    using Microsoft.EntityFrameworkCore;

    [Owned]
    public class FinancialUserCashHistory   
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? Qantity { get; set; }
        public decimal? PayedAmount { get; set; }
        public int PaymentStatus { get; set; }
        public int HospitalOroprationtyp { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;

        public string? SystemUserId { get; set; }
        public string? SystemUserName { get; set; }
        public int FinancialUserCashId { get; set; }
 
        public FinancialUserCash FinancialUserCash { get; set; }
         public virtual ICollection<FinancialUserCashHistoryPriceProductebytypes> FinancialUserCashHistoryPriceProductebytypes { get; set; }

    }


}
