using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollParallax : MonoBehaviour
{
    //    [SerializeField] private float maxPosition;
    [SerializeField] private float spriteWidth;
    [SerializeField] private float scrollSpeed;
    // private Vector3 startPosition;
    private SpriteRenderer spriteRenderer;

    // public float initialScrollSpeed;
    // public float targetScrollSpeed;
    // public float transitionDuration = 1f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.sprite.bounds.size.x * 10;

        // startPosition = transform.position;
        // Debug.Log(startPosition);
    }

    void Update()
    {
        /*     transform.Translate(-scrollSpeed * Time.deltaTime, 0f, 0f);

            if (transform.position.x < -spriteWidth)
            {
                transform.localPosition = startPosition + Vector3.right * spriteWidth;
            } */

        if (!GameManager.Instance.IsGamePlaying()) return;

        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, spriteWidth);
        transform.position = Vector3.left * newPosition;



        /*         scrollSpeed = Mathf.Lerp(scrollSpeed, targetScrollSpeed, Time.deltaTime / transitionDuration);
                Debug.Log(scrollSpeed); */
    }

}
