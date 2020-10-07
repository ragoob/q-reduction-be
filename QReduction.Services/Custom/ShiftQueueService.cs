using QReduction.Core.Domain;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Repository.Custom;
using QReduction.Core.Repository.Generic;
using QReduction.Core.Service.Custom;
using QReduction.Infrastructure.UnitOfWorks;
using QReduction.Services.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Services.Custom
{
    public class ShiftQueueService : Service<ShiftQueue>, IShiftQueueService
    {

        private readonly IShiftQueueRepository _shiftQueueRepository;


        public ShiftQueueService(IQReductionUnitOfWork unitOfWork, IShiftQueueRepository shiftQueueRepository)
                : base(unitOfWork, shiftQueueRepository)
        {
            _shiftQueueRepository = shiftQueueRepository;


        }
        public ShiftQueue GetNextQueueUser(ShiftQueue shiftQueue)
        {
            var nextQueue= _shiftQueueRepository.GetNextQueueUser(shiftQueue);
            _unitOfWork.SaveChanges();
            return nextQueue;
        }
        public ShiftQueue CancelAndNextQueueUser(ShiftQueue shiftQueue)
        {

            
           var nextShiftQueue=  _shiftQueueRepository.CancelAndNextQueueUser(shiftQueue);

             _unitOfWork.SaveChanges();

            return nextShiftQueue;
        }

        public void UpdateQueue(ShiftQueue shiftQueue)
        {
            _shiftQueueRepository.UpdateQueue(shiftQueue);
            _unitOfWork.SaveChanges();
        }
        public List<User> GetOrganizationUsers(int OrganizationId)
        {
            return _shiftQueueRepository.GetOrganizationUsers(OrganizationId);
        }
    }
}
