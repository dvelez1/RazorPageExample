using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RazorPageExample.BussinesLogic.ViewModel;
using RazorPageExample.DataAccess.DataAccess;
using RazorPageExample.DataAccess.ModelBuilder;

namespace RazorPageExample.BussinesLogic.Layer
{
    public class PositionsLogicLayer
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public  PositionsLogicLayer(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public  async Task<Tuple<List<PositionsViewModel>, int>> GetAllPositionsModelBuilderAsync()
        {
            List<PositionsViewModel> positionsViewModel = new List<PositionsViewModel>();
            var myObject = new PositionsModelBuilder(_sqlDataAccess);
            var (model,sqlResult) = await myObject.GetAllPositionsModelBuilderAsync();
            foreach (var item in model)
            {
                positionsViewModel.Add(
                    new PositionsViewModel { PositionId = item.PositionId, PositionDescription = item.PositionDescription });
            }

             return new Tuple<List<PositionsViewModel>, int>(positionsViewModel, sqlResult);
        }



        //// Get with parameter
        //var(myViewModel2, sqlTransactionResult2) = myObject.GetPositionsModelBuilderAsync(1).GetAwaiter().GetResult();

        //// Insert
        ////var sqlTransactionResult3 = myObject.InsertPositionsModelBuilderAsync("Human Resrouce Specialist").GetAwaiter().GetResult();

        //// Update
        //var sqlTransactionResult4 = myObject.UpdatePositionsModelBuilderAsync(1, "Human Resrouce Specialist-").GetAwaiter().GetResult();

    }
}
