using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageExample.DataAccess.DataAccess
{
    public interface ISqlDataAccess
    {
        public DataTable GetDataTableWithoutParameters(string sqlQuery);
    }
}
