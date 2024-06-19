using AutoMapper.Configuration.Annotations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static C_Utilities.Enumes;

namespace Cf_Viewmodels
{
    public class FinancialUserCashHistoryVM : BaseVM
    {
         public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? payedAmount { get; set; }
        public decimal? PayedTotalAmount { get; set; }
        public int? Qantity { get; set; }
        public int Frercahid { get; set; }
        public int? Pricforonproduct { get; set; }
        public int? Productid { get; set; }
         public HospitalOroprationtyp HospitalaoOrprationtyp { get; set; }
        public CategoryType CategoryTyPe { get; set; }

         public string CategoryName { get; set; }
         public DateTime CreationTime { get; set; }
        public IEnumerable<SelectListItem>? CategoryIdList { get; set; } = Enumerable.Empty<SelectListItem>();
        
        public IFormFile ? Cover { get; set; } = default!;
        public string  CoverString { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
         public string? Description { get; set; }
         public PaymentStatus PaymentStatus { get; set; }
        public int? Discount { get; set; } // Nullable discount property

     }
}
