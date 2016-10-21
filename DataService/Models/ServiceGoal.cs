﻿using System;

namespace DataService.Models
{
    public class ServiceGoal : IServiceGoal
    {
        public int ServiceId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
