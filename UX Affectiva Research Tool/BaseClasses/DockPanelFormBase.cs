using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
namespace UX_Affectiva_Research_Tool
{
    /// <summary>
    /// This is a base class for working with new forms and making new forms dockable to containers,
    /// This will help make customizeable layouts by letting the user move the form seprately
    /// </summary>
    public partial class DockPanelFormBase : DockContent
    {
        public DockPanelFormBase()
        {
            InitializeComponent();
        }
    }
}
