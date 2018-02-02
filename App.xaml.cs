using Assignment5.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment5 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    //this is prob wrong...i'm not sure how to get rid of it??

    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            Assignment5.MainWindow window = new MainWindow();
            EmployeeViewModel VM = new EmployeeViewModel();
            window.DataContext = VM;
            window.Show();
        }
    }
}
