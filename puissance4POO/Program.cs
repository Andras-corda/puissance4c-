namespace puissance4POO
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continuerJeu = true;

            while (continuerJeu)
            {
                JeuPuissance4 jeu = new JeuPuissance4();
                jeu.Jouer();

                Console.Write("Voulez-vous rejouer ? (Oui/Non) : ");
                string reponse = Console.ReadLine().Trim();

                if (!reponse.Equals("Oui", StringComparison.OrdinalIgnoreCase))
                {
                    continuerJeu = false;
                }
            }
        }

        public class JeuPuissance4
        {
            private const int Lignes = 6;
            private const int Colonnes = 7;
            private char[,] plateau;
            private char joueurCourant;

            public JeuPuissance4()
            {
                plateau = new char[Lignes, Colonnes];
                joueurCourant = 'X';
                InitialiserPlateau();
            }

            public void Jouer()
            {
                bool estPartieTerminee = false;

                while (!estPartieTerminee)
                {
                    AfficherPlateau();
                    int colonne = ObtenirCoupValide();
                    int ligne = PlacerJeton(colonne);

                    if (VerifierVictoire(ligne, colonne))
                    {
                        AfficherPlateau();
                        Console.WriteLine("Le joueur " + joueurCourant + " gagne !");
                        estPartieTerminee = true;
                    }
                    else if (PlateauPlein())
                    {
                        AfficherPlateau();
                        Console.WriteLine("Match nul !");
                        estPartieTerminee = true;
                    }
                    else
                    {
                        joueurCourant = (joueurCourant == 'X') ? 'O' : 'X';
                    }
                }
            }

            private void InitialiserPlateau()
            {
                for (int ligne = 0; ligne < Lignes; ligne++)
                {
                    for (int colonne = 0; colonne < Colonnes; colonne++)
                    {
                        plateau[ligne, colonne] = ' ';
                    }
                }
            }

            private void AfficherPlateau()
            {
                Console.Clear();
                for (int ligne = 0; ligne < Lignes; ligne++)
                {
                    for (int colonne = 0; colonne < Colonnes; colonne++)
                    {
                        char jeton = plateau[ligne, colonne];
                        Console.Write("|");
                        if (jeton == 'O')
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(" " + jeton + " ");
                            Console.ResetColor();
                        }
                        else if (jeton == 'X')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" " + jeton + " ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                    }
                    Console.WriteLine("|");
                }
                Console.WriteLine("  1   2   3   4   5   6   7");
                Console.WriteLine("Joueur actuel : " + joueurCourant);
            }

            private int ObtenirCoupValide()
            {
                int colonne;
                while (true)
                {
                    Console.Write("Joueur " + joueurCourant + ", entrez la colonne (1-7) : ");
                    if (int.TryParse(Console.ReadLine(), out colonne) && colonne >= 1 && colonne <= 7)
                    {
                        colonne--;
                        if (plateau[0, colonne] == ' ')
                        {
                            return colonne;
                        }
                        else
                        {
                            Console.WriteLine("La colonne est pleine. Réessayez.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Colonne invalide. Réessayez.");
                    }
                }
            }

            private int PlacerJeton(int colonne)
            {
                for (int ligne = Lignes - 1; ligne >= 0; ligne--)
                {
                    if (plateau[ligne, colonne] == ' ')
                    {
                        plateau[ligne, colonne] = joueurCourant;
                        return ligne;
                    }
                }
                return -1;
            }

            private bool VerifierVictoire(int ligne, int colonne)
            {
                char jeton = plateau[ligne, colonne];

                int jetonsConsecutifs = 1;
                for (int c = colonne + 1; c < Colonnes && plateau[ligne, c] == jeton; c++)
                {
                    jetonsConsecutifs++;
                }
                for (int c = colonne - 1; c >= 0 && plateau[ligne, c] == jeton; c--)
                {
                    jetonsConsecutifs++;
                }
                if (jetonsConsecutifs >= 4)
                {
                    return true;
                }

                jetonsConsecutifs = 1;
                for (int l = ligne + 1; l < Lignes && plateau[l, colonne] == jeton; l++)
                {
                    jetonsConsecutifs++;
                }
                if (jetonsConsecutifs >= 4)
                {
                    return true;
                }

                jetonsConsecutifs = 1;
                for (int l = ligne + 1, c = colonne + 1; l < Lignes && c < Colonnes && plateau[l, c] == jeton; l++, c++)
                {
                    jetonsConsecutifs++;
                }
                for (int l = ligne - 1, c = colonne - 1; l >= 0 && c >= 0 && plateau[l, c] == jeton; l--, c--)
                {
                    jetonsConsecutifs++;
                }
                if (jetonsConsecutifs >= 4)
                {
                    return true;
                }

                jetonsConsecutifs = 1;
                for (int l = ligne + 1, c = colonne - 1; l < Lignes && c >= 0 && plateau[l, c] == jeton; l++, c--)
                {
                    jetonsConsecutifs++;
                }
                for (int l = ligne - 1, c = colonne + 1; l >= 0 && c < Colonnes && plateau[l, c] == jeton; l--, c++)
                {
                    jetonsConsecutifs++;
                }
                return jetonsConsecutifs >= 4;
            }

            private bool PlateauPlein()
            {
                for (int colonne = 0; colonne < Colonnes; colonne++)
                {
                    if (plateau[0, colonne] == ' ')
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}