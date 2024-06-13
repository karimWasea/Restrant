using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAcessLayers
{
    public class NotPayedmoneyHistoryPriceProductebytypes
    {
        public int Id { get; set; }
    public virtual NotPayedmoneyHistory NotPayedmoneyHistory { get; set; }
    public   int NotPayedmoneyHistoryid { get; set; }
    public int PriceProductebytypesid { get; set; }
    public virtual PriceProductebytypes  PriceProductebytypes { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;

    }


}
