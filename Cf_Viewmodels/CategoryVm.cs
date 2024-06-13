using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

using static C_Utilities.Enumes;

namespace Cf_Viewmodels
{
    public class BaseVM
    {
        public int Id { get; set; }
        public int ?PageNumber { get; set; }
      public  int pageSize { get; set; }
        public string SystemUserId { get; set; } =string.Empty;
        public string SystemUserName { get; set; }=
        string.Empty;


    }
    public class CategoryVm
    {
        public int? PageNumber { get; set; }
        public int pageSize { get; set; }
        public string SystemUserId { get; set; } = string.Empty;
        public string SystemUserName { get; set; } =
        string.Empty;

        public CategoryType Id { get; set; }
        public IEnumerable<SelectListItem>? CategoryIdList { get; set; } = Enumerable.Empty<SelectListItem>();

        public string? CategoryName { get; set; }
         public IFormFile? Imge { get; set; }
        public string? Description { get; set; }
    }
}
