namespace Jimy.Core.Entities;

public class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public FullName FullName { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User() {}

    public User(UserId id, Email email, Username username, Password password, FullName fullName, Role role, DateTime createdAt)
    {
        Id = id;
        Email = email;
        Username = username;
        Password = password;
        FullName = fullName;
        Role = role;
        CreatedAt = createdAt;
    }

    public void UpdateDetails(Username username, Email email)
    {
        Username = username;
        Email = email;
    }

    public void ChangePassword(Password newPassword)
    {
        Password = newPassword;
    }
}
