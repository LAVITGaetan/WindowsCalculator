using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WindowsCalculatorV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /* Ajouter la valeur du button à l écran */
        private void AddValue(object sender, RoutedEventArgs e)
        {
            if (result.Text == "Impossible de diviser par 0" || result.Text.Length > 0 && operation.Text.Split(" ").Contains("="))
            {
                operation.Text = "";
                result.Text = "";
                result.Text += (sender as Button).Content;
            }
            else if (result.Text.Split(",").Length - 1 > 0 && (sender as Button).Content.ToString() == ",")
            {
                result.Text = result.Text;
            }
            else
            {
                result.Text += (sender as Button).Content;
            }
        }

        /* Supprimer le dernier charactère affiché à l'écran */
        private void DeleteLastChar(object sender, RoutedEventArgs e)
        {
            if (result.Text == "Impossible de diviser par 0")
            {
                result.Text = "";
            }
            else if (result.Text.Length > 0)
            {
                result.Text = result.Text.Remove(result.Text.Length - 1);
            }

        }

        /* Supprimer la chaîne de charactères affichée à l'écran */
        private void ClearValue(object sender, RoutedEventArgs e)
        {
            if(operation.Text.Split(" ").Contains("="))
            {
                operation.Text = "";
            }
            result.Text = "";
        }

        /* Supprimer la chaîne de charactères affichée à l'écran ainsi que la mémoire d'opérations*/
        private void ClearAll(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            operation.Text = "";
        }

        /* Inverser la valeur affiché à l'écran */
        private void RevertValue(object sender, RoutedEventArgs e)
        {
            if (result.Text.Length >= 1 && result.Text != "Impossible de diviser par 0" && result.Text != ",")
            {
                String LastChar = result.Text.Substring(result.Text.Length - 1);
                if (LastChar == ",")
                {
                    result.Text = result.Text.Remove(result.Text.Length - 1);
                }
                if (Convert.ToDouble(result.Text) > 0)
                {
                    result.Text = "-" + result.Text;
                }
                else if (Convert.ToDouble(result.Text) < 0)
                {
                    result.Text = result.Text.Remove(0, 1);
                }
            }
        }

        /* Actions liées aux opérateurs */
        private void AddOperator(object sender, RoutedEventArgs e)
        {
            if (result.Text.Length >= 1 && result.Text != "Impossible de diviser par 0" && result.Text != ",")
            {
                /* Ecrire correctement les nombres à virgule sans décimales*/
                if (result.Text.Substring(result.Text.Length - 1) == ",")
                {
                    result.Text = result.Text.Remove(result.Text.Length - 1);
                }

                /* Effacer la mémoire si un calcul a déjà été fait */
                if (operation.Text.Split(" ").Contains("=")){
                    operation.Text = "";
                }

                /* Effectuer le calcul au préalable si une opération existe déjà*/
                if (operation.Text.Length > 1)
                {
                    string[] operationArray = operation.Text.Split(" ");
                    string operationOperator = operationArray[1];
                    switch (operationOperator)
                    {
                        case "+":
                            double add = Convert.ToDouble(operationArray[0]) + Convert.ToDouble(result.Text);
                            result.Text = "";
                            operation.Text = "";
                            operation.Text = add.ToString() + " " + (sender as Button).Content.ToString() + " ";
                            break;

                        case "-":
                            double substract = Convert.ToDouble(operationArray[0]) - Convert.ToDouble(result.Text);
                            result.Text = "";
                            operation.Text = "";
                            operation.Text = substract.ToString() + " " + (sender as Button).Content.ToString() + " ";
                            break;

                        case "*":
                            double multiply = Convert.ToDouble(operationArray[0]) * Convert.ToDouble(result.Text);
                            result.Text = "";
                            operation.Text = "";
                            operation.Text = multiply.ToString() + " " + (sender as Button).Content.ToString() + " ";
                            break;

                        case "/":
                            double divide = Convert.ToDouble(operationArray[0]) / Convert.ToDouble(result.Text);
                            result.Text = "";
                            operation.Text = "";
                            operation.Text = divide.ToString() + " " + (sender as Button).Content.ToString() + " ";
                            break;
                    }
                }

                /* Si la mémoire est vide */
                if (operation.Text.Length < 1)
                {
                    string btnValue = (sender as Button).Content.ToString();
                    switch (btnValue)
                    {
                        case "+" :
                            operation.Text += result.Text + " + ";
                            result.Text = "";
                            break;

                        case "-":
                            operation.Text += result.Text + " - ";
                            result.Text = "";
                            break;

                        case "*":
                            operation.Text += result.Text + " * ";
                            result.Text = "";
                            break;

                        case "/":
                            operation.Text += result.Text + " / ";
                            result.Text = "";
                            break;
                    }
                }
            }
        }
        
        /* Calcul des opérations et affichage du résultat en cliquant sur '=' */
        private void ShowResult(object sender, RoutedEventArgs e)
        {
            if(result.Text.Length >= 1 && result.Text != ",")
            {
                /* Doubler le résultat si il n'y a pas d'opérations */
                if (operation.Text.Length == 0)
                {
                    double operationResult = Convert.ToDouble(result.Text) * 2;
                    result.Text = operationResult.ToString();
                }
                else
                {
                    string[] operationArray = operation.Text.Split(" ");
                    string operationOperator = operationArray[1];
                    string operationFirstElement = operationArray[0];
                    if (operationArray.Length == 3)
                    {
                        operation.Text += " " + result.Text;
                        operationArray = operation.Text.Split(" ");
                        operationOperator = operationArray[1];
                    }
                    if(operationArray.Length > 3)
                    {
                        if (operationArray.Contains("="))
                        {
                            operation.Text = "";
                            double operationResult = Convert.ToDouble(result.Text) * 2;
                            result.Text = operationResult.ToString();
                        }
                        else
                        {
                            bool divide = false;
                            switch (operationOperator)
                            {
                                case "+":
                                    double add = Convert.ToDouble(operationFirstElement) + Convert.ToDouble(result.Text);
                                    result.Text = add.ToString();
                                    operation.Text += " = ";
                                    break;

                                case "-":
                                    double substract = Convert.ToDouble(operationFirstElement) - Convert.ToDouble(result.Text);
                                    result.Text = substract.ToString();
                                    operation.Text += " = ";
                                    break;

                                case "*":
                                    double multiply = Convert.ToDouble(operationFirstElement) * Convert.ToDouble(result.Text);
                                    result.Text = multiply.ToString();
                                    operation.Text += " = ";
                                    break;

                                case "/":
                                    divide = true;
                                    break;
                            }
                            if (divide == true && result.Text == "0")
                            {
                                result.Text = "Impossible de diviser par 0";
                                operation.Text = "";
                            }
                            else if(divide == true && result.Text != "0")
                            {
                                double operationResult = Convert.ToDouble(operationFirstElement) / Convert.ToDouble(result.Text);
                                result.Text = operationResult.ToString();
                                operation.Text += " = ";
                            }
                        }
                    }
                }
            }
        }
    }
}
