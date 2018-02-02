using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SalariedEmployee : Employee, INotifyPropertyChanged
{
    private decimal weeklySalary;
   
    // four-parameter constructor
    public SalariedEmployee(string First, string Last, string SSN,
       decimal salary) : base(First, Last, SSN)
    {
        WeeklySalary = salary; // validate salary via property
        Payment = GetPaymentAmount();
    } // end four-parameter SalariedEmployee constructor

    // property that gets and sets salaried employee's salary

    public decimal WeeklySalary
    {
        get
        {
            return weeklySalary;
        } // end get
        set
        {
            if (value >= 0) // validation
                weeklySalary = value;
            else
                throw new ArgumentOutOfRangeException("WeeklySalary",
                   value, "WeeklySalary must be >= 0");
        } // end set
    } // end property WeeklySalary

    // calculate earnings; override abstract method Earnings in Employee
    public override decimal GetPaymentAmount()
    {
        return WeeklySalary;
        
    } // end method Earnings          
    
    // return string representation of SalariedEmployee object
    public override string ToString()
    {
        return string.Format("salaried employee: {0}\n{1}: {2:C}",
           base.ToString(), "weekly salary", WeeklySalary);
    } // end method ToString  


    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName) {
       
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
                                        
} // end class SalariedEmployee