using System;
using System.Windows;
using System.Windows.Controls;
using ChmodConverterApp;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private bool _isUpdating = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SymbolicTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating) return;
            _isUpdating = true;
            ErrorTextBlock.Text = "";
            try
            {
                string symbolic = SymbolicTextBox.Text;
                if (symbolic.Length == 9)
                {
                    string numeric = ChmodConverter.SymbolicToNumeric(symbolic);
                    NumericTextBox.Text = numeric;
                }
                else
                {
                    NumericTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                NumericTextBox.Text = "";
                ErrorTextBlock.Text = ex.Message;
            }
            _isUpdating = false;
        }

        private void NumericTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating) return;
            _isUpdating = true;
            ErrorTextBlock.Text = "";
            try
            {
                string numeric = NumericTextBox.Text;
                if (numeric.Length == 3)
                {
                    string symbolic = ChmodConverter.NumericToSymbolic(numeric);
                    SymbolicTextBox.Text = symbolic;
                }
                else
                {
                    SymbolicTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                SymbolicTextBox.Text = "";
                ErrorTextBlock.Text = ex.Message;
            }
            _isUpdating = false;
        }
    }
}