using QReduction.Core.Domain;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Service.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Service.Custom
{
   public interface IShiftQueueService : IService<ShiftQueue>
    {

        ShiftQueue GetNextQueueUser(ShiftQueue shiftQueue);

        void UpdateQueue(ShiftQueue shiftQueue);

        List<User> GetOrganizationUsers(int OrganizationId);

        ShiftQueue CancelAndNextQueueUser(ShiftQueue shiftQueue);

    }
}
