﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class RecordShow : MonoBehaviour {
    public UnityEngine.UI.Text no1;
    public UnityEngine.UI.Text no2;
    public UnityEngine.UI.Text no3;
    public UnityEngine.UI.Text no4;
    public UnityEngine.UI.Text no5;
    private string str_reco;
    private List<string>[] reco;
    private int datenum;
    private string[] text;

    // Use this for initialization
    void Start()
    {
        reco = new List<string>[5];
        for (int i = 0; i < 5; i++)
        {
            reco[i] = new List<string>(4);
        }
        FileStream file = new FileStream("Record.cvs", FileMode.Open, FileAccess.Read);
        BinaryReader reader = new BinaryReader(file);
        file.Seek(0, SeekOrigin.Begin);
        str_reco = reader.ReadString();

        int len;
        len = str_reco.Length;

        int first = 0;
        int end = len;
        int j = 0;
        int k = 0;
        for (int i = 0; i < len; i++)                                         //ファイル読み取り
        {
            if (str_reco[i] == ',' && str_reco[i - 1] == ',')
            {
                string temp = "";
                end = i - 1;
                for (int n = first; n < end; n++)
                {
                    temp += str_reco[n];
                }
                reco[j][k] = temp;
                j++; k = 0;
                first = i + 1;
            }
            else if (str_reco[i] == ',')
            {
                string temp = "";
                end = i;
                for (int n = first; n < end; n++)
                {
                    temp += str_reco[n];
                }
                reco[j][k] = temp;
                k++;
                first = i + 1;
            }
        }
        datenum = j;
        text = new string[datenum];

        for(int i = 0; i < datenum; i++)
        {
            text[i] = reco[i][0];
            for (k = 1; k < 4; k++)                                  // 順位測定
            {
                int time = 0;
                for (j = 0; j < reco[i][k].Length; j++)
                {
                    int num = reco[i][3][reco[i][k].Length - j] - '0';
                    for (int m = 0; m < j; m++)
                    {
                        num *= 10;
                    }
                    time += num;

                    text[i] += ("     " + (time / 600).ToString() + "'" + (time % 600 / 10).ToString() + "''" + (time % 10).ToString());
                }
               
            }
        }

        no1.text = text[0];
        no2.text = text[1];
        no3.text = text[2];
        no4.text = text[3];
        no5.text = text[4];
    }



    // Update is called once per frame
    void Update () {
        if (Input.GetAxisRaw("Submit") != 0)
        {
            SceneManager.LoadScene("Start");
        }
        
    }
}
