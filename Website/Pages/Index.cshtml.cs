using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Timweb.Models;
using Timweb.Website.Services;

namespace Timweb.Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRequestMaker _requestMaker;

        public IEnumerable<Brand> Brands { get; private set; } = new List<Brand>();

        public IndexModel(ILogger<IndexModel> logger, IRequestMaker requestMaker)
        {
            _logger = logger;
            _requestMaker = requestMaker;
        }

        public async Task OnGet()
        {
            Brands = await _requestMaker.Get<Brand>("brand");
        }
    }
}