namespace Interfaces
{
    public interface IUnitOfWork : IDisposable

    {
          ICategory _Category { get;   }
        //ICustomerType _CustomerType { get; }
        Ilookup _Ilookup { get; }
          IProduct _Product { get;   }
          IPriceProductebytypes _PriceProductebytypes { get;   }
        public IUserService _userService { get; }

    }

}
