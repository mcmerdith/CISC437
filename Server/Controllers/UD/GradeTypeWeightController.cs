using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.AspNetCore.Mvc;
using DOOR.Shared.Utils;
using DOOR.Server.Controllers.Common;
using DOOR.Server.Controllers.UD;
using DOOR.Shared.DTO;
using static System.Collections.Specialized.BitVector32;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeTypeWeightController : BaseController
    {
        public GradeTypeWeightController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetGradeTypeWeight")]
        public async Task<IActionResult> GetGradeTypeWeight()
        {
            List<GradeTypeWeightDTO> lst = await DatabaseHelper.GetAllObjects(
                _context.GradeTypeWeights,
                gtw => new GradeTypeWeightDTO
                {
                    SchoolId = gtw.SchoolId,
                    SectionId = gtw.SectionId,
                    GradeTypeCode = gtw.GradeTypeCode,
                    NumberPerSection = gtw.NumberPerSection,
                    PercentOfFinalGrade = gtw.PercentOfFinalGrade,
                    DropLowest = gtw.DropLowest,
                    CreatedBy = gtw.CreatedBy,
                    CreatedDate = gtw.CreatedDate,
                    ModifiedBy = gtw.ModifiedBy,
                    ModifiedDate = gtw.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetGradeTypeWeight/{_SchoolId}/{_SectionId}/{_GradeTypeCode}")]
        public async Task<IActionResult> GetGradeTypeWeight(int _SchoolId, int _SectionId, string _GradeTypeCode)
        {
            GradeTypeWeightDTO? lst = await DatabaseHelper.GetObject(
                _context.GradeTypeWeights,
                x => x.SchoolId == _SchoolId && x.SectionId == _SectionId && x.GradeTypeCode == _GradeTypeCode,
                gtw => new GradeTypeWeightDTO
                {
                    SchoolId = gtw.SchoolId,
                    SectionId = gtw.SectionId,
                    GradeTypeCode = gtw.GradeTypeCode,
                    NumberPerSection = gtw.NumberPerSection,
                    PercentOfFinalGrade = gtw.PercentOfFinalGrade,
                    DropLowest = gtw.DropLowest,
                    CreatedBy = gtw.CreatedBy,
                    CreatedDate = gtw.CreatedDate,
                    ModifiedBy = gtw.ModifiedBy,
                    ModifiedDate = gtw.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostGradeTypeWeight")]
        public async Task<IActionResult> PostGradeTypeWeight([FromBody] GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.GradeTypeWeights,
                    x => x.SchoolId == _GradeTypeWeightDTO.SchoolId && x.SectionId == _GradeTypeWeightDTO.SectionId && x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode,
                    new GradeTypeWeight
                    {
                        SchoolId = _GradeTypeWeightDTO.SchoolId,
                        SectionId = _GradeTypeWeightDTO.SectionId,
                        GradeTypeCode = _GradeTypeWeightDTO.GradeTypeCode,
                        NumberPerSection = _GradeTypeWeightDTO.NumberPerSection,
                        PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade,
                        DropLowest = _GradeTypeWeightDTO.DropLowest,
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
        [Route("PutGradeTypeWeight")]
        public async Task<IActionResult> PutGradeTypeWeight([FromBody] GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.GradeTypeWeights,
                    x => x.SchoolId == _GradeTypeWeightDTO.SchoolId && x.SectionId == _GradeTypeWeightDTO.SectionId && x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode,
                    gtw =>
                    {
                        gtw.SchoolId = _GradeTypeWeightDTO.SchoolId;
                        gtw.SectionId = _GradeTypeWeightDTO.SectionId;
                        gtw.GradeTypeCode = _GradeTypeWeightDTO.GradeTypeCode;
                        gtw.NumberPerSection = _GradeTypeWeightDTO.NumberPerSection;
                        gtw.PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade;
                        gtw.DropLowest = _GradeTypeWeightDTO.DropLowest;
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
        [Route("DeleteGradeTypeWeight/{_SchoolId}/{_SectionId}/{_GradeTypeCode}")]
        public async Task<IActionResult> DeleteGradeTypeWeight(int _SchoolId, int _SectionId, string _GradeTypeCode)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.GradeTypeWeights,
                    x => x.SchoolId == _SchoolId && x.SectionId == _SectionId && x.GradeTypeCode == _GradeTypeCode
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