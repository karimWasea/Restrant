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

        public IPagedList<FinancialUserCashHistoryVM> SearchFinancialUserCashH(FinancialUserCashHistoryVM criteria)
        {
            var queryable = _context.FinancialUserCashHistories
                .Include(i => i.FinancialUserCash).Where(i =>         (i.CreationTime == criteria.CreationTime || criteria.CreationTime == DateTime.MinValue)


                         
                       
                         ).Select(i => new FinancialUserCashHistoryVM
                         {


                             Id = i.Id,
                             HospitalaoOrprationtyp = (Enumes.HospitalOroprationtyp)i.HospitalOroprationtyp
                  ,
                              CreationTime = i.CreationTime,
                          
 
                              PaymentStatus = (Enumes.PaymentStatus)i.PaymentStatus,
                             PayedTotalAmount = _context.FinancialUserCash.FirstOrDefault(p => p.Id == i.FinancialUserCashId).PayedTotalAmount ?? 0,

                  

                         }
            ).OrderBy(g => g.Id);

            // Provide a default value for PageNumber if it's null
            int pageNumber = criteria.PageNumber ?? 1;

            var pagedList = GetPagedData(queryable, pageNumber);

            return pagedList;


        }

        public bool DeleteFinancialUserCashHistories(int id)
        {
            var queryable = _context.FinancialUserCashHistories.Find(id);
            var itemsToRemove = _context. FinancialUserCashHistoryPriceProductebytypes
         .Where(i => i.financialUserCashHistoryid == id)
         .ToList();

            if (itemsToRemove.Any())
            {
                _context.FinancialUserCashHistoryPriceProductebytypes.RemoveRange(itemsToRemove);
                _context.SaveChanges();
            }



            _context.FinancialUserCashHistories.Remove(queryable);


            return true;

        }

        public IPagedList<FinancialUserCashHistoryVM> SearchFinancialUserCashHistoryDetails(int id,int  ?pageNuber)
            {
                var queryable = _context.FinancialUserCashHistories.Where(i => i.FinancialUserCashId == id



                            ).Select(i => new FinancialUserCashHistoryVM
                            {
                                
                                Id = i.Id,
                                HospitalaoOrprationtyp = (Enumes.HospitalOroprationtyp)i.HospitalOroprationtyp
                                 ,
                                 CreationTime = i.CreationTime,
                                   Qantity = i.Qantity,
                                PayedTotalAmount = (_context. FinancialUserCashHistoryPriceProductebytypes
                    .Where(pp => pp.financialUserCashHistoryid == i.Id)
                    .Select(pp => (
                    decimal?)pp.PriceProductebytypes.price)
                    .FirstOrDefault() ?? 0) * i.Qantity,
                                PaymentStatus = (Enumes.PaymentStatus)i.PaymentStatus,

 
                            }
                           ).OrderBy(g => g.Id);

                // Provide a default value for PageNumber if it's null
                int pageNumber = pageNuber ?? 1;

                var pagedList = GetPagedData(queryable, pageNumber);

                return pagedList;
            }




        }
}
