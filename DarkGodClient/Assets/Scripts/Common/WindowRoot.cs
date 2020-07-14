using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI界面基类
/// </summary>
public class WindowRoot : MonoBehaviour
{
    protected ResSvc resSvc = null;
    protected AudioSvc audioSvc = null;
    protected NetSvc netSvc = null;

    public void SetWndState(bool isActive)
    {
        if (gameObject.activeSelf != isActive)
        {
            SetActive(gameObject, isActive);
        }
        if (isActive)
        {
            InitWnd();
        }
        else
        {
            ClearWnd();
        }
    }

    protected virtual void InitWnd()
    {
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
        netSvc = NetSvc.Instance;
    }

    protected virtual void ClearWnd()
    {
        resSvc = null;
        audioSvc = null;
        netSvc = null;
    }


    #region Tool Functions

    protected void SetActive(GameObject go, bool isActive) { go.SetActive(isActive); }
    protected void SetActive(Transform trans, bool state = true) { trans.gameObject.SetActive(state); }
    protected void SetActive(RectTransform rectTrans, bool state = true) { rectTrans.gameObject.SetActive(state); }
    protected void SetActive(Image img, bool state = true) { img.transform.gameObject.SetActive(state); }
    protected void SetActive(Text txt, bool state = true) { txt.transform.gameObject.SetActive(state); }
    
    protected void SetText(Text txt, string context) { txt.text = context; }
    protected void SetText(Transform trans, int num) { SetText(trans.GetComponent<Text>(), num); }
    protected void SetText(Transform trans, string context) { SetText(trans.GetComponent<Text>(), context); }
    protected void SetText(Text txt, int num) { SetText(txt, num.ToString()); }
    
    #endregion
}
