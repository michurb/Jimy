namespace Jimy.Core.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public DateTime DateJoined { get; private set; }

    protected User() {}

    public User(string username, string email)
    {
        Id = Guid.NewGuid();
        Username = username;
        Email = email;
        DateJoined = DateTime.UtcNow;
    }

    public void UpdateDetails(string username, string email)
    {
        Username = username;
        Email = email;
    }
}
