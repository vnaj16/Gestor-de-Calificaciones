using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion
{
    public class ViewManager
    {//El nombre del button y del grid deben sen iguales
            //ej: Button_RegistrarNota; Grid_RegistrarNota
        Grid Grid_Selected = new Grid();//Grid seleccionada actualmente
        Grid Grid_PreSelected = new Grid(); //Grid seleccionada anteriormente

        Button Button_Selected;//Button seleccionado actualmente
        Button Button_PreSelected = new Button();//Button seleccionado anteriormente

        Style Style_Button_Normal = new Style();//Style para el button no seleccionado
        Style Style_Button_Selected = new Style();//Style para el button seleccionado

        public ViewManager(Button Button_Initial)
        {
            Button_Selected = Button_Initial;
        }

        public void SetStyles(Style Style_Button_Normal, Style Style_Button_Selected)
        {
            this.Style_Button_Normal = Style_Button_Normal;
            this.Style_Button_Selected = Style_Button_Selected;
        }

        public void Change_View(List<Grid> List_Grid)
        {
            int index1 = Button_PreSelected.Name.IndexOf('_');
            int index2 = Button_Selected.Name.IndexOf('_');

            string Name_Button_Preselected = Button_PreSelected.Name.Substring(index1 + 1);
            string Name_Button_Selected = Button_Selected.Name.Substring(index2 + 1);

            Grid_PreSelected = List_Grid.Find(x => x.Name.Contains(Name_Button_Preselected));
            Grid_Selected = List_Grid.Find(x => x.Name.Contains(Name_Button_Selected));

            try
            {
                Grid_PreSelected.Visibility = Visibility.Hidden;
                Grid_Selected.Visibility = Visibility.Visible;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Change_Button(Button Button_Selected)
        {
            this.Button_PreSelected = this.Button_Selected;
            this.Button_Selected = Button_Selected;

            Buttons_Change_Color();
        }

        void Buttons_Change_Color()
        {
            Button_PreSelected.Style = Style_Button_Normal;

            Button_Selected.Style = Style_Button_Selected;
        }
    }
}
