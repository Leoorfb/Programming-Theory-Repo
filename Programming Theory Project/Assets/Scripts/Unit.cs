using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public float speed = 5f;

    protected bool isSelected_ = false;
    protected bool isHovered_ = false;
    protected Vector3 mousePosition;

    protected Camera mainCamera_;
    protected IEnumerator moveCoroutine_;

    [SerializeField] protected LayerMask layerMask_;

    protected virtual void Start()
    {
        mainCamera_ = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    protected void Update()
    {
        if (isSelected_)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (moveCoroutine_ != null)
                {
                    StopCoroutine(moveCoroutine_);
                    Debug.Log("Parou corrotina");
                }
                FindMousePosition();
                moveCoroutine_ = MoveTo(mousePosition);
                Debug.Log("MOUSE POS " + mousePosition);
                StartCoroutine(moveCoroutine_);
            }
            if (Input.GetMouseButtonDown(0) && !isHovered_)
            {
                Deselect();
            }
        }
    }

    protected abstract IEnumerator MoveTo(Vector3 position);

    private void Deselect()
    {
        Debug.Log("Deselecionado: " + name);
        isSelected_ = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("Selecionado: " + name);
        isSelected_ = true;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Entrou: " + name);
        isHovered_ = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("Saiu: " + name);
        isHovered_ = false;
    }

    private void FindMousePosition()
    {
        Ray ray = mainCamera_.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        Debug.Log("DSASSDA");
        if (Physics.Raycast(ray, out raycastHit, float.MaxValue, layerMask_))
        {
            Debug.Log(raycastHit.point);
            mousePosition = raycastHit.point;
            //return;
        }

        //mousePosition = Vector3.zero;
    }
}
