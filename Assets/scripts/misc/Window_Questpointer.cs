using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Window_Questpointer : MonoBehaviour
{
    private Vector3 targetpos;
    private RectTransform pointer;
    private Image pointerImage;

    [SerializeField] private Camera uiCamera;
    [SerializeField] private Sprite arrow;
    [SerializeField] private Sprite cross;

    private void Awake()
    {
        pointer = transform.Find("pointer").GetComponent<RectTransform>();
        pointerImage = transform.Find("pointer").GetComponent<Image>();

        hide();
    }

    private void Update()
    {
        float bordersize = 100f;
        Vector3 targetPosScreenPoint = Camera.main.WorldToScreenPoint(targetpos);
        bool isOffScreen = targetPosScreenPoint.x <= bordersize || targetPosScreenPoint.x >= Screen.width - bordersize || targetPosScreenPoint.y <= bordersize || targetPosScreenPoint.y >= Screen.height - bordersize;
        Debug.Log(isOffScreen + " " + targetPosScreenPoint);

        if (isOffScreen)
        {
            RotatePointerTagretPosition();
            pointerImage.sprite = arrow;
            Vector3 cappedTargetSrcPos = targetPosScreenPoint;
            if (cappedTargetSrcPos.x <= bordersize)
            {
                cappedTargetSrcPos.x = bordersize;
            }
            if (cappedTargetSrcPos.x >= Screen.width - bordersize)
            {
                cappedTargetSrcPos.x = Screen.width - bordersize;
            }
            if (cappedTargetSrcPos.y <= bordersize)
            {
                cappedTargetSrcPos.y = bordersize;
            }
            if (cappedTargetSrcPos.y >= Screen.height - bordersize)
            {
                cappedTargetSrcPos.y = Screen.height - bordersize;
            }
            Vector3 pointerWorldPos = uiCamera.ScreenToWorldPoint(cappedTargetSrcPos);
            pointer.position = pointerWorldPos;
            pointer.localPosition = new Vector3(pointer.localPosition.x, pointer.localPosition.y, 0f);
        }
        else
        {
            pointerImage.sprite = cross;
            Vector3 pointerWorldPos = uiCamera.ScreenToWorldPoint(targetPosScreenPoint);
            pointer.position = pointerWorldPos;
            pointer.localPosition = new Vector3(pointer.localPosition.x, pointer.localPosition.y, 0f);
            pointer.localEulerAngles = Vector3.zero;
        }
    }

    private void RotatePointerTagretPosition()
    {
        Vector3 toPos = targetpos;
        Vector3 fromPos = Camera.main.transform.position;
        fromPos.z = 0f;
        Vector3 dir = (toPos - fromPos).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointer.localEulerAngles = new Vector3(0, 0, angle);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void show(Vector3 targetpos)
    {
        gameObject.SetActive(true);
        this.targetpos = targetpos;
    }
}