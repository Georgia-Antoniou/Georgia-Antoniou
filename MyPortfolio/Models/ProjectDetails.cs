namespace MyPortfolio.Models;

public record ProjectName(string Name)
{
    public string GetEncoded() => Name.Replace(" ", "-").Replace(".", "-dot-").ToLowerInvariant();

    public static implicit operator ProjectName(string name) => new ProjectName(name);

    public static ProjectName FromEncoded(string encodedName)
    {
        var name = encodedName.Replace("-dot-", ".").Replace("-", " ");
        return new ProjectName(name);
    }
    public override string ToString()
    {
        return Name;
    }
}
public class ProjectDetails
{

    public required string Title { get; set; }

    public required string Role { get; set; }
    public required string Description { get; set; }

    public required List<Responsibility> Responsibilities { get; set; }

    public required List<Artifact> Artifcacts { get; set; }
}

public class Artifact
{
}

public class Responsibility
{
    public required string Description { get; set; }
}