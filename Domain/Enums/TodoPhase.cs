using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.Enums
{
    public enum TodoPhase
    {
        [Display(Name = "Planeado")]
        PLANNED,

        [Display(Name = "Iniciado")]
        STARTED,

        [Display(Name = "En curso")]
        IN_PROGRESS,

        [Display(Name = "Terminado")]
        COMPLETED
    }
}
