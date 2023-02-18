using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoomWhackerBattles;

public class NoteBlock: MonoBehaviour
{
    public GameObject go; // this will be the name of the game object
    public int parentIndex; //eg. "nb0" -> that is the name of the GameObject underneath NoteblockHolder
    public int spawnIndex; //eg. 0 -> that is the left mask spawn index
    public string note;

    public NoteBlock(int ParentIndex, bool fromIsa)
    {
        this.parentIndex = ParentIndex;
        this.spawnIndex = 110 * ParentIndex; // 110 cus there are 110.0f units between each NB core. if ParentIndex = 0 spawnindex will also be zero. 

        if (fromIsa)
        {
            GameObject nbholder = GameObject.Find("NotenBovenIsaHolder");
            go = nbholder.transform.GetChild(parentIndex).gameObject;
        }
        else
        {
            //foo
        }
    }

    public void setNote(Sprite note)
    {
        Transform noteSpriteHolder = go.transform.GetChild(0);
        SpriteRenderer sr = noteSpriteHolder.GetComponent<SpriteRenderer>();

        sr.sprite = note;
    }
    public void setSlider(Sprite slider)
    {
        Transform noteSpriteHolder = go.transform.GetChild(0);
        SpriteRenderer sr = noteSpriteHolder.GetComponent<SpriteRenderer>();

        sr.sprite = slider;
    }

    public void advancePosition()
    {
        if (spawnIndex < SavedPossibleSpawns.possibleSpawns.Length - 1)
        {
            spawnIndex++;
        }
        else
        {
            spawnIndex = 0;
        }

        float xCoord = SavedPossibleSpawns.possibleSpawns[spawnIndex];
        float yCoord = go.transform.position.y;
        float zCoord = go.transform.position.z;

        go.transform.position = new Vector3(xCoord, yCoord, zCoord);
    }

    public float getCurrentXCoord()
    {
        float xCoord = SavedPossibleSpawns.possibleSpawns[spawnIndex];
        return xCoord;
    }

}
