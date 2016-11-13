using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie2.Models;

namespace Zadanie2
{
    public class CalkaParametersWriteList
    {
        public string Type { get; set; }
        public List<CalkaParametersWriteModel> Data { get; set; }

        public CalkaParametersWriteList()
        {
            Data = new List<CalkaParametersWriteModel>();
        }
    }
}
