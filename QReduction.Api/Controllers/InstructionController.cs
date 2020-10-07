using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QReduction.Api;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Domain;
using QReduction.Core.Models;
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
    public class InstructionController : CustomBaseController
    {
        #region Fields
        private readonly IService<Instruction> _instructionService;
        #endregion

        #region ctor
        public InstructionController(IService<Instruction> instructionService)
        {
            _instructionService = instructionService;
        }
        #endregion

        #region Actions

        [HttpPost]
        [CustomAuthorizationFilter("Instruction.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Post(Instruction instruction)
        {
            if (await _instructionService.AnyAsync(info => info.Code == instruction.Code))
                return BadRequest(Messages.Exist_Code);

            if (await _instructionService.AnyAsync(info => info.NameAr == instruction.NameAr))
                return BadRequest(Messages.Exist_NameAr);

            if (await _instructionService.AnyAsync(info => info.NameEn == instruction.NameEn))
                return BadRequest(Messages.Exist_NameEn);

            instruction.CreateAt = DateTime.UtcNow;
            instruction.CreateBy = UserId;
            instruction.OrganizationId = OrganizationId;
            await _instructionService.AddAsync(instruction);
            return Ok();
        }

        [HttpPut]
        [CustomAuthorizationFilter("Instruction.Edit")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Put(Instruction instruction)
        {

            if (await _instructionService.AnyAsync(info => info.Code == instruction.Code && info.Id != instruction.Id))
                return BadRequest(Messages.Exist_Code);

            if (await _instructionService.AnyAsync(info => info.NameAr == instruction.NameAr && info.Id != instruction.Id))
                return BadRequest(Messages.Exist_NameAr);

            if (await _instructionService.AnyAsync(info => info.NameEn == instruction.NameEn && info.Id != instruction.Id))
                return BadRequest(Messages.Exist_NameEn);

            instruction.UpdateAt = DateTime.UtcNow;
            instruction.UpdateBy = UserId;
            await _instructionService.EditAsync(instruction);
            return Ok();
        }

        [HttpPut]
        [Route("RemoveToRecycleBin/{id}")]
        [CustomAuthorizationFilter("Instruction.Delete")]
        [ApiExplorerSettings(GroupName = "OrganizationAdmin")]
        public async Task<IActionResult> RemoveToRecycleBin(int id)
        {
            Instruction instruction = await _instructionService.GetByIdAsync(id);

            if (instruction != null)
            {
                instruction.DeletedAt = DateTime.UtcNow;
                instruction.DeletedBy = UserId;
                instruction.IsDeleted = true;
                await _instructionService.EditAsync(instruction);
            }

            return Ok();
        }

        [HttpPut]
        [Route("RestoreDeleted/{id}")]
        [CustomAuthorizationFilter("Instruction.Restore")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> RestoreDeleted(int id)
        {
            var entity = await _instructionService.GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = false;
                entity.DeletedBy = null;
                entity.DeletedAt = null;
                await _instructionService.EditAsync(entity);
            }
            return Ok();
        }

        [HttpPut]
        [Route("MultiRemoveToRecycleBin/{ids}")]
        [CustomAuthorizationFilter("Instruction.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> MultiRemoveToRecycleBin(string ids)
        {
            int[] idsToDelete = ids.Split(',').Select(Int32.Parse).ToArray();

            List<Instruction> instructions = _instructionService.Find(item => idsToDelete.Contains(item.Id)).ToList();
            if (instructions == null)
                return BadRequest(Messages.CanNotDeleteMulti);
            instructions.ForEach(e =>
            {
                e.DeletedAt = DateTime.UtcNow;
                e.DeletedBy = UserId;
                e.IsDeleted = true;
            });

            await _instructionService.EditRangeAsync(instructions);
            return Ok();
        }

        [HttpPut]
        [Route("MultiRestoreDeleted/{ids}")]
        [CustomAuthorizationFilter("Instruction.Restore")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> MultiRestoreDeleted(string ids)
        {
            int[] idsToRestor = ids.Split(',').Select(Int32.Parse).ToArray();

            List<Instruction> instructions = _instructionService.Find(item => idsToRestor.Contains(item.Id)).ToList();
            if (instructions == null)
                return BadRequest(Messages.CanNotRestorMulti);
            instructions.ForEach(e =>
            {
                e.IsDeleted = false;
                e.DeletedBy = null;
                e.DeletedAt = null;
            });
            await _instructionService.EditRangeAsync(instructions);
            return Ok();
        }

        [HttpGet]
        [Route("{currentPage}/{pageSize}")]
        [CustomAuthorizationFilter("Instruction")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int currentPage, int pageSize,
            [FromQuery] string sortBy,
            [FromQuery] SearchOrders? sortOrder,
            [FromQuery] string searchText,
            [FromQuery] int? code,
            [FromQuery] string nameAr,
            [FromQuery] string nameEn,
            [FromQuery] string phone,
            [FromQuery] bool isDeleted)
        {
            PagedListModel<Instruction> pagedList = new PagedListModel<Instruction>(currentPage, pageSize);
            pagedList.QueryOptions.SortField = sortBy ?? pagedList.QueryOptions.SortField;
            pagedList.QueryOptions.SearchOrder = sortOrder ?? pagedList.QueryOptions.SearchOrder;

            pagedList.DataList = await 
                _instructionService.FindAsync(pagedList.QueryOptions, 
                c => c.IsDeleted == isDeleted && 
                (code == null || c.Code == code) &&
                (nameAr == null || c.NameAr.Contains(nameAr)) &&
                (nameEn == null || c.NameEn.Contains(nameEn)) &&
                (string.IsNullOrWhiteSpace(searchText) || 
                    (c.Code.ToString().Contains(searchText) ||
                     c.NameAr.Contains(searchText) ||
                     c.NameEn.Contains(searchText))
                ));

            return Ok(pagedList);
        }

        [HttpGet("{id}")]
        [CustomAuthorizationFilter("Instruction")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            Instruction instruction = (await _instructionService.FindAsync(c => c.Id == id,
                "InstructionServices")).SingleOrDefault();
            return Ok(instruction);
        }

        [HttpGet]
        [Route("GetItemList/{lang}")]
        [AllowAnonymous]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<ActionResult> GetItemList(string lang)
        {
            bool isArabic = lang.Equals("ar", StringComparison.OrdinalIgnoreCase);
            IEnumerable<SelectItemModel> items = (await _instructionService.GetAllAsync())
                .Select(info =>
                    new SelectItemModel
                    {
                        Value = info.Id,
                        Label = isArabic ? info.NameAr : info.NameEn
                    });

            return Ok(items);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetNextCode")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> GetNextCode()
        {
            int nextCode = (await _instructionService.MaxAsync<int>(c => c.Code)) + 1;

            return Ok(new
            {
                nextCode
            });
        }

        #endregion
    }
}