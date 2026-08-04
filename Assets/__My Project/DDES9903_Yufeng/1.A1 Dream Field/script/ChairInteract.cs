using UnityEngine;

public class ChairInteract : MonoBehaviour
{
    [Header("放松独白音频")]
    public AudioClip relaxVoice;
    [Header("玩家标签统一为Player，不用修改")]
    private bool playedOnce = false;

    
    void OnTriggerEnter(Collider other)
    {
        
        if (playedOnce || !other.CompareTag("Player") || relaxVoice == null)
            return;

        playedOnce = true;
       
        AudioSource.PlayClipAtPoint(relaxVoice, Camera.main.transform.position, 0.6f);
        Debug.Log("玩家靠近椅子，播放放松台词");
    }
}