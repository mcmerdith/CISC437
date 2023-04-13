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
    public class StudentController : BaseController
    {
        public StudentController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetStudent")]
        public async Task<IActionResult> GetStudent()
        {
            List<StudentDTO> lst = await DatabaseHelper.GetAllObjects(
                _context.Students,
                s => new StudentDTO
                {
                    StudentId = s.StudentId,
                    Salutation = s.Salutation,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    StreetAddress = s.StreetAddress,
                    Zip = s.Zip,
                    Phone = s.Phone,
                    Employer = s.Employer,
                    RegistrationDate = s.RegistrationDate,
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
        [Route("GetStudent/{_StudentId}/{_SchoolId}")]
        public async Task<IActionResult> GetStudent(int _StudentId, int _SchoolId)
        {
            StudentDTO? lst = await DatabaseHelper.GetObject(
                _context.Students,
                x => x.StudentId == _StudentId && x.SchoolId == _SchoolId,
                s => new StudentDTO
                {
                    StudentId = s.StudentId,
                    Salutation = s.Salutation,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    StreetAddress = s.StreetAddress,
                    Zip = s.Zip,
                    Phone = s.Phone,
                    Employer = s.Employer,
                    RegistrationDate = s.RegistrationDate,
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
        [Route("PostStudent")]
        public async Task<IActionResult> PostStudent([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.Students,
                    x => x.StudentId == _StudentDTO.StudentId && x.SchoolId == _StudentDTO.SchoolId,
                    new Student
                    {
                        StudentId = _StudentDTO.StudentId,
                        Salutation = _StudentDTO.Salutation,
                        FirstName = _StudentDTO.FirstName,
                        LastName = _StudentDTO.LastName,
                        StreetAddress = _StudentDTO.StreetAddress,
                        Zip = _StudentDTO.Zip,
                        Phone = _StudentDTO.Phone,
                        Employer = _StudentDTO.Employer,
                        RegistrationDate = _StudentDTO.RegistrationDate,
                        SchoolId = _StudentDTO.SchoolId,
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
        [Route("PutStudent")]
        public async Task<IActionResult> PutStudent([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.Students,
                    x => x.StudentId == _StudentDTO.StudentId && x.SchoolId == _StudentDTO.SchoolId,
                    s =>
                    {
                        s.StudentId = _StudentDTO.StudentId;
                        s.Salutation = _StudentDTO.Salutation;
                        s.FirstName = _StudentDTO.FirstName;
                        s.LastName = _StudentDTO.LastName;
                        s.StreetAddress = _StudentDTO.StreetAddress;
                        s.Zip = _StudentDTO.Zip;
                        s.Phone = _StudentDTO.Phone;
                        s.Employer = _StudentDTO.Employer;
                        s.RegistrationDate = _StudentDTO.RegistrationDate;
                        s.SchoolId = _StudentDTO.SchoolId;
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
        [Route("DeleteStudent/{_StudentId}/{_SchoolId}")]
        public async Task<IActionResult> DeleteStudent(int _StudentId, int _SchoolId)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.Students,
                    x => x.StudentId == _StudentId && x.SchoolId == _SchoolId
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