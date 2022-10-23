using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrosshairCursor : MonoBehaviour
{
    private SpriteRenderer Rend;
    public Sprite MouseCursorCrosshair;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        //Get the Sprite component 
        Rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            Rend.sprite = MouseCursorCrosshair;
        }
    }
}
