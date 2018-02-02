using System;
using System.Collections.Generic;
using System.Xml;
using System.ComponentModel;
using System.Windows.Input;
using CommandSample;
using Assignment5.Util;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;

namespace Assignment5.ViewModel {
    public class EmployeeViewModel  {
        
        private string path = @"C:\Users\tonyd\Documents\school\Fall2016\475\Assignment5\Assignment5\Util\SalariedEmployee.xml";
        public ObservableCollection<IPayable> EmployeeList;

        public DelegateCommand SortLast { get; private set; }
        public DelegateCommand SortPay { get; private set; }
        public DelegateCommand SortSSN { get; private set; }
        public DelegateCommand ResetL { get; private set; }

        public enum SortingOrder {
            [Description("Ascending")]
            Ascending = 1,
            [Description("Descending")]
            Descending = 2
        }

        private List<KeyValuePair<string, SortingOrder>> sortingList;
        private SortingOrder selectedSorting = SortingOrder.Ascending;
        
        public SortingOrder SelectedSorting {
            get { return selectedSorting; }
            set {
                selectedSorting = value;
            }
        }

        //The code below is used for binding a combo to an enums in wpf and mvvm
        public List<KeyValuePair<string, SortingOrder>> SortingList {
            get {
                if (sortingList == null) {
                    sortingList = new List<KeyValuePair<string, SortingOrder>>();
                    foreach (SortingOrder level in Enum.GetValues(typeof(SortingOrder))) {
                        string description;
                        FieldInfo fieldInfo = level.GetType().GetField(level.ToString());
                        DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (attributes != null && attributes.Length > 0) { description = attributes[0].Description; }
                        else { description = string.Empty; }
                        KeyValuePair<string, SortingOrder> TypeKeyValue =
                        new KeyValuePair<string, SortingOrder>(description, level);
                        sortingList.Add(TypeKeyValue);
                    }
                }
                return sortingList;
            }
        }

        //Constructor
        public EmployeeViewModel() {
            EmployeeList = new ObservableCollection<IPayable>();
            InitEmployees(EmployeeList, path);
            
            SortLast = new DelegateCommand(SortByLast, CanExecute);
            SortSSN = new DelegateCommand(SortBySSN, CanExecute);
            SortPay = new DelegateCommand(SortByPay, CanExecute);
            ResetL = new DelegateCommand(ReloadCollection, CanExecute);
        }
        
        //exposed collection
        public ObservableCollection<IPayable> Employees {
            get { return EmployeeList; }
            set { EmployeeList = value;
               // RaisePropertyChanged("Update");
            }
        }
        
        //shared CanExecute
        bool CanExecute(object parameter) {
            return true;
        }

        //sorting methods
        void SortByLast() {
            if (SelectedSorting == SortingOrder.Ascending) {
               
                var asc = new ObservableCollection<IPayable>
                    (EmployeeList.OrderBy(name => ((Employee)name).LastName));
                EmployeeList.Clear();
                foreach (var i in asc) {
                    EmployeeList.Add(i);
                }
            }else {
                var des = new ObservableCollection<IPayable>
                    (EmployeeList.OrderByDescending(name => ((Employee)name).LastName));
                EmployeeList.Clear();
                foreach (var i in des) {
                    EmployeeList.Add(i);
                }
            }
        }

        void SortByPay() {
            if (SelectedSorting == SortingOrder.Ascending) {

                var asc = new ObservableCollection<IPayable>
                    (EmployeeList.OrderBy(pay => ((Employee)pay).Payment));
                EmployeeList.Clear();
                foreach (var i in asc) {
                    EmployeeList.Add(i);
                }
            }
            else {
                var des = new ObservableCollection<IPayable>
                    (EmployeeList.OrderByDescending(pay => ((Employee)pay).Payment));
                EmployeeList.Clear();
                foreach (var i in des) {
                    EmployeeList.Add(i);
                }
            }
        }

