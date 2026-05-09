using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static NetworkDevice selected;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray =
                Camera.main.ScreenPointToRay(
                    Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                NetworkDevice nd =
                    hit.collider.GetComponent<
                        NetworkDevice>();

                if(nd != null)
                {
                    if(selected != null)
                        selected.Deselect();

                    selected = nd;

                    selected.Select();
                }
            }
        }
    }
}