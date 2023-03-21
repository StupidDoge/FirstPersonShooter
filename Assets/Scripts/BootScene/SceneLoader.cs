using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace BootScene
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private AssetReference _scene;

        public async void StartGame()
        {
            var asyncOperationHandle = Addressables.LoadSceneAsync(_scene, LoadSceneMode.Single);
            await asyncOperationHandle.Task;
        }
    }
}
