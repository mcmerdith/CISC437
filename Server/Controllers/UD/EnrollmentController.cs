using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.AspNetCore.Mvc;
using DOOR.Shared.Utils;
using DOOR.Server.Controllers.Common;
using DOOR.Server.Controllers.UD;

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
        public async Task<IActionResult> GetCourse()
        {
            List<Enrollment> lst = await DatabaseHelper.GetAllObjects(
                _context.Enrollments,
                e => new Enrollment
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
        [Route("GetEnrollment/{_StudentId}/{_SectionNo}")]
        public async Task<IActionResult> GetCourse(int _StudentId, int _SectionNo)
        {
            Enrollment? lst = await DatabaseHelper.GetObject(
                _context.Enrollments,
                x => x.StudentId == _StudentId && x.SectionId == _SectionNo,
                e => new Enrollment
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
        public async Task<IActionResult> PostCourse([FromBody] Enrollment _Enrollment)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.Enrollments,
                    _Enrollment,
                    x => x.StudentId == _Enrollment.StudentId && x.SectionId == _Enrollment.SectionId
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
        public async Task<IActionResult> PutCourse([FromBody] Enrollment _Enrollment)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.Enrollments,
                    x => x.StudentId == _Enrollment.StudentId && x.SectionId == _Enrollment.SectionId,
                    e =>
                    {
                        e.StudentId = _Enrollment.StudentId;
                        e.SectionId = _Enrollment.SectionId;
                        e.EnrollDate = _Enrollment.EnrollDate;
                        e.FinalGrade = _Enrollment.FinalGrade;
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
        [Route("DeleteCourse/{_StudentNo}/{_SectionNo}")]
        public async Task<IActionResult> DeleteCourse(int _StudentNo, int _SectionNo)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.Enrollments,
                    x => x.StudentId == _StudentNo && x.SectionId == _SectionNo
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