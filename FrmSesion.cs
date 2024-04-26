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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsAppColegio
{
    public partial class FrmSesion : Form
    {
        ClassEntidad objeuser = new ClassEntidad();
        ClassNegocio objnuser = new ClassNegocio();
        public static string usuario_nombre;
        public static string id_tipo;
        public static string usuario_nick;
        public static string usuario_codigo;
        Principal frm1 = new Principal();
        //FrmPrincipal frm1 = new FrmPrincipal();

        public FrmSesion()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            iniciarSesion();
        }

        private void iniciarSesion()
        {
            DataTable dt = new DataTable();

            objeuser.usuario = txtUsuario.Text;
            objeuser.clave = txtContrasena.Text;

            dt = objnuser.N_login(objeuser);



            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Bienvenido " + dt.Rows[0][0].ToString(), "Mensaje",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                usuario_nombre = dt.Rows[0][0].ToString();
                id_tipo = dt.Rows[0][3].ToString();
                usuario_nick = dt.Rows[0][1].ToString();
                usuario_codigo = dt.Rows[0][4].ToString();
                frm1.ShowDialog();

                txtUsuario.Clear();
                txtContrasena.Clear();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrecta", "Mensaje",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToInt16(Keys.Enter))
            {
                iniciarSesion();
            }
        }
    }
}
