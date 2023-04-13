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
    public class CourseController : BaseController
    {
        public CourseController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetCourse")]
        public async Task<IActionResult> GetCourse()
        {
            List<CourseDTO> lst = await DatabaseHelper.GetAllObjects(
                _context.Courses,
                c => new CourseDTO
                {
                    Cost = c.Cost,
                    CourseNo = c.CourseNo,
                    Description = c.Description,
                    Prerequisite = c.Prerequisite,
                    SchoolId = c.SchoolId,
                    PrerequisiteSchoolId = c.PrerequisiteSchoolId,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                    ModifiedBy = c.ModifiedBy,
                    ModifiedDate = c.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetCourse/{_CourseNo}")]
        public async Task<IActionResult> GetCourse(int _CourseNo)
        {
            CourseDTO? lst = await DatabaseHelper.GetObject(
                _context.Courses,
                x => x.CourseNo == _CourseNo,
                c => new CourseDTO
                {
                    Cost = c.Cost,
                    CourseNo = c.CourseNo,
                    CreatedBy = c.CreatedBy,
                    Description = c.Description,
                    Prerequisite = c.Prerequisite,
                    SchoolId = c.SchoolId,
                    PrerequisiteSchoolId = c.PrerequisiteSchoolId,
                    CreatedDate = c.CreatedDate,
                    ModifiedBy = c.ModifiedBy,
                    ModifiedDate = c.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostCourse")]
        public async Task<IActionResult> PostCourse([FromBody] CourseDTO _CourseDTO)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.Courses,
                    x => x.CourseNo == _CourseDTO.CourseNo,
                    new Course
                    {
                        Cost = _CourseDTO.Cost,
                        Description = _CourseDTO.Description,
                        Prerequisite = _CourseDTO.Prerequisite,
                        SchoolId = _CourseDTO.SchoolId,
                        PrerequisiteSchoolId = _CourseDTO.PrerequisiteSchoolId,
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
        [Route("PutCourse")]
        public async Task<IActionResult> PutCourse([FromBody] CourseDTO _CourseDTO)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.Courses,
                    x => x.CourseNo == _CourseDTO.CourseNo,
                    c =>
                    {
                        c.Cost = _CourseDTO.Cost;
                        c.Description = _CourseDTO.Description;
                        c.Prerequisite = _CourseDTO.Prerequisite;
                        c.SchoolId = _CourseDTO.SchoolId;
                        c.PrerequisiteSchoolId = _CourseDTO.PrerequisiteSchoolId;
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
        [Route("DeleteCourse/{_CourseNo}")]
        public async Task<IActionResult> DeleteCourse(int _CourseNo)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.Courses,
                    x => x.CourseNo == _CourseNo
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