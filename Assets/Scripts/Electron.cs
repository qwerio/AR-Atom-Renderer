using System.Collections;
using UnityEngine;

public class Electron : MonoBehaviour
{
    public Vector3 _axis;
    private int _rotationAngle;

    public float _energyLevel;
    public float _subLevel;
    public bool _positiveSpin;

    public TrailRenderer trail;
    public LineRenderer orbit;
    public int timesSelected;

    private Coroutine teleportCoroutine;

    private void Start()
    {
        timesSelected = 0;
        trail = GetComponent<TrailRenderer>();

        orbit = GetComponent<LineRenderer>();
        orbit.enabled = false;
        orbit.startWidth = 0.08f;
        orbit.endWidth = 0.08f;
        orbit.material = trail.material;

        _positiveSpin = true;

        Vector3 pos = Random.insideUnitSphere.normalized * (_energyLevel + _subLevel);
        Vector3 tangent = Random.insideUnitSphere.normalized * (_energyLevel + _subLevel);

        while(pos == tangent)
        {
            tangent = Random.insideUnitSphere.normalized * (_energyLevel + _subLevel);
        }

        transform.localPosition = pos;

        if (_positiveSpin)
        {
            _axis = Vector3.Cross(tangent, pos);
        }

        _axis = -Vector3.Cross(tangent, pos);
        _rotationAngle = ((int)Random.Range(90.0f, 180.0f));

        CreateLineRenderer();
    }

    public void RotateElectron()
    {
        if (Atom.isRunning)
        {
            transform.RotateAround(transform.parent.position, _axis, _rotationAngle * Time.deltaTime);
        }
    }
    public IEnumerator TeleportElectron()
    {
        while (true)
        {
            float secondsToWait = Random.Range(0.3f, 3.0f);
            yield return new WaitForSeconds(secondsToWait);
            
            if (Atom.isRunning)
            {
                yield return null;
            }

            float rotationAngle = Random.Range(30.0f, 150.0f);
            transform.RotateAround(transform.parent.position, _axis, rotationAngle);
        }
    }
    public void BeginTeleport()
    {
        teleportCoroutine = StartCoroutine(TeleportElectron());
    }
    public void StopTeleport()
    {
        if(teleportCoroutine != null) 
        { 
            StopCoroutine(teleportCoroutine);
        }
    }
    private void CreateLineRenderer()
    {
        orbit.useWorldSpace = true;
        int angle = 5;
        orbit.positionCount = (360 / angle) + 1;
        int count = orbit.positionCount;

        for (int i = 0; i < count; i++)
        {
            transform.RotateAround(transform.parent.position, _axis, angle);
            orbit.SetPosition(i, transform.position);
        }
    }
}