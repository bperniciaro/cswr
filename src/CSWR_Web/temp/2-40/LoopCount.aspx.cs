using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class temp_2_40_LoopCount : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {

  }

  protected void butStart_Click(object sender, EventArgs e)
  {
    int desiredLoopCount = 1;
    int.TryParse(tbLoopCount.Text, out desiredLoopCount);
    int actualLoopCount = 0;
    for(int i = 0;i < desiredLoopCount; i++)
    {
      Slow();
      actualLoopCount++;
    }
    labLoops.Text = actualLoopCount.ToString();
  }

  public void Slow()
  {
    long nthPrime = FindPrimeNumber(1000); //set higher value for more time
  }

  public long FindPrimeNumber(int n)
  {
    int count = 0;
    long a = 2;
    while (count < n)
    {
      long b = 2;
      int prime = 1;// to check if found a prime
      while (b * b <= a)
      {
        if (a % b == 0)
        {
          prime = 0;
          break;
        }
        b++;
      }
      if (prime > 0)
      {
        count++;
      }
      a++;
    }
    return (--a);
  }
}