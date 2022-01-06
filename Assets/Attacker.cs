using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Timers;

public class Attacker : MonoBehaviour
{
    public float hP;

    public float HP
    {
        get { return hP; }
        set
        {
            if (value <= 0)
            {
                value = 0;
                gameObject.SetActive(false);
            }

            hP = value;
        }
    }

    public int guiIndenter;
    public float damage;
    public float defense;
    public float speed;
    public KeyCode left;
    public KeyCode right;
    private bool isAttacking = false;
    private ProgressBar progressBar;
    private List<Element> elements;
    private List<Element> initialElements;
    public ElementHandler elementHandler;
    
    private float accumulatedDamage;

    // Start is called before the first frame update
    void Start()
    {
        elements = elementHandler.elements;
        progressBar = elementHandler.progressBar.GetComponent<ProgressBar>();

        initialElements = new List<Element>();
        for (int i = 0; i < elements.Count; i++)
        {
            initialElements.Add(elements[i].clone());
        }
    }

    // Update is called once per frame
    void Update()
    {
        var translationAmount = this.speed * Time.deltaTime;
        if (Input.GetKey(right))
        {
            isAttacking = true;
            transform.Translate(+translationAmount, 0, 0);
        }
        else if (Input.GetKey(left))
        {
            isAttacking = true;
            transform.Translate(-translationAmount, 0, 0);
        }
        else if (Input.GetKeyUp(right) || Input.GetKeyUp(left))
        {
            isAttacking = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (isAttacking)
        {
            var enemy = other.gameObject.GetComponent<Attacker>();
            var damageTaken = damage - enemy.defense + accumulatedDamage;
            accumulatedDamage = 0;
            enemy.HP -= damageTaken > 0 ? damageTaken : 0;
            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                var elementType = element.element;
                var index = elements.FindIndex(elem => elem.element == elementType);
                
                if (!progressBar.locked)
                {
                    progressBar.Progress += element.factor;
                    accumulatedDamage += elementHandler.handleElement(element);
                    additionalElementHandle(elementType, other.gameObject.GetComponent<Attacker>());
                }
                else
                {
                    elements[index] = initialElements[index].clone();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
    }
    
    void OnGUI()
    {
        var tabs = "";
        for (int i = 0; i < guiIndenter; i++)
        {
            tabs += "\t";
        }
        GUILayout.Button($"{tabs}{tag} Health: {HP}");
        GUILayout.Button($"{tabs}{tag} ProgressBar: {progressBar.Progress}");
        if (progressBar.Progress >= progressBar.cap)
        {
            int fireElementIndex = elementHandler.getElementIndex(Elements.Fire);
            int poisonElementIndex = elementHandler.getElementIndex(Elements.Poison);
            if (fireElementIndex >= 0 && GUILayout.Button($"{tabs}{tag} Fire Damage"))
            {
                var element = elements[fireElementIndex];
                accumulatedDamage += 50 * element.factor;
                progressBar.Progress = 0;
                StartCoroutine("unlock");
            }
        }
    }
    
    IEnumerator unlock()
    {
        yield return new WaitForSeconds(3);
       progressBar.locked = false;
        for (int i = 0; i < initialElements.Count; i++)
        {
            elements[i] = initialElements[i].clone();
        }
    }

    private void additionalElementHandle(Elements elementType, Attacker other)
    {
        switch (elementType)
        {
            case Elements.Poison:
                StartCoroutine("dot", other);
                return;
            default:
                return;
        }
    }

    IEnumerator dot(Attacker other)
    {
        var timer = new Timer(100);
        var poisonElement = elements.Find(element => element.element == Elements.Poison);
        timer.Elapsed += (sender, args) =>
        {
            other.HP -= Mathf.Floor(poisonElement.factor / 100);
            Debug.Log("DOT is taking effect");
        };

        timer.Enabled = true;
        yield return new WaitForSeconds(1);
        Debug.Log("End of DOT Effect");
        timer.Stop();
        timer.Enabled = false;
        timer.Dispose();
    }
}
