using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float duration;
    private static readonly int percentID = Shader.PropertyToID("_Percent");
    private Material myMat; 

    public static Checkpoint CurrentCheckpoint { get; private set; }    

    public static Vector3 GetCheckpointPos()
    {
        return CurrentCheckpoint.transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        myMat = GetComponent<MeshRenderer>().material; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CurrentCheckpoint != null)
        {
            if (CurrentCheckpoint == this)
            {
                return; 
            }
            CurrentCheckpoint.ResetColor(); 
        }

        CurrentCheckpoint = this;
        StartCoroutine(SetColor()); 
    }

    private void ResetColor()
    {
        StopAllCoroutines();
        myMat.SetFloat(percentID, 0); 
    }

    private IEnumerator SetColor()
    {
        float currentTime = 0; 
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            myMat.SetFloat(percentID, currentTime / duration); 
            yield return null;
        }
    }
    
}
