/* Anthony Martinez
 * Comparable
 * Comparer
 * Delegate
 * */

using System;
using System.Collections;
using System.ComponentModel;

public delegate int ComparerDel(object obj1, object obj2);

public abstract class Employee : IPayable, IComparable, INotifyPropertyChanged {

    private decimal payAmount = 0;
    //every type of employee gets a payment of some type
    public decimal Payment {
        get { return payAmount; }

        set { payAmount = value;
            RaisedPropertyChanged("Payment Amount");
        }
    }
    // read-only property that gets employee's first name
    public string FirstName { get; set; }

    // read-only property that gets employee's last name
    public string LastName { get; set; }

    // read-only property that gets employee's social security number
    public string SocialSecurityNumber { get; private set; }

   

    // three-parameter constructor
    public Employee(string first, string last, string ssn) {
        FirstName = first;
        LastName = last;
        SocialSecurityNumber = ssn;
       // Payment = GetPaymentAmount();
    } // end three-parameter Employee constructor


    //-----------new 
    public event PropertyChangedEventHandler PropertyChanged;
    public void RaisedPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    //-------------end 

    // return string representation of Employee object, using properties
    public override string ToString() {
        return string.Format("{0} {1}\nsocial security number: {2}",
           FirstName, LastName, SocialSecurityNumber);
    } // end method ToString

    public abstract decimal GetPaymentAmount();

    public int CompareTo(object obj) {
        if (obj == null) return 1;
        Employee otherEmployee = obj as Employee;

        if (otherEmployee != null) {
            if (this.LastName.CompareTo(otherEmployee.LastName) < 0) {
                return 1;
            }
            else if (this.LastName.CompareTo(otherEmployee.LastName) > 0) {
                return -1;
            }
            else {
                return 0;
            }
        }
        else {
            throw new ArgumentException("Object Not an Employee");
        }
    }

    public class sortAscendingHelper : IComparer {
        int IComparer.Compare(object x, object y) {
            Employee first = x as Employee;
            Employee second = y as Employee;
            if (first.GetPaymentAmount() > second.GetPaymentAmount())
                return 1;

            if (first.GetPaymentAmount() < second.GetPaymentAmount())
                return -1;

            return 0;
        }
    }

    //Method to return IComparer Object
    public static IComparer sortPayAscending() {
        return (IComparer)new sortAscendingHelper();
    }

    public static int CompareSSN(object n1, object n2) {
        string ssn1 = ((Employee)n1).SocialSecurityNumber;
        string ssn2 = ((Employee)n2).SocialSecurityNumber;

        //1 is greater than , -1 is less than , 0 is equal
        if (String.Compare(ssn1, ssn2) > 0)
            return -1;
        if (String.Compare(ssn1, ssn2) < 0)
            return 1;

        return 0;
    }

    public static int CompareLast(object n1, object n2) {
        string ln1 = ((Employee)n1).LastName;
        string ln2 = ((Employee)n2).LastName;

        if (String.Compare(ln1, ln2) > 0)
            return 1;
        if (String.Compare(ln1, ln2) < 0)
            return -1;

        return 0;
    }

    public static int ComparePay(object n1, object n2) {
        Employee first = n1 as Employee;
        Employee second = n2 as Employee;
        if (first.GetPaymentAmount() > second.GetPaymentAmount())
                return 1;

            if (first.GetPaymentAmount() < second.GetPaymentAmount())
                return -1;

            return 0;
       
    }

} // end abstract class Employee
