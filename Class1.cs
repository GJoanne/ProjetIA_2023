using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA2022
{
    public class Node2 : GenericNode 
    {
        public int x;
        public int y;

        // Méthodes abstrates, donc à surcharger obligatoirement avec override dans une classe fille
        public override bool IsEqual(GenericNode N2)
        {
            Node2 N2bis = (Node2)N2;

            return (x == N2bis.x) && (y == N2bis.y);
        }

        public override double GetArcCost(GenericNode N2)
        {
            // Ici, N2 ne peut être qu'1 des 8 voisins, inutile de le vérifier
            // Par contre, selon la direction d'avancement, le coût est différent
            Node2 N2bis = (Node2)N2;     // On "cast" car on sait que c'est un objet de la classe Node2.
            // Le vent est orienté verticalement du haut vers le bas de l'écran, donc selon des y croissants
            // Premier cas, on est sur la même ligne horizontale, devant ou derrière, vent à 90°
            // => 30 secondes par case
            if ((N2bis.y == y) && (N2bis.x != x))
                return 30;
            else // Deuxième cas, la case visée est juste au-dessus, on est contre le vent, on met un coût
                 // de 10000, car ce n'est pas impossible, mais il faut y aller à la rame ...
                 if ((N2bis.x == x) && (N2bis.y < y))
                return 10000;
            else // Troisième cas, la case visée est en diagonale au-dessus, donc un peu contre le vent mais
                 // faisable, on met 50s par case
                if ((N2bis.y < y) && (N2bis.x != x))
                return 50;
            else // Quatrième cas, la case visée est juste en dessous => vent arrière, 20 secondes par case 
                 if ((N2bis.x == x) && (N2bis.y > y))
                return 21;
            else // Cinquième cas, la case visée est vent 3/4 arrière, optimal pour le bateau => 20s par case
                if ((N2bis.y > y) && (N2bis.x != x))
                return 20;
            else // Normalement on ne devrait pas avoir d'autre cas ...
                return -1000000;
        }

        public override bool EndState()
        {
            return (x == Form1.xfinal) && (y == Form1.yfinal);
        }

        public override List<GenericNode> GetListSucc()
        {
            List<GenericNode> lsucc = new List<GenericNode>();

            for (int dx=-1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if ((x + dx >= 0) && (x + dx < Form1.nbcolonnes)
                            && (y + dy >= 0) && (y + dy < Form1.nblignes) && ((dx != 0) || (dy != 0)))
                        if (Form1.matrice[x + dx, y + dy] > -1)
                        {
                            Node2 newnode2 = new Node2();
                            newnode2.x = x + dx;
                            newnode2.y = y + dy;
                            lsucc.Add(newnode2);
                        }
                }

            }
            return lsucc;
        }


        public override double CalculeHCost()
        {
            // variables disponibles
            // x et y du noeud examiné
            // Form1.xinitial Form1.yinitial   coordonnées du point de départ du bateai
            // Form1.xfinal   Form1.yfinal     coordonnées du point d'arrivée du bateau
            // matrice[x,y] indique le type de case  ( 0 si rien, -2 si obstacle)

            int deltaX = Math.Abs(x - Form1.xfinal);
            int deltaY = Math.Abs(y - Form1.yfinal);

            // Coût de base
            double heuristicCost = deltaX + deltaY;

            // Ajustements en fonction de la direction et de la possibilité de déplacement diagonal
            if (Form1.xinitial < Form1.xfinal)
            {
               // .... Coder en fonction de la direction de départ, de là où il doit aller, faire des rapports petits à petits
            }


            ///Tout est a revoir
            if (y < Form1.yfinal)
            {
                // Déplacement vers le bas, favorisé
                heuristicCost -= 10;
            }
            else if (y > Form1.yfinal)
            {
                // Déplacement vers le haut, défavorisé ou rendu impossible au centre
                if (x == Form1.xfinal && y > Form1.yfinal)
                {
                    // Au centre, rendu impossible vers le haut
                    heuristicCost = double.PositiveInfinity;
                }
                else
                {
                    heuristicCost += 7;
                }
            }

            // Ajustements pour les déplacements diagonaux
            if (Math.Abs(x - Form1.xfinal) == Math.Abs(y - Form1.yfinal))
            {
                // Déplacement diagonal, ajustez le coût en conséquence
                heuristicCost -= 5;
            }

            // Ajustements pour la vitesse du voilier (hypothétiques)
            // ...

            return heuristicCost;

        }

        public override string ToString()
        {
            return Convert.ToString(x)+","+ Convert.ToString(y);
        }
    }
}
