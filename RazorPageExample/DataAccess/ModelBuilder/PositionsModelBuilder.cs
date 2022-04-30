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

        public async Task<Tuple<List<Model.Positions>, int>> GetAllPositionsModelBuilderAsync()
        {
            List<Model.Positions> dataModelList = new List<Model.Positions>();
            var (dt, sqlTransactionResult) = await _sqlDataAccess.GetDataTableWithoutParametersAsync("select * from [dbo].[Positions]");
            dataModelList = dt.AsEnumerable().Select(s => new Model.Positions()
            {
                PositionId = s.Field<int>("PositionId"),
                PositionDescription = s.Field<string>("PositionDescription")
            }).ToList();

             return new Tuple<List<Model.Positions>, int>(dataModelList, sqlTransactionResult); 
        }

        public async Task<Tuple<List<Model.Positions>, int>> GetPositionsModelBuilderAsync(int id)
        {
            List<Model.Positions> dataModelList = new List<Model.Positions>();
            List<(string ParameterName, object Value)> parameters = new()
            {
                ("@PositionId",id)
            };

            var (dt, sqlTransactionResult) = await _sqlDataAccess.GetDataTableWithParametersAsync("select * from [dbo].[Positions] Where PositionId = @PositionId", parameters);
            dataModelList = dt.AsEnumerable().Select(s => new Model.Positions()
            {
                PositionId = s.Field<int>("PositionId"),
                PositionDescription = s.Field<string>("PositionDescription")
            }).ToList();

            return new Tuple<List<Model.Positions>, int>(dataModelList, sqlTransactionResult);
        }
    }
}
