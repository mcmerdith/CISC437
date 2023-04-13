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
    public class EnrollmentController : BaseController
    {
        public EnrollmentController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetEnrollment")]
        public async Task<IActionResult> GetEnrollment()
        {
            List<EnrollmentDTO> lst = await DatabaseHelper.GetAllObjects(
                _context.Enrollments,
                e => new EnrollmentDTO
                {
                    StudentId = e.StudentId,
                    SectionId = e.SectionId,
                    EnrollDate = e.EnrollDate,
                    FinalGrade = e.FinalGrade,
                    SchoolId = e.SchoolId,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,
                    ModifiedBy = e.ModifiedBy,
                    ModifiedDate = e.ModifiedDate
                }
            );
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetEnrollment/{_StudentId}/{_SectionId}")]
        public async Task<IActionResult> GetEnrollment(int _StudentId, int _SectionId)
        {
            EnrollmentDTO? lst = await DatabaseHelper.GetObject(
                _context.Enrollments,
                x => x.StudentId == _StudentId && x.SectionId == _SectionId,
                e => new EnrollmentDTO
                {
                    StudentId = e.StudentId,
                    SectionId = e.SectionId,
                    EnrollDate = e.EnrollDate,
                    FinalGrade = e.FinalGrade,
                    SchoolId = e.SchoolId,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,
                    ModifiedBy = e.ModifiedBy,
                    ModifiedDate = e.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostEnrollment")]
        public async Task<IActionResult> PostEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.Enrollments,
                    x => x.StudentId == _EnrollmentDTO.StudentId && x.SectionId == _EnrollmentDTO.SectionId,
                    new Enrollment
                    {
                        StudentId = _EnrollmentDTO.StudentId,
                        SectionId = _EnrollmentDTO.SectionId,
                        EnrollDate = _EnrollmentDTO.EnrollDate,
                        FinalGrade = _EnrollmentDTO.FinalGrade,
                        SchoolId = _EnrollmentDTO.SchoolId,
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
        [Route("PutEnrollment")]
        public async Task<IActionResult> PutEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.Enrollments,
                    x => x.StudentId == _EnrollmentDTO.StudentId && x.SectionId == _EnrollmentDTO.SectionId,
                    e =>
                    {
                        e.StudentId = _EnrollmentDTO.StudentId;
                        e.SectionId = _EnrollmentDTO.SectionId;
                        e.EnrollDate = _EnrollmentDTO.EnrollDate;
                        e.FinalGrade = _EnrollmentDTO.FinalGrade;
                        e.SchoolId = _EnrollmentDTO.SchoolId;
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
        [Route("DeleteEnrollment/{_StudentId}/{_SectionId}")]
        public async Task<IActionResult> DeleteEnrollment(int _StudentId, int _SectionId)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.Enrollments,
                    x => x.StudentId == _StudentId && x.SectionId == _SectionId
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