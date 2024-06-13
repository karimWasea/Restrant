using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAcessLayers
{
    public class FinancialUserCashHistoryPriceProductebytypes
    {
        public int Id { get; set; }
    public virtual FinancialUserCashHistory financialUserCashHistory { get; set; }
    public   int financialUserCashHistoryid { get; set; }
    public int PriceProductebytypesid { get; set; }
    public virtual PriceProductebytypes  PriceProductebytypes { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;

    }


}
