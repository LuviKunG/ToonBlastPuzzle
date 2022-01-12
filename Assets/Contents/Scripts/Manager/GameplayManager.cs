using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using LuviKunG.Attribute;

namespace ToonBlastPuzzle
{
    public class GameplayManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private PuzzleManager m_puzzleManager;
        [SerializeField]
        private UIPanelGameplayHeader m_panelHeader = default;

        [Header("Configurations")]
        [SerializeField, StringScene]
        private string m_sceneNameMain = default;

        private GameContext m_gameContext;
        private Coroutine m_routineHandlerLoadScene;

        private IEnumerator Start()
        {
            // Waiting for next frame for let unity finish scene load.
            yield return null;
            // Initialize (Async).
            m_gameContext = GameInstance.context;
            m_panelHeader.buttonBack.onClick.AddListener(() =>
            {
                if (m_routineHandlerLoadScene == null)
                    m_routineHandlerLoadScene = StartCoroutine(RoutineLoadScene(m_sceneNameMain));
            });
            yield return m_puzzleManager.InitializeAsync(m_gameContext);
            // Execution.
            m_puzzleManager.CreatePuzzle();
        }

        private IEnumerator RoutineLoadScene(string sceneName)
        {
            var async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            async.allowSceneActivation = true;
            yield return async;
        }
    }
}