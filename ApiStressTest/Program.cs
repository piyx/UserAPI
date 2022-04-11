using NBomber;
using NBomber.Contracts;
using NBomber.CSharp;
using static NBomber.Time;

using var httpClient = new HttpClient();

var step = Step.Create("fetch users data", async context =>
{
    var response = await httpClient.GetAsync("https://localhost:44329/api/users", context.CancellationToken);
    return response.IsSuccessStatusCode
        ? Response.Ok(statusCode: (int)response.StatusCode)
        : Response.Fail(statusCode: (int)response.StatusCode);
});

var scenario = ScenarioBuilder
    .CreateScenario("simple_http", step)
    .WithWarmUpDuration(Seconds(5))
    .WithLoadSimulations(Simulation.KeepConstant(24, TimeSpan.FromSeconds(60)));

NBomberRunner
    .RegisterScenarios(scenario)
    .Run();