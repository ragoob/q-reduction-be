namespace QReduction.QReduction.Infrastructure.DbMappings.Domain.Controllers
{
    public class QuestionVM
    {
        public int Id { get; set; }
        public string QuestionTextAr { get; set; }
        public string QuestionTextEn { get; set; }

        public int OrganizationId { get; set; }
        public int Code { get; set; }
        public int AnswerValue { get; set; }
    }
}