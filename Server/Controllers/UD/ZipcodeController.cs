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
    public class ZipcodeController : BaseController
    {
        public ZipcodeController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetZipcode")]
        public async Task<IActionResult> GetZipcode()
        {
            List<ZipcodeDTO> lst = await DatabaseHelper.GetAllObjects(
                _context.Zipcodes,
                z => new ZipcodeDTO
                {
                    Zip = z.Zip,
                    City = z.City,
                    State = z.State,
                    CreatedBy = z.CreatedBy,
                    CreatedDate = z.CreatedDate,
                    ModifiedBy = z.ModifiedBy,
                    ModifiedDate = z.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetZipcode/{_Zip}")]
        public async Task<IActionResult> GetZipcode(string _Zip)
        {
            ZipcodeDTO? lst = await DatabaseHelper.GetObject(
                _context.Zipcodes,
                x => x.Zip == _Zip,
                z => new ZipcodeDTO
                {
                    Zip = z.Zip,
                    City = z.City,
                    State = z.State,
                    CreatedBy = z.CreatedBy,
                    CreatedDate = z.CreatedDate,
                    ModifiedBy = z.ModifiedBy,
                    ModifiedDate = z.ModifiedDate,
                }
            );
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostZipcode")]
        public async Task<IActionResult> PostZipcode([FromBody] ZipcodeDTO _ZipcodeDTO)
        {
            try
            {
                await DatabaseHelper.PostObject(
                    _context,
                    _context.Zipcodes,
                    x => x.Zip == _ZipcodeDTO.Zip,
                    new Zipcode
                    {
                        Zip = _ZipcodeDTO.Zip,
                        City = _ZipcodeDTO.City,
                        State = _ZipcodeDTO.State,
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
        [Route("PutZipcode")]
        public async Task<IActionResult> PutZipcode([FromBody] ZipcodeDTO _ZipcodeDTO)
        {
            try
            {
                await DatabaseHelper.PutObject(
                    _context,
                    _context.Zipcodes,
                    x => x.Zip == _ZipcodeDTO.Zip,
                    z =>
                    {
                        z.Zip = _ZipcodeDTO.Zip;
                        z.City = _ZipcodeDTO.City;
                        z.State = _ZipcodeDTO.State;
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
        [Route("DeleteZipcode/{_Zip}")]
        public async Task<IActionResult> DeleteZipcode(string _Zip)
        {
            try
            {
                await DatabaseHelper.DeleteObject(
                    _context,
                    _context.Zipcodes,
                    x => x.Zip == _Zip
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