﻿using System;
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
using System.Windows.Shapes;

namespace MailSender2.View
{
    /// <summary>
    /// Логика взаимодействия для ErorrWindow.xaml
    /// </summary>
    public partial class ErorrWindow : Window
    {
        public ErorrWindow()
        {
            InitializeComponent();
        }

        private void BtnErorrOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
