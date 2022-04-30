using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorPageExample.DataAccess.ModelBuilder;
using RazorPageExample.DataAccess.DataAccess;

namespace RazorPageExample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ISqlDataAccess _sqlDataAccess;

        public IndexModel(ILogger<IndexModel> logger, ISqlDataAccess sqlDataAccess)
        {
            _logger = logger;
            _sqlDataAccess = sqlDataAccess; 
        }

        public void OnGet()
        {
            var myObject = new PositionsModelBuilder(_sqlDataAccess);
            //Simple Get
            var (myViewModel, sqlTransactionResult) = myObject.GetAllPositionsModelBuilderAsync().GetAwaiter().GetResult();
            // Get with parameter
            var (myViewModel2, sqlTransactionResult2) = myObject.GetPositionsModelBuilderAsync(1).GetAwaiter().GetResult();
        }
    }
}
