using System.Collections.Generic;
using System.Threading.Tasks;
using gorila_cdb.IService;
using gorila_cdb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace gorila_cdb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly gorila_cdbContext _context;
        private IEnumerable<CdbOutput> _cdbOutput;

        private readonly ICdbOutputService _iCdbOutputService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, gorila_cdbContext context, ICdbOutputService iCdbOutputService)
        {
            _logger = logger;
            _context = context;
            _iCdbOutputService = iCdbOutputService;
        }

        // POST: api/Home
        [HttpPost]
        public IActionResult PostCdbInput(CdbInput cdbInput)
        {
            IList<Error> error = _iCdbOutputService.Validate(cdbInput);

            if(error.Count == 0)
            {
                _context.CdbInput.Add(cdbInput);
                _context.SaveChangesAsync();

                _cdbOutput = _iCdbOutputService.GetResult();
                return CreatedAtAction(nameof(GetCdbOutput), _cdbOutput);
            }
            else
            {
                return BadRequest(error);
            }
        }

        // GET: api/Home
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CdbOutput>>> GetCdbOutput()
        {
            return await _context.CdbOutput.ToListAsync();
        }
    }
}
