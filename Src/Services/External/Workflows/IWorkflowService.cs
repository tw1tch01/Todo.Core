using System.Threading.Tasks;
using Todo.Services.Common;

namespace Todo.Services.External.Workflows
{
    public interface IWorkflowService
    {
        Task Process(IWorkflowProcess request);
    }
}