using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class VaseInteraction : MonoBehaviour
{
    private bool player1Interacted = false;
    private bool player2Interacted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player1"))
        {
            player1Interacted = true;
            Debug.Log("Vase Enter 1");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player2"))
        {
            player2Interacted = true;
            Debug.Log("Vase Enter 2");
        }

        if (player1Interacted && player2Interacted)
        {
            LoadNextScene();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player1"))
        {
            player1Interacted = false;
            Debug.Log("Vase Exit 1");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player2"))
        {
            player2Interacted = false;
            Debug.Log("Vase Exit 2");
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
