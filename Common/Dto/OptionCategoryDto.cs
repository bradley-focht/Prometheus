using System.Collections.Generic;


namespace Common.Dto 
{
    public class OptionCategoryDto : IOptionCategoryDto, ICatalogable
    {
        public int Id { get; set; }
        public int Popularity { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<IServiceOptionDto> Options { get; set; }
    }
}
