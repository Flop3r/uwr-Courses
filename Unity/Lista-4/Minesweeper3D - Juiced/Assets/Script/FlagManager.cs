using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlagManager : MonoBehaviour
{
    [SerializeField] private GameObject flagPrefab;
    [SerializeField] private Vector2 gridSize = new Vector2(-4f, 5f);
    [SerializeField] private float spacing = 0.5f;

    public GameObject[,] flags;

    [Header("Animation Settings")]
    [SerializeField] private float fallDuration = 1.0f;
    [SerializeField] private Ease fallEaseType = Ease.OutBounce;
    [SerializeField] private float scaleDuration = 0.2f;
    [SerializeField] private float initialScaleFactor = 0.5f;
    [SerializeField] private Vector3 targetScale = new Vector3(0.75f, 0.75f, 0.75f);

    [Header("Particles")]
    [SerializeField] private GameObject particlePrefab;

    [Header("Sounds")]
    [SerializeField] private AudioSource spawnFlagSoundEffect;


    void Start()
    {
        int rows = Mathf.CeilToInt((gridSize.y - gridSize.x) / spacing) + 1;
        int columns = Mathf.CeilToInt((gridSize.y - gridSize.x) / spacing) + 1;

        flags = new GameObject[rows, columns];
    }

    public void SpawnFlag(int x, int y)
    {
        Vector3 spawnPosition = new Vector3(gridSize.x + x - spacing, 2f, gridSize.x + y - spacing);

        GameObject newFlag = Instantiate(flagPrefab, spawnPosition, Quaternion.identity);
        flags[x, y] = newFlag;

        newFlag.transform.SetParent(transform);
        newFlag.name = "Flag (" + y + ", " + x + ")";

        // Post Processing

        // Fall Animation
        newFlag.transform.DOMoveY(0.005f, fallDuration).SetEase(fallEaseType);

        // Scale Animation
        newFlag.transform.DOScale(targetScale * initialScaleFactor, scaleDuration)
        .SetDelay(0.2f)
        .OnComplete(() => 
        {   
            spawnFlagSoundEffect.Play();
            newFlag.transform.DOScale(targetScale, scaleDuration);
            Instantiate(particlePrefab, newFlag.transform.position, Quaternion.identity); // Particles
        });
    }

    public void DestroyFlag(int x, int y)
{
    if (flags[x, y] != null)
    {
        GameObject destroyedFlag = flags[x, y]; 

        // Zmniejszanie => usunięcie flagi 
        destroyedFlag.transform.DOScale(Vector3.zero, scaleDuration)
            .OnComplete(() =>
            {
                Destroy(destroyedFlag);
            });

        flags[x, y] = null;
    }
}


    public void DestroyAllFlags()
    {
        for (int i = 0; i < flags.GetLength(0); i++)
        {
            for (int j = 0; j < flags.GetLength(1); j++)
            {
                if (flags[i, j] != null)
                {
                    Destroy(flags[i, j]);
                    flags[i, j] = null;
                }
            }
        }
    }
}
