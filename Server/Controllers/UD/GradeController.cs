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
    public class GradeController : BaseController
    {
        public GradeController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetGrade")]
        public async Task<IActionResult> GetGrade()
        {
            List<GradeDTO> lst = await DatabaseHelper.GetAllObjects(
                _context.Grades,
                g => new GradeDTO
                {
                    SchoolId = g.SchoolId,
                    StudentId = g.StudentId,
                    SectionId = g.SectionId,
                    GradeTypeCode = g.GradeTypeCode,
                    GradeCodeOccurrence = g.GradeCodeOccurrence,
                    NumericGrade = g.NumericGrade,
                    Comments = g.Comments,
                    CreatedBy = g.CreatedBy,
                    CreatedDate = g.CreatedDate,
                    ModifiedBy = g.ModifiedBy,
                    ModifiedDate = g.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetGrade/{_SchoolId}/{_StudentId}/{_SectionId}/{_GradeTypeCode}/{_GradeCodeOccurrence}")]
        public async Task<IActionResult> GetGrade(int _SchoolId, int _StudentId, int _SectionId, string _GradeTypeCode, byte _GradeCodeOccurrence)
        {
            GradeDTO? lst = await DatabaseHelper.GetObject(
                _context.Grades,
                x => x.SchoolId == _SchoolId && x.StudentId == _StudentId && x.SectionId == _SectionId && x.GradeCodeOccurrence == _GradeCodeOccurrence,
                g => new GradeDTO
                {
                    SchoolId = g.SchoolId,
                    StudentId = g.StudentId,
                    SectionId = g.SectionId,
                    GradeTypeCode = g.GradeTypeCode,
                    GradeCodeOccurrence = g.GradeCodeOccurrence,
                    NumericGrade = g.NumericGrade,
                    Comments = g.Comments,
                    CreatedBy = g.CreatedBy,
                    CreatedDate = g.CreatedDate,
                    ModifiedBy = g.ModifiedBy,
                    ModifiedDate = g.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostGrade")]
        public async Task<IActionResult> PostGrade([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.Grades,
                    x => x.SchoolId == _GradeDTO.SchoolId && x.StudentId == _GradeDTO.StudentId && x.SectionId == _GradeDTO.SectionId && x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence,
                    new Grade
                    {
                        SchoolId = _GradeDTO.SchoolId,
                        StudentId = _GradeDTO.StudentId,
                        SectionId = _GradeDTO.SectionId,
                        GradeTypeCode = _GradeDTO.GradeTypeCode,
                        GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence,
                        NumericGrade = _GradeDTO.NumericGrade,
                        Comments = _GradeDTO.Comments,
                        CreatedBy = _GradeDTO.CreatedBy,
                        CreatedDate = _GradeDTO.CreatedDate,
                        ModifiedBy = _GradeDTO.ModifiedBy,
                        ModifiedDate = _GradeDTO.ModifiedDate,
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
        [Route("PutGrade")]
        public async Task<IActionResult> PutGrade([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.Grades,
                    x => x.SchoolId == _GradeDTO.SchoolId && x.StudentId == _GradeDTO.StudentId && x.SectionId == _GradeDTO.SectionId && x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence,
                    g =>
                    {
                        g.SchoolId = _GradeDTO.SchoolId;
                        g.StudentId = _GradeDTO.StudentId;
                        g.SectionId = _GradeDTO.SectionId;
                        g.GradeTypeCode = _GradeDTO.GradeTypeCode;
                        g.GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence;
                        g.NumericGrade = _GradeDTO.NumericGrade;
                        g.Comments = _GradeDTO.Comments;
                        g.CreatedBy = _GradeDTO.CreatedBy;
                        g.CreatedDate = _GradeDTO.CreatedDate;
                        g.ModifiedBy = _GradeDTO.ModifiedBy;
                        g.ModifiedDate = _GradeDTO.ModifiedDate;
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
        [Route("DeleteGrade/{_SchoolId}/{_StudentId}/{_SectionId}/{_GradeTypeCode}/{_GradeCodeOccurrence}")]
        public async Task<IActionResult> DeleteGrade(int _SchoolId, int _StudentId, int _SectionId, string _GradeTypeCode, byte _GradeCodeOccurrence)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.Grades,
                    x => x.SchoolId == _SchoolId && x.StudentId == _StudentId && x.SectionId == _SectionId && x.GradeCodeOccurrence == _GradeCodeOccurrence
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