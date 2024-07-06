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
    public class ProductService : PaginationHelper<productVM>, IProduct
    {
        public readonly ApplicationDBcontext _context; 
        Imgoperation _Imgoperation;

        private readonly IMapper _mapper;

        public ProductService(ApplicationDBcontext context, IMapper mapper    ,     Imgoperation imgoperation
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


        public void Save(productVM criteria)
        {
            var Entity = _mapper.Map< DataAcessLayers.Product  >(criteria);
            if (criteria.Id > 0)
            {
              var updated =  _context.Update(Entity);
                _context.SaveChanges();
                //if (criteria.Cover == null)
                //{


                //    var path = criteria.CoverString;
                //    var ProductAttachment = new ProductAttachment();

                //    ProductAttachment.ProductId = updated.Entity.Id;
                //    ProductAttachment.FilePath = path;
                //    ProductAttachment.FileType = "Product";
                //    _context.Add(ProductAttachment);
 
                //}
                //else
                //{
                //    var path = _Imgoperation.UploadFile(criteria.Cover, "Product", Entity.ProductName);
                //     var ProductAttachment = new ProductAttachment();

                //    ProductAttachment.ProductId = updated.Entity.Id;
                //    ProductAttachment.FilePath = path;
                //    ProductAttachment.FileType = "Product";
                //    _context.Add(ProductAttachment);


                //}
                //_context.SaveChanges();


            }else
            {

               var add= _context.Add(Entity);
                _context.SaveChanges();

                //var path = _Imgoperation.UploadFile(criteria.Cover, "Product", Entity.ProductName);
                //var ProductAttachment = new ProductAttachment();

                //ProductAttachment.ProductId = add.Entity.Id;
                //ProductAttachment.FilePath = path;
                //ProductAttachment.FileType = "Product";
                //_context.Add(ProductAttachment);


            }
        



            _context.SaveChanges();

        }
        public void Delete(int id)
        {
            _context.Remove(_context.products.Find(id));
            _context.SaveChanges();

        }

        public productVM Get(int id)
        {


            return _mapper.Map<productVM>(_context.products.Find(id));

            

        }

        public IPagedList<productVM> Search(productVM criteria)
        {
            var queryable = _context.products.Where(
                product =>
                    (criteria.ProductName == null || product.ProductName.Contains(criteria.ProductName))
                    && (criteria.CategoryTyPe == null|| criteria.CategoryTyPe == 0 || product.CategoryTyPe== (int)criteria.CategoryTyPe))
                .Select(i => new productVM
                {
                    Id = i.Id,
                    ProductName = i.ProductName??"",
                    Description = i.Description,
                    Discount = i.Discount,
                    CategoryTyPe = (Enumes.CategoryType)i.CategoryTyPe,
                    Price = i.Price ?? 0.0m,
                    Qantity = i.Qantity ?? 0.0m,
 
                    //, CategoryName= i.Category.CategoryName,

                })
                .OrderBy(g => g.Id);

            // Provide a default value for PageNumber if it's null
            int pageNumber = criteria.PageNumber ?? 1;

            var pagedList = GetPagedData(queryable, pageNumber);

            return pagedList;
        }




    }
}
