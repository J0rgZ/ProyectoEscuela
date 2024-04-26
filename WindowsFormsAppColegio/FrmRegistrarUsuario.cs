using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsAppColegio
{
    public partial class FrmRegistrarUsuario : Form
    {
        ClassEntidad objent = new ClassEntidad();
        ClassNegocio objneg = new ClassNegocio();
        Usuarios frmUsuario = new Usuarios();
        public FrmRegistrarUsuario(ClassEntidad objUsuario, Usuarios frmUsu)
        {
            InitializeComponent();
            objent = objUsuario;
            this.frmUsuario = frmUsu;
        }

        private void FrmRegistrarUsuario_Load(object sender, EventArgs e)
        {
            cbotipo.DataSource = objneg.N_listar_tipo();
            cbotipo.ValueMember = "id_tipo";
            cbotipo.DisplayMember = "tipo_nombre";
            cargarRegistro(objent);
        }

        private void cargarRegistro(ClassEntidad objUsuario)
        {
            if(objUsuario.codigo != "" && objUsuario.codigo != null)
            {
                txtcodigo.Text = objUsuario.codigo;
                txtusuario.Text = objUsuario.usuario;
                txtnombre.Text = objUsuario.nombre;
                cbotipo.SelectedValue = objUsuario.id_tipo;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sMensaje = "¿Deseas registrar a este usuario?";
            string accion = "1";
            if (txtcodigo.Text != "")
            {
                sMensaje = "¿Deseas actualizar a este usuario?";
                accion = "2";
            }
            if (MessageBox.Show(sMensaje, "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                System.Windows.Forms.DialogResult.Yes)
            {
                mantusuario(accion);
                limpiar();
                this.frmUsuario.recargar();
                this.Close();
            }

        }

        private void mantusuario(String accion)
        {

            objent.codigo = txtcodigo.Text;
            objent.nombre = txtnombre.Text;
            objent.usuario = txtusuario.Text;
            objent.id_tipo = cbotipo.SelectedValue.ToString();
            objent.accion = accion;
            String men = objneg.N_mantenimientousuario(objent);
            MessageBox.Show(men, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void limpiar()
        {
            txtcodigo.Text = "";
            txtusuario.Text = "";
            txtnombre.Text = "";
            cbotipo.SelectedIndex = 0;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
