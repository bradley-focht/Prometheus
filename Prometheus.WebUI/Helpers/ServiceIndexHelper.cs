using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common.Dto;
using Common.Enums;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers.Enums;

namespace Prometheus.WebUI.Helpers
{
    public class ServiceIndexHelper
    {
        private IEnumerable<IServiceDto> _services;
        public List<Tuple<FilterByType, int>> Filters { get; set; }
        public string AppliedFilter { get; private set; }


        /// <summary>
        /// Build menu and services with no filter applied
        /// </summary>
        /// <param name="services"></param>
        public ServiceIndexHelper(IEnumerable<IServiceDto> services)
        {
            _services = services;

            Filters = new List<Tuple<FilterByType, int>>();
        }

        /// <summary>
        /// Add a filter, will be applied to GetServices
        /// </summary>
        /// <param name="filterBy"></param>
        /// <param name="filterArg"></param>
        public void AddFilter(FilterByType filterBy, int filterArg)
        {
            Filters.Add(new Tuple<FilterByType, int>(filterBy, filterArg));
        }

        public IEnumerable<IServiceDto> GetServices()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Filtered by: ");
           
            if (Filters.Any())
            {
                foreach (Tuple<FilterByType, int> filter in Filters)
                {
                    if (filter.Item1 == FilterByType.Status)
                    {
                        _services = _services.Where(s => s.LifecycleStatusId == filter.Item2);
                        sb.Append("Status is " + (from s in _services
                            where s.LifecycleStatusDto.Id == filter.Item2
                            select s.LifecycleStatusDto.Name).First());
                    }
                    else if (filter.Item1 == FilterByType.Catalog)
                    {
                        _services = _services.Where(s => s.ServiceTypeRole == (ServiceTypeRole)filter.Item2);
                        string name = ((ServiceTypeRole) filter.Item2).ToString();
                        string output = Regex.Replace(name, "([a-z])_?([A-Z])", "$1 $2");
                        sb.Append(output + " Catalog");
                    }
                    else if (filter.Item1 == FilterByType.ServiceOwner)
                    {
                        sb.Append("Service Owner is " + filter.Item2);
                        //do nothing for now.... sorry.
                    }

                }
            }
            AppliedFilter = sb.ToString();
            return _services;
        }

        /// <summary>
        /// Create controls model for the Service Controls
        /// </summary>
        /// <returns></returns>
        public List<Tuple<string, string, IEnumerable<Tuple<int, string>>>> GetControlsModel()
        {

            List<Tuple<string, string, IEnumerable<Tuple<int, string>>>> menuList = new List<Tuple<string, string, IEnumerable<Tuple<int, string>>>>();

            //Add Service Owners
            IEnumerable<Tuple<int, string>> serviceOwners = (from s in _services
                                                             where s.ServiceOwner != null
                                                             select new Tuple<int, string>(0, s.ServiceOwner)).Distinct();
            if (serviceOwners.Any())
            {
                try
                {
                    menuList.Add(
                        new Tuple<string, string, IEnumerable<Tuple<int, string>>>("Service Owner", "ServiceOwner",
                            serviceOwners
                            ));
                }
                 catch { } //just going to skip "problem items" for now. You know them by their null values....
            }

            //Add Service Catalog Types
            menuList.Add(new Tuple<string, string, IEnumerable<Tuple<int, string>>>("Catalog", "Catalog",
                new List<Tuple<int, string>>
                {
                    new Tuple<int, string>((int)ServiceTypeRole.Business, "Business"),
                    new Tuple<int, string>((int)ServiceTypeRole.Supporting, "Technical")
                }));

            //Add Statuses
            IEnumerable<Tuple<int, string>> statuses = (from s in _services
                                                        select new Tuple<int, string>(s.LifecycleStatusDto.Id, s.LifecycleStatusDto.Name)).Distinct();

            menuList.Add(new Tuple<string, string, IEnumerable<Tuple<int, string>>>("Status", "Status",
               statuses));

            return menuList;
        }
    }
}