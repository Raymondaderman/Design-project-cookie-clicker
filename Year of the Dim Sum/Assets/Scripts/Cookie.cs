using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Cookie : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    private float timer;
    public void OnCookieClick()
    {
        Debug.Log("Click cookie");
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > timeToDestroy) Destroy(gameObject);
    }
}
