using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour {
	#region Singelton
	public static InventoryManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<InventoryManager>();
			}
			return _instance;
		}
	}
	static InventoryManager _instance;

	void Awake()
	{
		_instance = this;
	}
	#endregion

	#region Variables
	public List<GameObject> Weapons = new List<GameObject>();
	public int inventroySize = 4;
	public GameObject currentWeapon;
	public GameObject LastWeapon;
	public GameObject[] inventoryIcons;
	public List<Sprite> icons = new List<Sprite>();
	public bool changeIcons;
	#endregion

	#region Methods
	public void Start()
	{
		inventoryIcons = GameObject.FindGameObjectsWithTag("InventoryIcon");
	}
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			SwitchToGunOne();
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			SwitchToGunTwo();
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			SwitchToGunThree();
		}
		if (Input.GetKeyDown(KeyCode.T)) {
			TellMeIconNames();
		}
		GetIcons();
	}
	public void ChangeIcons() {
		changeIcons = true;
	}
	public void TellMeIconNames() {
		for (int i = 0; i < icons.Count; i++)
		{
			Debug.Log("" + icons[i].name);
		}
	}
	public void SwitchToGunOne()
	{
		Weapons[0].SetActive(true);
		Weapons[1].SetActive(false);
		Weapons[2].SetActive(false);
		Weapons[3].SetActive(false);
	}
	public void SwitchToGunTwo()
	{
		Weapons[2].SetActive(false);
		Weapons[0].SetActive(false);
		Weapons[1].SetActive(true);
		Weapons[3].SetActive(false);
	}
	public void SwitchToGunThree()
	{
		Weapons[1].SetActive(false);
		Weapons[0].SetActive(false);
		Weapons[2].SetActive(true);
		Weapons[3].SetActive(false);
	}
	public void SwitchToGunFour()
	{
		Weapons[2].SetActive(false);
		Weapons[1].SetActive(false);
		Weapons[0].SetActive(false);
		Weapons[3].SetActive(true);
	}
	public void AddWeapon(GameObject Weapon) {
		if (Weapons.Count <= inventroySize) {
			Weapons.Add(Weapon);
		}
	}
	public void RemoveWeapon(GameObject weapon) {
		Weapons.Remove(weapon);
	}
	public void ChangeInventoryIcon(Sprite WeaponSprite) {
		for (int i = 0; i < icons.Count; i++) {
			if (icons[i].name == "Unity")
			{
				Debug.Log("Should Change " + icons[i].name + " to " + WeaponSprite);
				icons[i] = WeaponSprite;
				Debug.Log("icons name is" + icons[i].name);
				changeIcons = false;
				return;
			}
			else {
				return;
			}
		}
	}
	public void GetIcons() {
		for (int i = 0; i < inventoryIcons.Length; i++) {
			//Debug.Log("GettingIcons");
			icons[i] = inventoryIcons[i].GetComponent<Image>().sprite;
		}
	}

	public bool IsWeaponAlreadyInInventory(GameObject weapon) {
		if (Weapons.Contains(weapon))
		{
			return true;
		}
		else {
			return false;
		}
	}
	#endregion
}
