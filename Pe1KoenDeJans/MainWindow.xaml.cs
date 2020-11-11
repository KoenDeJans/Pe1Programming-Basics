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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pe1KoenDeJans
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        // initialiseren van de gebruikte variabelen

        const decimal hourlyCostRoom = 7M;
        int refusedReservations;
        int confirmedReservations;
        int waitingListReservations;
        decimal reduction;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        // opvullen combobox met de opgegeven waarden

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbStartHour.Items.Add(8);
            cmbStartHour.Items.Add(9);
            cmbStartHour.Items.Add(10);
            cmbStartHour.Items.Add(11);
            cmbStartHour.Items.Add(12);
            cmbStartHour.Items.Add(13);
            cmbStartHour.Items.Add(14);
            cmbStartHour.Items.Add(15);
            cmbStartHour.Items.Add(16);
            cmbStartHour.Items.Add(17);

            cmbEndHour.Items.Add(8);
            cmbEndHour.Items.Add(9);
            cmbEndHour.Items.Add(10);
            cmbEndHour.Items.Add(11);
            cmbEndHour.Items.Add(12);
            cmbEndHour.Items.Add(13);
            cmbEndHour.Items.Add(14);
            cmbEndHour.Items.Add(15);
            cmbEndHour.Items.Add(16);
            cmbEndHour.Items.Add(17);
        }

        void ErrorMessage()
        {
            MessageBox.Show("Er is een foutje opgetreden bij het ingeven. Gelieve het correcte beginuur en einduur te selecteren");
        }

        //aantal uren berekenen voor reservatie
 
        int CalculatingHours()
        {
            return (Convert.ToInt32(cmbEndHour.SelectedItem) - Convert.ToInt32(cmbStartHour.SelectedItem));

        }

        //berekenen van de totale prijs afgerond 2 cijfers na de komma
     
        decimal CalculatingTotalPrice()
        {
            reduction = decimal.Parse(txtReduction.Text);
            return Math.Round((CalculatingHours() * hourlyCostRoom) - (reduction), 2);
        }

        // Wat er gebeurd als je op de button reserveer klikt
       
        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            if (CalculatingHours() <= 0)
            {
                ErrorMessage();
            }
            else
            {
                waitingListReservations++;
                lblWaitingListReservations.Content = waitingListReservations;
                lstData.Items.Add($"Reservatie van {txtName.Text} van {cmbStartHour.SelectedItem} tot {cmbEndHour.SelectedItem} met €{txtReduction.Text} korting voor €{CalculatingTotalPrice()}");
            }
        }

        // wat er gebeurd als je op de button "Weiger" klikt

        private void btnRefuse_Click(object sender, RoutedEventArgs e)
        {
            if (lstData.SelectedItem != null)
            {
                lstData.Items.RemoveAt(lstData.SelectedIndex);
                refusedReservations++;
                waitingListReservations--;
                lblGeweigerdeReservaties.Content = refusedReservations;
                lblWaitingListReservations.Content = waitingListReservations;
            }
            else
            {
                MessageBox.Show("Gelieve eerst de lijn van de reservatie te selecteren dat U wenst te verplaatsen naar geweigerde reservaties");
            }
        }


        // wat er gebeurd als je op de button "aanvaard" klikt

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (lstData.SelectedItem != null)
            {
                string selectedString = lstData.SelectedItem.ToString();
                lstData.Items.RemoveAt(lstData.SelectedIndex);
                confirmedReservations++;
                waitingListReservations--;
                lblConfirmedReservations.Content = confirmedReservations;
                lblWaitingListReservations.Content = waitingListReservations;
                decimal result = SeperatePrice(selectedString);
                lblTotalAmount.Content = Convert.ToDecimal(lblTotalAmount.Content) + result;
            }
            else
            {
                MessageBox.Show("Gelieve eerst de lijn te selecteren dat U wenst te verplaatsen naar Bevestigde reservaties");
            }
        }

        // deze methode was gegeven bij de opgave, wat het doet is, beginnend van achter, de waarde nemen op de 1ste positie(dus tot aan het euro teken hier in dit geval
        
        decimal SeperatePrice(string selectedString)
        {
            return decimal.Parse(selectedString.Split(" ").Last().Substring(1));
        }

    }
       

    
}
