using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Threading;


namespace Yahtzee
{
    /// <summary>
    /// Interaction logic for OnePlayer.xaml
    /// </summary>
    public partial class OnePlayer
    {
        public OnePlayer()
        {
            InitializeComponent();
        }

        private void TheEnd()
        {
            if (_turnCount != 13) return;
            var win2 = new EndGame(_totalScore);
            win2.Show();
            Close();
        }
        private int _rollCount;
        /// Hold state of dice
        public bool HoldState = true;
        //Scored?
        private bool _scored;
        private int _sumUpper;
        private int _bonusVal;
        private int _totalUpper;
        private int _totalLower;
        private int _totalScore;
        /// value of dice

        private readonly bool[] _holdCheck = new bool[5];
        private readonly int[] _dice = new int[5];

        private int _turnCount;

        public int Reset()
        {
            RollButton.IsEnabled = false;
            if (!_scored) return _totalScore;
            CheckBox0.IsChecked = false;
            CheckBox1.IsChecked = false;
            CheckBox2.IsChecked = false;
            CheckBox3.IsChecked = false;
            CheckBox4.IsChecked = false;
            _rollCount = 0;
            UpperTotalLabel.Text = _sumUpper.ToString();
            if (_sumUpper > 62)
            {
                _bonusVal = 35;
                BonusScore.Text = _bonusVal.ToString();
            }
            _totalUpper = _sumUpper + _bonusVal;
            UpperTotalLabel.Text = _totalUpper.ToString();
            _totalScore = _totalUpper + _totalLower;
            TotalScoreLabel.Text = _totalScore.ToString();
            _scored = false;
            RollButton.IsEnabled = true;
            return _totalScore;
        }

        public int Roll0()
        {
            Thread.Sleep(2);
            var num0 = new Random();
            var number = num0.Next(1, 7);
            _dice[0] = number;
            return number;
        }

