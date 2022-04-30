using RazorPageExample.DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageExample.DataAccess.ModelBuilder
{
    
    public class PositionsModelBuilder
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public PositionsModelBuilder(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public List<Model.Positions> GetAllPositionsModelBuilder()
        {
            List<Model.Positions> list = new List<Model.Positions>();
            DataTable dt = _sqlDataAccess.GetDataTableWithoutParameters("select * from [dbo].[Positions]");
            list = dt.AsEnumerable().Select(s => new Model.Positions()
            {
                PositionId = s.Field<int>("PositionId"),
                PositionDescription = s.Field<string>("PositionDescription")
            }).ToList();

            return list;
        }

    }
}
