using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ASPFeaturesDemonstration.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;

        public string[] toDoList = { 
            "User identity registration",
            "Additional user identity fields",
            "Email contact submission form",
            "Passwords from Azure Key Vault"
        };

        public IndexModel(ILogger<IndexModel> logger) {
            _logger = logger;
        }

        public void OnGet() {

        }
    }
}
