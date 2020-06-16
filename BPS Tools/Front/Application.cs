using System.Windows.Forms;

namespace BPSTools.Front
{
    public partial class Application : Form
    {
        public Application()
        {
            InitializeComponent();
        }

        private void New(object sender, System.EventArgs e)
        {
            tbFiles.TabPages.Add(new TabPage("new file"));
        }
    }
}
