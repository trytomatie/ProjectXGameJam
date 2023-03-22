using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeFloor : MonoBehaviour
{
    public List<Transform> fakeFloors;
    public TextMeshPro floorText;
    public float fakeFloorOffset;

    private Dictionary<Transform, Vector3> basePositions = new Dictionary<Transform, Vector3>();
    // Update is called once per frame

    private void Start()
    {
        foreach (Transform fakeFloor in fakeFloors)
        {
            basePositions.Add(fakeFloor, fakeFloor.position);
        }
    }
    void Update()
    {
        foreach(Transform fakeFloor in fakeFloors)
        {
            fakeFloor.position = basePositions[fakeFloor] + new Vector3(0, fakeFloorOffset %8, 0);
        }
        floorText.text = "U " + (4 + Mathf.RoundToInt(fakeFloorOffset / 8));
    }

    public void ChangeScreen(AnimationEvent ae)
    {
        SceneManager.LoadScene(ae.intParameter);
    }
}
