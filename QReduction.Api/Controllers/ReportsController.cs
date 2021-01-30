using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.EntityFrameworkCore.Internal;
using QReduction.Api;
using QReduction.Api.Models;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Domain;
using QReduction.Core.Extensions;
using QReduction.Core.Models;
using QReduction.Core.Service.Custom;
using QReduction.Core.Service.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
        private readonly INodeServices _nodeServices;
        private readonly IShiftQueueService _shiftQueueService;
        #endregion

        #region ctor
        public ReportsController(IShiftQueueService shiftQueueService, IService<Evaluation> EvaluationService, IService<EvaluationQuestionAnswer> EvalutionQuestionAnswer, INodeServices nodeServices)
        {

            _shiftQueueService = shiftQueueService;
            _EvaluationService = EvaluationService;
            _EvaluationQuestionAnswerService = EvalutionQuestionAnswer;
            _nodeServices = nodeServices;
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


            pagedListModel.DataList = oragnizationTotalVisitorRequest.BranchId.HasValue ?
                await _shiftQueueService.FindAsync(pagedListModel.QueryOptions, v => v.UserBy.OrganizationId == OrganizationId &&
                   v.Shift.BranchId == oragnizationTotalVisitorRequest.BranchId.Value &&
                    v.IsServiceDone == true

                    , "UserMobile",
                    "Shift.Branch"

            ) :
              await _shiftQueueService.FindAsync(pagedListModel.QueryOptions, v => v.UserBy.OrganizationId == OrganizationId &&
                    v.IsServiceDone == true

                    , "UserMobile",
                    "Shift.Branch"

            );

            if (oragnizationTotalVisitorRequest.DateFrom.HasValue && oragnizationTotalVisitorRequest.DateTo.HasValue)
            {
                pagedListModel.DataList = pagedListModel.DataList.Where(c => c.CreatedDate >= oragnizationTotalVisitorRequest.DateFrom.Value
              && c.CreatedDate <= oragnizationTotalVisitorRequest.DateTo.Value);
            }

            PagedListModel<OragnizationTotalVisitorResponse> ReturendModel = new PagedListModel<OragnizationTotalVisitorResponse>();


            ReturendModel.DataList = pagedListModel.DataList.Distinct().GroupBy(r => r.UserMobile).Select
                (r => new OragnizationTotalVisitorResponse() { MobileUser = r.Key, NumberOfVisits = r.Count() }).OrderByDescending(c => c.NumberOfVisits);

            ReturendModel.QueryOptions = pagedListModel.QueryOptions;

            return Ok(ReturendModel);
        }

        [HttpPost]
        [Route("OrganizationVisitorTotalReportFile")]
        [CustomAuthorizationFilter("Evaluation.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> OrganizationVisitorTotalReportFile(OragnizationTotalVisitorRequest oragnizationTotalVisitorRequest)
        {
            var dataList = oragnizationTotalVisitorRequest.BranchId.HasValue ? await _shiftQueueService.FindAsync(v => v.UserBy.OrganizationId == OrganizationId &&
                   v.Shift.BranchId == oragnizationTotalVisitorRequest.BranchId.Value &&
                    v.IsServiceDone == true

                     , "UserMobile",
                     "Shift.Branch"

             ) :
               await _shiftQueueService.FindAsync(v => v.UserBy.OrganizationId == OrganizationId &&
                     v.IsServiceDone == true

                     , "UserMobile",
                     "Shift.Branch"
             );
            var dataListVm = dataList.Distinct().GroupBy(r => r.UserMobile).Select
                (r => new OragnizationTotalVisitorResponse() { MobileUser = r.Key, NumberOfVisits = r.Count() }).OrderByDescending(c => c.NumberOfVisits)
                .AsQueryable();

            var OragnizationTotalVisitorResponseHtml = GetHtmlForOragnizationTotalVisitor(dataListVm);

            var file = await _nodeServices.InvokeAsync<byte[]>("./html-to-pdf", OragnizationTotalVisitorResponseHtml);

            return File(file, "application/pdf", "OragnizationTotalVisitor.pdf");

        }

        [HttpPost]
        [Route("VisitorTotalReport")]
        [CustomAuthorizationFilter("Evaluation.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> VisitorTotalReport(VisitorTotalRequest VisitorTotalRequest)
        {

            PagedListModel<ShiftQueue> pagedListModel = new PagedListModel<ShiftQueue>(VisitorTotalRequest.currentPage, VisitorTotalRequest.pageSize);


            pagedListModel.DataList = VisitorTotalRequest.BranchId.HasValue ?
                await _shiftQueueService.FindAsync(pagedListModel.QueryOptions, v => v.UserBy.OrganizationId == VisitorTotalRequest.OrganizationId &&
                   v.Shift.BranchId == VisitorTotalRequest.BranchId.Value &&
                    v.IsServiceDone == true

                    , "UserMobile",
                    "Shift.Branch"

            ) :
              await _shiftQueueService.FindAsync(pagedListModel.QueryOptions, v => v.UserBy.OrganizationId == VisitorTotalRequest.OrganizationId &&
                    v.IsServiceDone == true

                    , "UserMobile",
                    "Shift.Branch"

            );

            //PagedListModel<OragnizationTotalVisitorResponse> ReturendModel = new PagedListModel<OragnizationTotalVisitorResponse>();
            if (VisitorTotalRequest.DateFrom.HasValue && VisitorTotalRequest.DateTo.HasValue)
            {
                pagedListModel.DataList = pagedListModel.DataList.Where(c => c.CreatedDate >= VisitorTotalRequest.DateFrom.Value
              && c.CreatedDate <= VisitorTotalRequest.DateTo.Value);
            }

            var queryGroup = pagedListModel.DataList.Distinct().GroupBy(r => r.UserMobile).Select
                (r => new OragnizationTotalVisitorResponse() { MobileUser = r.Key, NumberOfVisits = r.Count() }).OrderByDescending(c => c.NumberOfVisits);


            var VisitorResponse = new VisitorResponse()
            {
                NumberOfVisits = queryGroup.Sum(c => c.NumberOfVisits),
                NumberOfUsers = queryGroup.Count()
            };



            return Ok(VisitorResponse);
        }


        [HttpPost]
        [Route("VisitorTotalReportFile")]
        [CustomAuthorizationFilter("Evaluation.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> VisitorTotalReportFile(VisitorTotalRequest VisitorTotalRequest)
        {

            PagedListModel<ShiftQueue> pagedListModel = new PagedListModel<ShiftQueue>(VisitorTotalRequest.currentPage, VisitorTotalRequest.pageSize);


            pagedListModel.DataList = VisitorTotalRequest.BranchId.HasValue ?
                await _shiftQueueService.FindAsync( v => v.UserBy.OrganizationId == VisitorTotalRequest.OrganizationId &&
                   v.Shift.BranchId == VisitorTotalRequest.BranchId.Value &&
                    v.IsServiceDone == true

                    , "UserMobile",
                    "Shift.Branch"

            ) :
              await _shiftQueueService.FindAsync( v => v.UserBy.OrganizationId == VisitorTotalRequest.OrganizationId &&
                    v.IsServiceDone == true

                    , "UserMobile",
                    "Shift.Branch"

            );

            //PagedListModel<OragnizationTotalVisitorResponse> ReturendModel = new PagedListModel<OragnizationTotalVisitorResponse>();


            var queryGroup = pagedListModel.DataList.Distinct().GroupBy(r => r.UserMobile).Select
                (r => new OragnizationTotalVisitorResponse() { MobileUser = r.Key, NumberOfVisits = r.Count() }).OrderByDescending(c => c.NumberOfVisits);


            var VisitorResponse = new VisitorResponse()
            {
                NumberOfVisits = queryGroup.Sum(c => c.NumberOfVisits),
                NumberOfUsers = queryGroup.Count()
            };

            var VisitorResponseHtml = GetHtmlVisitorTotalReport(VisitorResponse);

            var file = await _nodeServices.InvokeAsync<byte[]>("./html-to-pdf", VisitorResponseHtml);
            return File(file, "application/pdf", "OragnizationTotalVisitor.pdf");

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
                (r => new OragnizationDetailVisitorResponse() { MobileUser = r.UserMobile, TellerUser = r.UserBy, Service = r.Service, DateTime = r.CreatedDate});

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


        private string GetHtmlForOragnizationTotalVisitor(IQueryable<OragnizationTotalVisitorResponse> OragnizationTotalVisitorResponses)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(@"
                        <html>
                            <head>
                                <style>
                                header {
                                    text-align: center;
                                    color: green;
                                    padding-bottom: 35px;
                                }

                                table {
                                    width: 80%;
                                    border-collapse: collapse;
                                }

                                td, th {
                                    border: 1px solid gray;
                                    padding: 15px;
                                    font-size: 22px;
                                    text-align: center;
                                }

                                table th {
                                    background-color: green;
                                    color: white;
                                }
                                </style>
                            </head>
                            <body>
                                <div class='header'><h1></h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>First name </th>
                                        <th>Last name</th>
                                        <th>Number Of Visits</th>
                                    </tr>");

            foreach (var vistor in OragnizationTotalVisitorResponses)
            {
                stringBuilder.AppendFormat($@"<tr>
                                    <td>{vistor.MobileUser.FirstName}</td>
                                    <td>{vistor.MobileUser.LastName}</td>
                                    <td>{vistor.NumberOfVisits}</td>
                                  </tr>");
            }

            stringBuilder.Append(@"
                                </table>
                            </body>
                        </html>");

            return stringBuilder.ToString();

        }

        private string GetHtmlVisitorTotalReport(VisitorResponse visitorResponse)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(@"
                        <html>
                            <head>
                                <style>
                                header {
                                    text-align: center;
                                    color: green;
                                    padding-bottom: 35px;
                                }

                                table {
                                    width: 80%;
                                    border-collapse: collapse;
                                }

                                td, th {
                                    border: 1px solid gray;
                                    padding: 15px;
                                    font-size: 22px;
                                    text-align: center;
                                }

                                table th {
                                    background-color: green;
                                    color: white;
                                }
                                </style>
                            </head>
                            <body>
                                <div class='header'><h1></h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Number Of Users</th>
                                        <th>Number Of Visits</th>
                                    </tr>");


            stringBuilder.AppendFormat($@"<tr>
                                    <td>{visitorResponse.NumberOfUsers}</td>
                                    <td>{visitorResponse.NumberOfVisits}</td>
                                  </tr>
                                    </table>
                                    </body>
                                </html>");


            return stringBuilder.ToString();

        }
        #endregion
    }
}