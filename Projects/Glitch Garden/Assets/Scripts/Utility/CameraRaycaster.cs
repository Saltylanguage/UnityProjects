using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;
    RaycastHit m_hit;
    Layer m_layerHit;
    public bool validHit = false;
    public Layer[] layerPriorities = {
        Layer.icons,
        Layer.currency,
        Layer.defenders,
        Layer.gameField,
        Layer.raycastEndStop,
    };

    

    public RaycastHit hit
    {
        get { return m_hit; }
    }

    public Layer layerHit
    {
        get { return m_layerHit; }
    }

    void Start() // TODO Awake?
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer, out validHit);
            if (hit.HasValue)
            {
                Debug.Log(layer);
                m_hit = hit.Value;
                m_layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        validHit = false;
        m_hit.distance = distanceToBackground;
        m_layerHit = Layer.raycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layer layer, out bool hasHit)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        RaycastHit hit; // used as an out parameter
        hasHit = Physics.Raycast(viewCamera.ScreenPointToRay(Input.mousePosition), out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
