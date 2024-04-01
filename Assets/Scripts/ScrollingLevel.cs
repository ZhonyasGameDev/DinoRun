using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingLevel : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;

    private void Start()
    {
        // GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    /*     private void GameManager_OnStateChanged(object sender, System.EventArgs e)
        {
            if (!GameManager.Instance.IsGamePlaying()) return;
            ScrollingObject(GameManager.Instance.Level(defaultSpeed));
        } */

    void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        ScrollingObject(GameManager.Instance.Level(defaultSpeed));
    }

    public void ScrollingObject(float scrollSpeed)
    {
        transform.Translate(-scrollSpeed * Time.deltaTime, 0f, 0f);
        // this.scrollingVelocity = scrollingVelocity;
    }
}
