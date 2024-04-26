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
    public partial class Alumnos : Form
    {

        ClassNegocio objneg = new ClassNegocio();
        ClassEntidad objent = new ClassEntidad();
        ClassEntidad oAlumnos = new ClassEntidad();
        public Alumnos()
        {
            InitializeComponent();
        }

        private void Alumnos_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = objneg.N_listaralumnos();
        }

        void mantalumno()
        {
            String men = objneg.N_mantenimientoalumno(oAlumnos);
            MessageBox.Show(men, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            oAlumnos.id_alumno = Convert.ToString(dataGridView1.CurrentRow.Cells["idCodigo"].Value);
            oAlumnos.codigo = Convert.ToString(dataGridView1.CurrentRow.Cells["idCodigo"].Value);
            oAlumnos.nombre = Convert.ToString(dataGridView1.CurrentRow.Cells["nombre"].Value);
            oAlumnos.telefono = Convert.ToInt32(dataGridView1.CurrentRow.Cells["telefono"].Value);
            oAlumnos.matricula = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Matricula"].Value);
            oAlumnos.id_curso = Convert.ToString(dataGridView1.CurrentRow.Cells["id_cursos"].Value);
            oAlumnos.id_salon = Convert.ToString(dataGridView1.CurrentRow.Cells["id_salon"].Value);

            if (e.ColumnIndex == 0)
            {
                oAlumnos.accion = "2";
                FrmRegistrarAlumno frmReg = new FrmRegistrarAlumno(oAlumnos, this);
                frmReg.Text = "ACTUALIZAR ALUMNO";
                frmReg.ShowDialog();
            }
            else if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("¿Deseas eliminar este alumno?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                System.Windows.Forms.DialogResult.Yes)
                {
                    oAlumnos.accion = "3";
                    mantalumno();
                    dataGridView1.DataSource = objneg.N_listaralumnos();
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            objent.nombre = textBox8.Text + "%";
            DataTable dt = new DataTable();
            dt = objneg.N_buscaralumnos(objent);
            dataGridView1.DataSource = dt;
        }

        public void recargar()
        {
            dataGridView1.DataSource = objneg.N_listaralumnos();
            dataGridView1.Refresh();

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            oAlumnos = new ClassEntidad();
            FrmRegistrarAlumno frmReg = new FrmRegistrarAlumno(oAlumnos, this);
            frmReg.ShowDialog();
        }
    }
}
