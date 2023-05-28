using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Flow : MonoBehaviour
{
    public Text speedText;
    public TMP_Text speedValue;
    public Text disconnectText;
    public GameObject bulb;

    //public bool test;

    float currentSpeed = 0.0f;
    float inputSpeed = 0.0f;
    float calculatedSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        speedText.text = "Одометр:";
        disconnectText.DOText("Соединение не установлено!", 2, true, ScrambleMode.All).SetEase(Ease.Linear).SetAutoKill(false).SetLoops(-1, LoopType.Yoyo);
        speedText.DOColor(RandomColor(),3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        DOTween.PlayAll();
    }

    // Update is called once per frame
    void Update()
    {
        speedValue.text = calculatedSpeed.ToString();
        currentSpeed = calculatedSpeed;

        while (inputSpeed != SettingsHandler.speedFromServer)
        {
            inputSpeed = SettingsHandler.speedFromServer;
            StartCoroutine(ChangeSpeed(currentSpeed, SettingsHandler.speedFromServer, 10f));
        }
        
        if (SettingsHandler.online)
        {
            DOTween.Pause(disconnectText);
            disconnectText.text = "";
            bulb.GetComponent<Renderer>().material.color = Color.green;
        }

        else
        {
            DOTween.Play(disconnectText);
            bulb.GetComponent<Renderer>().material.color = Color.red;
        }
    }


    Color RandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
    }

    //Speed routine
    IEnumerator ChangeSpeed(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            calculatedSpeed = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        calculatedSpeed = v_end;
    }
}
