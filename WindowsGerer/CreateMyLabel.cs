using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsGerer
{
    class CreateMyLabel : Label
    {
        CreateMyLabel() : base()
        {
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        }
    }
}
