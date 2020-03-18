using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Entidades
{
    public class eCurso : INotifyPropertyChanged
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public int Creditos { get; set; }

        public int Numero_Campos { get; set; }


        private float promedio;
        public float Promedio { get { return this.promedio; }
            set {
                if (this.promedio != value)
                {
                    this.promedio = value;
                    this.NotifyPropertyChanged("Promedio");
                }
            } }

        public float Promedio_Nuevo { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string nameProperty)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(nameProperty));
            }
        }

        public int Vez { get; set; }
        public eCiclo Ciclo { get; set; }
        //public List<eCampo> Campos { get; set; }
        public ObservableCollection<eCampo> Campos { get; set; }

        public eCurso()
        {
            Ciclo = new eCiclo();
            Campos = new ObservableCollection<eCampo>();
            Promedio = 0;
            Nombre = "";
        }

        public override string ToString()
        {
            return Codigo + "    " + Nombre;
        }

        public float getPorcentaje_Completado()
        {
            float Porcentaje_Completado = 0;

            foreach(eCampo x in Campos)
            {
                if (x.Rellenado == true)
                    Porcentaje_Completado += x.Peso; 
            }

            return Porcentaje_Completado;
        }

        public string ToString(string Format = "")
        {
            if (Format == "")
            {
                return Codigo + ' ' + Nombre;
            }
            else if (Format == "Name")
            {
                return Nombre;
            }
            else if(Format =="ALL")
            {
                return "Nombre: " + Nombre + '\n' +
                    "Codigo: " + Codigo + '\n' +
                    "Creditos: " + Creditos + '\n' +
                    "Numero de campos: " + Numero_Campos + '\n' +
                    "Vez: " + Vez + '\n' +
                    "Ciclo: " + Ciclo.Periodo;
            }
            else
            {
                return Nombre;
            }
        }

    }
}
