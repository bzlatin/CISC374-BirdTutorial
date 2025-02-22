using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;
   public AudioClip gameover;
   public AudioClip jump;
   public AudioClip newpoint;
   public AudioClip highScoreSound;


   

   public void PlaySFX(AudioClip clip) 
   {
        SFXSource.PlayOneShot(clip);
   }

   
}
