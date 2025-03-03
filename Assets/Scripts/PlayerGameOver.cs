using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject[] Guards;

    PlayerMovement ScriptMovement;
    Camera Cam;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        ScriptMovement = GetComponent<PlayerMovement>();
        Cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver)
        {
            GameOver();
        }
        else
        {
            GameOverUI.SetActive(false);
        }
    }

    void GameOver()
    {
        GameOverUI.SetActive(true);
        Cam.clearFlags = CameraClearFlags.SolidColor;
        Cam.backgroundColor = Color.black;
        Cam.farClipPlane = 2f;
        ScriptMovement.enabled = false;

        foreach (GameObject guard in Guards)
        {
            guard.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            if (other is SphereCollider)
            {
                isGameOver = true;
            }
        }
    }
}
