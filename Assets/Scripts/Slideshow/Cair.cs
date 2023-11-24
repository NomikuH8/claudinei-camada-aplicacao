using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cair : MonoBehaviour
{
    [SerializeField]
    private float shakeFrequency;
    [SerializeField]
    private float timeShake;
    [SerializeField]
    private bool hasSomethingToEnable;
    [SerializeField]
    private GameObject somethingToEnable;
    [SerializeField]
    private Player paddle;
    [SerializeField]
    private Player paddle2;
    [SerializeField]
    private float botSpeedFaster;

    private Transform cam;
    private Vector2 originalCamPos;
    private bool isCamShaking;

    void Awake()
    {
        cam = Camera.main.transform;
        originalCamPos = cam.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            GetComponent<Animator>().SetTrigger("fall");
        if (isCamShaking)
            CamShake();
    }

    void CamShake()
    {
        cam.position = originalCamPos + Random.insideUnitCircle * shakeFrequency;
    }

    public void ActivateSomethingToPlay()
    {
        somethingToEnable.SetActive(true);
    }

    public void EnablePong()
    {
        paddle.isPerson = true;
        paddle2.botSpeed = botSpeedFaster;
    }

    public void StartCamShake()
    {
        StartCoroutine(CamShakeDisable());
    }

    IEnumerator CamShakeDisable()
    {
        isCamShaking = true;
        yield return new WaitForSeconds(timeShake);
        isCamShaking = false;
        cam.position = originalCamPos;
    }
}
