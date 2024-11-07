using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.User_Events;

namespace Bookify.Domain.Users;


public sealed class User : Entity
{
    private User() { }
    private User(Guid id, FirstName firstName, LastName lastName, UserEmail email, UserName userName) : base(id)
    { 
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
    }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public UserEmail Email { get; private set; }
    public UserName UserName { get; private set; }
    public static User CreateUser(FirstName firstName, LastName lastName, UserEmail email, UserName userName)
    {

        var user = new User(Guid.NewGuid(), firstName, lastName, email, userName);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }

}
