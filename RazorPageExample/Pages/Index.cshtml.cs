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
        private readonly IDataAccess _dataAccess;

        public IndexModel(ILogger<IndexModel> logger, IDataAccess dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess; 
        }

        public void OnGet()
        {
            var list = new RazorPageExample.DataAccess.ModelBuilder.PositionsModelBuilder(_dataAccess);
            list.GetAllPositionsModelBuilder();
        }
    }
}
