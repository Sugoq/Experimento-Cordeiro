using UnityEngine;

public class EndPlataform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("P1"))
        {
            Debug.Log("voze paso de faze");
            Invoke("CallNextLevel", 1f);
        }
    }

    void CallNextLevel() => LevelManager.instance.NextLevel();

}
