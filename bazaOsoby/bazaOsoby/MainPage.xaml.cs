using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using System.Text.RegularExpressions;

namespace bazaOsoby
{
    public partial class MainPage : ContentPage
    {
        protected List<Person> persons;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            persons = await App.Database.GetAllPersonsAsync();

            ListaKontaktow.ItemsSource = persons;
        }

        private async void btnDodaj(object sender, EventArgs e)
        {
            string adressRegex = @"\b[0-9]{2}-[0-9]{3}\b";

            if (!string.IsNullOrEmpty(entryName.Text) && !string.IsNullOrEmpty(entrySurname.Text) && !string.IsNullOrEmpty(entryMail.Text) && !string.IsNullOrEmpty(entryAdres.Text))
            {
                if(Regex.IsMatch(entryAdres.Text, adressRegex))
                {
                    await App.Database.InsertPersonAsync(new Person
                    {
                        Name = entryName.Text,
                        Surname = entrySurname.Text,
                        Mail = entryMail.Text,
                        Adress = entryAdres.Text
                    });

                    entryName.Text = string.Empty;
                    entrySurname.Text = string.Empty;
                    entryMail.Text = string.Empty;
                    entryAdres.Text = string.Empty;

                    persons = await App.Database.GetAllPersonsAsync();

                    ListaKontaktow.ItemsSource = persons;
                }
                else
                {
                    DisplayAlert("Błąd", "Podaj poprawne dane", "OK?");
                }
            }
        }

        private async void btnDelete(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedItem = button.CommandParameter as Person;

            if (selectedItem != null)
            {
                await App.Database.DeletePersonAsync(selectedItem);
            }

            persons = await App.Database.GetAllPersonsAsync();
            ListaKontaktow.ItemsSource = persons;
        }

        private void vcEdit(object sender, EventArgs e)
        {
            var viewCell = sender as ViewCell;
            if (viewCell != null)
            {
                var selectedPerson = viewCell.BindingContext as Person;

                if (selectedPerson != null)
                {
                    Navigation.PushAsync(new EditPage(selectedPerson));
                }
            }
        }
    }
}
