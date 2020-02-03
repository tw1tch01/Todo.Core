using System.Threading.Tasks;
using Todo.DomainModels.Common;

namespace Todo.Services.External.Workflows
{
    public interface IWorkflowService
    {
        Task Process(IWorkflowProcess request);
    }
}