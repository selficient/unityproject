using System.Collections;
using System.Collections.Generic;
using Task;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPopout : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] public Transform m_Transform;         // Used to control the movement whatever needs to pop out.
    [SerializeField] public float m_PopSpeed = 8f;         // The speed at which the item should pop out.
    [SerializeField] public float m_PopDistance = 0.5f;    // The distance the item should pop out.
    [SerializeField] private Camera CurrentCamera;

    private Vector3 m_StartPosition;                        // The position aimed for when the item should not be popped out.
    private Vector3 m_PoppedPosition;                       // The position aimed for when the item should be popped out.
    private Vector3 m_TargetPosition;                       // The current position being aimed for.

    private bool IsOver = false;



    public void OnPointerClick(PointerEventData eventData)
    {
        EventManager.TriggerEvent("startSceneRendering", null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsOver = false;
    }

    private void Start()
    {
        // Store the original position as the one that is not popped out.
        m_StartPosition = m_Transform.position;

        // Calculate the position the item should be when it's popped out.
        m_PoppedPosition = m_Transform.position - m_Transform.forward * m_PopDistance;
    }


    private void Update()
    {
        // Set the target position based on whether the item is being looked at or not.
        m_TargetPosition = IsOver ? m_PoppedPosition : m_StartPosition;

        // Move towards the target position.
       m_Transform.position = Vector3.MoveTowards(m_Transform.position, m_TargetPosition, m_PopSpeed * Time.deltaTime);
    }
    
}
