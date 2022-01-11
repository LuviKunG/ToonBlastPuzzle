namespace ToonBlastPuzzle
{
    public static class GameInstance
    {
        private static GameContext m_context;
        public static GameContext context => m_context;

        static GameInstance()
        {
            m_context = new GameContext();
        }
    }
}