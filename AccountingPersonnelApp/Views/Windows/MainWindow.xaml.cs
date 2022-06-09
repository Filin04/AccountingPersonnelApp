﻿using AccountingPersonnelApp.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace AccountingPersonnelApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowVM();
            cbDepartments.SelectedIndex = 0;
            cbPositions.SelectedIndex = 0;
        }
    }
}
