using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    [SerializeField]
    PeriodicTable m_PeriodicTablePrefab;
    [SerializeField]
    Atom m_AtomToPlace;
    [SerializeField]
    GameObject PlacementIndicator;
    [SerializeField]
    Canvas CanvasObject;
    [SerializeField]
    Camera arCamera;
    [SerializeField]
    Material elementMaterial;

    private ARRaycastManager m_RaycastManager;
    private GameObject visual;
    private Color elColor;

    private bool m_IsPlacementValid;
    private bool isPeriodicTablePlaced;
   
    private GameObject atomParent;

    public PeriodicTable PeriodicTable
    {
        get { return m_PeriodicTablePrefab; }
        private set { m_PeriodicTablePrefab = value; }
    }
    public List<Atom> PeriodicElements { get; private set; }
    public Atom spawnedAtom;

    private void Awake()
    {
        atomParent = new GameObject("Atom");

        CanvasObject.enabled = false;

        m_RaycastManager = FindObjectOfType<ARRaycastManager>();

        visual = Instantiate(PlacementIndicator);
        visual.SetActive(false);

        m_IsPlacementValid = false;
        isPeriodicTablePlaced = false;

        PeriodicElements = new List<Atom>();

        elColor = elementMaterial.color;
    }
    void Update()
    {
        if (!isPeriodicTablePlaced)
        {
            UpdatePlacementIndicator();
            TapToPlacePeriodicTable();
        }
        else
        {
            GetTouch();
        }
    }
    private void UpdatePlacementIndicator()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        m_RaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        m_IsPlacementValid = hits.Count > 0;
        if (m_IsPlacementValid)
        {
            visual.transform.position = hits[0].pose.position;

            Vector3 cameraForward = arCamera.transform.forward;
            Vector3 cameraBearing = new Vector3(cameraForward.x, 0.0f, cameraForward.z).normalized;
            visual.transform.rotation = Quaternion.LookRotation(cameraBearing);
            visual.SetActive(true);
        }
        else
        {
            visual.SetActive(false);
        }
    }
    private bool IsScreenTouched()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
            return true;
#else
        if (Input.touchCount > 0)
            return true;
#endif
        return false;
    }
    private void TapToPlacePeriodicTable()
    {
#if UNITY_EDITOR
        if (IsScreenTouched())
        {
            PeriodicTable = Instantiate(m_PeriodicTablePrefab, Vector3.zero, Quaternion.identity);
            arCamera.transform.position = PeriodicTable.transform.position + new Vector3(18.0f, -8.0f, -30.0f);
            isPeriodicTablePlaced = true;
        }
#else
        if (m_IsPlacementValid && IsScreenTouched())
        {
            PeriodicTable = Instantiate(m_PeriodicTablePrefab, visual.transform.position + new Vector3(0.0f, 10.0f, 0.0f), visual.transform.rotation);
            isPeriodicTablePlaced = true;
            visual.SetActive(false);
        }
#endif
    }
    private void GetTouch()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hit = Physics.Raycast(ray, out hitInfo);

            if (!CanvasObject.enabled && hit)
            {
                CanvasObject.enabled = true;
            }

            if(hit)
            {
                bool electronHit = !Atom.isRunning && (hitInfo.collider.GetType() == typeof(SphereCollider));
                
                PeriodicElement hitElement = hitInfo.transform.gameObject.GetComponent<PeriodicElement>();
                bool elementHit = (hitInfo.collider.GetType() == typeof(BoxCollider)) && hitElement.timesSelected == 0;

                if (electronHit)
                {
                    Electron selectedElectron = hitInfo.transform.gameObject.GetComponent<Electron>();
                    Atom selectedAtom = hitInfo.transform.parent.gameObject.GetComponent<Atom>();
                    EnableOrbits(selectedAtom, selectedElectron);
                    
                }
                else if (elementHit)
                {
                    ChangeAtoms(hitElement);
                }
            }         
        }
#else
        if (Input.touchCount > 0)
        {
            RaycastHit hitInfo = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hit = Physics.Raycast(ray, out hitInfo);

            if (!CanvasObject.enabled && hit)
            {
                CanvasObject.enabled = true;
            }
            bool electronHit = hit && !Atom.isRunning && (hitInfo.collider.GetType() == typeof(SphereCollider));

            PeriodicElement hitElement = hitInfo.transform.gameObject.GetComponent<PeriodicElement>();
            bool elementHit = hit && (hitInfo.collider.GetType() == typeof(BoxCollider)) && hitElement.timesSelected == 0;

            if (electronHit)
            {
                Electron selectedElectron = hitInfo.transform.gameObject.GetComponent<Electron>();
                Atom selectedAtom = hitInfo.transform.parent.gameObject.GetComponent<Atom>();
                EnableOrbits(selectedAtom, selectedElectron);
            }
            else if (elementHit)
            {
                ChangeAtoms(hitElement);
            }
        }
#endif
    }
    private void EnableOrbits(Atom atom, Electron electron)
    {
        for (int i = 0; i < atom._electrons.Count; i++)
        {
            if (atom._electrons[i] == electron)
            {
                if (atom._electrons[i].timesSelected == 1)
                {
                    atom._electrons[i].orbit.enabled = false;
                    atom._electrons[i].timesSelected = 0;
                    continue;
                }

                atom._electrons[i].orbit.enabled = true;
                atom._electrons[i].timesSelected++;
            }
            else
            {
                atom._electrons[i].orbit.enabled = false;
                atom._electrons[i].timesSelected = 0;
            }
        }
    }
    private void ChangeAtoms(PeriodicElement selected)
    {
        int flag = -1;
        for (int i = 0; i < PeriodicTable.periodicElements.Count; i++)
        {
            MeshRenderer meshRenderer = PeriodicTable.periodicElements[i].GetComponent<MeshRenderer>();

            if (selected != PeriodicTable.periodicElements[i])
            {
                elColor.a = 0.2f;
                meshRenderer.material.color = elColor;
            }
            else
            {
                elColor.a = 1.0f;
                meshRenderer.material.color = elColor;
                flag = i;
            }
        }
        InstantiateAtom(PeriodicTable.periodicElements[flag].number - 1);
    }
    private void InstantiateAtom(int atomicNumber)
    {
        if (atomParent.transform.childCount > 0)
        {
            Destroy(spawnedAtom.gameObject);
        }
        
        Vector3 atomPosition = new Vector3();
        GetAtomPosition(ref atomPosition);

        spawnedAtom = Instantiate(m_AtomToPlace, atomParent.transform).GetComponent<Atom>();
        spawnedAtom._atomicMass = PeriodicTable.periodicElements[atomicNumber].mass;
        spawnedAtom._atomicNumber = PeriodicTable.periodicElements[atomicNumber].number;
        spawnedAtom.transform.position = atomPosition;
        spawnedAtom.name = PeriodicTable.periodicElements[atomicNumber].name;
    }
    private void GetAtomPosition(ref Vector3 positionToSpawn)
    {
        float xA = PeriodicTable.periodicElements[0].transform.position.x;
        float yA = PeriodicTable.periodicElements[0].transform.position.y;
        float xB = PeriodicTable.periodicElements[PeriodicTable.periodicElements.Count - 1].transform.position.x;
        float yB = PeriodicTable.periodicElements[100].transform.position.y;

        positionToSpawn.x = (xA + xB) / 2.0f;
        positionToSpawn.y = (yA + yB) / 2.0f;
        positionToSpawn.z = -6.0f;
    }
}