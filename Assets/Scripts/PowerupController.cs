using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    [SerializeField]
    private float cooldownTime;
    [SerializeField]
    private float startTime;

    public GameObject[] powerups;
    public BoxCollider2D GridArea;

    public GameObject shieldUI;
    public GameObject multiplierUI;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(startTime);
        StartCoroutine(LoopedSpawning());
    }

    IEnumerator LoopedSpawning()
    {
        Bounds bounds = GridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        GameObject powerup = Instantiate(powerups[Random.Range(0, powerups.Length)], new Vector3(Mathf.Round(x), Mathf.Round(y), 0), Quaternion.identity);

        yield return new WaitForSeconds(cooldownTime);
        Destroy(powerup);
        yield return new WaitForSeconds(cooldownTime);
        StartCoroutine(LoopedSpawning());
    }

    public void RefreshUI(PowerupType ptype,bool value)
    {
        switch (ptype)
        {
            case PowerupType.Multiplier:multiplierUI.SetActive(value);
                break;
            case PowerupType.Shield:shieldUI.SetActive(value);
                break;
        }

    }
}
