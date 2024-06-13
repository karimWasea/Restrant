﻿using Cf_Viewmodels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using X.PagedList;

namespace Interfaces
{
    public interface IPriceProductebytypes : IGenericService<PriceProductebytypesVM>
    {
        public IPagedList<PriceProductebytypesVM> SearchForTypes(PriceProductebytypesVM criteria);
        public void AddShopingCaterCashHistory(PriceProductebytypesVM criteria);
        public void AddShopingCaterNotpayedHistory(PriceProductebytypesVM criteria);
        public void UpdateShopingCaterCashHistory(PriceProductebytypesVM criteria);
        public void UpdateShopingCaterNotpayedHistory(PriceProductebytypesVM criteria);
        public IEnumerable<PriceProductebytypesVM> GetallfromShopingCart(PriceProductebytypesVM criteria);
        public IEnumerable<PriceProductebytypesVM> GetallfromShopingCartNopayed(PriceProductebytypesVM criteria);
        public void DeleteShopingCaterCashHistory(int id);
        public void DeleteShopingCaterNotpayedHistory(int id)
;
        public void FreeShopingCaterCashHistoryToFinancialUserCash(string? SystemUserId, string? SystemUserName);
        public void FreeShopingCaterCashHistoryToNotpayed(string? SystemUserId, string? SystemUserName);




    }
}
