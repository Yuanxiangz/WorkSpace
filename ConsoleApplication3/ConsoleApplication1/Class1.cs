using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	public class VolatileObjectTest {  
      
    bool boolValue;
   
    public void waitToExit() {  
        if(boolValue == !boolValue) Console.WriteLine();
    }  
       
    public void swap() {
        try {
            Thread.Sleep(100);
        } catch (Exception e) {
            Console.WriteLine(e);
        }
        boolValue = !boolValue;  
        Console.WriteLine(boolValue);
    }  
   
} 
}
