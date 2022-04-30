using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageExample.DataAccess.DataAccess
{
    public interface ISqlDataAccess
    {
        public Task<Tuple<DataTable,int>> GetDataTableWithoutParametersAsync(string sqlQuery);
        public Task<Tuple<DataTable, int>> GetDataTableWithParametersAsync(string sqlQuery, List<(string parameterName,object value)> parameters);
    }
}
