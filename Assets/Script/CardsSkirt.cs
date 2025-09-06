using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsSkirt : MonoBehaviour
{
    public Skirts skirts;
    public Animator anim;
    public SpriteRenderer sr;
    bool active;
    public Enemy enemy;
    public BattleChecker bc;
    public You you;
    public Rules rules;
    private void Start()
    {
        enemy.onKill += Refresh;
        active = true;
        switch (PlayerPrefs.GetInt("VFX"))
        {
            case 0:
                skirts = Skirts.blue;
                break;
            case 1:
                skirts = Skirts.green;
                break;
            case 2:
                skirts = Skirts.red;
                break;
            case 3:
                skirts = Skirts.yellow;
                break;

        }
        switch (skirts)
        {
            case Skirts.blue:
                sr.sprite = Resources.Load<Sprite>("blue");
                break;
            case Skirts.yellow:
                sr.sprite = Resources.Load<Sprite>("yellow");
                break;
            case Skirts.green:
                sr.sprite = Resources.Load<Sprite>("green");
                break;
            case Skirts.red:
                sr.sprite = Resources.Load<Sprite>("red");
                break;
        }
    }
    public void Play(int damage)
    {
        if (!active) return;
        GetComponent<SpriteRenderer>().color=new(1,1,1,1);
        switch (skirts)
        {
            case Skirts.blue:
                anim.Play("ice");
                enemy.freeze = true;
                break;
            case Skirts.yellow:
                anim.Play("money");
                rules.coins += 25;
                break;
            case Skirts.green:
                anim.Play("health");
                you.TakeHeal(50);
                break;
            case Skirts.red:
                anim.Play("blood");
                bc.vfxDamage = damage;
                break;
        }
        StartCoroutine(N());
        active = false;
    }
    IEnumerator N()
    {
        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().color = new(1, 1, 1, 0);
    }
    public void Refresh()
    {
        active = true;
    }
}
public enum Skirts
{
    blue,yellow,green,red
}