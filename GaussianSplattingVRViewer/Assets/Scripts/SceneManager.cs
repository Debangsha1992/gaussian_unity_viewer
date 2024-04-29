using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public InputActionReference CarousalMenuActivate;
    public GameObject CarousalView;
    public GameObject ContentParent;
    public GameObject SplatUIObjectPrefab;
    public CarouselViewManager carousel;

    private void Start()
    {
        CarousalMenuActivate.action.performed += CarousalMenuActivate_performed;
    }

    private void Update()
    {
        
    }

    private void CarousalMenuActivate_performed(InputAction.CallbackContext obj)
    {
        if(CarousalView.activeInHierarchy)
        {
            CarousalView.SetActive(false);
        }
        else
        {
            CarousalView.SetActive(true);
        }
    }

    public void CallBack_DataLoadedFromJSON()
    {
        
       // ContentParent.GetComponent<CarouselViewManager>().items
            var Splats = SplatDataManager.Instance.Dat.SplatObjects;

        foreach (SplatObject s in Splats)
        {
            GameObject So = Instantiate(SplatUIObjectPrefab);

            So.gameObject.GetComponent<SplatUIElement>().PopulateUI(s.SplatFileName,s.UID);

            So.transform.SetParent(ContentParent.transform);

            So.transform.localPosition = new Vector3(0,0,0);
            So.transform.localScale = new (1,1,1);

            carousel.items.Add(So.GetComponent<RectTransform>());
        }
        //ContentParent.gameObject.SetActive(true);

        ContentParent.gameObject.GetComponent<CarouselViewManager>().CanScroll = true;

        Debug.Log("can Scroll");
    }
}
