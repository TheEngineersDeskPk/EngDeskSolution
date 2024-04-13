using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Models
{
    public  class QnABank
    {

        public string ?  Question { get; set; }

        public List<Answers> ? Answers { get; set; }
    }
}
