using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorPageExample.BussinesLogic;
using RazorPageExample.BussinesLogic.ViewModel;
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

        [BindProperty]
        public List<PositionsViewModel> positions { get; set; }

        public void OnGet()
        {
            try
            {
                var (_positions, errorIndicatorId) = new BussinesLogic.Layer.PositionsLogicLayer(_sqlDataAccess).GetAllPositionsModelBuilderAsync().GetAwaiter().GetResult();

                this.positions = _positions;
                if (errorIndicatorId < 0) { RedirectToPage("Errors/Error404"); }
            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                
            }
            
        }
    }
}
