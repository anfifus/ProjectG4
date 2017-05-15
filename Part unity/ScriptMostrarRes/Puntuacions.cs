using System;

namespace AssemblyCSharp
{
	public class Puntuacions
	{
		private string IdJugador;
		private string NomUsuari;
		private string Contrasenya;
		private string ResultatMarcador;
		/// <summary>
		/// Clase que serveix per guardar les informacions de cada jugador amb la seva puntuacio
		/// </summary>
		/// <param name="IdJugador">Identifier jugador.</param>
		/// <param name="NomUsuari">Nom usuari.</param>
		/// <param name="Contrasenya">Contrasenya.</param>
		/// <param name="ResultatMarcador">Resultat marcador.</param>
		public Puntuacions (string IdJugador,string NomUsuari,string Contrasenya,string ResultatMarcador)
		{
			this.IdJugador = IdJugador;
			this.NomUsuari = NomUsuari;
			this.Contrasenya = Contrasenya;
			this.ResultatMarcador = ResultatMarcador;
		}
		//Aconseguim les dades si les volem recuperar d'una a una
		public string getIdJugador()
		{
			return this.IdJugador;
		}
		public string getNomUsuari()
		{
			return this.NomUsuari;
		}
		public string getContrasenya()
		{
			return this.Contrasenya;
		}
		public string getResultatMarcador()
		{
			return this.ResultatMarcador;
		}
		//-----------------------------------
		//Permet inserir  les dades d'una a una
		public void setIdJugador(string IdJugador)
		{
			this.IdJugador = IdJugador;
		}
		public void setNomUsuari(string NomUsuari)
		{
			this.NomUsuari = NomUsuari;
		}
		public void setContrasenya(string Contrasenya)
		{
			this.Contrasenya = Contrasenya;
		}
		public void setResultatMarcador(string ResultatMarcador)
		{
			this.ResultatMarcador = ResultatMarcador;
		}
		//---------------------------------------
	}
}

