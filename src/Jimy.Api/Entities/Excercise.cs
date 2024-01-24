using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Jimy.Api.Exceptions;

namespace Jimy.Api.Entities;

public class Exercise
{
    public Guid Id { get;}

    public string Name { get; private set; }

    public Exercise(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyNameException();
        }
        Name = name;
    }
}