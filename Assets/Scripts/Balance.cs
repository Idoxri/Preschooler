using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
	[SerializeField] private SpriteRenderer imageFruit = default;
	[SerializeField] private Sprite spriteValidationImage = default;
	[SerializeField] private Button btnValidation;

	[HideInInspector]
    public bool isDrop = true;
	//[HideInInspector]
	public List<Item> fruitToDisplay = new List<Item>();

	private Item actualFruit;
	private Item actualFruitToWeight = default;

	static public Balance Instance = default;

	private void Start()
	{
		if (Instance == null)
			Instance = this;

		fruitToDisplay = CustomerHandler.Instance.CurrentCustomer.ItemList;
		btnValidation.onClick.AddListener(ValidWeighting);
		DisplayFruit();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Item fruitDrop = collision.GetComponent<Item>();

		if (CheckRightFruit(fruitDrop))
		{
			actualFruit = collision.GetComponent<Item>();
		}
	}

	private bool CheckRightFruit(Item fruitToCheck)
	{
		if (fruitToCheck.name == actualFruitToWeight.name)
			return true;
		else
			return false;
	}

	public void ValidWeighting()
	{
		Debug.Log(actualFruit.name);

		if (CheckRightFruit(actualFruit))
		{
			fruitToDisplay.Remove(actualFruit);
			imageFruit.sprite = spriteValidationImage;
			Debug.Log("BonFruit");
		}
		else
		{
			Debug.Log("MauvaisFruit");
		}
	}

	private void DisplayFruit()
	{
		int randomIndex = Random.RandomRange(0, fruitToDisplay.Count);

		Item randomFruit =  fruitToDisplay[randomIndex];
		actualFruitToWeight = randomFruit;
		imageFruit.sprite = randomFruit.Sprite;
	}
}
