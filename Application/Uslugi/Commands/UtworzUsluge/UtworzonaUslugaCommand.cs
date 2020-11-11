using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge
{
    public class UtworzonaUslugaCommand : IRequest
    {
        public long DataPrzyjeciaZlecenia { get; set; }
        public Guid IdKlienta { get; set; }
        public List<UtworzonaMisjaCommand> Misje { get; set; }
    }

    public class UtworzUslugeCommandHandler : IRequestHandler<UtworzonaUslugaCommand>
    {
        public UtworzUslugeCommandHandler()
        {

        }

        public Task<Unit> Handle(UtworzonaUslugaCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}
