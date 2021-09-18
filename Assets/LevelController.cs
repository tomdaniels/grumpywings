using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Enemy[] _enemies;
    private static int _nextLevelIndex = 1;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    void Update()
    {
        foreach(Enemy enemy in _enemies)
        {
            if (enemy != null)
            {
                return;
            }

            Debug.Log("You killed all enemies!");

            _nextLevelIndex++;
            string nextLevelName = "Level" + _nextLevelIndex;
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
            return;
        }
    }
}
