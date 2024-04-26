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
    public partial class FrmRegistrarAlumno : Form
    {
        ClassNegocio objneg = new ClassNegocio();
        ClassEntidad objent = new ClassEntidad();
        Alumnos frmAlumno = new Alumnos();
        public FrmRegistrarAlumno(ClassEntidad objAlumno, Alumnos frmAlu)
        {
            InitializeComponent();
            objent = objAlumno;
            this.frmAlumno = frmAlu;
        }

        private void FrmRegistrarAlumno_Load(object sender, EventArgs e)
        {
            cbocurso.DataSource = objneg.N_listar_curso();
            cbocurso.ValueMember = "id_cursos";
            cbocurso.DisplayMember = "curso_nombre";

            cbosalon.DataSource = objneg.N_listar_salon();
            cbosalon.ValueMember = "id_salon";
            cbosalon.DisplayMember = "salon_nombre";

            cargarRegistro(objent);
        }

        private void cargarRegistro(ClassEntidad objAlumno)
        {
            if (objAlumno.codigo != "" && objAlumno.codigo != null)
            {
                txtcodigo.Text = objAlumno.codigo;
                txtnombre.Text = objAlumno.nombre;
                txttel.Text = Convert.ToString(objAlumno.telefono);
                txtmatricula.Text = Convert.ToString(objAlumno.matricula);
                cbocurso.SelectedValue = objAlumno.id_curso;
                cbosalon.SelectedValue = objAlumno.id_salon;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sMensaje = "¿Deseas registrar a este alumno?";
            string accion = "1";
            if (txtcodigo.Text != "")
            {
                sMensaje = "¿Deseas actualizar a este alumno?";
                accion = "2";
            }
            if (MessageBox.Show(sMensaje, "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                System.Windows.Forms.DialogResult.Yes)
            {
                mantalumno(accion);
                limpiar();
                this.frmAlumno.recargar();
                this.Close();
            }
        }

        private void mantalumno(String accion)
        {
            objent.id_alumno = txtcodigo.Text;
            objent.nombre = txtnombre.Text;
            objent.telefono = Convert.ToInt32(txttel.Text);
            objent.matricula = Convert.ToInt32(txtmatricula.Text);
            objent.id_curso = cbocurso.SelectedValue.ToString();
            objent.id_salon = cbosalon.SelectedValue.ToString();
            objent.accion = accion;
            String men = objneg.N_mantenimientoalumno(objent);
            MessageBox.Show(men, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void limpiar()
        {
            txtcodigo.Text = "";
            txtnombre.Text = "";
            txttel.Text = "";
            txtmatricula.Text = "";
            cbocurso.SelectedIndex = 0;
            cbosalon.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
