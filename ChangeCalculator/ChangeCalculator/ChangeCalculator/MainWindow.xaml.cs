using System; 
 using System.Collections.Generic; 
 using System.Linq; 
 using System.Text; 
 using System.Threading.Tasks; 
 using System.Windows; 
 using System.Windows.Controls; 
 using System.Windows.Data; 
 using System.Windows.Documents; 
 using System.Windows.Input; 
 using System.Windows.Media; 
 using System.Windows.Media.Imaging; 
 using System.Windows.Navigation; 
 using System.Windows.Shapes; 
 
 namespace WpfChangeCalculator
 { 
      
     public partial class MainWindow : Window 
     { 
         static decimal itemPrice, amountGiven, change; 
       
         static decimal[,] coinInfo =
             { {0, 0, 0, 0 } , { .25M, .10M, .05M, .01M } };

        static int coinInfoIndexNum = 0, coinInfoIndexVal = 1; 
         
         static string[] coinTypeLabels =
             {"Quarters: ", "Dimes: ", "Nickels: ", "Pennies: "};       
 
 
         public MainWindow()
         { 
             InitializeComponent(); 
         } 
 
 
         private void button_Click(object sender, RoutedEventArgs e)
         { 
             bool inputAmountsValid; 
 
             Label[] labelCoins = { labelQuarters, labelDimes, labelNickels, labelPennies };           
 
             try 
             { 
                  
                 inputAmountsValid = validateInputAmounts(); 

                 if (inputAmountsValid) 
                 { 
                     calcCoinAmounts(); 
 
                     displayResults(labelCoins); 
                 } 
             } 
             catch (Exception exceptionObject) 
             { 
                 MessageBox.Show("An error occurred: " + exceptionObject.Message); 
             }            
 
         } 
 
         private void textBoxItemPrice_TextChanged(object sender, TextChangedEventArgs e)
         { 
             labelPriceError.Content = ""; 
         } 
 
 
         private void textBoxAmountFromCustomer_TextChanged(object sender, TextChangedEventArgs e)
         { 
             labelAmountGivenError.Content = ""; 
         } 
 

         private void buttonClearAll_Click(object sender, RoutedEventArgs e)
         { 
             textBoxItemPrice.Text = ""; 
             textBoxAmountFromCustomer.Text = ""; 
             labelPriceError.Content = ""; 
             labelAmountGivenError.Content = ""; 
             labelChange.Content = "Change: ";            
             labelQuarters.Content = "Quarters: "; 
             labelNickels.Content = "Dimes: "; 
             labelDimes.Content = "Nickels: "; 
             labelPennies.Content = "Pennies: "; 
         } 
 
         private void buttonClearPrice_Click(object sender, RoutedEventArgs e)
         { 
             textBoxItemPrice.Text = ""; 
             labelPriceError.Content = ""; 
         } 
 
         private void buttonClearAmount_Click(object sender, RoutedEventArgs e)
         { 
             textBoxAmountFromCustomer.Text = ""; 
             labelAmountGivenError.Content = ""; 
         } 
 
         private bool validateInputAmounts()
         { 
             bool result = false;  
    
             bool itemPriceValid = validateMoney(textBoxItemPrice, labelPriceError, out itemPrice);  
 
             bool AmountGivenValid =
                validateMoney(textBoxAmountFromCustomer, labelAmountGivenError, out amountGiven); 
 
             if (itemPriceValid && AmountGivenValid) 
            {                             
                 if (amountGiven >= itemPrice) 
                 { 
                     result = true; 
                 } 
                 else 
                 { 
                     labelAmountGivenError.Content = 
                         "Please enter a number greater than or equal to the item price.";                   
                } 
             } 
             return result; 
         } 
 
         private void calcCoinAmounts()
         { 
             
             change = amountGiven  - itemPrice; 
             decimal changeLeft = change; 
 
 
             for (int i = 0; i<coinInfo.GetLength(1); i++) 
             { 
                coinInfo[coinInfoIndexNum, i] = 
                   Decimal.Floor(changeLeft / coinInfo[coinInfoIndexVal, i]); 
                 
                 changeLeft = changeLeft % coinInfo[coinInfoIndexVal, i]; 
             } 
        } 
  
         private void displayResults(Label[] labelCoins)
         { 
             labelChange.Content = "Change: " + String.Format("{0:C2}", change); 
             for (int i = 0; i<labelCoins.Length; i++) 
             { 
                 labelCoins[i].Content =  
                     coinTypeLabels[i] + coinInfo[coinInfoIndexNum, i]; 
             } 
         } 
 
 
         private bool validateMoney(TextBox inputTextBox, Label labelError, out decimal resultAmount)
         { 
             bool amountValid = validatePosDecNum(inputTextBox, labelError, out resultAmount); 
             if (amountValid) 
             { 
                            
                if (resultAmount != Math.Round(resultAmount, 2)) 
                { 
                     labelError.Content =  
                                 "Please enter a number with no more than 2 decimal places."; 
                     amountValid = false; 
                 } 
             }           
             return amountValid; 
         } 
 
         private bool validatePosDecNum(TextBox inputTextBox, Label labelError,
                                                             out decimal resultNum)
         { 
             bool numValid = false; 
        
             if (Decimal.TryParse(inputTextBox.Text, out resultNum)) 
             { 
                 if (resultNum > 0) 
                 { 
                     numValid = true; 
                 } 
                 else 
                 { 
                     labelError.Content = "Please enter a positive number."; 
                 } 
             } 
             else 
             { 
                 labelError.Content = "Please enter a valid number that is not too large."; 
            } 
            return numValid; 
         } 
 
     } 
 } 













    


