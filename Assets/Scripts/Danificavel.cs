using UnityEngine;
using System.Collections;


public Enum TipoDeDano {LASER, COLISAO, EXPLOSAO};

public class Danificavel : MonoBehaviour{
	
	public int MAX_HP;
	public int pontosDeVida;
	public float resistenciaExplosoes;
	public float resistenciaColisoes;
	public float resistenciaLasers;
	
	public HPBar barraDeHP
	
	public List<DanificavelListener> ouvintes;
	
	public void Start(){
		ouvintes = new List<DanificavelListener>();
	}
	
  
  // FUNCOES PUBLICAS: RecebeDano(valor, tipoDeDano), RecebeCura(valor); 
  //                  AddListener(DanificavelListener), RemoveListener(DanificavelListener);
	
	public void RecebeDano(int valor, TipoDeDano damageType){
		int dano = 0;
		switch(damageType){
			case TipoDeDano.EXPLOSAO:
				dano = danoReduzido(valor,resistenciaExplosoes);
				break;
			case TipoDeDano.COLISAO:
				dano = danoReduzido(valor, resistenciaColisoes);
				break;
			case TipoDeDano.LASER:
				dano = danoReduzido(valor, resistenciaLasers);
				break;
		}
		diminuiHP(dano);
		danificavelRecebeuDano(dano);
		}
	}
	
	public void RecebeCura(int valor){
		int aCurar = valor;
		if(pontosDeVida+aCurar > MAX_HP){
			aCurar = MAX_HP-pontosDeVida;
		}	
		pontosDeVida += aCurar;
		atualizaBarraDeHP();
		danificavelRecebeuCura(valor);
		
		}
	
  public void AddListener(DanificavelListener listener){
    this.ouvintes.Add(listener);
  }
  
  public void RemoveListener(DanificavelListener listener){
    this.ouvintes.Remove(listener);
  
  }
  
  
  // ---- FUNCOES PRIVADAS ---- FUNCOES PRIVADAS ---- FUNCOES PRIVADAS ---- FUNCOES PRIVADAS 
  // ---- FUNCOES PRIVADAS ---- FUNCOES PRIVADAS ---- FUNCOES PRIVADAS ---- FUNCOES PRIVADAS 
  
	private int danoReduzido(int dano, int resistencia){
		int reduzido = 0;
		if(dano>resistencia)
			reduzido = dano-resistencia;
		return reduzido
	}
	
	
	
	private void diminuiHP(int valor){
		if(valor >= pontosDeVida){
			pontosDeVida = 0;
			DanificavelMorreu();
		} else{
			pontosDeVida -= valor;
		}
		
		atualizaBarraDeHP();
	}
	
	private void aumentaHP(int valor){
		// TODO
	}
	
	
	private void atualizaBarraDeHP(){
		float fracaoDeVidaRestante = pontosDeVida/MAX_HP;
		barraDeHP.value = fracaoDeVidaRestante;
	}
	
	
	public void DanificavelDestruido(){
		foreach(DanificavelListener dl in ouvintes){
			dl.OnDanificavelDestruido();
		}
	}
	
	private void danificavelRecebeuDano(int valor){
		foreach(DanificavelListener dl in ouvintes){
			dl.OnDanificavelRecebeuDano(valor);
		}
	}
	
	private void danificavelRecebeuCura(int valor){
		foreach(DanificavelListener dl in ouvintes){
			dl.OnDanificavelRecebeuCura(valor);
		}
	}
}
