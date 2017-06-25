using UnityEngine;
using System.Collections;

public class Consumer : MonoBehaviour {
    public static Consumer instance;
    public int multiplier = 3;
    public int energy = 0;

    void Start () {
        instance = this;
    }
    
    void Update () {
        
    }

    public void ConsumeBone () {
        energy += multiplier;
    }

    public bool CanDig () {
        return energy > 0;
    }

    public void ConsumeEnergy () {
        energy--;
    }
}
