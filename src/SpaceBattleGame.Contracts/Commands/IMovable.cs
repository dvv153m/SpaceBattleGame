using System.Numerics;
using SpaceBattleGame.Contracts.Common;
using Vector = SpaceBattleGame.Contracts.Common.Vector;

namespace SpaceBattleGame.Contracts.Commands
{
    public interface IMovable
    {
        /// <summary>
        /// Текущая позиция объекта
        /// </summary>
        Vector Position { get; set; }

        /// <summary>
        /// Мгновенная скорость объекта
        /// </summary>
        Vector Velocity { get; set; }
    }
}
