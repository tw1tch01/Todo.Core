using System.Threading.Tasks;

namespace Todo.Services.External.Workflows
{
    public interface IWorkflowService
    {
        Task Process(IWorkflowProcess request);
    }
}