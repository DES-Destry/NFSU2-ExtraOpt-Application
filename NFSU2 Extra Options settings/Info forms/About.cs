using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NFSU2_Extra_Options_settings.Info_forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        void ButtOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ButtNLGYouTube_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/user/nlgzrgn77");
        }

        void ButtNLGTwitter_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/nlgzrgn");
        }

        void ButtNLGGit_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/nlgzrgn");
        }

        void ButtDESYouTube1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UC1-Y0q3ZYjDcT98unhArmkw?view_as=subscriber");
        }

        void ButtDESYouTube2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCzByESKwutH9wUBTmsL792Q?view_as=subscriber");
        }

        void ButtDESTwitter_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/DESDestry1");
        }

        void ButtDESVK_Click(object sender, EventArgs e)
        {
            Process.Start("https://vk.com/ilkov1");
        }

        void ButtDESGit_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/DES-Destry");
        }

        private void ButtSrc_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/redirect?redir_token=k-JCGRjd5i8rmBxnKqk50GFxLNZ8MTU1ODI2NTI2MkAxNTU4MTc4ODYy&q=https%3A%2F%2Fgithub.com%2FExOptsTeam%2FNFSU2ExOpts&event=video_description&v=6eTX3wakbpk");
        }
    }
}
