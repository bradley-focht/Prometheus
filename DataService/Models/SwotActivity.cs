﻿using System;

namespace DataService.Models
{
    public class SwotActivity : ISwotActivity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
