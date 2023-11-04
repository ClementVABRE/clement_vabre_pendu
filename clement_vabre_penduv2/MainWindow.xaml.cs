using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Text;

namespace clement_vabre_penduv2
{
    public partial class MainWindow : Window
    {
        public class PenduGame // Créez une classe PenduGame
        {

            public string[] mots; // Ajoutez des mots
            public string MotSecret; // Ajoutez un mot secret
            public string MotMasque; // Ajoutez un mot masqué
            public int vies = 6;    // Ajoutez un nombre de vies
        }

        PenduGame penduGame; // Créez une instance de PenduGame

        public MainWindow()
        {
            InitializeComponent();
            penduGame = new PenduGame(); // Initialisez l'instance de PenduGame
            ChargerMotsDepuisFichier("ressource/mots.txt");
            NouvellePartie(); // Initialise une nouvelle partie
            Reset();
        }

        public void ChargerMotsDepuisFichier(string cheminFichier)
        {
             penduGame.mots = File.ReadAllLines(cheminFichier);
        }


        public void NouvellePartie() // Créez une méthode pour initialiser une nouvelle partie
        {

            Random random = new Random(); // Créez une instance de Random
            int MotAleatoire = random.Next(penduGame.mots.Length); // Générez un nombre aléatoire entre 0 et le nombre de mots
            penduGame.MotSecret = penduGame.mots[MotAleatoire].ToUpper(); // Récupérez le mot secret
            penduGame.MotMasque = new string('*', penduGame.MotSecret.Length); // Créez un mot masqué
            TB_Display.Text = penduGame.MotMasque; // Affichez le mot masqué
            penduGame.vies = 6;  // Réinitialisez le nombre de vies
            UpdateVies(); // Mettez à jour les vies 

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
                string musichemin = "ressource/son/loose.mp3"; // Chemin du son
                MediaPlayer mediaPlayer = new MediaPlayer(); // Créez une instance de MediaPlayer
                mediaPlayer.Open(new Uri(musichemin, UriKind.Relative)); // Ouvrez le fichier audio
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

        private void RestartButton_Click(object sender, RoutedEventArgs e) // Créez une méthode pour gérer le clic sur le bouton "Recommencer"
        {
            NouvellePartie(); // Initialisez une nouvelle partie
            Reset(); // Réinitialisez les boutons
        }

        //cree un bouton pour ouvirr une aide
        private void BTN_regle_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bienvenue dans le jeu du pendu ! \n\nLe but du jeu est de trouver le mot secret en devinant les lettres qui le composent. \n\nPour cela, cliquez sur les lettres que vous pensez être dans le mot secret. \n\nSi vous trouvez toutes les lettres du mot secret, vous gagnez la partie. \n\nSi vous faites 6 erreurs, vous perdez la partie. \n\nBonne chance !", "Aide");
        }

        
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool isPlaying = false;
        private void BTN_Son_Click(object sender, RoutedEventArgs e)
        {
        
           
            string source = "ressource/son/mariosong.mp3"; // Chemin du son

            if (isPlaying)
            {
                mediaPlayer.Stop(); // Arrêtez la lecture si le son est en cours de lecture
                isPlaying = false; // Mettez à jour le booléen
                BTN_Son.Background = Brushes.Green; // Mettez à jour le bouton
            }
            else
            {
                mediaPlayer.Open(new Uri(source, UriKind.Relative)); // Ouvrez le fichier audio
                mediaPlayer.Play(); // Jouez le son
                isPlaying = true; // Mettez à jour le booléen
                BTN_Son.Background = Brushes.Red; // Mettez à jour le bouton
               
            }
        }
    }
}