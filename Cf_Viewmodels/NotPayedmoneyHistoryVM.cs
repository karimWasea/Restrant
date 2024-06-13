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
    public class NotPayedmoneyHistoryVM : BaseVM
    {
       


            public decimal? TotalPayedAmount { get; set; }
            public decimal? TotalNotpayedAmount { get; set; }
            public decimal? payedAmount { get; set; }
      
        public string? ChangedByUserId { get; set; }
        public int? Qantity { get; set; }
        public decimal? totalpricforanyproduct { get; set; }
        public decimal? Totalmaoytohospital { get; set; }

        public string UserNotPayedmoneyName { get; set; }=string.Empty;
            public string? SystemUserId { get; set; }
            public string? UserName { get; set; }
            public DateTime CreationTime { get; set; }
            public DateTime ChangeDate { get; set; }
            public string? SystemUserName { get; set; }

            public bool ishospital { get; set; } = false;






            public decimal? NotpayedAmount { get; set; }

            public PaymentStatus PaymentStatus { get; set; }
            public int HospitalaoOrprationtyp { get; set; }
            public string? UserNotPayedmoneyId { get; set; }

            public int NotPayedmoneyId { get; set; }
            public int? pageNumber { get; set; }

        


    }
}
