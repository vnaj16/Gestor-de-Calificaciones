using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Entidades;
using Negocio;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ///////Controladores
        private nCiclo Ciclo_Controller = new nCiclo();
        private nCurso Curso_Controller = new nCurso();
        private nCampo Campo_Controller = new nCampo();
        private ViewManager viewManager;
        private List<Grid> List_Grid;

        ///////VARIABLES PARA SECCION VER INFORMACION (VI)
        private eCiclo Ciclo_Seleccionado_VI = new eCiclo();
        private eCurso Curso_Seleccionado_VI = new eCurso();
        private eCampo Campo_Seleccionado_VI = new eCampo();


        ///////VARIABLES PARA SECCION REGISTRAR NOTA (RN)
        private eCiclo Ciclo_Seleccionado_RN = new eCiclo();
        private eCurso Curso_Seleccionado_RN = new eCurso();
        private eCampo Campo_Seleccionado_RN = new eCampo();

        ///////VARIABLES PARA SECCION REGISTRAR CICLO (RC)
        private eCiclo Ciclo_Seleccionado_RC = new eCiclo();
        private eCurso Curso_Seleccionado_RC = new eCurso();
        private eCampo Campo_Seleccionado_RC = new eCampo();

        public MainWindow()
        {
            InitializeComponent();
            viewManager = new ViewManager(Button_Initial: Button_VerInfo);
            viewManager.SetStyles(this.FindResource("Style_Button_Normal") as Style, this.FindResource("Style_Button_Selected") as Style);
            List_Grid = new List<Grid>() { Grid_VerInformacion, Grid_RegistrarNotas, Grid_RegistrarCiclo };//Aca agrego las views(grids) para llevar el control de las views
            
            string Periodo = DateTime.Today.Year + "-0" + (DateTime.Today.Month < 8 ? 1 : 2);//Veo que periodo es el actual

            /////////VER INFORMACION
            /////////////////////////
            ComboBox_Ciclo_VerInfo.ItemsSource = Ciclo_Controller.GetCiclos();
            //Selecciono el ciclo actual
            ComboBox_Ciclo_VerInfo.SelectedItem = (ComboBox_Ciclo_VerInfo.ItemsSource as List<eCiclo>).Find(x => x.Periodo == Periodo);

            /////////REGISTRAR NOTA
            /////////////////////////
            ComboBox_Ciclo_RegistrarNota.ItemsSource = Ciclo_Controller.GetCiclos();
            //Selecciono el ciclo actual
            ComboBox_Ciclo_RegistrarNota.SelectedItem = (ComboBox_Ciclo_RegistrarNota.ItemsSource as List<eCiclo>).Find(x => x.Periodo == Periodo);

            /////////REGISTRAR CICLO
            /////////////////////////
            ComboBox_Ciclos_RegistrarCiclo.ItemsSource = Ciclo_Controller.GetCiclos();
        }

        ///////////CUESTIONES DE DESIGN O ANIMACION///////////
        /////////////////////////////////////////////////////
        ////////////////////////////////////////////////////
        /////////////////////////////////////////////////////
        private void Button_Any_Click(object sender, RoutedEventArgs e)
        {
            viewManager.Change_Button(sender as Button);
            viewManager.Change_View(List_Grid);
            
        }


        ///////////////VER INFORMACION///////////////////////
        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////
        private void ComboBox_Ciclo_VerInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Ciclo_Seleccionado_VI = ComboBox_Ciclo_VerInfo.SelectedItem as eCiclo; //Obtengo los datos del ciclo seleccionado
                                                                                       //Actualizo los datos del ciclo seleccionado
                TextBlock_Ciclo_VerInfo.Text = Ciclo_Seleccionado_VI.Periodo;
                TextBlock_PromedioCiclo_VerInfo.Text = Ciclo_Seleccionado_VI.Promedio.ToString();
                TextBlock_PromedioBeca_VerInfo.Text = Ciclo_Seleccionado_VI.Promedio_Beca.ToString();
                TextBlock_PromedioAcumulado_VerInfo.Text = Ciclo_Controller.getPromedio_Acumulado().ToString("N1");

                Ciclo_Seleccionado_VI.Cursos = Curso_Controller.GetCursos(Ciclo_Seleccionado_VI.Periodo);

                //Relleno los campos de cada curso
                foreach (eCurso x in Ciclo_Seleccionado_VI.Cursos)
                {
                    x.Campos = Campo_Controller.GetCampos_(x.Codigo + "-0" + x.Vez);
                }

                ListBox_Cursos_VerInfo.ItemsSource = Ciclo_Seleccionado_VI.Cursos;//Obtengo y muestro los cursos del ciclo escogido
                ListBox_Cursos_VerInfo.SelectedIndex = 0;

                //Muestra de promedio de ciclo anterior
                int indexCiclo = ComboBox_Ciclo_VerInfo.SelectedIndex;
                if (indexCiclo != 0)
                {
                    TextBlock_PromedioCicloAnterior_VerInfo.Text = (ComboBox_Ciclo_VerInfo.ItemsSource as List<eCiclo>).ElementAt(indexCiclo - 1).Promedio.ToString("N2");
                }
                else
                {
                    TextBlock_PromedioCicloAnterior_VerInfo.Text = "-";
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void ListBox_Cursos_VerInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Curso_Seleccionado_VI = ListBox_Cursos_VerInfo.SelectedItem as eCurso;//Obtengo el curso seleccionado

                //Curso_Seleccionado_VI.Campos = Campo_Controller.GetCampos_(Curso_Seleccionado_VI.Codigo + "-0" + Curso_Seleccionado_VI.Vez);
                DataGrid_Evaluaciones_VerInfo.ItemsSource = Curso_Seleccionado_VI.Campos;

                //Actualizo los datos del curso seleccionado
                TextBlock_NombreCurso_VerInfo.Text = Curso_Seleccionado_VI.Nombre;
                TextBlock_PromedioCurso_VerInfo.Text = Curso_Seleccionado_VI.Promedio.ToString("N2");
                TextBlock_CreditosCurso_VerInfo.Text = Curso_Seleccionado_VI.Creditos.ToString();
                TextBlock_PromedioFinalCurso_VerInfo.Text = Curso_Seleccionado_VI.Promedio.ToString("N0");
                TextBlock_PorcentajeCurso_VerInfo.Text = Curso_Seleccionado_VI.getPorcentaje_Completado().ToString();
                TextBlock_NotaAbsolutaCurso_VerInfo.Text = ((Curso_Seleccionado_VI.getPorcentaje_Completado() / 100.0) * 20).ToString();
                TextBlock_PuntosPerdidosCurso_VerInfo.Text = (((Curso_Seleccionado_VI.getPorcentaje_Completado() / 100.0) * 20) - Curso_Seleccionado_VI.Promedio).ToString("N2");
            }
            catch (Exception ex)
            {

            }
        }

        private void Button_RestablecerNotas_VerInfo_Click(object sender, RoutedEventArgs e)
        {
            int indexCiclo = ComboBox_Ciclo_VerInfo.SelectedIndex;
            int indexCurso = ListBox_Cursos_VerInfo.SelectedIndex;

            ComboBox_Ciclo_VerInfo.ItemsSource = Ciclo_Controller.GetCiclos();
            //Selecciono el ciclo actual
            ComboBox_Ciclo_VerInfo.SelectedIndex = indexCiclo;
            ListBox_Cursos_VerInfo.SelectedIndex = indexCurso;
        }

        private void DataGrid_Evaluaciones_VerInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Campo_Seleccionado_VI = DataGrid_Evaluaciones_VerInfo.SelectedItem as eCampo;
        }

        private void DataGrid_Evaluaciones_VerInfo_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //Obtengo el valor de la celda que es la interseccion de la columna actual y de la fila pasada como parametro
            try
            {
                float Nota_Nueva = Convert.ToSingle((DataGrid_Evaluaciones_VerInfo.Columns[4].GetCellContent(DataGrid_Evaluaciones_VerInfo.SelectedCells[4].Item) as TextBox).Text);

                if (Nota_Nueva >= 0 && Nota_Nueva <= 20)
                {
                    Campo_Seleccionado_VI.Nota_Nueva = Nota_Nueva;

                    Actualizar_Nota(Ciclo_Seleccionado_VI, Curso_Seleccionado_VI, Campo_Seleccionado_VI);

                    Campo_Seleccionado_VI.Rellenado = true;

                    TextBlock_PromedioCurso_VerInfo.Text = Curso_Seleccionado_VI.Promedio.ToString();
                    TextBlock_PromedioFinalCurso_VerInfo.Text = Curso_Seleccionado_VI.Promedio.ToString("N0");
                    TextBlock_PorcentajeCurso_VerInfo.Text = Curso_Seleccionado_VI.getPorcentaje_Completado().ToString();
                    TextBlock_NotaAbsolutaCurso_VerInfo.Text = ((Curso_Seleccionado_VI.getPorcentaje_Completado() / 100.0) * 20).ToString();
                    TextBlock_PuntosPerdidosCurso_VerInfo.Text = (((Curso_Seleccionado_VI.getPorcentaje_Completado() / 100.0) * 20) - Curso_Seleccionado_VI.Promedio).ToString("N2");
                    TextBlock_PromedioCiclo_VerInfo.Text = Ciclo_Seleccionado_VI.Promedio.ToString();
                }
                else
                {
                    MessageBox.Show("Ingrese una nota correcta");
                }
            }
            catch(Exception ex) { MessageBox.Show("Ingrese la nota en un formato correcto"); }
        }

        ///////////////REGISTRAR NOTA////////////////////////
        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////
        private void ComboBox_Ciclo_RegistrarNota_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Ciclo_Seleccionado_RN = ComboBox_Ciclo_RegistrarNota.SelectedItem as eCiclo;
                TextBlock_Ciclo_RegistrarNota.Text = Ciclo_Seleccionado_RN.Periodo;
                TextBlock_PromedioCiclo_RegistrarNota.Text = Ciclo_Seleccionado_RN.Promedio.ToString("N2");

                ComboBox_Curso_RegistrarNota.ItemsSource = Curso_Controller.GetCursos(Ciclo_Seleccionado_RN.Periodo);

                Ciclo_Seleccionado_RN.Cursos = ComboBox_Curso_RegistrarNota.ItemsSource as ObservableCollection<eCurso>;

                foreach (eCurso x in Ciclo_Seleccionado_RN.Cursos)
                {
                    x.Campos = Campo_Controller.GetCampos_(x.Codigo + "-0" + x.Vez);
                }

                ComboBox_Curso_RegistrarNota.SelectedIndex = 0;
            }
            catch (Exception ex) { }
        }

        private void ComboBox_Curso_RegistrarNota_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Curso_Seleccionado_RN = ComboBox_Curso_RegistrarNota.SelectedItem as eCurso;

                //Datos del curso
                TextBlock_NombreCurso_RegistrarNota.Text = Curso_Seleccionado_RN.Nombre;
                TextBlock_PromedioCurso_RegistrarNota.Text = Curso_Seleccionado_RN.Promedio.ToString("N2");
                TextBlock_PromedioFinalCurso_RegistrarNota.Text = Curso_Seleccionado_RN.Promedio.ToString("N0");
                TextBlock_CreditosCurso_RegistrarNota.Text = Curso_Seleccionado_RN.Creditos.ToString();
                TextBlock_PorcentajeCurso_RegistrarNota.Text = Curso_Seleccionado_RN.getPorcentaje_Completado().ToString();
                TextBlock_NotaAbsolutaCurso_RegistrarNota.Text = ((Curso_Seleccionado_RN.getPorcentaje_Completado() / 100.0) * 20).ToString();
                TextBlock_PuntosPerdidosCurso_RegistrarNota.Text = (((Curso_Seleccionado_RN.getPorcentaje_Completado() / 100.0) * 20) - Curso_Seleccionado_RN.Promedio).ToString("N2");



                //Campos
                ComboBox_Evaluaciones_RegistrarNota.ItemsSource = Curso_Seleccionado_RN.Campos;
                DataGrid_Evaluaciones_RegistrarNota.ItemsSource = Curso_Seleccionado_RN.Campos;

                ComboBox_Evaluaciones_RegistrarNota.SelectedIndex = 0;
            }
            catch (Exception ex) { }
        }

        private void ComboBox_Evaluaciones_RegistrarNota_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid_Evaluaciones_RegistrarNota.SelectedItem = ComboBox_Evaluaciones_RegistrarNota.SelectedItem;
            Campo_Seleccionado_RN = ComboBox_Evaluaciones_RegistrarNota.SelectedItem as eCampo;
        }

        private void DataGrid_Evaluaciones_RegistrarNota_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox_Evaluaciones_RegistrarNota.SelectedItem = DataGrid_Evaluaciones_RegistrarNota.SelectedItem;
            Campo_Seleccionado_RN = ComboBox_Evaluaciones_RegistrarNota.SelectedItem as eCampo;
        }

        private void Button_RegistrarNota_RegistrarNota_Click(object sender, RoutedEventArgs e)
        {
            float Nota;
            try
            {
                Nota = Convert.ToSingle(TextBox_NotaEva_RegistrarNota.Text);
            }
            catch (Exception E) { Nota = -1; }

            if (Nota >= 0 && Nota <= 20)
            {
                switch (
           MessageBox.Show(string.Format("Se registrara al campo {0} la siguiente nota: {1} ",
               (ComboBox_Evaluaciones_RegistrarNota.SelectedItem as eCampo).Descripcion, Nota), "Confirmar Registro", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.OK)
               )
                {
                    case MessageBoxResult.OK:

                        Campo_Seleccionado_RN.Nota_Nueva = Nota;

                        Actualizar_Nota(Ciclo_Seleccionado_RN,Curso_Seleccionado_RN,Campo_Seleccionado_RN); //Actualizo los promedios del curso y del ciclo
                        //INGRESAR NOTA A LA BASE DE DATOS
                        /*MessageBox.Show("Ciclo " + Ciclo_Seleccionado_RN.Promedio.ToString());
                        MessageBox.Show("Curso " + Curso_Seleccionado_RN.Promedio.ToString());
                        MessageBox.Show("Campo " + Campo_Seleccionado_RN.Nota.ToString());*/

                        if (
                        Curso_Controller.Actualizar_Nota(Curso_Seleccionado_RN) &&
                        Campo_Controller.Actualizar_Nota(Campo_Seleccionado_RN) &&
                        Ciclo_Controller.Actualizar_Nota(Ciclo_Seleccionado_RN)
                        )//FALTA REGISTRAR EL NUEVO PROMEDIO DEL CICLO
                        {
                            MessageBox.Show("NOTA REGISTRADA");

                            Campo_Seleccionado_RN.Rellenado = true;

                            //Actualizo Datos mostrar
                            TextBlock_PromedioCiclo_RegistrarNota.Text = Ciclo_Seleccionado_RN.Promedio.ToString("N2");
                            TextBlock_PromedioCurso_RegistrarNota.Text = Curso_Seleccionado_RN.Promedio.ToString("N2");
                            TextBlock_PromedioFinalCurso_RegistrarNota.Text = Curso_Seleccionado_RN.Promedio.ToString("N0");
                            TextBlock_PorcentajeCurso_RegistrarNota.Text = Curso_Seleccionado_RN.getPorcentaje_Completado().ToString();
                            TextBlock_NotaAbsolutaCurso_RegistrarNota.Text = ((Curso_Seleccionado_RN.getPorcentaje_Completado() / 100.0) * 20).ToString();
                            TextBlock_PuntosPerdidosCurso_RegistrarNota.Text = (((Curso_Seleccionado_RN.getPorcentaje_Completado() / 100.0) * 20) - Curso_Seleccionado_RN.Promedio).ToString("N2");


                            //Si estan todas las notas del curso ya registradas, calculo el promedio normal
                            if(Curso_Seleccionado_RN.getPorcentaje_Completado() == 100)
                            {
                                if (Ciclo_Seleccionado_RN.AllCoursesRegistered())
                                {
                                    MessageBox.Show("Ya se han registrado todas las notas de cada curso. Se procedera a calcular el nuevo promedio ponderado del ciclo sin margen de error");
                                    double Promedio_Correcto = Ciclo_Seleccionado_RN.GetNotaCorrecta();

                                    Ciclo_Seleccionado_RN.Promedio = Convert.ToSingle(Promedio_Correcto);

                                    TextBlock_PromedioCiclo_RegistrarNota.Text = Promedio_Correcto.ToString("N2");
                                    Ciclo_Controller.Actualizar_Nota(Ciclo_Seleccionado_RN);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("HA OCURRIDO UN ERROR, INTENTE DE NUEVO");
                        }

                        TextBox_NotaEva_RegistrarNota.Clear();
                        break;

                    case MessageBoxResult.Cancel:
                        TextBox_NotaEva_RegistrarNota.Focus();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Ingrese una nota valida");
                TextBox_NotaEva_RegistrarNota.Clear();
                TextBox_NotaEva_RegistrarNota.Focus();
            }


        }

        private void TextBox_NotaEva_RegistrarNota_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                float Nota;
                try
                {
                    Nota = Convert.ToSingle(TextBox_NotaEva_RegistrarNota.Text);
                }
                catch (Exception E) { Nota = -1; }

                if (Nota >= 0 && Nota <= 20)
                {
                    switch (
               MessageBox.Show(string.Format("Se registrara al campo {0} la siguiente nota: {1} ",
                   (ComboBox_Evaluaciones_RegistrarNota.SelectedItem as eCampo).Descripcion, Nota), "Confirmar Registro", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.OK)
                   )
                    {
                        case MessageBoxResult.OK:

                            Campo_Seleccionado_RN.Nota_Nueva = Nota;

                            Actualizar_Nota(Ciclo_Seleccionado_RN, Curso_Seleccionado_RN, Campo_Seleccionado_RN); //Actualizo los promedios del curso y del ciclo
                                                                                                                  //INGRESAR NOTA A LA BASE DE DATOS
                                                                                                                  /*MessageBox.Show("Ciclo " + Ciclo_Seleccionado_RN.Promedio.ToString());
                                                                                                                  MessageBox.Show("Curso " + Curso_Seleccionado_RN.Promedio.ToString());
                                                                                                                  MessageBox.Show("Campo " + Campo_Seleccionado_RN.Nota.ToString());*/

                            if (
                            Curso_Controller.Actualizar_Nota(Curso_Seleccionado_RN) &&
                            Campo_Controller.Actualizar_Nota(Campo_Seleccionado_RN) &&
                            Ciclo_Controller.Actualizar_Nota(Ciclo_Seleccionado_RN)
                            )//FALTA REGISTRAR EL NUEVO PROMEDIO DEL CICLO
                            {
                                MessageBox.Show("NOTA REGISTRADA");

                                Campo_Seleccionado_RN.Rellenado = true;

                                //Actualizo Datos mostrar
                                TextBlock_PromedioCiclo_RegistrarNota.Text = Ciclo_Seleccionado_RN.Promedio.ToString("N2");
                                TextBlock_PromedioCurso_RegistrarNota.Text = Curso_Seleccionado_RN.Promedio.ToString("N2");
                                TextBlock_PromedioFinalCurso_RegistrarNota.Text = Curso_Seleccionado_RN.Promedio.ToString("N0");
                                TextBlock_PorcentajeCurso_RegistrarNota.Text = Curso_Seleccionado_RN.getPorcentaje_Completado().ToString();
                                TextBlock_NotaAbsolutaCurso_RegistrarNota.Text = ((Curso_Seleccionado_RN.getPorcentaje_Completado() / 100.0) * 20).ToString();
                                TextBlock_PuntosPerdidosCurso_RegistrarNota.Text = (((Curso_Seleccionado_RN.getPorcentaje_Completado() / 100.0) * 20) - Curso_Seleccionado_RN.Promedio).ToString("N2");


                                //Si estan todas las notas del curso ya registradas, calculo el promedio normal
                                if (Curso_Seleccionado_RN.getPorcentaje_Completado() == 100)
                                {
                                    if (Ciclo_Seleccionado_RN.AllCoursesRegistered())
                                    {
                                        MessageBox.Show("Ya se han registrado todas las notas de cada curso. Se procedera a calcular el nuevo promedio ponderado del ciclo sin margen de error");
                                        double Promedio_Correcto = Ciclo_Seleccionado_RN.GetNotaCorrecta();

                                        Ciclo_Seleccionado_RN.Promedio = Convert.ToSingle(Promedio_Correcto);

                                        TextBlock_PromedioCiclo_RegistrarNota.Text = Promedio_Correcto.ToString("N2");
                                        Ciclo_Controller.Actualizar_Nota(Ciclo_Seleccionado_RN);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("HA OCURRIDO UN ERROR, INTENTE DE NUEVO");
                            }

                            TextBox_NotaEva_RegistrarNota.Clear();
                            break;

                        case MessageBoxResult.Cancel:
                            TextBox_NotaEva_RegistrarNota.Focus();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese una nota valida");
                    TextBox_NotaEva_RegistrarNota.Clear();
                    TextBox_NotaEva_RegistrarNota.Focus();
                }


            }
        }

        ///////////////REGISTRAR CICLO///////////////////////
        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////


        private void Button_RegistrarCiclo_RegistrarCiclo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Periodo_Ciclo_Nuevo = TextBox_Periodo_RegistrarCiclo.Text;
                int Numero_Cursos_Ciclo_Nuevo = Convert.ToInt32(TextBox_NumCursos_RegistrarCiclo.Text);

                if (isPeriod(Periodo_Ciclo_Nuevo))//Comprobar formato, clase Ciclo_Controller
                {
                    if (Numero_Cursos_Ciclo_Nuevo > 0 && Numero_Cursos_Ciclo_Nuevo<=10) 
                    {
                        string message;
                        eCiclo Ciclo_Nuevo = new eCiclo() { Periodo = Periodo_Ciclo_Nuevo, Numero_Cursos = Numero_Cursos_Ciclo_Nuevo };//Creo el nuevo ciclo para registrar


                        switch (MessageBox.Show(string.Format("Se va a registrar el ciclo {0} en el sistema", Ciclo_Nuevo.Periodo), "Registro de Ciclo",
                            MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.OK))
                        {
                            case MessageBoxResult.OK:

                                if (Ciclo_Controller.Registrar_Ciclo(Ciclo_Nuevo, out message))//Si registro el ciclo de manera exitosa en la base de datos
                                {
                                    ComboBox_Ciclos_RegistrarCiclo.ItemsSource = Ciclo_Controller.GetCiclos().OrderBy(x => x.Periodo);//Actualizo el combobox
                                    ComboBox_Ciclos_RegistrarCiclo.SelectedValue = Ciclo_Nuevo.Periodo;

                                    //Ciclo_Seleccionado_RC = ComboBox_Ciclos_RegistrarCiclo.SelectedItem as eCiclo;

                                    TextBox_Periodo_RegistrarCiclo.Clear();
                                    TextBox_NumCursos_RegistrarCiclo.Clear();
                                    TextBox_NombreCurso_RegistrarCiclo.Focus();

                                    MessageBox.Show(message);
                                }
                                else
                                {
                                    MessageBox.Show(message);
                                }
                                break;
                            case MessageBoxResult.Cancel:
                                TextBox_Periodo_RegistrarCiclo.Focus();
                                break;

                        }


                    }
                    else
                    {
                        MessageBox.Show("Ingrese una cantidad correcta");
                        TextBox_NumCursos_RegistrarCiclo.Clear();
                        TextBox_NumCursos_RegistrarCiclo.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un periodo correcto");
                    TextBox_Periodo_RegistrarCiclo.Clear();
                    TextBox_Periodo_RegistrarCiclo.Focus();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBox_Ciclos_RegistrarCiclo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Ciclo_Seleccionado_RC = ComboBox_Ciclos_RegistrarCiclo.SelectedItem as eCiclo;
                TextBlock_Ciclo_RegistrarCiclo.Text = Ciclo_Seleccionado_RC.Periodo;
                TextBox_PromedioBeca_RegistrarCiclo.Text = Ciclo_Seleccionado_RC.Promedio_Beca.ToString();
                TextBlock_CursosRegistrados_RegistrarCiclo.Text = Ciclo_Controller.GetCursosRegistrados(Ciclo_Seleccionado_RC.Periodo).ToString();
                TextBlock_CursosPorRegistrar_RegistrarCiclo.Text = Ciclo_Seleccionado_RC.Numero_Cursos.ToString();
                TextBlock_Ciclo2_RegistrarCiclo.Text = Ciclo_Seleccionado_RC.Periodo;
                //Agregar una variable booleana para determinar si aun se pueden registrar cursos o campos para bloquear los textbox respectivos
                //MessageBox.Show(isComplete_Ciclo(Ciclo_Seleccionado_RC).ToString());

                Ciclo_Seleccionado_RC.Cursos = Curso_Controller.GetCursos(Ciclo_Seleccionado_RC.Periodo);
                ListBox_Cursos_RegistrarCiclo.ItemsSource = Ciclo_Seleccionado_RC.Cursos;
                ListBox_Cursos_RegistrarCiclo.SelectedIndex = 0;

                if (isComplete_Ciclo(Ciclo_Seleccionado_RC))//si ya estan todos los cursos registrados
                {//Bloqueo la entradas
                    Button_RegistrarCurso_RegistrarCiclo.IsEnabled = false;
                    TextBox_NombreCurso_RegistrarCiclo.IsEnabled = false;
                    TextBox_CodigoCurso_RegistrarCiclo.IsEnabled = false;
                    TextBox_CreditosCurso_RegistrarCiclo.IsEnabled = false;
                    TextBox_EvaluacionesCurso_RegistrarCiclo.IsEnabled = false;
                    ComboBox_NumeroVeces_RegistrarCiclo.IsEnabled = false;

                    MessageBox.Show("Este ciclo ya tiene todos sus cursos registrados","Ciclo Completo");
                }
                else//O si falta un curso por registrar
                {
                    Button_RegistrarCurso_RegistrarCiclo.IsEnabled = true;
                    TextBox_NombreCurso_RegistrarCiclo.IsEnabled = true;
                    TextBox_CodigoCurso_RegistrarCiclo.IsEnabled = true;
                    TextBox_CreditosCurso_RegistrarCiclo.IsEnabled = true;
                    TextBox_EvaluacionesCurso_RegistrarCiclo.IsEnabled = true;
                    ComboBox_NumeroVeces_RegistrarCiclo.IsEnabled = true;
                }
            }
            catch(Exception ex) { }
        }

        private void Button_RegistrarCurso_RegistrarCiclo_Click(object sender, RoutedEventArgs e)
        {
            try
            {//Falta validacion, guiarse de estructura de registro ciclo
                
                string Nombre_Curso_Nuevo = TextBox_NombreCurso_RegistrarCiclo.Text;
                string Codigo_Curso_Nuevo = TextBox_CodigoCurso_RegistrarCiclo.Text;
                int Creditos_Curso_Nuevo = Convert.ToInt32(TextBox_CreditosCurso_RegistrarCiclo.Text);
                int Cantidad_Campos_Curso_Nuevo = Convert.ToInt32(TextBox_EvaluacionesCurso_RegistrarCiclo.Text);
                int Numero_Veces_Curso_Nuevo = Convert.ToInt32(ComboBox_NumeroVeces_RegistrarCiclo.Text);

                if (Nombre_Curso_Nuevo.Length != 0)
                {
                    if (Codigo_Curso_Nuevo.Length >= 4 && Codigo_Curso_Nuevo.Length<=6)
                    {
                        if (Creditos_Curso_Nuevo > 0 && Creditos_Curso_Nuevo <= 10)
                        {
                            if (Cantidad_Campos_Curso_Nuevo > 0 && Cantidad_Campos_Curso_Nuevo <= 20)
                            {

                                eCurso Curso_Nuevo = new eCurso()
                                {
                                    Nombre = Nombre_Curso_Nuevo,
                                    Codigo = Codigo_Curso_Nuevo,
                                    Creditos = Creditos_Curso_Nuevo,
                                    Numero_Campos = Cantidad_Campos_Curso_Nuevo,
                                    Vez = Numero_Veces_Curso_Nuevo,
                                    Ciclo = Ciclo_Seleccionado_RC
                                };

                                switch(MessageBox.Show(string.Format("Se va a registrar el curso {0} - {1} en el sistema", Curso_Nuevo.Nombre, Curso_Nuevo.Codigo), "Registro de Curso",
                                            MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.OK))
                                {
                                    case MessageBoxResult.OK:
                                        string message;
                                        if(Curso_Controller.Registrar_Curso(Curso_Nuevo, out message))//Si se pudo registrar el curso
                                        {
                                            Ciclo_Seleccionado_RC.Cursos.Add(Curso_Nuevo);//Agrego el curso nuevo al ciclo actual

                                            ListBox_Cursos_RegistrarCiclo.ItemsSource = Ciclo_Seleccionado_RC.Cursos;
                                            ListBox_Cursos_RegistrarCiclo.SelectedItem = Curso_Nuevo.ToString();

                                            TextBox_NombreCurso_RegistrarCiclo.Clear();
                                            TextBox_CodigoCurso_RegistrarCiclo.Clear();
                                            TextBox_CreditosCurso_RegistrarCiclo.Clear();
                                            TextBox_EvaluacionesCurso_RegistrarCiclo.Clear();
                                            ComboBox_NumeroVeces_RegistrarCiclo.SelectedIndex = 0;

                                            TextBlock_CursosRegistrados_RegistrarCiclo.Text = Ciclo_Controller.GetCursosRegistrados(Ciclo_Seleccionado_RC.Periodo).ToString();

                                            MessageBox.Show(message);

                                            if(isComplete_Ciclo(Ciclo_Seleccionado_RC))
                                            {
                                                Button_RegistrarCurso_RegistrarCiclo.IsEnabled = false;
                                                TextBox_NombreCurso_RegistrarCiclo.IsEnabled = false;
                                                TextBox_CodigoCurso_RegistrarCiclo.IsEnabled = false;
                                                TextBox_CreditosCurso_RegistrarCiclo.IsEnabled = false;
                                                TextBox_EvaluacionesCurso_RegistrarCiclo.IsEnabled = false;
                                                ComboBox_NumeroVeces_RegistrarCiclo.IsEnabled = false;

                                                MessageBox.Show("Este ciclo ya tiene todos sus cursos registrados", "Ciclo Completo");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(message);
                                        }

                                        break;

                                    case MessageBoxResult.Cancel:
                                        TextBox_NombreCurso_RegistrarCiclo.Focus();
                                        break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ingrese una cantidad de campos validos");
                                TextBox_EvaluacionesCurso_RegistrarCiclo.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ingrese una cantidad de creditos validos");
                            TextBox_CreditosCurso_RegistrarCiclo.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese un codigo valido");
                        TextBox_CodigoCurso_RegistrarCiclo.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un nombre valido");
                    TextBox_NombreCurso_RegistrarCiclo.Focus();
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListBox_Cursos_RegistrarCiclo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Curso_Seleccionado_RC = ListBox_Cursos_RegistrarCiclo.SelectedItem as eCurso;
                Curso_Seleccionado_RC.Campos = Campo_Controller.GetCampos_(Curso_Seleccionado_RC.Codigo + "-0" + Curso_Seleccionado_RC.Vez);

                TextBlock_Curso_RegistrarCiclo.Text = Curso_Seleccionado_RC.Nombre;
                TextBlock_EvasPorRegistrar_RegistrarCiclo.Text = Curso_Seleccionado_RC.Numero_Campos.ToString();
                int Numero_Campos_Registrados;
                int Porcentaje_Campos_Registrados;

                Curso_Controller.GetCamposRegistrados(Curso_Seleccionado_RC, out Numero_Campos_Registrados, out Porcentaje_Campos_Registrados);

                TextBlock_EvasRegistradas_RegistrarCiclo.Text = Numero_Campos_Registrados.ToString();
                TextBlock_PorcentajeRegistrado_RegistrarCiclo.Text = Porcentaje_Campos_Registrados.ToString();

                DataGrid_Campos_RegistrarCiclo.ItemsSource = Curso_Seleccionado_RC.Campos;

                //Verifico si ya estan todos los campos registrados

                if (Porcentaje_Campos_Registrados == 100)//Si ya estan registrados todos los campos
                {
                    ComboBox_NombreEva_RegistrarCiclo.IsEnabled = false;
                    TextBox_PesoEva_RegistrarCiclo.IsEnabled = false;
                    Button_RegistrarEva_RegistrarCiclo.IsEnabled = false;

                    MessageBox.Show("Este curso ya tiene todos sus campos registrados", "Curso Completo");
                }
                else
                {
                    ComboBox_NombreEva_RegistrarCiclo.IsEnabled = true;
                    TextBox_PesoEva_RegistrarCiclo.IsEnabled = true;
                    Button_RegistrarEva_RegistrarCiclo.IsEnabled = true;
                }
            }
            catch (Exception ex) { }
        }

        private void Button_RegistrarEva_RegistrarCiclo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Campo = ComboBox_NombreEva_RegistrarCiclo.Text;
                string Tipo_N = Campo.Substring(0, 2);
                int Numero_N;
                try
                {
                    Numero_N = Convert.ToInt32(Campo.Substring(2, 1));
                }
                catch (Exception E) { Numero_N = 1; }
                string Descripcion_N = Campo.Substring(Campo.IndexOf('-') + 2);

                float Peso_N = Convert.ToSingle(TextBox_PesoEva_RegistrarCiclo.Text);

                //if(Peso_N > 0 && Peso_N < 100)//Comprobar si hay peso disponible para ir agregando, en vez de 100, que sea el espacio disponible
                if (Peso_N > 0 && Peso_N <= Curso_Controller.GetPorcentajePorCompletar(Curso_Seleccionado_RC))
                {
                    eCampo Campo_Nuevo = new eCampo() { Descripcion = Descripcion_N, Tipo = Tipo_N, Numero = Numero_N, Peso = Peso_N };
                    Campo_Nuevo.Curso = Curso_Seleccionado_RC;

                    switch (MessageBox.Show(string.Format("Se va a registrar el campo {0} al curso {1}", Campo, Curso_Seleccionado_RC.ToString()), "Registro de Campo",
                                            MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.OK))
                    {
                        case MessageBoxResult.OK:
                            string message;
                            if (Campo_Controller.Registrar_Campo(Campo_Nuevo,out message))//Si se pudo registrar el curso
                            {
                                Curso_Seleccionado_RC.Campos.Add(Campo_Nuevo); //Agrego el campo al curso seleccionado

                                DataGrid_Campos_RegistrarCiclo.ItemsSource = Curso_Seleccionado_RC.Campos;

                                TextBox_PesoEva_RegistrarCiclo.Clear();

                                int Numero_Campos_Registrados;
                                int Porcentaje_Campos_Registrados;

                                Curso_Controller.GetCamposRegistrados(Curso_Seleccionado_RC, out Numero_Campos_Registrados, out Porcentaje_Campos_Registrados);

                                TextBlock_EvasRegistradas_RegistrarCiclo.Text = Numero_Campos_Registrados.ToString();
                                TextBlock_PorcentajeRegistrado_RegistrarCiclo.Text = Porcentaje_Campos_Registrados.ToString();

                                //Verifico si ya estan todos los campos registrados

                                if (Porcentaje_Campos_Registrados == 100)//Si ya estan registrados todos los campos
                                {
                                    ComboBox_NombreEva_RegistrarCiclo.IsEnabled = false;
                                    TextBox_PesoEva_RegistrarCiclo.IsEnabled = false;
                                    Button_RegistrarEva_RegistrarCiclo.IsEnabled = false;

                                    MessageBox.Show("Este curso ya tiene todos sus campos registrados", "Curso Completo");
                                }
                                else if (Porcentaje_Campos_Registrados > 100)
                                {
                                    //Mostrar mensaje de error y eliminar el ultimo campo registrado
                                }
                                else
                                {
                                    ComboBox_NombreEva_RegistrarCiclo.IsEnabled = true;
                                    TextBox_PesoEva_RegistrarCiclo.IsEnabled = true;
                                    Button_RegistrarEva_RegistrarCiclo.IsEnabled = true;
                                }
                                MessageBox.Show(message);

                                /*if (isComplete_Ciclo(Ciclo_Seleccionado_RC))
                                {
                                    Button_RegistrarCurso_RegistrarCiclo.IsEnabled = false;
                                    TextBox_NombreCurso_RegistrarCiclo.IsEnabled = false;
                                    TextBox_CodigoCurso_RegistrarCiclo.IsEnabled = false;
                                    TextBox_CreditosCurso_RegistrarCiclo.IsEnabled = false;
                                    TextBox_EvaluacionesCurso_RegistrarCiclo.IsEnabled = false;
                                    ComboBox_NumeroVeces_RegistrarCiclo.IsEnabled = false;

                                    MessageBox.Show("Este ciclo ya tiene todos sus cursos registrados", "Ciclo Completo");
                                }*/
                            }
                            else
                            {
                                MessageBox.Show(message);
                            }

                            break;

                        case MessageBoxResult.Cancel:
                            TextBox_PesoEva_RegistrarCiclo.Focus();
                            break;
                    }


                }
                else
                {
                    MessageBox.Show("Ingrese un peso correcto");
                    TextBox_PesoEva_RegistrarCiclo.Focus();
                }

               


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        ///////////////LOGICA GLOBAL////////////////////////
        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////
        private void Actualizar_Nota(eCiclo Ciclo_Selected, eCurso Curso_Selected, eCampo Campo_Selected)//Actualiza las notas de manera local, sin afectar a la base de datos
        {//
         //Para el curso
            if (Campo_Selected.Nota != Campo_Selected.Nota_Nueva)
            {
                Ciclo_Selected.Promedio -= Curso_Selected.Promedio * Curso_Selected.Creditos / Ciclo_Selected.getPesoTotal();

                Curso_Selected.Promedio -= Campo_Selected.Nota * Campo_Selected.Peso / 100;

                Campo_Selected.Nota = Campo_Selected.Nota_Nueva;
                Campo_Selected.Nota_Nueva = 0;
                Curso_Selected.Promedio_Nuevo = Curso_Selected.Promedio + (Campo_Selected.Nota * Campo_Selected.Peso / 100);//El curso ya tiene su nuevo promedio
                Curso_Selected.Promedio = Curso_Selected.Promedio_Nuevo;
                Curso_Selected.Promedio_Nuevo = 0;

                Ciclo_Selected.Promedio += Curso_Selected.Promedio * Curso_Selected.Creditos / Ciclo_Selected.getPesoTotal();
            }
        }

        private bool isComplete_Ciclo(eCiclo Ciclo_Selected)
        {
           return Ciclo_Controller.GetCursosRegistrados(Ciclo_Selected.Periodo) == Ciclo_Selected.Numero_Cursos? true:false;
        }
           
        private bool isPeriod(string Periodo)//2019-02
        {//Falta comprobar el rango de los digitos
            bool isPeriodo = true;
            if (Periodo.Length == 7)
            {
                for (int i = 0; i < Periodo.Length; i++)
                {
                    if (i == 4)
                    {
                        if (Periodo[i] != '-')
                        {
                            isPeriodo = false;
                        }
                    }
                    else
                    {
                        if (!Char.IsDigit(Periodo[i]))
                        {
                            isPeriodo = false;
                        }
                        /*else//Si es digito
                        {
                            if (i == 0 && Periodo[i] != '2')
                            {
                                isPeriodo = false;
                            }
                            if (i == 0 && Periodo[i] != '2')
                            {
                                isPeriodo = false;
                            }
                        }*/
                    }
                }
            }
            else
            {
                isPeriodo = false;
            }
            return isPeriodo;
        }

        private void Button_PromedioBeca_RegistrarCiclo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                float valor = Convert.ToSingle(TextBox_PromedioBeca_RegistrarCiclo.Text);
                if ((valor >= 0 && valor <= 20))
                {
                    string message;
                    Ciclo_Seleccionado_RC.Promedio_Beca = valor;
                    if (Ciclo_Controller.Registrar_PromedioBeca(Ciclo_Seleccionado_RC, out message)) {
                        MessageBox.Show(message);
                    }
                    else
                    {
                        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                }
                else
                {
                    MessageBox.Show("Ingrese un valor correcto");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_CalculadoraPonderado_Click(object sender, RoutedEventArgs e)
        {
            UI_CalcularPonderado CPonderado = new UI_CalcularPonderado();
            CPonderado.Owner = this;
            CPonderado.ShowDialog();
        }
    }
}
