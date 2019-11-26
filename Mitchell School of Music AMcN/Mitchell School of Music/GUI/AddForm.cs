﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mitchell_School_of_Music.Objects;
using Mitchell_School_of_Music.dbAccess;
using Mitchell_School_of_Music.GUI;

namespace Mitchell_School_of_Music
{
    public partial class frmAddForm : Form
    {
        public frmAddForm(Database db)
        {
            InitializeComponent();
            initialiseInstrumentTypes();
            
            Db = db;
            initAddRentalComboBoxes();
  
        }

        Database Db;

        public void initialiseInstrumentTypes()
        {
            string[] items =
            {"Please Select",
             "Cello",
            "Clarinet",
            "Cymbals",
            "Double Bass",
            "Flute",
            "Oboe",
            "Piccolo",
            "Tenor Drum",
            "Trumpet",
            "Tuba",
            "Viola",
            "Violin"
            };

            for (int i = 0; i < items.Length; i++)
            {
                cbxInstrumentSearch.Items.Add(items[i]);
                cbxInstrument.Items.Add(items[i]);
            }
            cbxInstrumentSearch.SelectedIndex = 0;
            cbxInstrument.SelectedIndex = 0;
        }
        //public void DisplayInstruments(List<Instrument> list)
        //{
        //    DataTable table = new DataTable();

        //    table.Columns.Add("");
        //}

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmAddStudent_Load(object sender, EventArgs e)
        {
           
        }

        private void txtForename_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        private void btnClearFields_Click(object sender, EventArgs e)
        {

        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {   StudentDBAccess a = new StudentDBAccess(Db);
            Student s = new Student();
            bool ok = false;

           try
            {
                
                s.StudentForename = txtForename.Text;
                s.StudentSurname = txtSurname.Text;
                if (rbMale.Checked)
                {
                    s.StudentGender = "M";
                }
                if (rbFemale.Checked)
                {
                    s.StudentGender = "F";
                }
                s.StudentInstrument = cbxInstrument.SelectedItem.ToString();
                if (rbYes.Checked)
                {
                    s.StudentRequiresRental = "Y";
                }
                if (rbNo.Checked)
                {
                    s.StudentRequiresRental = "N";
                }
                s.StudentAddressLine1 = txtAddress.Text;
                s.StudentAddressLine2 = txtAddress2.Text;
                s.StudentCity = txtCity.Text;

                bool postcodeValid = ValidationMethods.validPostcode(txtPostCode, 8, lblPostCode);
                if (postcodeValid == true)
                {
                    ok = true;
                    s.StudentPostCode = txtPostCode.Text;
                    
                }
                else
                {
                    ok = false;
                    s.StudentPostCode = txtPostCode.Text;
                    
                }              

                
                s.StudentPhone = txtPhoneNumber.Text;


                if (ok == true)
                {
                    a.insertStudent(s);
                    MessageBox.Show("Student added", "Success");
                }
                else
                {
                    MessageBox.Show("Student not added", "Error");
                }
                
            }
           catch
           {
               MessageBox.Show("Student not added", "Error");
           }
                   
             
        }
       
        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void cbxInstrumentSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClearInstrument_Click(object sender, EventArgs e)
        {

        }

        private void viewRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new frmAddForm(Db).Show();
        }

        public List<Student> rentingStudents;

        public void initAddRentalComboBoxes()
        {
            cbxStudentID.Items.Clear();
            cbxInstrumentID.Items.Clear();

            StudentDBAccess sDb = new StudentDBAccess(Db);
            

            cbxStudentID.Items.Add("Please select");
            cbxInstrumentID.Items.Add("Please select");

            rentingStudents = new List<Student>();
            rentingStudents = sDb.getStudentByRentalStatus("Y");

            
            foreach (Student s in rentingStudents)
            {
                cbxStudentID.Items.Add(s.StudentID + " - " + s.StudentForename + " " + s.StudentSurname);
            }

        }

        private void cbxStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Student s = new Student();
            s = rentingStudents[cbxStudentID.SelectedIndex];

            InstrumentAccess iDb = new InstrumentAccess(Db);
            List<Instrument> instruments = new List<Instrument>();
            instruments = iDb.getInstrumentsByType(s.StudentInstrument);

            foreach (Instrument i in instruments)
            {
                cbxInstrumentID.Items.Add(i.InstrumentID + " - " + i.InstrumentName);
            }
        }


        private void btnAddRental_Click(object sender, EventArgs e)
        {
            RentalDBAccess B = new RentalDBAccess(Db);
            Rental r = new Rental();
            //try
            //{
               
            r.StudentID = (cbxStudentID.SelectedIndex);
            r.InstrumentID = (cbxInstrumentID.SelectedIndex); ;
            r.DateRented = dtpDateRented.Value;
            r.ReturnDate = dtpReturnDate.Value;
            r.PaymentDate = dtpPaymentDate.Value;
            
           
            B.insertRental(r);
            MessageBox.Show("Student added", "Success");
            
            //else
            //{
            //    MessageBox.Show("Student not added", "Error");
        //    //}
        //}
        //    catch
        //    {
        //        MessageBox.Show("Student not added", "Error");
        //    }


        }

        private void tabViewInstruments_Click(object sender, EventArgs e)
        {

        }

        private void btnAddRepair_Click(object sender, EventArgs e)
        {
            RepairDBAccess C = new RepairDBAccess(Db);
            Repairs re = new Repairs();

            //try
            //{
                re.RentalID = int.Parse(txtRepairRentalID.Text);
                re.RepairDate = dtpRepairDate.Value;
                re.DamageDetails = txtDamageDetails.Text;
                re.RepairCost = double.Parse(txtRepairCost.Text);
                re.RepairPaymentDate = dtpRepairPaymentDate.Value;
            
                C.insertRepair(re);
                MessageBox.Show("Student added", "Success");
            
            //}
            //catch
            //{
            //    MessageBox.Show("Student not added", "Error");
            //}


        }

        private void Reports_Click(object sender, EventArgs e)
        {

        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new frmInstrumentsByStudentsReport().Show();
        }



    }
}
