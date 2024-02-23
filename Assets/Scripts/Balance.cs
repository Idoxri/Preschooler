using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
	[SerializeField] private SpriteRenderer imageFruit = default;
	[SerializeField] private Sprite spriteValidationImage = default;
	[SerializeField] private GameObject ticketToSpawn;
	[SerializeField] private Transform pointToSpawnTicket;

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
		DisplayFruit();
	}

    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Fruit"))
        {
			Item fruitDrop = collision.gameObject.GetComponent<Item>();

			Debug.Log(CheckRightFruit(fruitDrop));

			if (CheckRightFruit(fruitDrop))
			{
				actualFruit = collision.gameObject.GetComponent<Item>();
			}
		}



	}

	private bool CheckRightFruit(Item fruitToCheck)
	{
		if (fruitToCheck.DisplayName == actualFruitToWeight.DisplayName)
			return true;
		else
			return false;
	}

	public void ValidWeighting()
	{
		if (actualFruit)
		{

			if (CheckRightFruit(actualFruit))
			{
				fruitToDisplay.Remove(actualFruit);
				imageFruit.sprite = spriteValidationImage;
				Debug.Log("BonFruit");
				Instantiate(ticketToSpawn, pointToSpawnTicket.transform.position, Quaternion.identity);
				actualFruit = null;
				DisplayFruit();
			}
			else
			{
				Debug.Log("MauvaisFruit");
			}
		}
	}

	private void DisplayFruit()
	{
		int randomIndex = Random.Range(0, fruitToDisplay.Count);

		Item randomFruit =  fruitToDisplay[randomIndex];
		actualFruitToWeight = randomFruit;
		imageFruit.sprite = randomFruit.Sprite;
	}
}
