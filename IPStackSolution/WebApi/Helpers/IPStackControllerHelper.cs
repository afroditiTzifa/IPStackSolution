using System;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Helpers
{
    public static class IPStackControllerHelper{



        public static IEnumerable<IEnumerable<IPDetails>> IntoBatches<IPDetails>( this IEnumerable<IPDetails> list, int size){
                if (size < 1)
                    throw new ArgumentException();

                var rest = list;

                while (rest.Any()) {
                    yield return rest.Take(size);
                    rest = rest.Skip(size);
                }
       }
    }
}