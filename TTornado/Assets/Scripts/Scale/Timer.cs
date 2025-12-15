using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timer;

    [SerializeField]
    private TMP_Text _timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (!ValueManager.GameHasEnded)
        {
            _timer += Time.deltaTime;

        }
        _timerText.text = $"Time: {_timer:F1}";


        ValueManager.Timer = _timer;
    }
}
