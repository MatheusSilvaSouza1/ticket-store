using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace Core.Messages;

[NotMapped]
public abstract class Event : Message, INotification
{
}