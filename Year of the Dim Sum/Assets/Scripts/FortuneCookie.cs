using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

public class FortuneCookie : MonoBehaviour
{
    [SerializeField] GameObject[] fortuneCookiePrefab;
    [SerializeField] float timeToRespawn = 0.5f;
    [SerializeField] float currentTime = 0.5f;
    [SerializeField] float xRangeForSpawn;
    [SerializeField] float yRangeForSpawn;

    [SerializeField] bool canSpawnCookie = true;

    [SerializeField] Vector2 offset;

    [SerializeField] Transform placeToPutCookie;

    void Start()
    {
        canSpawnCookie = true;

    }

    void Update()
    {
        #region Cookie Spawning
        if (canSpawnCookie)
        {
            if (fortuneCookiePrefab.Length == 1)
            {
                Instantiate(fortuneCookiePrefab[0], CookiePos(), Quaternion.identity, placeToPutCookie);
            }
            else
            {
                int cookieToSpawn = Random.Range(0, fortuneCookiePrefab.Length);

                Instantiate(fortuneCookiePrefab[cookieToSpawn], CookiePos(), Quaternion.identity, placeToPutCookie);
            }
            canSpawnCookie = false;
        }

        if (currentTime <= 0)
        {
            canSpawnCookie = true;
            currentTime = timeToRespawn;
        }

        currentTime -= Time.deltaTime;
        #endregion

    }

    Vector2 CookiePos()
    {
        Vector2 newPos = new(Random.Range(-xRangeForSpawn, xRangeForSpawn), Random.Range(-yRangeForSpawn, yRangeForSpawn));

        Vector2 finalPos = newPos + offset;

        return finalPos;
    }


}
