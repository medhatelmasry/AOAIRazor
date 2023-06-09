using AzureOpenAIClient.Http;

namespace AOAIRazor.Services;

public class AzureOpenAIService {
    private readonly OpenAIClient _openAiClient;

    public AzureOpenAIService(OpenAIClient client) {
        _openAiClient = client;
    }

    public async Task<CompletionResponse?> GetTextCompletionResponse(string input, int maxTokens) {
        var completionRequest = new CompletionRequest() {
            Prompt = input,
            MaxTokens = maxTokens
        };

        return await _openAiClient.GetTextCompletionResponseAsync(completionRequest);
    }
}