        readonly Stopwatch x = new Stopwatch();
        public int Roll1()
        {
            var num1 = new Random();
            var number = 0;
            x.Start();
            while (x.Elapsed < TimeSpan.FromSeconds(.08))
            {
                number = num1.Next(1, 7);
                _dice[1] = number;
            }
            x.Reset();
            return number;
        }
        public int Roll2()
        {
            var num2 = new Random();
            var number = 0;
            x.Start();
            while (x.Elapsed < TimeSpan.FromSeconds(.03))
            {
            number = num2.Next(1, 7);
            _dice[2] = number;
            }
            x.Reset();
            return number;
        }
        public int Roll3()
        {
            var num3 = new Random();
            var number = 0;
            x.Start();
            while (x.Elapsed < TimeSpan.FromSeconds(.02))
            {
                number = num3.Next(1, 7);
                _dice[3] = number;
            }
            x.Reset();
            return number;
        }
        public int Roll4()
        {
            var num4 = new Random();
            var number = 0;
            x.Start();
            while (x.Elapsed < TimeSpan.FromSeconds(.05))
            {
                number = num4.Next(1, 7);
                _dice[4] = number;
            }
            x.Reset();
            return number;
        }


        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount >= 10) return;
            for (var i = 0; i < 5; i++)
            {
                if (_holdCheck[0]) continue;
                var die0 = Roll0().ToString();
                var img = new BitmapImage(new Uri(@"Images/dice" + die0 + ".png", UriKind.Relative));
                ImgDice0.Source = img;
            }
            for (var i = 0; i < 5; i++)
            {
                if (_holdCheck[1]) continue;
                var die1 = Roll1().ToString();
                var img = new BitmapImage(new Uri(@"Images/dice" + die1 + ".png", UriKind.Relative));
                ImgDice1.Source = img;
            }
            for (var i = 0; i < 5; i++)
            {
                if (_holdCheck[2]) continue;
                var die2 = Roll2().ToString();
                var img = new BitmapImage(new Uri(@"Images/dice" + die2 + ".png", UriKind.Relative));
                ImgDice2.Source = img;
            }
            for (var i = 0; i < 5; i++)
            {
                if (_holdCheck[3]) continue;
                var die3 = Roll3().ToString();
                var img = new BitmapImage(new Uri(@"Images/dice" + die3 + ".png", UriKind.Relative));
                ImgDice3.Source = img;
            }
            for (var i = 0; i < 5; i++)
            {
                if (_holdCheck[4]) continue;
                var die4 = Roll4().ToString();
                var img = new BitmapImage(new Uri(@"Images/dice" + die4 + ".png", UriKind.Relative));
                ImgDice4.Source = img;
            }
            _rollCount++;
        }

        private void checkBox0_Checked(object sender, RoutedEventArgs e)
        {
            _holdCheck[0] = true;
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            _holdCheck[1] = true;
        }

        private void checkBox2_Checked(object sender, RoutedEventArgs e)
        {
            _holdCheck[2] = true;
        }

        private void checkBox3_Checked(object sender, RoutedEventArgs e)
        {
            _holdCheck[3] = true;
        }

        private void checkBox4_Checked(object sender, RoutedEventArgs e)
        {
            _holdCheck[4] = true;
        }
        private void checkBox0_Unchecked(object sender, RoutedEventArgs e)
        {
            _holdCheck[0] = false;
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            _holdCheck[1] = false;
        }

        private void checkBox2_Unchecked(object sender, RoutedEventArgs e)
        {
            _holdCheck[2] = false;
        }

        private void checkBox3_Unchecked(object sender, RoutedEventArgs e)
        {
            _holdCheck[3] = false;
        }

        private void checkBox4_Unchecked(object sender, RoutedEventArgs e)
        {
            _holdCheck[4] = false;

        }

        private void AcesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            { _scored = false; }
            else
            {
                CalculateAces();
                _turnCount++;
                AcesButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void TwosButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            { _scored = false; }
            else
            {
                CalculateTwos();
                _turnCount++;
                TwosButton.IsEnabled = false;
                _rollCount = 0;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void ThreesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            { _scored = false; }
            else
            {
                CalculateThrees();
                _turnCount++;
                ThreesButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void FoursButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            { _scored = false; }
            else
            {
                CalculateFours();
                _turnCount++;
                FoursButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void FivesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            { _scored = false; }
            else
            {
                CalculateFives();
                _turnCount++;
                FivesButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void SixesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            { _scored = false; }
            else
            {
                CalculateSixes();
                _turnCount++;
                SixesButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void ThreeOfAKindButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            { _scored = false; }
            else
            {
                CalculateThreeOfAKind();
                _turnCount++;
                ThreeOfAKindButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void FourOfAKindButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            {
                _scored = false;
            }
            else
            {

                CalculateFourOfAKind();
                _turnCount++;
                FourOfAKindButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void FullHouseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            {
                _scored = false;
            }
            else
            {
                CalculateFullHouse();
                _turnCount++;
                FullHouseButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void SmallStraightButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            {
                _scored = false;
            }
            else
            {
                CalculateSmallStraight();
                _turnCount++;
                SmallStraightButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void LargeStraightButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            {
                _scored = false;
            }
            else
            {
                CalculateLargeStraight();
                _turnCount++;
                LargeStraightButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void ChanceButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            {
                _scored = false;
            }
            else
            {
                CalculateChance();
                _turnCount++;
                ChanceButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void YahtzeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            {
                _scored = false;
            }
            else
            {
                CalculateYahtzee();
                _turnCount++;
                YahtzeeButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        private void YahtzeeBonusButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rollCount == 0)
            {
                _scored = false;
            }
            else
            {
                CalculateYahtzeeBonus();
                YahtzeeButton.IsEnabled = false;
                _scored = true;
                Reset();
                TheEnd();
            }
        }

        //Calculate Aces
        public int CalculateAces()
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                if (_dice[i] == 1)
                {
                    count++;
                }
            }
            var sum = count;
            AcesScore.Text = sum.ToString();
            _sumUpper += sum;
            return sum;
        }
        // Calculate Twos
        public int CalculateTwos()
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                if (_dice[i] == 2)
                {
                    count++;
                }
            }
            var sum = count * 2;
            TwosScore.Text = sum.ToString();
            _sumUpper += sum;
            return sum;
        }

        // Calculate Threes
        public int CalculateThrees()
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                if (_dice[i] == 3)
                {
                    count++;
                }
            }
            var sum = count * 3;
            ThreesScore.Text = sum.ToString();
            _sumUpper += sum;
            return sum;
        }
        // Calculate Fours
        public int CalculateFours()
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                if (_dice[i] == 4)
                {
                    count++;
                }
            }
            var sum = count * 4;
            FoursScore.Text = sum.ToString();
            _sumUpper += sum;
            return sum;
        }

        // Calculate Fives
        public int CalculateFives()
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                if (_dice[i] == 5)
                {
                    count++;
                }
            }
            var sum = count * 5;
            FivesScore.Text = sum.ToString();
            _sumUpper += sum;
            return sum;
        }

        // Calculate Sixes
        public int CalculateSixes()
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                if (_dice[i] == 6)
                {
                    count++;
                }
            }
            var sum = count * 6;
            SixesScore.Text = sum.ToString();
            _sumUpper += sum;
            return sum;
        }

        // Calculate three of a kind
        public int CalculateThreeOfAKind()
        {
            var sum = 0;
            var i = _dice;
            Array.Sort(i);
            if ((i[0] == i[1]) && (i[1] == i[2]) ||
                (i[1] == i[2]) && (i[2] == i[3]) ||
                (i[2] == i[3]) && (i[3] == i[4]))
            {
                for (var j = 0; j < 5; j++)
                {
                    sum += _dice[j];
                }
            }
            _totalLower += sum;
            ThreeOfAKindScore.Text = sum.ToString();
            return sum;
        }

        //Calculate Four of a Kind
        public int CalculateFourOfAKind()
        {
            var sum = 0;
            var i = _dice;
            Array.Sort(i);
            if ((i[0] == i[1]) && (i[1] == i[2]) && (i[2] == i[3]) ||
                (i[1] == i[2]) && (i[2] == i[3]) && (i[3] == i[4]))
            {
                for (var j = 0; j < 5; j++)
                {
                    sum += _dice[j];
                }
            }
            _totalLower += sum;
            FourOfAKindScore.Text = sum.ToString();
            return sum;
        }

        // Calculate Full House
        public int CalculateFullHouse()
        {
            var sum = 0;
            var i = _dice;
            Array.Sort(i);
            if ((((i[0] == i[1]) && (i[1] == i[2])) && // Three of a Kind
                 (i[3] == i[4]) && // Two of a Kind
                 (i[2] != i[3])) ||
                ((i[0] == i[1]) && // Two of a Kind
                 ((i[2] == i[3]) && (i[3] == i[4])) && // Three of a Kind
                 (i[1] != i[2])))
                sum = 25;
            FullHouseScore.Text = sum.ToString();
            _totalLower += sum;
            return sum;
        }

        // Calculate Small Straight
        public int CalculateSmallStraight()
        {
            var sum = 0;
            var i = new int[5];
            i[0] = _dice[0];
            i[1] = _dice[1];
            i[2] = _dice[2];
            i[3] = _dice[3];
            i[4] = _dice[4];
            Array.Sort(i);
            // Problem here if there is more than one of the same number, so move any doubles to the end
            for (var j = 0; j < 4; j++)
            {
                if (i[j] != i[j + 1]) continue;
                var temp = i[j];
                for (var k = j; k < 4; k++)
                {
                    i[k] = i[k + 1];
                }
                i[4] = temp;
            }
            if (((i[0] == 1) && (i[1] == 2) && (i[2] == 3) && (i[3] == 4)) ||
                ((i[0] == 2) && (i[1] == 3) && (i[2] == 4) && (i[3] == 5)) ||
                ((i[0] == 3) && (i[1] == 4) && (i[2] == 5) && (i[3] == 6)) ||
                ((i[1] == 1) && (i[2] == 2) && (i[3] == 3) && (i[4] == 4)) ||
                ((i[1] == 2) && (i[2] == 3) && (i[3] == 4) && (i[4] == 5)) ||
                ((i[1] == 3) && (i[2] == 4) && (i[3] == 5) && (i[4] == 6)))
            {
                sum = 30;
            }
            SmallStraightScore.Text = sum.ToString();
            _totalLower += sum;
            return sum;
        }
        // Claculate Large Straight
        public int CalculateLargeStraight()
        {
            var sum = 0;
            var i = new int[5];
            i[0] = _dice[0];
            i[1] = _dice[1];
            i[2] = _dice[2];
            i[3] = _dice[3];
            i[4] = _dice[4];
            Array.Sort(i);
            if (((i[0] == 1) && (i[1] == 2) && (i[2] == 3) && (i[3] == 4) && (i[4] == 5)) ||
                ((i[0] == 2) && (i[1] == 3) && (i[2] == 4) && (i[3] == 5) && (i[4] == 6)))
            {
                sum = 40;
            }
            LargeStraightScore.Text = sum.ToString();
            _totalLower += sum;
            return sum;
        }
        //Calculate Yahtzee
        public int CalculateYahtzee()
        {
            var sum = 0;
            var i = _dice;
            Array.Sort(i);
            if ((i[0] == i[1]) && (i[1] == i[2]) && (i[2] == i[3]) && (i[3] == i[4]))
            {
                sum = 50;
            }
            if (sum >= 50)
                YahtzeeBonusButton.IsEnabled = true;
            YahtzeeScore.Text = sum.ToString();
            _totalLower += sum;
            return sum;
        }
        //Calculate Yahtzee Bonus
        public int CalculateYahtzeeBonus()
        {
            var sum = 0;
            var i = _dice;
            Array.Sort(i);
            if ((i[0] == i[1]) && (i[1] == i[2]) && (i[2] == i[3]) && (i[3] == i[4]))
            {
                sum = 100;
            }
            YahtzeeBonusScore.Text = sum.ToString();
            _totalLower += sum;
            return sum;

        }
        //Calculate Chance
        public int CalculateChance()
        {
            var sum = 0;
            for (var i = 0; i < 5; i++)
            {
                sum += _dice[i];
            }
            ChanceScore.Text = sum.ToString();
            _totalLower += sum;
            return sum;
        }
    }
    //public partial class EndGame : DXWindow
    //{
    //    public int TotalScore { get; set; }
    //}
}
