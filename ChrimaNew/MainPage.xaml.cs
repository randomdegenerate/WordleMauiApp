using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace ChrimaNew
{
    public partial class MainPage : ContentPage
    {
        public const int ROW = 5, COL = 6;
        public String[] validWords = ["water", "fires", "magic", "fucks", "wafer", "crane", "paper"];
        public string keyWord;
        public static int counter = 0;
        public MainPage()
        {
            InitializeComponent();
            CreateTheGrid();

            Random rand = new Random();
            keyWord = validWords[rand.Next(validWords.Length - 1)];//random number from 0 - 5
        }

        private void AttemptGridUpdate(String wrd)
        {
            int[] attemptColors = WordCheck(wrd);

            int tempCount = 0;
            for (int i = 0; i < attemptColors.Length; i++) {
                Border b1 = new Border() { BackgroundColor = Colors.DarkGrey };
                Border b2 = new Border() { BackgroundColor = Colors.Orange };
                Border b3 = new Border() { BackgroundColor = Colors.Green };
                switch (attemptColors[i])
                {
                    case 1:
                        GridPageContent.Add(b1, tempCount, counter);
                    break;
                    case 2:
                        GridPageContent.Add(b2, tempCount, counter);
                    break;
                    case 3:
                        GridPageContent.Add(b3, tempCount, counter);
                    break;


                }

                GridPageContent.Add(new Label
                {
                    FontSize = 24,
                    TextColor = Colors.White,
                    Text = wrd.Substring(i, 1),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }, i, counter);//readds letters lol
                tempCount++;
            }
        }
        private int[] WordCheck(String word)
        {
            int[] blockColours = [0, 0, 0, 0, 0];//1 = grey 2 = orange 3 = green
            //check whether word is correct
            //check letter correlations
            Debug.Write("Array result:");
            for (int i = 0; i < blockColours.Length;i++)
            {
                string wordSub = word.Substring(i, 1);
                string keySub = keyWord.Substring(i, 1);

                if (wordSub == keySub)
                {
                    blockColours[i] = 3;
                }
                else if (keyWord.Contains(wordSub)) { 

                    blockColours[i] = 2;
                }
                else
                {
                    blockColours[i] = 1;
                }

                Debug.WriteLine("'{0}' ", blockColours[i]);
            }

            return blockColours;
        }

        private void CreateTheGrid()
        {
            for (int i = 0; i < ROW; i++)
            {
                GridPageContent.AddRowDefinition(new RowDefinition { Height = 60 });
            }
            for (int i = 0; i < COL; i++)
            {
                GridPageContent.AddColumnDefinition(new ColumnDefinition { Width = 60 });

            }

            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    Border b = new Border();
                    b.BackgroundColor = Colors.Grey;
                    b.Stroke = Colors.Grey;
                    GridPageContent.Add(b, i, j);
                }
            }
        }//end of CREATETHEGRID method

        private void Entry_Completed(object sender, EventArgs e)
        {
            string txt = entry.Text;
            bool isWordValid = false;

            if (txt.Length == 5) {
                foreach (string x in validWords)
                {
                    if (txt.Contains(x))
                    {

                        for (int i = 0; i < txt.Length; i++)
                        {
                            GridPageContent.Add(new Label
                            {
                                Text = txt.Substring(i, 1),
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }
                            , i, counter);

                            isWordValid = true;
                        }
                        AttemptGridUpdate(txt);
                        counter++;
                    }
                    Debug.WriteLine("Valid word:" + isWordValid);
                }
            }
            else
            {
                Debug.WriteLine("Word too small...");
            }


        }
    }//end of Entry_Completed event handler
}
