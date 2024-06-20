using AutoMapper;
using C_Utilities;

using Cf_Viewmodels;
using DataAcessLayers;
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
    public class FinancialUserCashHistoryServess : PaginationHelper<FinancialUserCashHistoryVM>, IFinancialUserCashHistoryServess
    {
        public readonly ApplicationDBcontext _context; 
        Imgoperation _Imgoperation;

        private readonly IMapper _mapper;

        public FinancialUserCashHistoryServess(ApplicationDBcontext context, IMapper mapper    ,     Imgoperation imgoperation
)
        {
            _Imgoperation = imgoperation;
            _context = context;
            _mapper = mapper;

        }



        public bool DeleteFinancialUserCash( int id)
        {
            // Find the NotPayedmoney entity by id
            var notPayedmoney = _context.FinancialUserCash.Find(id);
            if (notPayedmoney == null)
            {
                return false; // or throw an exception
            }

            // Find the associated NotPayedmoneyHistory entities
            var notPayedmoneyHistoryItems = _context.FinancialUserCashHistories
                .Where(i => i. FinancialUserCashId == id)
                .ToList();

            // Find the associated NotPayedmoneyHistoryPriceProductebytypes entities
            var notPayedmoneyHistoryPriceProductebytypesItems = _context.FinancialUserCashHistoryPriceProductebytypes
                .Where(i => notPayedmoneyHistoryItems.Select(h => h.Id).Contains(i.financialUserCashHistoryid))
                .ToList();

            // Remove associated NotPayedmoneyHistoryPriceProductebytypes entities
            if (notPayedmoneyHistoryPriceProductebytypesItems.Any())
            {
                _context.FinancialUserCashHistoryPriceProductebytypes.RemoveRange(notPayedmoneyHistoryPriceProductebytypesItems);
            }

            // Remove associated NotPayedmoneyHistory entities
            if (notPayedmoneyHistoryItems.Any())
            {
                _context.FinancialUserCashHistories.RemoveRange(notPayedmoneyHistoryItems);
            }

            // Remove the NotPayedmoney entity
            _context.FinancialUserCash.Remove(notPayedmoney);

            // Save all changes
            _context.SaveChanges();

            return true;
        }
        public bool Salesreturns(int id)
        {
            // Find the NotPayedmoney entity by id
            var FinancialUserCash = _context.FinancialUserCash.Find(id);
            if (FinancialUserCash == null)
            {
                return false; // or throw an exception
            }

            // Find the associated NotPayedmoneyHistory entities
            var FinancialUserCashHistories = _context.FinancialUserCashHistories
                .Where(i => i.FinancialUserCashId == id)
                .ToList();

            var financialUserCashHistoryPriceProductebytypes = _context.FinancialUserCashHistoryPriceProductebytypes
                .Include(pp => pp.PriceProductebytypes)
                    .ThenInclude(p => p.Product)
                .Where(i => FinancialUserCashHistories.Select(h => h.Id).Contains(i.financialUserCashHistoryid))
                .ToList();

            // Update the quantity of associated products
            foreach (var historyItem in FinancialUserCashHistories)
            {
                var productItems = financialUserCashHistoryPriceProductebytypes
                    .Where(pp => pp.financialUserCashHistoryid == historyItem.Id)
                    .Select(pp => pp.PriceProductebytypes?.Product) // Use ?. to prevent NullReferenceException
                    .Where(p => p != null) // Filter out null products
                    .ToList();

                foreach (var product in productItems)
                {
                    product.Qantity += historyItem.Qantity; // Ensure the property name is correct
                }
            }

            // Remove associated NotPayedmoneyHistoryPriceProductebytypes entities
            if (financialUserCashHistoryPriceProductebytypes.Any())
            {
                _context.FinancialUserCashHistoryPriceProductebytypes.RemoveRange(financialUserCashHistoryPriceProductebytypes);


                // Remove associated NotPayedmoneyHistory entities
                if (FinancialUserCashHistories.Any())
                {
                    _context.FinancialUserCashHistories.RemoveRange(FinancialUserCashHistories);
                }

                // Remove the NotPayedmoney entity
            }
            _context.FinancialUserCash.Remove(FinancialUserCash);

            // Save all changes
            
            _context.SaveChanges();

                return true;
            
        }

        public IPagedList<FinancialUserCashHistoryVM> SearchFinancialUserCashH(FinancialUserCashHistoryVM criteria)
        {
            var queryable = _context.FinancialUserCash
                .Where(i =>
                    i.PayedTotalAmount == criteria.payedAmount || criteria.payedAmount == 0 || criteria.payedAmount == null
                )
                .Select(i => new FinancialUserCashHistoryVM
                {
                    Id = i.Id,
                    HospitalaoOrprationtyp = _context.FinancialUserCashHistories
                        .Where(p => p.FinancialUserCashId == i.Id)
                        .Select(p => (Enumes.HospitalOroprationtyp?)p.HospitalOroprationtyp)
                        .FirstOrDefault() ?? default(Enumes.HospitalOroprationtyp),
                    CreationTime = i.CreationTime,
                    PaymentStatus = (Enumes.PaymentStatus)i.PaymentStatus,
                    PayedTotalAmount = i.PayedTotalAmount
                })
                .OrderBy(g => g.Id);

            // Provide a default value for PageNumber if it's null
            int pageNumber = criteria.PageNumber ?? 1;

            var pagedList = GetPagedData(queryable, pageNumber);

            return pagedList;
        

          


        }

        public bool DeleteFinancialUserCashHistories(int id, int payedTotalAmount, int frercahid, int productid)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Find the financial user cash history record
                    var financialUserCashHistory = _context.FinancialUserCashHistories.Find(id);
                    if (financialUserCashHistory == null)
                    {
                        throw new Exception("FinancialUserCashHistory not found");
                    }

                    // Find the associated FinancialUserCash record
                    var financialUserCash = _context.FinancialUserCash.Find(frercahid);
                    if (financialUserCash == null)
                    {
                        throw new Exception("FinancialUserCash not found");
                    }

                    // Update the PayedTotalAmount in FinancialUserCash
                    financialUserCash.PayedTotalAmount -= financialUserCashHistory. PayedAmount;

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
                    _context.FinancialUserCashHistories.Remove(financialUserCashHistory);

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


        public IPagedList<FinancialUserCashHistoryVM> SearchFinancialUserCashHistoryDetails(int id,int  ?pageNuber)
            {
            var queryable = _context.FinancialUserCashHistories.Where(i => i.FinancialUserCashId == id



                        ).Select(i => new FinancialUserCashHistoryVM
                        {

                            Id = i.Id,
                            HospitalaoOrprationtyp = (Enumes.HospitalOroprationtyp)i.HospitalOroprationtyp
                             ,
                            Frercahid = id,
                            ProductName = _context.FinancialUserCashHistoryPriceProductebytypes
    .FirstOrDefault(pp => pp.financialUserCashHistoryid == i.Id).PriceProductebytypes.Product.ProductName
  
   
     ,

                             Productid = _context.FinancialUserCashHistoryPriceProductebytypes
                .Where(pp => pp.financialUserCashHistoryid == i.Id)
                .Select(pp => 
                pp.PriceProductebytypes.Product.Id)
                .FirstOrDefault()  ,
                            
                            Pricforonproduct = _context.FinancialUserCashHistoryPriceProductebytypes
                .Where(pp => pp.financialUserCashHistoryid == i.Id)
                .Select(pp => 
                pp.PriceProductebytypes.price)
                .FirstOrDefault()  ,
                                 CreationTime = i.CreationTime,
                                   Qantity = i.Qantity,
                                PayedTotalAmount = (_context. FinancialUserCashHistoryPriceProductebytypes
                    .Where(pp => pp.financialUserCashHistoryid == i.Id)
                    .Select(pp => (
                    decimal?)pp.PriceProductebytypes.price)
                    .FirstOrDefault() ?? 0) * i.Qantity,
                                PaymentStatus = (Enumes.PaymentStatus)i.PaymentStatus,

 
                            }
                           ).OrderByDescending(i=>i.Id);

                // Provide a default value for PageNumber if it's null
                int pageNumber = pageNuber ?? 1;

                var pagedList = GetPagedData(queryable, pageNumber);

                return pagedList;
            }


        public bool SaveFinancialUserCashHistories(FinancialUserCashHistoryVM criteria)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Find the financial user cash history record
                    var updated = _context.FinancialUserCashHistories.Find(criteria.Id);
                    if (updated == null)
                    {
                        throw new Exception("FinancialUserCashHistory not found");
                    }

                    // Update the quantity
 
                    // Find the associated FinancialUserCash record
                    var financialUserCash = _context.FinancialUserCash.Find(criteria.Frercahid);
                    if (financialUserCash == null)
                    {
                        throw new Exception("FinancialUserCash not found");
                    }

                    // Adjust the PayedTotalAmount
                    financialUserCash.PayedTotalAmount -= updated.PayedAmount;
                    var newTotalPrice = criteria.Qantity * criteria.Pricforonproduct;
                    financialUserCash.PayedTotalAmount += newTotalPrice;

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


    }
}
