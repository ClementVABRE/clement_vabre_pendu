using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging; 

namespace clement_vabre_penduv2
{
    public partial class MainWindow : Window
    {
        public class PenduGame // Créez une classe PenduGame
        {
            public string[] mots = { "PROGRAMMATION", "INFORMATIQUE", "PENDU", "FENETRE","CIEL","POISSON","DISCORD" }; // Ajoutez des mots
            public string MotSecret; // Ajoutez un mot secret
            public string MotMasque; // Ajoutez un mot masqué
            public int vies = 6;    // Ajoutez un nombre de vies
        }

         PenduGame penduGame; // Créez une instance de PenduGame

        public MainWindow()
        {
            InitializeComponent(); 
            penduGame = new PenduGame(); // Initialisez l'instance de PenduGame
            NouvellePartie(); // Initialise une nouvelle partie
            Reset();
        }

        public void NouvellePartie() // Créez une méthode pour initialiser une nouvelle partie
        {

            Random random = new Random(); // Créez une instance de Random
            int MotAleatoire = random.Next(penduGame.mots.Length);  
            penduGame.MotSecret = penduGame.mots[MotAleatoire].ToUpper();  
            penduGame.MotMasque = new string('*', penduGame.MotSecret.Length); 
            TB_Display.Text = penduGame.MotMasque; 
            penduGame.vies = 6;    
            UpdateVies();
          
        }

        public void Reset() //création d'une méthode pour réinitialiser les boutons
        {
            BTN_A.IsEnabled = true;
            BTN_B.IsEnabled = true;
            BTN_C.IsEnabled = true;
            BTN_D.IsEnabled = true;
            BTN_E.IsEnabled = true;
            BTN_F.IsEnabled = true;
            BTN_G.IsEnabled = true;
            BTN_H.IsEnabled = true;
            BTN_I.IsEnabled = true;
            BTN_J.IsEnabled = true;
            BTN_K.IsEnabled = true;
            BTN_L.IsEnabled = true;
            BTN_M.IsEnabled = true;
            BTN_N.IsEnabled = true;
            BTN_O.IsEnabled = true;
            BTN_P.IsEnabled = true;
            BTN_Q.IsEnabled = true;
            BTN_R.IsEnabled = true;
            BTN_S.IsEnabled = true;
            BTN_T.IsEnabled = true;
            BTN_U.IsEnabled = true;
            BTN_V.IsEnabled = true;
            BTN_W.IsEnabled = true;
            BTN_X.IsEnabled = true;
            BTN_Y.IsEnabled = true;
            BTN_Z.IsEnabled = true;
        }

        private void BTN_Click(object sender, RoutedEventArgs e) // Créez une méthode pour gérer les clics sur les boutons
        {
            if (penduGame.vies <= 0) // Si le nombre de vies est inférieur ou égal à 0, ne faites rien
            {
                return; 
            }

            Button bouton = (Button)sender; // Récupérez le bouton cliqué
            char lettre = Convert.ToChar(bouton.Content); // Récupérez la lettre du bouton cliqué

            if (penduGame.MotSecret.Contains(lettre)) // Si le mot secret contient la lettre
            {
                for (int MotAleatoire = 0; MotAleatoire < penduGame.MotSecret.Length; MotAleatoire++) // Parcourez le mot secret
                {
                    if (penduGame.MotSecret[MotAleatoire] == lettre && penduGame.MotMasque[MotAleatoire] == '*') // Si la lettre est dans le mot secret et que la lettre n'est pas déjà affichée
                    {
                        penduGame.MotMasque = penduGame.MotMasque.Remove(MotAleatoire, 1).Insert(MotAleatoire, lettre.ToString()); // Affichez la lettre
                    }
                }
                TB_Display.Text = penduGame.MotMasque; // Mettez à jour le mot masqué
            }
            else
            {
                penduGame.vies--; // Retirez une vie
                UpdateVies(); // Mettez à jour les vies
            }
            ((Button)sender).IsEnabled = false;

            if (penduGame.MotMasque == penduGame.MotSecret) // Si le mot masqué est égal au mot secret
            {
                
                string musicPath = "ressource/son/victoryff.mp3"; // Chemin du son
                MediaPlayer mediaPlayer = new MediaPlayer(); // Créez une instance de MediaPlayer
                mediaPlayer.Open(new Uri(musicPath, UriKind.Relative)); // Ouvrez le fichier audio
                mediaPlayer.Play(); // Jouez le son
                
                
                MessageBox.Show("Félicitations, vous avez gagné !"); // Affichez un message de victoire
              
               
                NouvellePartie();
                Reset();
                



            }
            else if (penduGame.vies == 0) // Si le nombre de vies est égal à 0
            {
                string musicPath = "ressource/son/loose.mp3"; // Chemin du son
                MediaPlayer mediaPlayer = new MediaPlayer(); // Créez une instance de MediaPlayer
                mediaPlayer.Open(new Uri(musicPath, UriKind.Relative)); // Ouvrez le fichier audio
                mediaPlayer.Play(); // Jouez le son
                
                MessageBox.Show("Dommage, vous avez perdu. Le mot était : " + penduGame.MotSecret); // Affichez un message de défaite
                
               
                        
                
                NouvellePartie();
                Reset();


            }
        }

        private void UpdateVies()
        {
            string imagePath = "ressource/image/" + penduGame.vies + ".png"; // Chemin de l'image

            pendu.Source = new ImageSourceConverter().ConvertFromString(imagePath) as ImageSource; // Met à jour l'image
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            NouvellePartie();
           Reset();
        }
    }
}
