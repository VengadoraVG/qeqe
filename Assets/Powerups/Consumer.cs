using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Consumer : MonoBehaviour {
    public static Consumer instance;
    public int multiplier = 3;
    public int energy = 0;

    public Text energyIndicator;

    void Start () {
        instance = this;
    }
    
    void Update () {
        energyIndicator.text = energy + "";
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

    public void SetEnergy (int energy) {
        this.energy = energy;
    }
}
