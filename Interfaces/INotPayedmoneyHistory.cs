using Cf_Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using X.PagedList;

namespace Interfaces
{
    public interface INotPayedmoneyHistoryServess : IPaginationHelper<NotPayedmoneyHistoryVM> 
    {


        bool SaveNotPayedmoney(NotPayedmoneyHistoryVM criteria);
        bool DeleteNotPayedmoney(int id);
        bool CheckIfExisitNotPayedmoney(int id);
        IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoney(NotPayedmoneyHistoryVM criteria);


        public IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoneyHistoryDetails(int id, int? pageNuber);
        bool DeleteNotPayedmoneyHistory(int id);
        public IPagedList<NotPayedmoneyHistoryVM> PrintforHospitallDay(NotPayedmoneyHistoryVM criteria);  
            bool SaveNotPayedmoneyHistory(NotPayedmoneyHistoryVM criteria);

        bool CheckIfExisitNotPayedmoneyDetails(int id);


    }




    public interface IFinancialUserCashHistoryServess : IPaginationHelper<FinancialUserCashHistoryVM>
    {


         bool DeleteFinancialUserCash(int id);
        IPagedList<FinancialUserCashHistoryVM> SearchFinancialUserCashH(FinancialUserCashHistoryVM criteria);


        public IPagedList<FinancialUserCashHistoryVM> SearchFinancialUserCashHistoryDetails(int id, int? pageNuber);
        public bool DeleteFinancialUserCashHistories(int id);
       
    }










}
