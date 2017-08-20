using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Areas.Identity.Data;

// Add profile data for application users by adding properties to the LibraryManagementUser class
public class LibraryManagementUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; }
}

