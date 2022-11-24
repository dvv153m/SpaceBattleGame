using SpaceBattleGame.Contracts.Common;


namespace FastMovePlugin
{
    public interface IFastMovable
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
