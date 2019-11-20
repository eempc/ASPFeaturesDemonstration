using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using ASPFeaturesDemonstration.Data;

namespace ASPFeaturesDemonstration.Areas.Identity.Data {
    // Add profile data for application users by adding properties to the ASPFeaturesDemonstrationUser class
    public class ASPFeaturesDemonstrationUser : IdentityUser {
        [PersonalData, Required]
        public string Name { get; set; } // Example of a simple string
        [PersonalData, Required]
        public DateTime DateOfBirth { get; set; } // Example of a DateTime
        [PersonalData, Required]
        public Gender Gender { get; set; } // Example of an enum, this will automatically become a select drop down
        [PersonalData, Required]
        public string CountryIso3166_1_alpha_2 { get; set; } // Example of something that will be crafted into a drop down list via SelectList
    }
}
