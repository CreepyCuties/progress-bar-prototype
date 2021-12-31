using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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
    private ElementHandler handler;
    public ElementHandler elementHandler;

    // Start is called before the first frame update
    void Start()
    {
        handler = this.elementHandler.GetComponent<ElementHandler>();
        elements = handler.elements;
        progressBar = handler.progressBar.GetComponent<ProgressBar>();

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
        var enemy = other.gameObject.GetComponent<Attacker>();
        if (isAttacking)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                enemy.HP -= element.amount;
                var index = elements.FindIndex(elem => elem.element == element.element);
                
                if (!progressBar.locked)
                {
                    progressBar.Progress += element.amount;
                    elements[index] = handler.handleElement(element.clone());
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
            if (GUILayout.Button($"{tabs}{tag} Fire Damage"))
            {
                var index = elements.FindIndex(elem => elem.element == Elements.Fire);
                var element = elements[index];
                element.amount = 50 * element.baseAmount;
                elements[index] = element;
                progressBar.Progress = 0;
                StartCoroutine("unlock");
            }
        }
    }
    
    IEnumerator unlock()
    {
        yield return new WaitForSeconds(3);
        this.progressBar.locked = false;
        for (int i = 0; i < initialElements.Count; i++)
        {
            elements[i] = initialElements[i].clone();
        }
    }
}
