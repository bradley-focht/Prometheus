using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public interface ICatalogable
    {
        int Id { get; set; }
        int ServiceId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
