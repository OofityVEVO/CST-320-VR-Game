using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUI : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Panel.SetActive(true);
    }
}
