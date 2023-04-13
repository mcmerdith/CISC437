using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.AspNetCore.Mvc;
using DOOR.Shared.Utils;
using DOOR.Server.Controllers.Common;
using DOOR.Server.Controllers.UD;
using DOOR.Shared.DTO;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : BaseController
    {
        public SchoolController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetSchool")]
        public async Task<IActionResult> GetSchool()
        {
            List<SchoolDTO> lst = await DatabaseHelper.GetAllObjects(
                _context.Schools,
                s => new SchoolDTO
                {
                    SchoolId = s.SchoolId,
                    SchoolName = s.SchoolName,
                    CreatedBy = s.CreatedBy,
                    CreatedDate = s.CreatedDate,
                    ModifiedBy = s.ModifiedBy,
                    ModifiedDate = s.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetSchool/{_SchoolId}")]
        public async Task<IActionResult> GetSchool(int _SchoolId)
        {
            SchoolDTO? lst = await DatabaseHelper.GetObject(
                _context.Schools,
                x => x.SchoolId == _SchoolId,
                s => new SchoolDTO
                {
                    SchoolId = s.SchoolId,
                    SchoolName = s.SchoolName,
                    CreatedBy = s.CreatedBy,
                    CreatedDate = s.CreatedDate,
                    ModifiedBy = s.ModifiedBy,
                    ModifiedDate = s.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostSchool")]
        public async Task<IActionResult> PostSchool([FromBody] SchoolDTO _SchoolDTO)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.Schools,
                    x => x.SchoolId == _SchoolDTO.SchoolId,
                    new School
                    {
                        SchoolId = _SchoolDTO.SchoolId,
                        SchoolName = _SchoolDTO.SchoolName,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status417ExpectationFailed,
                    ErrorHelper.HandleDBException(_context, _OraTranslateMsgs, ex)
                );
            }

            return Ok();
        }

        [HttpPut]
        [Route("PutSchool")]
        public async Task<IActionResult> PutSchool([FromBody] SchoolDTO _SchoolDTO)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.Schools,
                    x => x.SchoolId == _SchoolDTO.SchoolId,
                    s =>
                    {
                        s.SchoolId = _SchoolDTO.SchoolId;
                        s.SchoolName = _SchoolDTO.SchoolName;
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("DeleteSchool/{_SchoolId}")]
        public async Task<IActionResult> DeleteSchool(int _SchoolId)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.Schools,
                    x => x.SchoolId == _SchoolId
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status417ExpectationFailed,
                    ErrorHelper.HandleDBException(_context, _OraTranslateMsgs, ex)
                );
            }

            return Ok();
        }
    }
}