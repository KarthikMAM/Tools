using System.Windows;
using System.Windows.Controls;

namespace InvertedLetters
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] invertedCharactersLower = { 'z', 'ʎ', 'x', 'ʍ', 'ʌ', 'n', 'ʇ', 's', 'ɹ', 'b', 'd', 'o', 'u', 'ɯ', 'l', 'ʞ', 'ɾ', 'ᴉ', 'ɥ', 'ƃ', 'ɟ', 'ǝ', 'p', 'ɔ', 'q', 'ɐ' };
        char[] invertedCharactersUpper = { 'Z', '⅄', 'X', 'M', 'Λ', '∩', '┴', 'S', 'ɹ', 'Q', 'Ԁ', 'O', 'N', 'W', '˥', 'ʞ', 'ſ', 'I', 'H', 'פ', 'Ⅎ', 'Ǝ', 'p', 'Ɔ', 'q', '∀' };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            //97 - 'a'
            //65  - 'A'
            //"zʎxʍʌnʇsɹbdouɯlʞɾᴉɥƃɟǝpɔqɐZ⅄XMΛ∩┴SɹQԀONW˥ʞſIH פℲƎpƆq∀"
            Result.Text = "";
            foreach(char letter in Input.Text)
            {
                if(letter >= 'a' && letter <= 'z')
                {
                    Result.Text = invertedCharactersLower[25 - (letter - 'a')] + Result.Text; 
                }
                else if(letter >= 'A' && letter <= 'Z')
                {
                    Result.Text = invertedCharactersUpper[25 - (letter - 'A')] + Result.Text;
                }
                else
                {
                    Result.Text = letter + Result.Text;
                }
            }
        }

        private void Input_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Clear();
        }
    }
}
