using System;
using System.Windows.Forms;
using Teste;

namespace Teste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int codigo;
        Dal acesso = new Dal();
        private void btnExibir_Click(object sender, EventArgs e)
        {
            try
            {
                atualizarExibicao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void atualizarExibicao()
        {
            dgvFunci.DataSource = acesso.GetTodosRegistros();
        }

        private void dgvFunci_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            codigo = Convert.ToInt32(dgvFunci.Rows[e.RowIndex].Cells[0].Value);
            txtNome.Text = Convert.ToString(dgvFunci.Rows[e.RowIndex].Cells[1].Value);
            txtEmail.Text = Convert.ToString(dgvFunci.Rows[e.RowIndex].Cells[2].Value);
            txtIdade.Text = Convert.ToString(dgvFunci.Rows[e.RowIndex].Cells[3].Value);
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == string.Empty || txtEmail.Text == string.Empty || txtIdade.Text == string.Empty)
            {
                txtNome.Focus();
                return;
            }

            try
            {
                acesso.AtualizarRegistro(codigo, txtEmail.Text, Convert.ToInt32(txtIdade.Text));
                atualizarExibicao();
                Mensagem();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
            btnInserir.Enabled = true;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            if (btnInserir.Text == "Inserir")
            {
                limpaTextBox(this);
                txtNome.Focus();
                btnInserir.Text = "Salvar";
            }
            else
            {
                if (btnInserir.Text == "Salvar")
                {
                    if (txtNome.Text == string.Empty)
                    {
                        btnInserir.Text = "Inserir";
                        return;
                    }
                    acesso.InserirRegistros(txtNome.Text, txtEmail.Text, Convert.ToInt32(txtIdade.Text));
                    btnInserir.Text = "Inserir";
                    atualizarExibicao();
                    Mensagem();
                }
            }
        }
        public void limpaTextBox(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                if (c.HasChildren)
                {
                    limpaTextBox(c);
                }
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == string.Empty)
            {
                return;
            }

            try
            {
                acesso.DeletarRegistro(txtNome.Text);
                atualizarExibicao();
                Mensagem();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void Mensagem()
        {
            MessageBox.Show("Operação realizada com sucesso !");
        }
    }
}
