using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class SceneButton {
	public string sceneName;
	public string buttonText;
}

public class SceneSelector : MonoBehaviour {
	public GameObject buttonContainer;
	public GameObject buttonPrefab;
	public SceneButton[] sceneButtons;

	void Start() {
		foreach (SceneButton sceneButton in sceneButtons) {
			GameObject b = Instantiate(buttonPrefab, buttonContainer.transform);
			b.GetComponentInChildren<Button>().onClick.AddListener(() => { SceneManager.LoadScene(sceneButton.sceneName); });
			b.GetComponentInChildren<Text>().text = sceneButton.buttonText;
		}
	}
}
