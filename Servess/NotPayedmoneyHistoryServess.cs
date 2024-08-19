using AutoMapper;
using C_Utilities;

using Cf_Viewmodels;
using DataAcessLayers;
using DataAcessLayers.Migrations;

using Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Servess
{
    public class NotPayedmoneyHistoryServess : PaginationHelper<NotPayedmoneyHistoryVM>, INotPayedmoneyHistoryServess
    {
        public readonly ApplicationDBcontext _context; 
    
        private readonly IMapper _mapper;

        public NotPayedmoneyHistoryServess(ApplicationDBcontext context, IMapper mapper    )
        {
             _context = context;
            _mapper = mapper;

        }

       

        public bool SaveNotPayedmoney(NotPayedmoneyHistoryVM criteria)
        {
            var ispayed = false;

            var queryable = _context.NotPayedmoney.FirstOrDefault(i=>i.Id==criteria.Id); 
             if(queryable.TotalNotpayedAmount == queryable.TotalPayedAmount)
            {
                return  true; 

            }
              if(criteria.payedAmount> queryable.TotalNotpayedAmount)
            {
                return false;

            }
                var Decpayed = queryable.TotalNotpayedAmount - criteria.payedAmount;
            queryable.TotalNotpayedAmount = Decpayed;
            var incPayed = queryable.TotalPayedAmount + criteria.payedAmount;
            queryable.TotalPayedAmount = incPayed;
            if (queryable.TotalNotpayedAmount == 0)
            {
                queryable.PaymentStatus = (int)Enumes.PaymentStatus.Paid;
                ispayed = true;


            }
            else
            {
                queryable.PaymentStatus = (int)Enumes.PaymentStatus.NotPaid;
                ispayed=true;
            }
            _context.Update(queryable);
            _context.SaveChanges();
            return ispayed;    


        }
        public bool DeleteNotPayedmoney(int id)
        {
            // Find the NotPayedmoney entity by id
            var notPayedmoney = _context.NotPayedmoney.Find(id);
            if (notPayedmoney == null)
            {
                return false; // or throw an exception
            }

            // Find the associated NotPayedmoneyHistory entities
            var notPayedmoneyHistoryItems = _context.NotPayedmoneyHistory
                .Where(i => i.NotPayedmoneyId == id)
                .ToList();

            // Find the associated NotPayedmoneyHistoryPriceProductebytypes entities
            var notPayedmoneyHistoryPriceProductebytypesItems = _context.NotPayedmoneyHistoryPriceProductebytypes
                .Where(i => notPayedmoneyHistoryItems.Select(h => h.Id).Contains(i.NotPayedmoneyHistoryid))
                .ToList();

            // Remove associated NotPayedmoneyHistoryPriceProductebytypes entities
            if (notPayedmoneyHistoryPriceProductebytypesItems.Any())
            {
                _context.NotPayedmoneyHistoryPriceProductebytypes.RemoveRange(notPayedmoneyHistoryPriceProductebytypesItems);
            }

            // Remove associated NotPayedmoneyHistory entities
            if (notPayedmoneyHistoryItems.Any())
            {
                _context.NotPayedmoneyHistory.RemoveRange(notPayedmoneyHistoryItems);
            }

            // Remove the NotPayedmoney entity
            _context.NotPayedmoney.Remove(notPayedmoney);

            // Save all changes
            _context.SaveChanges();

            return true;
        }
        public bool Salesreturns(int id)
        {
            // Find the NotPayedmoney entity by id
            var notPayedmoney = _context.NotPayedmoney.Find(id);
            if (notPayedmoney == null)
            {
                return false; // or throw an exception if preferred
            }

            // Find the associated NotPayedmoneyHistory entities
            var notPayedmoneyHistoryItems = _context.NotPayedmoneyHistory.
                Include(i=>i.NotPayedmoneyHistoryPriceProductebytypes).ThenInclude(i=>i.PriceProductebytypes
                ).ThenInclude(i=>i.Product)
                .Where(i => i.NotPayedmoneyId == id)
                .ToList();

            // Find the associated NotPayedmoneyHistoryPriceProductebytypes entities
            var notPayedmoneyHistoryPriceProductebytypesItems = _context.NotPayedmoneyHistoryPriceProductebytypes
                .Where(i => notPayedmoneyHistoryItems.Select(h => h.Id).Contains(i.NotPayedmoneyHistoryid))
                .ToList();

            // Update the quantity of associated products
            foreach (var historyItem in notPayedmoneyHistoryPriceProductebytypesItems)
            {
                // Ensure Product is not null before updating quantity
                var product = historyItem.PriceProductebytypes?.Product;
                if (product != null)
                {
                     foreach (var item in notPayedmoneyHistoryItems)
                    {
                        product.Qantity += item.Qantity;

                    }
                }
            }

            // Remove associated NotPayedmoneyHistoryPriceProductebytypes entities
            _context.NotPayedmoneyHistoryPriceProductebytypes.RemoveRange(notPayedmoneyHistoryPriceProductebytypesItems);

            // Remove associated NotPayedmoneyHistory entities
            _context.NotPayedmoneyHistory.RemoveRange(notPayedmoneyHistoryItems);

            // Remove the NotPayedmoney entity
            _context.NotPayedmoney.Remove(notPayedmoney);

            // Save all changes
            _context.SaveChanges();

            return true;
        }


        //public IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoney2(NotPayedmoneyHistoryVM criteria)
        //{
        //    var queryable = _context.NotPayedmoney
        //        .Include(i => i.NotPayedmoneyHistory)
        //            .ThenInclude(nph => nph.UserNotPayedmoney)
        //        .Where(i =>
        //            (criteria.PaymentStatus == 0 || i.PaymentStatus == (int)criteria.PaymentStatus)
        //        &&
        //        (i.NotPayedmoneyHistory.FirstOrDefault().HospitalaoOrprationtyp == (int)criteria.HospitalaoOrprationtyp || criteria.HospitalaoOrprationtyp == 0) &&
        //        (criteria.UserNotPayedmoneyName == null || criteria.UserNotPayedmoneyName == string.Empty || i.NotPayedmoneyHistory.Any(nph => nph.UserNotPayedmoney.FullCustumName.Contains(criteria.UserNotPayedmoneyName)))
        //        )
        //        .Select(i => new NotPayedmoneyHistoryVM
        //        {
        //            Id = i.Id,
        //            HospitalaoOrprationtyp = (Enumes.HospitalOroprationtyp)(i.NotPayedmoneyHistory.FirstOrDefault().HospitalaoOrprationtyp),
        //            UserNotPayedmoneyName = i.NotPayedmoneyHistory.FirstOrDefault().UserNotPayedmoney.FullCustumName ?? "",
        //            CreationTime = i.CreationTime,
        //            TotalNotpayedAmount = i.TotalNotpayedAmount,
        //            ChangedByUserId = i.ChangedByUserId,
        //            TotalPayedAmount = i.TotalPayedAmount,
        //            PaymentStatus = (Enumes.PaymentStatus)i.PaymentStatus,
        //            UserNotPayedmoneyId = i.NotPayedmoneyHistory.FirstOrDefault().UserNotPayedmoney.Id ?? ""
        //        })
        //        .OrderBy(g => g.Id);

        //    // Provide a default value for PageNumber if it's null
        //    int pageNumber = criteria.PageNumber ?? 1;

        //    var pagedList = GetPagedData(queryable, pageNumber);

        //    return pagedList;
        //}





        public IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoney(NotPayedmoneyHistoryVM criteria)
        {
            var queryable = from notPayed in _context.NotPayedmoney
                            join history in _context.NotPayedmoneyHistory on notPayed.Id equals history.NotPayedmoneyId


                            //join userNotPayed in _context.Users on history.UserNotPayedmoneyId equals userNotPayed.Id
                            //  into userNotPayed2
                            //from userNotPayed in userNotPayed2.DefaultIfEmpty()
                             let userNotPayed = _context.Users.FirstOrDefault(I=>I.Id== history.UserNotPayedmoneyId )
                            where
                            (criteria.PaymentStatus == 0 || notPayed.PaymentStatus == (int)criteria.PaymentStatus)
                            &&
                                  (history.UserNotPayedmoneyId == criteria.UserNotPayedmoneyId || criteria.UserNotPayedmoneyId==null)
                                  &&
                                  (criteria.HospitalaoOrprationtyp == 0 || history.HospitalaoOrprationtyp == (int)criteria.HospitalaoOrprationtyp)
                                          && (criteria.CreationTime == new DateTime(1, 1, 1) || notPayed.CreationTime.Date == criteria.CreationTime)

                            select new NotPayedmoneyHistoryVM
                            {
                                Id = notPayed.Id,
                                HospitalaoOrprationtyp = (Enumes.HospitalOroprationtyp)history.HospitalaoOrprationtyp,
                                UserNotPayedmoneyName = userNotPayed.FullCustumName ?? "",
                                CreationTime = notPayed.CreationTime,
                                TotalNotpayedAmount = notPayed.TotalNotpayedAmount,
                                ChangedByUserId = notPayed.ChangedByUserId,
                                TotalPayedAmount = notPayed.TotalPayedAmount,
                                PaymentStatus = (Enumes.PaymentStatus)notPayed.PaymentStatus,
                                UserNotPayedmoneyId = userNotPayed.Id ?? ""
                            };

            // Apply ordering
            queryable = queryable.OrderBy(g => g.CreationTime.Day).Distinct();

            // Get the page number from criteria, default to 1 if not provided
            int pageNumber = criteria.PageNumber ?? 1;

            // Get paginated data
            var pagedList = GetPagedData(queryable, pageNumber);

            return pagedList;
        }



        public IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoneyOneUser(NotPayedmoneyHistoryVM criteria)
        {




        

              var queryable =  
                            from history in _context.NotPayedmoneyHistory

                                //join userNotPayed in _context.Users on history.UserNotPayedmoneyId equals userNotPayed.Id
                            let notpayed = _context.NotPayedmoney.FirstOrDefault(i => i.Id == history.NotPayedmoneyId) 
                            
                            let userNotPayed = _context.Users.FirstOrDefault(i => i.Id == history.UserNotPayedmoneyId)
                            where
                            (criteria.PaymentStatus == 0 || notpayed.PaymentStatus == (int)criteria.PaymentStatus)
                            &&
                                  (history.UserNotPayedmoneyId == criteria.UserNotPayedmoneyId)

                            //&& (criteria.CreationTime == new DateTime(1, 1, 1) || history.CreationTime.Date == criteria.CreationTime)

                            select new NotPayedmoneyHistoryVM
                            {
                                 HospitalaoOrprationtyp = (Enumes.HospitalOroprationtyp)history.HospitalaoOrprationtyp,
                                UserNotPayedmoneyName = userNotPayed.FullCustumName ?? "",
                                CreationTime = history.CreationTime,
                                PaymentStatus = (Enumes.PaymentStatus)notpayed.PaymentStatus,
                                UserNotPayedmoneyId = userNotPayed.Id ?? "",
                                Qantity = history.Qantity,
                                totalpricforanyproduct = (_context.NotPayedmoneyHistoryPriceProductebytypes
                                .Where(pp => pp.NotPayedmoneyHistoryid == history.Id)
                                .Select(pp => (decimal?)pp.PriceProductebytypes.price)
                                .FirstOrDefault() ?? 0) * history.Qantity,
                                TotalNotpayedAmount = _context.NotPayedmoney.Where(i => i.Id == history.NotPayedmoneyId) .Sum(i=>i. TotalNotpayedAmount)??0,
                                TotalPayedAmount = _context.NotPayedmoney.Where(i => i.Id == history.NotPayedmoneyId).Sum(i => i.TotalPayedAmount)??0,

                                productName = _context.NotPayedmoneyHistoryPriceProductebytypes
                                .Where(pp => pp.NotPayedmoneyHistoryid == history.Id)
                                .Select(pp =>
                                pp.PriceProductebytypes.Product.ProductName)
                                .FirstOrDefault() ?? "",
                            };

            // Apply ordering
            //queryable = queryable.OrderBy(g => g.CreationTime.Day).Distinct();

            // Get the page number from criteria, default to 1 if not provided
            int pageNumber = criteria.PageNumber ?? 1;

            // Get paginated data
            var pagedList = GetPagedData(queryable, pageNumber);

            return pagedList;
        }
















        //public IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoney(NotPayedmoneyHistoryVM criteria)
        //{
        //    var queryable = from userNotPayed in _context.Users
        //                    join history in _context.NotPayedmoneyHistory on userNotPayed.Id equals history.UserNotPayedmoneyId into userHistory
        //                    from history in userHistory.DefaultIfEmpty()
        //                    join notPayed in _context.NotPayedmoney on history.NotPayedmoneyId equals notPayed.Id into notPayedHistory
        //                    from notPayed in notPayedHistory.DefaultIfEmpty()
        //                    where
        //                        (criteria.PaymentStatus == 0 || notPayed.PaymentStatus == (int)criteria.PaymentStatus) &&
        //                        (string.IsNullOrEmpty(criteria.UserNotPayedmoneyName) || userNotPayed.FullCustumName.Contains(criteria.UserNotPayedmoneyName)) &&
        //                        (criteria.HospitalaoOrprationtyp == 0 || history.HospitalaoOrprationtyp == (int)criteria.HospitalaoOrprationtyp)
        //                    select new NotPayedmoneyHistoryVM
        //                    {
        //                        Id = notPayed != null ? notPayed.Id : 0,
        //                        HospitalaoOrprationtyp = history != null ? (Enumes.HospitalOroprationtyp)history.HospitalaoOrprationtyp : default,
        //                        UserNotPayedmoneyName = userNotPayed.FullCustumName ?? "",
        //                        CreationTime = notPayed != null ? notPayed.CreationTime : DateTime.MinValue,
        //                        TotalNotpayedAmount = notPayed != null ? notPayed.TotalNotpayedAmount : 0.0m,
        //                        ChangedByUserId = notPayed != null ? notPayed.ChangedByUserId : "",
        //                        TotalPayedAmount = notPayed != null ? notPayed.TotalPayedAmount : 0.0m,
        //                        PaymentStatus = notPayed != null ? (Enumes.PaymentStatus)notPayed.PaymentStatus : default,
        //                        UserNotPayedmoneyId = userNotPayed.Id
        //                    };

        //    // Apply ordering
        //    queryable = queryable.OrderBy(g => g.Id);

        //    // Get the page number from criteria, default to 1 if not provided
        //    int pageNumber = criteria.PageNumber ?? 1;

        //    // Get paginated data
        //    var pagedList = GetPagedData(queryable, pageNumber);

        //    return pagedList;
        //}


        // Method to get paginated data





        public IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoneyHistoryDetails(int id , int? pageNuber )
        {
            var queryable = _context.NotPayedmoneyHistory.Include(i => i.UserNotPayedmoney).Where(i => i.NotPayedmoneyId == id



                        ).Select(i => new NotPayedmoneyHistoryVM
                        {

                            Id = i.Id,
                            HospitalaoOrprationtyp = (Enumes.HospitalOroprationtyp)i.HospitalaoOrprationtyp
                             ,
                            UserNotPayedmoneyName = i.UserNotPayedmoney.FullCustumName ?? "",
                            ChangedByUserId = i.ChangedByUserId,
                            CreationTime = i.CreationTime,
                            NotpayedAmount = i.NotpayedAmount,
                            ishospital = i.ishospital,
                            NotPayedmoneyId = i.NotPayedmoneyId,
                            Qantity = i.Qantity,
                             payedAmount= i.payedAmount,
                                TotalNotpayedAmount=i.NotPayedmoneys.TotalNotpayedAmount,
                             productName = _context.NotPayedmoneyHistoryPriceProductebytypes
                .Where(pp => pp.NotPayedmoneyHistoryid == i.Id)
                .Select(pp =>
                pp.PriceProductebytypes.Product.ProductName)
                .FirstOrDefault() ?? "",
                            Productid = _context.NotPayedmoneyHistoryPriceProductebytypes
                .Where(pp => pp.NotPayedmoneyHistoryid == i.Id)
                .Select(pp =>
                pp.PriceProductebytypes.Product.Id)
                .FirstOrDefault(),

                            Pricforonproduct = _context.NotPayedmoneyHistoryPriceProductebytypes
                .Where(pp => pp.NotPayedmoneyHistoryid == i.Id)
                .Select(pp =>
                pp.PriceProductebytypes.price)
                .FirstOrDefault(),


                            totalpricforanyproduct = (_context.NotPayedmoneyHistoryPriceProductebytypes
                .Where(pp => pp.NotPayedmoneyHistoryid == i.Id)
                .Select(pp => (decimal?)pp.PriceProductebytypes.price)
                .FirstOrDefault() ?? 0) * i.Qantity,
                            PaymentStatus = (Enumes.PaymentStatus)i.PaymentStatus,

                            UserNotPayedmoneyId = i.UserNotPayedmoneyId,

                        }
                       ).OrderBy(g => g.Id);

            // Provide a default value for PageNumber if it's null
            int pageNumber = pageNuber??   1;

            var pagedList = GetPagedData(queryable, pageNumber);

            return pagedList;
        }

        public bool DeleteFinancialUserCashHistories(int id, int payedTotalAmount, int NotPayedmoneyid, int productid)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Find the financial user cash history record
                    var financialUserCashHistory = _context.NotPayedmoneyHistory.Find(id);
                    if (financialUserCashHistory == null)
                    {
                        throw new Exception("FinancialUserCashHistory not found");
                    }

                    // Find the associated FinancialUserCash record
                    var financialUserCash = _context.NotPayedmoney.Find(NotPayedmoneyid);
                    if (financialUserCash == null)
                    {
                        throw new Exception("FinancialUserCash not found");
                    }

                    // Update the PayedTotalAmount in FinancialUserCash
                    financialUserCash.TotalPayedAmount -= payedTotalAmount;

                    // Find the associated product
                    var product = _context.products.Find(productid);
                    if (product == null)
                    {
                        throw new Exception("Product not found");
                    }

                    // Update the quantity of the product
                    product.Qantity += financialUserCashHistory.Qantity;

                    // Find and remove the related FinancialUserCashHistoryPriceProductebytypes records
                    var itemsToRemove = _context.FinancialUserCashHistoryPriceProductebytypes
                        .Where(i => i.financialUserCashHistoryid == id)
                        .ToList();
                    if (itemsToRemove.Any())
                    {
                        _context.FinancialUserCashHistoryPriceProductebytypes.RemoveRange(itemsToRemove);
                    }

                    // Remove the financial user cash history record
                    _context.NotPayedmoneyHistory.Remove(financialUserCashHistory);

                    // Save all changes and commit the transaction
                    _context.SaveChanges();
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    transaction.Rollback();
                    // Log the exception (ex) here if needed
                    return false;
                }
            }
        }



        [HttpPost]

        public bool SaveNotPayedmoneyHistory(NotPayedmoneyHistoryVM criteria)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Find the financial user cash history record
                    var updated = _context.NotPayedmoneyHistory.Find(criteria.Id);
                    if (updated == null)
                    {
                        throw new Exception("FinancialUserCashHistory not found");
                    }

                    // Update the quantity

                    // Find the associated FinancialUserCash record
                    var NotPayedmoney = _context.NotPayedmoney.Find(criteria.NotPayedmoneyId);
                    if (NotPayedmoney == null)
                    {
                        throw new Exception("FinancialUserCash not found");
                    }

                    // Adjust the PayedTotalAmount
                    NotPayedmoney. TotalNotpayedAmount -= updated.NotpayedAmount;
                    var newTotalPrice = criteria.Qantity * criteria.Pricforonproduct;
                    updated.NotpayedAmount = newTotalPrice;

                    NotPayedmoney.TotalNotpayedAmount += newTotalPrice;

                    // Find the associated product
                    var product = _context.products.Find(criteria.Productid);
                    if (product == null)
                    {
                        throw new Exception("Product not found");
                    }

                    // Update the quantity of the product
                    product.Qantity += updated.Qantity;
                    updated.Qantity = criteria.Qantity;
                    product.Qantity -= updated.Qantity;


                    // Save all changes and commit the transaction
                    _context.SaveChanges();
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    transaction.Rollback();
                    // Log the exception (ex) here if needed
                    return false;
                }
            }
        }


        public IPagedList<NotPayedmoneyHistoryVM> PrintforHospitallDay(NotPayedmoneyHistoryVM criteria)
        {
            var today = DateTime.Today;
            var now = DateTime.Now.Date.ToLocalTime;

            var queryable = _context.NotPayedmoneyHistory
                .Include(i => i.UserNotPayedmoney)
                .Include(i => i.NotPayedmoneys)
                .Where(i => i.HospitalaoOrprationtyp == (int)Enumes.HospitalOroprationtyp.Hospital
                           && i.ChangeDate == DateTime.Now.Date && i.NotPayedmoneys.PaymentStatus == (int)Enumes.PaymentStatus.NotPaid

                          )
                .Select(i => new NotPayedmoneyHistoryVM
                {
                    Id = i.Id,
                    HospitalaoOrprationtyp = (Enumes.HospitalOroprationtyp)i.HospitalaoOrprationtyp,
                    CreationTime = i.CreationTime,
                    NotpayedAmount = i.NotpayedAmount,
                    NotPayedmoneyId = i.NotPayedmoneyId,
                    PaymentStatus = (Enumes.PaymentStatus)i.PaymentStatus,
                    Qantity = i.Qantity,
                    totalpricforanyproduct = (_context.NotPayedmoneyHistoryPriceProductebytypes
                        .Where(pp => pp.NotPayedmoneyHistoryid == i.Id)
                        .Select(pp => (decimal?)pp.PriceProductebytypes.price)
                        .FirstOrDefault() ?? 0) * i.Qantity,
                    productName = _context.NotPayedmoneyHistoryPriceProductebytypes
                        .Where(pp => pp.NotPayedmoneyHistoryid == i.Id)
                        .Select(pp => pp.PriceProductebytypes.Product.ProductName)
                        .FirstOrDefault() ?? "",
                })
                .OrderBy(g => g.Id);

            // Provide a default value for PageNumber if it's null
            int pageNum = criteria.pageNumber ?? 1;

            var pagedList = GetPagedData(queryable, pageNum);

            return pagedList;
        }

        public bool CheckIfExisitNotPayedmoney(int id)
        {
            return _context.NotPayedmoney.Any(i => i.Id ==id );
        }

        public bool CheckIfExisitNotPayedmoneyDetails(int id)
        {
            return _context.NotPayedmoneyHistory.Any(i => i.Id == id);

        }

        public bool DeleteAllNotPayedmoney(List<int> allIds)
        {
            // Retrieve the list of entities corresponding to the given IDs
            var notPayedMoneyRecords = _context.NotPayedmoneyHistory
                .Where(record => allIds.Contains(record.Id))
                .ToList();

            // Remove the retrieved entities from the context
            _context.NotPayedmoneyHistory.RemoveRange(notPayedMoneyRecords);

            // Save changes to the database
            _context.SaveChanges();

            return true;
        }

        public bool DeletLLNotPayedmoney(List<int> Allides)
        {
            throw new NotImplementedException();
        }
    }
}
