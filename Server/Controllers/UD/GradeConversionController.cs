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
    public class GradeConversionController : BaseController
    {
        public GradeConversionController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetGradeConversion")]
        public async Task<IActionResult> GetGradeConversion()
        {
            List<GradeConversionDTO> lst = await DatabaseHelper.GetAllObjects(
                _context.GradeConversions,
                gc => new GradeConversionDTO
                {
                    SchoolId = gc.SchoolId,
                    LetterGrade = gc.LetterGrade,
                    GradePoint = gc.GradePoint,
                    MaxGrade = gc.MaxGrade,
                    MinGrade = gc.MinGrade,
                    CreatedBy = gc.CreatedBy,
                    CreatedDate = gc.CreatedDate,
                    ModifiedBy = gc.ModifiedBy,
                    ModifiedDate = gc.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetGradeConversion/{_SchoolId}/{_LetterGrade}")]
        public async Task<IActionResult> GetGradeConversion(int _SchoolId, string _LetterGrade)
        {
            GradeConversionDTO? lst = await DatabaseHelper.GetObject(
                _context.GradeConversions,
                x => x.SchoolId == _SchoolId && x.LetterGrade == _LetterGrade,
                gc => new GradeConversionDTO
                {
                    SchoolId = gc.SchoolId,
                    LetterGrade = gc.LetterGrade,
                    GradePoint = gc.GradePoint,
                    MaxGrade = gc.MaxGrade,
                    MinGrade = gc.MinGrade,
                    CreatedBy = gc.CreatedBy,
                    CreatedDate = gc.CreatedDate,
                    ModifiedBy = gc.ModifiedBy,
                    ModifiedDate = gc.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostGradeConversion")]
        public async Task<IActionResult> PostGradeConversion([FromBody] GradeConversionDTO _GradeConversionDTO)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.GradeConversions,
                    x => x.SchoolId == _GradeConversionDTO.SchoolId && x.LetterGrade == _GradeConversionDTO.LetterGrade,
                    new GradeConversion
                    {
                        SchoolId = _GradeConversionDTO.SchoolId,
                        LetterGrade = _GradeConversionDTO.LetterGrade,
                        GradePoint = _GradeConversionDTO.GradePoint,
                        MaxGrade = _GradeConversionDTO.MaxGrade,
                        MinGrade = _GradeConversionDTO.MinGrade,
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
        [Route("PutGradeConversion")]
        public async Task<IActionResult> PutGradeConversion([FromBody] GradeConversionDTO _GradeConversionDTO)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.GradeConversions,
                    x => x.SchoolId == _GradeConversionDTO.SchoolId && x.LetterGrade == _GradeConversionDTO.LetterGrade,
                    gc =>
                    {
                        gc.SchoolId = _GradeConversionDTO.SchoolId;
                        gc.LetterGrade = _GradeConversionDTO.LetterGrade;
                        gc.GradePoint = _GradeConversionDTO.GradePoint;
                        gc.MaxGrade = _GradeConversionDTO.MaxGrade;
                        gc.MinGrade = _GradeConversionDTO.MinGrade;
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
        [Route("DeleteGradeConversion/{_SchoolId}/{_LetterGrade}")]
        public async Task<IActionResult> DeleteGradeConversion(int _SchoolId, string _LetterGrade)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.GradeConversions,
                    x => x.SchoolId == _SchoolId && x.LetterGrade == _LetterGrade
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