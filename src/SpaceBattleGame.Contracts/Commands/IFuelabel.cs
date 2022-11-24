using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattleGame.Contracts.Commands
{
    public interface IFuelabel
    {
        /// <summary>
        /// Объем топлива
        /// </summary>
        public int FuelVolume { get; set; }

        /// <summary>
        /// Расход топлива
        /// </summary>
        public int FuelRate { get; set; }
    }
}
