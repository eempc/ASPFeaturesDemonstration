﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ASPFeaturesDemonstration.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ASPFeaturesDemonstration.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPFeaturesDemonstration.Areas.Identity.Pages.Account {
    [AllowAnonymous]
    public class RegisterModel : PageModel {
        private readonly SignInManager<ASPFeaturesDemonstrationUser> _signInManager;
        private readonly UserManager<ASPFeaturesDemonstrationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        // This list should be populated from a database either in constructor or OnGet
        // SelectListItem items require a value and display text, which        
        public Countries Countries { get; set; }
        public SelectList CountrySelect { get; set; }

        public RegisterModel(
            UserManager<ASPFeaturesDemonstrationUser> userManager,
            SignInManager<ASPFeaturesDemonstrationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender) {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;

            // The instantiation of Countries is quite unnecessary as it could become a static object
            Countries = new Countries();
            //CountrySelect, first arg is Dict, second argument is the value to be stored and third argument is the value to be displayed when populating from a dictionary
            CountrySelect = new SelectList(Countries.CountryList.OrderBy(x => x.Value), "Key", "Value");
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel {
            // These are the custom user identity fields added in the user identity model
            // namespace ASPFeaturesDemonstration.Areas.Identity.Data.ASPFeaturesDemonstrationUser
            [Required, DataType(DataType.Text), Display(Name = "Full Name")]
            public string Name { get; set; }

            [Required, DataType(DataType.Date), Display(Name = "Date of Birth")]
            public DateTime DateOfBirth { get; set; }

            [Required, Display(Name = "Gender")]
            public Gender Gender { get; set; }

            [Required, DataType(DataType.Text), Display(Name = "Country"), StringLength(2)]
            public string CountryIso3166_1_alpha_2 { get; set; }

            // These are the default custom user identity fields generated by scaffolding
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null) {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid) {
                var user = new ASPFeaturesDemonstrationUser { 
                    Name = Input.Name,
                    DateOfBirth = Input.DateOfBirth,
                    Gender = Input.Gender,
                    CountryIso3166_1_alpha_2 = Input.CountryIso3166_1_alpha_2,
                    UserName = Input.Email, 
                    Email = Input.Email 
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded) {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount) {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    } else {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
