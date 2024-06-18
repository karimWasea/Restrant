using AutoMapper;
using Cf_Viewmodels;

using DataAcessLayers;

using Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using X.PagedList;

using static C_Utilities.Enumes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Servess
{
    public class PriceProductebytypesServess : PaginationHelper<PriceProductebytypesVM>, IPriceProductebytypes
    {
        public readonly ApplicationDBcontext _context;

        private readonly IMapper _mapper;
        private readonly UserManager<Applicaionuser> _user;


        
        public PriceProductebytypesServess(ApplicationDBcontext context, IMapper mapper , UserManager<Applicaionuser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _user = userManager;

        }

        public bool CheckIfExisit(PriceProductebytypesVM entity)
        {
            return _context.PriceProductebytypes.Any(i => i.Id != entity.Id && i.ProductId == entity.ProductId

            && i.CustomerType == entity.CustomerType && i.price == entity.price);
        }

        public void Save(PriceProductebytypesVM criteria)
        {
            var Entity = _mapper.Map<DataAcessLayers.PriceProductebytypes>(criteria);
            if (criteria.Id > 0)
            {
                _context.Update(Entity);

            }
            {

                _context.Add(Entity);

            }
            _context.SaveChanges();

        }
        public void Delete(int id)
        {
            _context.Remove(_context.PriceProductebytypes.Find(id));
            _context.SaveChanges();

        }

        public PriceProductebytypesVM Get(int id)
        {


            return _mapper.Map<PriceProductebytypesVM>(_context.PriceProductebytypes.Find(id));



        }

        public IPagedList<PriceProductebytypesVM> Search(PriceProductebytypesVM criteria)
        {
            var queryable = _context.PriceProductebytypes.Include(i => i.Product).Where(
                product =>
                    (criteria.ProductId == 0 || product.ProductId == criteria.ProductId)
                    && (criteria.CustomerType == 0 || product.CustomerType == criteria.CustomerType))
                .Select(i => new PriceProductebytypesVM
                {
                    ProductName = i.Product.ProductName,
                    CustomerType = i.CustomerType,
                    ProductId = i.ProductId,
                    Id = i.Id,
                    Discount = i.Discount,
                    Qantity = i.Qantity,
                    price = i.price ,
 
                })
                .OrderBy(g => g.Id);

            // Provide a default value for PageNumber if it's null
            int pageNumber = criteria.PageNumber ?? 1;

            var pagedList = GetPagedData(queryable, pageNumber);

            return pagedList;
        }

        public IPagedList<PriceProductebytypesVM> SearchForTypes(PriceProductebytypesVM searchCriteria)
        {
            var query = _context.PriceProductebytypes
                 .Include(i => i.Product)
                   .ThenInclude(i => i.ProductAttachment)
               .Where(product =>
                   (product.CustomerType == searchCriteria.CustomerType) &&
                   (searchCriteria.Catid == 0 || product.Product.CategoryTyPe == (int)searchCriteria.Catid) &&
                   (searchCriteria.ProductName == null || product.Product.ProductName.Contains(searchCriteria.ProductName)))
               .Select(i => new PriceProductebytypesVM
               {
                   ProductName = i.Product.ProductName??"",
                   CustomerType = searchCriteria.CustomerType,
                   ProductId = i.ProductId,
                   Id = i.Id,
                   Discount = i.Discount,
                   Qantity = i.Qantity,
                   price = i.price,
                   Catid = (CategoryType)i.Product.CategoryTyPe,

               })
               .OrderBy(g => g.Id);

            int pageNumber = searchCriteria.PageNumber ?? 1;
            var data = GetPagedData(query, pageNumber);
            return data;
        }







        public IEnumerable<PriceProductebytypesVM> GetallfromShopingCart(PriceProductebytypesVM criteria)
        {
            var query = _context.ShopingCaterCashHistory.Select(p => new PriceProductebytypesVM {
                Id = p.PriceProductebytypesId,
                totalprice = p.TotalAmount,
                ShopingCaterQantity = p.Qantity,
                ShopingCaterId = p.Id,
                ProductName = p.productName,
                Catid = (CategoryType)p.catid,
                                    ProductId = p.productid,



            }).ToList() ;
      
            return query;
        }  
        
        public IEnumerable<PriceProductebytypesVM> GetallfromShopingCartNopayed(PriceProductebytypesVM criteria)
        {
            var query = _context.ShopingCaterNotpayedHistory
                .Select(p => new PriceProductebytypesVM {
                Id = p.PriceProductebytypesId,
                totalprice = p.TotalAmount,
                ShopingCaterQantity = p.Qantity,
                    ShopingCaterId = p.Id,
                ProductName = p.productName,
                    ProductId = p.productid,
                Catid = (CategoryType)p.catid,
                 NotpayedUserid=p.NotpayedUserid,
                    ClientName = _user.Users.Where(i=>i.Id==p.NotpayedUserid).FirstOrDefault().UserName


                }).ToList() ;
      
            return query;
        }
        #region AddShopingCaterCashHistory
        public void AddShopingCaterCashHistory(PriceProductebytypesVM criteria)
        {
            
            var Entity = new ShopingCaterCashHistory();
            Entity.TotalAmount = criteria.totalprice;
            Entity.PriceProductebytypesId = criteria.Id;
            Entity.Qantity = criteria.ShopingCaterQantity;
            Entity.productName = criteria.ProductName;
            Entity.catid = (int)criteria.Catid;    
            Entity.productid = criteria.ProductId;    
             
 
            _context.Add(Entity);
            _context.SaveChanges();

        }  
        
        public void AddShopingCaterNotpayedHistory(PriceProductebytypesVM criteria)
        {
            var result = criteria.HospitalOroprationtypId == 0 ? HospitalOroprationtyp.oprationtyp : criteria.HospitalOroprationtypId;
       

            var Entity = new ShopingCaterNotpayedHistory();
            
                Entity.TotalAmount = criteria.totalprice;
            Entity.PriceProductebytypesId = criteria.Id;
            Entity.Qantity = criteria.ShopingCaterQantity;
             Entity.productName = criteria.ProductName;
            Entity.catid = (int)criteria.Catid;
            Entity.productid = criteria.ProductId;

            if (result == HospitalOroprationtyp.Hospital)
            {
                var roloid = _context.Roles.FirstOrDefault(i => i.Name == C_Utilities.ConstsntValuse.SuperAdmin).Id ?? "";
                var hospiyaid =
                _context.UserRoles.FirstOrDefault(i => i.RoleId == roloid).UserId;
                Entity.NotpayedUserid = hospiyaid;
            }
            Entity.NotpayedUserid = criteria.NotpayedUserid ;
            Entity.HospitalaoOrprationtyp = (int)result;

             

            _context.Add(Entity);
            _context.SaveChanges();

        }
        public void UpdateShopingCaterNotpayedHistory(PriceProductebytypesVM criteria)
        {
            var result = _context.ShopingCaterNotpayedHistory.Find(criteria.ShopingCaterId);

            var product = _context.PriceProductebytypes.Include(p => p.Product).Where(i => i.Id == result.PriceProductebytypesId).FirstOrDefault();

       
            result.Qantity =criteria.ShopingCaterQantity;
            result.TotalAmount = product.price * criteria.ShopingCaterQantity;














            _context.Update(result);
            _context.SaveChanges();
        }
        public void UpdateShopingCaterCashHistory(PriceProductebytypesVM criteria)
        {

            var result = _context.ShopingCaterCashHistory.Find(criteria.ShopingCaterId);

            var product = _context.PriceProductebytypes.Include(p => p.Product).Where(i => i.Id == result.PriceProductebytypesId).FirstOrDefault();


            result.Qantity = criteria.ShopingCaterQantity;
            result.TotalAmount = product.price * criteria.ShopingCaterQantity;
            _context.Update(result);
            _context.SaveChanges();

        }



        public void DeleteShopingCaterCashHistory(int id)
        {

           

            _context.Remove(_context.ShopingCaterCashHistory.Find(id));
            _context.SaveChanges();

        } 
        public void DeleteShopingCaterNotpayedHistory(int id)
        {
 
            _context.Remove(_context.ShopingCaterNotpayedHistory.Find(id));
            _context.SaveChanges();

        }






        #endregion
        public void FreeShopingCaterCashHistoryToFinancialUserCash(string? SystemUserId, string? SystemUserName)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var newCashHistory = _context.ShopingCaterCashHistory.ToList();
                    var totalAmount = newCashHistory.Sum(i => i.TotalAmount);

                    var financialUserCash = new FinancialUserCash
                    {
                        PayedTotalAmount = totalAmount,
                        SystemUserId = SystemUserId,
                        SystemUserName = SystemUserName,
                        PaymentStatus = (int)PaymentStatus.Paid ,
 
                    };

                    var addFinancialUserCash = _context.Add(financialUserCash);
                    _context.SaveChanges();

                    var financialUserCashId = addFinancialUserCash.Entity.Id;
                     
                    foreach (var item in newCashHistory)
                    {
                        var historyCash = new FinancialUserCashHistory
                        {
                            Qantity = item.Qantity,
                            FinancialUserCashId = financialUserCashId,
                            SystemUserId = SystemUserId ?? "",
                            SystemUserName = SystemUserName ?? "",
                            PaymentStatus = (int)PaymentStatus.Paid,
                            PayedAmount = item.TotalAmount,
                             

                        };

                      
                        var addedHistoryCash = _context.Add(historyCash);
                        _context.SaveChanges();

                        var historyPriceProduct = new FinancialUserCashHistoryPriceProductebytypes
                        {
                            financialUserCashHistoryid = addedHistoryCash.Entity.Id,
                            PriceProductebytypesid = item.PriceProductebytypesId
                        };

                        _context.Add(historyPriceProduct);
                        // Decreasing the quantity of the product
                        var product = _context.products.FirstOrDefault(x => x.Id == item.productid);
                        if (product != null)
                        {
                            product.Qantity -= item.Qantity;
                        }
                        _context.SaveChanges();

                    }

                    _context.ShopingCaterCashHistory.RemoveRange(newCashHistory);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Log the original exception for debugging
                    // Optionally, rethrow the original exception or handle it as needed
                    throw;
                }
            }
        }




        public void Decreasmonyfromcashopration(int id, decimal payedTotalAmount)
        {
            try
            {
                var dec = _context.FinancialUserCash.Find (id);

                if (dec == null)
                {
                    throw new Exception($"FinancialUserCash with ID {id} not found.");
                }

                if (dec.PayedTotalAmount < payedTotalAmount)
                {
                    throw new InvalidOperationException($"Insufficient funds. The available amount is {dec.PayedTotalAmount}, but {payedTotalAmount} was requested.");
                }

                dec.PayedTotalAmount -= payedTotalAmount;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the error (not shown)
                // Optionally, handle the exception (e.g., by rethrowing it or converting it into a user-friendly message)
                throw new Exception("An error occurred while decreasing the money from the cash operation.", ex);
            }
        }

        public void Deletfromhistorycash(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var relatedObj = _context.FinancialUserCashHistoryPriceProductebytypes.FirstOrDefault(i => i.financialUserCashHistoryid == id);
                    if (relatedObj != null)
                    {
                        _context.Remove(relatedObj);
                    }
                    else
                    {
                        throw new Exception($"FinancialUserCashHistoryPriceProductebytypes with financialUserCashHistoryid {id} not found.");
                    }

                    var history = _context.FinancialUserCashHistories.Find(id);
                    if (history != null)
                    {
                        _context.Remove(history);
                    }
                    else
                    {
                        throw new Exception($"FinancialUserCashHistory with ID {id} not found.");
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Log the error (not shown)
                    // Optionally, handle the exception (e.g., by rethrowing it or converting it into a user-friendly message)
                    throw new Exception("An error occurred while deleting from history cash.", ex);
                }
            }
        }

        public void FreeShopingCaterCashHistoryToNotpayed(string? SystemUserId, string? SystemUserName)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Fetching all ShopingCaterNotpayedHistory items
                    var newNotpayedHistory = _context.ShopingCaterNotpayedHistory.ToList();
                    var totalAmount = newNotpayedHistory.Sum(i => i.TotalAmount);

                    // Creating a new NotPayedmoney entry
                    var notPayedmoney = new NotPayedmoney
                    {
                        TotalNotpayedAmount = totalAmount,
                        SystemUserId = SystemUserId,
                        SystemUserName = SystemUserName,
                        ChangedByUserId = SystemUserId,
                        CreationTime = DateTime.Now,
                        PaymentStatus = (int)PaymentStatus.NotPaid
                    };

                    _context.NotPayedmoney.Add(notPayedmoney);
                    _context.SaveChanges();
                    var notPayedmoneyId = notPayedmoney.Id;

                    foreach (var item in newNotpayedHistory)
                    {
                        // Creating a new NotPayedmoneyHistory entry
                        var historyCash = new NotPayedmoneyHistory
                        {
                            NotPayedmoneyId = notPayedmoneyId,
                            SystemUserId = SystemUserId ?? "",
                            SystemUserName = SystemUserName ?? "",
                            NotpayedAmount = item.TotalAmount,
                            UserNotPayedmoneyId = item.NotpayedUserid,
                            PaymentStatus = (int)PaymentStatus.NotPaid,
                            ishospital = item.ishospital,
                            Qantity = item.Qantity,
                            ChangeDate = DateTime.Now.Date,
                            HospitalaoOrprationtyp = item.HospitalaoOrprationtyp
                        };

                        _context.NotPayedmoneyHistory.Add(historyCash);
                        _context.SaveChanges();
                        var historyCashId = historyCash.Id;

                        // Creating a new NotPayedmoneyHistoryPriceProductebytypes entry
                        var historyPriceProduct = new NotPayedmoneyHistoryPriceProductebytypes
                        {
                            NotPayedmoneyHistoryid = historyCashId,
                            PriceProductebytypesid = item.PriceProductebytypesId
                        };

                        _context.NotPayedmoneyHistoryPriceProductebytypes.Add(historyPriceProduct);

                        // Decreasing the quantity of the product
                        var product = _context.products .FirstOrDefault(x => x.Id == item.productid);
                        if (product != null)
                        {
                            product.Qantity -= item.Qantity;
                        }

                        _context.SaveChanges();
                    }

                    // Removing all processed ShopingCaterNotpayedHistory entries
                    _context.ShopingCaterNotpayedHistory.RemoveRange(newNotpayedHistory);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Log the original exception for debugging
                    // Optionally, rethrow the original exception or handle it as needed
                    throw;
                }
            }
        }

        #region EndDibts
        public bool EnDDebite(PriceProductebytypesVM entity)
        {
            try
            {
                // Get all NotPayedmoneyHistory entries for the given user
                var deptiforone = _context.NotPayedmoneyHistory
                    .Include(i => i.NotPayedmoneys)
                    .Where(u => u.UserNotPayedmoneyId == entity.NotpayedUserid &&u.PaymentStatus== (int)PaymentStatus.NotPaid)
                    .ToList();

                if (!deptiforone.Any())
                {
                    return false; // No records found to update
                }

                // Begin transaction to ensure atomicity
                using (var transaction = _context.Database.BeginTransaction())
                {
                    foreach (var item in deptiforone)
                    {
                        // Update individual NotPayedmoneyHistory entry
                        item.payedAmount = item.NotpayedAmount.Value;
                        item.PaymentStatus = (int)PaymentStatus.Paid;

                        // Update related NotPayedmoneys entry
                        item.NotPayedmoneys.TotalPayedAmount = item.NotPayedmoneys.TotalNotpayedAmount;
                        item.NotPayedmoneys.PaymentStatus = (int)PaymentStatus.Paid;
                    }

                    // Save changes to the database
                    _context.SaveChanges();

                    // Commit the transaction
                    transaction.Commit();
                }

                return true; // Successfully updated
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as per your application needs)
                // Example: _logger.LogError(ex, "An error occurred while processing payments.");
                return false; // Indicate failure
            }
        }

        public PriceProductebytypesVM GetDibts(PriceProductebytypesVM entity)
        {
            try
            {
                  if(!string.IsNullOrEmpty(entity.NotpayedUserid))
                {
                    entity.totalDibte = _context.NotPayedmoneyHistory
                                   .Where(u => (u.UserNotPayedmoneyId == entity.NotpayedUserid || entity.NotpayedUserid == null)
                                               && (u.PaymentStatus == (int)PaymentStatus.NotPaid || u.PaymentStatus == 0))
                                   .Sum(i => (decimal?)i.NotpayedAmount) ?? 0;
                    entity.NotpayedUserName = _user.Users.FirstOrDefault(p => p.Id == entity.NotpayedUserid).UserName ?? "";
                    return entity;

                }
                return entity;

            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as per your application needs)
                // Example: _logger.LogError(ex, "An error occurred while calculating user debts.");
                return null; // Indicate failure
            }
        }














        public bool EnDDebiteHospital( )
        {
            try
            {
                // Get all NotPayedmoneyHistory entries for the given user
                var deptiforone = _context.NotPayedmoneyHistory
                    .Include(i => i.NotPayedmoneys)
                    .Where(u => u.HospitalaoOrprationtyp ==   (int)HospitalOroprationtyp.Hospital && u.PaymentStatus == (int)PaymentStatus.NotPaid)
                    .ToList();

                if (!deptiforone.Any())
                {
                    return false; // No records found to update
                }

                // Begin transaction to ensure atomicity
                using (var transaction = _context.Database.BeginTransaction())
                {
                    foreach (var item in deptiforone)
                    {
                        // Update individual NotPayedmoneyHistory entry
                        item.payedAmount = item.NotpayedAmount.Value;
                        item.PaymentStatus = (int)PaymentStatus.Paid;

                        // Update related NotPayedmoneys entry
                        item.NotPayedmoneys.TotalPayedAmount = item.NotPayedmoneys.TotalNotpayedAmount;
                        item.NotPayedmoneys.PaymentStatus = (int)PaymentStatus.Paid;
                    }

                    // Save changes to the database
                    _context.SaveChanges();

                    // Commit the transaction
                    transaction.Commit();
                }

                return true; // Successfully updated
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as per your application needs)
                // Example: _logger.LogError(ex, "An error occurred while processing payments.");
                return false; // Indicate failure
            }
        }


        public PriceProductebytypesVM GetDibtsHospital()
        {
            var entity = new PriceProductebytypesVM();
            try
            {
                entity.totalDibtehospital = _context.NotPayedmoneyHistory
                    .Where(u => u.HospitalaoOrprationtyp == (int)HospitalOroprationtyp.Hospital
                                && u.PaymentStatus == (int)PaymentStatus.NotPaid)
                    .Sum(i => (decimal?)i.NotpayedAmount) ?? 0;

                return entity;
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as per your application needs)
                // Example: _logger.LogError(ex, "An error occurred while calculating hospital debts.");
                return null; // Indicate failure
            }
        }









        #endregion
    }
}
