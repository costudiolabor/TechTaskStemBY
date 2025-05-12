using System;
using UnityEngine.SceneManagement;

[Serializable]
public class GameHandler {
  
    public void ReLoadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
}
