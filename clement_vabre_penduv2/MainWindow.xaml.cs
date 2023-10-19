using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace clement_vabre_penduv2
{
    public partial class MainWindow : Window
    {
        // Liste de mots pour le jeu
        private string[] mots = { "PROGRAMMATION", "INFORMATIQUE", "PENDU", "FENETRE" };
        private string motSecret; // Le mot à deviner
        private string motMasque; // Le mot masqué avec des astérisques
        private int vies = 6; // Le nombre de vies du joueur

        public MainWindow()
        {
            InitializeComponent();
            NouvellePartie(); // Initialise une nouvelle partie
        }

        private void NouvellePartie()
        {
            Random random = new Random();
            int Motaleatoire = random.Next(mots.Length); // Sélectionne un mot aléatoire
            motSecret = mots[Motaleatoire].ToUpper(); // Met le mot en majuscules (pour faciliter la comparaison)
            motMasque = new string('*', motSecret.Length); // Crée un mot masqué
            TB_Display.Text = motMasque; // Affiche le mot masqué
            vies = 6; // Réinitialise le nombre de vies
            UpdateVies(); // Met à jour l'image du pendu

        }

        private void BTN_Click(object sender, RoutedEventArgs e) // Quand on clique sur une lettre
        {
            if (vies <= 0) // Si le joueur n'a plus de vies, ne faites rien
            {
                return; // Si le joueur n'a plus de vies, ne faites rien
            }

            Button bouton = (Button)sender;
            char lettre = Convert.ToChar(bouton.Content);

            if (motSecret.Contains(lettre)) // Si le mot contient la lettre
            {
                for (int MotAleatoire = 0; MotAleatoire < motSecret.Length; MotAleatoire++) // Parcours le mot
                {
                    if (motSecret[MotAleatoire] == lettre && motMasque[MotAleatoire] == '*') // Si la lettre est dans le mot et n'est pas déjà trouvée
                    {
                        motMasque = motMasque.Remove(MotAleatoire, 1).Insert(MotAleatoire, lettre.ToString()); // Remplace l'astérisque par la lettre
                    }
                }
                TB_Display.Text = motMasque; // Met à jour l'affichage
            }
            else
            {
                vies--; // Décrémente le nombre de vies
                UpdateVies(); // Met à jour l'image du pendu
            }

            

            if (motMasque == motSecret)
            {
                MessageBox.Show("Félicitations, vous avez gagné !");
                NouvellePartie(); // Démarre une nouvelle partie
            }
            else if (vies == 0)
            {
                MessageBox.Show("Dommage, vous avez perdu. Le mot était : " + motSecret);
                NouvellePartie(); // Démarre une nouvelle partie
            }
        }


        private void UpdateVies()   // Met à jour l'image du pendu
        {
            string imagePath = "ressource/image/" + vies + ".png"; // Chemin de l'image

            pendu.Source = new ImageSourceConverter().ConvertFromString(imagePath) as ImageSource; // Met à jour l'image
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e) // Bouton pour redémarrer une partie
        {
            NouvellePartie(); // Redémarre une nouvelle partie
            IsEnabled = true; // Réactive les boutons
            
        }





    }
}

