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
    [ApiExplorerSettings(GroupName = "Mobile")]
    public class EvaluationController : CustomBaseController
    {
        #region Fields
        private readonly IService<Evaluation> _EvaluationService;
        #endregion

        #region ctor
        public EvaluationController(IService<Evaluation> EvaluationService)
        {
            _EvaluationService = EvaluationService;
        }
        #endregion

        #region Actions

        [HttpPost]
        [CustomAuthorizationFilter("valuation.Add")]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> Post(Evaluation evaluation)
        {
            evaluation.CreateAt = DateTime.UtcNow;
          
            await _EvaluationService.AddAsync(evaluation);
            return Ok();
        }


        //[HttpPut]
        //[Route("RemoveToRecycleBin/{id}")]
        //[CustomAuthorizationFilter("Evaluation.Delete")]
        //[ApiExplorerSettings(GroupName = "Mobile")]
        //public async Task<IActionResult> RemoveToRecycleBin(int id)
        //{
        //    Evaluation evaluation = await _EvaluationService.GetByIdAsync(id);

        //    if (evaluation != null)
        //    {
        //        evaluation.DeletedAt = DateTime.UtcNow;
        //        evaluation.DeletedBy = UserId;
        //        evaluation.IsDeleted = true;
        //        await _EvaluationService.EditAsync(evaluation);
        //    }

        //    return Ok();
        //}

        //[HttpPut]
        //[Route("RestoreDeleted/{id}")]
        //[CustomAuthorizationFilter("Evaluation.Restore")]
        //[ApiExplorerSettings(GroupName = "Mobile")]
        //public async Task<IActionResult> RestoreDeleted(int id)
        //{
        //    var entity = await _EvaluationService.GetByIdAsync(id);
        //    if (entity != null)
        //    {
        //        entity.IsDeleted = false;
        //        entity.DeletedBy = null;
        //        entity.DeletedAt = null;
        //        await _EvaluationService.EditAsync(entity);
        //    }
        //    return Ok();
        //}

        //[HttpPut]
        //[Route("MultiRemoveToRecycleBin/{ids}")]
        //[CustomAuthorizationFilter("Evaluation.Delete")]
        //[ApiExplorerSettings(GroupName = "Mobile")]
        //public async Task<IActionResult> MultiRemoveToRecycleBin(string ids)
        //{
        //    int[] idsToDelete = ids.Split(',').Select(Int32.Parse).ToArray();

        //    List<Evaluation> Evaluations = _EvaluationService.Find(item => idsToDelete.Contains(item.Id)).ToList();
        //    if (Evaluations == null)
        //        return BadRequest(Messages.CanNotDeleteMulti);
        //    Evaluations.ForEach(e =>
        //    {
        //        e.DeletedAt = DateTime.UtcNow;
        //        e.DeletedBy = UserId;
        //        e.IsDeleted = true;
        //    });

        //    await _EvaluationService.EditRangeAsync(Evaluations);
        //    return Ok();
        //}

        //[HttpPut]
        //[Route("MultiRestoreDeleted/{ids}")]
        //[CustomAuthorizationFilter("Evaluation.Restore")]
        //[ApiExplorerSettings(GroupName = "Mobile")]
        //public async Task<IActionResult> MultiRestoreDeleted(string ids)
        //{
        //    int[] idsToRestor = ids.Split(',').Select(Int32.Parse).ToArray();

        //    List<Evaluation> Evaluations = _EvaluationService.Find(item => idsToRestor.Contains(item.Id)).ToList();
        //    if (Evaluations == null)
        //        return BadRequest(Messages.CanNotRestorMulti);
        //    Evaluations.ForEach(e =>
        //    {
        //        e.IsDeleted = false;
        //        e.DeletedBy = null;
        //        e.DeletedAt = null;
        //    });
        //    await _EvaluationService.EditRangeAsync(Evaluations);
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("{currentPage}/{pageSize}")]
        //[CustomAuthorizationFilter("Evaluation")]
        //[ApiExplorerSettings(GroupName = "Mobile")]
        //public async Task<IActionResult> Get(int currentPage, int pageSize,
        //    [FromQuery] string sortBy,
        //    [FromQuery] SearchOrders? sortOrder,
        //    [FromQuery] string searchText,
          
        //    [FromQuery] bool isDeleted)
        //{
        //    PagedListModel<Evaluation> pagedList = new PagedListModel<Evaluation>(currentPage, pageSize);
        //    pagedList.QueryOptions.SortField = sortBy ?? pagedList.QueryOptions.SortField;
        //    pagedList.QueryOptions.SearchOrder = sortOrder ?? pagedList.QueryOptions.SearchOrder;

        //    pagedList.DataList = await 
        //        _EvaluationService.FindAsync(pagedList.QueryOptions, 
        //        c => c.IsDeleted == isDeleted && 
             
        //        (string.IsNullOrWhiteSpace(searchText) 
        //        ));

        //    return Ok(pagedList);
        //}

        //[HttpGet("{id}")]
        //[CustomAuthorizationFilter("Evaluation")]
        //[ApiExplorerSettings(GroupName = "Mobile")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    Evaluation Evaluation = (await _EvaluationService.FindAsync(c => c.Id == id,
        //        "EvaluationServices")).SingleOrDefault();
        //    return Ok(Evaluation);
        //}

        #endregion
    }
}