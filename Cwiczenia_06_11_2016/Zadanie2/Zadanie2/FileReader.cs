using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    public class FileReader
    {
        public CalkaParametersList GetParametersListFromFile(string path)
        {
            var list = new CalkaParametersList();
            using (StreamReader reader = new StreamReader(path))
            {
                var data = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<CalkaParametersList>(data);
            }

            return list;
        }
    }
}
