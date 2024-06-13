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
    public class FinancialUserCashVM : BaseVM
    {
         public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? Qantity { get; set; }
        public CategoryType CategoryTyPe { get; set; }

         public string CategoryName { get; set; }
        public IEnumerable<SelectListItem>? CategoryIdList { get; set; } = Enumerable.Empty<SelectListItem>();
        //public List<IFormFile> Files { get; set; }

        //[AllowedExtensions(FileSettings.AllowedExtensions),
        //    MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile ? Cover { get; set; } = default!;
        public string  CoverString { get; set; } = string.Empty;
         public string? Description { get; set; }
        public int? Discount { get; set; } // Nullable discount property

     }
}
