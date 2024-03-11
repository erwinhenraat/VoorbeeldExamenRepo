using System;
using UntitledCube.Maze.Generation;

namespace UntitledCube.Player.Coins
{
    public static class CoinPurse
    {
        private const int SPAWN_CHANCE = 10;
        private const int RUSH_SPAWN_CHANCE = 75;
        private const int GOLD_RUSH_CHANCE = 9;

        private static int _coins;
        private static bool _goldRush;

        public static int Coins => _coins;

        public static int SpawnChance => _goldRush ? RUSH_SPAWN_CHANCE : SPAWN_CHANCE;

        public static Action<int> OnCoinAdded;

        static CoinPurse() =>  MazeGenerator.OnGenerate += RushChance;

        /// <summary>
        /// Adds a coin to the player's purse and triggers any registered OnCoinAdded events.
        /// </summary>
        public static void Add()
        {
            _coins++;
            OnCoinAdded?.Invoke(_coins);
        }

        /// <summary>
        /// Determines if a "gold rush" event should occur, increasing coin spawn rates.
        /// </summary>
        public static void RushChance()
        {
            int random = UnityEngine.Random.Range(0, GOLD_RUSH_CHANCE);
            _goldRush = random == 0;
        }
    }
}