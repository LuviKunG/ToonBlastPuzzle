using System.Collections;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private PuzzleManager puzzleManager;

        private IEnumerator Start()
        {
            // Waiting for next frame for let unity finish scene load.
            yield return null;
            // Initialize (Async).
            yield return puzzleManager.InitializeAsync();
            // Execution.
            puzzleManager.CreatePuzzle();
        }
    }
}