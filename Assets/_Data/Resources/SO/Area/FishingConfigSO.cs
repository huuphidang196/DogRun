using UnityEngine;

[CreateAssetMenu(
    fileName = "FishingConfig",
    menuName = "SO/Fishing Config")]
public class FishingConfigSO : ScriptableObject
{
    [Header("Thời gian chờ")]
    [SerializeField] private float minWaitTime = 3f;
    [SerializeField] private float maxWaitTime = 8f;

    [Header("Số cá nhận được")]
    [SerializeField] private int minFishAmount = 1;
    [SerializeField] private int maxFishAmount = 3;

    public float RandomWaitTime =>
        Random.Range(minWaitTime, maxWaitTime);

    public int RandomFishAmount =>
        Random.Range(minFishAmount, maxFishAmount + 1);
}