using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text.Json;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Http.Headers;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using DOOR.Server.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Data;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Numerics;
using DOOR.Shared.DTO;
using DOOR.Shared.Utils;
using DOOR.Server.Controllers.Common;

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
            List<Enrollment> lst = await _context.Enrollments
                .Select(sp => new Enrollment
                {
                    StudentId = sp.StudentId,
                    SectionId = sp.SectionId,
                    EnrollDate = sp.EnrollDate,
                    FinalGrade = sp.FinalGrade,
                    SchoolId = sp.SchoolId,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetEnrollment/{_StudenNo}/{_SectionNo}")]
        public async Task<IActionResult> GetCourse(int _StudentNo, int _SectionNo)
        {
            Enrollment? lst = await _context.Enrollments
                .Where(x => x.StudentId == _StudentNo && x.SectionId == _SectionNo)
                .Select(sp => new Enrollment
                {
                    StudentId=sp.StudentId,
                    SectionId=sp.SectionId,
                    EnrollDate = sp.EnrollDate,
                    FinalGrade = sp.FinalGrade,
                    SchoolId = sp.SchoolId,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostEnrollment")]
        public async Task<IActionResult> PostCourse([FromBody] Enrollment _Enrollment)
        {
            try
            {
                Enrollment c = await _context.Enrollments.Where(x => x.StudentId == _Enrollment.StudentId && x.SectionId == _Enrollment.SectionId).FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Enrollment
                    {
                        Cost = _Enrollment.Cost,
                        Description = _Enrollment.Description,
                        Prerequisite = _Enrollment.Prerequisite
                    };
                    _context.Courses.Add(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }








        [HttpPut]
        [Route("PutCourse")]
        public async Task<IActionResult> PutCourse([FromBody] CourseDTO _CourseDTO)
        {
            try
            {
                Course c = await _context.Courses.Where(x => x.CourseNo == _CourseDTO.CourseNo).FirstOrDefaultAsync();

                if (c != null)
                {
                    c.Description = _CourseDTO.Description;
                    c.Cost = _CourseDTO.Cost;
                    c.Prerequisite = _CourseDTO.Prerequisite;

                    _context.Courses.Update(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }


        [HttpDelete]
        [Route("DeleteCourse/{_CourseNo}")]
        public async Task<IActionResult> DeleteCourse(int _CourseNo)
        {
            try
            {
                Course c = await _context.Courses.Where(x => x.CourseNo == _CourseNo).FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.Courses.Remove(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }



    }
}