using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class loadti : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        
        WebClient cli = new WebClient();
        ttis.gets(cli.DownloadString("http://kinglista1.cn3v.net/getti.asp"));

        ttis.rands = getrandom(ttis.mtis.Count);

        createti();
        //Debug.Log();
        Debug.Log(ttis.mtis.Count.ToString());
    }
	public int[] getrandom(int c)
    {
        int[] re = new int[c];
        for (int i = 0; i < re.Length; i++)
        {
            re[i] = i;
        }
        for (int i = 0; i < re.Length; i++)
        {
            for (int j = 0; j < re.Length-1; j++)
            {
                
                if(UnityEngine.Random.Range(0,10)>5)
                {
                    int temp = re[j];
                    re[j] = re[j + 1];
                    re[j + 1] = temp;
                }
                

            }
        }
        return re;
    }

    public void createti()
    {
        int c = ttis.rands[ttis.currentti];
        GameObject.Find("mtext").GetComponent<UnityEngine.UI.Text>().text = ttis.mtis[ttis.rands[ttis.currentti]].question;

        for (int i = 0; i < ttis.mtis[c].options.Length; i++)

        {

            Button buttonPrefab = UnityEngine.Resources.Load<Button>("Prefabs/Button");

            Button obj = (Button)UnityEngine.Object.Instantiate(buttonPrefab);
            GameObject mUICanvas = GameObject.Find("Canvas");
            obj.transform.parent = mUICanvas.transform;
            obj.GetComponentInChildren<Text>().text = ttis.mtis[c].options[i];

            obj.transform.Translate(new Vector3(mUICanvas.transform.localScale.x + 100 + (float)((i % 5) * (75)), mUICanvas.transform.localScale.y + 200 - ((int)(i / 5) * 50)), 0); //按钮显示的位置
            obj.onClick.AddListener(delegate () {
                this.mbuttonsclick(obj);
            });

        }
        if(ttis.currentti<(ttis.mtis.Count-1))
        {
            ttis.currentti++;
        }
    }
    public void mbuttonsclick(Button game)
    {
        int c = ttis.rands[ttis.currentti-1];

        if (game.GetComponentInChildren<Text>().text==ttis.mtis[c].answer)
        {
            Debug.Log("成功！");
            createti();
            ttis.score += 1;
            settext("score", "分数:"+ttis.score.ToString());
        }
        else
        {
            Debug.Log("失败！");
        }
        Debug.Log("click");
    }
    public void settext(string name,string txt)
    {
        GameObject.Find(name).GetComponent<UnityEngine.UI.Text>().text = txt;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
public class tti
{
    public int id;
    public string question;
    public string answer;
    public string[] options;
    public tti(string src)
    {
        string[] sArray = Regex.Split(src, "@", RegexOptions.IgnoreCase);
        ///Debug.Log(sArray[0]);
        id = Convert.ToInt32(sArray[0]);
        question = sArray[1];
        answer = sArray[2];
        options = Regex.Split(sArray[3], ",", RegexOptions.IgnoreCase);
    }
}
public static class ttis
{
    public static int[] rands;
    public static List<tti> mtis = new List<tti>();
    public static List<Button> mbuttons = new List<Button>();
    public static int currentti = 0;
    public static int score = 0;
    public static void clearbuttons()
    {
        foreach (var item in mbuttons)
        {
            GameObject.Destroy(item);
        }
        mbuttons.Clear();
    }
    public static void gets(string src)
    {
        string[] sArray = Regex.Split(src, "~", RegexOptions.IgnoreCase);
        foreach (var item in sArray)
        {
            if (item != "")
                mtis.Add(new tti(item));
        }
    }
}