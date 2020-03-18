using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Entidades
{
    public class eCiclo : INotifyPropertyChanged
    {
        public string Periodo { get; set; }

        public int Numero_Cursos { get; set; }

        public float Promedio_Beca { get; set; }

        private float promedio;

        public float Promedio { get { return this.promedio; }
            set
            {
                if (this.promedio != value)
                {
                    this.promedio = value;
                    this.NotifyPropertyChanged("Promedio");
                }
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string nameProperty)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(nameProperty));
            }
        }

        // public List<eCurso> Cursos { get; set; }
        public ObservableCollection<eCurso> Cursos { get; set; }

        public eCiclo()
        {
            Cursos = new ObservableCollection<eCurso>();
            Promedio = 0;
            //Promedio_Beca = 17.0f;
        }

        public int getPesoTotal()
        {
            int Peso = 0;

            foreach (eCurso x in Cursos)
                Peso += x.Creditos;

            return Peso;
        }

        public bool AllCoursesRegistered()
        {
            bool AreRegistered = true;

            foreach(eCurso x in Cursos)
            {
                if (x.getPorcentaje_Completado() != 100)
                {
                    AreRegistered = false;
                    return AreRegistered;
                }
            }

            return AreRegistered;
        }

        public double GetNotaCorrecta()//En observacion
        {
            double Promedio = 0;

            foreach(eCurso x in Cursos)
            {
                double nota_redon = x.Promedio;

                double dec = nota_redon % 1;

                if (dec < 0.5)
                    nota_redon = Math.Round(nota_redon);
                else if (dec >= 0.5)
                    nota_redon = Math.Ceiling(nota_redon);


                Promedio += nota_redon * x.Creditos;
            }

            return Promedio / getPesoTotal();
        }

        public override string ToString()
        {
            return Periodo;
        }
    }
}
