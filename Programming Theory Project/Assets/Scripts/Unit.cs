using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected GameObject moveIndicatorPrefab_;
    private GameObject moveIndicator_;
    [SerializeField] protected static GameObject selectionIndicator_;
    [SerializeField] protected float selectionIndicatorHeight_ = 2.5f;

    protected float m_Speed = 5f;
    public float speed 
    {
        get { return m_Speed; }
        set 
        {
            if (value <= 0)
                Debug.LogError("You can't set speed to a negative or zero value!");
            else
                m_Speed = value;
        }
    } // ENCAPSULATION

    protected bool isSelected_ = false;
    protected bool isHovered_ = false;
    protected Vector3 mousePosition;

    protected Camera mainCamera_;
    protected IEnumerator moveCoroutine_;

    [SerializeField] protected LayerMask layerMask_;

    protected virtual void Start()
    {
        mainCamera_ = GameObject.Find("Main Camera").GetComponent<Camera>();
        
        if (selectionIndicator_ == null)
        {
            selectionIndicator_ = GameObject.Find("SelectionIndicator");
        }
    }

    protected void Update()
    {
        if (isSelected_)
        {
            if (Input.GetMouseButtonDown(0) && !isHovered_)
            {
                Deselect();// ABSTRACTION
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (moveCoroutine_ != null)
                {
                    StopCoroutine(moveCoroutine_);
                    Destroy(moveIndicator_);
                    //Debug.Log("Parou corrotina");
                }
                FindMousePosition();// ABSTRACTION
                moveIndicator_ = Instantiate(moveIndicatorPrefab_, mousePosition, moveIndicatorPrefab_.transform.rotation);
                //moveIndicator_.SetActive(true);
                moveCoroutine_ = MoveTo(mousePosition);
                //Debug.Log("MOUSE POS " + mousePosition);
                StartCoroutine(moveCoroutine_);
            }
        }
    }

    protected abstract IEnumerator MoveTo(Vector3 position);

    private void Deselect()
    {
        //Debug.Log("Deselecionado: " + name);
        if(selectionIndicator_.transform.parent == transform)
        {
            selectionIndicator_.SetActive(false);
        }
        isSelected_ = false;
    }

    private void OnMouseDown()
    {
        //Debug.Log("Selecionado: " + name);
        selectionIndicator_.transform.position = new Vector3(transform.position.x, selectionIndicatorHeight_, transform.position.z);
        selectionIndicator_.transform.SetParent(transform, true);
        selectionIndicator_.SetActive(true);
        isSelected_ = true;
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Entrou: " + name);
        isHovered_ = true;
    }

    private void OnMouseExit()
    {
        //Debug.Log("Saiu: " + name);
        isHovered_ = false;
    }

    private void FindMousePosition()
    {
        Ray ray = mainCamera_.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        //Debug.Log("DSASSDA");
        if (Physics.Raycast(ray, out raycastHit, float.MaxValue, layerMask_))
        {
            Debug.Log(raycastHit.point);
            mousePosition = raycastHit.point;
            //return;
        }

        //mousePosition = Vector3.zero;
    }
}
