using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class CountPoint : MonoBehaviour
{
    private bool GameMode;
    private bool Manual;
    private float countStartIn = 3.0f;
    public float countPlaying = 10.0f;
    private float countTimeUp = 2.5f;
    private float countWaitingForResult = 2.5f;
    private float cS;
    private float cP;
    private float cT;
    private float cW;
    private int countPoint;
    private int result;
    private string phase;

    private int centorX;
    private int centorY;

    GUIStyle style = new GUIStyle();

    void Start()
    {
        GameMode = false;
        Manual = false;

        cS = countStartIn;
        cP = countPlaying;
        cT = countTimeUp;
        cW = countWaitingForResult;

        countPoint = 0;
        result = 0;

        phase = "StartIn";

        style.fontSize = 256;
        style.normal.textColor = Color.green;

        centorX = Screen.width / 2;
        centorY = Screen.height / 2;

    }

    void OnGUI()
    {
        if (GameMode)
        {
            style.fontSize = 256;
            if (phase == "StartIn")
            {
                GUI.Label(new Rect(centorX - 100, centorY - 150, Screen.width, Screen.height), $"{countStartIn.ToString("f0")}", style);
            }
            else if (phase == "Playing")
            {
                GUI.Label(new Rect(30, 0, Screen.width, Screen.height), $"Score : {countPoint}", style);
                GUI.Label(new Rect(Screen.width - 1200, 0, Screen.width, Screen.height), $"Time : {countPlaying.ToString("f1")}", style);
            }
            else if (phase == "TimeUp")
            {
                GUI.Label(new Rect(centorX - 600, centorY - 150, Screen.width, Screen.height), $"TIME UP!", style);
            }
            else if (phase == "WaitingForResult")
            {
                GUI.Label(new Rect(centorX - 1000, centorY - 150, Screen.width, Screen.height), $"あなたの結果は...", style);
            }
            else if (phase == "SeeingResult")
            {
                if (result < 5000)
                {
                    GUI.Label(new Rect(centorX - 800, centorY - 450, Screen.width, Screen.height), $"ざんねん！笑", style);
                    GUI.Label(new Rect(centorX - 500, centorY - 100, Screen.width, Screen.height), $"{result} pts", style);
                    GUI.Label(new Rect(centorX - 1200, centorY + 250, Screen.width, Screen.height), $"Thank you for playing!", style);
                }
                else if (result < 10000)
                {
                    GUI.Label(new Rect(centorX - 900, centorY - 450, Screen.width, Screen.height), $"もうすこし！笑", style);
                    GUI.Label(new Rect(centorX - 500, centorY - 100, Screen.width, Screen.height), $"{result} pts", style);
                    GUI.Label(new Rect(centorX - 1200, centorY + 250, Screen.width, Screen.height), $"Thank you for playing!", style);
                }
                else if (result < 15000)
                {
                    GUI.Label(new Rect(centorX - 1100, centorY - 450, Screen.width, Screen.height), $"がんばったほう！笑", style);
                    GUI.Label(new Rect(centorX - 600, centorY - 100, Screen.width, Screen.height), $"{result} pts", style);
                    GUI.Label(new Rect(centorX - 1200, centorY + 250, Screen.width, Screen.height), $"Thank you for playing!", style);
                }
                else
                {
                    GUI.Label(new Rect(centorX - 700, centorY - 450, Screen.width, Screen.height), $"すごい！笑", style);
                    GUI.Label(new Rect(centorX - 600, centorY - 100, Screen.width, Screen.height), $"{result} pts", style);
                    GUI.Label(new Rect(centorX - 1200, centorY + 250, Screen.width, Screen.height), $"Thank you for playing!", style);
                }
            }
        }

        if (Manual)
        {
            style.fontSize = 96;
            GUI.Label(new Rect(100, 300, Screen.width, Screen.height), $"ZR : ローラーをふる\nZR+Lスティック : ローラーコロコロ\nB : ジャンプ\n↑ : エモート\n+ : マニュアル表示切り替え\n- : ゲームモード切り替え\nHomeボタン : 再起動", style);
        }
    }

    public void CountUp()
	{
        if (GameMode && phase == "Playing")
        {
            countPoint += 500;
        }
	}

    public void SwitchGameMode()
    {
        GameMode = !GameMode;
    }

    public void SwitchManual()
    {
        Manual = !Manual;
    }

    void Update()
	{
        if (GameMode)
        {

            if (phase == "StartIn")
            {
                countStartIn -= Time.deltaTime;
            }
            else if (phase == "Playing")
            {
                countPlaying -= Time.deltaTime;
            }
            else if (phase == "TimeUp")
            {
                countTimeUp -= Time.deltaTime;
            }
            else if (phase == "WaitingForResult")
            {
                countWaitingForResult -= Time.deltaTime;
            }

            if (countStartIn <= 0f)
            {
                phase = "Playing";
            }

            if (countPlaying <= 0f)
            {
                phase = "TimeUp";
                result = countPoint;
            }

            if (countTimeUp <= 0f)
            {
                phase = "WaitingForResult";
            }

            if (countWaitingForResult <= 0f)
            {
                phase = "SeeingResult";
            }
        }
        else
        {
			phase = "StartIn";
            countStartIn = cS;
            countPlaying = cP;
            countTimeUp = cT;
            countWaitingForResult = cW;
            countPoint = 0;
        }
    }
}
