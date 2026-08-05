using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OfficePhone : MonoBehaviour
{
    [Header("音频组件")]
    public AudioSource phoneRing;
    public AudioSource bossVoice;
    public AudioSource timVoice;
    public AudioSource knockAudio;      // 敲门声
    public AudioSource timSecondVoice;  // Tim第二段语音

    [Header("UI对象")]
    public GameObject btnAnswer;
    public GameObject btnReject;
    public GameObject mailPanel;

    private bool isOperate = false;

    void Start()
    {
        if (btnAnswer != null) btnAnswer.SetActive(false);
        if (btnReject != null) btnReject.SetActive(false);
        if (mailPanel != null) mailPanel.SetActive(false);

        StartCoroutine(PhoneTotalFlow());
    }

    IEnumerator PhoneTotalFlow()
    {
        yield return new WaitForSeconds(5f);

        if (phoneRing != null && phoneRing.clip != null)
        {
            phoneRing.Play();
            yield return new WaitForSeconds(phoneRing.clip.length);
        }

        if (btnAnswer != null) btnAnswer.SetActive(true);
        if (btnReject != null) btnReject.SetActive(true);
    }

    public void OnClickAnswer()
    {
        Debug.Log("触发接听");
        if (isOperate) return;
        isOperate = true;
        HideAllButton();
        StartCoroutine(AnswerProcess());
    }

    IEnumerator AnswerProcess()
    {
        if (bossVoice != null && bossVoice.clip != null)
        {
            bossVoice.Play();
            yield return new WaitForSeconds(bossVoice.clip.length);
        }

        if (timVoice != null && timVoice.clip != null)
        {
            timVoice.Play();
            // 等待Tim第一段语音完整播放完毕
            yield return new WaitForSeconds(timVoice.clip.length);
        }

        // 进入统一后续流程：3秒敲门声 → 停顿1秒 → 第二段语音
        yield return StartCoroutine(AfterTimVoiceCommonLogic());
    }

    public void OnClickReject()
    {
        Debug.Log("触发拒接");
        if (isOperate) return;
        isOperate = true;

        HideAllButton();
        if (mailPanel != null)
        {
            mailPanel.SetActive(true);
        }

        StartCoroutine(RejectVoice());
    }

    IEnumerator RejectVoice()
    {
        yield return new WaitForSeconds(2f);
        if (timVoice != null && timVoice.clip != null)
        {
            timVoice.Play();
            yield return new WaitForSeconds(timVoice.clip.length);
        }

        yield return StartCoroutine(AfterTimVoiceCommonLogic());
    }

    /// <summary>
    /// 公共后置逻辑：Tim语音结束→等3秒敲门声→敲门声结束等1秒→播放第二段语音
    /// </summary>
    IEnumerator AfterTimVoiceCommonLogic()
    {
        // 1. Tim语音播放完成，等待3秒
        yield return new WaitForSeconds(3f);

        // 2. 播放敲门声
        if (knockAudio != null && knockAudio.clip != null)
        {
            knockAudio.Play();
            yield return new WaitForSeconds(knockAudio.clip.length);
        }

        // 3. 敲门声结束等待1秒
        yield return new WaitForSeconds(1f);

        // 4. 播放Tim第二条语音
        if (timSecondVoice != null && timSecondVoice.clip != null)
        {
            timSecondVoice.Play();
        }
    }

    void HideAllButton()
    {
        if (btnAnswer != null) btnAnswer.SetActive(false);
        if (btnReject != null) btnReject.SetActive(false);
    }
}