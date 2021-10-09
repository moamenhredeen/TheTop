using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.ViewModels
{
    public class TaskVM : BASEEntity
    {
        [Display(Name = "Title")]
        [MaxLength(255, ErrorMessage = "Title shoald not exced 255 char!")]
        [MinLength(5, ErrorMessage = "Title should not be less than 5 char!")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description should have 5 up to 500 char!")]
        public string Description { get; set; }

        [Display(Name = "Duration(Day)")]
        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
        public StatusType Status { get; set; }

        public PriorityType Priority { get; set; }

        [Display(Name = "Employee")]
        public string EmployeeId { get; set; }
        public EmployeeDTO EmployeeDTO { get; set; }
        public ICollection<SelectListItem> Employees { get; set; }
    }
}
