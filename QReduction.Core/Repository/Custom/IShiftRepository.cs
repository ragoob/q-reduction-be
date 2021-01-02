using QReduction.Core.Domain;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QReduction.Core.Repository.Custom
{
   public interface IShiftRepository :  IRepository<Shift>
    {
        IQueryable<Shift> GetBranchOpenShiftIds(int BranchId, string CurrentTime);
    }
}
