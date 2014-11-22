using UnityEngine;
using System.Collections;
using Pathfinding;
using UnityEngine.UI;

public class TileProperties : MonoBehaviour {

    public GameObject UnitOnTile = null;
    public GameObject UnitMoveController;
    public GameObject datTarget;
    public GameObject datNode;
    public GameObject AStarController;
    public GameObject SoundController;
    public bool fighting = false;
    public double fightTimer;
    public bool Occupied = false;
    public bool canPlace;
    public static bool pieceSelected = false; 

    public Animator PieceAnim;

    GameObject Canvas;
    int whatScene = 0;
    float backTimer;
    bool transStarted;
    public bool cameBack;
    
    // Use this for initialization
	void Start () {
        //UnitOnTile = null;
        Canvas = GameObject.Find("Canvas");
        UnitMoveController = GameObject.Find("MovementController");
        datNode = transform.FindChild("Node").gameObject;
        AStarController = GameObject.Find("ISOCamera");
        SoundController = GameObject.Find("UISoundController");
        if(UnitOnTile != null)
        {
            if (UnitOnTile.GetComponentInChildren<Animator>() != null)
            {
                print("Set Animator");
                PieceAnim = UnitOnTile.GetComponentInChildren<Animator>();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("BattleSceneAdditive") == null && cameBack == true)
        {
            if (backTimer <= 3)
            {
                backTimer += Time.deltaTime;
                if (backTimer >= 2f && backTimer < 2.1f)
                {
                    Canvas.GetComponent<SceneTrans>().open = true;
                    Canvas.GetComponent<SceneTrans>().startTimer = 0;
                }
            }
            else
            {
                cameBack = false;
                backTimer = 0;
            }
        }

        if(fighting)
        {
            if(fightTimer <= 3)
            {
                fightTimer += Time.deltaTime;
                if (fightTimer >= 2f && fightTimer < 2.1f)
                {
                    Canvas.GetComponent<SceneTrans>().close = true;
                    Canvas.GetComponent<SceneTrans>().startTimer = 0;
                }
            }
            else
            {
                whatScene = Random.Range(1, 10);
                Debug.Log("GOTO fight Scene!!!");
                fighting = false; 
                fightTimer = 0;
                //Application.LoadLevel(6);
                
                if (GameObject.Find("EnemyTurnController") != null)
                {
                    Debug.Log("EnemyTurnController Found");
                    Application.LoadLevelAdditive("TutorialTestBattle");
                }
                else
                {
                    if (whatScene <=5)
                    {
                        Application.LoadLevelAdditive("RobotBattleSmall");
                    }
                    else
                    {
                        Application.LoadLevelAdditive("AlienBattleSmall");
                    }
                }
            }
        }
	}

    public void OnMouseOver()
    {
        if(this.UnitOnTile != null)
        {
            if (this.UnitOnTile.name == "BlackTank(Clone)")
            {
                if (UnitMoveController.GetComponent<PawnMove>().BlackTankPan != null)
                {
                    UnitMoveController.GetComponent<PawnMove>().BlackTankPan.SetActive(true);
                }
                
                if (GameObject.Find("PieceNameText") != null)
                {
                    GameObject.Find("PieceNameText").GetComponent<Text>().text = "Organic Tank";
                }
                if (GameObject.Find("SpecialText") != null)
                {
                    GameObject.Find("SpecialText").GetComponent<Text>().text = "Heat Seeking Missile(F Key)";
                }
                
            }
            else if (this.UnitOnTile.name == "BlackGrunt(Clone)")
            {
                if (UnitMoveController.GetComponent<PawnMove>().BlackGruntPan != null)
                {
                    UnitMoveController.GetComponent<PawnMove>().BlackGruntPan.SetActive(true);
                }
                
                if (GameObject.Find("PieceNameText") != null)
                {
                    GameObject.Find("PieceNameText").GetComponent<Text>().text = "Organic Grunt";
                }
                if (GameObject.Find("SpecialText") != null)
                {
                    GameObject.Find("SpecialText").GetComponent<Text>().text = "Shield(F Key)";
                }
            }
            else if (this.UnitOnTile.name == "BlackScout(Clone)")
            {
                if (UnitMoveController.GetComponent<PawnMove>().BlackTankPan != null)
                {
                    UnitMoveController.GetComponent<PawnMove>().BlackScoutPan.SetActive(true);
                }
                
                if (GameObject.Find("PieceNameText") != null)
                {
                    GameObject.Find("PieceNameText").GetComponent<Text>().text = "Organic Scout";
                }

                if(GameObject.Find("SpecialText") != null)
                {
                    GameObject.Find("SpecialText").GetComponent<Text>().text = "";
                }

            }
            else if (this.UnitOnTile.name == "BlackRunner(Clone)")
            {
                if (UnitMoveController.GetComponent<PawnMove>().BlackGruntPan != null)
                {
                    UnitMoveController.GetComponent<PawnMove>().BlackRunnerPan.SetActive(true);
                }
                
                if (GameObject.Find("PieceNameText") != null)
                {
                    GameObject.Find("PieceNameText").GetComponent<Text>().text = "Organic Runner";
                }
                if (GameObject.Find("SpecialText") != null)
                {
                    GameObject.Find("SpecialText").GetComponent<Text>().text = "";
                }
            }
            else if (this.UnitOnTile.name == "WhiteTank(Clone)")
            {
                if (UnitMoveController.GetComponent<PawnMove>().WhiteTankPan != null)
                {
                    UnitMoveController.GetComponent<PawnMove>().WhiteTankPan.SetActive(true);
                }
                
                if (GameObject.Find("PieceNameText") != null)
                {
                    GameObject.Find("PieceNameText").GetComponent<Text>().text = "Synthetic Tank";
                }
                if (GameObject.Find("SpecialText") != null)
                {
                    GameObject.Find("SpecialText").GetComponent<Text>().text = "Heat Seeking Missile(F Key)";
                }
            }
            else if (this.UnitOnTile.name == "WhiteScout(Clone)")
            {
                if (UnitMoveController.GetComponent<PawnMove>().WhiteScoutPan != null)
                {
                    UnitMoveController.GetComponent<PawnMove>().WhiteScoutPan.SetActive(true);
                }
                
                if (GameObject.Find("PieceNameText") != null)
                {
                    GameObject.Find("PieceNameText").GetComponent<Text>().text = "Synthetic Scout";
                }

                if (GameObject.Find("SpecialText") != null)
                {
                    GameObject.Find("SpecialText").GetComponent<Text>().text = "";
                }
            }
            else if (this.UnitOnTile.name == "WhiteGrunt(Clone)")
            {

                if (UnitMoveController.GetComponent<PawnMove>().WhiteTankPan != null)
                {
                    UnitMoveController.GetComponent<PawnMove>().WhiteGruntPan.SetActive(true);
                }
                if (GameObject.Find("PieceNameText") != null)
                {
                    GameObject.Find("PieceNameText").GetComponent<Text>().text = "Synthetic Grunt";
                }
                if (GameObject.Find("SpecialText") != null)
                {
                    GameObject.Find("SpecialText").GetComponent<Text>().text = "Shield(F Key)";
                }
            }
            else if (this.UnitOnTile.name == "WhiteRunner(Clone)")
            {
                if (UnitMoveController.GetComponent<PawnMove>().WhiteScoutPan != null)
                {
                    UnitMoveController.GetComponent<PawnMove>().WhiteRunnerPan.SetActive(true);
                }
                if (GameObject.Find("PieceNameText") != null)
                {
                    GameObject.Find("PieceNameText").GetComponent<Text>().text = "Synthetic Runner";
                }
                if (GameObject.Find("SpecialText") != null)
                {
                    GameObject.Find("SpecialText").GetComponent<Text>().text = "";
                }
            }
            
            if(UnitMoveController.GetComponent<PawnMove>().StatPan != null)
            {
                UnitMoveController.GetComponent<PawnMove>().StatPan.SetActive(true);
            }
            if (GameObject.Find("HealthSlider") != null)
            {
                GameObject.Find("HealthSlider").GetComponent<Slider>().maxValue = this.UnitOnTile.GetComponent<PiecePropScript>().MaxHealth;
                GameObject.Find("HealthSlider").GetComponent<Slider>().value = this.UnitOnTile.GetComponent<PiecePropScript>().Health;
            }
            if (GameObject.Find("healthText") != null)
            {
                GameObject.Find("healthText").GetComponent<Text>().text = this.UnitOnTile.GetComponent<PiecePropScript>().Health.ToString();
            }
            if(GameObject.Find("DamageSlider") != null)
            {
                GameObject.Find("DamageSlider").GetComponent<Slider>().value = this.UnitOnTile.GetComponent<PiecePropScript>().Damage;
            }
            if (GameObject.Find("SpeedSlider") != null)
            {
                GameObject.Find("SpeedSlider").GetComponent<Slider>().value = this.UnitOnTile.GetComponent<PiecePropScript>().Movement;
            }
            
            
            
        }
        if (UnitMoveController.GetComponent<PawnMove>().isMoving == false)
        {
            TurnStateMachine.OnHoverPiece = this.UnitOnTile;
            renderer.material.color = Color.yellow;
        }
        else if (UnitMoveController.GetComponent<PawnMove>().isMoving == true)
        {
            UnitMoveController.GetComponent<PawnMove>().MoveToTile = this.gameObject;
            if (this.gameObject.GetComponent<TileProperties>().UnitOnTile != null)
            {
                this.datNode.SetActive(true);
                GridManager.rescan = true; 
            }
            
            //this should draw out path prior to moving
            UnitMoveController.GetComponent<PawnMove>().SelectedPiece.GetComponent<pieceMove>().targetPosition = UnitMoveController.GetComponent<PawnMove>().MoveToTile.transform.position;
            print("past assigning targetposition");
            UnitMoveController.GetComponent<PawnMove>().SelectedPiece.GetComponent<pieceMove>().GenNewPath();
            print("new path generated!!!!!");

            if(Vector3.Distance(UnitMoveController.GetComponent<PawnMove>().SelectedPiece.transform.position, this.transform.position) <= UnitMoveController.GetComponent<PawnMove>().MaxMove)
            {
                renderer.material.color = Color.green;
                canPlace = true;
            }
            else
            {
                renderer.material.color = Color.red;
                canPlace = false;
            }
        }

    }

    public void OnMouseExit()
    {
        if(UnitMoveController.GetComponent<PawnMove>().StatPan != null)
        {
            UnitMoveController.GetComponent<PawnMove>().StatPan.SetActive(false);
        }
        if(UnitMoveController.GetComponent<PawnMove>().BlackTankPan != null)
        {
            UnitMoveController.GetComponent<PawnMove>().BlackTankPan.SetActive(false);
        }
        if(UnitMoveController.GetComponent<PawnMove>().BlackGruntPan != null)
        {
            UnitMoveController.GetComponent<PawnMove>().BlackGruntPan.SetActive(false);
        }
        if (UnitMoveController.GetComponent<PawnMove>().BlackRunnerPan != null)
        {
            UnitMoveController.GetComponent<PawnMove>().BlackRunnerPan.SetActive(false);
        }
        if (UnitMoveController.GetComponent<PawnMove>().BlackScoutPan != null)
        {
            UnitMoveController.GetComponent<PawnMove>().BlackScoutPan.SetActive(false);
        }
        if(UnitMoveController.GetComponent<PawnMove>().WhiteTankPan != null)
        {
            UnitMoveController.GetComponent<PawnMove>().WhiteTankPan.SetActive(false);
        }
        if(UnitMoveController.GetComponent<PawnMove>().WhiteScoutPan != null)
        {
            UnitMoveController.GetComponent<PawnMove>().WhiteScoutPan.SetActive(false);
        }
        if (UnitMoveController.GetComponent<PawnMove>().WhiteRunnerPan != null)
        {
            UnitMoveController.GetComponent<PawnMove>().WhiteRunnerPan.SetActive(false);
        }
        if (UnitMoveController.GetComponent<PawnMove>().WhiteGruntPan != null)
        {
            UnitMoveController.GetComponent<PawnMove>().WhiteGruntPan.SetActive(false);
        }
        if (GameObject.Find("PieceNameText") != null)
        {
            GameObject.Find("PieceNameText").GetComponent<Text>().text = "";
        }
        if (GameObject.Find("SpecialText") != null)
        {
            GameObject.Find("SpecialText").GetComponent<Text>().text = "";
        }
        if(this.tag == "WhiteTile")
        {
            renderer.material.color = Color.white;
        }
        if (this.tag == "BlackTile")
        {
            renderer.material.color = Color.black;
        }
        canPlace = false;
        //UnitMoveController.GetComponent<PawnMove>().MoveToTile = null;
        if (this.gameObject.GetComponent<TileProperties>().UnitOnTile != null && this.gameObject != UnitMoveController.GetComponent<PawnMove>().currentTile)
        {
            this.datNode.SetActive(false);
            GridManager.rescan = true;
        }
    }

    public void OnMouseDown()
    {
        MouseDownCall();
    }

    void ReplaceWhitePiece(GameObject selectedPiece)
    {
        Destroy(selectedPiece.gameObject);
        this.UnitOnTile = Instantiate(selectedPiece, this.transform.position, Quaternion.Euler(0, -90, 0)) as GameObject;
        this.Occupied = false;
        UnitMoveController.GetComponent<PawnMove>().isMoving = false;
        canPlace = false; 
    }
    void ReplaceBlackPiece(GameObject selectedPiece)
    {
        Destroy(selectedPiece.gameObject);
        this.UnitOnTile = Instantiate(selectedPiece, this.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
        this.Occupied = false; 
        UnitMoveController.GetComponent<PawnMove>().isMoving = false;
        canPlace = false; 
    }
    void SetTarget()
    {
        TileProperties.pieceSelected = false; 
        UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.SetActive(true);
        GridManager.rescan = true;
        print("targetTile true");
        GameObject newTarget = Instantiate(datTarget, transform.position, transform.rotation) as GameObject;
        UnitMoveController.GetComponent<PawnMove>().SelectedPiece.GetComponent<pieceMove>().targetPosition = newTarget.transform.position;
        UnitMoveController.GetComponent<PawnMove>().SelectedPiece.GetComponent<pieceMove>().GetNewPath();
        UnitMoveController.GetComponent<PawnMove>().SelectedPiece.GetComponent<pieceMove>().isMoving = true;
        Destroy(newTarget.gameObject);
        this.UnitOnTile = UnitMoveController.GetComponent<PawnMove>().SelectedPiece;
        UnitMoveController.GetComponent<PawnMove>().SelectedPiece.GetComponent<pieceMove>().datTile = this.gameObject;
        //this.datNode.gameObject.SetActive(true);
        UnitMoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
        if (UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile != null)
        {
            UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(false);
        }
        
        UnitMoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().UnitOnTile = null;
        UnitMoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().Occupied = false;
        UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().Occupied = false;
        UnitMoveController.GetComponent<PawnMove>().isMoving = false;
        canPlace = false;

    }

    void SelectPiece()
    {
        if (GameObject.Find("EnemyTurnController") != null)
        {
            if(TurnStateMachine.OnHoverPiece.name == "WhiteTank(Clone)")
            {
                TileProperties.pieceSelected = true; 
                UnitMoveController.GetComponent<PawnMove>().SelectedPiece = UnitOnTile;
                UnitMoveController.GetComponent<PawnMove>().isMoving = true;
                UnitMoveController.GetComponent<PawnMove>().currentTile = this.gameObject;
                UnitMoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
                datNode.gameObject.SetActive(true);
                if (UnitMoveController.GetComponent<PawnMove>().isMoving)
                {
                    GridManager.rescan = true;
                }
            }
            else
            {
                SoundController.GetComponent<UISoundsScript>().playError();
            }
        }
        else
        {
            TileProperties.pieceSelected = true; 
            UnitMoveController.GetComponent<PawnMove>().SelectedPiece = UnitOnTile;
            UnitMoveController.GetComponent<PawnMove>().isMoving = true;
            UnitMoveController.GetComponent<PawnMove>().currentTile = this.gameObject;
            UnitMoveController.GetComponent<PawnMove>().currentTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
            datNode.gameObject.SetActive(true);
            if (UnitMoveController.GetComponent<PawnMove>().isMoving)
            {
                GridManager.rescan = true;
            }
        }
    }

    public void MouseDownCall()
    {
        Debug.Log("on mouse down called");
        if (canPlace == false && UnitOnTile != null && pieceSelected == false)
        {
            SoundController.GetComponent<UISoundsScript>().playSelectPiece();
            if (TurnStateMachine.state == TurnStateMachine.State.playerTurn && TurnStateMachine.OnHoverPiece != null && TurnStateMachine.OnHoverPiece.tag == "White")
            {
                SelectPiece();
            }
            else if (TurnStateMachine.state == TurnStateMachine.State.otherTurn && TurnStateMachine.OnHoverPiece != null && TurnStateMachine.OnHoverPiece.tag == "Black")
            {
                SelectPiece();
            }
        }
        else if (canPlace == true && UnitOnTile == null)//these checks result in a piece moving to an empty hex tile
        {
            SoundController.GetComponent<UISoundsScript>().playMovePiece();
            if (UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().Occupied == false && canPlace == true)
            {
                if (UnitMoveController.GetComponent<PawnMove>().SelectedPiece.tag == "Black")
                {
                    //ReplaceBlackPiece(UnitMoveController.GetComponent<PawnMove>().SelectedPiece);
                    SetTarget();
                    if (UnitMoveController.GetComponent<PawnMove>().SelectedPiece.name == "BlackGrunt(Clone)")
                    {
                        PiecePlaceScript.Black01Tile = UnitMoveController.GetComponent<PawnMove>().MoveToTile;
                        Debug.Log("SetBlackPawnTile");
                    }
                    else
                    {
                        PiecePlaceScript.Black02Tile = UnitMoveController.GetComponent<PawnMove>().MoveToTile;
                    }
                    if(GameObject.Find("EnemyAI") == null)
                    {
                        //TurnStateMachine.state = TurnStateMachine.State.playerTurn;
                    }
                }
                else
                {
                    if (GameObject.Find("EnemyTurnController") != null)
                    {
                        Debug.Log("Incorrect");
                        SoundController.GetComponent<UISoundsScript>().playError();
                        pieceSelected = false; 
                        canPlace = false; 
                        if (GameObject.Find("DummyPlayerPlaceHolder") != null)
                        {
                            GameObject.Find("DummyPlayerPlaceHolder").GetComponent<PlayerTutorialControl>().PlayerError = true; 
                        }
                        //GameObject.Find("EnemyTurnController").GetComponent<TutorialEnemyBoardScript>().enemyTurn = 2;
                    }
                    else
                    {
                        //ReplaceWhitePiece(UnitMoveController.GetComponent<PawnMove>().SelectedPiece);
                        SetTarget();
                        if (UnitMoveController.GetComponent<PawnMove>().SelectedPiece.name == "WhitePawn(Clone)")
                        {
                            PiecePlaceScript.White01Tile = UnitMoveController.GetComponent<PawnMove>().MoveToTile;
                        }
                        else
                        {
                            PiecePlaceScript.White02Tile = UnitMoveController.GetComponent<PawnMove>().MoveToTile;
                        }
                        //TurnStateMachine.state = TurnStateMachine.State.otherTurn;
                    }
                }
            }
            UnitMoveController.GetComponent<PawnMove>().isMoving = false;
        }
        else if (canPlace == true && UnitOnTile != null && UnitMoveController.GetComponent<PawnMove>().SelectedPiece.tag != UnitOnTile.tag)
        {
            SoundController.GetComponent<UISoundsScript>().playFight();
            if (UnitMoveController.GetComponent<PawnMove>().SelectedPiece.tag == "White")
            {
                if (UnitOnTile.tag == "Black" && canPlace)
                {
                    if (GameObject.Find("EnemyTurnConroller") != null)
                    {
                        if (UnitOnTile.name == "BlackGrunt(Clone)")
                        {
                            Debug.Log("Fight");
                            UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
                            GridManager.rescan = true;
                            UnitMoveController.GetComponent<PawnMove>().Player02 = UnitMoveController.GetComponent<PawnMove>().SelectedPiece;
                            UnitMoveController.GetComponent<PawnMove>().Player01 = UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile;
                            fighting = true;
                            SetTarget();
                        }
                        else
                        {
                            SoundController.GetComponent<UISoundsScript>().playError();
                        }
                    }
                    else
                    {
                        Debug.Log("Fight");
                        UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
                        GridManager.rescan = true;
                        UnitMoveController.GetComponent<PawnMove>().Player01 = UnitMoveController.GetComponent<PawnMove>().SelectedPiece;
                        UnitMoveController.GetComponent<PawnMove>().Player02 = UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile;
                        fighting = true;
                        SetTarget();
                    }
                }
            }
            else//if selectedPiece tag is black
            {
                if (UnitOnTile.tag == "White" && canPlace)
                {
                    Debug.Log("Fight");
                    UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
                    GridManager.rescan = true;
                    UnitMoveController.GetComponent<PawnMove>().Player01 = UnitMoveController.GetComponent<PawnMove>().SelectedPiece;
                    UnitMoveController.GetComponent<PawnMove>().Player02 = UnitMoveController.GetComponent<PawnMove>().MoveToTile.GetComponent<TileProperties>().UnitOnTile;
                    fighting = true;
                    SetTarget();
                }
            }
        }
        else
        {
            SoundController.GetComponent<UISoundsScript>().playError();
        }
    }
}

