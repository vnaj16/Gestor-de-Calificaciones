using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class eCampo: INotifyPropertyChanged //ESTA INTERFAZ SIRVE PARA REFLEJAR CAMBIOS DE LAS PROPIEDADES EN LA UI, EL OBS. COLLECTION SIRVE PARA REFLEJAR CAMBIOS EN LA LISTA
    {
        public string Tipo { get; set; }
        public int Numero { get; set; }
        public string Descripcion { get; set; }
        public float Peso { get; set; }
        public bool Rellenado { get; set; }

        //PARA ESTA PROPIEDAD IMPLEMENTO LA INTERFAZ PARA REFLEJAR SUS CAMBIOS EN LA UI
        private float nota;
        public float Nota {
            get { return this.nota; }
            set
            {
                if (this.nota != value)
                {
                    this.nota = value;
                    this.NotifyPropertyChanged("Nota");
                }
            }
        }

        public float Nota_Nueva { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string nameProperty)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(nameProperty));
            }
        }



        public eCurso Curso { get; set; }

        public eCampo()
        {
            Curso = new eCurso();
            Nota = 0;
        }

        public override string ToString()
        {
            return Tipo + Numero+ "       " + Descripcion;
        }
    }
}
