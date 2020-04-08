using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gorila_cdb.Models;
using gorila_cdb.IService;

namespace gorila_cdb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly cdbContext _context;
        private IEnumerable<CdbOutput> _cdbOutput;

        private readonly ICdbOutputService _iCdbOutputService;

        public HomeController(cdbContext context, ICdbOutputService iCdbOutputService)
        {
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
