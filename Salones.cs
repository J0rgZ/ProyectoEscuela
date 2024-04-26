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
    public partial class Salones : Form
    {
        ClassNegocio objneg = new ClassNegocio();
        ClassEntidad objent = new ClassEntidad();
        ClassEntidad oSalon = new ClassEntidad();
        public Salones()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mantsalon()
        {
            String men = objneg.N_mantenimientosalon(oSalon);
            MessageBox.Show(men, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Salones_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = objneg.N_listar_salon();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            oSalon.codigo = Convert.ToString(dataGridView1.CurrentRow.Cells["idCodigo"].Value);
            oSalon.nombre = Convert.ToString(dataGridView1.CurrentRow.Cells["nombre"].Value);

            if (e.ColumnIndex == 0)
            {
                oSalon.accion = "2";
                FrmRegistrarSalon frmReg = new FrmRegistrarSalon(oSalon, this);
                frmReg.Text = "ACTUALIZAR SALON";
                frmReg.ShowDialog();
            }
            else if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("¿Deseas eliminar este salon?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                System.Windows.Forms.DialogResult.Yes)
                {
                    oSalon.accion = "3";
                    mantsalon();
                    dataGridView1.DataSource = objneg.N_listar_salon();
                }
            }
        }

        public void recargar()
        {
            dataGridView1.DataSource = objneg.N_listar_salon();
            dataGridView1.Refresh();

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            oSalon = new ClassEntidad();
            FrmRegistrarSalon frmReg = new FrmRegistrarSalon(oSalon, this);
            frmReg.ShowDialog();
        }
    }
}
