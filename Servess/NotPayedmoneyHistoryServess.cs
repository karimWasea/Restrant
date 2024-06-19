using AutoMapper;
using C_Utilities;

using Cf_Viewmodels;
using DataAcessLayers;
using DataAcessLayers.Migrations;

using Interfaces;

using Microsoft.AspNetCore.Http;
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

            queryable.TotalNotpayedAmount -= criteria.payedAmount;
            queryable.TotalPayedAmount += criteria.payedAmount;
            if (queryable.TotalNotpayedAmount == queryable.TotalPayedAmount)
            {
                queryable.PaymentStatus = (int)Enumes.PaymentStatus.Paid;

            }
            queryable.PaymentStatus = (int)Enumes.PaymentStatus.NotPaid;
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

            // Update the quantity of associated products
            foreach (var historyItem in notPayedmoneyHistoryItems)
            {
                var productItems = notPayedmoneyHistoryPriceProductebytypesItems
                    .Where(pp => pp.NotPayedmoneyHistoryid == historyItem.Id)
                    .Select(pp => pp.PriceProductebytypes.Product);

                foreach (var product in productItems)
                {
                    product.Qantity += historyItem.Qantity; // Ensure the property name is correct
                }
            }

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


        public IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoney(NotPayedmoneyHistoryVM criteria)
        {
            var queryable = _context.NotPayedmoneyHistory.Include(i => i.NotPayedmoneys).Include(i => i.UserNotPayedmoney).Where(i => (i.PaymentStatus == (int)criteria.PaymentStatus || criteria.PaymentStatus == 0)

              && (criteria.UserNotPayedmoneyName == null || i.UserNotPayedmoney.UserName.Contains(criteria.UserNotPayedmoneyName)) &&
              (i.PaymentStatus == (int)criteria.PaymentStatus || criteria.PaymentStatus == 0)

              ).Select(i => new NotPayedmoneyHistoryVM
              {

                  Id = i.Id,
                  HospitalaoOrprationtyp = (Enumes.HospitalOroprationtyp)i.HospitalaoOrprationtyp
                   ,
                  UserNotPayedmoneyName = i.UserNotPayedmoney.UserName ?? "",
                  CreationTime = i.CreationTime,
                  NotpayedAmount = i.NotpayedAmount,
                  ChangedByUserId = i.ChangedByUserId,
                  payedAmount = i.payedAmount,

                  ishospital = i.ishospital,
                  NotPayedmoneyId = i.NotPayedmoneyId,
                  PaymentStatus = (Enumes.PaymentStatus)i.PaymentStatus,
                  TotalNotpayedAmount = _context.NotPayedmoney.FirstOrDefault(p => p.Id == i.NotPayedmoneyId).TotalNotpayedAmount ?? 0,
                  TotalPayedAmount = _context.NotPayedmoney.FirstOrDefault(p => p.Id == i.NotPayedmoneyId).TotalPayedAmount ?? 0,

                
                 UserNotPayedmoneyId = i.UserNotPayedmoneyId,

             }
            ).OrderBy(g => g.Id);

            // Provide a default value for PageNumber if it's null
            int pageNumber = criteria.PageNumber ?? 1;

            var pagedList = GetPagedData(queryable, pageNumber);

            return pagedList;



        }



        public IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoneyHistoryDetails(int id , int? pageNuber )
        {
            var queryable = _context.NotPayedmoneyHistory.Include(i => i.UserNotPayedmoney).Where(i => i.NotPayedmoneyId == id



                        ).Select(i => new NotPayedmoneyHistoryVM
                        {

                            Id = i.Id,
                            HospitalaoOrprationtyp = (Enumes.HospitalOroprationtyp)i.HospitalaoOrprationtyp
                             ,
                            UserNotPayedmoneyName = i.UserNotPayedmoney.UserName ?? "",
                            ChangedByUserId = i.ChangedByUserId,
                            CreationTime = i.CreationTime,
                            NotpayedAmount = i.NotpayedAmount,
                            ishospital = i.ishospital,
                            NotPayedmoneyId = i.NotPayedmoneyId,
                            Qantity = i.Qantity,
                             payedAmount= i.payedAmount,

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
                    updated.Qantity = criteria.Qantity;

                    // Find the associated FinancialUserCash record
                    var financialUserCash = _context.NotPayedmoney.Find(criteria.NotPayedmoneyId);
                    if (financialUserCash == null)
                    {
                        throw new Exception("FinancialUserCash not found");
                    }

                    // Adjust the PayedTotalAmount
                    financialUserCash.TotalPayedAmount -= updated.payedAmount;
                    var newTotalPrice = criteria.Qantity * criteria.Pricforonproduct;
                    financialUserCash.TotalPayedAmount += newTotalPrice;

                    // Find the associated product
                    var product = _context.products.Find(criteria.Productid);
                    if (product == null)
                    {
                        throw new Exception("Product not found");
                    }

                    // Update the quantity of the product
                    product.Qantity -= criteria.Qantity;

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
                           && i.ChangeDate == DateTime.Now.Date && i.PaymentStatus == (int)Enumes.PaymentStatus.NotPaid

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


    }
}
