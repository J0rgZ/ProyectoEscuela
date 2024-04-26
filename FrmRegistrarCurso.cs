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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using Capa_Entidad;
using Capa_Negocio;

namespace WindowsFormsAppColegio
{
    public partial class FrmRegistrarCurso : Form
    {
        ClassNegocio objneg = new ClassNegocio();
        ClassEntidad objent = new ClassEntidad();
        Cursos frmCurso = new Cursos();
        public FrmRegistrarCurso(ClassEntidad objCurso, Cursos frmCur)
        {
            InitializeComponent();
            objent = objCurso;
            this.frmCurso = frmCur;
        }

        private void FrmRegistrarCurso_Load(object sender, EventArgs e)
        {
            cargarRegistro(objent);
        }

        private void cargarRegistro(ClassEntidad objCurso)
        {
            if (objCurso.codigo != "" && objCurso.codigo != null)
            {
                txtCodigo.Text = objCurso.codigo;
                txtNombre.Text = objCurso.nombre;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sMensaje = "¿Deseas registrar este curso?";
            string accion = "1";
            if (txtCodigo.Text != "")
            {
                sMensaje = "¿Deseas actualizar este curso?";
                accion = "2";
            }
            if (MessageBox.Show(sMensaje, "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                System.Windows.Forms.DialogResult.Yes)
            {
                mantcurso(accion);
                limpiar();
                this.frmCurso.recargar();
                this.Close();
            }
        }

        private void mantcurso(String accion)
        {
            objent.codigo = txtCodigo.Text;
            objent.nombre = txtNombre.Text;
            objent.accion = accion;
            String men = objneg.N_mantenimientocurso(objent);
            MessageBox.Show(men, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void limpiar()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
