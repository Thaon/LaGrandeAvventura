using UnityEngine;
using System.Collections;

public static class RendererExtensions
{
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}

public class POVSwapper : MonoBehaviour {

    #region member variables

    private Renderer m_renderer;
    private enum States { inactive, active, changed };
    private States m_state = States.inactive;

    #endregion

    void Start ()
    {
        m_renderer = GetComponent<Renderer>();
	}
	
	void Update ()
    {
        if (m_renderer.IsVisibleFrom(Camera.main) && (m_state == States.inactive || m_state == States.changed))
        {

        }
        else
        {

        }
    }
}
