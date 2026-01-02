using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextPopUpManager : MonoBehaviour
{

    private int _previousDestruction;

    //sound
    private float _pitchUpper = 1;
    [SerializeField] float plopCooldown = 0.1f;
    private float _lastPlopTime;


    //model
    [SerializeField] private Transform _playerModel;
    private Vector3 _startModelSize;
    private float _modelTargetScale = 1;

    //player
    [SerializeField] private Transform _player;
    private Vector3 _startSize;
    private float _targetScale = 1;



    [SerializeField] private Transform _playerPosition;

    [SerializeField]
    private GameObject FloatingTextPrefab;

    private bool _hasGrown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _previousDestruction = ValueManager.DestructionCounter;
        _startSize = _player.transform.localScale;
        _startModelSize = _playerModel.transform.localScale;


    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        _modelTargetScale = 1 + ValueManager.SizeCounter/5;
        _targetScale = 1 + ValueManager.SizeCounter * 2f;


        if (ValueManager.DestructionCounter != _previousDestruction)
        {

            ShowPopUpText();
            SizeUpPlayer();
            PlayPlop();
            _previousDestruction = ValueManager.DestructionCounter;
        }
    }

    private void PlayPlop()
    {
        if (Time.time - _lastPlopTime < plopCooldown)
            return;

        _lastPlopTime = Time.time;
        _pitchUpper += 0.01f;
        AudioManager.Instance.PlaySFX("Plop");

        AudioManager.Instance.sfxSource.pitch = _pitchUpper;
    }

    private void SizeUpPlayer()
    {


        _player.transform.localScale = Vector3.Lerp(
            transform.localScale,
            _startSize * _targetScale,
            Time.deltaTime * 5f);




        StopAllCoroutines();
        StartCoroutine(ScaleBounce());



        //ValueManager.PlayerSize = transform.localScale.magnitude;
    }
    IEnumerator ScaleBounce()
    {
        Vector3 baseScale = _startModelSize * _modelTargetScale;
        Vector3 overshootScale = baseScale * 1.15f; // how much it "shoots up"

        float upTime = 0.1f;
        float downTime = 0.15f;
        float t = 0f;

        // Scale up fast
        while (t < 1f)
        {
            t += Time.deltaTime / upTime;
            _playerModel.transform.localScale = Vector3.Lerp(baseScale, overshootScale, t);
            yield return null;
        }

        t = 0f;

        // Scale back smoothly
        while (t < 1f)
        {
            t += Time.deltaTime / downTime;
            _playerModel.transform.localScale = Vector3.Lerp(overshootScale, baseScale, t);
            yield return null;
        }

        _playerModel.transform.localScale = baseScale;
    }
    private void ShowPopUpText()
    {
        if (FloatingTextPrefab != null)
        {
            Instantiate(FloatingTextPrefab, _playerPosition.position, Quaternion.identity, _playerPosition);
        }
    }
 
}
