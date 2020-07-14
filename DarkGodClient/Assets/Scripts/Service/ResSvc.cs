using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 资源加载服务
/// </summary>
public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance = null;

    public void InitSvc()
    {
        Instance = this;
        InitRDNameCfg();

        PECommon.Log("Init ResSvc...");
    }

    public void AsyncLoadScene(string sceneName, Action loaded)
    {
        StartCoroutine(IEAsyncLoadScene(sceneName, loaded));
    }

    IEnumerator IEAsyncLoadScene(string name, Action loaded)
    {
        GameRoot.Instance.loadingWnd.SetWndState(true);
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            GameRoot.Instance.loadingWnd.SetProgress(ao.progress);
            if (ao.progress > .85f)
            {
                GameRoot.Instance.loadingWnd.SetProgress(1);
                ao.allowSceneActivation = true;     // 这里可以设置让玩家点击某个按键之后再设置为true
            }
            yield return ao.progress;
        }
        loaded?.Invoke();
        GameRoot.Instance.loadingWnd.SetWndState(false);
    }

    private Dictionary<string, AudioClip> adDic = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path, bool cache = false)
    {
        AudioClip au = null;
        if (!adDic.TryGetValue(path, out au))
        {
            au = Resources.Load<AudioClip>(path);
            if (cache)
                adDic.Add(path, au);
        }
        return au;
    }

    #region InitCfgs

    private List<string> surnameLst = new List<string>();
    private List<string> manLst = new List<string>();
    private List<string> womanLst = new List<string>();

    private void InitRDNameCfg()
    {
        TextAsset xml = Resources.Load<TextAsset>(PathDefine.RDNameCfg);
        if (!xml)
        {
            PECommon.Log("xml file:" + PathDefine.RDNameCfg + " not exist", LogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);

            XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;

            for (int i = 0; i < nodLst.Count; i++)
            {
                XmlElement ele = nodLst[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null) continue;
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "surname":
                            surnameLst.Add(e.InnerText);
                            break;
                        case "man":
                            manLst.Add(e.InnerText);
                            break;
                        case "woman":
                            womanLst.Add(e.InnerText);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public string GetRDNameData(bool man = true)
    {
        System.Random rd = new System.Random();
        string rdName = surnameLst[PETools.RDInt(0, surnameLst.Count - 1)];
        if (man)
            rdName += manLst[PETools.RDInt(0, manLst.Count - 1)];
        else
            rdName += womanLst[PETools.RDInt(0, manLst.Count - 1)];
        return rdName;
    }

    #endregion

}
