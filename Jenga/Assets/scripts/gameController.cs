using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{


    public Text TextComponent;
    public bool isPlaying = true;
    public string NextLevelName;

    int NumberOfSpecialBlocksAtTheBeginning;
    void Start()
    {
        FixLighting();
        TextComponent.enabled = false;
        NumberOfSpecialBlocksAtTheBeginning = CountBlocks(true);

    }

    public void OnValidate()
    {
        FixLighting();
    }

    void FixLighting()
    {
        RenderSettings.ambientLight = Color.white;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
    }


    void Update()
    {
        
    }

    public int CountBlocks(bool special)
    {
        return FindObjectsOfType<block>().
            Count(block => block.isSpecial == special && block.enabled);
    }

    private void OnTriggerExit(Collider other)
    {
        var block = other.GetComponent<block>();

        if (block == null) return;

        block.enabled = false;
        
        if (block.isSpecial)
            StartCoroutine(EndGameCoroutine(won: false));
        
        else if (CountBlocks(special: false) == 0)
            StartCoroutine(EndGameCoroutine(won: true));

        Destroy(block.gameObject);

    }

    IEnumerator EndGameCoroutine(bool won)
    {
        if (isPlaying == false) yield break;

        TextComponent.enabled = true;
        isPlaying = false;

        if (won)
        {
            for (int i = 5; i > 0; i--)
            {
                TextComponent.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }

            if (NumberOfSpecialBlocksAtTheBeginning != CountBlocks(special: true))
                won = false;

        }


        TextComponent.text = won ? "Wygrałeś" : "Przegrałeś";
        yield return new WaitForSeconds(3f);

        if (string.IsNullOrEmpty(NextLevelName))
        {
            Debug.Log("Koniec gry");
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(NextLevelName);
        }

    }

        
}
