﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPFeaturesDemonstration.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASPFeaturesDemonstration.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPFeaturesDemonstration.Areas.Identity.Pages.Account.Manage {
    public partial class IndexModel : PageModel {
        private readonly UserManager<ASPFeaturesDemonstrationUser> _userManager;
        private readonly SignInManager<ASPFeaturesDemonstrationUser> _signInManager;

        // This list should be populated from a database either in constructor or OnGet
        // SelectListItem items require a value and display text, which        
        public Countries Countries { get; set; }
        public SelectList CountrySelect { get; set; }

        public IndexModel(
            UserManager<ASPFeaturesDemonstrationUser> userManager,
            SignInManager<ASPFeaturesDemonstrationUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;

            // The instantiation of Countries is quite unnecessary as it could become a static object
            Countries = new Countries();
            //CountrySelect, first arg is Dict, second argument is the value to be stored and third argument is the value to be displayed when populating from a dictionary
            CountrySelect = new SelectList(Countries.CountryList.OrderBy(x => x.Value), "Key", "Value");
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel {
            // These are the custom user identity fields added in the user identity model
            // namespace ASPFeaturesDemonstration.Areas.Identity.Data.ASPFeaturesDemonstrationUser
            [DataType(DataType.Text), Display(Name = "Full Name")]
            public string Name { get; set; }

            [DataType(DataType.Date), Display(Name = "Date of Birth")]
            public DateTime DateOfBirth { get; set; }

            [Display(Name = "Gender")]
            public Gender Gender { get; set; }

            [DataType(DataType.Text), Display(Name = "Country"), StringLength(2)]
            public string CountryIso3166_1_alpha_2 { get; set; }

            // These are the default custom user identity fields generated by scaffolding
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ASPFeaturesDemonstrationUser user) {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel {
                Name = user.Name,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                CountryIso3166_1_alpha_2 = user.CountryIso3166_1_alpha_2,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync() {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid) {
                await LoadAsync(user);
                return Page();
            }

            if (Input.Name != user.Name) user.Name = Input.Name;
            if (Input.DateOfBirth != user.DateOfBirth) user.DateOfBirth = Input.DateOfBirth;
            if (Input.Gender != user.Gender) user.Gender = Input.Gender;
            if (Input.CountryIso3166_1_alpha_2 != user.CountryIso3166_1_alpha_2) user.CountryIso3166_1_alpha_2 = Input.CountryIso3166_1_alpha_2;

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber) {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded) {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
