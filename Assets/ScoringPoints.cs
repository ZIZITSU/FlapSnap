using UnityEngine;

public class ScoringPoints : MonoBehaviour
{
    private LogicManager logic;
    private bool alreadyScored = false;

    private void Start()
    {
        GameObject logicObject =
            GameObject.FindGameObjectWithTag("LogicTag");

        if (logicObject == null)
        {
            Debug.LogError("No GameObject with LogicTag was found!");
            return;
        }

        logic = logicObject.GetComponent<LogicManager>();

        if (logic == null)
        {
            Debug.LogError(
                "LogicManager is missing from the LogicTag GameObject!"
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Debug.Log(
                "Score trigger touched by: " +
                collision.gameObject.name
            );

            if (!alreadyScored && logic != null)
            {
                alreadyScored = true;

                // Adds score and plays the point sound
                logic.AddScore(1);
            }
        }
    }
}