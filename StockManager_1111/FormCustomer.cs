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
    public partial class FormCustomer : Form
    {
        public FormCustomer()
        {
            InitializeComponent();
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            LoadCustomersGrid();
            if (cbxCustomerType.Items.Count == 0)
            {
                cbxCustomerType.Items.Add("매입처");
                cbxCustomerType.Items.Add("매출처");
                cbxCustomerType.SelectedIndex = 0; // 일단 매입처 기본 설정
            }

            if (GlobalContext.CurrentUser.Role != "Admin")
            {
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                this.Text += " (조회 전용)";
            }

        }
        private void LoadCustomersGrid()
        {
            CustomerRepository customerRepo = new CustomerRepository();
            dgvCustomers.DataSource = customerRepo.GetAllCustomers();

            dgvCustomers.Columns["CustomerId"].HeaderText = "거래처ID";
            dgvCustomers.Columns["CustomerName"].HeaderText = "거래처명";
            dgvCustomers.Columns["ContactPerson"].HeaderText = "담당자";
            dgvCustomers.Columns["PhoneNumber"].HeaderText = "연락처";
            dgvCustomers.Columns["Address"].HeaderText = "주소";
            dgvCustomers.Columns["Notes"].HeaderText = "메모";
            dgvCustomers.Columns["CustomerType"].HeaderText = "거래처 구분";

            // 컬럼 조절
            dgvCustomers.Columns["CustomerId"].Width = 80;
            dgvCustomers.Columns["CustomerName"].Width = 100;
            dgvCustomers.Columns["ContactPerson"].Width = 70;
            dgvCustomers.Columns["Address"].Width = 180;
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];

            tbSelectedCustomerId.Text = row.Cells["CustomerId"].Value.ToString();
            cbxCustomerType.SelectedItem = row.Cells["CustomerType"].Value?.ToString();
            tbCustomerName.Text = row.Cells["CustomerName"].Value.ToString();
            tbContactPerson.Text = row.Cells["ContactPerson"].Value?.ToString(); // NULL 안전 처리
            tbPhoneNumber.Text = row.Cells["PhoneNumber"].Value?.ToString();
            tbAddress.Text = row.Cells["Address"].Value?.ToString();
            tbMemo.Text = row.Cells["Notes"].Value?.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 오입력 방지용...
            if (string.IsNullOrEmpty(tbCustomerName.Text))
            {
                MessageBox.Show("거래처명을 입력해주세요!");
                return;
            }

            Customer newCustomer = new Customer()
            {
                CustomerName = tbCustomerName.Text,
                ContactPerson = tbContactPerson.Text,
                PhoneNumber = tbPhoneNumber.Text,
                Address = tbAddress.Text,
                Notes = tbMemo.Text,
                CustomerType = cbxCustomerType.SelectedItem.ToString()
            };

            CustomerRepository customerRepo = new CustomerRepository();
            bool success = customerRepo.AddNewCustomer(newCustomer);

            if (success)
            {
                MessageBox.Show("거래처가 저장되었습니다!");
                LoadCustomersGrid(); // 새로고침
            }
            else
            {
                MessageBox.Show("저장에 실패했습니다ㅠ_ㅠ");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int customerId;
            if (!int.TryParse(tbSelectedCustomerId.Text, out customerId))
            {
                MessageBox.Show("수정할 거래처를 선택해주세요!");
                return;
            }

            Customer customerToUpdate = new Customer()
            {
                CustomerId = customerId,
                CustomerName = tbCustomerName.Text,
                ContactPerson = tbContactPerson.Text,
                PhoneNumber = tbPhoneNumber.Text,
                Address = tbAddress.Text,
                Notes = tbMemo.Text,
                CustomerType = cbxCustomerType.SelectedItem.ToString()  //
            };

            CustomerRepository customerRepo = new CustomerRepository();
            bool success = customerRepo.UpdateCustomer(customerToUpdate);

            if (success)
            {
                MessageBox.Show("거래처 정보가 수정되었습니다!");
                LoadCustomersGrid(); // 새로고침
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int customerId;
            if (!int.TryParse(tbSelectedCustomerId.Text, out customerId))
            {
                MessageBox.Show("삭제할 거래처를 선택해주세요!");
                return;
            }

            DialogResult result = MessageBox.Show("이 거래처를 삭제하시겠습니까?\n\n이 거래처와 연결된 REDW 규칙이 있다면 함께 삭제해야 할 수 있습니다.", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                CustomerRepository customerRepo = new CustomerRepository();
                bool success = customerRepo.DeleteCustomer(customerId);

                if (success)
                {
                    MessageBox.Show("거래처가 삭제되었습니다.");
                    LoadCustomersGrid(); // 새로고침
                }
                // FK 오류 처리 필요???
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tbCustomerName.Text = "";
            tbContactPerson.Text = "";
            tbPhoneNumber.Text = "";
            tbAddress.Text = "";
            tbMemo.Text = "";

            FormCustomer_Load(sender, e);
            tbCustomerName.Focus();
        }
    }
}
