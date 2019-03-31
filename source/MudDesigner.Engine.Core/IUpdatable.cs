using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IUpdatable
    {
        Task Update(UpdateContext updateContext);
    }
}
