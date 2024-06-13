using AutoMapper;

using Cf_Viewmodels;

using DataAcessLayers;

using Interfaces;

using Microsoft.AspNetCore.Identity;
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
                    price = i.price

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
                Catid = (CategoryType)p.catid 



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
                Catid = (CategoryType)p.catid,
                 NotpayedUserid=p.NotpayedUserid,
                    ClientName = _user.Users.Where(i=>i.Id==p.NotpayedUserid).FirstOrDefault().UserName


                }).ToList() ;
      
            return query;
        }
        #region AddShopingCaterCashHistory
        public void AddShopingCaterCashHistory(PriceProductebytypesVM criteria)
        {
             var Entity = new ShopingCaterCashHistory
            {
                 TotalAmount = criteria.totalprice,
                PriceProductebytypesId = criteria.Id,
                Qantity = criteria.ShopingCaterQantity,
                  productName= criteria.ProductName,
               catid= (int)criteria.Catid,    
             
             };

            _context.Add(Entity);
            _context.SaveChanges();

        }  
        
        public void AddShopingCaterNotpayedHistory(PriceProductebytypesVM criteria)
        {
            var result = criteria.HospitalOroprationtypId == 0 ? HospitalOroprationtyp.oprationtyp : criteria.HospitalOroprationtypId;
             var Entity = new ShopingCaterNotpayedHistory
            {
                 TotalAmount = criteria.totalprice,
                PriceProductebytypesId = criteria.Id,
                Qantity = criteria.ShopingCaterQantity,
                  productName= criteria.ProductName,
               catid= (int)criteria.Catid,    
               NotpayedUserid= criteria.NotpayedUserid,    
                HospitalaoOrprationtyp= (int)result,

             };

            _context.Add(Entity);
            _context.SaveChanges();

        }
        public void UpdateShopingCaterNotpayedHistory(PriceProductebytypesVM criteria)
        {
            var result = _context.ShopingCaterNotpayedHistory.Find(criteria.ShopingCaterId);

            var product = _context.PriceProductebytypes.Include(p => p.Product).Where(i => i.Id == criteria.Id).FirstOrDefault();

       
            result.Qantity =criteria.ShopingCaterQantity;
            result.TotalAmount = product.price * criteria.ShopingCaterQantity;














            _context.Update(result);
            _context.SaveChanges();
        }
        public void UpdateShopingCaterCashHistory(PriceProductebytypesVM criteria)
        {

            var result = _context.ShopingCaterNotpayedHistory.Find(criteria.ShopingCaterId);

            var product = _context.PriceProductebytypes.Include(p => p.Product).Where(i => i.Id == criteria.Id).FirstOrDefault();


            result.Qantity = criteria.ShopingCaterQantity;
            result.TotalAmount = product.price * criteria.ShopingCaterQantity;
            _context.Update(result);
            _context.SaveChanges();

        }



        public void DeleteShopingCaterCashHistory(int id)
        {

            //var product = _context.PriceProductebytypes.Include(p => p.Product).Where(i => i.Id == criteria.Id).FirstOrDefault();

            ////if (criteria.ShopingCaterQantity > result.Qantity && criteria.ShopingCaterQantity<= product.Qantity)
            ////{
            ////    product.Qantity
            ////         -= criteria.ShopingCaterQantity;
            ////}
            ////if (criteria.ShopingCaterQantity < result.Qantity)
            ////{
            ////    product.Qantity += criteria.ShopingCaterQantity;


            ////}

            _context.Remove(_context.ShopingCaterCashHistory.Find(id));
            _context.SaveChanges();

        } 
        public void DeleteShopingCaterNotpayedHistory(int id)
        {


            //var product = _context.PriceProductebytypes.Include(p => p.Product).Where(i => i.Id == criteria.Id).FirstOrDefault();

            ////if (criteria.ShopingCaterQantity > result.Qantity && criteria.ShopingCaterQantity<= product.Qantity)
            ////{
            ////    product.Qantity
            ////         -= criteria.ShopingCaterQantity;
            ////}
            ////if (criteria.ShopingCaterQantity < result.Qantity)
            ////{
            ////    product.Qantity += criteria.ShopingCaterQantity;


            ////}
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
                        CreationTime = DateTime.Now,

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
                            CreationTime = DateTime.Now,

                        };

                        var addedHistoryCash = _context.Add(historyCash);
                        _context.SaveChanges();

                        var historyPriceProduct = new FinancialUserCashHistoryPriceProductebytypes
                        {
                            financialUserCashHistoryid = addedHistoryCash.Entity.Id,
                            PriceProductebytypesid = item.PriceProductebytypesId
                        };

                        _context.Add(historyPriceProduct);
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
                    var newNotpayedHistory = _context.ShopingCaterNotpayedHistory.ToList();
                    var totalAmount = newNotpayedHistory.Sum(i => i.TotalAmount);

                    var NotPayedmoney = new NotPayedmoney
                    {
                         TotalNotpayedAmount = totalAmount,
                        SystemUserId = SystemUserId,
                        SystemUserName = SystemUserName,
                        ChangedByUserId = SystemUserId,
                         CreationTime = DateTime.Now,
                        PaymentStatus = (int)PaymentStatus.NotPaid
                        
                    };

                    var ADDNotPayedmoney = _context.Add(NotPayedmoney);
                    _context.SaveChanges();

                    var ADDNotPayedmoneyId = ADDNotPayedmoney.Entity.Id;

                    foreach (var item in newNotpayedHistory)
                    {
                         var historyCash = new NotPayedmoneyHistory
                        {
                             NotPayedmoneyId = ADDNotPayedmoneyId,
                            SystemUserId = SystemUserId ?? "",
                            SystemUserName = SystemUserName ?? "",
                             NotpayedAmount= item.TotalAmount,
                             UserNotPayedmoneyId= item.NotpayedUserid??"",
                            PaymentStatus = (int)PaymentStatus.NotPaid,
                            ishospital = item.ishospital,
                             Qantity= item.Qantity,
                         };

                        var ADDNotPayedmoneyIdHistoryCash = _context.Add(historyCash);
                        _context.SaveChanges();

                        var historyPriceProduct = new NotPayedmoneyHistoryPriceProductebytypes
                        {
                            NotPayedmoneyHistoryid = ADDNotPayedmoneyIdHistoryCash.Entity.Id,
                            PriceProductebytypesid = item.PriceProductebytypesId
                        };

                        _context.Add(historyPriceProduct);
                        _context.SaveChanges();

                    }

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

      
    }
}
