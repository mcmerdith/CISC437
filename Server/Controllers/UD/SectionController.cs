using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.AspNetCore.Mvc;
using DOOR.Shared.Utils;
using DOOR.Server.Controllers.Common;
using DOOR.Server.Controllers.UD;
using DOOR.Shared.DTO;
using static System.Collections.Specialized.BitVector32;
using Section = DOOR.EF.Models.Section;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectionController : BaseController
    {
        public SectionController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetSection")]
        public async Task<IActionResult> GetSection()
        {
            List<SectionDTO> lst = await DatabaseHelper.GetAllObjects(
                _context.Sections,
                s => new SectionDTO
                {
                    SectionId = s.SectionId,
                    CourseNo = s.CourseNo,
                    SectionNo = s.SectionNo,
                    StartDateTime = s.StartDateTime,
                    Location = s.Location,
                    InstructorId = s.InstructorId,
                    Capacity = s.Capacity,
                    SchoolId = s.SchoolId,
                    CreatedBy = s.CreatedBy,
                    CreatedDate = s.CreatedDate,
                    ModifiedBy = s.ModifiedBy,
                    ModifiedDate = s.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetSection/{_SectionId}")]
        public async Task<IActionResult> GetSection(int _SectionId)
        {
            SectionDTO? lst = await DatabaseHelper.GetObject(
                _context.Sections,
                x => x.SectionNo == _SectionId,
                s => new SectionDTO
                {
                    SectionId = s.SectionId,
                    CourseNo = s.CourseNo,
                    SectionNo = s.SectionNo,
                    StartDateTime = s.StartDateTime,
                    Location = s.Location,
                    InstructorId = s.InstructorId,
                    Capacity = s.Capacity,
                    SchoolId = s.SchoolId,
                    CreatedBy = s.CreatedBy,
                    CreatedDate = s.CreatedDate,
                    ModifiedBy = s.ModifiedBy,
                    ModifiedDate = s.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostSection")]
        public async Task<IActionResult> PostSection([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.Sections,
                    x => x.SectionNo == _SectionDTO.SectionNo,
                    new Section
                    {
                        SectionId = _SectionDTO.SectionId,
                        CourseNo = _SectionDTO.CourseNo,
                        SectionNo = _SectionDTO.SectionNo,
                        StartDateTime = _SectionDTO.StartDateTime,
                        Location = _SectionDTO.Location,
                        InstructorId = _SectionDTO.InstructorId,
                        Capacity = _SectionDTO.Capacity,
                        SchoolId = _SectionDTO.SchoolId,
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
        [Route("PutSection")]
        public async Task<IActionResult> PutSection([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.Sections,
                    x => x.SectionNo == _SectionDTO.SectionNo,
                    s =>
                    {
                        s.SectionId = _SectionDTO.SectionId;
                        s.CourseNo = _SectionDTO.CourseNo;
                        s.SectionNo = _SectionDTO.SectionNo;
                        s.StartDateTime = _SectionDTO.StartDateTime;
                        s.Location = _SectionDTO.Location;
                        s.InstructorId = _SectionDTO.InstructorId;
                        s.Capacity = _SectionDTO.Capacity;
                        s.SchoolId = _SectionDTO.SchoolId;
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
        [Route("DeleteSection/{_SectionId}")]
        public async Task<IActionResult> DeleteSection(int _SectionId)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.Sections,
                    x => x.SectionNo == _SectionId
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