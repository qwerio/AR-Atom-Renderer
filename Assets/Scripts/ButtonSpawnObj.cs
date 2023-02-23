using UnityEngine;

public class ButtonSpawnObj : MonoBehaviour
{
    public Atom m_atomToPlace;
    public int m_elementNumber;
    public float m_elementMass;

    private bool m_isPressed;

    private void Awake()
    {
        m_isPressed = true;
    }
    public Atom spawnedObject
    {
        get
        {
            return m_atomToPlace;
        }
        private set
        {
            m_atomToPlace = value;
        }
    }
    public void SpawnObj()
    {
        if (m_isPressed)
        {
            spawnedObject._atomicNumber = m_elementNumber;
            spawnedObject._atomicMass = m_elementMass;
            spawnedObject = Instantiate(m_atomToPlace, Camera.main.transform.position, Camera.main.transform.rotation);
            m_isPressed = false;
        }
    }
}