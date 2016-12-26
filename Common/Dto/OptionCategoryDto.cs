using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto 
{
    public class OptionCategoryDto : ICatalogable
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<IServiceOptionDto> Options { get; set; }
    }
}
