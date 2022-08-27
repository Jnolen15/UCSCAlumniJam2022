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
        Vector2 endPos = new Vector2(transform.position.x, transform.position.y + 2);
        Color endCol = Color.green;
        StartCoroutine(Animate(endPos, endCol));
    }

    public void Fail()
    {
        Vector2 endPos = new Vector2(transform.position.x, transform.position.y - 2);
        Color endCol = Color.red;
        StartCoroutine(Animate(endPos, endCol));
    }

    IEnumerator Animate(Vector2 endPos, Color endCol)
    {
        float time = 0;
        float total = 0.5f;

        Vector2 startPos = transform.position;

        Color startCol = Color.white;

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
