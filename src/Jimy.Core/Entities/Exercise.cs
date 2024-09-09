namespace Jimy.Core.Entities;

public class Exercise
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    protected Exercise() {}

    public Exercise(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void UpdateDetails(string name, string description)
    {
        Name = name;
        Description = description;
    }
}