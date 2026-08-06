// 所有using必须放在脚本最顶部
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RadioFinalDialog : MonoBehaviour
{
    [Header("持续吸引的沙沙噪音")]
    public AudioSource noiseAudio;

    [Header("停留2秒后播放 Tim 的语音")]
    public AudioSource timVoice;

    [Header("UI 组件")]
    public Text tipsText;
    public GameObject btnA;
    public GameObject btnB;

    [Header("选项对应的祖父语音")]
    public AudioSource voiceGrandpaA;
    public AudioSource voiceGrandpaB;

    [Header("设置")]
    public Transform playerTr;
    public float detectRange = 3.2f;

    private bool triggered = false;

    void Start()
    {
        // 开局循环播放噪音吸引玩家
        if (noiseAudio != null)
        {
            noiseAudio.loop = true;
            noiseAudio.Play();
        }

        // 界面默认隐藏
        tipsText.gameObject.SetActive(false);
        btnA.SetActive(false);
        btnB.SetActive(false);
    }

    void Update()
    {
        if (triggered) return;

        float dis = Vector3.Distance(transform.position, playerTr.position);
        // 玩家进入范围
        if (dis <= detectRange)
        {
            StartCoroutine(PlayerStayProcess());
        }
    }

    IEnumerator PlayerStayProcess()
    {
        triggered = true;

        // 玩家靠近，立刻关掉沙沙噪音
        if (noiseAudio != null)
            noiseAudio.Stop();

        // 停留等待2秒
        yield return new WaitForSeconds(2f);

        // 播放Tim语音
        if (timVoice != null)
            timVoice.Play();
        yield return new WaitForSeconds(timVoice.clip.length);

        // 弹出文字 + 两个选择按钮
        tipsText.gameObject.SetActive(true);
        btnA.SetActive(true);
        btnB.SetActive(true);
    }

    // 选择A
    public void ChooseA()
    {
        HideUI();
        if (voiceGrandpaA != null)
            voiceGrandpaA.Play();
    }

    // 选择B
    public void ChooseB()
    {
        HideUI();
        if (voiceGrandpaB != null)
            voiceGrandpaB.Play();
    }

    // 选完隐藏UI
    void HideUI()
    {
        tipsText.gameObject.SetActive(false);
        btnA.SetActive(false);
        btnB.SetActive(false);
    }
}