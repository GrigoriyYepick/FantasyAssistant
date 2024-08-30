using System.Text;
using FantasyAssistant.Application;
using FantasyAssistant.AutoMapper;
using FantasyAssistant.DI;
using FantasyAssistant.Executable.Internal;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.ChatCompletion;

var configuration = BuildConfiguration();
var serviceProvider = BuildServiceProvider(configuration);

var builder = Kernel.CreateBuilder();

// Services
builder.AddOpenAIChatCompletion(configuration["OpenAi:Model"]!, configuration["OpenAi:ApiKey"]!);

// Plugins

var kernel = builder.Build();
var chatService = kernel.GetRequiredService<IChatCompletionService>();

var fantasyProvider = serviceProvider.GetService<ILeagueInfoProvider>()!;
var data = await fantasyProvider.ReadDataAsync();

var promptBuilder = serviceProvider.GetService<IPromptBuilder>()!;
var question = promptBuilder.BuildPrompt(data.MyTeam, data.AllPlayers);

var chatHistory = new ChatHistory();
chatHistory.AddUserMessage(question);

Console.Write($"Prompt: {question}\n\n");

var sb = new StringBuilder();

while (true)
{
    sb.Clear();
    var completion = chatService.GetStreamingChatMessageContentsAsync(chatHistory, kernel: kernel);

    await foreach (var msg in completion)
    {
        sb.Append(msg.Content);
        Console.Write(msg.Content);
    }

    chatHistory.AddAssistantMessage(sb.ToString());

    Console.WriteLine();

    Console.Write("Prompt: ");
    chatHistory.AddUserMessage(Console.ReadLine() ?? string.Empty);
}

static IConfiguration BuildConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    return builder.Build();
}

static IServiceProvider BuildServiceProvider(IConfiguration configuration)
{
    var services = new ServiceCollection();
    services.AddSingleton(configuration);

    services.AddAutoMapper(typeof(MapperProfile));
    services.AddInternalServices(configuration);

    return services.BuildServiceProvider();
}