using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QReduction.Api;
using QReduction.Api.Models;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Domain;
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
    [ApiExplorerSettings(GroupName = "Mobile")]
    public class ReportsController : CustomBaseController
    {
        #region Fields
        private readonly IService<Evaluation> _EvaluationService;
        private readonly IService<EvaluationQuestionAnswer> _EvaluationQuestionAnswerService;

        private readonly IShiftQueueService _shiftQueueService;
        #endregion

        #region ctor
        public ReportsController(IShiftQueueService shiftQueueService, IService<Evaluation> EvaluationService, IService<EvaluationQuestionAnswer> EvalutionQuestionAnswer)
        {

            _shiftQueueService = shiftQueueService;
            _EvaluationService = EvaluationService;
            _EvaluationQuestionAnswerService = EvalutionQuestionAnswer;

        }

        #endregion

        #region Actions

        [HttpPost]
        [Route("EvalutionTotalReport")]
        [CustomAuthorizationFilter("Evaluation.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> EvalutionTotalReport(EvalutionTotalReportRequest evalutionTotalReport)
        {

            PagedListModel<Evaluation> pagedListModel = new PagedListModel<Evaluation>(evalutionTotalReport.currentPage, evalutionTotalReport.pageSize);


            pagedListModel.DataList = await _EvaluationService.FindAsync(pagedListModel.QueryOptions, v => v.ShiftQueue.UserBy.OrganizationId == OrganizationId &&
            v.CreateAt >= evalutionTotalReport.DateFrom &&
            v.CreateAt <= evalutionTotalReport.DateTo &&
            v.ShiftQueue.ServiceId == evalutionTotalReport.ServiceId, "ShiftQueue", "ShiftQueue.UserMobile", "ShiftQueue.UserBy", "EvaluationQuestionAnswers", "ShiftQueue.UserBy.Branch"

          );


            PagedListModel<EvalutionTotalReportResponse> ReturendModel = new PagedListModel<EvalutionTotalReportResponse>();


            ReturendModel.DataList = pagedListModel.DataList.Select(r => new EvalutionTotalReportResponse() { EvalutionId = r.Id, TellerUser = r.ShiftQueue?.UserBy, UserMobile = r.ShiftQueue?.UserMobile, EvalutionPrecentage = CalculateEvalutionPrecentage(r.EvaluationQuestionAnswers?.ToList()) });
            ReturendModel.QueryOptions = pagedListModel.QueryOptions;


            return Ok(ReturendModel);
        }



        [HttpPost]
        [Route("EvalutionDetailReport")]
        [CustomAuthorizationFilter("Evaluation.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> EvalutionDetailReport(int EvalutionId)

        {

            PagedListModel<Evaluation> pagedListModel = new PagedListModel<Evaluation>();
            var EvalutionData = await _EvaluationService.FindAsync(v => v.ShiftQueue.UserBy.OrganizationId == OrganizationId &&

             v.Id == EvalutionId, "ShiftQueue", "ShiftQueue.UserMobile", "ShiftQueue.UserBy", "EvaluationQuestionAnswers", "EvaluationQuestionAnswers.Question", "ShiftQueue.UserBy.Branch"

            );

            var List = EvalutionData.Select(r => new EvalutionDetailReportResponse() { EvalutionComment = r.Comment, EvalutionId = r.Id, TellerUser = r.ShiftQueue?.UserBy, UserMobile = r.ShiftQueue?.UserMobile, evaluationQuestionAnswers = r.EvaluationQuestionAnswers?.ToList() });




            return Ok(List);
        }









        [HttpPost]
        [Route("OrganizationVisitorTotalReport")]
        [CustomAuthorizationFilter("Evaluation.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> OrganizationVisitorTotalReport(OragnizationTotalVisitorRequest oragnizationTotalVisitorRequest)
        {

            PagedListModel<ShiftQueue> pagedListModel = new PagedListModel<ShiftQueue>(oragnizationTotalVisitorRequest.currentPage, oragnizationTotalVisitorRequest.pageSize);


            pagedListModel.DataList = await _shiftQueueService.FindAsync(pagedListModel.QueryOptions, v => v.UserBy.OrganizationId == OrganizationId &&
            v.Shift.BranchId == oragnizationTotalVisitorRequest.BranchId.Value &&
          v.IsServiceDone == true

           , "UserMobile",
           "Shift.Branch"

          );

            PagedListModel<OragnizationTotalVisitorResponse> ReturendModel = new PagedListModel<OragnizationTotalVisitorResponse>();

            
            ReturendModel.DataList = pagedListModel.DataList.Distinct().GroupBy(r => r.UserMobile).Select
                (r => new OragnizationTotalVisitorResponse() { MobileUser = r.Key, NumberOfVisits = r.Count() });

            ReturendModel.QueryOptions = pagedListModel.QueryOptions;

            return Ok(ReturendModel);
        }



        [HttpPost]
        [Route("OrganizationVisitorDetailReport")]
        [CustomAuthorizationFilter("Evaluation.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> OrganizationVisitorDetailReport(OragnizationDetailVisitorRequest oragnizationDetailVisitorRequest)
        {

            PagedListModel<ShiftQueue> pagedListModel = new PagedListModel<ShiftQueue>(oragnizationDetailVisitorRequest.currentPage, oragnizationDetailVisitorRequest.pageSize);


            pagedListModel.DataList = await _shiftQueueService.FindAsync(pagedListModel.QueryOptions, v => v.UserBy.OrganizationId == OrganizationId && v.UserIdMobile == oragnizationDetailVisitorRequest.MobileUserId
            && v.IsServiceDone == true
           , "UserMobile", "UserBy", "Service", "UserBy.Branch");

            PagedListModel<OragnizationDetailVisitorResponse> ReturendModel = new PagedListModel<OragnizationDetailVisitorResponse>();

            ReturendModel.DataList = pagedListModel.DataList.Select
                (r => new OragnizationDetailVisitorResponse() { MobileUser = r.UserMobile, TellerUser = r.UserBy, Service = r.Service, DateTime = DateTime.Now });

            ReturendModel.QueryOptions = pagedListModel.QueryOptions;

            return Ok(ReturendModel);
        }






        private string CalculateEvalutionPrecentage(List<EvaluationQuestionAnswer> evaluationQuestionAnswers)
        {
            if (evaluationQuestionAnswers != null && evaluationQuestionAnswers.Any())
            {
                evaluationQuestionAnswers.ForEach(r => { r.AnswerValue = r.AnswerValue == 1 ? 33 : r.AnswerValue == 2 ? 66 : r.AnswerValue == 3 ? 100 : 0; });
                var precentage = evaluationQuestionAnswers.Sum(r => r.AnswerValue) / evaluationQuestionAnswers.Count();

                return precentage.ToString();

            }
            return "Not Answered Evalution";

        }







        #endregion
    }
}