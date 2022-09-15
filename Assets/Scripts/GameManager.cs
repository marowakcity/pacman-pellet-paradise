using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;


    public int ghostMultiplier{ get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set;}

    


    private void Start() //called at start of game
    {
        NewGame();
    }

    private void Update() // called every frame
    {
        if(this.lives <= 0 && Input.anyKeyDown){
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();

    }

    private void NewRound()
    {
        foreach(Transform pellet in this.pellets){
            pellet.gameObject.SetActive(true);
        }

        ResetState();

    }

    private void ResetState()
    {
        ResetMultiplier();
        for(int i=0; i < this.ghosts.Length; i++){
            this.ghosts[i].ResetState();
        }
        this.pacman.ResetState();

    }


    private void GameOver()
    {
        //TO DO: UI 
        for(int i=0; i < this.ghosts.Length; i++){
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
    }


    public void GhostEaten(Ghost ghost)
    {
        SetScore(this.score + (ghost.points * ghostMultiplier));
        this.ghostMultiplier++;
    }

    public void PlayerDeath()
    {
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if(this.lives > 0){
            Invoke(nameof(ResetState), 3.0f);
        }
        else{
            GameOver();
        }
        
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);
        if(!HasRemainingPellets()){
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }


    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for(int i = 0; i < this.ghosts.Length; i++){
            this.ghosts[i].frightened.Enable(pellet.duration);
        }


        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetMultiplier), pellet.duration);
        

    }

    private bool HasRemainingPellets()
    {
        foreach(Transform pellet in this.pellets)
        {
            if(pellet.gameObject.activeSelf){
                return true;
            }
        }
        return false;
    }

    private void ResetMultiplier(){
        ghostMultiplier = 1;
    }



}
