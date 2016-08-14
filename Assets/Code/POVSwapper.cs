using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class RendererExtensions
{
    public static bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}

public class POVSwapper : MonoBehaviour {

    #region member variables

    public int m_roundsToActivate = 0; //times you have to look at an object before it activates
    public bool m_includeSelf = false;
    public enum ExpireOptions { Loop, Destroy };
    public ExpireOptions m_expiringOption = ExpireOptions.Loop;

    private bool m_hasBeenLooked = false;
    private enum States { inactive, active, changed };
    private States m_state = States.inactive;
    private int m_index = 0; //starting object
    private Transform[] m_objects;

    #endregion

    void Start ()
    {
        List<Transform> tempTransforms = new List<Transform>();
        foreach (Transform temp in this.transform.GetComponentsInChildren<Transform>(true))
        {
            if (m_includeSelf)
            {
                tempTransforms.Add(temp);
            }
            else if (temp != this.transform)
            {
                tempTransforms.Add(temp);
            }
        }
        m_objects = tempTransforms.ToArray();
	}
	
	void Update ()
    {
        if (RendererExtensions.IsVisibleFrom(m_objects[m_index].gameObject.GetComponent<Renderer>(), Camera.main)) //check if the currently active object is visible
        {
            //update the fact that the object has been looked at
            m_hasBeenLooked = true;
            
            //reset state if active
            if (m_state == States.changed)
                m_state = States.active;
        }
        else
        {
            //activate object the first time
            if (m_state == States.inactive && m_hasBeenLooked)
            {
                if (m_roundsToActivate > 0)
                {
                    m_roundsToActivate--;
                }
                else
                {
                    m_state = States.active;
                }
            }

            m_hasBeenLooked = false;

            //if it's active already, change GameObject
            if (m_state == States.active)
            {
                if (m_index < m_objects.Length - 1)
                {
                    m_objects[m_index].gameObject.SetActive(false);
                    m_index++;
                    //print("changing");
                    m_objects[m_index].gameObject.SetActive(true);
                }
                else
                {
                    if (m_expiringOption == ExpireOptions.Loop)
                    {
                        m_objects[m_index].gameObject.SetActive(false);
                        m_index = 0;
                        //print("looping");
                        m_objects[m_index].gameObject.SetActive(true);
                    }
                    else if (m_expiringOption == ExpireOptions.Destroy)
                    {
                        this.gameObject.SetActive(false);
                    }
                }
                m_state = States.changed;
            }
        }
    }
}
