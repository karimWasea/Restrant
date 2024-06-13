using DataAcessLayers;

using Interfaces;
using System.Runtime.InteropServices.JavaScript;

namespace Servess
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        public ICategory _Category { get; set; }
        public IProduct _Product { get; set; }
 
        //public ICustomerType _CustomerType { get; }
        public Ilookup _Ilookup { get; }
        public IUserService _userService { get; }
        public IPriceProductebytypes _PriceProductebytypes { get; }
        public INotPayedmoneyHistoryServess _NotPayedmoneyHistoryServess { get; }

        public readonly ApplicationDBcontext _context;

        public UnitOfWork(

            CategoryServess categoryServess,

            ApplicationDBcontext context, ProductService productService,  lookupServess lookupServess , PriceProductebytypesServess priceProductebytypesServess , ApplicationUserService applicationUserService , NotPayedmoneyHistoryServess notPayedmoneyHistoryServess
            )
        {_NotPayedmoneyHistoryServess = notPayedmoneyHistoryServess;    
            _PriceProductebytypes= priceProductebytypesServess; 
             _context = context;
            _Category = categoryServess;
            _Product = productService;
            _Ilookup = lookupServess;
            _userService = applicationUserService;  




        }

        #region Implement the Dispose method to release resources


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }




        // Implement the finalizer to release unmanaged resources
        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion

















    }

}
