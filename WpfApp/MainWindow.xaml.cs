using System.ComponentModel;
using System.Windows;

namespace ChmodConverterApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _numericInput;
        private string _symbolicOutput;
        private bool _isNumericValid;
        private bool _userRead, _userWrite, _userExecute;
        private bool _groupRead, _groupWrite, _groupExecute;
        private bool _othersRead, _othersWrite, _othersExecute;
        private bool _isUpdating;

        public event PropertyChangedEventHandler PropertyChanged;

        public string NumericInput
        {
            get => _numericInput;
            set
            {
                _numericInput = value;
                OnPropertyChanged(nameof(NumericInput));
                if (IsLoaded) UpdateFromNumeric(); 
            }
        }

        public string SymbolicOutput
        {
            get => _symbolicOutput;
            set
            {
                _symbolicOutput = value;
                OnPropertyChanged(nameof(SymbolicOutput));
            }
        }

        public bool IsNumericValid
        {
            get => _isNumericValid;
            set
            {
                _isNumericValid = value;
                OnPropertyChanged(nameof(IsNumericValid));
            }
        }

        public bool UserRead
        {
            get => _userRead;
            set { _userRead = value; OnPropertyChanged(nameof(UserRead)); if (IsLoaded) UpdateFromSymbolic(); }
        }
        public bool UserWrite
        {
            get => _userWrite;
            set { _userWrite = value; OnPropertyChanged(nameof(UserWrite)); if (IsLoaded) UpdateFromSymbolic(); }
        }
        public bool UserExecute
        {
            get => _userExecute;
            set { _userExecute = value; OnPropertyChanged(nameof(UserExecute)); if (IsLoaded) UpdateFromSymbolic(); }
        }
        public bool GroupRead
        {
            get => _groupRead;
            set { _groupRead = value; OnPropertyChanged(nameof(GroupRead)); if (IsLoaded) UpdateFromSymbolic(); }
        }
        public bool GroupWrite
        {
            get => _groupWrite;
            set { _groupWrite = value; OnPropertyChanged(nameof(GroupWrite)); if (IsLoaded) UpdateFromSymbolic(); }
        }
        public bool GroupExecute
        {
            get => _groupExecute;
            set { _groupExecute = value; OnPropertyChanged(nameof(GroupExecute)); if (IsLoaded) UpdateFromSymbolic(); }
        }
        public bool OthersRead
        {
            get => _othersRead;
            set { _othersRead = value; OnPropertyChanged(nameof(OthersRead)); if (IsLoaded) UpdateFromSymbolic(); }
        }
        public bool OthersWrite
        {
            get => _othersWrite;
            set { _othersWrite = value; OnPropertyChanged(nameof(OthersWrite)); if (IsLoaded) UpdateFromSymbolic(); }
        }
        public bool OthersExecute
        {
            get => _othersExecute;
            set { _othersExecute = value; OnPropertyChanged(nameof(OthersExecute)); if (IsLoaded) UpdateFromSymbolic(); }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NumericInput = "644"; 
        }

        private void UpdateFromNumeric()
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                string symbolic = ChmodConverter.NumericToSymbolic(NumericInput);
                SymbolicOutput = symbolic;
                IsNumericValid = true;

                
                _isUpdating = true; 
                UserRead = symbolic[0] == 'r';
                UserWrite = symbolic[1] == 'w';
                UserExecute = symbolic[2] == 'x';
                GroupRead = symbolic[3] == 'r';
                GroupWrite = symbolic[4] == 'w';
                GroupExecute = symbolic[5] == 'x';
                OthersRead = symbolic[6] == 'r';
                OthersWrite = symbolic[7] == 'w';
                OthersExecute = symbolic[8] == 'x';
            }
            catch
            {
                IsNumericValid = false;
                SymbolicOutput = "Invalid";
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private void UpdateFromSymbolic()
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                string symbolic = $"{(UserRead ? "r" : "-")}{(UserWrite ? "w" : "-")}{(UserExecute ? "x" : "-")}" +
                                 $"{(GroupRead ? "r" : "-")}{(GroupWrite ? "w" : "-")}{(GroupExecute ? "x" : "-")}" +
                                 $"{(OthersRead ? "r" : "-")}{(OthersWrite ? "w" : "-")}{(OthersExecute ? "x" : "-")}";
                string numeric = ChmodConverter.SymbolicToNumeric(symbolic);
                _isUpdating = true;
                NumericInput = numeric;
                SymbolicOutput = symbolic;
                IsNumericValid = true;
            }
            catch
            {
                IsNumericValid = false;
                NumericInput = "Error";
            }
            finally
            {
                _isUpdating = false;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}