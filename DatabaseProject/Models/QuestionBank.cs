using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Models
{
    public class QuestionBank
    {
        public int QuestionBankId { get; set; }
        public int QuestionId { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public bool isCorrect { get; set; }
    }
}
