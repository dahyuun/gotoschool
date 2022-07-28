using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    private int RandomPattern = 0;
    private int min = 0;
    private int max = 0;

    private List<GameObject> patternList = new List<GameObject>();
    public GameObject StartPattern;
    public GameObject pattern1;
    public GameObject pattern2;
    public GameObject pattern3;
    public GameObject pattern4;
    public GameObject pattern5;
    public GameObject pattern6;
    public GameObject pattern7;
    public GameObject pattern8;
    public GameObject pattern9;
    public GameObject pattern10;

    public float Speed = 0.0f;
    public float delayTime = 0.0f;
    public float posX = 0.0f;
    
    private void Start()
    {
        patternList.Add(pattern1);
        patternList.Add(pattern2);
        patternList.Add(pattern3);
        patternList.Add(pattern4);
        patternList.Add(pattern5);
        patternList.Add(pattern6);
        patternList.Add(pattern7);
        patternList.Add(pattern8);
        patternList.Add(pattern9);
        patternList.Add(pattern10);
        StartCoroutine("CreatePattern");
    }

    private void Update()
    {
        switch (GameManager.instance.stage)
        {
            case 1:
                min = 0;
                max = 3; break;
            case 2:
                min = 3;
                max = 6; break;
            case 3:
                min = 6;
                max = patternList.Count; break;
        }
    }

    IEnumerator CreatePattern()
    {
        GameObject s_pattern = Instantiate(StartPattern, new Vector2(0.0f, 0.0f), Quaternion.identity);
        s_pattern.GetComponent<Pattern>().Init(Speed);

        while (!GameManager.instance.gameOver)
        {
            RandomPattern = Random.Range(min, max);
            
            GameObject t_pattern = Instantiate(patternList[RandomPattern], new Vector2(posX, 0.0f), Quaternion.identity);
            t_pattern.GetComponent<Pattern>().Init(Speed);
            yield return new WaitForSeconds(delayTime);
        }
    }

}
