using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    // [SerializeField] private float scrollingVelocity = 5f;
    [SerializeField] private float defaultSpeed;
    // public int scrollingVelocity = 5;

    private void Start()
    {
        // GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }


    void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        ScrollingObject(defaultSpeed);
    }

    public void ScrollingObject(float scrollSpeed)
    {
        transform.Translate(-scrollSpeed * Time.deltaTime, 0f, 0f);
        // this.scrollingVelocity = scrollingVelocity;
    }


}
