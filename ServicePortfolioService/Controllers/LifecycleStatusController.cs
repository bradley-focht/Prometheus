using AutoMapper;
using Common.Dto;
using DataService.DataAccessLayer;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Utilities;
using DataService;
using ServicePortfolioService.AutoMapperConfig;

namespace ServicePortfolioService.Controllers
{
    public class LifecycleStatusController : ILifecycleStatusController
    {
        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public LifecycleStatusController()
        {
            _userId = PortfolioService.GuestUserId;
        }

        public LifecycleStatusController(int userId)
        {
            _userId = userId;
        }

        //TODO: remove man mapper
        public IEnumerable<Tuple<int, string>> GetLifecycleStatusNames()
        {
            using (var context = new PrometheusContext())
            {
                var lifecycleStatusRecords = context.LifecycleStatuses;

                //Empty list
                if (!lifecycleStatusRecords.Any())
                    return new List<Tuple<int, string>>();


                //var statuses = lifecycleStatusRecords.Select(x => Mapper.Map<LifecycleStatusDto>(x));
                var statuses = new List<LifecycleStatusDto>();
                foreach (var status in lifecycleStatusRecords)
                    statuses.Add(ManualMapper.MapLifecycleStatusToDto(status));
                        //mapping and linq don't seem to get along

                var nameList = new List<Tuple<int, string>>();
                nameList.AddRange(statuses.Select(x => new Tuple<int, string>(x.Id, x.Name)));
                return nameList.OrderBy(x => x.Item2);
            }
        }

        //TODO: remove man mapper here
        public ILifecycleStatusDto GetLifecycleStatus(int lifecycleStatusId)
        {
            using (var context = new PrometheusContext())
            {
                var lifecycleStatus = context.LifecycleStatuses.Find(lifecycleStatusId);
                //return Mapper.Map<LifecycleStatusDto>(lifecycleStatus);
                if (lifecycleStatus != null)
                    return ManualMapper.MapLifecycleStatusToDto(lifecycleStatus);
                return null;
            }
        }

        public ILifecycleStatusDto SaveLifecycleStatus(ILifecycleStatusDto lifecycleStatus)
        {

            //TODO: remove man mapper in here
            using (var context = new PrometheusContext())
            {
                var existingStatus = context.LifecycleStatuses.Find(lifecycleStatus.Id);
                if (existingStatus == null)
                {
                    //var savedStatus = context.LifecycleStatuses.Add(Mapper.Map<LifecycleStatus>(lifecycleStatus));
                    var savedStatus =
                        context.LifecycleStatuses.Add(ManualMapper.MapDtoToLifecycleStatus(lifecycleStatus));
                    context.SaveChanges(_userId);
                    //return Mapper.Map<LifecycleStatusDto>(savedStatus);
                    return ManualMapper.MapLifecycleStatusToDto(savedStatus);
                }
                else
                {
                    return UpdateLifecycleStatus(lifecycleStatus);
                }
            }
        }

        //TODO: remove man mapper in here
        private ILifecycleStatusDto UpdateLifecycleStatus(ILifecycleStatusDto lifecycleStatus)
        {
            using (var context = new PrometheusContext())
            {
              //TODO: Sean - ef messes up below because it is tracking the record from here
              // then when it gets to the else, it gets messed up with the attach
              // code works fine with this commented out so i don't think it is needed 
              /*  var existingStatus = context.LifecycleStatuses.Find(lifecycleStatus.Id);
                if (existingStatus == null)
                {
                    throw new InvalidOperationException("Serivce record must exist in order to be updated.");
                }
                else
                { */
                    //var updatedStatus = Mapper.Map<LifecycleStatus>(lifecycleStatus);
                    var updatedStatus = ManualMapper.MapDtoToLifecycleStatus(lifecycleStatus);
                    context.LifecycleStatuses.Attach(updatedStatus);
                    context.Entry(updatedStatus).State = EntityState.Modified;
                    
                    

                    context.SaveChanges(_userId);
                    //return Mapper.Map<LifecycleStatusDto>(updatedStatus);
                    return ManualMapper.MapLifecycleStatusToDto(updatedStatus);
           //     }
            }
        }

        public bool DeleteLifecycleStatus(int lifecycleStatusId)
        {
            using (var context = new PrometheusContext())
            {
                var toDelete = context.LifecycleStatuses.Find(lifecycleStatusId);
                context.LifecycleStatuses.Remove(toDelete);
                context.SaveChanges(_userId);
            }
            return true;
        }

        public int CountLifecycleStatuses()
        {
            using (var context = new PrometheusContext())
            {
                return context.LifecycleStatuses.Count();
            }
        }
    }
}
