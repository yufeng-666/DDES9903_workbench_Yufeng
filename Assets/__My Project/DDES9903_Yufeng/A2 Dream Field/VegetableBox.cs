using UnityEngine;

public class VegetableBox : MonoBehaviour
{
    public int limitNum = 3;
    public AudioSource finishAudio;
    [HideInInspector] public int currentNum = 0;
    private bool audioDone = false;

    public void AddVegetable()
    {
        currentNum++;
        // 数量大于3播放语音，只播放一次
        if (currentNum > limitNum && audioDone == false)
        {
            audioDone = true;
            if (finishAudio != null)
                finishAudio.Play();
        }
    }
}