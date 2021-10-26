using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Unit>
    {

        public UpdateRoomCommandHandler()
        { }

        public async Task<Unit> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
