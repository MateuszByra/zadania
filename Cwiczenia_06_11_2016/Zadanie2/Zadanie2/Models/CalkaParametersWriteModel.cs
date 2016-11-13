using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2.Models
{
    public class CalkaParametersWriteModel : CalkaParametersReadModel
    {
        public double Result { get; set; }
        public double Miliseconds { get; set; }
    }
}
