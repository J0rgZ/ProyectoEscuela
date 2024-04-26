using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppColegio
{
    public partial class FrmRegistrarSalon : Form
    {
        ClassNegocio objneg = new ClassNegocio();
        ClassEntidad objent = new ClassEntidad();
        Salones frmSalon = new Salones();
        public FrmRegistrarSalon(ClassEntidad objSalon, Salones frmSal)
        {
            InitializeComponent();
            objent = objSalon;
            this.frmSalon = frmSal;
        }

        private void FrmRegistrarSalon_Load(object sender, EventArgs e)
        {
            cargarRegistro(objent);
        }

        private void cargarRegistro(ClassEntidad objSalon)
        {
            if (objSalon.codigo != "" && objSalon.codigo != null)
            {
                txtcodigo.Text = objSalon.codigo;
                txtnombre.Text = objSalon.nombre;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sMensaje = "¿Deseas registrar este salon?";
            string accion = "1";
            if (txtcodigo.Text != "")
            {
                sMensaje = "¿Deseas actualizar este salon?";
                accion = "2";
            }
            if (MessageBox.Show(sMensaje, "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                System.Windows.Forms.DialogResult.Yes)
            {
                mantsalon(accion);
                limpiar();
                this.frmSalon.recargar();
                this.Close();
            }
        }

        private void mantsalon(String accion)
        {
            objent.codigo = txtcodigo.Text;
            objent.nombre = txtnombre.Text;
            objent.accion = accion;
            String men = objneg.N_mantenimientosalon(objent);
            MessageBox.Show(men, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void limpiar()
        {
            txtcodigo.Text = "";
            txtnombre.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
