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
    //[EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    public class QuestionController : CustomBaseController
    {
        #region Fields
        private readonly IService<Question> _questionService;
        private readonly IService<EvaluationQuestionAnswer> _evaluationQuestionAnswerService;
        private readonly IService<Evaluation> _evaluationService;



        #endregion

        #region ctor
        public QuestionController(IService<Question> QuestionService, IService<EvaluationQuestionAnswer> evaluationQuestionAnswerService, IService<Evaluation> evaluationService)
        {
            _questionService = QuestionService;
            _evaluationQuestionAnswerService = evaluationQuestionAnswerService;
            _evaluationService = evaluationService;
        }
        #endregion

        #region Actions


        [HttpPost]
        [CustomAuthorizationFilter("Question.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Post(Question Question)
        {
            if (await _questionService.AnyAsync(info => info.Code == Question.Code))
                return BadRequest(Messages.Exist_Code);

            if (await _questionService.AnyAsync(info => info.QuestionTextAr == Question.QuestionTextAr))
                return BadRequest(Messages.Exist_NameAr);

            if (await _questionService.AnyAsync(info => info.QuestionTextEn == Question.QuestionTextEn))
                return BadRequest(Messages.Exist_NameEn);



            Question.CreateAt = DateTime.UtcNow;
            Question.CreateBy = UserId;
            Question.OrganizationId = OrganizationId;
            await _questionService.AddAsync(Question);
            return Ok();
        }

        [HttpPut]
        [CustomAuthorizationFilter("Question.Edit")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Put(Question Question)
        {

            if (await _questionService.AnyAsync(info => info.Code == Question.Code && info.Id != Question.Id))
                return BadRequest(Messages.Exist_Code);

            if (await _questionService.AnyAsync(info => info.QuestionTextAr == Question.QuestionTextAr && info.Id != Question.Id))
                return BadRequest(Messages.Exist_NameAr);

            if (await _questionService.AnyAsync(info => info.QuestionTextEn == Question.QuestionTextEn && info.Id != Question.Id))
                return BadRequest(Messages.Exist_NameEn);

            var dbQuestion = (await _questionService.FindAsync(a => a.Id == Question.Id)).FirstOrDefault();

            dbQuestion.QuestionTextAr = Question.QuestionTextAr;
            dbQuestion.QuestionTextEn = Question.QuestionTextEn;
            dbQuestion.Code = Question.Code;

            dbQuestion.UpdateAt = DateTime.UtcNow;
            dbQuestion.UpdateBy = UserId;
            await _questionService.EditAsync(dbQuestion);
            return Ok();
        }

        [HttpPut]
        [Route("RemoveToRecycleBin/{id}")]
        [CustomAuthorizationFilter("Question.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> RemoveToRecycleBin(int id)
        {
            Question Question = await _questionService.GetByIdAsync(id);

            if (Question != null)
            {
                Question.DeletedAt = DateTime.UtcNow;
                Question.DeletedBy = UserId;
                Question.IsDeleted = true;
                await _questionService.EditAsync(Question);
            }

            return Ok();
        }

        [HttpPut]
        [Route("RestoreDeleted/{id}")]
        [CustomAuthorizationFilter("Question.Restore")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> RestoreDeleted(int id)
        {
            var entity = await _questionService.GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = false;
                entity.DeletedBy = null;
                entity.DeletedAt = null;
                await _questionService.EditAsync(entity);
            }
            return Ok();
        }

        [HttpPut]
        [Route("MultiRemoveToRecycleBin/{ids}")]
        [CustomAuthorizationFilter("Question.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> MultiRemoveToRecycleBin(string ids)
        {
            int[] idsToDelete = ids.Split(',').Select(Int32.Parse).ToArray();

            List<Question> Questions = _questionService.Find(item => idsToDelete.Contains(item.Id)).ToList();
            if (Questions == null)
                return BadRequest(Messages.CanNotDeleteMulti);
            Questions.ForEach(e =>
            {
                e.DeletedAt = DateTime.UtcNow;
                e.DeletedBy = UserId;
                e.IsDeleted = true;
            });

            await _questionService.EditRangeAsync(Questions);
            return Ok();
        }

        [HttpPut]
        [Route("MultiRestoreDeleted/{ids}")]
        [CustomAuthorizationFilter("Question.Restore")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> MultiRestoreDeleted(string ids)
        {
            int[] idsToRestor = ids.Split(',').Select(Int32.Parse).ToArray();

            List<Question> Questions = _questionService.Find(item => idsToRestor.Contains(item.Id)).ToList();
            if (Questions == null)
                return BadRequest(Messages.CanNotRestorMulti);
            Questions.ForEach(e =>
            {
                e.IsDeleted = false;
                e.DeletedBy = null;
                e.DeletedAt = null;
            });
            await _questionService.EditRangeAsync(Questions);
            return Ok();
        }

        [HttpGet]
        [Route("{currentPage}/{pageSize}")]
        [CustomAuthorizationFilter("Question")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int currentPage, int pageSize,
            [FromQuery] string sortBy,
            [FromQuery] SearchOrders? sortOrder,
            [FromQuery] string searchText,
            [FromQuery] int? code,
            [FromQuery] string questionTextAr,
            [FromQuery] string questionTextEn,
            [FromQuery] bool isDeleted)
        {
            PagedListModel<Question> pagedList = new PagedListModel<Question>(currentPage, pageSize);
            pagedList.QueryOptions.SortField = sortBy ?? pagedList.QueryOptions.SortField;
            pagedList.QueryOptions.SearchOrder = sortOrder ?? pagedList.QueryOptions.SearchOrder;

            pagedList.DataList = await
                _questionService.FindAsync(pagedList.QueryOptions,
                c => c.IsDeleted == isDeleted && c.OrganizationId == OrganizationId &&
                (code == null || c.Code == code) &&
                (questionTextAr == null || c.QuestionTextAr.Contains(questionTextAr)) &&
                (questionTextEn == null || c.QuestionTextEn.Contains(questionTextEn)) &&

                (string.IsNullOrWhiteSpace(searchText) ||
                    (c.Code.ToString().Contains(searchText) ||
                     c.QuestionTextAr.Contains(searchText) ||
                     c.QuestionTextEn.Contains(searchText))
                ));

            return Ok(pagedList);
        }

        [HttpGet("{id}")]
        [CustomAuthorizationFilter("Question")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            Question Question = (await _questionService.FindAsync(c => c.Id == id)).SingleOrDefault();
            return Ok(Question);
        }

        [HttpGet]
        [Route("GetItemList/{lang}")]
        [ApiExplorerSettings(GroupName = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult> GetItemList(string lang)
        {
            bool isArabic = lang.Equals("ar", StringComparison.OrdinalIgnoreCase);
            IEnumerable<SelectItemModel> items = (await _questionService.GetAllAsync())
                .Select(info =>
                    new SelectItemModel
                    {
                        Value = info.Id,
                        Label = isArabic ? info.QuestionTextAr : info.QuestionTextEn
                    });

            return Ok(items);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetNextCode")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> GetNextCode()
        {
            int nextCode = (await _questionService.MaxAsync<int>(c => c.Code)) + 1;

            return Ok(new
            {
                nextCode
            });
        }

        #endregion


        #region  Evaluation_Question

        [HttpGet]
        [Route("GetUserQuestions/{evaluationId}")]
        //[CustomAuthorizationFilter("Shift.Add")]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> GetUserQuestions(int evaluationId)
        {
            var eval = (await _evaluationService.FindAsync(a => a.Id == evaluationId, "ShiftQueue", "ShiftQueue.UserBy")).FirstOrDefault();
            List<QuestionVM> questions = (await _questionService.FindAsync(a => a.OrganizationId == eval.ShiftQueue.UserBy.OrganizationId)).Select(a => new QuestionVM
            {
                Id = a.Id,
                QuestionTextAr = a.QuestionTextAr,
                OrganizationId = a.OrganizationId,
                Code = a.Code,
                QuestionTextEn = a.QuestionTextEn
            }).ToList();

            return Ok(new EvaluationMobileClass()
            {
                Evaluation = eval,
                Questions = questions
            });

        }
        [HttpPost]
        [Route("SubmitUserAnswers")]
        //[CustomAuthorizationFilter("Shift.Add")]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> SubmitUserAnswers(EvaluationMobileClass model)
        {
            foreach (var item in model.Questions)
            {
                EvaluationQuestionAnswer obj = new EvaluationQuestionAnswer()
                {
                    EvaluationId = model.Evaluation.Id,
                    QuestionId = item.Id,
                    AnswerValue = item.AnswerValue,
                    
                };
                _evaluationQuestionAnswerService.Add(obj);
            }
            //update comment in Evaluation
            var eval = (await _evaluationService.FindAsync(a => a.Id == model.Evaluation.Id)).FirstOrDefault();
            eval.Comment = model.Evaluation.Comment;
            _evaluationService.Edit(eval);

            return Ok();

        }

        #endregion
    }
    public class EvaluationMobileClass
    {
        public List<QuestionVM> Questions { get; set; }
        public Evaluation Evaluation { get; set; }
    }
}