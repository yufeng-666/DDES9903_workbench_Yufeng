using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OfficeGameLogic : MonoBehaviour
{
    [Header("音频")]
    public AudioSource timComplainAudio;    // 开局Tim抱怨
    public AudioSource phoneRingAudio;     // 电话铃声
    public AudioSource bossCallAudio;      // 老板电话语音
    public AudioSource timVoice1;
    public AudioSource timVoice2;
    public AudioSource knockDoorAudio;     // 敲门声
    public AudioSource timAfterKnockVoice; // 敲门响起后Tim的语音

    [Header("UI")]
    public GameObject choosePanel;
    public Button btnAnswer;
    public Button btnReject;
    public GameObject mailTipText;         // 老板邮件UI文本

    // 全局压力数值，农场场景可读取
    public static int stressValue = 0;

    private bool alreadySelect = false;

    void Start()
    {
        choosePanel.SetActive(false);
        mailTipText.SetActive(false);

        btnAnswer.onClick.AddListener(OnAnswerPhone);
        btnReject.onClick.AddListener(OnRejectPhone);

        StartCoroutine(StartOfficeSequence());
    }

    IEnumerator StartOfficeSequence()
    {
        // 1. 播放Tim加班抱怨
        timComplainAudio.Play();
        yield return new WaitForSeconds(timComplainAudio.clip.length);

        // 2. 电话响起
        phoneRingAudio.Play();
        yield return new WaitForSeconds(2.2f);

        // 弹出选择按钮
        choosePanel.SetActive(true);
    }

    #region 接听电话分支
    void OnAnswerPhone()
    {
        if (alreadySelect) return;
        alreadySelect = true;
        choosePanel.SetActive(false);
        phoneRingAudio.Stop();
        stressValue += 1;

        StartCoroutine(AnswerBranch());
    }

    IEnumerator AnswerBranch()
    {
        // 老板说话
        bossCallAudio.Play();
        yield return new WaitForSeconds(bossCallAudio.clip.length);

        // Tim两段语音
        timVoice1.Play();
        yield return new WaitForSeconds(timVoice1.clip.length);

        timVoice2.Play();
        yield return new WaitForSeconds(timVoice2.clip.length);

        // 等待5秒敲门声响起
        yield return new WaitForSeconds(5f);
        PlayKnockEvent();
    }
    #endregion

    #region 拒接电话分支
    void OnRejectPhone()
    {
        if (alreadySelect) return;
        alreadySelect = true;
        choosePanel.SetActive(false);
        phoneRingAudio.Stop();
        stressValue += 1;

        StartCoroutine(RejectBranch());
    }

    IEnumerator RejectBranch()
    {
        // 拒接3秒后弹出邮件
        yield return new WaitForSeconds(3f);
        mailTipText.SetActive(true);

        yield return new WaitForSeconds(1.2f);

        // Tim两段语音
        timVoice1.Play();
        yield return new WaitForSeconds(timVoice1.clip.length);

        timVoice2.Play();
        yield return new WaitForSeconds(timVoice2.clip.length);

        // 等待5秒敲门声响起
        yield return new WaitForSeconds(5f);
        PlayKnockEvent();
    }
    #endregion

    // 敲门声 + Tim语音，之后交由你自己的转场系统执行黑屏切农场
    void PlayKnockEvent()
    {
        knockDoorAudio.Play();
        timAfterKnockVoice.Play();

        // 敲门声结束后，你原本做好的转场逻辑在这里触发即可
        // 例：你的转场脚本.PlayFade();
    }
}