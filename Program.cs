using optional_list_bug;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<FooQuery>()
    .AddMutationType<BarMutation>()
    .AddGlobalObjectIdentification();
var app = builder.Build();

app.MapGraphQL();

app.Run();
