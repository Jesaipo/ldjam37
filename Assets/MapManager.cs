using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {

	#region Singleton
	public static MapManager m_instance;
	void Awake(){
		if(m_instance == null){
			//If I am the first instance, make me the Singleton
			m_instance = this;
		}else{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != m_instance)
				Destroy(this.gameObject);
		}
	}
	#endregion Singleton

	public LayerMask UsedMask;
	public LayerMask GroundMask;
	public LayerMask NotGoMask;

	public IList<Vector2> allDot;
	public IList<Vector2> usedDot;
	public IList<Vector2> GroundDot;

	public IList<Vector2> alreadyHit;

	public Transform startTransform;
	public Transform rightLimit;
	public Transform leftLimit;
	public Transform TopLimit;

	public GameObject CurrentBonus;

	float delta = 0.2f;
	// Use this for initialization
	void Start () {
		GroundDot = new List<Vector2>();
		usedDot = new List<Vector2>();
		allDot =  new List<Vector2>();
		//remplir le tableau
		IList<Vector2> line = new List<Vector2>();
		float currentX = startTransform.position.x;
		line.Add (startTransform.position);
		currentX -= 3;
		while (currentX >= leftLimit.position.x) {
			line.Add (new Vector2(currentX,startTransform.position.y));
			currentX -= 3;
		}

		currentX = startTransform.position.x;
		currentX += 3;
		while (currentX <= rightLimit.position.x) {
			line.Add (new Vector2(currentX,startTransform.position.y));
			currentX += 3;
		}

		float currentY = startTransform.position.y;
		while (currentY < TopLimit.position.y) {
			foreach(Vector2 v in line){
				allDot.Add (new Vector2(v.x, currentY));
			}
			currentY += 3;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentBonus.activeSelf == false) {
		
			Vector3 newPosition = new Vector3(-1000,-1000,-1000);
			while (newPosition.x < -900) {
				Vector2 groundPosition = GroundDot[Random.Range (0, GroundDot.Count)];
				int x = Random.Range (-2, 2);
				int y = Random.Range (-2, 2);
				Vector2 decalage = groundPosition + 3 * new Vector2 (x,y);
				if (!PersonnalContains (GroundDot, decalage) && PersonnalContains(allDot,decalage)) {
					newPosition = decalage;
				}
				}

			CurrentBonus.transform.position = newPosition;
			CurrentBonus.SetActive (true);

		}
	}

	public void refreshUsedDot(){
		usedDot.Clear ();
		GroundDot.Clear ();
		foreach (Vector2 v in allDot) {
			if (Physics2D.OverlapCircle (v, 0.1f, UsedMask)) {
				usedDot.Add (v);
			}
			if (Physics2D.OverlapCircle (v, 0.1f, GroundMask)) {
				GroundDot.Add (v);
			}
		}
	}

	public List<Vector2> getDotBetween(Vector2 playerPosition,Vector2 position){
		alreadyHit = new List<Vector2> ();
		List<Vector2> path = new List<Vector2> ();
		if (PersonnalContains(GroundDot,position)) {
			path = strikeAndContinue(playerPosition,position);
		}
	

		return path;
	}

	public List<Vector2> strikeAndContinue(Vector2 position, Vector2 target){

		alreadyHit.Add (position);
		List<Vector2> FinalList = new List<Vector2> ();
		if (target.x <= position.x+delta && target.x >= position.x-delta  && target.y <= position.y+delta && target.y >= position.y-delta) {
			FinalList.Add (position);
			return FinalList;
		}


		List<Vector2> accesibleDotFromHere = new List<Vector2>();
		//cross 
		RaycastHit2D hitLeft = Physics2D.Raycast (position, new Vector2(1,0),3.2f,NotGoMask);
		RaycastHit2D hitRight = Physics2D.Raycast (position, new Vector2(-1,0),3.2f,NotGoMask);
		RaycastHit2D hitTop = Physics2D.Raycast (position, new Vector2(0,1),3.2f,NotGoMask);
		RaycastHit2D hitBottom = Physics2D.Raycast (position, new Vector2(0,-1),3.2f,NotGoMask);

		if (!hitLeft) {
			Vector2 leftPosition = position + new Vector2 (3f, 0);
			bool toto = !PersonnalContains(alreadyHit,leftPosition);
			bool titi = PersonnalContains(GroundDot,leftPosition);
			if (toto &&titi) {
				FinalList.AddRange (strikeAndContinue (leftPosition, target));
				if (FinalList.Count > 0) {
					FinalList.Insert (0, position);
				}
			}
		}

		if (!hitRight) {
			Vector2 rightPosition = position + new Vector2 (-3f, 0);
			bool toto = !PersonnalContains(alreadyHit,rightPosition);
			bool titi = PersonnalContains(GroundDot,rightPosition);
			if (toto &&titi) {
				FinalList.AddRange (strikeAndContinue (rightPosition, target));
				if (FinalList.Count > 0) {
					FinalList.Insert (0, position);
				}
			}
		}

		if (!hitTop) {
			Vector2 topPosition = position + new Vector2 (0, 3f);
			bool toto = !PersonnalContains(alreadyHit,topPosition);
			bool titi = PersonnalContains(GroundDot,topPosition);
			if (toto &&titi) {
				FinalList.AddRange (strikeAndContinue (topPosition, target));
				if (FinalList.Count > 0) {
					FinalList.Insert (0, position);
				}
			}
		}

		if (!hitBottom) {
			Vector2 bottomPosition = position + new Vector2 (0, -3f);
			bool toto = !PersonnalContains(alreadyHit,bottomPosition);
			bool titi = PersonnalContains(GroundDot,bottomPosition);
			if (toto &&titi) {
				FinalList.AddRange (strikeAndContinue (bottomPosition, target));
				if (FinalList.Count > 0) {
					FinalList.Insert (0, position);
				}
			}
		}

		return FinalList;
	}

	public bool PersonnalContains(IList<Vector2> list, Vector2 pos){
		
		foreach (Vector2 v in list) {
			
			if (v.x <= pos.x+delta && v.x >= pos.x-delta  && v.y <= pos.y+delta && v.y >= pos.y-delta ) {
				return true;
			}
		}

		return false;
	}
}
