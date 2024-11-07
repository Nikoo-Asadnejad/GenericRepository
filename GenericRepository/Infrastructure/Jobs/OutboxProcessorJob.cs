
using System.Reflection;
using System.Text.Json;
using GenericRepository.Application.Interfaces.GenericRepository.Command;
using GenericRepository.Application.Interfaces.GenericRepository.Query;
using GenericRepository.Application.Interfaces.UnitOfWork;
using GenericRepository.Domain;
using GenericRepository.Domain.Entities;
using GenericRepository.Domain.Enums;
using GenericRepository.Infrastructure.Context;
using MediatR;
using Microsoft.Extensions.Hosting;
using MassTransit;
namespace GenericRepository.Infrastructure.Jobs;

public class OutboxProcessorJob(
    IQueryGenericRepository<OutBoxMessage> readRepository,
    IUnitOfWork<CommandContext> unitOfWork,
    IMediator mediator,
    IPublishEndpoint publisher) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var messages = GetMessages();

        foreach (var message in messages)
        {
            await PublishMessage(message);
            
            message.Process();
            
            await Save(message);
        }
    }

    private IReadOnlyList<OutBoxMessage> GetMessages()
    {
        var messages =  readRepository.GetList(query: m=> !m.ProcessedOnUtc.HasValue);
        return messages;
    }

    private async Task PublishMessage(OutBoxMessage message)
    {
        var @event = GetEvent(message);
        
        if (message.EventType.EventType is EventTypeEnum.Internal)
        {
            await mediator.Publish(@event);
        }
        else
        {
            await publisher.Publish(@event);
        }
    }
   
    private async Task Save(OutBoxMessage message)
    {
        await unitOfWork.OutboxMessages.UpdateAsync(message);
        await unitOfWork.SaveAsync();
    }

    private static object? GetEvent(OutBoxMessage message)
    {
        var messageType = Type.GetType(message.EntityType);
        var @event = JsonSerializer.Deserialize(message.Content, messageType);
        return @event;
    }

 
}