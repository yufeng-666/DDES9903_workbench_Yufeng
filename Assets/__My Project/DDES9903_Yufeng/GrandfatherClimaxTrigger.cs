using UnityEngine;
using UnityEngine.SceneManagement;

public class GrandfatherClimaxTrigger : MonoBehaviour
{
    [Header("祖父高潮独白音频")]
    public AudioClip grandpaVoice;
    [Header("语音播放完等待几秒切场景")]
    public float waitAfterVoice = 3f;
    [Header("办公室场景文件名（和Build列表完全一致）")]
    public string officeSceneName = "OfficeScene";

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player") || grandpaVoice == null) return;
        triggered = true;

        // 播放祖父独白
        AudioSource.PlayClipAtPoint(grandpaVoice, Camera.main.transform.position, 0.7f);
        Debug.Log("播放祖父核心独白");

        // 获取音频时长，语音播完再额外等待设定时间后切换场景
        float totalWaitTime = grandpaVoice.length + waitAfterVoice;
        Invoke(nameof(BackToOffice), totalWaitTime);
    }

    void BackToOffice()
    {
        Debug.Log("梦境结束，跳转回办公室");
        SceneManager.LoadScene(officeSceneName);
    }
}