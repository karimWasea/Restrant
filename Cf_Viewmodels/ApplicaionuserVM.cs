using AutoMapper.Configuration.Annotations;

using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static C_Utilities.Enumes;

namespace Cf_Viewmodels
{
    public class ApplicaionuserVM 
    {
        public string Id { get; set; } = string.Empty; 
        public string UserNme { get; set; }  
        public bool isIncreas { get; set; }
        public IEnumerable<SelectListItem>? CustomerTypeList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem>? GenderList { get; set; } = Enumerable.Empty<SelectListItem>();
  
        public CustomerType CustomerType { get; set; }
        public Gender Gender { get; set; }
        public int? PageNumber { get; set; }
        public HospitalOroprationtyp HospitalOroprationtypId { get; set; }
       
 

    }
}
