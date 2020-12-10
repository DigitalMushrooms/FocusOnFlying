using FocusOnFlying.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Common.Behaviours
{
    public class QueryBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;

        public QueryBehaviour(IFocusOnFlyingContext focusOnFlyingContext)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            if (typeof(TRequest).Name.EndsWith("Query"))
            {
                _focusOnFlyingContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            await Task.CompletedTask;
        }
    }
}
