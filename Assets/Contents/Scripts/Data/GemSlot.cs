using LuviKunG.Pooling;

namespace ToonBlastPuzzle
{
    public class GemSlot : IPoolable
    {
        private UIGem m_gem;
        public UIGem gem
        {
            get => m_gem;
            set
            {
                m_gem = value;
                m_gem?.SetGem(ref m_gemData);
            }
        }

        private GemData m_gemData;
        public GemData gemData
        {
            get => m_gemData;
            set
            {
                m_gemData = value;
                m_gem?.SetGem(ref m_gemData);
            }
        }

        public bool isAvailable;
        public int x;
        public int y;

        private bool m_isPoolActive;
        public bool isPoolActive
        {
            get => m_isPoolActive;
            set => m_isPoolActive = value;
        }

        public void SwapGem(GemSlot targetGem)
        {
            UIGem cacheGem = m_gem;
            GemData cacheData = m_gemData;
            this.gem = targetGem.gem;
            this.gemData = targetGem.gemData;
            targetGem.gem = cacheGem;
            targetGem.gemData = cacheData;
        }

        public void ResetGem()
        {
            if (m_gem != null)
                m_gem.isPoolActive = false;
            m_gem = null;
            m_gemData.Reset();
        }

        public bool IsValid()
        {
            return isAvailable && m_gemData.IsValid();
        }
    }
}