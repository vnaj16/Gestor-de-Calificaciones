using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Entidades;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para UI_CalcularPonderado.xaml
    /// </summary>
    public partial class UI_CalcularPonderado : Window
    {
        public UI_CalcularPonderado()
        {
            InitializeComponent();
            Grid_Pedir_Cantidad_Cursos.Visibility = Visibility.Visible;
            Grid_Calcular_Ponderado.Visibility = Visibility.Hidden;
        }

        private int Cantidad_Cursos { get; set; }

        private void Button_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            Cantidad_Cursos = Convert.ToInt32(ComboBox_CantidadCursos.Text);
            Grid_Pedir_Cantidad_Cursos.Visibility = Visibility.Hidden;

            Generar_Labels();
            Grid_Calcular_Ponderado.Visibility = Visibility.Visible;
        }

        public void Generar_Labels()
        {
            Thickness margin_txtbx = new Thickness(10, 0, 0, 0);
            Thickness margin_lbl = new Thickness(10, 10, 0, 0);
            /*
            <StackPanel x:Name="StackPanel_Cursos" Orientation="Vertical" Margin="10,10,442,10" Background="#FFE8E5E5">
                <Label x:Name="Label_Curso1" Margin="10,10,0,0">
                    <StackPanel Orientation="Horizontal">
                        <TextBlock><Run Text="Curso 1:"/></TextBlock>
                        <TextBox Margin="10,0,0,0"  x:Name="TextBox_Nombre_Curso1" Width="100" Text="Nombre"/>
                        <TextBox Margin="10,0,0,0"  x:Name="Textbox_Creditos_Curso1" Width="60" Text="Creditos"/>
                        <TextBox Margin="10,0,0,0"  x:Name="Textbox_Promedio_Curso1" Width="60" Text="Promedio"/>
                    </StackPanel>
                </Label>
            </StackPanel>
             */

            for (int i = 0; i < Cantidad_Cursos; i++)
            {
                ////ELEMENTOS INTERNOS DE UN STACKPANEL HIJO
                TextBox TextBox_Promedio_Curso = new TextBox
                {
                    Width = 60,
                    Text = "0",
                    Margin = margin_txtbx,
                    IsReadOnly = false
                };
                TextBox TextBox_Creditos_Curso = new TextBox
                {
                    Width = 60,
                    Text = "0",
                    Margin = margin_txtbx,
                    IsReadOnly = false,
                    MaxLength = 1,
                };
                TextBox TextBox_Nombre_Curso = new TextBox
                {
                    Width = 100,
                    Text = "Nombre",
                    Margin = margin_txtbx,
                    IsReadOnly = false
                };

                //AGREGO LAS RESPECTIVAS FUNCIONES A LOS TEXTBOX PARA VALIDAR ENTRADAS
                TextBox_Creditos_Curso.KeyDown += TextBox_Creditos_KeyDown;
                TextBox_Promedio_Curso.KeyDown += TextBox_Promedio_KeyDown;
                TextBlock x = new TextBlock
                {
                    Text = string.Format("Curso {0}:", i + 1)
                };

                ////AGREGO LOS ELEMENTOS AL STACKPANEL HIJO
                StackPanel panelhijo = new StackPanel(); panelhijo.Orientation = Orientation.Horizontal;
                panelhijo.Children.Add(x);
                panelhijo.Children.Add(TextBox_Nombre_Curso);
                panelhijo.Children.Add(TextBox_Creditos_Curso);
                panelhijo.Children.Add(TextBox_Promedio_Curso);//ORDEN: TextBlock TextBox1 TextBox2 TextBox3

                /////AGREGO EL STACKPANEL HIJO AL LABEL, YA QUE ESTE ULTIMO SOLO ACEPTA 1 CONTENT, ENTONCES PARA TENER VARIOS SUB ELEMENTOS LE PASO UN ELEMENTOS QUE TIENE VARIOS
                Label Label_Curso = new Label(); Label_Curso.Margin = margin_lbl;
                Label_Curso.Content = panelhijo;
                ////AGREGO EL LABEL AL STACKPANEL GENERAL
                StackPanel_Cursos.Children.Add(Label_Curso);
                //double A = (((StackPanel_Cursos.Children[0] as Label).Content as StackPanel).Children[1] as TextBox).Width; Obtengo los objetos //VERSION LARGA
               
            }
            //(((StackPanel_Cursos.Children[0] as Label).Content as StackPanel).Children[1] as TextBox).Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool Campos_Completos = true;

            //int variacion = Cantidad_Cursos - 1;
            for (int i = 0; i < Cantidad_Cursos && Campos_Completos; i++)//RECORRO TODOS LOS CAMPOS PARA VER SI ESTAN COMPLETOS  
            {// 0 1 2 3 4     CC = 5     0  4  1 2 1
                for(int j = 1; j < 4; j++)
                {
                    if((((StackPanel_Cursos.Children[i] as Label).Content as StackPanel).Children[j] as TextBox).Text == "")
                    {
                        Campos_Completos = false; break;
                    }

                }
            }

            if (Campos_Completos)//Creo la lista de los cursos
            {
                List<eCurso> Lista_Cursos = new List<eCurso>();
                eCurso Curso;

                //ORDEN: TextBlock TextBox_Nombre TextBox_Creditos TextBox_Promedio
                for (int i = 0; i < Cantidad_Cursos && Campos_Completos; i++)//RECORRO TODOS LOS CAMPOS PARA OBTENER SU INFOR 
                {
                    Curso = new eCurso();
                    Curso.Nombre = (((StackPanel_Cursos.Children[i] as Label).Content as StackPanel).Children[1] as TextBox).Text;
                    Curso.Creditos = Convert.ToInt32((((StackPanel_Cursos.Children[i] as Label).Content as StackPanel).Children[2] as TextBox).Text);
                    Curso.Promedio = Convert.ToSingle((((StackPanel_Cursos.Children[i] as Label).Content as StackPanel).Children[3] as TextBox).Text);

                    Lista_Cursos.Add(Curso);
                }


                //////////////ACTUALIZO Y MUESTRO LOS DATOS
                TextBlock_PP.Text = Calcular_Promedio_Ponderado(Lista_Cursos).ToString("0.##");
            }
            else
            {
                MessageBox.Show("Complete los campos restantes");
            }
        }


        private float Calcular_Promedio_Ponderado(List<eCurso> Lista_Cursos)
        {
            float Total_Puntos = 0;
            int Total_Creditos = 0;
            foreach(eCurso x in Lista_Cursos)
            {
                Total_Creditos += x.Creditos;
                Total_Puntos += (x.Creditos) * (x.Promedio);
            }

            return Total_Puntos / Total_Creditos;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Grid_Calcular_Ponderado.Visibility = Visibility.Hidden;
            //Limpio los Containers
            StackPanel_Cursos.Children.Clear();
            TextBlock_PP.Text = "";

            Grid_Pedir_Cantidad_Cursos.Visibility = Visibility.Visible;
        }

        private void TextBox_Creditos_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (Char.IsDigit(e.Key) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }*/
            if((e.Key >= Key.D0 && e.Key<=Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox_Promedio_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
