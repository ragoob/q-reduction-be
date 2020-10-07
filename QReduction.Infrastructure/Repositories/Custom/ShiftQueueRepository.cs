using Microsoft.EntityFrameworkCore;
using QReduction.Core.Domain;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Extensions;
using QReduction.Core.Repository.Custom;
using QReduction.Infrastructure.DbContexts;
using QReduction.Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QReduction.Infrastructure.Repositories.Custom
{
    public class ShiftQueueRepository : Repository<ShiftQueue>, IShiftQueueRepository
    {

        private readonly DbSet<ShiftQueue> _shiftQueueSet;
        private readonly DbSet<ShiftUser> _shiftUserSet;
        private readonly DbSet<Shift> _shiftSet;
        private readonly DbSet<User> _userSet;
        private readonly DbSet<Branch> _branchSet;
       // private readonly DbSet<Organization> _organizationSet;

        public ShiftQueueRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _shiftQueueSet = databaseContext.Set<ShiftQueue>();
            _shiftUserSet = databaseContext.Set<ShiftUser>();
            _shiftSet = databaseContext.Set<Shift>();
            _userSet = databaseContext.Set<User>();
            _branchSet = databaseContext.Set<Branch>();
           // _organizationSet = databaseContext.Set<Organization>();
        }
        public ShiftQueue GetNextQueueUser(ShiftQueue shiftQueue)
        {


            if (_shiftQueueSet.Any(r => r.ShiftId == shiftQueue.ShiftId)) // Ensure That shift have at least one  Queue
            {

                //Select Queue to Update Order 
                var Queue = _shiftQueueSet.Where(r => r.ShiftId == shiftQueue.ShiftId && r.IsServiceDone==false &&
                r.UserIdBy == shiftQueue.UserIdBy && r.WindowNumber==shiftQueue.WindowNumber).ToList();

                foreach (var item in Queue)
                {
                    item.UserTurn = item.UserTurn - 1;
                    _shiftQueueSet.Update(item);
                }

                var shiftUser = _shiftQueueSet.Where(a => a.Id == shiftQueue.Id && a.UserIdMobile == shiftQueue.UserIdMobile).FirstOrDefault();

                shiftUser.IsServiceDone = true;
                _shiftQueueSet.Update(shiftUser);
                Queue.Remove(Queue.FirstOrDefault(a => a.UserTurn == 0));
                return Queue.FirstOrDefault(r => r.UserTurn == 1 && r.IsServiceDone == false);
            }

            return  new  ShiftQueue();


        }
        public ShiftQueue CancelAndNextQueueUser(ShiftQueue shiftQueue)
        {


            if (_shiftQueueSet.Any(r => r.ShiftId == shiftQueue.ShiftId)) // Ensure That shift have at least one  Queue
            {

                //Select Queue to Update Order 
                var Queues = _shiftQueueSet.Where(r => r.ShiftId == shiftQueue.ShiftId && r.IsServiceDone == false &&
                r.UserIdBy == shiftQueue.UserIdBy && r.WindowNumber == shiftQueue.WindowNumber).ToList();

                foreach (var item in Queues)
                {
                    item.UserTurn = item.UserTurn - 1;
                    _shiftQueueSet.Update(item);
                }

                //remove current shift queue from DM
                var deleteQueue = _shiftQueueSet.Where(a => a.Id == shiftQueue.Id && a.UserIdMobile == shiftQueue.UserIdMobile).FirstOrDefault();
                _shiftQueueSet.Remove(deleteQueue);


                //remove from queue
                Queues.Remove(Queues.FirstOrDefault(a=>a.UserTurn==0));
                
                // return first queue
                return Queues.FirstOrDefault(a=>a.UserTurn==1);
            }

            return new ShiftQueue();

        }

        public void UpdateQueue(ShiftQueue shiftQueue)
        {
            _shiftQueueSet.Update(shiftQueue);
        }

        public List<User> GetOrganizationUsers(int OrganizationId)
        {
            var shiftUserQuery = from shU in _shiftUserSet
                             join sh in _shiftSet on shU.ShiftId equals sh.Id where !sh.IsEnded 
                             join b in _branchSet on sh.BranchId equals b.Id where b.OrganizationId==OrganizationId
                             select shU.UserId;

            var result = from u in _userSet where !shiftUserQuery.Contains(u.Id) &&u.OrganizationId==OrganizationId
                         && u.IsActive && u.UserTypeId == UserTypes.Tailor
                         select u;

            return result.ToList();
        }
       
    }
}
