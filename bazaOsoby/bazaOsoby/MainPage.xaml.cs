using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;

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
            if(!string.IsNullOrEmpty(entryName.Text) && !string.IsNullOrEmpty(entrySurname.Text) && !string.IsNullOrEmpty(entryMail.Text) && !string.IsNullOrEmpty(entryAdres.Text))
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
            var listItem = sender as MenuItem;
            var selectedPerson = listItem?.CommandParameter as Person;

            if(selectedPerson != null)
            {
                Navigation.PushAsync(new EditPage(selectedPerson));
            }
        }
    }
}
