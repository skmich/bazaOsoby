using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bazaOsoby
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        public EditPage(Person osoba)
        {
            InitializeComponent();

            lblId.Text = osoba.Id.ToString();
            entryName.Text = osoba.Name;
            entrySurname.Text = osoba.Surname;
            entryMail.Text = osoba.Mail;
            entryAdres.Text = osoba.Adress;
        }

        private void btnEdit(object sender, EventArgs e)
        {
            string adressRegex = @".*\b\d{2}-\d{3}\b.*";

            if(!string.IsNullOrEmpty(entryName.Text) && !string.IsNullOrEmpty(entrySurname.Text) && !string.IsNullOrEmpty(entryMail.Text) && !string.IsNullOrEmpty(entryAdres.Text))
            {
                if(Regex.IsMatch(entryAdres.Text, adressRegex))
                {
                    Person person = new Person { 
                        Id = int.Parse(lblId.Text), 
                        Name = entryName.Text, 
                        Surname = entrySurname.Text, 
                        Mail = entryMail.Text, 
                        Adress = entryAdres.Text };

                    App.Database.UpdatePersonAsync(person);
                    Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Błąd", "Podane wartości są niepoprawne", "OK");
                }
            }
            else
            {
                DisplayAlert("Błąd", "Uzupełnij wszystkie wartości", "OK");
            }
        }
    }
}