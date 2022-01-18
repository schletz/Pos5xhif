using Microsoft.AspNetCore.Mvc.Rendering;
using SPG_Fachtheorie.Aufgabe2.Model;
using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe3Mvc.Views.Home
{
    public record IndexViewModel(
        List<Student> Students,
        List<SelectListItem> UserItems,
        string CurrentUser,
        string SelectedUser);
}
