using UnityEngine;

namespace ToonBlastPuzzle
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private PuzzleManager puzzleManager;

        private void Awake()
        {
            puzzleManager.Initialize();
        }

        private void Start()
        {
            puzzleManager.CreatePuzzle();
        }
    }
}