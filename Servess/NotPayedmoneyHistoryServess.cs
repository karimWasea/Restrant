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


        public IPagedList<NotPayedmoneyHistoryVM> SearchNotPayedmoney(NotPayedmoneyHistoryVM criteria)
        {
           var queryable =  _context.NotPayedmoneyHistory.Include(i => i.NotPayedmoneys).Include(i => i.UserNotPayedmoney).Where(i => (i.PaymentStatus == (int)criteria.PaymentStatus || criteria.PaymentStatus == 0)

             && (criteria.UserNotPayedmoneyName == null || i.UserNotPayedmoney.UserName.Contains(criteria.UserNotPayedmoneyName)) && 
             (i.PaymentStatus == (int)criteria.PaymentStatus || criteria.PaymentStatus == 0)

             ).Select(i => new NotPayedmoneyHistoryVM
             {

                 Id = i.Id,
                 HospitalaoOrprationtyp = (Enumes.HospitalOroprationtyp)i.HospitalaoOrprationtyp
                  ,
                 UserNotPayedmoneyName = i.UserNotPayedmoney.UserName ??"",
                  CreationTime = i.CreationTime,
                 NotpayedAmount = i.NotpayedAmount,
                 ChangedByUserId = i.ChangedByUserId,
                 payedAmount = i.payedAmount,

                 ishospital = i.ishospital,
                 NotPayedmoneyId = i.NotPayedmoneyId,
                 PaymentStatus = (Enumes.PaymentStatus)i.PaymentStatus,
                 TotalNotpayedAmount = _context.NotPayedmoney.FirstOrDefault(p=>p.Id==i.NotPayedmoneyId).TotalNotpayedAmount??0,
                 TotalPayedAmount = _context.NotPayedmoney.FirstOrDefault(p=>p.Id==i.NotPayedmoneyId).TotalPayedAmount??0,
                 
               
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

        public bool DeleteNotPayedmoneyHistory(int id)
        {
          var   queryable = _context.NotPayedmoneyHistory.Find( id);
            var itemsToRemove = _context.NotPayedmoneyHistoryPriceProductebytypes
         .Where(i => i.NotPayedmoneyHistoryid == id)
         .ToList();

            if (itemsToRemove.Any())
            {
                _context.NotPayedmoneyHistoryPriceProductebytypes.RemoveRange(itemsToRemove);
                _context.SaveChanges();
            }



            _context.NotPayedmoneyHistory.Remove(queryable);

            
            return true;

        }
 
     


        public bool SaveNotPayedmoneyHistory(NotPayedmoneyHistoryVM criteria)
        {

            var updated =  _context.NotPayedmoneyHistory.Find(criteria.Id);
            updated.Qantity= criteria.Qantity;
            _context.SaveChanges();
             return true;   
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
