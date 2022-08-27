using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public TextMeshPro text;
    public SpriteRenderer sprite;

    public void SetUp(string character)
    {
        text.text = character.ToUpper();
    }

    public void Success()
    {
        StartCoroutine(AnimateSuccess());
    }

    public void Fail()
    {
        StartCoroutine(AnimateFailure());
    }

    IEnumerator AnimateSuccess()
    {
        float time = 0;
        float total = 0.5f;

        Vector2 startPos = transform.position;
        Vector2 endPos = new Vector2(transform.position.x, transform.position.y + 2);

        Color startCol = Color.white;
        Color endCol = Color.green;

        while (time < total)
        {
            transform.position = Vector3.Lerp(startPos, endPos, time / total);
            sprite.color = Color.Lerp(startCol, endCol, time / total);

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }

    IEnumerator AnimateFailure()
    {
        float time = 0;
        float total = 0.5f;

        Vector2 startPos = transform.position;
        Vector2 endPos = new Vector2(transform.position.x, transform.position.y - 2);

        Color startCol = Color.white;
        Color endCol = Color.red;

        while (time < total)
        {
            transform.position = Vector3.Lerp(startPos, endPos, time / total);
            sprite.color = Color.Lerp(startCol, endCol, time / total);

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
