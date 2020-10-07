using QReduction.Core.Domain;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QReduction.Core.Repository.Custom
{
    public interface IShiftQueueRepository : IRepository<ShiftQueue>
    {

        ShiftQueue GetNextQueueUser(ShiftQueue shiftQueue);

        void UpdateQueue(ShiftQueue shiftQueue);
        List<User> GetOrganizationUsers(int OrganizationId);
        ShiftQueue CancelAndNextQueueUser(ShiftQueue shiftQueue);
    }
}
