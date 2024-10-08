﻿using Cf_Viewmodels;

using Microsoft.AspNetCore.Mvc.Rendering;

using X.PagedList;

using static C_Utilities.Enumes;

namespace Interfaces
{
    public interface Ilookup
    {
        
          List<SelectListItem> GetCustomerType();
          List<SelectListItem> GetCategoryType();
          List<SelectListItem> HospitalOroprationtyp();
        public IQueryable<SelectListItem> Users(CustomerType CustomerType);
        public IQueryable<SelectListItem> Users();
        public IQueryable<SelectListItem> Users(string userid);
        public List<SelectListItem> GetPaymentStatus();
        public List<SelectListItem> GetGenderType();

 
        //public IQueryable<SelectListItem> GetCustomerTypesId(int selectedId = 0);




    }

}
