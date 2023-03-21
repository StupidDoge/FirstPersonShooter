using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BootScene
{
    public class BootMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
