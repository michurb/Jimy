using System.ComponentModel.DataAnnotations;

namespace Jimy.Blazor.Models;

public class CreateActivityLogDto
{
    public Guid UserId { get; set; }
    [Required(ErrorMessage = "Activity type is required")]
    public string ActivityType { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Duration is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0")]
    public int Duration { get; set; }

    public Guid? WorkoutPlanId { get; set; }
}