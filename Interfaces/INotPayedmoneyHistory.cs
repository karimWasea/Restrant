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


        void SaveNotPayedmoney(FinancialUserCashHistoryVM criteria);
        void DeleteNotPayedmoney(FinancialUserCashHistoryVM criteria);
        IPagedList<FinancialUserCashHistoryVM> SearchNotPayedmoney(FinancialUserCashHistoryVM criteria);


        void SaveNotPayedmoneyHistoryDetails(int id);
        void DeleteNotPayedmoneyHistory(int id);
        IPagedList<FinancialUserCashHistoryVM> SearchNotPayedmoneyHistory(FinancialUserCashHistoryVM criteria);
    }










}
