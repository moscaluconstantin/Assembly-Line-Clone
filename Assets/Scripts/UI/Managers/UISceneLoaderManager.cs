using NaughtyAttributes;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
#endif

#if UNITY_EDITOR
#endif

namespace UI.Managers
{
    public class UISceneLoaderManager : MonoBehaviour
    {
        [SerializeField] [Scene] private int uiSceneIndex;
        [SerializeField] private bool canLoadUI;

        private void Awake()
        {
            InitData();
        }

        private void InitData()
        {
            if (!canLoadUI)
                return;

            if (IsUISceneLoaded())
                return;

            TryLoadUIScene();
        }

        private bool IsUISceneLoaded()
        {
            bool result = false;

            int scenesOpenCount = SceneManager.sceneCount;

            if (scenesOpenCount > 1)
            {
                for (var i = 0; i < scenesOpenCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);

                    if (scene.buildIndex == uiSceneIndex)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private void TryLoadUIScene()
        {
            SceneManager.LoadScene(uiSceneIndex, LoadSceneMode.Additive);
        }

        [Button("LoadUIScene")]
        private void LoadUIScene()
        {
            if (IsUISceneLoaded())
                return;

#if UNITY_EDITOR
            EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(uiSceneIndex), OpenSceneMode.Additive);
#endif
        }

        [Button("RemoveUIScene")]
        private void RemoveUIScene()
        {
            if (!IsUISceneLoaded())
                return;

#if UNITY_EDITOR
            EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByBuildIndex(uiSceneIndex), true);
#endif
        }
    }
}