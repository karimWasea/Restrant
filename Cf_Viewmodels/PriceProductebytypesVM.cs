using AutoMapper.Configuration.Annotations;

using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static C_Utilities.Enumes;

namespace Cf_Viewmodels
{
    public class PriceProductebytypesVM : BaseVM
    {
        public string NotpayedUserid { get; set; }  
        public string NotpayedUserName { get; set; }  =string.Empty;
        public bool isIncreas { get; set; }
        public IEnumerable<SelectListItem>? CustomerTypeIdList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem>? UsersLists { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem>? HospitalaoOrprationtypLists { get; set; } = Enumerable.Empty<SelectListItem>();
 
        public CustomerType CustomerType { get; set; }
        public HospitalOroprationtyp HospitalOroprationtypId { get; set; }
         public string CustomerTypeName { get; set; } =string.Empty;
        // Call extension method on the instance
        //public string CustomerTypeName => CustomerType.GetDescription();  // Call extension method on the instance

        public int ProductId { get; set; }
        public int ShopingCaterId
        { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ClientName { get; set; }=string.Empty;    
        public string ProductImg{ get; set; } =string.Empty  ;
        [Required(ErrorMessage = "هذا الحقل مطلوب")]

        public CategoryType Catid { get; set; }  
        public decimal? ProductOldPrice { get; set; }
          public int? ShopingCaterQantity { get; set; }
        public int? Discount { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Range(1, int.MaxValue, ErrorMessage = " ادخل قيمه اقل ")]
        public int Qantity { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Range(1, int.MaxValue, ErrorMessage = " ادخل قيمه اقل ")]
        public int price { get; set; }
        public decimal? totalprice { get; set; }
         public decimal? totalDibte { get; set; }
         public decimal? totalDibtehospital { get; set; }
 

    }
}
