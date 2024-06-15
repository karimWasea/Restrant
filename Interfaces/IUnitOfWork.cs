namespace Interfaces
{
    public interface IUnitOfWork : IDisposable

    {
          ICategory _Category { get;   }
         Ilookup _Ilookup { get; }
          IProduct _Product { get;   }
          IPriceProductebytypes _PriceProductebytypes { get;   }
        INotPayedmoneyHistoryServess _NotPayedmoneyHistoryServess { get;   }
        IFinancialUserCashHistoryServess _iFinancialUserCashHistoryServess { get;   }
        public IUserService _userService { get; }

    }

}
