//using AutoMapper;

//using Cf_Viewmodels;

//using DataAcessLayers;
//using DataAcessLayers.Migrations;

//using Interfaces;

//using Microsoft.EntityFrameworkCore;

//using X.PagedList;

//namespace Servess
//{
//    public class ICustomerTypeServess :  PaginationHelper<CustomerTypeVm>, ICustomerType
//    {
//        public readonly ApplicationDBcontext _context;

//        private readonly IMapper _mapper;
        
//        public ICustomerTypeServess(ApplicationDBcontext context , IMapper mapper)  
//        {
//            _context = context;
//            _mapper = mapper;

//        }
//        public bool CheckIfExisit(CustomerTypeVm entity)
//        {
//         return   _context.CustomerTypes.Any(i => i.Id != entity.Id && i.Types == entity.Types);
//                }

      

//        public void Delete(int id)
//        {
//             _context.Remove(_context.CustomerTypes.Find(id));
//            _context.SaveChanges();


//        }

//        public CustomerTypeVm Get(int id)
//        {


//            return _mapper.Map<CustomerTypeVm>(_context.CustomerTypes.Find(id));

          
        
//        }

//        public IPagedList<CustomerTypeVm> GetAll()
//        {
//            var CustomerTypeVm= _context.CustomerTypes.Select(i => new CustomerTypeVm {
            
//               Types = i.Types,
//                Id = i.Id,
            
            
//            });  
//          return   GetPagedData<CustomerTypeVm>(CustomerTypeVm);
//         }

//        public IPagedList<CustomerTypeVm> GetAll(int pageNumber, int pageSize)
//        {
//            throw new NotImplementedException();
//        }

//        public void Save(CustomerTypeVm entity)
//        {
//           if(entity.Id > 0) {

//                //_context.CustomerTypes.Update(entity);
                    
                    
                    
//                    } else {  }
//            _context.SaveChanges();
//        }

       

//        public IPagedList<CustomerTypeVm> Search(CustomerTypeVm criteria)
//        {
//            var CustomerTypeVm = _context.CustomerTypes.Where(i=>i.Types==null||i.Types==0||i.Types== criteria.Types).Select(i => new CustomerTypeVm
//            {

//                Types = i.Types,
//                Id = i.Id,


//            });
//            return GetPagedData<CustomerTypeVm>(CustomerTypeVm, pageNumber: (int)criteria.PageNumber);
//        }

      

       





//        //public IPagedList<RoomlDto> Seach(RoomSM DtoSearch)
//        //{
//        //    var Qarable =


//        //        _context.Rooms.Include(i => i.Department).Where(
//        //        Departments =>

//        //        (DtoSearch.Id == 0 || DtoSearch.Id == null || Departments.Id == DtoSearch.Id)


//        //              && (DtoSearch.DepartmentName == null || Departments.Department.DepartmentName.Contains(DtoSearch.DepartmentName))

//        //              &&
//        //              (DtoSearch.Building == null || Departments.Building.Contains(DtoSearch.DepartmentName)) &&
//        //              (DtoSearch.RoomNumber == null || Departments.RoomNumber.Contains(DtoSearch.RoomNumber))


//        //              )
//        //        .Select(Departments => new RoomlDto
//        //        {
//        //            Id = Departments.Id,
//        //            DepartmentName = Departments.Department.DepartmentName,
//        //            Building = DtoSearch.Building,
//        //            RoomNumber = Departments.RoomNumber,
//        //            Capacity = Departments.Capacity,
//        //            DepartmentId = Departments.DepartmentId



//        //        }).OrderBy(g => g.Id);
//        //    var IPagedList = GetPagedData(Qarable, DtoSearch.PageNumber);

//        //    return IPagedList;
//        //}









//    }
//}
