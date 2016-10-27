using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum ServiceTypeRole
    {
        [Display(Name="Customer-Facing Service")]
        Business,
        [Display(Name="Supporting Service")]
        Supporting
    }
}
