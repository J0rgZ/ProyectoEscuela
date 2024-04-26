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
using System.Runtime.CompilerServices;

namespace WindowsFormsAppColegio
{
    public partial class Cursos : Form
    {
        ClassNegocio objneg = new ClassNegocio();
        ClassEntidad oCursos = new ClassEntidad();
        public Cursos()
        {
            InitializeComponent();
        }

        private void Cursos_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = objneg.N_listar_curso();
        }

        void mantcurso()
        {
            String men = objneg.N_mantenimientocurso(oCursos);
            MessageBox.Show(men, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            oCursos.codigo = Convert.ToString(dataGridView1.CurrentRow.Cells["idCodigo"].Value);
            oCursos.nombre = Convert.ToString(dataGridView1.CurrentRow.Cells["nombre"].Value);
            
            if (e.ColumnIndex == 0)
            {
                oCursos.accion = "2";
                FrmRegistrarCurso frmReg = new FrmRegistrarCurso(oCursos, this);
                frmReg.Text = "ACTUALIZAR CURSO";
                frmReg.ShowDialog();
            }
            else if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("¿Deseas eliminar este curso?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                System.Windows.Forms.DialogResult.Yes)
                {
                    oCursos.accion = "3";
                    mantcurso();
                    dataGridView1.DataSource = objneg.N_listar_curso();
                }
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            oCursos = new ClassEntidad();
            FrmRegistrarCurso frmReg = new FrmRegistrarCurso(oCursos, this);
            frmReg.ShowDialog();
        }

        public void recargar()
        {
            dataGridView1.DataSource = objneg.N_listar_curso();
            dataGridView1.Refresh();

        }
    }
}
