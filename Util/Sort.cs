using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Util {
    static class Sort {
        public static void SelectionSort(ObservableCollection<Employee> obj, ComparerDel cmp, bool ascending) {
            Employee temp;
            int h = 0;  //using h and j for sort

            if (ascending) {
                for (int j = 0; j < obj.Count - 1; j++) {
                    int iMin = j;
                    for (h = j + 1; h < obj.Count; h++) {   //delegate in use here
                        if (cmp(obj[h], obj[iMin]) < 0) {
                            iMin = h;
                        }
                    }
                    if (iMin != j) {   //swap
                        temp = obj[iMin];
                        obj[iMin] = obj[j];
                        obj[j] = temp;
                    }
                }
            }
            else {
                for (int j = 0; j < obj.Count - 1; j++) {
                    int iMax = j;
                    for (h = j + 1; h < obj.Count; h++) {   //delegate in use here
                        if (cmp(obj[h], obj[iMax]) > 0) {
                            iMax = h;
                        }
                    }
                    if (iMax != j) {   //swap
                        temp = obj[iMax];
                        obj[iMax] = obj[j];
                        obj[j] = temp;
                    }
                }
            }
        }
    }
}
