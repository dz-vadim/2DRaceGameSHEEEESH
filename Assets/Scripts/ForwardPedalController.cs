using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ForwardPedalController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    Sprite pedalDown, pedalUp;

    [SerializeField]
    GameObject car;

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = pedalDown;
        car.GetComponent<CarController>().moveForward = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = pedalUp;
        car.GetComponent<CarController>().moveForward = false;
    }
}
