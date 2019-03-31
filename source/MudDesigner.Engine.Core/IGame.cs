using MudDesigner.Engine.Components.Environment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IGame : IInitializable, IDescriptor, IEquatable<IGame>
    {
        IWorld[] GetWorldsInGame();

        Task<IWorld> CreateWorld(string name);

        Task AddWorldsToGame(IWorld[] worlds);

        Task AddWorldToGame(IWorld world);

        Task RemoveWorldFromGame(IWorld world);

        Task RemoveWorldsFromGame(IWorld[] worlds);
    }

    public interface IGame<TConfiguration> : IGame, IConfigurable<TConfiguration> where TConfiguration : IConfiguration
    {
    }
}
