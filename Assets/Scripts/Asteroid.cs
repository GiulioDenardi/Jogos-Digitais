using UnityEngine;
using System.Collections;



public class Asteroid : MonoBehaviour{
	public Rigidbody corpo;
	public Collider colisor;
	
	[Tooltip("O som que tocará quando o asteroide explodir")]
	public AudioClip somDaExplosao;
	
	
	public int danoDaColisao = 10;
	public int velocidade = 10;
	
	
	public void Start(){
		Vector3 direcao = gameObject.transform.forward;
		corpo.AddForce(direcao*velocidade*Time.deltaTime, ForceMode.VelocityChange);
		}
	
	public void OnCollisionEnter(Collision colisao){
		if(colisao.gameObject.CompareTag("Player") ){
			Danificavel jogador = colisao.gameObject.GetComponent<Danificavel>();
      if(jogador){
        atingeJogador();
      }

		}
	}
	
	private void atingeJogador(){
		jogador.RecebeDano(danoDaColisao, TipoDeDano.COLISAO);
		AudioSource.PlayClipAtPoint(somDaExplosao, transform.position);
		Destroy(gameObject);
	}
}
