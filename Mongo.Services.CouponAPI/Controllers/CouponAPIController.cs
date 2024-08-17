using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mongo.Services.CouponAPI.Data;
using Mongo.Services.CouponAPI.Models;
using Mongo.Services.CouponAPI.Models.DTOs;

namespace Mongo.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private ResponseDTO _response;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDTO();
        }
        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _context.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(objList);
                return _response;
            }
            catch (Exception ex) {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Coupon objList = _context.Coupons.First(x => x.CouponId == id);
                
                _response.Result = _mapper.Map<CouponDTO>(objList);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess=false;
                _response.Message = ex.Message.ToString();
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDTO GetByCode(string code)
        {
            try
            {
                Coupon objList = _context.Coupons.FirstOrDefault(x => x.CouponCode.ToLower() == code.ToLower());
                if(objList == null)
                {
                    _response.IsSuccess = false;
                }
                _response.Result = _mapper.Map<CouponDTO>(objList);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }
            return _response;
        }


        [HttpPost]
        public ResponseDTO Post([FromBody] CouponDTO coupon)
        {
            try
            {
                Coupon objList = _mapper.Map<Coupon>(coupon);
                _context.Coupons.Add(objList);
                _context.SaveChanges();
                _response.Result = _mapper.Map<CouponDTO>(objList);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }
            return _response;
        }

        [HttpPut]
        public ResponseDTO Put([FromBody] CouponDTO coupon)
        {
            try
            {
                Coupon objList = _mapper.Map<Coupon>(coupon);
                _context.Coupons.Update(objList);
                _context.SaveChanges();
                _response.Result = _mapper.Map<CouponDTO>(objList);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }
            return _response;
        }

        [HttpDelete]
        public ResponseDTO Delete(int id)
        {
            try
            {
                Coupon objList = _context.Coupons.First(u => u.CouponId == id);
                _context.Coupons.Remove(objList);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }
            return _response;
        }
    }
}
