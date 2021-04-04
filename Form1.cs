using System;
using System.Windows.Forms;
using System.Data.SqlClient;
/// <summary>
/// Lenguaje de programacion III
/// Autor: Rene Gonzalez Rodriguez
/// Maestro: Aarón I. Salazar
/// </summary>
namespace Actividad_Integradora_6_Problema_1
{
    public partial class Form1 : Form
    {
        SqlConnection conexion;
        string query = string.Empty;
        public Form1()
        {
            InitializeComponent();
            conexion = new SqlConnection("server=RENEGONZALEZ\\SQLEXPRESS ; database=Personal ; integrated security = true");
            conexion.Open();
            rbtAlta.Focus();
            txtDia.ReadOnly = false;
            txtMes.ReadOnly = false;
            txtAño.ReadOnly = false;
            txtDireccion.ReadOnly = false;
            txtSexo.ReadOnly = false;
            txtEstCivil.ReadOnly = false;
            txtCiudad.ReadOnly = false;
            listEstados.SelectedItem = 0; ;
            txtEmpleado.ReadOnly = false;
            txtHabilidades.ReadOnly = false;
            Limpiar();
        }

        private void rbtAlta_CheckedChanged(object sender, EventArgs e)
        {
            txtDia.ReadOnly = false;
            txtMes.ReadOnly = false;
            txtAño.ReadOnly = false;
            txtDireccion.ReadOnly = false;
            txtSexo.ReadOnly = false;
            txtEstCivil.ReadOnly = false;
            txtCiudad.ReadOnly = false;
            listEstados.SelectedItem = 0; ;
            txtEmpleado.ReadOnly = false;
            txtHabilidades.ReadOnly = false;
            Limpiar();
        }

        private void rbtBaja_CheckedChanged(object sender, EventArgs e)
        {
            txtDia.ReadOnly = true;
            txtMes.ReadOnly = true;
            txtAño.ReadOnly = true;
            txtDireccion.ReadOnly = true;
            txtSexo.ReadOnly = true;
            txtEstCivil.ReadOnly = true;
            txtCiudad.ReadOnly = true;
            listEstados.SelectedItem = 0; ;
            txtEmpleado.ReadOnly = true;
            txtHabilidades.ReadOnly = true;
            Limpiar();
        }

        private void rbtModificacion_CheckedChanged(object sender, EventArgs e)
        {
            txtDia.ReadOnly = false;
            txtMes.ReadOnly = false;
            txtAño.ReadOnly = false;
            txtDireccion.ReadOnly = false;
            txtSexo.ReadOnly = false;
            txtEstCivil.ReadOnly = false;
            txtCiudad.ReadOnly = false;
            listEstados.SelectedItem = 0; ;
            txtEmpleado.ReadOnly = false;
            txtHabilidades.ReadOnly = false;
            Limpiar();
        }
        public void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtDia.Text = string.Empty;
            txtMes.Text = string.Empty;
            txtAño.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtSexo.Text = string.Empty;
            txtEstCivil.Text = string.Empty;
            txtCiudad.Text = string.Empty;
            listEstados.SelectedItem = 0; ;
            txtEmpleado.Text = string.Empty;
            txtHabilidades.Text = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (rbtAlta.Checked)
            {
                query = string.Format("INSERT INTO persona VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                    txtNombre.Text, txtDia.Text, txtMes.Text, txtAño.Text, txtDireccion.Text, txtSexo.Text, txtEstCivil.Text, txtCiudad.Text,
                    listEstados.Text, txtEmpleado.Text, txtHabilidades.Text);
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    string dato = string.Format("Error al realizar la insercion: {0}", ex.Message);
                    MessageBox.Show(dato, "Error");
                }
            }
            if (rbtBaja.Checked)
            {
                query = string.Format("DELETE FROM persona WHERE nombre = '{0}'",
                   txtNombre.Text);
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    string dato = string.Format("Error al realizar la Eliminacion: {0}", ex.Message);
                    MessageBox.Show(dato, "Error");
                }
            }
            if (rbtModificacion.Checked)
            {
                query = string.Format("UPDATE persona SET dianacimiento ='{1}',mesnacimiento = '{2}', añonacimiento= '{3}', direccion ='{4}'," +
                    " sexo = '{5}', estadocivil = '{6}', ciudad ='{7}', estado = '{8}', empleado = '{9}', habilidades ='{10}' WHERE nombre = '{0}'",
                   txtNombre.Text, txtDia.Text, txtMes.Text, txtAño.Text, txtDireccion.Text, txtSexo.Text, txtEstCivil.Text, txtCiudad.Text,
                   listEstados.Text, txtEmpleado.Text, txtHabilidades.Text);
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    string dato = string.Format("Error al realizar la insercion: {0}", ex.Message);
                    MessageBox.Show(dato, "Error");
                }
            }
            MessageBox.Show("Exito", "Mensaje");
            Limpiar();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                query = string.Format("select dianacimiento, mesnacimiento, añonacimiento, direccion, sexo, estadocivil, ciudad, " +
                    "estado, empleado, habilidades  from persona WHERE nombre = '{0}'", txtNombre.Text);
                SqlCommand cmd = new SqlCommand( query, conexion);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtDia.Text = dr["dianacimiento"].ToString();
                    txtMes.Text = dr["mesnacimiento"].ToString();
                    txtAño.Text = dr["añonacimiento"].ToString();
                    txtDireccion.Text = dr["direccion"].ToString();
                    txtSexo.Text = dr["sexo"].ToString();
                    txtEstCivil.Text = dr["estadocivil"].ToString();
                    txtCiudad.Text = dr["ciudad"].ToString();
                    listEstados.Text = dr["estado"].ToString();
                    txtEmpleado.Text = dr["empleado"].ToString();
                    txtHabilidades.Text = dr["habilidades"].ToString();
                }
                dr.Close();
            }
        }
    }
}
