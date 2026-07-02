using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("田野场景名称")]
    public string fieldSceneName = "FieldScene";
    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        triggered = true;
        // 直接加载田野场景，无黑屏过渡动画
        SceneManager.LoadScene(fieldSceneName);
    }
}