using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QReduction.Api;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Domain;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Models;
using QReduction.Core.Service.Custom;
using QReduction.Core.Service.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.QReduction.Infrastructure.DbMappings.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    [ApiExplorerSettings(GroupName = "Admin")]
    public class ShiftUserController : CustomBaseController
    {
        #region Fields
        private readonly IService<ShiftUser> _shiftUserService;
        private readonly IService<User> _userService;
        private readonly IShiftQueueService _shiftQueueService;

        #endregion

        #region ctor
        public ShiftUserController(IService<ShiftUser> shiftUserService, IService<User> userService, IShiftQueueService shiftQueueService)
        {
            _shiftUserService = shiftUserService;
            _userService = userService;
            _shiftQueueService = shiftQueueService;
        }
        #endregion

        #region Actions

        [HttpPost]
        [Route("AssignUserShift")]
        [CustomAuthorizationFilter("Shift.AssignUserShift")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> AssignUserShift(ShiftUser shiftUser)
        {

            if (await _shiftUserService.AnyAsync(s => s.Shift.Id == shiftUser.ShiftId && s.Shift.IsEnded))
                return BadRequest(Messages.ShiftIsClosed);

            if (await _shiftUserService.AnyAsync(s => s.ShiftId == shiftUser.ShiftId && s.WindowNumber == shiftUser.WindowNumber))
                return BadRequest(Messages.AlreadyAssignedToUser);

            // shiftUser.UserId = UserId;
            await _shiftUserService.AddAsync(shiftUser);
            return Ok();
        }

        [HttpPost]
        [Route("AssignUserToOpenShift")]
        [CustomAuthorizationFilter("Shift.AssignUserShift")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> AssignUserToOpenShift(ShiftUser shiftUser)
        {

            try
            {
                var _ShiftUser = await _shiftUserService.FindOneAsync(s => s.Id == shiftUser.Id/*s.ShiftId == shiftUser.ShiftId && s.UserId == UserId && s.CreatedAt.Date == DateTime.Now.Date*/);
                if (_ShiftUser is object)
                {
                    _ShiftUser.ServiceId = shiftUser.ServiceId;
                    _ShiftUser.WindowNumber = shiftUser.WindowNumber;
                    await _shiftUserService.EditAsync(_ShiftUser);
                    return Ok();
                }
                shiftUser.UserId = UserId;
                shiftUser.CreatedAt = DateTime.Now;
                await _shiftUserService.AddAsync(shiftUser);
                return Ok();
            }
            catch 
            {
                return BadRequest();
                //throw;
            }
        }




        [HttpGet]
        [Route("GetOrganizationUser")]
        //[CustomAuthorizationFilter("Shift.GetUsers")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public IActionResult GetOrganizationUsers()
        {
            List<User> users = new List<User>();
            users = _shiftQueueService.GetOrganizationUsers(OrganizationId);
            if (users != null && users.Any())
                return Ok(users.Select(u => new { Id = u.Id, Name = u.FirstName + " " + u.LastName, u.UserName }));

            else
                return Ok(users.Select(u => new { Id = u.Id, Name = u.FirstName + " " + u.LastName, u.UserName }));
        }



        //[HttpGet]
        //[Route("GetOrganizationUser")]
        ////[CustomAuthorizationFilter("Shift.GetUsers")]
        //[ApiExplorerSettings(GroupName = "Admin")]
        //public async Task<IActionResult> GetavailableTailors()
        //{
        //    List<User> users = new List<User>();
        //    users = _userService.FindAsync(a=>a.OrganizationId==OrganizationId && a.)
        //    return Ok(users);
        //}


        [HttpGet]
        [Route("{currentPage}/{pageSize}")]
        //[CustomAuthorizationFilter("Shift")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int currentPage, int pageSize,
       [FromQuery] string sortBy,
       [FromQuery] SearchOrders? sortOrder,
       [FromQuery] string searchText,
       [FromQuery] int shiftId)
        {
            PagedListModel<ShiftUser> pagedList = new PagedListModel<ShiftUser>(currentPage, pageSize);
            pagedList.QueryOptions.SortField = sortBy ?? pagedList.QueryOptions.SortField;
            pagedList.QueryOptions.SearchOrder = sortOrder ?? pagedList.QueryOptions.SearchOrder;

            pagedList.DataList = await
                _shiftUserService.FindAsync(pagedList.QueryOptions,
                c => c.ShiftId == shiftId, "User", "Service", "Shift"
                );

            return Ok(pagedList);
        }

        [HttpPut]
        [Route("DeleteUserShift/{id}")]
        // [CustomAuthorizationFilter("Shift.Restore")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> DeleteUserShift(int userId, int shiftId)
        {
            var entity = (await _shiftUserService.FindAsync(a => a.ShiftId == shiftId && a.UserId == userId)).FirstOrDefault();
            if (entity != null)
                _shiftUserService.Remove(entity);

            return Ok();
        }
        #endregion
    }
}