using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

class Sector
{
    public int ID;
    public float Left;
    public float Right;

    public Sector(int id, float left, float right)
    {
        ID = id;
        Left = left;
        Right = right;
    }
    public Sector(int id, RectTransform transform)
    {
        ID = id;
        Left = transform.anchoredPosition.x - transform.rect.width/2;
        Right = transform.anchoredPosition.x + transform.rect.width / 2;
    }
}

public class MinigameStage : MonoBehaviour
{
    [SerializeField] private Sprite goodSector;
    [SerializeField] private Sprite badSector;
    [SerializeField] private Image[] sectors;

    [Range(0f, 1f)] [SerializeField] private float stageDifficulty;
    private int _stageBadNum;
    private int _stageGoodNum;
    private List<Image> _sectorsList;
    private List<Sector> _badSmallSectors;
    private List<Sector> _badBigSectors;

    private void Awake()
    {
        _sectorsList = new List<Image>(sectors);
        _badSmallSectors = new List<Sector>();
        _badBigSectors = new List<Sector>();
        _stageBadNum = (int) (sectors.Length * stageDifficulty);
        _stageGoodNum = (int) (sectors.Length * (1 - stageDifficulty));
    }
    //Надо всё поменять на хорошие сектора
    //иначе вообще плохо
    private void Start()
    {
        if (_stageBadNum == 0) return;
        List<int> randomNums = new List<int>();
        for (int i = 0; i < _sectorsList.Count; i++)
        {
            randomNums.Add(i);
        }

        for (int i = 0; i < _sectorsList.Count - _stageBadNum; i++)
        {
            randomNums.RemoveAt(Random.Range(0, randomNums.Count));
        }

        int index;
        foreach (var t in randomNums)
        {
            index = t;
            _sectorsList[index].sprite = badSector;
            _badSmallSectors.Add(new Sector(index, (_sectorsList[index].transform as RectTransform)));
            //_sectorsList.RemoveAt(index);
        }

        int previousBad = _badSmallSectors[0].ID;
        _badBigSectors.Add(_badSmallSectors[0]);
        for (int i = 1; i < _badSmallSectors.Count; i++)
        {
            if (previousBad == _badSmallSectors[i].ID - 1)
            {
                _badBigSectors[_badBigSectors.Count - 1].Right = _badSmallSectors[i].Right;
            }
            else
            {
                _badBigSectors.Add(_badSmallSectors[i]);
            }

            previousBad = _badSmallSectors[i].ID;

        }
    }

    public bool Check(float position)
    {
        foreach (var sector in _badBigSectors)
        {
            if (position > sector.Left && position < sector.Right)
            {
                return false;
            }
        }

        return true;
    }
}
