using Cf_Viewmodels;

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
;   bool CheckQantityProduct(int id, decimal quantityFromShoppingCard);
        public bool checkedifShopingCaterCashHistoryHavedata();
      
    public bool checkedifhopingCaterNotpayedHavedata();
         public void FreeShopingCaterCashHistoryToFinancialUserCash(string? SystemUserId, string? SystemUserName);
        public void FreeShopingCaterCashHistoryToNotpayed(string? SystemUserId, string? SystemUserName);
        public bool EnDDebite(PriceProductebytypesVM entity);
        public PriceProductebytypesVM GetDibts(PriceProductebytypesVM entity);
        public bool EnDDebiteHospital();
        public PriceProductebytypesVM GetDibtsHospital();








    }
}
