using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public float margin;

    private List<GameObject> elements;

    private Rect screen;

    void Start()
    {
        elements = new List<GameObject>();
        GetElements();
    }

    private void FixedUpdate()
    {
        PartitionElements();
    }

    private void GetElements()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            elements.Add(transform.GetChild(i).gameObject);
        }
    }

    private Vector2 GetElementPosition(GameObject element)
    {
        return element.GetComponent<UIElement>().position;
    }

    private GameObject GetElementAt(Vector2 position)
    {
        foreach (GameObject element in elements)
        {
            if (GetElementPosition(element) == position)
            {
                return element;
            }
        }

        return null;
    }

    private Vector2 GetElementSize(GameObject element)
    {
        return element.GetComponent<UIElement>().size;
    }

    private void GetScreenRect()
    {
        screen.width = Screen.width;
        screen.height = Screen.height;
    }

    private int GetRows()
    {
        int max = 0;
        foreach (GameObject element in elements)
        {
            int row = (int) GetElementPosition(element).y;
            if (row > max)
            {
                max = row;
            }
        }

        return max + 1;
    }

    private int GetColumns()
    {
        int max = 0;
        foreach (GameObject element in elements)
        {
            int column = (int) GetElementPosition(element).x;
            if (column > max)
            {
                max = column;
            }
        }

        return max + 1;
    }

    private void PartitionElements()
    {
        GetScreenRect();
        int rows = GetRows();
        int columns = GetColumns();

        for (int i = 0; i < rows; i++)
        {
            int elementsPerRow = 0;
            for (int j = columns - 1; j >= 0; j--)
            {
                GameObject element = GetElementAt(new Vector2(j, i));
                if (element != null)
                {
                    elementsPerRow = (int) GetElementPosition(element).x + 1;
                    j = -1;
                }
            }

            for (int j = 0; j < elementsPerRow; j++)
            {
                GameObject element = GetElementAt(new Vector2(j, i));
                element.GetComponent<RectTransform>().position = new Vector3(j * screen.width / elementsPerRow + margin, screen.height - i * screen.height / rows - margin, 0);

                element.GetComponent<RectTransform>().sizeDelta = new Vector2(screen.width / elementsPerRow - 2 * margin, screen.height / rows - 2 * margin);
            }
        }
    }
}
