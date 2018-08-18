using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class ScheduleService
    {
        DSS.Data.Models.Entities.Repositories.IScheduleRepository
            scheduleRepository = DependencyUtils
                .Resolve<Repositories.IScheduleRepository>();


        public List<Schedule> GetScheduleIdByBrandId(int brandId)
        {
            List<Schedule> result = null;
            result = scheduleRepository
                .Get(a => a.Scenario.BrandID == brandId)
                .ToList();
            return result;
        }
    }
    public partial interface IScheduleService
    {
        List<Schedule> GetScheduleIdByBrandId(int brandId);
    }
}
