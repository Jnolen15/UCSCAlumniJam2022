using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public TextMeshPro text;
    public SpriteRenderer sprite;

    public Color successColor;
    public Color failColor;

    public void SetUp(string character)
    {
        text.text = character.ToUpper();
        StartCoroutine(Grow());
    }

    public void Success()
    {
        StartCoroutine(AnimateSuccess());
    }

    public void Fail()
    {
        StartCoroutine(AnimateFail());
    }

    IEnumerator AnimateSuccess()
    {
        float time = 0;
        float total = 0.5f;

        Vector2 startPos = transform.position;
        Vector2 endPos = new Vector2(transform.position.x, transform.position.y - 2);

        Vector2 startScale = transform.localScale;
        Vector2 endScale = new Vector2(0.0f, 0.0f);

        while (time < total)
        {
            transform.position = Vector3.Lerp(startPos, endPos, time / total);
            transform.localScale = Vector2.Lerp(startScale, endScale, time / total);
            sprite.color = Color.Lerp(Color.white, successColor, time / total);

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }

    IEnumerator AnimateFail()
    {
        float time = 0;
        float total = 0.5f;

        Quaternion startAngle = transform.rotation;
        Quaternion endAngle = Quaternion.Euler(0, 0, 180);

        Vector2 startScale = transform.localScale;
        Vector2 endScale = new Vector2(0.0f, 0.0f);

        while (time < total)
        {
            transform.rotation = Quaternion.Lerp(startAngle, endAngle, time / total);
            transform.localScale = Vector2.Lerp(startScale, endScale, time / total);
            sprite.color = Color.Lerp(Color.white, failColor, time / total);

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }

    IEnumerator Grow()
    {
        float time = 0;
        float total = 0.2f;

        Vector2 startPos = transform.localScale;
        Vector2 endPos = new Vector2(0.5f, 0.5f);

        while (time < total)
        {
            transform.localScale = Vector2.Lerp(startPos, endPos, time / total);

            time += Time.deltaTime;
            yield return null;
        }
    }
}
