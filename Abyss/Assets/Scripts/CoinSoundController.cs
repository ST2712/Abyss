using UnityEngine;

public class CoinSoundController : MonoBehaviour
{
    public static CoinSoundController Instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void playSound(AudioClip audioClip)
    {
        Debug.Log("Playing sound");
        audioSource.PlayOneShot(audioClip);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
