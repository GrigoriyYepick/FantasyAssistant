using System.Text;
using FantasyAssistant.Application;
using FantasyAssistant.Application.Model;
using FantasyAssistant.AutoMapper;
using FantasyAssistant.DI;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.ChatCompletion;

var configuration = BuildConfiguration();
var serviceProvider = BuildServiceProvider(configuration);

var builder = Kernel.CreateBuilder();

// Services
builder.AddOpenAIChatCompletion("gpt-3.5-turbo", configuration["OpenAi:ApiKey"]!);

// Plugins

var kernel = builder.Build();
var chatService = kernel.GetRequiredService<IChatCompletionService>();
var fantasyProvider = serviceProvider.GetService<ILeagueInfoProvider>()!;

var players = await fantasyProvider.ReadDataAsync();
var question = BuildInitialPrompt(players);

var chatHistory = new ChatHistory();
chatHistory.AddUserMessage(question);

Console.Write($"Prompt: {question}");

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

static string BuildInitialPrompt(FantasyData data)
{
    var goalkeepers = string.Join("\n", data.Goalkeepers.Select(player => player.ToString()));
    var defenders = string.Join("\n", data.Defenders.Select(player => player.ToString()));
    var midfielders = string.Join("\n", data.Midfielders.Select(player => player.ToString()));
    var forwards = string.Join("\n", data.Forwards.Select(player => player.ToString()));
    var myPlayers = string.Join("\n", data.MyPlayers.Select(player => player.ToString()));

    var sb = new StringBuilder();
    sb.Append("Hi! Using only the info provided below about Fantasy English Premier League, what transfers for my squad do you suggest? ");
    sb.Append("The goal is to have a team with 2 goalkeepers, 5 defenders, 5 midfielders and 3 forwards. ");
    sb.Append("With the total price limit of 100. And there should not be more than 3 players from the same football team.");
    sb.AppendLine();
    sb.AppendLine("My team:");
    sb.AppendLine();
    sb.AppendLine($"{myPlayers}");
    sb.AppendLine();
    sb.AppendLine("Here are the best players by position in the league right now");
    sb.AppendLine();
    sb.AppendLine("Goalkeepers");
    sb.AppendLine($"{goalkeepers}");
    sb.AppendLine();
    sb.AppendLine("Defenders:");
    sb.AppendLine($"{defenders}");
    sb.AppendLine();
    sb.AppendLine("Midfielders:");
    sb.AppendLine($"{midfielders}");
    sb.AppendLine();
    sb.AppendLine("Forwards:");
    sb.AppendLine($"{forwards}");
    sb.AppendLine();
    sb.AppendLine("Please, take my squad and provide transfers suggestions, mentioning my player and a suggested one. Both players should be of the same position.");

    return sb.ToString();
}