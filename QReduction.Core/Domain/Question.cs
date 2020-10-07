using System;
using System.Collections;
using System.Collections.Generic;

namespace QReduction.Core.Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionTextAr { get; set; }
        public string QuestionTextEn { get; set; }

        //public string  AnswerValue{ get; set; }
        public int OrganizationId { get; set; }
        public int Code { get; set; }

        public int? CreateBy { get; set; }
        public DateTime CreateAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }

        public Organization Organization { get; set; }

    }
}
