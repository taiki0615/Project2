using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeMagnitude;

    private Player player;
    private Vector3 initPos;
    private float shakeCount;
    private int CurrentPlayerHP;

    void Start()
    {
        player = Object.FindFirstObjectByType<Player>();
        CurrentPlayerHP = player.GetHP();
        initPos = transform.position;
    }
    void Update()
    {
        ShakeCheck();
        FollowPlayer();
    }
    private void ShakeCheck()
    {
        if(CurrentPlayerHP != player.GetHP())
        {
            CurrentPlayerHP = player.GetHP();
            shakeCount = 0.0f;
            StartCoroutine(Shake());
        }
    }
    IEnumerator Shake()
    {
        Vector3 initPos = transform.position;

        while(shakeCount < shakeTime)
        {
            float x = initPos.x + Random.Range(-shakeMagnitude,shakeMagnitude);
            float y = initPos.y + Random.Range(-shakeMagnitude,shakeMagnitude);
            transform.position = new Vector3(x, y, initPos.z);

            shakeCount += Time.deltaTime;

            yield return null;
        }

        transform.position = initPos;
    }
    private void FollowPlayer()
    {
        if(player == null) return;
        
        float x = player.transform.position.x;
        x = Mathf.Clamp(x, initPos.x, Mathf.Infinity);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

}