using LuviKunG.Attribute;
using LuviKunG.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToonBlastPuzzle
{
    public sealed class MainManager : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField]
        private PuzzleLevelData[] m_levels = default;
        [SerializeField]
        private PuzzleComboRuleData[] m_rules = default;
        [SerializeField]
        private PuzzleScoreCalculationData[] m_scoreCalculations = default;
        [SerializeField]
        private PuzzleGemRandomizerData[] m_gemRandomizers = default;
        [SerializeField]
        private PuzzleGemStyleData[] m_gemStyles = default;

        [Header("UI")]
        [SerializeField]
        private UIPanelMainGameSelection m_gameSelection = default;

        [Header("Configurations")]
        [SerializeField, StringScene]
        private string m_sceneNameGameplay = default;

        private GameContext m_gameContext = null;
        private Coroutine routineHandlerLoadLevel;

        private IEnumerator Start()
        {
            // Wait for a frame for unity prepare for rendering.
            yield return null;
            m_gameContext = GameInstance.context;
            m_gameContext.level = m_levels[0];
            m_gameContext.comboRule = m_rules[0];
            m_gameContext.scoreCalculation = m_scoreCalculations[0];
            m_gameContext.gemRandomizer = m_gemRandomizers[0];
            m_gameContext.gemStyle = m_gemStyles[0];
            m_gameSelection.Initialize();
            m_gameSelection.buttonPlay.onClick.AddListener(() =>
            {
                if (routineHandlerLoadLevel == null)
                    routineHandlerLoadLevel = StartCoroutine(RoutineLoadScene(m_sceneNameGameplay));
            });
            m_gameSelection.buttonSelectLevel.onClick.AddListener(() =>
            {
                m_gameSelection.menuSelectionLevel.Show();
            });
            m_gameSelection.buttonSelectComboRule.onClick.AddListener(() =>
            {
                m_gameSelection.menuSelectionComboRule.Show();
            });
            m_gameSelection.buttonSelectScoreCalculation.onClick.AddListener(() =>
            {
                m_gameSelection.menuSelectionScoreCalculation.Show();
            });
            m_gameSelection.buttonSelectGemRandomizer.onClick.AddListener(() =>
            {
                m_gameSelection.menuSelectionGemRandomizer.Show();
            });
            m_gameSelection.buttonSelectGemStyle.onClick.AddListener(() =>
            {
                m_gameSelection.menuSelectionGemStyle.Show();
            });
            m_gameSelection.menuSelectionLevel.buttonClose.onClick.AddListener(() =>
            {
                m_gameSelection.menuSelectionLevel.Hide();
            });
            m_gameSelection.menuSelectionComboRule.buttonClose.onClick.AddListener(() =>
            {
                m_gameSelection.menuSelectionComboRule.Hide();
            });
            m_gameSelection.menuSelectionScoreCalculation.buttonClose.onClick.AddListener(() =>
            {
                m_gameSelection.menuSelectionScoreCalculation.Hide();
            });
            m_gameSelection.menuSelectionGemRandomizer.buttonClose.onClick.AddListener(() =>
            {
                m_gameSelection.menuSelectionGemRandomizer.Hide();
            });
            m_gameSelection.menuSelectionGemStyle.buttonClose.onClick.AddListener(() =>
            {
                m_gameSelection.menuSelectionGemStyle.Hide();
            });
            foreach (var level in m_levels)
            {
                m_gameSelection.menuSelectionLevel.AddElement(level.name, () =>
                {
                    m_gameContext.level = level;
                    m_gameSelection.menuSelectionLevel.Hide();
                });
            }
            foreach (var rule in m_rules)
            {
                m_gameSelection.menuSelectionComboRule.AddElement(rule.name, () =>
                {
                    m_gameContext.comboRule = rule;
                    m_gameSelection.menuSelectionComboRule.Hide();
                });
            }
            foreach (var scoreCalculation in m_scoreCalculations)
            {
                m_gameSelection.menuSelectionScoreCalculation.AddElement(scoreCalculation.name, () =>
                {
                    m_gameContext.scoreCalculation = scoreCalculation;
                    m_gameSelection.menuSelectionScoreCalculation.Hide();
                });
            }
            foreach (var gemRandomizer in m_gemRandomizers)
            {
                m_gameSelection.menuSelectionGemRandomizer.AddElement(gemRandomizer.name, () =>
                {
                    m_gameContext.gemRandomizer = gemRandomizer;
                    m_gameSelection.menuSelectionGemRandomizer.Hide();
                });
            }
            foreach (var gemStyle in m_gemStyles)
            {
                m_gameSelection.menuSelectionGemStyle.AddElement(gemStyle.name, () =>
                {
                    m_gameContext.gemStyle = gemStyle;
                    m_gameSelection.menuSelectionGemStyle.Hide();
                });
            }
        }

        private IEnumerator RoutineLoadScene(string sceneName)
        {
            var asyncLoadScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            asyncLoadScene.allowSceneActivation = true;
            yield return asyncLoadScene;
        }
    }
}