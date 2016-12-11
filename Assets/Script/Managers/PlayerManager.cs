using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	public bombeCOunter bombeCounter;
	public LayerMask VictoryMask;
	public LayerMask NextLevelMask;
	public LayerMask BonusMask;
	public LayerMask VoidMask;
	public LayerMask NoGoLayer;
	public LayerMask GroundLayer;
	public GameObject Bombe;
	public Vector3 offsetBomb;
	public bool askForBombe = false;
	public int NBBomb = 0;
	public GameObject followGO;
	public List<Vector2> dotPath;

	#region Singleton
	public static PlayerManager m_instance;
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



	// Use this for initialization
	void Start () {
		GameStateManager.onChangeStateEvent += handleChangeGameState;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (followGO) {
			this.transform.position = followGO.gameObject.transform.position;
		}
		var VictoryCollider = Physics2D.OverlapCircle (this.transform.position, 0.3f, VictoryMask);
		if (VictoryCollider) {
			GameStateManager.setGameState (GameState.GameOver);
			Application.LoadLevelAsync ("GameOverScene");
		}

		var nextLevelCollider = Physics2D.OverlapCircle (this.transform.position, 0.3f,NextLevelMask);
		if (nextLevelCollider) {
			MapManager.m_instance.ChangeLevel ();
			nextLevelCollider.gameObject.SetActive (false);
		}

		var bombCollider = Physics2D.OverlapCircle (this.transform.position, 0.3f, BonusMask);
		if (bombCollider) {
			bombCollider.gameObject.SetActive (false);
			NBBomb += 3;
			bombeCounter.addBombes (NBBomb);
		}

		if (dotPath.Count > 0) {
			Vector2 direction = dotPath [0] - new Vector2(this.transform.position.x,this.transform.position.y) ;
			direction.Normalize ();
			this.transform.position = this.transform.position + new Vector3(direction.x,direction.y);
			if(this.transform.position.x >  dotPath [0].x -0.2f && this.transform.position.x <  dotPath [0].x +0.2f 
				&& this.transform.position.y >  dotPath [0].y -0.2f && this.transform.position.y <  dotPath [0].y +0.2f)
				dotPath.RemoveAt (0);
			Collider2D hit = Physics2D.OverlapCircle (transform.position, 0.05f, GroundLayer);
			if (hit) {
				followGO = hit.gameObject;
			}
		}


	}

	void handleChangeGameState(GameState newState){
		Debug.Log ("PLAYER SEE THE NEW STATE : " + newState);
	}



	#region Intéraction
	public static void UP(){
		Debug.Log("UP ! ");
		if (!Physics2D.OverlapCircle (m_instance.transform.position + new Vector3 (0, 0.6f), 0.05f, m_instance.NoGoLayer)) {
			PlayerManager.m_instance.transform.position += new Vector3 (0, 0.5f);
	}
	}

	public static void DOWN(){
		Debug.Log("DOWN ! ");
		if (!Physics2D.OverlapCircle (m_instance.transform.position + new Vector3 (0, -0.6f), 0.05f, m_instance.NoGoLayer)) {
			PlayerManager.m_instance.transform.position += new Vector3 (0, -0.5f);
		}
	}

	public static void LEFT(){
		Debug.Log("LEFT ! ");
		if (!Physics2D.OverlapCircle (m_instance.transform.position + new Vector3 (-0.6f, 0), 0.05f, m_instance.NoGoLayer)) {
				PlayerManager.m_instance.transform.position += new Vector3 (-0.5f,0);
		}
	}

	public static void RIGHT(){
		if (!Physics2D.OverlapCircle (m_instance.transform.position + new Vector3 (0.6f, 0), 0.05f, m_instance.NoGoLayer)) {
				PlayerManager.m_instance.transform.position += new Vector3 (0.5f, 0);
		} //TODO NOP animation
	}

	public static void ASKFORBOMBE(Vector3 mousePosition){
		Vector3 center = CenterBlockFromMouse (mousePosition);
		if (center.x > -900) {
			if (m_instance.NBBomb > 0) {
				var newBombe = Instantiate (m_instance.Bombe);
				newBombe.transform.position = center;
				m_instance.NBBomb--;
				m_instance.bombeCounter.removeBombe (m_instance.NBBomb);
			}
		} else {
//NOP SOUND
		}
	}

	public static Vector3 CenterBlockFromMouse(Vector3 mousePosition){
		var position = Camera.main.ScreenToWorldPoint (mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (position, Vector2.zero);
		Vector3 center = new Vector3(-1000,-1000,-1000);
		if (hit) {
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("Block")) {
				
				center = hit.collider.gameObject.transform.position;
			}
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
				
				Block[] blocks = hit.collider.gameObject.transform.parent.GetComponentsInChildren<Block>();

				Vector3 max = new Vector3(-1000,-1000,-1000);
				Vector3 min = new Vector3(1000,1000,1000);
				foreach (Block b in blocks) {
					Vector3 p = b.transform.position;
					if (p.x >= max.x-0.2f && p.y >= max.y-0.2f)
						max = p;
					if (p.x <= min.x+0.2f && p.y <= min.y+0.2f)
						min = p;
				}

				foreach (Block b in blocks) {
					Vector3 p = b.transform.position;
					if (p.x < max.x-0.5f && p.y < max.y-0.5f && p.x > min.x+0.5f && p.y > min.y +0.5f)
						center = p;
				}

			}
		}
		return center;
	}

	public static void MOVEATPOSITION(Vector3 mousePosition){
		Vector3 center = CenterBlockFromMouse (mousePosition);
		if (center.x > -900) {
			List<Vector2> tempdotPath = MapManager.m_instance.getDotBetween (m_instance.transform.position, center);
			if (tempdotPath.Count > 0) {
				m_instance.dotPath = tempdotPath;
				return;
			}
		}

		//NOP SOUND
	}
		

	#endregion Intéraction

	void OnDestroy(){
		GameStateManager.onChangeStateEvent -= handleChangeGameState;
	}
}