        void SortBySSN() {
            if (SelectedSorting == SortingOrder.Ascending) {

                var asc = new ObservableCollection<IPayable>
                    (EmployeeList.OrderBy(ssn=>((Employee)ssn).SocialSecurityNumber));
                EmployeeList.Clear();
                foreach (var i in asc) {
                    EmployeeList.Add(i);
                }
            }
            else {
                var des = new ObservableCollection<IPayable>
                    (EmployeeList.OrderByDescending(ssn => ((Employee)ssn).SocialSecurityNumber));
                EmployeeList.Clear();
                foreach (var i in des) {
                    EmployeeList.Add(i);
                }
            }
        }
        
        //Reset and reload the Employee collection
        void ReloadCollection() {
            ResetCollection(EmployeeList, path);
        }
        private void ResetCollection(ObservableCollection<IPayable> empL, string path) {
            EmployeeList.Clear();
            InitEmployees(empL, path);
        }

        //load xml file into an ObservableCollection
        private void InitEmployees(ObservableCollection<IPayable> EmployeeList, string path) {
          
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            XmlReader xmlIn = XmlReader.Create(path, settings);

            if (xmlIn.ReadToDescendant("Employee")) {
                // xmlIn.ReadToDescendant("Employee");
                do {
                    string id = xmlIn.GetAttribute("id");
                    if (id.Equals("salaried")) {
                        xmlIn.ReadStartElement("Employee");
                        string ssn = xmlIn.ReadElementContentAsString();
                        string first = xmlIn.ReadElementContentAsString();
                        string last = xmlIn.ReadElementContentAsString();
                        decimal salary = xmlIn.ReadElementContentAsDecimal();
                        Employee salaryEmp = new SalariedEmployee(first, last, ssn, salary);
                        EmployeeList.Add(salaryEmp);
                    }
                    else if (id.Equals("commission")) {
                        xmlIn.ReadStartElement("Employee");
                        string ssn = xmlIn.ReadElementContentAsString();
                        string first = xmlIn.ReadElementContentAsString();
                        string last = xmlIn.ReadElementContentAsString();
                        decimal sales = xmlIn.ReadElementContentAsDecimal();
                        decimal rate = xmlIn.ReadElementContentAsDecimal();
                        Employee salaryEmp = new CommissionEmployee(first, last, ssn, sales, rate);
                        EmployeeList.Add(salaryEmp);
                    }
                    else if (id.Equals("hourly")) {
                        xmlIn.ReadStartElement("Employee");
                        string ssn = xmlIn.ReadElementContentAsString();
                        string first = xmlIn.ReadElementContentAsString();
                        string last = xmlIn.ReadElementContentAsString();
                        decimal hourlyWage = xmlIn.ReadElementContentAsDecimal();
                        decimal hoursWorked = xmlIn.ReadElementContentAsDecimal();
                        Employee salaryEmp = new HourlyEmployee(first, last, ssn, hourlyWage, hoursWorked);
                        EmployeeList.Add(salaryEmp);
                    }
                    else if (id.Equals("base")) {
                        xmlIn.ReadStartElement("Employee");
                        string ssn = xmlIn.ReadElementContentAsString();
                        string first = xmlIn.ReadElementContentAsString();
                        string last = xmlIn.ReadElementContentAsString();
                        decimal sales = xmlIn.ReadElementContentAsDecimal();
                        decimal rate = xmlIn.ReadElementContentAsDecimal();
                        decimal salary = xmlIn.ReadElementContentAsDecimal();
                        Employee salaryEmp = new BasePlusCommissionEmployee(first, last, ssn, sales, rate, salary);
                        EmployeeList.Add(salaryEmp);
                    }
                }
                while (xmlIn.ReadToNextSibling("Employee"));
            }
            xmlIn.Close();
        }
    }

    public class CommandHandler : ICommand {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute) {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter) {
            _action();
        }
    }

}
