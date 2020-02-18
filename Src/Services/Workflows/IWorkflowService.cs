using System.Threading.Tasks;

namespace Todo.Services.Workflows
{
    public interface IWorkflowService
    {
        Task Process(IWorkflowProcess request);
    }
}