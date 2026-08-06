
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RadioFinalDialog : MonoBehaviour
{
    [Header("沙沙噪音音源")]
    public AudioSource noiseAudio;

    [Header("停留2秒后播放 Tim 的语音")]
    public AudioSource timVoice;

    [Header("UI：C按钮承载问题文本，A/B选择按钮")]
    public GameObject btnQuestionC;
    public GameObject btnA;
    public GameObject btnB;

    [Header("A选项：前置音频 → 祖父语音")]
    public AudioSource preVoiceA;
    public AudioSource voiceGrandpaA;

    [Header("B选项祖父语音")]
    public AudioSource voiceGrandpaB;

    [Header("检测参数")]
    public Transform playerTr;
    public float playNoiseRange = 10f;    // 距离≤5米时播放噪音
    public float triggerRange = 3.2f;   // 靠近3.2米停止噪音触发剧情

    private bool triggered = false;
    private bool noiseHasPlayed = false; // 标记噪音是否已经播放过

    void Start()
    {
        // 开局禁止播放噪音
        if (noiseAudio != null)
        {
            noiseAudio.Stop();
        }

        // UI全部隐藏
        btnQuestionC.SetActive(false);
        btnA.SetActive(false);
        btnB.SetActive(false);
    }

    void Update()
    {
        float dis = Vector3.Distance(transform.position, playerTr.position);

        #region 距离5米时播放噪音，且只播放一次
        if (!noiseHasPlayed && !triggered && dis <= playNoiseRange)
        {
            noiseAudio.Play();
            noiseHasPlayed = true; // 标记已播放，永远不再重复播放
        }
        #endregion

        if (triggered) return;

        // 走到近距离区间，停止噪音、开启剧情
        if (dis <= triggerRange)
        {
            StartCoroutine(PlayerEnterProcess());
        }
    }

    IEnumerator PlayerEnterProcess()
    {
        triggered = true;
        noiseAudio.Stop(); // 靠近立刻关掉噪音

        yield return new WaitForSeconds(2f);

        // 播放Tim语音
        if (timVoice != null) timVoice.Play();
        yield return new WaitForSeconds(timVoice.clip.length);

        // 弹出UI按钮
        btnQuestionC.SetActive(true);
        btnA.SetActive(true);
        btnB.SetActive(true);
    }

    // 选择A
    public void ChooseA()
    {
        HideAllUI();
        StartCoroutine(PlayAudioA());
    }
    IEnumerator PlayAudioA()
    {
        if (preVoiceA != null)
        {
            preVoiceA.Play();
            yield return new WaitForSeconds(preVoiceA.clip.length);
        }
        if (voiceGrandpaA != null) voiceGrandpaA.Play();
    }

    // 选择B
    public void ChooseB()
    {
        HideAllUI();
        if (voiceGrandpaB != null) voiceGrandpaB.Play();
    }

    void HideAllUI()
    {
        btnQuestionC.SetActive(false);
        btnA.SetActive(false);
        btnB.SetActive(false);
    }
}