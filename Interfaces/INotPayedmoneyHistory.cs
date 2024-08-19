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
        bool DeleteAllNotPayedmoney(List<int> Allides);
        bool CheckIfExisitNotPayedmoney(int id);
        IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoney(NotPayedmoneyHistoryVM criteria);
        public IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoneyOneUser(NotPayedmoneyHistoryVM criteria);
        public bool Salesreturns(int id);

        public IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoneyHistoryDetails(int id, int? pageNuber);
        public bool DeleteFinancialUserCashHistories(int id, int payedTotalAmount, int NotPayedmoneyid, int productid);
        public IPagedList<NotPayedmoneyHistoryVM> PrintforHospitallDay(NotPayedmoneyHistoryVM criteria);  
            bool SaveNotPayedmoneyHistory(NotPayedmoneyHistoryVM criteria);

        bool CheckIfExisitNotPayedmoneyDetails(int id);


    }




    public interface IFinancialUserCashHistoryServess : IPaginationHelper<FinancialUserCashHistoryVM>
    {


         bool DeleteFinancialUserCash(int id);
        public bool Salesreturns(int id);
        public decimal? CalCCashByDay();
        public (decimal? TotalPayment, decimal? TotalNotPayed) GetPaymentTotalForDay(DateTime specificDate);

        IPagedList<FinancialUserCashHistoryVM> SearchFinancialUserCashH(FinancialUserCashHistoryVM criteria);

        public bool SaveFinancialUserCashHistories(FinancialUserCashHistoryVM criteria);

        public IPagedList<FinancialUserCashHistoryVM> SearchFinancialUserCashHistoryDetails(int id, int? pageNuber);
        public bool DeleteFinancialUserCashHistories(int id,int PayedTotalAmount, int Frercahid,int Productid);

                     
       
    }










}
