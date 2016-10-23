using System;

namespace DataService.Models
{
    public interface ISwotActivity
    {
        DateTime Date { get; set; }
        string Description { get; set; }
        int Id { get; set; }
        string Name { get; set; }
    }
}