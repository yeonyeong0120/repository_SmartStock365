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
    public partial class FormCustomerRules : Form
    {
        public FormCustomerRules()
        {
            InitializeComponent();
        }
        private void FormCustomerRules_Load(object sender, EventArgs e)
        {
            LoadRulesGrid();
            LoadCustomerComboBox();
            LoadProductComboBox();

            if (GlobalContext.CurrentUser.Role.Trim().ToLower() != "admin")
            {
                // 관리자가 아니면 버튼 끄기
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                this.Text += " (조회 전용)";
            }
        }
        private void LoadRulesGrid()
        {
            CustomerRuleRepository ruleRepo = new CustomerRuleRepository();
            dgvRules.DataSource = ruleRepo.GetAllCustomerRules();

            dgvRules.Columns["CustomerId"].Visible = false;

            dgvRules.Columns["RuleId"].HeaderText = "규칙ID";
            // dgvRules.Columns["CustomerId"].HeaderText = "거래처ID";
            dgvRules.Columns["ProductId"].HeaderText = "상품ID";
            dgvRules.Columns["CustomerName"].HeaderText = "거래처명";
            dgvRules.Columns["ProductName"].HeaderText = "상품명";
            dgvRules.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRules.Columns["Required_REDW_days"].HeaderText = "입고기준일";

            dgvRules.Columns["RuleId"].Width = 80;
            dgvRules.Columns["ProductId"].Width = 80;
            dgvRules.Columns["ProductName"].Width = 208;

            dgvRules.Columns["Required_REDW_days"].DisplayIndex = 5;

        }
        private void LoadCustomerComboBox()
        {
            CustomerRepository customerRepo = new CustomerRepository();
            cbxCustomer.DataSource = customerRepo.GetAllCustomers();
            cbxCustomer.DataSource = customerRepo.GetCustomersByType("매출처"); // 매출처만
            cbxCustomer.DisplayMember = "CustomerName"; // 보여줄 이름
            cbxCustomer.ValueMember = "CustomerId";
            cbxCustomer.SelectedIndex = -1; // 기본 선택 없음
        }
        private void LoadProductComboBox()
        {
            ProductRepository productRepo = new ProductRepository();
            cbxProduct.DataSource = productRepo.GetAllProducts();
            cbxProduct.DisplayMember = "ProductName";
            cbxProduct.ValueMember = "ProductId";
            cbxProduct.SelectedIndex = -1;
        }

        private void dgvRules_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvRules.Rows[e.RowIndex];

            tbSelectedRule.Text = row.Cells["RuleId"].Value.ToString();

            // 콤보박스 값 설정 (ValueMember인 ID로 매칭)
            cbxCustomer.SelectedValue = row.Cells["CustomerId"].Value;
            cbxProduct.SelectedValue = row.Cells["ProductId"].Value;

            // REDW 일수 설정
            numRedw.Value = Convert.ToDecimal(row.Cells["Required_REDW_days"].Value);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbxCustomer.SelectedValue == null || cbxProduct.SelectedValue == null)
            {
                MessageBox.Show("거래처와 상품을 모두 선택해주세요!");
                return;
            }

            CustomerRule newRule = new CustomerRule();
            newRule.CustomerId = (int)cbxCustomer.SelectedValue;
            newRule.ProductId = (int)cbxProduct.SelectedValue;
            newRule.Required_REDW_days = (int)numRedw.Value;

            try
            {
                CustomerRuleRepository ruleRepo = new CustomerRuleRepository();
                if (ruleRepo.AddNewRule(newRule))
                {
                    MessageBox.Show("규칙이 저장되었습니다!");
                    LoadRulesGrid();
                }
            }
            catch (Exception ex)
            {
                // 이미 같은 거래처-상품 조합이 있으면 에러
                MessageBox.Show("이미 존재하는 상품 규칙이 있습니다!\n" + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int ruleId;
            if (!int.TryParse(tbSelectedRule.Text, out ruleId)) return;

            CustomerRule ruleToUpdate = new CustomerRule();
            ruleToUpdate.RuleId = ruleId;
            ruleToUpdate.CustomerId = (int)cbxCustomer.SelectedValue;
            ruleToUpdate.ProductId = (int)cbxProduct.SelectedValue;
            ruleToUpdate.Required_REDW_days = (int)numRedw.Value;

            CustomerRuleRepository ruleRepo = new CustomerRuleRepository();
            if (ruleRepo.UpdateRule(ruleToUpdate))
            {
                MessageBox.Show("규칙이 수정되었습니다!");
                LoadRulesGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ruleId;
            if (!int.TryParse(tbSelectedRule.Text, out ruleId)) return;

            if (MessageBox.Show("정말 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CustomerRuleRepository ruleRepo = new CustomerRuleRepository();
                if (ruleRepo.DeleteRule(ruleId))
                {
                    MessageBox.Show("삭제되었습니다!");
                    LoadRulesGrid();
                    tbSelectedRule.Text = "-";
                }
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tbSelectedRule.Text = "";
            cbxCustomer.SelectedIndex = -1;
            cbxProduct.SelectedIndex = -1;
            numRedw.Value = 0;

            FormCustomerRules_Load(sender, e);
        }

    }
}
