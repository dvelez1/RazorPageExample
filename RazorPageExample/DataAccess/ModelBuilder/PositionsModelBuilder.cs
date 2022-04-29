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
        private readonly IDataAccess _dataAccess;

        public PositionsModelBuilder(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Model.Positions> GetAllPositionsModelBuilder()
        {
            List<Model.Positions> list = new List<Model.Positions>();
            DataTable dt = _dataAccess.GetDataTableWithoutParameters("Select * from Employees.Positions");
            list = dt.AsEnumerable().Select(s => new Model.Positions()
            {
                PositionId = s.Field<int>("PositionId"),
                PositionDescription = s.Field<string>("PositionDescription")
            }).ToList();

            return list;
        }

    }
}
