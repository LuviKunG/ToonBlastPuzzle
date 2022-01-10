using LuviKunG.Pooling;

namespace ToonBlastPuzzle
{
    public class GemSlot : IPoolable
    {
        public UIGem gem;
        public GemData gemData;
        public bool isAvailable;

        private bool m_isPoolActive;
        public bool isPoolActive
        {
            get => m_isPoolActive;
            set => m_isPoolActive = value;
        }

        public void UpdateGem()
        {
            gem.SetGem(ref gemData);
        }
    }
}