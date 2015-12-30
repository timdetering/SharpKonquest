using System;
using System.Collections.Generic;
using System.Text;

namespace SharpKonquest.Clases
{
    public class Batalla
    {
        public ResultadoBatalla Resultado;
        public int RestanteAtacante;
        public int RestanteDefensor;

        public enum ResultadoBatalla
        {
            GanaAtacante,
            GanaDefensor,
            Empate
        }
      private static Random generadorAleatorios = new Random();

        public static Batalla SimularBatalla(int NavesFlota, int TecnoFlota, int NavesDestino, int TecnoDestino)
        {
            Batalla res = new Batalla();           

            int PuntosAtacante = NavesFlota * (TecnoFlota + generadorAleatorios.Next(-8, 8));
            int PuntosDefensor = NavesDestino * (TecnoDestino + generadorAleatorios.Next(-8, 8));
            
            double porcentageSimilitud = Math.Round( 100d / Math.Max(PuntosAtacante, PuntosDefensor) * Math.Min(PuntosAtacante, PuntosDefensor));

            if (porcentageSimilitud >= 75)//Empate
            {
                res.Resultado = ResultadoBatalla.Empate;
                int diferenciaPuntos = Math.Abs(PuntosAtacante - PuntosDefensor);
                res.RestanteAtacante = (PuntosAtacante-diferenciaPuntos) / TecnoFlota;
                res.RestanteDefensor = (PuntosDefensor - diferenciaPuntos) / TecnoDestino;
            }
            else if (PuntosDefensor >= PuntosAtacante)
            {
                res.Resultado = ResultadoBatalla.GanaDefensor;
                res.RestanteDefensor = (PuntosDefensor - PuntosAtacante) / TecnoDestino;
            }
            else
            {
                res.Resultado = ResultadoBatalla.GanaAtacante;
                res.RestanteAtacante = (PuntosAtacante - PuntosDefensor) / TecnoFlota;
            }

            return res;
        }
        /*

      private class EstadoNave
        {
            public int Casco;
            public int Escudo;
            public int Ataque;
        }

        private class EstadoFlota
        {
            public EstadoNave[] Naves;
            public int Restantes;
             public int Tecno;
            public int AtaqueOriginal;
        }

         public static Batalla SimularBatalla(int NavesFlota, int TecnoFlota, int NavesDestino, int TecnoDestino)
        {
            System.Random generadorAleatorios = new Random();
            Batalla res = new Batalla();
            EstadoFlota atacantes = InicializarFlota(NavesFlota, TecnoFlota);
            EstadoFlota defensores = InicializarFlota(NavesDestino, TecnoDestino);

            int ronda=0;
            while (true)
            {
                Ronda(atacantes, defensores, generadorAleatorios);
                Ronda(defensores, atacantes, generadorAleatorios);

                FinalRonda(atacantes, generadorAleatorios);
                FinalRonda(defensores, generadorAleatorios);

                if(atacantes.Restantes==0 && defensores.Restantes==0)
                {
                    res.Resultado = ResultadoBatalla.Empate;
                    res.RestanteAtacante = 0;
                    res.RestanteDefensor = 0;
                    break;
                }

                double porcentageSimilitud = 100 / Math.Max(atacantes.Restantes, defensores.Restantes) * Math.Min(atacantes.Restantes, defensores.Restantes);

                if (atacantes.Restantes<=0)//Pierden los atacantes
                {
                    res.Resultado = ResultadoBatalla.GanaDefensor;
                    res.RestanteDefensor = defensores.Restantes;
                    break;
                }
                else if (defensores.Restantes <= 0)//Gana el atacante
                {
                    res.Resultado = ResultadoBatalla.GanaAtacante;
                    res.RestanteAtacante = atacantes.Restantes;
                    break;
                }
                else if (ronda > 10 && porcentageSimilitud > 75)//Empate
                {
                    res.Resultado = ResultadoBatalla.Empate;
                    res.RestanteAtacante = atacantes.Restantes;
                    res.RestanteDefensor = defensores.Restantes;
                    break;
                }

                ronda++;
            }

            return res;
        }

        private static void Ronda(EstadoFlota atacantes, EstadoFlota defensores,System.Random rand)
        {
            foreach (EstadoNave atacante in atacantes.Naves)
            {
                if (defensores.Restantes <= 0)
                    return;

                EstadoNave atacada;
                do
                {
                    atacada = defensores.Naves[rand.Next(0, defensores.Naves.Length)];
                } while (atacada.Casco <= 0);

                if (rand.Next(0, 100) < 5)//Esquivado
                    continue;

                if (atacada.Escudo >= atacante.Ataque)
                {
                    atacada.Escudo -= atacante.Ataque;
                }
                else if (atacada.Escudo > 0)
                {
                    //Quitar el escudo y el resto al casco
                    atacada.Casco -= (atacante.Ataque - atacada.Escudo);
                    atacada.Escudo = 0;
                }
                else
                    atacada.Casco -= atacante.Ataque;

                if (atacada.Casco <= 0)
                    defensores.Restantes--;
            }
        }

        private static void FinalRonda(EstadoFlota flota, System.Random rand)
        {            
            //Ajustar ataque y escudos
            foreach (EstadoNave nave in flota.Naves)
            {
                if (nave.Escudo == 0)
                    nave.Escudo = 10;
                nave.Escudo = (int)Math.Round(Math.Min(50d, (nave.Casco/2) * (rand.Next(80, 120) / 100d) * (flota.Tecno / 100d)));

                if (nave.Ataque == 0)
                    nave.Ataque = 10;
                if (nave.Casco == 100)
                    nave.Ataque = flota.AtaqueOriginal;
                else
                    nave.Ataque = (int)Math.Round(Math.Min(flota.AtaqueOriginal,
                        (nave.Casco / 2) * (rand.Next(80, 120) / 100d) * (flota.Tecno / 100d)));
            }
        }

        private static EstadoFlota InicializarFlota(int naves, int tecno)
        {
            EstadoFlota flota = new EstadoFlota();
            EstadoNave[] res = new EstadoNave[naves];
            for (int contador = 0; contador < naves; contador++)
            {
                res[contador] = new EstadoNave();
                res[contador].Ataque = (int)Math.Round(50 * (tecno / 100d));
                res[contador].Escudo = 50;
                res[contador].Casco = 100;
            }
            flota.AtaqueOriginal = (int)Math.Round(50 * (tecno / 100d));

            flota.Naves = res;
            flota.Restantes = res.Length;
            flota.Tecno = tecno;

            return flota;
        }*/
    }
   public class Flota
    {
        public int Naves;
        public int TecnologiaMilitar;
        public Planeta Destino;
        public Planeta Origen;
        public int RondaLlegada;
        public int RondaSalida;
        public double Distancia;

    }
}
