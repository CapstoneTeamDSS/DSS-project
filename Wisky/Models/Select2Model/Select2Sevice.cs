using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisky.Models.Select2Model
{
    public class Select2Sevice
    {
        List<Select2Model> GetPagedListOptions(IQueryable<Select2Model> select2Models, int pageSize, int pageNumber, out int totalSearchRecords)
        {
            totalSearchRecords = select2Models.Count();
            return select2Models.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
        public Select2PageResults GetSelect2PagedResult(IQueryable<Select2Model> select2Models, int pageSize, int pageNumber)
        {
            var select2PageResult = new Select2PageResults();
            var totalResults = 0;
            select2PageResult.Results = GetPagedListOptions(select2Models, pageSize, pageNumber, out totalResults);
            select2PageResult.Total = totalResults;
            return select2PageResult;
        }
    }

    public class Select2SeviceNest
    {
        List<Select2ModelNest> GetPagedListOptions(IQueryable<Select2ModelNest> select2Models, int pageSize, int pageNumber, out int totalSearchRecords)
        {
            totalSearchRecords = select2Models.Count();
            return select2Models.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
        public Select2PageResultsNest GetSelect2PagedResult(IQueryable<Select2ModelNest> select2Models, int pageSize, int pageNumber)
        {
            var select2PageResult = new Select2PageResultsNest();
            var totalResults = 0;
            select2PageResult.Results = GetPagedListOptions(select2Models, pageSize, pageNumber, out totalResults);
            select2PageResult.Total = totalResults;
            return select2PageResult;
        }
    }
}
