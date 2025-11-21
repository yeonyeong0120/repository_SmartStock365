using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockManager.Models;
using StockManagerDAL;

namespace StockManager_1111
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string id = tbUsername.Text.Trim(); // 공백 제거필요..
            string pw = tbPassword.Text.Trim();



            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
            {
                MessageBox.Show("아이디와 비밀번호를 모두 입력해주세요!", "입력 오류");
                return;
            }

            try
            {
                UserRepository userRepo = new UserRepository();
                User user = userRepo.GetUserByUsername(id);

                if (user != null && user.PasswordHash == pw)
                {
                    GlobalContext.CurrentUser = user; // 사용자 정보 저장

                    string displayName = "";

                    if (user.Role == "Admin")
                        displayName = "관리자";
                    else if (user.Role == "Staff")
                        displayName = "직원";
                    else
                        displayName = user.Role; // 그 외? 아직 안정함..

                    MessageBox.Show($"\n\n      {displayName}님 환영합니다!\n             ({user.Role})", "로그인 성공");

                    MainForm mainForm = new MainForm();
                    this.Hide();
                    mainForm.ShowDialog();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("아이디 또는 비밀번호가 일치하지 않습니다!", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("로그인 처리 중 오류 발생: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // 로그인
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = tbUsername;
        }
        private void panel1_Paint(object sender, PaintEventArgs e) // 판넬
        {
            Color borderColor = Color.SteelBlue;
            int borderSize = 1;
            ControlPaint.DrawBorder(e.Graphics,
                                    panel1.ClientRectangle,
                                    borderColor, borderSize, ButtonBorderStyle.Dashed, // 왼쪽
                                    borderColor, borderSize, ButtonBorderStyle.Dashed, // 위쪽
                                    borderColor, borderSize, ButtonBorderStyle.Dashed, // 오른쪽
                                    borderColor, borderSize, ButtonBorderStyle.Dashed  // 아래쪽
                                    );
        }
    } // 클래스
}
