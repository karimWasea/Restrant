using AutoMapper.Configuration.Annotations;

using Microsoft.AspNetCore.Http;
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
    public class productVM : BaseVM
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]

        public string ProductName { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Range(0.01, double.MaxValue, ErrorMessage = "ادخل قيمه مختلفه")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Range(1, int.MaxValue, ErrorMessage = "ادخل قيمه مختلفه")]
        public decimal Qantity { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]

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
