using System;
using System.Collections;
using System.Collections.Generic;

namespace QReduction.Core.Domain
{
    public class EvaluationQuestionAnswer
    {
        public int Id { get; set; }
      
        public int? EvaluationId { get; set; }

        public int? QuestionId { get; set; }

        public int? AnswerValue { get; set; }

        public Evaluation Evaluation { get; set; }

        public Question Question { get; set; }


    }
}
