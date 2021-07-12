using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text text;

    private int score = 0;

    private void Update()
    {
        text.text = score.ToString();
    }

    public void AddPoints(int points)
    {
        score += points;
    }
}
