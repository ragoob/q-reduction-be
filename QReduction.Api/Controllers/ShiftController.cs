using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QReduction.Api;
using QReduction.Api.Models;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Domain;
using QReduction.Core.Extensions;
using QReduction.Core.Infrastructure;
using QReduction.Core.Models;
using QReduction.Core.Service.Custom;
using QReduction.Core.Service.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    [ApiExplorerSettings(GroupName = "Admin")]
    public class ShiftController : CustomBaseController
    {
        const string lorim = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

        #region Fields
        private readonly IService<Shift> _shiftService;
        private readonly IService<BranchService> _branchServiceService;
        private readonly IService<ShiftUser> _shiftUserService;
        //     private readonly IShiftQueueService _shiftQueueService;
        #endregion

        #region ctor
        public ShiftController(IShiftQueueService shiftQueueService, IService<Shift> shiftService, IService<BranchService> branchServiceService, IService<ShiftUser> shiftUserService
            //, IService<ShiftUser> shiftUserService
            )
        {
            _shiftService = shiftService;
            //  _shiftQueueService = shiftQueueService;
            _branchServiceService = branchServiceService;
            _shiftUserService = shiftUserService;
        }
        #endregion

        #region Actions

        [HttpPost]
        [Route("OpenShift")]
        [CustomAuthorizationFilter("Shift.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> OpenShift(Shift shift)
        {
            if (await _shiftService.AnyAsync(s => s.BranchId == shift.BranchId && !s.IsEnded))
                return BadRequest(Messages.CantOpenShiftWhileOpendOne);

            if (await _shiftService.AnyAsync(s => s.Code == shift.Code))
                return BadRequest(Messages.Exist_Code);

            shift.StartAt = DateTime.UtcNow;
            shift.QRCode = Guid.NewGuid().ToString();
            shift.IsEnded = false;
            shift.EndAt = null;

            shift.CreateAt = DateTime.UtcNow;
            shift.CreateBy = UserId;
            await _shiftService.AddAsync(shift);
            return Ok();
        }

        [HttpPost]
        [Route("AddBranchShifts")]
        [ApiExplorerSettings(GroupName = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> AddBranchShifts(AddShiftModel Input)
        {
            try
            {
                List<Shift> DataList = new List<Shift>();
                foreach (var shift in Input.Shifts)
                {
                    DataList.Add(
                        new Shift()
                        {
                            StartAt = shift.StartAt,
                            Start = shift.StartAt.TimeOfDay,
                            QRCode = Guid.NewGuid().ToString(),
                            IsEnded = true,
                            End = shift.EndAt.TimeOfDay,
                            CreateAt = DateTime.UtcNow,
                            BranchId = Input.BranchId

                        });

                }
                await _shiftService.AddRangeAsync(DataList);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("GetOpenBranchShifts")]
        [ApiExplorerSettings(GroupName = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOpenBranchShifts(int Id)
        {
            try
            {
                var shifts = await _shiftService.FindAsync(c => c.BranchId == Id && (c.Start >= DateTime.UtcNow.TimeOfDay && c.End <= DateTime.UtcNow.TimeOfDay));
                return Ok(shifts);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetAssignedShiftPerDayForBranch")]
        [CustomAuthorizationFilter("Shift.AssignUserShift")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> GetAssignedShiftPerDayForBranch(int branchId)
        {
            //var shifts = (await _shiftService.FindAsync(c => c.BranchId == Id && c.Start >= DateTime.UtcNow.TimeOfDay && c.End <= DateTime.UtcNow.TimeOfDay, "ShiftUsers"));///.Where(c=>c.ShiftUsers.Any(s=> s.UserId == UserId && c.CreateAt.Date ==DateTime.Now.Date ));
            var _shiftUser = (await _shiftUserService.FindAsync(c => c.UserId == UserId
            && c.CreatedAt.Date == DateTime.Now.Date && c.Shift.BranchId == branchId && c.Shift.Start >= DateTime.UtcNow.TimeOfDay && c.Shift.End <= DateTime.UtcNow.TimeOfDay, "Shift"))?
                .Select(c => new
                {
                    id = c.Id,
                    ShiftId = c?.ShiftId,
                    WindowNumber = c?.WindowNumber,
                    ServiceId = c?.ServiceId,
                    ShiftStart = c?.Shift.Start,
                    ShiftEnd = c?.Shift.End

                }).FirstOrDefault();

            if (!(_shiftUser is object))
            {
                var shifts = (await _shiftService.FindAsync(c => c.BranchId == branchId && (c.Start >= DateTime.UtcNow.TimeOfDay && c.End <= DateTime.UtcNow.TimeOfDay)))
                    .Select(
                    c => new
                    {
                        id = c.Id,
                        ShiftId = c?.Id,
                        WindowNumber = string.Empty,
                        ServiceId = 0,
                        ShiftStart = c?.Start,
                        ShiftEnd = c?.End
                    });
                return Ok(shifts);
            }
            return Ok(_shiftUser);

            //if (await _shiftUserService.AnyAsync(s => s.Shift.Id == shiftUser.ShiftId ))
            //    return BadRequest(Messages.ShiftIsClosed);

            //if (await _shiftUserService.AnyAsync(s => s.ShiftId == shiftUser.ShiftId && s.WindowNumber == shiftUser.WindowNumber
            //&& s.CreatedAt.Date == DateTime.Now.Date && s.ServiceId == shiftUser.ServiceId))
            //    return BadRequest(Messages.AlreadyAssignedToUser);

            //shiftUser.UserId = UserId;
            //shiftUser.CreatedAt = DateTime.Now;
            //await _shiftUserService.AddAsync(shiftUser);
            // return Ok();
            // return Ok();
        }
        [HttpGet]
        [Route("GetShiftById")]
        [ApiExplorerSettings(GroupName = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> GetShift(int Id)
        {
            try
            {
                var shift = await _shiftService.GetByIdAsync(Id);
                return Ok(shift);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("GetBranchShifts")]
        [ApiExplorerSettings(GroupName = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBranchShifts(int Id)
        {
            try
            {

                //var openShifts = await _shiftService.FindAsync(c => c.BranchId == Id && c.End >= DateTime.Now.TimeOfDay && c.Start <= DateTime.Now.TimeOfDay);
                //openShifts.ForEach(c => c.IsEnded = false);

                var shifts = (await _shiftService.FindAsync(c => c.BranchId == Id)).Select(
                    c => new
                    {
                        Id = c.Id,
                        BranchId = c.BranchId,
                        Start = c.Start,
                        End = c.End,
                        IsEnded = c.Start >= DateTime.UtcNow.TimeOfDay && c.End <= DateTime.UtcNow.TimeOfDay ? false : true
                    }
                    );

                return Ok(shifts);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("UpdateShift")]
        [ApiExplorerSettings(GroupName = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateBranchShifts(UpdateShiftModel Shift)
        {
            try
            {
                var _shift = _shiftService.GetById(Shift.Id);
                if (!(_shift is object))
                    return NotFound("Shift Not Found");
                _shift.End = Shift.EndAt.TimeOfDay;
                _shift.Start = Shift.StartAt.TimeOfDay;
                _shift.BranchId = Shift.BranchId;
                await _shiftService.EditAsync(_shift);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //[HttpPost]
        //[Route("OpenShiftAndAssignUser")]
        //[CustomAuthorizationFilter("Shift.Add")]
        //[ApiExplorerSettings(GroupName = "Admin")]
        //public async Task<IActionResult> OpenShiftAndAssignUser(Shift shift)
        //{
        //    if (await _shiftService.AnyAsync(s => s.BranchId == shift.BranchId && !s.IsEnded))
        //        return BadRequest(Messages.CantOpenShiftWhileOpendOne);

        //    if (await _shiftService.AnyAsync(s => s.Code == shift.Code))
        //        return BadRequest(Messages.Exist_Code);

        //    shift.StartAt = DateTime.UtcNow;
        //    shift.QRCode = Guid.NewGuid().ToString();
        //    shift.IsEnded = false;
        //    shift.EndAt = null;

        //    shift.CreateAt = DateTime.UtcNow;
        //    shift.CreateBy = UserId;
        //    await _shiftService.AddAsync(shift);
        //    // asignUser 

        //    ShiftUser shiftUser = new ShiftUser() {
        //        ShiftId=shift.Id,
        //        // UserId=shift.UserIdSupport,
        //       // WindowNumber=shift.
        //    };
        //    return Ok();
        //}

        [HttpPut]
        [Route("CloseShift/{id}")]
        [CustomAuthorizationFilter("Shift.Edit")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> CloseShift(int id)
        {
            Shift shift = await _shiftService.GetByIdAsync(id);

            if (shift == null)
                return NotFound();

            if (shift.IsEnded)
                return BadRequest(Messages.ShiftIsClosed);

            shift.IsEnded = true;
            shift.EndAt = DateTime.UtcNow;
            shift.UpdateAt = DateTime.UtcNow;
            shift.UpdateBy = UserId;

            await _shiftService.EditAsync(shift);

            return Ok();
        }

        [HttpGet]
        [Route("{currentPage}/{pageSize}")]
        [CustomAuthorizationFilter("Shift")]
        public async Task<IActionResult> Get(int currentPage, int pageSize,
            [FromQuery] string sortBy,
            [FromQuery] SearchOrders? sortOrder,
            [FromQuery] string searchText,
            [FromQuery] bool status)
        {
            PagedListModel<Shift> pagedList = new PagedListModel<Shift>(currentPage, pageSize);
            pagedList.QueryOptions.SortField = sortBy ?? pagedList.QueryOptions.SortField;
            pagedList.QueryOptions.SearchOrder = sortOrder ?? pagedList.QueryOptions.SearchOrder;

            pagedList.DataList = await
                _shiftService.FindAsync(pagedList.QueryOptions,
                c => c.IsEnded == status && c.Branch.OrganizationId == OrganizationId && c.CreateBy == UserId

                );

            return Ok(pagedList);
        }
        [HttpGet]
        [Route("GetAllShifts")]
        [CustomAuthorizationFilter("Shift")]
        public async Task<IActionResult> GetAllShifts(bool status)
        {
            return Ok((await
                   _shiftService.FindAsync(c => c.IsEnded == status && c.Branch.OrganizationId == OrganizationId)).ToList());
        }


        ////[HttpGet]
        //[Route("{currentPage}/{pageSize}")]
        //[CustomAuthorizationFilter("Shift")]
        //public async Task<IActionResult> Get(int currentPage, int pageSize,
        //    [FromQuery] string sortBy,
        //    [FromQuery] SearchOrders? sortOrder,
        //    [FromQuery] string searchText,
        //    [FromQuery] int? code,
        //    [FromQuery] string nameAr,
        //    [FromQuery] string nameEn,
        //    [FromQuery] bool isDeleted)
        //{
        //    PagedListModel<Shift> pagedList = new PagedListModel<Shift>(currentPage, pageSize);
        //    pagedList.QueryOptions.SortField = sortBy ?? pagedList.QueryOptions.SortField;
        //    pagedList.QueryOptions.SearchOrder = sortOrder ?? pagedList.QueryOptions.SearchOrder;

        //    pagedList.DataList = await
        //        _shiftService.FindAsync(pagedList.QueryOptions,
        //        c =>
        //        (code == null || c.Code == code) &&
        //        (nameAr == null || c.NameAr.Contains(nameAr)) &&
        //        (nameEn == null || c.NameEn.Contains(nameEn)) &&
        //        (string.IsNullOrWhiteSpace(searchText) ||
        //            (c.Code.ToString().Contains(searchText) || (c.NameAr.Contains(searchText) || (c.NameEn.Contains(searchText)))))
        //        );

        //    return Ok(pagedList);
        //}

        [HttpGet("{id}")]
        [CustomAuthorizationFilter("Shift")]
        public async Task<IActionResult> Get(int id)
        {
            Shift shift = (await _shiftService.FindAsync(c => c.Id == id)).SingleOrDefault();
            return Ok(shift);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("GetNextCode")]
        [ApiExplorerSettings(GroupName = "Customer")]
        public async Task<IActionResult> GetNextCode()
        {
            int nextCode = (await _shiftService.MaxAsync<int>(c => c.Code)) + 1;

            return Ok(new
            {
                nextCode
            });
        }

        #endregion


        #region Mobile

        [HttpGet]
        [Route("GetShiftByQrCode/{qrCode}")]
        [Authorize(Roles = SystemKeys.MobileRole)]
        //[ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> GetShiftByQrCode(string qrCode)
        {
            Shift shift = (await _shiftService.FindAsync(s => s.QRCode == qrCode)).SingleOrDefault();

            if (shift == null)
                return NotFound();

            IEnumerable<Service> services =
                (await _branchServiceService.FindAsync(bs => bs.BranchId == shift.BranchId,
                "Service")).Select(s => s.Service).Where(s => !s.IsDeleted);

            string instructions = lorim;

            return Ok(
                new
                {
                    Shift = shift,
                    Services = services,
                    Instructions = instructions
                });
        }






        [HttpGet]
        [Route("GetOpenedShiftForTest")]
        //[Authorize(Roles = SystemKeys.MobileRole)]
        //[ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> GetOpenedShiftForTest()
        {
            Shift shift = (await _shiftService.FindAsync(s => !s.IsEnded)).SingleOrDefault();

            if (shift == null)
                return NotFound();

            return Ok(shift);
        }

        #endregion
    }
}