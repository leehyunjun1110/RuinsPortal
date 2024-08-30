using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weight : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int weight;
    public int maxWeight;
    
    public bool isFull;

    

    // Start is called before the first frame update
    void Start()
    {
        weight = 0;
        
        isFull = false;
    }

    // Update is called once per frame
   

   

    public string GetText()
    {
        if(isFull)
        {return "full";}
        else
        {
            return weight.ToString();
        }
        
        
    }

    public void Interaction()
    {

         weight ++;
        if(weight >= maxWeight)
        {
            weight = maxWeight;
            isFull = true;
        }
    }
}
