using UnityEngine;
using UnityEngine.UI;

public class LoadingWnd : WindowRoot
{
    public Text txtTips;
    public Image imgFG;
    public Image imgPoint;
    public Text txtPrg;

    float fgWidth;

    protected override void InitWnd()
    {
        base.InitWnd();
        fgWidth = (imgFG.transform as RectTransform).sizeDelta.x;

        SetText(txtTips, "这是一条游戏Tips");
        SetText(txtPrg, "0%");
        imgFG.fillAmount = 0;
        imgPoint.transform.localPosition = new Vector3(-fgWidth/2, 0, 0);
    }

    public void SetProgress(float prg)
    {
        SetText(txtPrg, (int)(prg * 100) + "%");
        imgFG.fillAmount = prg;

        float posX = prg * fgWidth - fgWidth / 2;
        (imgPoint.transform as RectTransform).anchoredPosition = new Vector2(posX, 0);
    }
}
