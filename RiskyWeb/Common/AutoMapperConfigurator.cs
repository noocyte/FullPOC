using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace RiskyWeb.Common
{
    public static class AutoMapperConfigurator
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfile<AutoMapperProfile>());
        }
    }
}