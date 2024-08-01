using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventCameraFade : MonoBehaviour
{
    public IEnumerator CameraFadeEffect(float color, RawImage fadeImage, float alphaValue, float targetAlpha, float speed)
    {
        bool inOrOut = alphaValue > targetAlpha ? false : true; // Verificar a direção do efeito (Fade in ou Fade out)

        if (inOrOut)
        {
            while (alphaValue <= targetAlpha)
            {
                alphaValue += Time.deltaTime * speed;
                fadeImage.color = new Color(color, color, color, alphaValue);
                yield return null;
            }
        }
        else
        {
            while (alphaValue >= targetAlpha)
            {
                alphaValue -= Time.deltaTime * speed;
                fadeImage.color = new Color(color, color, color, alphaValue);
                yield return null;
            }
        }
    }
}
