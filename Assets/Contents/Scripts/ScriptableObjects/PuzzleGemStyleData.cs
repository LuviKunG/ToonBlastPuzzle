using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ToonBlastPuzzle
{
    [CreateAssetMenu(menuName = "Toon Blast Puzzle/Gem Styles")]
    public sealed class PuzzleGemStyleData : ScriptableObject
    {
        [SerializeField]
        private float _gemSize;
        public float gemSize => _gemSize;

        [SerializeField]
        private AssetReference assetRefPrefabGem;

        private UIGem _prefabGem;
        public UIGem prefabGem => _prefabGem;

        public IEnumerator InitializeAsync()
        {
            AsyncOperationHandle<GameObject> asyncPrefabGem = assetRefPrefabGem.LoadAssetAsync<GameObject>();
            yield return asyncPrefabGem;
            _prefabGem = asyncPrefabGem.Result.GetComponent<UIGem>();
            assetRefPrefabGem.ReleaseAsset();
            yield break;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_gemSize < 0)
                _gemSize = 0;
        }
#endif
    }
}