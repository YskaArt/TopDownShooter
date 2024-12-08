using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDeath : MonoBehaviour
{
    private AudioSource effect;
    [SerializeField] private AudioClip DeathSound;
    public GameObject bossLife;
    public GameObject Barrera;

    void Start()
    {
        effect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Agony()
    {
        effect.PlayOneShot(DeathSound);
        StartCoroutine(Death());
        
    }
    private IEnumerator Death()
    {
       
        yield return new WaitForSecondsRealtime(8f);
       
        Barrera.SetActive(false);

        bossLife.SetActive(false);


    }
}
