using Features.Extensions.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSText : BaseTextView
{
    protected float fps1 = default;
    protected float fps2 = default;
    protected float deltaTime = default;
    [SerializeField]
    protected float frames = 0;
    [SerializeField]
    protected float startTime = 0;

    private void OnEnable()
    {
        StartCoroutine(CountFPS());
        StartCoroutine(ShowFPS());
    }

    private IEnumerator CountFPS()
    {
        frames = 0;
        startTime = Time.time;
        while (isActiveAndEnabled)
        {
            frames++;
            fps1 = frames / (Time.time - startTime);

            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            fps2 = 1.0f / deltaTime;
            yield return null;
            if (frames == 1000)
            {
                frames = 0;
                startTime = Time.time;
            }
        }
    }

    protected IEnumerator ShowFPS()
    {
        while (isActiveAndEnabled)
        {
            SetView(fps1.ToString() + " - " + fps2.ToString());
            yield return new WaitForSeconds(0.1f);
        }
    }
}
