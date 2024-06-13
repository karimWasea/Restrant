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
    public class RoleViewModel
    {
        public string? roleId { get; set; }
        public string? roleName { get; set; }


        public bool useRole { get; set; }

    }
}
