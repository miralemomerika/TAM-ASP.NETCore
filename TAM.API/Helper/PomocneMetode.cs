using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAM.Core;

namespace TAM.Web.Helper
{
    public static class PomocneMetode
    {
        public static PagedResult<T> Paginacija<T>(string pretrazivanje, IQueryable<T> podaci,
            int pageNumber = 1,
            int pageSize = 5) where T : class
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var BrojKategorija = podaci.Count();
            podaci = podaci.Skip(ExcludeRecords).Take(pageSize);


            var rezultat = new PagedResult<T>
            {
                Data = podaci.AsNoTracking().ToList(),
                TotalItems = BrojKategorija,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return rezultat;
        }
        public static ExceptionHandler GenerisiException(Exception ex)
        {
            string poruka = ex.Message + Environment.NewLine;
            poruka += ex?.StackTrace + Environment.NewLine;
            poruka += ex?.Source + Environment.NewLine;
            poruka += ex?.InnerException + Environment.NewLine;
            DateTime dateTime = DateTime.Now;
            return new ExceptionHandler { SadrzajGreske = poruka, DatumIVrijemeGreske = dateTime };
        }
    }
}
