using QReduction.Core.Domain;
using QReduction.Core.Domain.Acl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Api.Models
{
    public class ReportsModels
    {
    }

    public class EvalutionTotalReportRequest
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

       public int currentPage { get; set; } public int pageSize { get; set; }

        public int ServiceId { get; set; }
    }

    public class OragnizationTotalVisitorRequest
    {
       

        public int currentPage { get; set; }
        public int pageSize { get; set; }


        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? BranchId { get; set; }

    }

    public class VisitorTotalRequest
    {


        public int currentPage { get; set; }
        public int pageSize { get; set; }


        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? BranchId { get; set; }

        public int OrganizationId { get; set; }

    }


    public class OragnizationDetailVisitorRequest
    {


        public int currentPage { get; set; }
        public int pageSize { get; set; }

        public int MobileUserId { get; set; }
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

    }

    


    public class OragnizationTotalVisitorResponse
    {


       public User MobileUser { get; set; }

       public int NumberOfVisits { get; set; }

        
    }

    public class VisitorResponse
    {


        public int NumberOfUsers { get; set; }

        public int NumberOfVisits { get; set; }


    }


    public class OragnizationDetailVisitorResponse
    {


        public User MobileUser { get; set; }

        public Service Service { get; set; }

        public User TellerUser { get; set; }

        public DateTime  DateTime { get; set; }

        public int NumberOfVisits { get; set; }


    }
    public class EvalutionTotalReportResponse
    {
        public User UserMobile { get; set; }

        public User TellerUser { get; set; }

        public int EvalutionId { get; set; }

        public string EvalutionPrecentage { get; set; }
    }


    public class EvalutionDetailReportResponse
    {
        public User UserMobile { get; set; }

        public User TellerUser { get; set; }

        public int EvalutionId { get; set; }

        public string EvalutionComment { get; set; }

        public List<EvaluationQuestionAnswer>  evaluationQuestionAnswers { get; set; }
    }

 
}
