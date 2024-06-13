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
        Imgoperation _Imgoperation;
        private object forech;
        private readonly IMapper _mapper;

        public NotPayedmoneyHistoryServess(ApplicationDBcontext context, IMapper mapper    ,     Imgoperation imgoperation
)
        {
            _Imgoperation = imgoperation;
            _context = context;
            _mapper = mapper;

        }

        public bool CheckIfExisit(productVM entity)
        {
            return _context.products.Any(i => i.Id != entity.Id && i.ProductName == entity.ProductName &&i.Price==entity.Price);
        }

        public bool SaveNotPayedmoney(NotPayedmoneyHistoryVM criteria)
        {
            var ispayed = false;

            var queryable = _context.NotPayedmoney.FirstOrDefault(i=>i.Id==criteria.Id); 
             if(queryable.TotalNotpayedAmount == queryable.TotalPayedAmount)
            {
                return  true; 

            }

            //queryable.TotalNotpayedAmount -= criteria.payedAmount;
            //queryable.TotalPayedAmount += criteria.payedAmount;
            if (queryable.TotalNotpayedAmount == queryable.TotalPayedAmount)
            {
                queryable.PaymentStatus = (int)Enumes.PaymentStatus.Paid;

            }
            queryable.PaymentStatus = (int)Enumes.PaymentStatus.NotPaid;
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
           var queryable =  _context.NotPayedmoneyHistory.Include(i => i.UserNotPayedmoney).Include(i => i.NotPayedmoneys).Where(i => (i.PaymentStatus == criteria.PaymentStatus || criteria.PaymentStatus == 0)

             //&& (criteria.UserNotPayedmoneyName == null || i.UserNotPayedmoney.UserName.Contains(criteria.UserNotPayedmoneyName)) && (i.HospitalaoOrprationtyp == criteria.HospitalaoOrprationtyp || criteria.HospitalaoOrprationtyp == 0)

             ).Select(i => new NotPayedmoneyHistoryVM
             {

                 Id = i.Id,
                 HospitalaoOrprationtyp = i.HospitalaoOrprationtyp
                  ,
                 //UserNotPayedmoneyName = i.UserNotPayedmoney.UserName,
                 //ChangeDate = i.ChangeDate,
                 //ChangedByUserId = i.ChangedByUserId,
                 //CreationTime = i.CreationTime,
                  NotpayedAmount = i.NotpayedAmount,
                 ishospital = i.ishospital,
                 NotPayedmoneyId = i.NotPayedmoneyId,
                 PaymentStatus = i.PaymentStatus,
                 TotalNotpayedAmount = i.NotPayedmoneys.TotalPayedAmount,
                 TotalPayedAmount = i.NotPayedmoneys.TotalPayedAmount,
                 UserNotPayedmoneyId = i.UserNotPayedmoneyId,

             }
            ).OrderBy(g => g.Id);

            // Provide a default value for PageNumber if it's null
            int pageNumber = criteria.PageNumber ?? 1;

            var pagedList = GetPagedData(queryable, pageNumber);

            return pagedList;



        }



        public IPagedList<NotPayedmoneyHistoryVM> SaveNotPayedmoneyHistoryDetails(int id , int? pageNuber )
        {
            var queryable = _context.NotPayedmoneyHistory   .Include(i => i.UserNotPayedmoney).Where(i =>  i.NotPayedmoneyId==id

                       

                        ).Select(i => new NotPayedmoneyHistoryVM
                        {

                            Id = i.Id,
                            HospitalaoOrprationtyp = i.HospitalaoOrprationtyp
                             ,
                            //UserNotPayedmoneyName = i.UserNotPayedmoney.UserName,
                            //ChangeDate = i.ChangeDate,
                            //ChangedByUserId = i.ChangedByUserId,
                            //CreationTime = i.CreationTime,
                            //Description = i.Description,
                            NotpayedAmount = i.NotpayedAmount,
                            ishospital = i.ishospital,
                            NotPayedmoneyId = i.NotPayedmoneyId,
                            PaymentStatus = i.PaymentStatus,
                            
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
            throw new NotImplementedException();
        }

        public IPagedList<NotPayedmoneyHistoryVM> PrintforHospitallDay(int id)
        {
            throw new NotImplementedException();
        }
    }
}
