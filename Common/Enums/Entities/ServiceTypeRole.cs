using System.ComponentModel.DataAnnotations;

namespace Common.Enums.Entities
{
    public enum ServiceTypeRole
    {
        [Display(Name="Customer-Facing Service")]
        Business,
        [Display(Name="Supporting Service")]
        Supporting
    }
}
