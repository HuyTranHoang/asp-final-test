using asp_final_test.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp_final_test.ViewModels;

public class VaccineDto
{
    public Vaccine Vaccine { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryListItem { get; set; }
}