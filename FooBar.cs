namespace optional_list_bug;

[Node]
public class Foo
{
    [ID]
    public Guid Id { get; set; }
    
    public static Task<Foo> Get(string id)
    {
        // Unimportant for the example
        return Task.FromResult(new Foo() { Id = Guid.NewGuid() });
    }
}

public class Bar
{
    
    // This is where the bug is
    // We should be able to use Optional<List<Guid>> with [ID]
    // Currently this does not work
    [ID(nameof(Foo))] public Optional<List<Guid>?> Ids { get; set; }
}

public class Bar2
{
    // This works as intended
    [ID(nameof(Foo))] public List<Guid>? Ids { get; set; }
}

public class BarMutation
{
    public List<Foo> CreateFoos(Bar bar)
    {
        return bar.Ids is { HasValue: true, Value: not null } 
            ? bar.Ids.Value.Select(x => new Foo() { Id = x }).ToList() 
            : [];
    }    
    
    public List<Foo> CreateFoos2(Bar2 bar)
    {
        return bar.Ids?.Select(x => new Foo() { Id = x }).ToList() ?? [];
    }
}

public class FooQuery
{
    public List<Foo> GetFoos()
    {
        return [new Foo() { Id = Guid.NewGuid() }];
    }
}