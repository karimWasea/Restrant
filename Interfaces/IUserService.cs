using Cf_Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using X.PagedList;

namespace Interfaces
{
    public interface IUserService : IPaginationHelper<ApplicaionuserVM>
    {
        Task<IPagedList<ApplicaionuserVM>> Search(ApplicaionuserVM ApplicaionuserVM);
        Task<ApplicaionuserVM> GetByIdAsync(string id);
        Task<ApplicaionuserVM> CreateAsync(ApplicaionuserVM user);
        Task<ApplicaionuserVM> UpdateAsync(ApplicaionuserVM user);
        Task<bool> DeleteAsync(string id);
    }
}
