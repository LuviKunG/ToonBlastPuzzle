using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ToonBlastPuzzle
{
    public sealed class UIGemDefault : UIGem
    {
        [Header("Components")]
        [SerializeField]
        private Image m_imageSprite = default;

        [Header("Sprites")]
        [SerializeField]
        private PairGemColorSprite[] m_listColor = default;
        [SerializeField]
        private Sprite m_spritePowerHorizontal = default;
        [SerializeField]
        private Sprite m_spritePowerVertical = default;
        [SerializeField]
        private Sprite m_spritePowerAxis = default;
        [SerializeField]
        private Sprite m_spritePowerGroup = default;
        [SerializeField]
        private PairGemColorSprite[] m_listPowerColor = default;

        [Header("Configurations")]
        [SerializeField]
        private float m_distancePerSeconds = default;

        private Sequence m_sequenceMovement = default;

        public override bool isPoolActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public override void SetGem(ref GemData gemData)
        {
            switch (gemData.power)
            {
                case GemPower.None:
                    m_imageSprite.sprite = PairGemColorSprite.GetSprite(ref m_listColor, gemData.color);
                    return;
                case GemPower.Horizontal:
                    m_imageSprite.sprite = m_spritePowerHorizontal;
                    return;
                case GemPower.Vertical:
                    m_imageSprite.sprite = m_spritePowerVertical;
                    return;
                case GemPower.Axis:
                    m_imageSprite.sprite = m_spritePowerAxis;
                    return;
                case GemPower.Bomb:
                    m_imageSprite.sprite = m_spritePowerGroup;
                    return;
                case GemPower.Color:
                    m_imageSprite.sprite = PairGemColorSprite.GetSprite(ref m_listPowerColor, gemData.color);
                    return;
                default: return;
            }
        }

        public override void Move(Vector3 position)
        {
            float distancePerSec = (position - rectTransform.position).magnitude * m_distancePerSeconds;
            m_sequenceMovement?.Kill(false);
            m_sequenceMovement = DOTween.Sequence();
            m_sequenceMovement.Append(rectTransform.DOMove(position, distancePerSec, false).SetEase(Ease.OutBounce));
            m_sequenceMovement.SetAutoKill(true);
            m_sequenceMovement.Play();
        }

        public override void PlayGemBreak()
        {
            isPoolActive = false;
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (m_distancePerSeconds < 0.0f)
                m_distancePerSeconds = 0.0f;
        }
#endif
    }
}