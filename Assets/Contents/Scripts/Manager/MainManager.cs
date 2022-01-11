using LuviKunG.UI;
using System.Collections;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public sealed class MainManager : MonoBehaviour
    {
        private GameContext m_gameContext;

        private IEnumerator Start()
        {
            // Wait for a frame for unity prepare for rendering.
            yield return null;
            m_gameContext = GameInstance.context;
        }
    }
}