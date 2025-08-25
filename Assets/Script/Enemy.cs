using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Enemys enemys;
    private Animator animator;
    private SpriteAnchor anchor;
    void Start()
    {
        animator = GetComponent<Animator>();
        anchor = GetComponent<SpriteAnchor>();
        EnemyChanger();
    }
   public void EnemyChanger()
    {
        string str = enemys.ToString();
        anchor.sr.color = new(255, 255, 255);
        anchor.useAutoFit = false;
        RuntimeAnimatorController run = Resources.Load<RuntimeAnimatorController>(str);
        animator.runtimeAnimatorController = run;
        float yPos = default;
        float xScale = default;
       float yScale = default;
        float yRot = default;
        switch (str)
        {
            case "Elf":
                print(str);
                yPos = 2.29f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                break;
            case "FireKnight":
                yPos = 2.29f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                break;
            case "Goblin":
                yPos = 0;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                break;
            case "Golem":
                yPos = 0.32f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                break;
            case "Hero":
                yPos = 0;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                anchor.sr.color = new(255,100,100);
                break;
            case "Knight":
                yPos = 0;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                break;
            case "Last":
                yPos = 1.31f;
                yRot = 180;
                xScale = 0.65f;
                yScale = 0.65f;
                break;
            case "Mashroom":
                yPos = -0.07f;
                yRot = 0;
                xScale = 6.5f;
                yScale = 5f;
                break;
            case "Necr":
                yPos = 0.48f;
                yRot = 0;
                xScale = 3;
                yScale = 3;
                break;
            case "Prince":
                yPos = -0.15f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                break;
            case "POP":
                yPos = 2.25f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                break;
            case "Samurai":
                yPos = -0.09f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                break;
            case "Skelet":
                yPos = -0.12f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                break;
            case "Stand":
                yPos = 0.17f;
                yRot = 0;
                xScale = 5;
                yScale = 5;
                break;
            case "Valkyrie":
                yPos = 0.13f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                break;
        }
        transform.position = new(transform.position.x, yPos, transform.position.z);
        transform.localScale = new(xScale, yScale, 1);
        transform.eulerAngles = new(0, yRot, 0);
        StartCoroutine(Next());
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(0.1f);
        anchor.useAutoFit = true;
    }
}
public enum Enemys
{
    Elf,
    FireKnight,
    Goblin,
    Golem,
    Hero,
    Knight,
    Last,
    Mashroom,
    Necr,
    Prince,
    POP,
    Samurai,
    Skelet,
    Stand,
    Valkyrie
}