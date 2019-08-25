using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Configuration
{
    public  static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappingModules()
        {
            var conf = new MapperConfiguration(cfg=>
            {
                cfg.AddProfile<AutoMapperProfile>();
           }
            ) ;

            return conf;
        }
    }
}