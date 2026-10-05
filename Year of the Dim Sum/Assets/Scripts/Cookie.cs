using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Cookie : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    [SerializeField] AudioClip fortunCookieSound;
    private float timer;
    public void OnFortunCookieClick()
    {
        Debug.Log("Click cookie");
        AudioManager.instance.PlayASound(fortunCookieSound, 1f);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > timeToDestroy) Destroy(gameObject);
    }
}
