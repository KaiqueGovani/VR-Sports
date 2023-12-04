using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;
    public GameObject notch;

    public ArcheryManager manager;
    public SonsArchery sonsArchery;
    public GameObject parabens;

    [SerializeField]
    private XRGrabInteractable _bow;
    [SerializeField]
    private bool _arrowNotched = false;
    [SerializeField]
    private GameObject _currentArrow = null;
    

    void Start()
    {
        _bow = GetComponent<XRGrabInteractable>();
        PullInteraction.PullActionReleased += NotchEmpty;
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -=NotchEmpty;
    }

    void Update()
    {
        if (_bow.isSelected && _arrowNotched == false)
        {
            //Debug.Log("Should spawn arrow");
            if (manager.flechas <= 0)
            {
                gameObject.SetActive(false);
                parabens.SetActive(true);
                sonsArchery.FelizSom();
            }
            manager.flechas -= 1;
            _arrowNotched = true;
            StartCoroutine("DelayedSpawn");
        }
        if (!_bow.isSelected && _currentArrow != null)
        {
            //Debug.Log("Should destroy arrow");
            Destroy(_currentArrow);
            NotchEmpty(1f);
        }
    }

    private void NotchEmpty(float value)
    {
        //Debug.Log("NotchEmpty");
        _arrowNotched = false;
        _currentArrow = null;
    }

    IEnumerator DelayedSpawn()
    {
        //Debug.Log("DelayedSpawn");
        yield return new WaitForSeconds(1f);
        _currentArrow = Instantiate(arrow, notch.transform);
    }

}
