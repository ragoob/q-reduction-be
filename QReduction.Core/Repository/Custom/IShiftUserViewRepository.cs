using QReduction.Core.Domain;
using QReduction.Core.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QReduction.Core.Repository.Custom
{
    public interface IShiftUserViewRepository : IRepository<ShiftUserView>
    {
        IQueryable<ShiftUserView> ShiftUserPerDay(int UserId ,int BranchId , string CurrentTime);
    }
}
