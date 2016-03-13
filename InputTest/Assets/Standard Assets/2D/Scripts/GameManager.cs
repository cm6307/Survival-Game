namespace UnityStandardAssets._2D
{
    internal class GameManager
    {
        private static int nextPlayer = 0;

        public static int GetNextNum()
        {
            nextPlayer++;
            return nextPlayer;
        }
    }
}