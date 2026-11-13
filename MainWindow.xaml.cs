using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using System.Linq;

namespace ContactosApp1
{
    public sealed partial class MainWindow : Window
    {
        private readonly ObservableCollection<Contacto> contactos;

        public MainWindow()
        {
            this.InitializeComponent();
            contactos = new ObservableCollection<Contacto>(JsonService.Cargar());
            ListaContactos.ItemsSource = contactos;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = NombreBox.Text.Trim();
            string correo = CorreoBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MensajeText.Text = "El nombre es obligatorio.";
                return;
            }
            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains('@'))
            {
                MensajeText.Text = "Escribe un correo válido.";
                return;
            }
            if (!int.TryParse(EdadBox.Text, out int edad) || edad < 1 || edad > 120)
            {
                MensajeText.Text = "La edad debe ser un número entero entre 1 y 120.";
                return;
            }

            contactos.Add(new Contacto { Nombre = nombre, Correo = correo, Edad = edad });
            JsonService.Guardar(contactos);

            NombreBox.Text = "";
            CorreoBox.Text = "";
            EdadBox.Text = "";
            MensajeText.Text = "";
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (ListaContactos.SelectedItem is not Contacto seleccionado)
            {
                MensajeText.Text = "Selecciona un contacto de la lista.";
                return;
            }

            contactos.Remove(seleccionado);
            JsonService.Guardar(contactos);
            MensajeText.Text = "";
        }
    }
}
