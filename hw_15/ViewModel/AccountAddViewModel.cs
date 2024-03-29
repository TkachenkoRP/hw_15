﻿using System.Windows;
using BankLibrary.BankAccount;
using hw_15.Command;

namespace hw_15.ViewModel
{
    class AccountAddViewModel : ApplicationViewModel
    {
        private BankRegularAccount account;
        private string amount;
        private string period;
        private string percent;
        private string errorMessage;

        private Visibility _periodVisibility;
        private Visibility _percentVisibility;
        private Visibility _capitalizationVisibility;


        public AccountAddViewModel(BankRegularAccount account)
        {
            this.account = account;
            PeriodVisibility = Visibility.Hidden;
            PercentVisibility = Visibility.Hidden;
            CapitalizationVisibility = Visibility.Hidden;
        }

        public string Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

        public string Period
        {
            get => period;
            set
            {
                period = value;
                OnPropertyChanged();
            }
        }

        public string Percent
        {
            get => percent;
            set {

                percent = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged();
            }
        }

        public Visibility PeriodVisibility
        {
            get => _periodVisibility;
            set
            {
                _periodVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility PercentVisibility
        {
            get => _percentVisibility;
            set
            {
                _percentVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility CapitalizationVisibility
        {
            get => _capitalizationVisibility;
            set
            {
                _capitalizationVisibility = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                       (saveCommand = new RelayCommand(obj =>
                           {

                               double percent = default;
                               int period = default;

                               if (!decimal.TryParse(Amount.Replace(" ", ""), out decimal amount) || amount < 100)
                               {
                                   ErrorMessage = "Введена неверная сумма! Сумма должна быть от 100!";
                                   return;
                               }

                               if (PercentVisibility == Visibility.Visible)
                               {
                                   if (string.IsNullOrEmpty(Percent))
                                   {
                                       ErrorMessage = "Введите процент!";
                                       return;
                                   }
                                   if (!double.TryParse(Percent.Replace('.', ','), out percent))
                                   {
                                       ErrorMessage = "Введен неверный процент!";
                                       return;
                                   }
                               }

                               if (PeriodVisibility == Visibility.Visible)
                               {
                                   if (string.IsNullOrEmpty(Period))
                                   {
                                       ErrorMessage = "Введите срок!";
                                       return;
                                   }
                                   if (!int.TryParse(Period.Replace(" ", ""), out period) || period < 12 || period > 62)
                                   {
                                       ErrorMessage = "Введен неверный срок кредитования! От 12 до 60 месяцев!";
                                       return;
                                   }
                               }

                               account.Amount = amount;

                               Window window = obj as Window;
                               window.DialogResult = true;
                               window.Close();
                           },
                           obj => !string.IsNullOrEmpty(Amount)));
            }
        }
    }
}
