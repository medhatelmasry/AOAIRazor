using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AOAIRazor.Services;
using AzureOpenAIClient.Http;

namespace AOAIRazor.Pages;

public class IndexModel : PageModel {
    CompletionResponse? completionResponse;

    [BindProperty]
    public string TextValue { get; set; } = default!;
    
    private readonly ILogger<IndexModel> _logger;
    private readonly AzureOpenAIService  _azureOpenAIService ;

    public IndexModel(ILogger<IndexModel> logger, 
    AzureOpenAIService azureOpenAIService) {
        _logger = logger;
        _azureOpenAIService = azureOpenAIService;
    }

    public IActionResult OnGetAsync() {
        // Retrieve the value of TextValue from TempData
        if (TempData.ContainsKey("TextValue")) {
            TextValue = (string)TempData["TextValue"]!;
        } else {
            TextValue = "Four score and seven years ago";
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync() {
        
        if (TextValue != null) {
            completionResponse = await _azureOpenAIService.GetTextCompletionResponse(TextValue, 500);

            if (completionResponse?.Choices.Count > 0) {
                TextValue = TextValue + completionResponse.Choices[0].Text;
                
            }
        }

        // Store the value of TextValue in TempData
        TempData["TextValue"] = TextValue;

        // Do other processing as needed
        return RedirectToPage();
    }
}