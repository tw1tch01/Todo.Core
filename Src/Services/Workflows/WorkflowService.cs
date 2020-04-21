﻿using System.Threading.Tasks;
using MediatR;

namespace Todo.Services.Workflows
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