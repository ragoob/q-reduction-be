using System;
using System.Collections;
using System.Collections.Generic;

namespace QReduction.Core.Domain
{
    public class Evaluation
    {
        public int Id { get; set; }
        //public int ? EvaluationValue { get; set; }
        public string Comment { get; set; }

        public int ShiftQueueId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public ShiftQueue ShiftQueue { get; set; }

        public virtual ICollection<EvaluationQuestionAnswer> EvaluationQuestionAnswers { get; set; }   

    }
}
