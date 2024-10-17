using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTRAINEMENT_PERSO_ALGO
{
  internal class Program
  {
    static Random Generateur = new Random();

    static void Main(string[] args)
    {
      int[,] Tableau = new int[20, 20];
      Init_Tableau_Valeurs_Aleatoires(Tableau);
      //Init_Tableau_Valeurs_Binaires(Tableau, 50);
      //Affiche_Tableau(Tableau);
      Moyenne_Zone_Aleatoire(Tableau);
      Console.WriteLine();
      Console.WriteLine("===========================");
      Affiche_Moyenne_Tableau(Tableau);
      Console.WriteLine("===========================");
      Affiche_Plus_Grosse_Moyenne_Lignes(Tableau);

      //Affiche_Valeur_Totale_Par_Ligne(Tableau);
    }

    private static void Affiche_Plus_Grosse_Moyenne_Lignes(int[,] P_Tableau)
    {
      int Numero_Ligne = 0;
      int Total_Ligne = 0;
      int Moyenne;
      int Valeur_Max = int.MinValue;
      for (int Y = 0; Y < P_Tableau.GetLength(0); Y++) {
        for (int X = 0; X < P_Tableau.GetLength(1); X++) {
          Total_Ligne += P_Tableau[Y, X];
        }
        Moyenne = Total_Ligne / P_Tableau.GetLength(1);
        if (Moyenne > Valeur_Max) {
          Valeur_Max = Moyenne;
          Numero_Ligne = Y;
        }
        Total_Ligne = 0;
        Moyenne = 0;
      }
            Console.WriteLine($"C'est la ligne {Numero_Ligne} avec {Valeur_Max}");
        }

    private static void Affiche_Valeur_Totale_Par_Ligne(int[,] P_Tableau)
    {
      int Total_Ligne;
      for (int Index_Y = 0; Index_Y < P_Tableau.GetLength(0); Index_Y++) {
        Total_Ligne = 0;
        for (int Index_X = 0; Index_X < P_Tableau.GetLength(1); Index_X++) {
          Console.Write($"{P_Tableau[Index_Y, Index_X]} ", 3);
          Total_Ligne += P_Tableau[Index_Y, Index_X];
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{Total_Ligne}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
      }
    }

    static void Affiche_Tableau(int[,] P_Tableau)
    {
      for (int Index_Y = 0; Index_Y < P_Tableau.GetLength(0); Index_Y++) {
        Console.WriteLine();
        for (int Index_X = 0; Index_X < P_Tableau.GetLength(1); Index_X++) {
          Console.Write($"{P_Tableau[Index_Y, Index_X]} ", 3);
        }
      }
    }

    static void Init_Tableau_Valeurs_Aleatoires(int[,] P_Tableau)
    {
      for (int Index_Y = 0; Index_Y < P_Tableau.GetLength(0); Index_Y++) {
        for (int Index_X = 0; Index_X < P_Tableau.GetLength(1); Index_X++) {
          P_Tableau[Index_Y, Index_X] = Generateur.Next() % 101;
        }
      }
    }

    static void Init_Tableau_Valeurs_Binaires(int[,] P_Tableau, int P_Quantite_1)
    {
      int Compteur = 0;
      while (Compteur < P_Quantite_1) {
        for (int Index_Y = 0; Index_Y < P_Tableau.GetLength(0); Index_Y++) {
          for (int Index_X = 0; Index_X < P_Tableau.GetLength(1); Index_X++) {
            if (P_Tableau[Index_Y, Index_X] != 1 && Compteur < P_Quantite_1) {
              if (Generateur.Next() % 101 <= 10) {
                P_Tableau[Index_Y, Index_X] = 1;
                Compteur++;
              }
            }
          }
        }
      }
    }

    static void Affiche_Moyenne_Tableau(int[,] P_Tableau)
    {
      int Nb_Valeurs = 0;
      int Total = 0;
      double Moyenne;
      for (int Index_Y = 0; Index_Y < P_Tableau.GetLength(0); Index_Y++) {
        for (int Index_X = 0; Index_X < P_Tableau.GetLength(1); Index_X++) {
          Total += P_Tableau[Index_Y, Index_X];
          Nb_Valeurs++;
        }
      }
      Moyenne = Total / Nb_Valeurs;
      Console.WriteLine($"Moyenne : {Moyenne} / {Total}");

    }

    static void Moyenne_Zone_Aleatoire(int[,] P_Tableau)
    {
      int Rand_Index_Y = Generateur.Next() % P_Tableau.GetLength(0) + 1;
      int Rand_Index_X = Generateur.Next() % P_Tableau.GetLength(1) + 1;

      if (Rand_Index_Y == 0) Rand_Index_X++;
      if (Rand_Index_X == 0) Rand_Index_X++;
      if (Rand_Index_Y == 20) Rand_Index_Y--;
      if (Rand_Index_X == 20) Rand_Index_X--;

      int Nb_Valeurs = 0;
      int Total = 0;
      double Moyenne;
      for (int Compt_Y = -1; Compt_Y < 1; Compt_Y++) {
        for (int Compt_X = -1; Compt_X < 1; Compt_X++) {
          Total += P_Tableau[Rand_Index_Y - Compt_Y, Rand_Index_X - Compt_X];
          Nb_Valeurs++;
        }
      }
      Affiche_Tableau_Zone_Moyenne(P_Tableau, Rand_Index_Y, Rand_Index_X);
      Console.WriteLine();
      Console.WriteLine("===================================");
      Moyenne = Total / Nb_Valeurs;
      Console.WriteLine($"Moyenne petit tableau: {Moyenne} / {Total}");
      Console.WriteLine("===================================");

    }

    private static void Affiche_Tableau_Zone_Moyenne(int[,] P_Tableau, int P_Index_Y, int P_Index_X)
    {
      int Y_Debut = P_Index_Y - 1;
      int Y_Fin = P_Index_Y + 1;
      int X_Debut = P_Index_X - 1;
      int X_Fin = P_Index_X + 1;


      for (int Y = 0; Y < P_Tableau.GetLength(0); Y++) {
        Console.WriteLine();
        for (int X = 0; X < P_Tableau.GetLength(1); X++) {
          if (Y >= Y_Debut && X >= X_Debut && Y <= Y_Fin && X <= X_Fin) {
            if (Y == P_Index_Y && X == P_Index_X) {
              Console.ForegroundColor = ConsoleColor.Green;
              Console.Write($"{P_Tableau[Y, X]} ", 3);
              Console.ForegroundColor = ConsoleColor.White;
            } else {
              Console.ForegroundColor = ConsoleColor.Red;
              Console.Write($"{P_Tableau[Y, X]} ", 3);
              Console.ForegroundColor = ConsoleColor.White;
            }
          } else {
            Console.Write($"{P_Tableau[Y, X]} ", 3);
          }
        }
      }
    }
  }
}
