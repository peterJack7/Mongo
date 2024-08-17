using AutoMapper;
using Mongo.Services.CouponAPI.Models;
using Mongo.Services.CouponAPI.Models.DTOs;

namespace Mongo.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
             {
                 config.CreateMap<CouponDTO, Coupon>();
                 config.CreateMap<Coupon,CouponDTO>();
             });
            return mappingConfig;
        }
    }
}
