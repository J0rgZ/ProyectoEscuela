using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Negocio;
using Capa_Entidad;

namespace WindowsFormsAppColegio
{
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        ClassNegocio objneg = new ClassNegocio();
        ClassEntidad objent = new ClassEntidad();
        ClassEntidad oUsuario = new ClassEntidad();
        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = objneg.N_listar_usuarios();
        }


        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            objent.nombre = textBox8.Text + "%";
            DataTable dt = new DataTable();
            dt = objneg.N_buscarusuario(objent);
            dataGridView1.DataSource = dt;
        }

        private void mantusuario()
        {
            String men = objneg.N_mantenimientousuario(oUsuario);
            MessageBox.Show(men, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            oUsuario.codigo = Convert.ToString(dataGridView1.CurrentRow.Cells["idCodigo"].Value);
            oUsuario.nombre = Convert.ToString(dataGridView1.CurrentRow.Cells["nombre"].Value);
            oUsuario.usuario = Convert.ToString(dataGridView1.CurrentRow.Cells["usuario"].Value);
            oUsuario.id_tipo = Convert.ToString(dataGridView1.CurrentRow.Cells["idTipo"].Value);
            if (e.ColumnIndex == 0)
            {
                oUsuario.accion = "2";
                FrmRegistrarUsuario frmReg = new FrmRegistrarUsuario(oUsuario, this);
                frmReg.Text = "ACTUALIZAR USUARIO";
                frmReg.ShowDialog();
            }
            else if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("¿Deseas eliminar a este usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                System.Windows.Forms.DialogResult.Yes)
                {
                    oUsuario.accion = "3";
                    mantusuario();
                    dataGridView1.DataSource = objneg.N_listar_usuarios();
                }
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            oUsuario = new ClassEntidad();
            FrmRegistrarUsuario frmReg = new FrmRegistrarUsuario(oUsuario, this);
            frmReg.ShowDialog();
        }

        public void recargar()
        {
            dataGridView1.DataSource = objneg.N_listar_usuarios();
            dataGridView1.Refresh();
            
        }
    }
}
