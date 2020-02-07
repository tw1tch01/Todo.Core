using System.Threading.Tasks;
using Todo.Services.Workflows;

namespace Todo.Services.Workflows
{
    public interface IWorkflowService
    {
        Task Process(IWorkflowProcess request);
    }
}