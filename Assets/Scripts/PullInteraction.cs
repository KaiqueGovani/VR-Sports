using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PullInteraction : XRBaseInteractable
{
    public static event Action<float> PullActionReleased;
    public SonsArchery sonsArchery;

    public Transform start, end;
    public GameObject notch;

    public float pullAmount  { get; private set; } = 0.0f;
    
    private LineRenderer _lineRenderer;
    private IXRSelectInteractor pullingInteractor = null;

    private AudioSource _audioSource;

    protected override void Awake ()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }
    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        //Debug.Log("SetPullInteractor");
        pullingInteractor = args.interactorObject;
        sonsArchery.PuxarFlechaSom();
    }
    public void Release()
    {
        //Debug.Log("Release");
        PullActionReleased?.Invoke(pullAmount);
        pullingInteractor = null;
        pullAmount = 0f;
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, 0f);
        UpdateString();

        sonsArchery.SoltarFlechaSom();
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                //Debug.Log("isSelected");
                Vector3 pullPosition = pullingInteractor.transform.position;
                pullAmount = CalculatePull(pullPosition);

                UpdateString();
            }
        }
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0, 1);
    }

    private void UpdateString()
    {
        //Debug.Log(pullAmount);
        Vector3 linePosition = Vector3.forward * Mathf.Lerp(-0.2f, 0, pullAmount);
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, -linePosition.z - 0.17f);
        _lineRenderer.SetPosition(1, linePosition);
    }

}
