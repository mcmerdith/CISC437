using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.AspNetCore.Mvc;
using DOOR.Shared.Utils;
using DOOR.Server.Controllers.Common;
using DOOR.Server.Controllers.UD;
using DOOR.Shared.DTO;
using static Duende.IdentityServer.Models.IdentityResources;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : BaseController
    {
        public InstructorController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetInstructor")]
        public async Task<IActionResult> GetInstructor()
        {
            List<InstructorDTO> lst = await DatabaseHelper.GetAllObjects(
                _context.Instructors,
                i => new InstructorDTO
                {
                    SchoolId = i.SchoolId,
                    InstructorId = i.InstructorId,
                    Salutation = i.Salutation,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    StreetAddress = i.StreetAddress,
                    Zip = i.Zip,
                    Phone = i.Phone,
                    CreatedBy = i.CreatedBy,
                    CreatedDate = i.CreatedDate,
                    ModifiedBy = i.ModifiedBy,
                    ModifiedDate = i.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetInstructor/{_SchoolId}/{_InstructorId}")]
        public async Task<IActionResult> GetInstructor(int _SchoolId, int _InstructorId)
        {
            InstructorDTO? lst = await DatabaseHelper.GetObject(
                _context.Instructors,
                x => x.SchoolId == _SchoolId && x.InstructorId == _InstructorId,
                i => new InstructorDTO
                {
                    SchoolId = i.SchoolId,
                    InstructorId = i.InstructorId,
                    Salutation = i.Salutation,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    StreetAddress = i.StreetAddress,
                    Zip = i.Zip,
                    Phone = i.Phone,
                    CreatedBy = i.CreatedBy,
                    CreatedDate = i.CreatedDate,
                    ModifiedBy = i.ModifiedBy,
                    ModifiedDate = i.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostInstructor")]
        public async Task<IActionResult> PostInstructor([FromBody] InstructorDTO _InstructorDTO)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.Instructors,
                    x => x.SchoolId == _InstructorDTO.SchoolId && x.InstructorId == _InstructorDTO.InstructorId,
                    new Instructor
                    {
                        SchoolId = _InstructorDTO.SchoolId,
                        InstructorId = _InstructorDTO.InstructorId,
                        Salutation = _InstructorDTO.Salutation,
                        FirstName = _InstructorDTO.FirstName,
                        LastName = _InstructorDTO.LastName,
                        StreetAddress = _InstructorDTO.StreetAddress,
                        Zip = _InstructorDTO.Zip,
                        Phone = _InstructorDTO.Phone,
                        CreatedBy = _InstructorDTO.CreatedBy,
                        CreatedDate = _InstructorDTO.CreatedDate,
                        ModifiedBy = _InstructorDTO.ModifiedBy,
                        ModifiedDate = _InstructorDTO.ModifiedDate,
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
        [Route("PutInstructor")]
        public async Task<IActionResult> PutInstructor([FromBody] InstructorDTO _InstructorDTO)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.Instructors,
                    x => x.SchoolId == _InstructorDTO.SchoolId && x.InstructorId == _InstructorDTO.InstructorId,
                    i =>
                    {
                        i.SchoolId = _InstructorDTO.SchoolId;
                        i.InstructorId = _InstructorDTO.InstructorId;
                        i.Salutation = _InstructorDTO.Salutation;
                        i.FirstName = _InstructorDTO.FirstName;
                        i.LastName = _InstructorDTO.LastName;
                        i.StreetAddress = _InstructorDTO.StreetAddress;
                        i.Zip = _InstructorDTO.Zip;
                        i.Phone = _InstructorDTO.Phone;
                        i.CreatedBy = _InstructorDTO.CreatedBy;
                        i.CreatedDate = _InstructorDTO.CreatedDate;
                        i.ModifiedBy = _InstructorDTO.ModifiedBy;
                        i.ModifiedDate = _InstructorDTO.ModifiedDate;
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
        [Route("DeleteInstructor/{_SchoolId}/{_InstructorId}")]
        public async Task<IActionResult> DeleteInstructor(int _SchoolId, int _InstructorId)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.Instructors,
                    x => x.SchoolId == _SchoolId && x.InstructorId == _InstructorId
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