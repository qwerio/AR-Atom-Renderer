using UnityEngine;

public class PeriodicElement : MonoBehaviour
{
    public int number;
    public float mass;
    public int timesSelected = 0;

    public int ElementNumber
    {
        get { return number; }
        set { number = value; }
    }
    public float ElementMass
    {
        get { return mass; }
        set { mass = value; }
    }
    public bool IsSelected { get; set; }
}
