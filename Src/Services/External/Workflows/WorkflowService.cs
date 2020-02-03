using System.Threading.Tasks;
using MediatR;
using Todo.DomainModels.Common;

namespace Todo.Services.External.Workflows
{
    public class WorkflowService : Mediator, IWorkflowService
    {
        public WorkflowService(ServiceFactory serviceFactory)
            : base(serviceFactory)
        {
        }

        public async Task Process(IWorkflowProcess request)
        {
            if (request is INotification)
            {
                await Publish(request);
            }
        }
    }
}