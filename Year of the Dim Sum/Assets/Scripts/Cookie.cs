using UnityEngine;
using UnityEngine.UI;

public class Cookie : MonoBehaviour
{
    public void OnCookieClick()
    {
        Debug.Log("Click cookie");
        Destroy(gameObject);
    }
}
