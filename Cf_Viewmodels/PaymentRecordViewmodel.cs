using AutoMapper.Configuration.Annotations;

using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static C_Utilities.Enumes;

namespace Cf_Viewmodels
{
    public class PaymentRecordViewmodel :BaseVM
    {
 
 
        public DateTime ? CreationTime { get; set; } 

        public decimal? TotalPaidAmount { get; set; }

        public decimal? TotalUnpaidAmount { get; set; }

        public string? UserId { get; set; }
        public string? UserName { get; set; }
         
        public int UnpaidMoneyId { get; set; }
         
        public string? Notes { get; set; } 
 
    }
}
