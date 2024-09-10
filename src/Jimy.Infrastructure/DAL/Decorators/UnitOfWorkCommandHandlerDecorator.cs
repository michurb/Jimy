using Jimy.Application.Abstraction;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Jimy.Infrastructure.DAL.Decorators;

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IUnitOfWork unitOfWork)
    {
        _commandHandler = commandHandler;
        _unitOfWork = unitOfWork;
    }
    public Task HandleAsync(TCommand command)
        => _unitOfWork.ExecuteAsync(() => _commandHandler.HandleAsync(command));
}