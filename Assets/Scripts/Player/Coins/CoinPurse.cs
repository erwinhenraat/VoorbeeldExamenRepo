using System;

namespace UntitledCube.Player.Coins
{
    public static class CoinPurse
    {
        private static int _coins;

        public static int Coins => _coins;

        public static Action<int> OnCoinAdded;

        public static void Add()
        {
            _coins++;
            OnCoinAdded?.Invoke(_coins);
        }
    }
}