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
    public class GradeTypeController : BaseController
    {
        public GradeTypeController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetGradeType")]
        public async Task<IActionResult> GetGradeType()
        {
            List<GradeTypeDTO> lst = await DatabaseHelper.GetAllObjects(
                _context.GradeTypes,
                gt => new GradeTypeDTO
                {
                    SchoolId = gt.SchoolId,
                    GradeTypeCode = gt.GradeTypeCode,
                    Description = gt.Description,
                    CreatedBy = gt.CreatedBy,
                    CreatedDate = gt.CreatedDate,
                    ModifiedBy = gt.ModifiedBy,
                    ModifiedDate = gt.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetGradeType/{_SchoolId}/{_GradeTypeCode}")]
        public async Task<IActionResult> GetGradeType(int _SchoolId, string _GradeTypeCode)
        {
            GradeTypeDTO? lst = await DatabaseHelper.GetObject(
                _context.GradeTypes,
                x => x.SchoolId == _SchoolId && x.GradeTypeCode == _GradeTypeCode,
                gt => new GradeTypeDTO
                {
                    SchoolId = gt.SchoolId,
                    GradeTypeCode = gt.GradeTypeCode,
                    Description = gt.Description,
                    CreatedBy = gt.CreatedBy,
                    CreatedDate = gt.CreatedDate,
                    ModifiedBy = gt.ModifiedBy,
                    ModifiedDate = gt.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostGradeType")]
        public async Task<IActionResult> PostGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.GradeTypes,
                    x => x.SchoolId == _GradeTypeDTO.SchoolId && x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode,
                    new GradeType
                    {
                        SchoolId = _GradeTypeDTO.SchoolId,
                        GradeTypeCode = _GradeTypeDTO.GradeTypeCode,
                        Description = _GradeTypeDTO.Description,
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
        [Route("PutGradeType")]
        public async Task<IActionResult> PutGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.GradeTypes,
                    x => x.SchoolId == _GradeTypeDTO.SchoolId && x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode,
                    gt =>
                    {
                        gt.SchoolId = _GradeTypeDTO.SchoolId;
                        gt.GradeTypeCode = _GradeTypeDTO.GradeTypeCode;
                        gt.Description = _GradeTypeDTO.Description;
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
        [Route("DeleteGradeType/{_SchoolId}/{_GradeTypeCode}")]
        public async Task<IActionResult> DeleteGradeType(int _SchoolId, string _GradeTypeCode)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.GradeTypes,
                    x => x.SchoolId == _SchoolId && x.GradeTypeCode == _GradeTypeCode
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