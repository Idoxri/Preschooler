using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
	[SerializeField] private Image imageFruit = default;
	[SerializeField] private Sprite spriteValidationImage = default;
	[SerializeField] private Button btnValidation;

	[HideInInspector]
    public bool isDrop = true;
	[HideInInspector]
	public List<GameObject> fruitToDisplay = new List<GameObject>();

	private GameObject actualFruit;
	private GameObject actualFruitToWeight = default;

	private void Start()
	{
		btnValidation.onClick.AddListener(ValidWeighting);
		DisplayFruit(null);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject fruitDrop = collision.GetComponent<GameObject>();

		if (CheckRightFruit(fruitDrop))
		{
			actualFruit = collision.GetComponent<GameObject>();
		}
	}

	private bool CheckRightFruit(GameObject fruitToCheck)
	{
		if (fruitToCheck == actualFruitToWeight)
			return true;
		else
			return false;
	}

	private void ValidWeighting()
	{
		if (CheckRightFruit(actualFruit))
		{
			fruitToDisplay.Remove(actualFruit);
			imageFruit.sprite = spriteValidationImage;
		}
	}

	private void DisplayFruit(Sprite newImageFruit)
	{
		int randomIndex = Random.RandomRange(0, fruitToDisplay.Count - 1);

		GameObject randomFruit =  fruitToDisplay[randomIndex];
		actualFruitToWeight = randomFruit;
		imageFruit.sprite = newImageFruit;
	}
}
