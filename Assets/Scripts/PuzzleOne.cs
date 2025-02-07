using UnityEngine;
using UnityEngine.Events; 

public class PuzzleOne : MonoBehaviour
{
    public UnityEvent MochiEvent1;


    void Start()
    {
        MochiEvent1 = new UnityEvent(); 
        MochiEvent1.AddListener(PuzzleSolve); 
    }

    void OnTriggerEnter(Collider collide)
    {
        if (collide.CompareTag("ChitterKin")) 
        {
            MochiEvent1.Invoke();
            MochiEvent1.RemoveListener(PuzzleSolve); 
        }
    }

    void PuzzleSolve()
    {
        Debug.Log("Mochi Escaped!! :DD");
    }
}
